namespace MapleDev83
{
    using System;
    using System.Net;
    using Network;
    using Data;

    /// <summary>
    /// Types of flags of world server
    /// </summary>
    public enum WorldFlag
    {
        None = 0,
        Event = 1,
        New = 2,
        Hot = 3
    }

    /// <summary>
    /// Names for world server
    /// </summary>
    public enum WorldName
    {
        Scania
    }

    /// <summary>
    /// Main class of the project
    /// </summary>
    public class Program
    {
        #region Configurations

        #region Server Configurations
        public static bool enablePin = true;
        public static bool enablePic = true;
        public static bool isBeta = false;
        public static int expRate = 1;
        public static int mesoRate = 1;
        public static int dropRate = 1;
        #endregion

        #region World Configurations
        public static string worldHost = "127.0.0.1";
        public static int worldPort = 8484;
        public static WorldName worldName = WorldName.Scania;
        public static string worldMessage = "Welcome!";
        public static WorldFlag worldFlag = WorldFlag.Hot;
        public static int worldChannelsCount = 20;
        public static int worldChannelsStartPort = 8590;
        public static bool worldDisableCharaterCreation = false;
        public static bool worldViewAllCharacters = true;
        #endregion

        #region MySQL Configurations
        public static string mysqlUser = "root";
        public static string mysqlPassword = "";
        public static string mysqlHost = "localhost";
        public static string mysqlDatabase = "mapledev";
        #endregion

        #endregion

        public static ushort MapleVersion = 83;
        public static string MapleSubVersion = "1";
        public static ulong AESKey = 0x130806B41B0F3352;
        public static WorldServer worldServer;
        public static MySqlDataProvider mysqlDataProvider;

        /// <summary>
        /// The main code
        /// </summary>
        /// <param name="args">Arguments to run differently the program</param>
        public static void Main(string[] args)
        {
            worldServer = new WorldServer(
                IPAddress.Parse(worldHost), 
                worldPort,
                worldName,
                worldMessage,
                worldFlag,
                worldChannelsCount,
                worldChannelsStartPort,
                worldDisableCharaterCreation,
                worldViewAllCharacters);

            mysqlDataProvider = new MySqlDataProvider(
                mysqlUser,
                mysqlPassword,
                mysqlHost,
                mysqlDatabase);

            if (mysqlDataProvider.Open())
            {
                worldServer.Start();
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}
