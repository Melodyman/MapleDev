namespace MapleDev83.Network.PacketHandling.Handlers.LoginHandlers
{ 
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Crypt;
    using Extensions;

    class Login
    {
        public static void LoginPassword(byte[] data, Client client, PacketReader reader)
        {
            MaplePacketCreator packet = client.PacketCreator;
            string username = Snippets.real_escape_string(reader.ReadMapleString());
            string password = Snippets.real_escape_string(reader.ReadMapleString());
            Dictionary<string, string> a = new Dictionary<string, string>();
            DataTable result = Program.mysqlDataProvider.Select($"SELECT gender, salt, verified, password, banned,permBan, tempBan, id, gm, logged, pin FROM Accounts WHERE username={username}");

            if (result.Rows.Count == 0)
            {
                client.sendDataEncrypted(packet.loginError(5));
            }
            else
            {
                password = PasswordCrypter.HashPassword(password, Convert.ToString(result.Rows[0]["salt"]));
                if (Convert.ToBoolean(result.Rows[0]["banned"]))
                {
                    if (Convert.ToBoolean(result.Rows[0]["permBan"]))
                    {
                        client.sendDataEncrypted(packet.loginError(10));
                        return;
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(result.Rows[0]["tempBan"]);
                        if (dt == DateTime.Today)
                        {
                            Program.mysqlDataProvider.Update($"UPDATE accounts SET tempBan='01-01-1900', banned='0', banReason='' WHERE username={username}");

                        }
                        else
                        {
                            client.sendDataEncrypted(packet.loginError(3));
                            return;
                        }
                    }
                }
                if (Convert.ToBoolean(result.Rows[0]["logged"]))
                {
                    client.sendDataEncrypted(packet.loginError(7));
                }
                else if (!Convert.ToBoolean(result.Rows[0]["verified"]))
                {
                    client.sendDataEncrypted(packet.loginError(16));
                }
                else if (Convert.ToString(result.Rows[0]["password"]).Equals(password))
                {
                    client.sendDataEncrypted(packet.loginSuccess(username, Convert.ToBoolean(result.Rows[0]["gm"]), Convert.ToInt32(result.Rows[0]["id"]), Convert.ToBoolean(result.Rows[0]["gender"])));
                }
                else
                {
                    client.sendDataEncrypted(packet.loginError(4));
                }
            }
        }
    }
}
