namespace MapleDev83.Extensions
{
    using System;
    using System.Text;

    /// <summary>
    /// Class of code snippets
    /// </summary>
    public static class Snippets
    {
        /// <summary>
        /// Randomize new number in the range of 0 and <see cref="int.MaxValue"/>
        /// </summary>
        /// <returns>Returns the random number</returns>
        public static int Random()
        {
            Random rnd = new Random();
            return rnd.Next();
        }

        /// <summary>
        /// Convert Hex from byte array into string variable
        /// </summary>
        /// <param name="byteArray">The byte array of hexes</param>
        /// <param name="appendSpace">Append spaces or not</param>
        /// <returns>Returns the string converted from hex</returns>
        public static string ToHexString(this byte[] byteArray, bool appendSpace = true)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteArray)
            {
                if (appendSpace)
                {
                    sb.Append(b.ToString("X2"));
                    sb.Append(' ');
                }
                else
                {
                    sb.Append(b.ToString("X2"));
                }
            }

            return sb.ToString();
        }
    }
}
