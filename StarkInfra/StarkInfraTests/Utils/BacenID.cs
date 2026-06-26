using System;


namespace StarkInfraTests
{
    public static class BacenID
    {
        public static string Create(string bankCode)
        {
            Random rand = new Random();
            string datePart = DateTime.UtcNow.ToString("yyyyMMddHHmm");
            string randomPart = rand.Next(1000000, 9999999).ToString();
            return "RR" + bankCode + datePart + randomPart;
        }
    }
}
