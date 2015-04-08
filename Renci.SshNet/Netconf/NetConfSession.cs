using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;

namespace Renci.SshNet.NetConf
{
    internal class NetConfSession : SubsystemSession
    {
        private const string _prompt = "]]>]]>";
        private readonly StringBuilder _data = new StringBuilder();
        private int _messageId;
        private StringBuilder _rpcReply = new StringBuilder();
        private EventWaitHandle _rpcReplyReceived = new AutoResetEvent(false);
        private EventWaitHandle _serverCapabilitiesConfirmed = new AutoResetEvent(false);
        private bool _usingFramingProtocol;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NetConfSession" /> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="operationTimeout">The operation timeout.</param>
        public NetConfSession(Session session, TimeSpan operationTimeout)
            : base(session, "netconf", operationTimeout, Encoding.UTF8)
        {
            ClientCapabilities = new XmlDocument();
            ClientCapabilities.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                       "<hello xmlns=\"urn:ietf:params:xml:ns:netconf:base:1.0\">" +
                                       "<capabilities>" +
                                       "<capability>" +
                                       "urn:ietf:params:netconf:base:1.0" +
                                       "</capability>" +
                                       "</capabilities>" +
                                       "</hello>");
        }

        /// <summary>
        ///     Gets NetConf server capabilities.
        /// </summary>
        public XmlDocument ServerCapabilities { get; private set; }

        /// <summary>
        ///     Gets NetConf client capabilities.
        /// </summary>
        public XmlDocument ClientCapabilities { get; }

        public XmlDocument SendReceiveRpc(XmlDocument rpc, bool automaticMessageIdHandling)
        {
            _data.Clear();

            XmlNamespaceManager ns = null;
            if (automaticMessageIdHandling)
            {
                _messageId++;
                ns = new XmlNamespaceManager(rpc.NameTable);
                ns.AddNamespace("nc", "urn:ietf:params:xml:ns:netconf:base:1.0");
                rpc.SelectSingleNode("/nc:rpc/@message-id", ns).Value = _messageId.ToString();
            }
            _rpcReply = new StringBuilder();
            _rpcReplyReceived.Reset();
            var reply = new XmlDocument();
            if (_usingFramingProtocol)
            {
                var command = new StringBuilder(rpc.InnerXml.Length + 10);
                command.AppendFormat("\n#{0}\n", rpc.InnerXml.Length);
                command.Append(rpc.InnerXml);
                command.Append("\n##\n");
                SendData(Encoding.UTF8.GetBytes(command.ToString()));

                WaitHandle(_rpcReplyReceived, _operationTimeout);
                reply.LoadXml(_rpcReply.ToString());
            }
            else
            {
                SendData(Encoding.UTF8.GetBytes(rpc.InnerXml + _prompt));
                WaitHandle(_rpcReplyReceived, _operationTimeout);
                reply.LoadXml(_rpcReply.ToString());
            }
            if (automaticMessageIdHandling)
            {
                //string reply_id = rpc.SelectSingleNode("/nc:rpc-reply/@message-id", ns).Value;
                var reply_id = rpc.SelectSingleNode("/nc:rpc/@message-id", ns).Value;
                if (reply_id != _messageId.ToString())
                {
                    throw new NetConfServerException("The rpc message id does not match the rpc-reply message id.");
                }
            }
            return reply;
        }

        protected override void OnChannelOpen()
        {
            _data.Clear();

            var message = string.Format("{0}{1}", ClientCapabilities.InnerXml, _prompt);

            SendData(Encoding.UTF8.GetBytes(message));

            WaitHandle(_serverCapabilitiesConfirmed, _operationTimeout);
        }

        protected override void OnDataReceived(uint dataTypeCode, byte[] data)
        {
            var chunk = Encoding.UTF8.GetString(data);

            if (ServerCapabilities == null) // This must be server capabilities, old protocol
            {
                _data.Append(chunk);

                if (!chunk.Contains(_prompt))
                {
                    return;
                }
                try
                {
                    chunk = _data.ToString();
                    _data.Clear();

                    ServerCapabilities = new XmlDocument();
                    ServerCapabilities.LoadXml(chunk.Replace(_prompt, ""));
                }
                catch (XmlException e)
                {
                    throw new NetConfServerException("Server capabilities received are not well formed XML", e);
                }

                var ns = new XmlNamespaceManager(ServerCapabilities.NameTable);

                ns.AddNamespace("nc", "urn:ietf:params:xml:ns:netconf:base:1.0");

                _usingFramingProtocol =
                    (ServerCapabilities.SelectSingleNode(
                        "/nc:hello/nc:capabilities/nc:capability[text()='urn:ietf:params:netconf:base:1.1']", ns) !=
                     null);

                _serverCapabilitiesConfirmed.Set();
            }
            else if (_usingFramingProtocol)
            {
                var position = 0;

                for (;;)
                {
                    var match = Regex.Match(chunk.Substring(position), @"\n#(?<length>\d+)\n");
                    if (!match.Success)
                    {
                        break;
                    }
                    var fractionLength = Convert.ToInt32(match.Groups["length"].Value);
                    _rpcReply.Append(chunk, position + match.Index + match.Length, fractionLength);
                    position += match.Index + match.Length + fractionLength;
                }
                if (Regex.IsMatch(chunk.Substring(position), @"\n##\n"))
                {
                    _rpcReplyReceived.Set();
                }
            }
            else // Old protocol
            {
                _data.Append(chunk);

                if (!chunk.Contains(_prompt))
                {
                    return;
                    //throw new NetConfServerException("Server XML message does not end with the prompt " + _prompt);
                }

                chunk = _data.ToString();
                _data.Clear();

                _rpcReply.Append(chunk.Replace(_prompt, ""));
                _rpcReplyReceived.Set();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (_serverCapabilitiesConfirmed != null)
                {
                    _serverCapabilitiesConfirmed.Dispose();
                    _serverCapabilitiesConfirmed = null;
                }

                if (_rpcReplyReceived != null)
                {
                    _rpcReplyReceived.Dispose();
                    _rpcReplyReceived = null;
                }
            }
        }
    }
}