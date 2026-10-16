namespace StarkInfra.Utils
{
    static public class PixSubscriptionBacenID
    {
        /// <summary>
        /// Create a random PixSubscriptionBacenID
        /// <br/>
        /// Generates a random Pix subscription bacenId based on your bank code (ISPB), used as the BacenID of a
        /// PixPullSubscription. The date part is always generated in UTC, regardless of the machine timezone.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>bankCode [string]: your bank code (ISPB). ex: "20018183"</item>
        ///     <item>prefix [string]: subscription prefix. ex: "RR"</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>29-character bacenId composed of the prefix, the bank code, the current UTC date (yyyyMMdd) and 11 random alphanumeric characters. ex: "RR2001818320261016aB3xK9mQ2wZ"</item>
        /// </list>
        /// </summary>
        static public string Create(string bankCode, string prefix)
        {
            return prefix + BacenID.Create(bankCode: bankCode, dateFormat: "yyyyMMdd");
        }
    }
}
