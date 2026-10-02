namespace StarkInfra.Utils
{
    static public class EndToEndID
    {
        static public string Create(string bankCode)
        {
            return "E" + BacenID.Create(bankCode: bankCode);
        }
    }
}
