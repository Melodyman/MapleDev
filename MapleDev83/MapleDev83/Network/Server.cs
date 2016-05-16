namespace MapleDev83.Network
{
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// An abstract Server class
    /// </summary>
    public abstract class Server
    {
        /// <summary>
        /// TCPListener variable to receive clients
        /// </summary>
        private TcpListener listener;
        
        /// <summary>
        /// Server IP address
        /// </summary>
        private IPAddress ipAddress;
        
        /// <summary>
        /// Server port number
        /// </summary>
        private int port;

        /// <summary>
        /// Gets or sets the IP address of the server
        /// </summary>
        public IPAddress IpAddress
        {
            get
            {
                return this.ipAddress;
            }

            protected set
            {
                this.ipAddress = value;
            }
        }

        /// <summary>
        /// Gets or sets the port number of the server
        /// </summary>
        public int Port
        {
            get
            {
                return this.port;
            }

            protected set
            {
                this.port = value;
            }
        }

        /// <summary>
        /// Gets or sets the TCPListener
        /// </summary>
        public TcpListener Listener
        {
            get
            {
                return this.listener;
            }

            protected set
            {
                this.listener = value;
            }
        }

        /// <summary>
        /// Starts the server
        /// </summary>
        public virtual void Start()
        {
            this.listener.Start();
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        public virtual void Stop()
        {
            this.listener.Stop();
        }
    }
}
