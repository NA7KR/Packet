namespace Renci.SshNet.Messages.Connection
{
    /// <summary>
    ///     Represents SSH_MSG_CHANNEL_OPEN_FAILURE message.
    /// </summary>
    [Message("SSH_MSG_CHANNEL_OPEN_FAILURE", 92)]
    public class ChannelOpenFailureMessage : ChannelMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChannelOpenFailureMessage" /> class.
        /// </summary>
        public ChannelOpenFailureMessage()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChannelOpenFailureMessage" /> class.
        /// </summary>
        /// <param name="localChannelNumber">The local channel number.</param>
        /// <param name="description">The description.</param>
        /// <param name="reasonCode">The reason code.</param>
        public ChannelOpenFailureMessage(uint localChannelNumber, string description, uint reasonCode)
        {
            LocalChannelNumber = localChannelNumber;
            Description = description;
            ReasonCode = reasonCode;
        }

        /// <summary>
        ///     Gets failure reason code.
        /// </summary>
        public uint ReasonCode { get; private set; }

        /// <summary>
        ///     Gets description for failure.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        ///     Gets message language.
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        ///     Called when type specific data need to be loaded.
        /// </summary>
        protected override void LoadData()
        {
            base.LoadData();
            ReasonCode = ReadUInt32();
            Description = ReadString();
            Language = ReadString();
        }

        /// <summary>
        ///     Called when type specific data need to be saved.
        /// </summary>
        protected override void SaveData()
        {
            base.SaveData();
            Write(ReasonCode);
            Write(Description ?? string.Empty);
            Write(Language ?? "en");
        }
    }
}