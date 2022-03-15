using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfra.Utils
{
    static public class EndToEndId
    {
        static public string Create(string ispb)
        {
            string randomSource = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            char[] charArr = randomSource.ToCharArray();
            int[] range = Enumerable.Range(1, 11).ToArray();
            
            string endToEndId = "E";
            
            endToEndId += ispb;
            endToEndId += DateTime.Now.ToString(@"yyyyMMddhhmm");
            
            foreach (int i in range)
            {
                Random rnd = new Random();
                int index = rnd.Next(charArr.Length);
                endToEndId += randomSource[index];
            }

            return endToEndId;
        }
    }
}