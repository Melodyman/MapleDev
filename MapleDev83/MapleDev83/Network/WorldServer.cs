namespace MapleDev83.Network
{
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
        /// Initializes a new instance of the <see cref="WorldServer" /> class.
        /// </summary>
        /// <param name="ipAddress">The server's IP address</param>
        /// <param name="port">The server's port number</param>
        public WorldServer(IPAddress ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            this.Listener = new TcpListener(IpAddress, Port);
            this.clientsAwaitToLogin = new List<Client>();
            this.connectedClients = new List<Client>();
        }

        /// <summary>
        /// Set the server to start listen for new incomes.
        /// </summary>
        public override void Start()
        {
            base.Start();
            Socket socket = this.Listener.AcceptSocket();
            ThreadPool.QueueUserWorkItem(this.HandleNewSocket, socket);
        }

        /// <summary>
        /// Handler new incomes
        /// </summary>
        /// <param name="obj">The socket of the new income</param>
        private void HandleNewSocket(object obj)
        {
            Socket socket = (Socket)obj;
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
