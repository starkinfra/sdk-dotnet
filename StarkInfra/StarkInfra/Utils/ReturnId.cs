namespace StarkInfra.Utils
{
    static public class ReturnID
    {
        static public string Create(string bankCode)
        {
            return "D" + BacenID.Create(bankCode: bankCode);
        }
    }
}
