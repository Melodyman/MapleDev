namespace MapleDev83.Network.PacketHandling
{
    using System;
    using Handlers.LoginHandlers;

    /// <summary>
    /// Processing income packets
    /// </summary>
    public class PacketProcessor
    {
        /// <summary>
        /// Transfers each packet to the right handler by operation code
        /// </summary>
        /// <param name="data">The packet</param>
        /// <param name="client">The client who received the packet</param>
        public void Handle(byte[] data, Client client)
        {
            PacketReader reader = new PacketReader(data);
            RecvOpcodes.RecvOpcode opcode = (RecvOpcodes.RecvOpcode)reader.ReadUShort();
            Console.WriteLine($"Packet received {opcode.ToString()}");
            switch (opcode)
            {
                case RecvOpcodes.RecvOpcode.LOGIN_PASSWORD:
                    {
                        Login.LoginPassword(data, client, reader);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
