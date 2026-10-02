namespace StarkInfra.Utils
{
    static public class PixSubscriptionBacenID
    {
        static public string Create(string bankCode, string prefix)
        {
            return prefix + BacenID.Create(bankCode: bankCode, dateFormat: "yyyyMMdd");
        }
    }
}
