using System;
using System.Text;


namespace StarkInfra.Utils
{
    static public class BacenID
    {
        private const string RandomSource = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const int RandomLength = 11;

        private static readonly Random random = new Random();
        private static readonly object randomLock = new object();

        /// <summary>
        /// Create a random BacenID
        /// <br/>
        /// Generates a random BacenID based on your bank code (ISPB). The date part is always generated in UTC,
        /// regardless of the machine timezone.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>bankCode [string]: your bank code (ISPB). ex: "20018183"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>dateFormat [string, default "yyyyMMddHHmm"]: format of the UTC date part. ex: "yyyyMMdd"</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>BacenID composed of the bank code, the current UTC date and 11 random alphanumeric characters</item>
        /// </list>
        /// </summary>
        static public string Create(string bankCode, string dateFormat = "yyyyMMddHHmm")
        {
            StringBuilder bacenID = new StringBuilder(bankCode);
            bacenID.Append(DateTime.UtcNow.ToString(dateFormat));

            lock (randomLock)
            {
                for (int i = 0; i < RandomLength; i++)
                {
                    bacenID.Append(RandomSource[random.Next(RandomSource.Length)]);
                }
            }

            return bacenID.ToString();
        }
    }
}
