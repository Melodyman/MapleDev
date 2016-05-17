namespace MapleDev83.Network
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>                            
    /// An WorldServer class that inherits from Server class
    /// </summary>
    public class WorldServer : Server
    {
        /// <summary>
        /// List of clients that are awaiting to login.
        /// </summary>
        private List<Client> clientsAwaitToLogin;

        /// <summary>
        /// List of clients that are already logged.
        /// </summary>
        private List<Client> connectedClients;

        /// <summary>
        /// List of channels in the world
        /// </summary>
        private ChannelServer[] channels;

        /// <summary>
        /// Name of the world
        /// </summary>
        private WorldName worldName;

        /// <summary>
        /// World's message
        /// </summary>
        private string worldMessage;

        /// <summary>
        /// World's flag
        /// </summary>
        private WorldFlag worldFlag;

        /// <summary>
        /// Can create new characters
        /// </summary>
        private bool characterCreationDisable;

        /// <summary>
        /// Can view all characters
        /// </summary>
        private bool viewAllCharacters;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldServer" /> class.
        /// </summary>
        /// <param name="ipAddress">The server's IP address</param>
        /// <param name="port">The server's port number</param>
        /// <param name="worldName">World's name</param>
        /// <param name="worldMessage">World's message</param>
        /// <param name="worldFlag">World's flag</param>
        /// <param name="channelsCount">Count of channels in the world</param>
        /// <param name="channelsStartPort">The port that the channels servers will start from</param>
        /// <param name="createCharactersDisable">Disable creating new characters</param>
        /// <param name="viewAllCharacters">Enable viewing all characters</param>
        public WorldServer(IPAddress ipAddress, int port, WorldName worldName,
                            string worldMessage, WorldFlag worldFlag, int channelsCount,
                            int channelsStartPort, bool createCharactersDisable, bool viewAllCharacters)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            this.Listener = new TcpListener(IpAddress, Port);
            this.clientsAwaitToLogin = new List<Client>();
            this.connectedClients = new List<Client>();
            this.channels = new ChannelServer[channelsCount];
            for (int i = 0; i < channelsCount; i++)
            {
                this.channels[i] = new ChannelServer(ipAddress, channelsStartPort + i);
            }
            this.worldName = worldName;
            this.worldMessage = worldMessage;
            this.worldFlag = worldFlag;
            this.characterCreationDisable = createCharactersDisable;
            this.viewAllCharacters = viewAllCharacters;

        }

        /// <summary>
        /// Sets the server to start receive connections
        /// </summary>
        public override void Start()
        {
            base.Start();
            Console.WriteLine($"[WORLD Server {this.worldName}[{this.worldFlag}] - (\"{this.worldMessage}\")] started on {((IPEndPoint)this.Listener.LocalEndpoint).Address}:{((IPEndPoint)this.Listener.LocalEndpoint).Port}");
            this.Listener.BeginAcceptSocket(this.HandleNewSocket, null);
            for (int i = 0; i < this.channels.Length; i++)
            {
                this.channels[i].Start();
            }
            
        }

        /// <summary>
        /// Handler new incomes
        /// </summary>
        /// <param name="ar">The socket of the new income</param>
        public void HandleNewSocket(IAsyncResult ar)
        {
            this.Listener.BeginAcceptSocket(this.HandleNewSocket, null);
            Socket socket = this.Listener.EndAcceptSocket(ar);
            Console.WriteLine("new client");
            Client client = new Client(socket);
            client.OnLogged += () =>
            {
                this.clientsAwaitToLogin.Remove(client);
                this.connectedClients.Add(client);
            };
            this.clientsAwaitToLogin.Add(client);
        }
    }
}
