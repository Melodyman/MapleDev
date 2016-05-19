namespace MapleDev83.Network
{
    using System;
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
        /// Data received buffer;
        /// </summary>
        private byte[] buffer;

        /// <summary>
        /// Wait for more data
        /// </summary>
        private bool dataWaiting;

        /// <summary>
        /// Packet processor
        /// </summary>
        private PacketProcessor packetProcessor;

        /// <summary>
        /// Packet creator
        /// </summary>
        private MaplePacketCreator packetCreator;

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
            this.buffer = new byte[1024];
            this.dataWaiting = false;
            this.packetProcessor = new PacketProcessor();
            this.packetCreator = new MaplePacketCreator();
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


        public MaplePacketCreator PacketCreator
        {
            get
            {
                return this.packetCreator;
            }
        }

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
            this.sendDataRaw(packet);
            this.BeginAcceptData();
        }

        public void sendDataEncrypted(byte[] data, bool toClient = true)
        {
            sendCipher.Encrypt(ref data, toClient);
            socket.Send(data);
        }

        public void sendDataRaw(byte[] data)
        {
            socket.Send(data);
        }

        public void BeginAcceptData()
        {
            SocketError error = SocketError.Success;
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, out error, new AsyncCallback(OnDataReceived), null);
        }

        private void OnDataReceived(IAsyncResult ar)
        {
            int received = this.socket.EndReceive(ar);
            if (received == 0)
            {

            }
            else
            {
                if (this.dataWaiting)
                {
                    // Handle more data then in the packet
                    /*
                    byte[] data = new byte[buffer.Length];
                    Buffer.BlockCopy(buffer, 0, data, 0, received);
                    if (received < recvCipher.GetPacketLength(data))
                    {

                    }
                    */
                }
                else
                {
                    byte[] data = new byte[this.buffer.Length];
                    Buffer.BlockCopy(this.buffer, 0, data, 0, received);
                    if (received < this.recvCipher.GetPacketLength(data))
                    {
                        /*
                        this.dataWaiting = true;
                        */
                    }
                    else
                    {
                        this.recvCipher.Decrypt(ref data);
                        this.packetProcessor.Handle(data, this);
                    }
                    this.BeginAcceptData();
                }
            }
        }
    }
}
