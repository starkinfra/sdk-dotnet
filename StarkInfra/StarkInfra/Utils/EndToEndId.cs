using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfra.Utils
{
    static public class EndToEndID
    {
        static public string Create(string bankCode)
        {
            string randomSource = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] charArr = randomSource.ToCharArray();
            int[] range = Enumerable.Range(1, 11).ToArray();

            string endToEndID = "E";

            endToEndID += bankCode;
            endToEndID += DateTime.Now.ToString(@"yyyyMMddHHmm");

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
