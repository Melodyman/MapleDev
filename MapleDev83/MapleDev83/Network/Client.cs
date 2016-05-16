namespace MapleDev83.Network
{
    using System.Net.Sockets;
    using Extensions;
    using PacketHandling;
    using Crypt;

    /// <summary>
    /// Class that contains client information
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Client's socket
        /// </summary>
        private Socket socket;

        /// <summary>
        /// Client's receive cipher
        /// </summary>
        private Cipher recvCipher;

        /// <summary>
        /// Client's send cipher
        /// </summary>
        private Cipher sendCipher;

        /// <summary>
        /// Client's receive initialize vector
        /// </summary>
        private uint recvIv;

        /// <summary>
        /// Client's send initialize vector
        /// </summary>
        private uint sendIv;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client" /> class.
        /// </summary>
        /// <param name="socket">The client's socket</param>
        public Client(Socket socket)
        {
            this.socket = socket;
            this.recvCipher = new Cipher(Program.MapleVersion, Program.AESKey);
            this.sendCipher = new Cipher(Program.MapleVersion, Program.AESKey);
            this.recvIv = (uint)Snippets.Random();
            this.sendIv = (uint)Snippets.Random();
            this.recvCipher.SetIV(this.recvIv);
            this.sendCipher.SetIV(this.sendIv);
            this.Handshake();
        }

        /// <summary>
        /// On client logged successfully into the server delegate
        /// </summary>
        public delegate void OnFinishLogin();

        /// <summary>
        /// On client logged successfully into the server, the event occurred
        /// </summary>
        public event OnFinishLogin OnLogged;

        /// <summary>
        /// Send handshake to the client
        /// </summary>
        private void Handshake()
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(0);
            writer.WriteUShort(Program.MapleVersion);
            writer.WriteMapleString(Program.MapleSubVersion);
            writer.WriteUInt(this.recvIv);
            writer.WriteUInt(this.sendIv);
            writer.WriteByte(8);
            writer.SetShort((short)(writer.Length - 2), 0);
            byte[] packet = writer.ToArray();
            this.socket.Send(packet);
        }
    }
}
