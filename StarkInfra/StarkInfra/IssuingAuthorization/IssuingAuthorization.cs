using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingAuthorization object
    /// <br/>
    /// An IssuingAuthorization presents purchase data to be analysed and answered with an approval or a declination.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>EndToEndID [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
    ///     <item>Amount [long]: IssuingPurchase value in cents. Minimum = 0. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Tax [integer]: IOF amount taxed for international purchases. ex: 1234 (= R$ 12.34)</item>
    ///     <item>CardID [string]: unique id returned when IssuingCard is created. ex: "5656565656565656"</item>
    ///     <item>IssuerAmount [long]: issuer amount. ex: 1234 (= R$ 12.34)</item>
    ///     <item>IssuerCurrencyCode [string]: issuer currency code. ex: "USD"</item>
    ///     <item>MerchantAmount [long]: merchant amount. ex: 1234 (= R$ 12.34)</item>
    ///     <item>MerchantCurrencyCode [string]: merchant currency code. ex: "USD"</item>
    ///     <item>MerchantCategoryCode [string]: merchant category code. ex: "fastFoodRestaurants"</item>
    ///     <item>MerchantCountryCode [string]: merchant country code. ex: "USA"</item>
    ///     <item>AcquirerID [string]: acquirer ID. ex: "5656565656565656"</item>
    ///     <item>MerchantID [string]: merchant ID. ex: "5656565656565656"</item>
    ///     <item>MerchantName [string]: merchant name. ex: "Google Cloud Platform"</item>
    ///     <item>MerchantFee [integer]: merchant fee charged. ex: 200 (= R$ 2.00)</item>
    ///     <item>WalletID [string]: virtual wallet ID. ex: "googlePay"</item>
    ///     <item>MethodCode [string]: method code. ex: "chip", "token", "server", "manual", "magstripe" or "contactless"</item>
    ///     <item>Score [float]: internal score calculated for the authenticity of the purchase. null in case of insufficient data. ex: 7.6</item>
    ///     <item>IsPartialAllowed [bool]: true if the the merchant allows partial purchases. ex: False</item>
    ///     <item>Purpose [string]: purchase purpose. ex: "purchase"</item>
    ///     <item>CardTags [list of strings]: tags of the IssuingCard responsible for this purchase. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>HolderTags [list of strings]: tags of the IssuingHolder responsible for this purchase. ex: new List<string>{ "technology", "john snow" }</item>
    /// </list>
    /// </summary>
    public partial class IssuingAuthorization : Resource
    {
        public string EndToEndID { get; }
        public long Amount { get; }
        public int Tax { get; }
        public string CardID { get; }
        public long IssuerAmount { get; }
        public string IssuerCurrencyCode { get; }
        public long MerchantAmount { get; }
        public string MerchantCurrencyCode { get; }
        public string MerchantCategoryCode { get; }
        public string MerchantCountryCode { get; }
        public string AcquirerID { get; }
        public string MerchantID { get; }
        public string MerchantName { get; }
        public int MerchantFee { get; }
        public string WalletID { get; }
        public string MethodCode { get; }
        public float? Score { get; }
        public bool? IsPartialAllowed { get; }
        public string Purpose { get; }
        public List<string> CardTags { get; }
        public List<string> HolderTags { get; }

        /// <summary>
        /// IssuingAuthorization object
        /// <br/>
        /// An IssuingAuthorization presents purchase data to be analysed and answered with an approval or a declination.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>endToEndId [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
        ///     <item>amount [long]: IssuingPurchase value in cents. Minimum = 0. ex: 1234 (= R$ 12.34)</item>
        ///     <item>tax [integer]: IOF amount taxed for international purchases. ex: 1234 (= R$ 12.34)</item>
        ///     <item>cardId [string]: unique id returned when IssuingCard is created. ex: "5656565656565656"</item>
        ///     <item>issuerAmount [long]: issuer amount. ex: 1234 (= R$ 12.34)</item>
        ///     <item>issuerCurrencyCode [string]: issuer currency code. ex: "USD"</item>
        ///     <item>merchantAmount [long]: merchant amount. ex: 1234 (= R$ 12.34)</item>
        ///     <item>merchantCurrencyCode [string]: merchant currency code. ex: "USD"</item>
        ///     <item>merchantCategoryCode [string]: merchant category code. ex: "fastFoodRestaurants"</item>
        ///     <item>merchantCountryCode [string]: merchant country code. ex: "USA"</item>
        ///     <item>acquirerId [string]: acquirer ID. ex: "5656565656565656"</item>
        ///     <item>merchantId [string]: merchant ID. ex: "5656565656565656"</item>
        ///     <item>merchantName [string]: merchant name. ex: "Google Cloud Platform"</item>
        ///     <item>merchantFee [integer]: merchant fee charged. ex: 200 (= R$ 2.00)</item>
        ///     <item>walletId [string]: virtual wallet ID. ex: "googlePay"</item>
        ///     <item>methodCode [string]: method code. ex: "chip", "token", "server", "manual", "magstripe" or "contactless"</item>
        ///     <item>score [float]: internal score calculated for the authenticity of the purchase. null in case of insufficient data. ex: 7.6</item>
        ///     <item>isPartialAllowed [bool]: true if the the merchant allows partial purchases. ex: False</item>
        ///     <item>purpose [string]: purchase purpose. ex: "purchase"</item>
        ///     <item>cardTags [list of strings]: tags of the IssuingCard responsible for this purchase. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>holderTags [list of strings]: tags of the IssuingHolder responsible for this purchase. ex: new List<string>{ "technology", "john snow" }</item>
        /// </list>
        /// </summary>
        public IssuingAuthorization(string id, string endToEndId, long amount, int tax, string cardId, long issuerAmount, string issuerCurrencyCode,
            long merchantAmount, string merchantCurrencyCode, string merchantCategoryCode, string merchantCountryCode, string acquirerId, string merchantId,
            string merchantName, int merchantFee, string walletId, string methodCode, float? score, bool? isPartialAllowed, string purpose, List<string> cardTags,
            List<string> holderTags
        ) : base(id)
        {
            EndToEndID = endToEndId;
            Amount = amount;
            Tax = tax;
            CardID = cardId;
            IssuerAmount = issuerAmount;
            IssuerCurrencyCode = issuerCurrencyCode;
            MerchantAmount = merchantAmount;
            MerchantCurrencyCode = merchantCurrencyCode;
            MerchantCategoryCode = merchantCategoryCode;
            MerchantCountryCode = merchantCountryCode;
            AcquirerID = acquirerId;
            MerchantID = merchantId;
            MerchantName = merchantName;
            MerchantFee = merchantFee;
            WalletID = walletId;
            MethodCode = methodCode;
            Score = score;
            IsPartialAllowed = isPartialAllowed;
            Purpose = purpose;
            CardTags = cardTags;
            HolderTags = holderTags;
        }

        /// <summary>
        /// Create single IssuingAuthorization from a content string
        /// <br/>
        /// Create a single IssuingAuthorization object received from IssuingAuthorization at the informed endpoint.
        /// If the provided digital signature does not check out with the StarkInfra public key, a stark.exception.InvalidSignatureException will be raised.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>content [string]: response content from request received at user endpoint (not parsed)</item>
        ///     <item>signature [string]: base-64 digital signature received at response header "Digital-Signature"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Parsed IssuingAuthorization object</item>
        /// </list>
        /// </summary>
        public static IssuingAuthorization ParseContent(string content, string signature, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Parse.ParseAndVerify(content, signature, resourceName, resourceMaker, user) as IssuingAuthorization;;
        }

        /// <summary>
        /// Helps you respond IssuingAuthorization requests.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>status [string]: sub-issuer response to the authorization. ex: "accepted" or "denied"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>amount [integer, default 0]: amount in cents that was authorized. ex: 1234 (= R$ 12.34)</item>
        ///     <item>reason [string, default ""]: denial reason. ex: "other"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved object. ex: new List<string> { "tony", "stark" }</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Dumped JSON string that must be returned to us on the IssuingAuthorization request</item>
        /// </list>
        /// </summary>
        public static string Response(string status, long amount = 0, string reason = "", List<string> tags = null)
        {
            if(tags == null) tags = new List<string>();

            Dictionary<string, object> json = new Dictionary<string, object>
            {
                { "status", status },
                { "amount", amount },
                { "reason", reason },
                { "tags", tags }
            };

            return JsonConvert.SerializeObject(json);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingAuthorization", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string endToEndId = json.endToEndId;
            long amount = json.amount;
            int tax = json.tax;
            string cardId = json.cardId;
            long issuerAmount = json.issuerAmount;
            string issuerCurrencyCode = json.issuerCurrencyCode;
            long merchantAmount = json.merchantAmount;
            string merchantCurrencyCode = json.merchantCurrencyCode;
            string merchantCategoryCode = json.merchantCategoryCode;
            string merchantCountryCode = json.merchantCountryCode;
            string acquirerId = json.acquirerId;
            string merchantId = json.merchantId;
            string merchantName = json.merchantName;
            int merchantFee = json.merchantFee;
            string walletId = json.walletId;
            string methodCode = json.methodCode;
            float? score = json.score.ToObject<float?>();
            bool? isPartialAllowed = json.isPartialAllowed.ToObject<bool?>();
            string purpose = json.purpose;
            List<string> cardTags = json.cardTags.ToObject<List<string>>();
            List<string> holderTags = json.holderTags.ToObject<List<string>>();

            return new IssuingAuthorization(
                id: id, endToEndId: endToEndId, amount: amount, tax: tax, cardId: cardId, issuerAmount: issuerAmount, issuerCurrencyCode: issuerCurrencyCode,
                merchantAmount: merchantAmount, merchantCurrencyCode: merchantCurrencyCode, merchantCategoryCode: merchantCategoryCode,
                merchantCountryCode: merchantCountryCode, acquirerId: acquirerId, merchantId: merchantId, merchantName: merchantName, merchantFee: merchantFee,
                walletId: walletId, methodCode: methodCode, score: score, isPartialAllowed: isPartialAllowed, purpose: purpose, cardTags: cardTags,
                holderTags: holderTags
            );
        }
    }
}
