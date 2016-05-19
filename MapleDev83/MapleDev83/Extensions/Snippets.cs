namespace MapleDev83.Extensions
{
    using System;
    using System.Linq;
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
            return Program.random.Next();
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

        
        public static void Split<T>(T[] array, int index, out T[] first, out T[] second)
        {
            first = array.Take(index).ToArray();
            second = array.Skip(index).ToArray();
        }

        public static string TodayDate()
        {
            DateTime today = DateTime.Today.Date;
            return today.ToString("MM-dd-yyyy");
        }
        public static string real_escape_string(string original)
        {
            return ReplaceStrings(original, new string[] { "\n", "\r", "\\", "'", "\'", "\"", "→" });
        }

        public static string ReplaceStrings(string original, string[] replace, string placetaker = "")
        {
            string copy = original;
            for (int i = 0; i < replace.Length; i++)
            {
                copy = copy.Replace(replace[i], placetaker);
            }
            return copy;
        }

        public static int IntLength(int p)
        {
            return (int)Math.Floor(Math.Log10(p)) + 1;
        }

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Program.Jan1st1970).TotalMilliseconds;
        }

        public static long MapleTime(long t)
        {
            switch (t)
            {
                case -1: // default
                    return 150842304000000000L;
                case -2: // zero time 
                    return 94354848000000000L;
                case -3: // perm
                    return 150841440000000000L;
                default:
                    return t * 10000 + 116444592000000000L;
            }
        }
    }
}
