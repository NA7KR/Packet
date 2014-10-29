namespace PacketComs
{
    public class SSHServerInfo
    {
        public byte[] anti_spoofing_cookie;
        public int server_key_bits;
        public BigInteger server_key_public_exponent;
        public BigInteger server_key_public_modulus;
        public int host_key_bits;
        public BigInteger host_key_public_exponent;
        public BigInteger host_key_public_modulus;

        internal SSHServerInfo(SshDataReader reader)
        {
            anti_spoofing_cookie = reader.Read(8); //first 8 bytes are cookie

            server_key_bits = reader.ReadInt32();
            server_key_public_exponent = reader.ReadMpInt();
            server_key_public_modulus = reader.ReadMpInt();
            host_key_bits = reader.ReadInt32();
            host_key_public_exponent = reader.ReadMpInt();
            host_key_public_modulus = reader.ReadMpInt();
        }
    }
}