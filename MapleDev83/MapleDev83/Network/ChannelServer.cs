namespace MapleDev83.Network
{
    using System.Net;
    using System.Net.Sockets;

    class ChannelServer : Server
    {

        public ChannelServer(IPAddress ipAddress, int port)
        {
            this.Listener = new TcpListener(ipAddress, port);
        }

        public override void Start()
        {
            base.Start();
            System.Console.WriteLine($"[Channel Server] started on {((IPEndPoint)this.Listener.LocalEndpoint).Address}:{((IPEndPoint)this.Listener.LocalEndpoint).Port}");
        }
    }
}
