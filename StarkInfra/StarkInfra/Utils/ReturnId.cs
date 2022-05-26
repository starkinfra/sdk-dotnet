using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfra.Utils
{
    static public class ReturnId
    {
        static public string Create(string bankCode)
        {
            string randomSource = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] charArr = randomSource.ToCharArray();
            int[] range = Enumerable.Range(1, 11).ToArray();

            string ReturnId = "D";

            ReturnId += bankCode;
            ReturnId += DateTime.Now.ToString(@"yyyyMMddhhmm");

            foreach (int i in range)
            {
                Random rnd = new Random();
                int index = rnd.Next(charArr.Length);
                ReturnId += randomSource[index];
            }

            return ReturnId;
        }
    }
}