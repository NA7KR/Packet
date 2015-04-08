using System.Collections.Generic;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
    /// <summary>
    ///     Represents "pty-req" type channel request information
    /// </summary>
    internal class PseudoTerminalRequestInfo : RequestInfo
    {
        /// <summary>
        ///     Channel request name
        /// </summary>
        public const string NAME = "pty-req";

        /// <summary>
        ///     Initializes a new instance of the <see cref="PseudoTerminalRequestInfo" /> class.
        /// </summary>
        public PseudoTerminalRequestInfo()
        {
            WantReply = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PseudoTerminalRequestInfo" /> class.
        /// </summary>
        /// <param name="environmentVariable">The environment variable.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="terminalModeValues">The terminal mode values.</param>
        public PseudoTerminalRequestInfo(string environmentVariable, uint columns, uint rows, uint width, uint height,
            IDictionary<TerminalModes, uint> terminalModeValues)
            : this()
        {
            EnvironmentVariable = environmentVariable;
            Columns = columns;
            Rows = rows;
            PixelWidth = width;
            PixelHeight = height;
            TerminalModeValues = terminalModeValues;
        }

        /// <summary>
        ///     Gets the name of the request.
        /// </summary>
        /// <value>
        ///     The name of the request.
        /// </value>
        public override string RequestName
        {
            get { return NAME; }
        }

        /// <summary>
        ///     Gets or sets the environment variable.
        /// </summary>
        /// <value>
        ///     The environment variable.
        /// </value>
        public string EnvironmentVariable { get; set; }

        /// <summary>
        ///     Gets or sets the columns.
        /// </summary>
        /// <value>
        ///     The columns.
        /// </value>
        public uint Columns { get; set; }

        /// <summary>
        ///     Gets or sets the rows.
        /// </summary>
        /// <value>
        ///     The rows.
        /// </value>
        public uint Rows { get; set; }

        /// <summary>
        ///     Gets or sets the width of the pixel.
        /// </summary>
        /// <value>
        ///     The width of the pixel.
        /// </value>
        public uint PixelWidth { get; set; }

        /// <summary>
        ///     Gets or sets the height of the pixel.
        /// </summary>
        /// <value>
        ///     The height of the pixel.
        /// </value>
        public uint PixelHeight { get; set; }

        /// <summary>
        ///     Gets or sets the terminal mode.
        /// </summary>
        /// <value>
        ///     The terminal mode.
        /// </value>
        public IDictionary<TerminalModes, uint> TerminalModeValues { get; set; }

        /// <summary>
        ///     Called when type specific data need to be saved.
        /// </summary>
        protected override void SaveData()
        {
            base.SaveData();

            Write(EnvironmentVariable);
            Write(Columns);
            Write(Rows);
            Write(Rows);
            Write(PixelHeight);

            if (TerminalModeValues != null)
            {
                Write((uint) TerminalModeValues.Count*(1 + 4) + 1);

                foreach (var item in TerminalModeValues)
                {
                    Write((byte) item.Key);
                    Write(item.Value);
                }
                Write(0);
            }
            else
            {
                Write((uint) 0);
            }
        }
    }
}