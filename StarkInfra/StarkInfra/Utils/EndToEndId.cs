using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfra.Utils
{
    static public class EndToEndID
    {
        /// <summary>
        /// Create a random EndToEndID
        /// <br/>
        /// Generates a random EndToEndID based on your bank code (ISPB). The datetime stamp (yyyyMMddHHmm)
        /// is always generated in UTC, regardless of the machine timezone.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>bankCode [string]: your bank code (ISPB). ex: "20018183"</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>EndToEndID composed of "E", the bank code, the current UTC datetime and 11 random alphanumeric characters</item>
        /// </list>
        /// </summary>
        static public string Create(string bankCode)
        {
            string randomSource = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] charArr = randomSource.ToCharArray();
            int[] range = Enumerable.Range(1, 11).ToArray();

            string endToEndID = "E";

            endToEndID += bankCode;
            endToEndID += DateTime.UtcNow.ToString(@"yyyyMMddHHmm");

            foreach (int i in range)
            {
                Random rnd = new Random();
                int index = rnd.Next(charArr.Length);
                endToEndID += randomSource[index];
            }

            return endToEndID;
        }
    }
}
