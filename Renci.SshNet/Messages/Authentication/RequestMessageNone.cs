namespace Renci.SshNet.Messages.Authentication
{
    /// <summary>
    ///     Represents "none" SSH_MSG_USERAUTH_REQUEST message.
    /// </summary>
    internal class RequestMessageNone : RequestMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RequestMessagePassword" /> class.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="username">Authentication username.</param>
        public RequestMessageNone(ServiceName serviceName, string username)
            : base(serviceName, username)
        {
        }

        /// <summary>
        ///     Gets the name of the authentication method.
        /// </summary>
        /// <value>
        ///     The name of the method.
        /// </value>
        public override string MethodName
        {
            get { return "none"; }
        }

        /// <summary>
        ///     Called when type specific data need to be saved.
        /// </summary>
        protected override void SaveData()
        {
            base.SaveData();
        }
    }
}