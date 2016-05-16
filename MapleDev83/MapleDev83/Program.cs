namespace MapleDev83
{
    using System;
    using System.Net;
    using Network;
    
    /// <summary>
    /// Main class of the project
    /// </summary>
    public class Program
    {
        public static ushort MapleVersion = 83;
        public static string MapleSubVersion = "1";
        public static ulong AESKey = 0x130806B41B0F3352;
        /// <summary>
        /// The main code
        /// </summary>
        /// <param name="args">Arguments to run differently the program</param>
        public static void Main(string[] args)
        {
            WorldServer worldServer = new WorldServer(IPAddress.Parse("127.0.0.1"), 8484);
            worldServer.Start();
            while(true)
            {
                Console.ReadLine();
            }
        }
    }
}
