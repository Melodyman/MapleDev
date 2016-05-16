using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MSDEV83.Net.Crypt
{
    static class PasswordCrypter
    {

        public static string GenerateSalt()
        {
            var g = Guid.NewGuid();
            return getMd5Hash(g.ToString("N"));
        }

        public static string HashPassword(string password, string salt)
        {
            password = getMd5Hash(password);
            string new_pwd = salt + password + salt;
            new_pwd = getMd5Hash(new_pwd);
            return new_pwd;
        }

        private static string getMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}
