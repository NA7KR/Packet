using System;
using Routrek.SSHC;

namespace PacketComs
{ 
    public delegate bool HostKeyCheckCallback(SshConnectionInfo connectionInfo);

    public struct PortForwardingCheckResult
    {
        public bool Allowed;
        public ISshChannelEventReceiver Channel;
        public int ReasonCode;
        public string ReasonMessage;
    }

    public interface ISshConnectionEventReceiver
    {
        void OnDebugMessage(bool alwaysDisplay, byte[] msg);
        void OnIgnoreMessage(byte[] msg);
        void OnUnknownMessage(byte type, byte[] data);
        void OnError(Exception error, string msg);
        void OnConnectionClosed();
        void OnAuthenticationPrompt(string[] prompts); //keyboard-interactive only

        PortForwardingCheckResult CheckPortForwardingRequest(string remoteHost, int remotePort, string originatorHost,
            int originatorPort);

        void EstablishPortforwarding(ISshChannelEventReceiver receiver, SshChannel channel);
    }

    public interface ISshChannelEventReceiver
    {
        void OnData(byte[] data, int offset, int length);
        void OnExtendedData(int type, byte[] data);
        void OnChannelClosed();
        void OnChannelEof();
        void OnChannelError(Exception error, string msg);
        void OnChannelReady();
        void OnMiscPacket(byte packetType, byte[] data, int offset, int length);
    }
}