namespace MapleDev83.Network.Crypt
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Password encryption class
    /// </summary>
    public static class PasswordCrypter
    {
        /// <summary>
        /// Generating random salt
        /// </summary>
        /// <returns>Return the generated salt</returns>
        public static string GenerateSalt()
        {
            var g = Guid.NewGuid();
            return GetMd5Hash(g.ToString("N"));
        }

        /// <summary>
        /// Creating new encrypted password
        /// </summary>
        /// <param name="password">The original password</param>
        /// <param name="salt">The generated salt</param>
        /// <returns>Returns the new encrypted password</returns>
        public static string HashPassword(string password, string salt)
        {
            password = GetMd5Hash(password);
            string new_pwd = salt + password + salt;
            new_pwd = GetMd5Hash(new_pwd);
            return new_pwd;
        }

        /// <summary>
        /// Creating MD5 hash out of string
        /// </summary>
        /// <param name="input">The string to hash</param>
        /// <returns>The hashed value</returns>
        private static string GetMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
