using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Connection;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet
{
    /// <summary>
    ///     Represents SSH command that can be executed.
    /// </summary>
    public partial class SshCommand : IDisposable
    {
        private readonly object _endExecuteLock = new object();
        private readonly Session _session;
        private CommandAsyncResult _asyncResult;
        private AsyncCallback _callback;
        private ChannelSession _channel;
        private StringBuilder _error;
        private Exception _exception;
        private bool _hasError;
        private StringBuilder _result;
        private EventWaitHandle _sessionErrorOccuredWaitHandle = new AutoResetEvent(false);

        /// <summary>
        ///     Initializes a new instance of the <see cref="SshCommand" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="encoding">The encoding.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="session" />, <paramref name="commandText" /> or
        ///     <paramref name="encoding" /> is null.
        /// </exception>
        internal SshCommand(Session session, string commandText)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            if (commandText == null)
                throw new ArgumentNullException("commandText");

            _session = session;
            CommandText = commandText;
            CommandTimeout = new TimeSpan(0, 0, 0, 0, -1);

            _session.Disconnected += Session_Disconnected;
            _session.ErrorOccured += Session_ErrorOccured;
        }

        /// <summary>
        ///     Gets the command text.
        /// </summary>
        public string CommandText { get; private set; }

        /// <summary>
        ///     Gets or sets the command timeout.
        /// </summary>
        /// <value>
        ///     The command timeout.
        /// </value>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs"
        ///         region="Example SshCommand CreateCommand Execute CommandTimeout" language="C#"
        ///         title="Specify command execution timeout" />
        /// </example>
        public TimeSpan CommandTimeout { get; set; }

        /// <summary>
        ///     Gets the command exit status.
        /// </summary>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs" region="Example SshCommand RunCommand ExitStatus"
        ///         language="C#" title="Get command execution exit status" />
        /// </example>
        public int ExitStatus { get; private set; }

        /// <summary>
        ///     Gets the output stream.
        /// </summary>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs"
        ///         region="Example SshCommand CreateCommand Execute OutputStream" language="C#"
        ///         title="Use OutputStream to get command execution output" />
        /// </example>
        public Stream OutputStream { get; private set; }

        /// <summary>
        ///     Gets the extended output stream.
        /// </summary>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs"
        ///         region="Example SshCommand CreateCommand Execute ExtendedOutputStream" language="C#"
        ///         title="Use ExtendedOutputStream to get command debug execution output" />
        /// </example>
        public Stream ExtendedOutputStream { get; private set; }

        /// <summary>
        ///     Gets the command execution result.
        /// </summary>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs" region="Example SshCommand RunCommand Result"
        ///         language="C#" title="Running simple command" />
        /// </example>
        public string Result
        {
            get
            {
                if (_result == null)
                {
                    _result = new StringBuilder();
                }

                if (OutputStream != null && OutputStream.Length > 0)
                {
                    using (var sr = new StreamReader(OutputStream, _session.ConnectionInfo.Encoding))
                    {
                        _result.Append(sr.ReadToEnd());
                    }
                }

                return _result.ToString();
            }
        }

        /// <summary>
        ///     Gets the command execution error.
        /// </summary>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs" region="Example SshCommand CreateCommand Error"
        ///         language="C#" title="Display command execution error" />
        /// </example>
        public string Error
        {
            get
            {
                if (_hasError)
                {
                    if (_error == null)
                    {
                        _error = new StringBuilder();
                    }

                    if (ExtendedOutputStream != null && ExtendedOutputStream.Length > 0)
                    {
                        using (var sr = new StreamReader(ExtendedOutputStream, _session.ConnectionInfo.Encoding))
                        {
                            _error.Append(sr.ReadToEnd());
                        }
                    }

                    return _error.ToString();
                }
                return string.Empty;
            }
        }

        /// <summary>
        ///     Begins an asynchronous command execution.
        /// </summary>
        /// <returns>
        ///     An <see cref="System.IAsyncResult" /> that represents the asynchronous command execution, which could still be
        ///     pending.
        /// </returns>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs"
        ///         region="Example SshCommand CreateCommand BeginExecute IsCompleted EndExecute" language="C#"
        ///         title="Asynchronous Command Execution" />
        /// </example>
        /// <exception cref="System.InvalidOperationException">Asynchronous operation is already in progress.</exception>
        /// <exception cref="SshException">Invalid operation.</exception>
        /// <exception cref="System.ArgumentException">CommandText property is empty.</exception>
        /// <exception cref="Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="Renci.SshNet.Common.SshOperationTimeoutException">Operation has timed out.</exception>
        /// <exception cref="InvalidOperationException">Asynchronous operation is already in progress.</exception>
        /// <exception cref="ArgumentException">CommandText property is empty.</exception>
        public IAsyncResult BeginExecute()
        {
            return BeginExecute(null, null);
        }

        /// <summary>
        ///     Begins an asynchronous command execution.
        /// </summary>
        /// <param name="callback">An optional asynchronous callback, to be called when the command execution is complete.</param>
        /// <returns>
        ///     An <see cref="System.IAsyncResult" /> that represents the asynchronous command execution, which could still be
        ///     pending.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Asynchronous operation is already in progress.</exception>
        /// <exception cref="SshException">Invalid operation.</exception>
        /// <exception cref="System.ArgumentException">CommandText property is empty.</exception>
        /// <exception cref="Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="Renci.SshNet.Common.SshOperationTimeoutException">Operation has timed out.</exception>
        /// <exception cref="InvalidOperationException">Asynchronous operation is already in progress.</exception>
        /// <exception cref="ArgumentException">CommandText property is empty.</exception>
        public IAsyncResult BeginExecute(AsyncCallback callback)
        {
            return BeginExecute(callback, null);
        }

        /// <summary>
        ///     Begins an asynchronous command execution.
        /// </summary>
        /// <param name="callback">An optional asynchronous callback, to be called when the command execution is complete.</param>
        /// <param name="state">
        ///     A user-provided object that distinguishes this particular asynchronous read request from other
        ///     requests.
        /// </param>
        /// <returns>
        ///     An <see cref="System.IAsyncResult" /> that represents the asynchronous command execution, which could still be
        ///     pending.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Asynchronous operation is already in progress.</exception>
        /// <exception cref="SshException">Invalid operation.</exception>
        /// <exception cref="System.ArgumentException">CommandText property is empty.</exception>
        /// <exception cref="Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="Renci.SshNet.Common.SshOperationTimeoutException">Operation has timed out.</exception>
        /// <exception cref="InvalidOperationException">Asynchronous operation is already in progress.</exception>
        /// <exception cref="ArgumentException">CommandText property is empty.</exception>
        public IAsyncResult BeginExecute(AsyncCallback callback, object state)
        {
            //  Prevent from executing BeginExecute before calling EndExecute
            if (_asyncResult != null)
            {
                throw new InvalidOperationException("Asynchronous operation is already in progress.");
            }

            //  Create new AsyncResult object
            _asyncResult = new CommandAsyncResult(this)
            {
                AsyncWaitHandle = new ManualResetEvent(false),
                IsCompleted = false,
                AsyncState = state
            };

            //  When command re-executed again, create a new channel
            if (_channel != null)
            {
                throw new SshException("Invalid operation.");
            }

            CreateChannel();

            if (string.IsNullOrEmpty(CommandText))
                throw new ArgumentException("CommandText property is empty.");

            _callback = callback;

            _channel.Open();

            //  Send channel command request
            _channel.SendExecRequest(CommandText);

            return _asyncResult;
        }

        /// <summary>
        ///     Begins an asynchronous command execution. 22
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="callback">An optional asynchronous callback, to be called when the command execution is complete.</param>
        /// <param name="state">
        ///     A user-provided object that distinguishes this particular asynchronous read request from other
        ///     requests.
        /// </param>
        /// <returns>
        ///     An <see cref="System.IAsyncResult" /> that represents the asynchronous command execution, which could still be
        ///     pending.
        /// </returns>
        /// <exception cref="Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="Renci.SshNet.Common.SshOperationTimeoutException">Operation has timed out.</exception>
        public IAsyncResult BeginExecute(string commandText, AsyncCallback callback, object state)
        {
            CommandText = commandText;

            return BeginExecute(callback, state);
        }

        /// <summary>
        ///     Waits for the pending asynchronous command execution to complete.
        /// </summary>
        /// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
        /// <returns>Command execution result.</returns>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs"
        ///         region="Example SshCommand CreateCommand BeginExecute IsCompleted EndExecute" language="C#"
        ///         title="Asynchronous Command Execution" />
        /// </example>
        /// <exception cref="System.ArgumentException">
        ///     Either the IAsyncResult object did not come from the corresponding async
        ///     method on this type, or EndExecute was called multiple times with the same IAsyncResult.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Either the IAsyncResult object did not come from the corresponding async method on
        ///     this type, or EndExecute was called multiple times with the same IAsyncResult.
        /// </exception>
        public string EndExecute(IAsyncResult asyncResult)
        {
            if (_asyncResult == asyncResult && _asyncResult != null)
            {
                lock (_endExecuteLock)
                {
                    if (_asyncResult != null)
                    {
                        //  Make sure that operation completed if not wait for it to finish
                        WaitHandle(_asyncResult.AsyncWaitHandle);

                        if (_channel.IsOpen)
                        {
                            _channel.SendEof();

                            _channel.Close();
                        }

                        _channel = null;

                        _asyncResult = null;

                        return Result;
                    }
                }
            }

            throw new ArgumentException(
                "Either the IAsyncResult object did not come from the corresponding async method on this type, or EndExecute was called multiple times with the same IAsyncResult.");
        }

        /// <summary>
        ///     Executes command specified by <see cref="CommandText" /> property.
        /// </summary>
        /// <returns>Command execution result</returns>
        /// <example>
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs" region="Example SshCommand CreateCommand Execute"
        ///         language="C#" title="Simple command execution" />
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs" region="Example SshCommand CreateCommand Error"
        ///         language="C#" title="Display command execution error" />
        ///     <code source="..\..\Renci.SshNet.Tests\Classes\SshCommandTest.cs"
        ///         region="Example SshCommand CreateCommand Execute CommandTimeout" language="C#"
        ///         title="Specify command execution timeout" />
        /// </example>
        /// <exception cref="Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="Renci.SshNet.Common.SshOperationTimeoutException">Operation has timed out.</exception>
        public string Execute()
        {
            return EndExecute(BeginExecute(null, null));
        }

        /// <summary>
        ///     Cancels command execution in asynchronous scenarios.
        /// </summary>
        public void CancelAsync()
        {
            if (_channel != null && _channel.IsOpen && _asyncResult != null)
            {
                _channel.Close();
            }
        }

        /// <summary>
        ///     Executes the specified command text.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns>Command execution result</returns>
        /// <exception cref="Renci.SshNet.Common.SshConnectionException">Client is not connected.</exception>
        /// <exception cref="Renci.SshNet.Common.SshOperationTimeoutException">Operation has timed out.</exception>
        public string Execute(string commandText)
        {
            CommandText = commandText;

            return Execute();
        }

        private void CreateChannel()
        {
            _channel = _session.CreateChannel<ChannelSession>();
            _channel.DataReceived += Channel_DataReceived;
            _channel.ExtendedDataReceived += Channel_ExtendedDataReceived;
            _channel.RequestReceived += Channel_RequestReceived;
            _channel.Closed += Channel_Closed;

            //  Dispose of streams if already exists
            if (OutputStream != null)
            {
                OutputStream.Dispose();
                OutputStream = null;
            }

            if (ExtendedOutputStream != null)
            {
                ExtendedOutputStream.Dispose();
                ExtendedOutputStream = null;
            }

            //  Initialize output streams and StringBuilders
            OutputStream = new PipeStream();
            ExtendedOutputStream = new PipeStream();

            _result = null;
            _error = null;
        }

        private void Session_Disconnected(object sender, EventArgs e)
        {
            //  If objected is disposed or being disposed don't handle this event
            if (_isDisposed)
                return;

            _exception =
                new SshConnectionException(
                    "An established connection was aborted by the software in your host machine.",
                    DisconnectReason.ConnectionLost);

            _sessionErrorOccuredWaitHandle.Set();
        }

        private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
        {
            //  If objected is disposed or being disposed don't handle this event
            if (_isDisposed)
                return;

            _exception = e.Exception;

            _sessionErrorOccuredWaitHandle.Set();
        }

        private void Channel_Closed(object sender, ChannelEventArgs e)
        {
            if (OutputStream != null)
            {
                OutputStream.Flush();
            }

            if (ExtendedOutputStream != null)
            {
                ExtendedOutputStream.Flush();
            }

            _asyncResult.IsCompleted = true;

            if (_callback != null)
            {
                //  Execute callback on different thread                
                ExecuteThread(() => { _callback(_asyncResult); });
            }
            ((EventWaitHandle) _asyncResult.AsyncWaitHandle).Set();
        }

        private void Channel_RequestReceived(object sender, ChannelRequestEventArgs e)
        {
            Message replyMessage = new ChannelFailureMessage(_channel.LocalChannelNumber);

            if (e.Info is ExitStatusRequestInfo)
            {
                var exitStatusInfo = e.Info as ExitStatusRequestInfo;

                ExitStatus = (int) exitStatusInfo.ExitStatus;

                replyMessage = new ChannelSuccessMessage(_channel.LocalChannelNumber);
            }

            if (e.Info.WantReply)
            {
                _session.SendMessage(replyMessage);
            }
        }

        private void Channel_ExtendedDataReceived(object sender, ChannelDataEventArgs e)
        {
            if (ExtendedOutputStream != null)
            {
                ExtendedOutputStream.Write(e.Data, 0, e.Data.Length);
                ExtendedOutputStream.Flush();
            }

            if (e.DataTypeCode == 1)
            {
                _hasError = true;
            }
        }

        private void Channel_DataReceived(object sender, ChannelDataEventArgs e)
        {
            if (OutputStream != null)
            {
                OutputStream.Write(e.Data, 0, e.Data.Length);
                OutputStream.Flush();
            }

            if (_asyncResult != null)
            {
                lock (_asyncResult)
                {
                    _asyncResult.BytesReceived += e.Data.Length;
                }
            }
        }

        /// <exception cref="SshOperationTimeoutException">Command '{0}' has timed out.</exception>
        /// <remarks>The actual command will be included in the exception message.</remarks>
        private void WaitHandle(WaitHandle waitHandle)
        {
            var waitHandles = new[]
            {
                _sessionErrorOccuredWaitHandle,
                waitHandle
            };

            switch (System.Threading.WaitHandle.WaitAny(waitHandles, CommandTimeout))
            {
                case 0:
                    throw _exception;
                case System.Threading.WaitHandle.WaitTimeout:
                    throw new SshOperationTimeoutException(string.Format(CultureInfo.CurrentCulture,
                        "Command '{0}' has timed out.", CommandText));
                default:
                    break;
            }
        }

        partial void ExecuteThread(Action action);

        #region IDisposable Members

        private bool _isDisposed;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged ResourceMessages.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged ResourceMessages.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_isDisposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged ResourceMessages.
                if (disposing)
                {
                    _session.Disconnected -= Session_Disconnected;
                    _session.ErrorOccured -= Session_ErrorOccured;

                    // Dispose managed ResourceMessages.
                    if (OutputStream != null)
                    {
                        OutputStream.Dispose();
                        OutputStream = null;
                    }

                    // Dispose managed ResourceMessages.
                    if (ExtendedOutputStream != null)
                    {
                        ExtendedOutputStream.Dispose();
                        ExtendedOutputStream = null;
                    }

                    // Dispose managed ResourceMessages.
                    if (_sessionErrorOccuredWaitHandle != null)
                    {
                        _sessionErrorOccuredWaitHandle.Dispose();
                        _sessionErrorOccuredWaitHandle = null;
                    }

                    // Dispose managed ResourceMessages.
                    if (_channel != null)
                    {
                        _channel.DataReceived -= Channel_DataReceived;
                        _channel.ExtendedDataReceived -= Channel_ExtendedDataReceived;
                        _channel.RequestReceived -= Channel_RequestReceived;
                        _channel.Closed -= Channel_Closed;

                        _channel.Dispose();
                        _channel = null;
                    }
                }

                // Note disposing has been done.
                _isDisposed = true;
            }
        }

        /// <summary>
        ///     Releases unmanaged resources and performs other cleanup operations before the
        ///     <see cref="SshCommand" /> is reclaimed by garbage collection.
        /// </summary>
        ~SshCommand()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}