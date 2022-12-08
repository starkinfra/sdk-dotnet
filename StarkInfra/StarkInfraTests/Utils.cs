using System;
using StarkInfra;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public static class TestUtils
    {
        internal static void Log(object log)
        {
            Console.WriteLine(log);
        }
        
        static public string GetEndToEndID()
        {
            string endToEndID = "";
            List<PixRequest> page;
            string cursor = null;
            while (endToEndID == "")
            {
                (page, cursor) = PixRequest.Page(limit: 5, cursor: cursor);
                foreach (PixRequest entity in page)
                {
                    if ((entity.Amount > 5) && (entity.Flow == "in") && (entity.Status == "success"))
                    {
                        endToEndID = entity.EndToEndID;
                    }
                }

                if (cursor == null) { break; }
            }
            return endToEndID;
        }

        public static string RandomPhoneNumber()
        {
            return "+5511" + RandomNumberString(100000000, 999999999);
        }

        public static string RandomNumberString(int start, int end)
        {
            Random randNum = new Random();
            string randomNumber = randNum.Next(start, end).ToString();

            return randomNumber;
        }
    }
}
