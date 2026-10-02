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

        static public string Create(string bankCode, string dateFormat = "yyyyMMddHHmm")
        {
            StringBuilder bacenID = new StringBuilder(bankCode);
            bacenID.Append(DateTime.Now.ToString(dateFormat));

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
