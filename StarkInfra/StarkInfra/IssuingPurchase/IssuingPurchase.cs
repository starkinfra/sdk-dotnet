using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingPurchase object
    /// <br/>
    /// Displays the IssuingPurchase objects created in your Workspace.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when IssuingPurchase is created. ex: "5656565656565656"</item>
    ///     <item>HolderName [string]: card holder name. ex: "Tony Stark"</item>
    ///     <item>CardID [string]: unique id returned when IssuingCard is created. ex: "5656565656565656"</item>
    ///     <item>CardEnding [string]: last 4 digits of the card number. ex: "1234"</item>
    ///     <item>Purpose [string]: purchase purpose. ex: "purchase"</item>
    ///     <item>Amount [long]: IssuingPurchase value in cents. Minimum = 0. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Tax [integer]: IOF amount taxed for international purchases. ex: 1234 (= R$ 12.34)</item>
    ///     <item>IssuerAmount [long]: issuer amount. ex: 1234 (= R$ 12.34)</item>
    ///     <item>IssuerCurrencyCode [string]: issuer currency code. ex: "USD"</item>
    ///     <item>IssuerCurrencySymbol [string]: issuer currency symbol. ex: "$"</item>
    ///     <item>MerchantAmount [long]: merchant amount. ex: 1234 (= R$ 12.34)</item>
    ///     <item>MerchantCurrencyCode [string]: merchant currency code. ex: "USD"</item>
    ///     <item>MerchantCurrencySymbol [string]: merchant currency symbol. ex: "$"</item>
    ///     <item>MerchantCategoryCode [string]: merchant category code. ex: "fastFoodRestaurants"</item>
    ///     <item>MerchantCountryCode [string]: merchant country code. ex: "USA"</item>
    ///     <item>AcquirerID [string]: acquirer ID. ex: "5656565656565656"</item>
    ///     <item>MerchantID [string]: merchant ID. ex: "5656565656565656"</item>
    ///     <item>MerchantName [string]: merchant name. ex: "Google Cloud Platform"</item>
    ///     <item>metadata [dictionary]: dictionary object used to store additional information about the IssuingPurchase object. ex: { authorizationId: 'OjZAqj' }
    ///     <item>merchantFee [integer]: fee charged by the merchant to cover specific costs, such as ATM withdrawal logistics, etc. ex: 200 (= R$ 2.00)</item>
    ///     <item>WalletID [string]: virtual wallet ID. ex: "5656565656565656"</item>
    ///     <item>MethodCode [string]: method code. Options: "chip", "token", "server", "manual", "magstripe" or "contactless"</item>
    ///     <item>Score [float]: internal score calculated for the authenticity of the purchase. null in case of insufficient data. ex: 7.6</item>
    ///     <item>EndToEndID [string]: Unique id used to identify the transaction through all of its life cycle, even before the purchase is denied or accepted and gets its usual id. ex: "679cd385-642b-49d0-96b7-89491e1249a5"</item>
    ///     <item>Tags [list of strings]: list of strings for tagging returned by the sub-issuer during the authorization. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ZipCode [string]: zip code of the merchant location. ex: "02101234"</item>
    ///     <item>IssuingTransactionIds [string]: ledger transaction ids linked to this Purchase</item>
    ///     <item>Status [string]: current IssuingCard status. Options: "approved", "canceled", "denied", "confirmed" or "voided"</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the IssuingPurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the IssuingPurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>IsPartialAllowed [bool]: true if the merchant allows partial purchases. ex: False</item>
    ///     <item>CardTags [list of strings]: tags of the IssuingCard responsible for this purchase. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>HolderTags [list of strings]: tags of the IssuingHolder responsible for this purchase. new List<string>{ "technology", "john snow" }</item>
    /// </list>
    /// </summary>
    public partial class IssuingPurchase : Resource
    {
        public string HolderName { get; }
        public string CardID { get; }
        public string CardEnding { get; }
        public string Purpose { get; }
        public long? Amount { get; }
        public int? Tax { get; }
        public long? IssuerAmount { get; }
        public string IssuerCurrencyCode { get; }
        public string IssuerCurrencySymbol { get; }
        public long? MerchantAmount { get; }
        public string MerchantCurrencyCode { get; }
        public string MerchantCurrencySymbol { get; }
        public string MerchantCategoryCode { get; }
        public string MerchantCountryCode { get; }
        public string AcquirerID { get; }
        public string MerchantID { get; }
        public string MerchantName { get; }
        public Dictionary<string, object> Metadata { get; }
        public int? MerchantFee { get; }
        public string WalletID { get; }
        public string MethodCode { get; }
        public float? Score { get; }
        public string EndToEndID { get; }
        public List<string> Tags { get; }
        public string ZipCode { get; }
        public List<string> IssuingTransactionIds { get; }
        public string Status { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }
        public bool? IsPartialAllowed { get; }
        public List<string> CardTags { get; }
        public List<string> HolderTags { get; }

        /// <summary>
        /// IssuingPurchase object
        /// <br/>
        /// Displays the IssuingPurchase objects created in your Workspace.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingPurchase is created. ex: "5656565656565656"</item>
        ///     <item>holderName [string]: card holder name. ex: "Tony Stark"</item>
        ///     <item>cardID [string]: unique id returned when IssuingCard is created. ex: "5656565656565656"</item>
        ///     <item>cardEnding [string]: last 4 digits of the card number. ex: "1234"</item>
        ///     <item>purpose [string]: purchase purpose. ex: "purchase"</item>
        ///     <item>amount [long]: IssuingPurchase value in cents. Minimum = 0. ex: 1234 (= R$ 12.34)</item>
        ///     <item>tax [integer]: IOF amount taxed for international purchases. ex: 1234 (= R$ 12.34)</item>
        ///     <item>issuerAmount [long]: issuer amount. ex: 1234 (= R$ 12.34)</item>
        ///     <item>issuerCurrencyCode [string]: issuer currency code. ex: "USD"</item>
        ///     <item>issuerCurrencySymbol [string]: issuer currency symbol. ex: "$"</item>
        ///     <item>merchantAmount [long]: merchant amount. ex: 1234 (= R$ 12.34)</item>
        ///     <item>merchantCurrencyCode [string]: merchant currency code. ex: "USD"</item>
        ///     <item>merchantCurrencySymbol [string]: merchant currency symbol. ex: "$"</item>
        ///     <item>merchantCategoryCode [string]: merchant category code. ex: "fastFoodRestaurants"</item>
        ///     <item>merchantCountryCode [string]: merchant country code. ex: "USA"</item>
        ///     <item>acquirerID [string]: acquirer ID. ex: "5656565656565656"</item>
        ///     <item>merchantID [string]: merchant ID. ex: "5656565656565656"</item>
        ///     <item>merchantName [string]: merchant name. ex: "Google Cloud Platform"</item>
        ///     <item>metadata [dictionary]: dictionary object used to store additional information about the IssuingPurchase object. ex: { authorizationId: 'OjZAqj' }
        ///     <item>merchantFee [integer]: fee charged by the merchant to cover specific costs, such as ATM withdrawal logistics, etc. ex: 200 (= R$ 2.00)</item>
        ///     <item>walletID [string]: virtual wallet ID. ex: "5656565656565656"</item>
        ///     <item>methodCode [string]: method code. Options: "chip", "token", "server", "manual", "magstripe" or "contactless"</item>
        ///     <item>score [float]: internal score calculated for the authenticity of the purchase. null in case of insufficient data. ex: 7.6</item>
        ///     <item>endToEndID [string]: Unique id used to identify the transaction through all of its life cycle, even before the purchase is denied or accepted and gets its usual id. ex: "679cd385-642b-49d0-96b7-89491e1249a5"</item>
        ///     <item>tags [list of strings]: list of strings for tagging returned by the sub-issuer during the authorization. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>zipCode [string]: zip code of the merchant location. ex: "02101234"</item>
        /// </list>
        /// Attributes (IssuingPurchase only):
        /// <list>
        ///     <item>issuingTransactionIds [string]: ledger transaction ids linked to this Purchase</item>
        ///     <item>status [string]: current IssuingCard status. Options: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the IssuingPurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the IssuingPurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// Attributes (authorization request only):
        /// <list>
        ///     <item>isPartialAllowed [bool]: true if the merchant allows partial purchases. ex: False</item>
        ///     <item>cardTags [list of strings]: tags of the IssuingCard responsible for this purchase. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>holderTags [list of strings]: tags of the IssuingHolder responsible for this purchase. ex: new List<string>{ "technology", "john snow" }</item>
        /// </list>
        /// </summary>
        public IssuingPurchase(string id = null, string holderName = null,  string cardID = null,  string cardEnding = null,  
            string purpose = null, long? amount = null, int? tax = null, long? issuerAmount = null, string issuerCurrencyCode = null,  
            string issuerCurrencySymbol = null, long? merchantAmount = null,  string merchantCurrencyCode = null,  
            string merchantCurrencySymbol = null,  string merchantCategoryCode = null,  string merchantCountryCode = null,  
            string acquirerID = null,  string merchantID = null,  string merchantName = null, Dictionary<string, object> metadata = null, int? merchantFee = null,  
            string walletID = null,  string methodCode = null,  float? score = null,  string endToEndID = null,  List<string> tags = null,  
            string zipCode = null,  List<string> issuingTransactionIds = null, string status = null, DateTime? updated = null, 
            DateTime? created = null, bool? isPartialAllowed = null, List<string> cardTags = null, List<string> holderTags = null           
        ) : base(id)
        {
            HolderName = holderName;
            CardID = cardID;
            CardEnding = cardEnding;
            Purpose = purpose;
            Amount = amount;
            Tax = tax;
            IssuerAmount = issuerAmount;
            IssuerCurrencyCode = issuerCurrencyCode;
            IssuerCurrencySymbol = issuerCurrencySymbol;
            MerchantAmount = merchantAmount;
            MerchantCurrencyCode = merchantCurrencyCode;
            MerchantCurrencySymbol = merchantCurrencySymbol;
            MerchantCategoryCode = merchantCategoryCode;
            MerchantCountryCode = merchantCountryCode;
            AcquirerID = acquirerID;
            MerchantID = merchantID;
            MerchantName = merchantName;
            Metadata = metadata;
            MerchantFee = merchantFee;
            WalletID = walletID;
            MethodCode = methodCode;
            Score = score;
            EndToEndID = endToEndID;
            Tags = tags;
            ZipCode = zipCode;
            IssuingTransactionIds = issuingTransactionIds;
            Status = status;
            Updated = updated;
            Created = created;
            IsPartialAllowed = isPartialAllowed;
            CardTags = cardTags;
            HolderTags = holderTags;
        }

        /// <summary>
        /// Retrieve a specific IssuingPurchase by its id
        /// <br/>
        /// Receive a single IssuingPurchase object previously created in the Stark Infra API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingPurchase object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IssuingPurchase Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingPurchase;
        }

        /// <summary>
        /// Retrieve IssuingPurchase objects
        /// <br/>
        /// Receive an IEnumerable of IssuingPurchase objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>ids [list of strings, default null]: purchase IDs</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>endToEndIds [list of strings, default null]: central bank's unique transaction ID. List<string>{ ""679cd385-642b-49d0-96b7-89491e1249a5"" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>cardIds [list of strings, default null]: card  IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingPurchase objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingPurchase> Query(List<string> ids = null, int? limit = 1, DateTime? after = null, 
            DateTime? before = null, List<string> endToEndIds = null, List<string> holderIds = null,List<string> cardIds = null, 
            string status = null, User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "ids" , ids },
                    { "limit" , limit },
                    { "after" , after },
                    { "before" , before },
                    { "endToEndIds" , endToEndIds },
                    { "holderIds" , holderIds },
                    { "cardIds" , cardIds },
                    { "status" , status }
                },
                user: user
            ).Cast<IssuingPurchase>();
        }

        /// <summary>
        /// Retrieve paged IssuingPurchase objects
        /// <br/>
        /// Receive a list of up to 100 IssuingPurchase objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>ids [list of strings, default null]: purchase IDs</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>endToEndIds [list of strings, default null]: Unique id used to identify the transaction through all of its life cycle, even before the purchase is denied or accepted and gets its usual id. List<string>{ ""679cd385-642b-49d0-96b7-89491e1249a5"" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>cardIds [list of strings, default null]: card  IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingPurchase objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingPurchase objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingPurchase> page, string pageCursor) Page(
            string cursor = null, List<string> ids = null, int? limit = 1, DateTime? after = null, 
            DateTime? before = null, List<string> endToEndIds = null, List<string> holderIds = null,List<string> cardIds = null, 
            string status = null, User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "ids" , ids },
                    { "limit" , limit },
                    { "after" , after },
                    { "before" , before },
                    { "endToEndIds" , endToEndIds },
                    { "holderIds" , holderIds },
                    { "cardIds" , cardIds },
                    { "status" , status }
                },
                user: user
            );
            List<IssuingPurchase> purchases = new List<IssuingPurchase>();
            foreach (SubResource subResource in page)
            {
                purchases.Add(subResource as IssuingPurchase);
            }
            return (purchases, pageCursor);
        }

        /// <summary>
        /// Create a single verified IssuingPurchase authorization request from a content string
        /// <br/>
        /// Use this method to parse and verify the authenticity of the authorization request received at the informed endpoint.
        /// Authorization requests are posted to your registered endpoint whenever IssuingPurchases are received.
        /// They present IssuingPurchase data that must be analyzed and answered with approval or declination.
        /// If the provided digital signature does not check out with the StarkInfra public key, a Error.InvalidSignatureException will be raised.
        /// If the authorization request is not answered within 2 seconds or is not answered with an HTTP status code 200 the IssuingPurchase will go through the pre-configured stand-in validation.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>content [string]: response content from request received at user endpoint (not parsed)</item>
        ///     <item>signature [string]: base-64 digital signature received at response header "Digital-Signature"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Parsed IssuingPurchase object</item>
        /// </list>
        /// </summary>
        public static IssuingPurchase Parse(string content, string signature, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Parse.ParseAndVerify(
                content: content,
                signature: signature,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                user: user
            ) as IssuingPurchase;
        }

        /// <summary>
        /// Helps you respond to a IssuingPurchase authorization request
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>status [string]: sub-issuer response to the authorization. ex: "approved" or "denied"</item>
        ///</list>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>reason [string]: denial reason. Options: "other", "blocked", "lostCard", "stolenCard", "invalidPin", "invalidCard", "cardExpired", "issuerError", "concurrency", "standInDenial", "subIssuerError", "invalidPurpose", "invalidZipCode", "invalidWalletID", "inconsistentCard", "settlementFailed", "cardRuleMismatch", "invalidExpiration", "prepaidInstallment", "holderRuleMismatch", "insufficientBalance", "tooManyTransactions", "invalidSecurityCode", "invalidPaymentMethod", "confirmationDeadline", "withdrawalAmountLimit", "insufficientCardLimit", "insufficientHolderLimit"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>amount [integer, default null]: amount in cents that was authorized. ex: 1234 (= R$ 12.34)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved object. ex: new List<string>{ "tony", "stark" }</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Dumped JSON string that must be returned to us on the IssuingPurchase request</item>
        /// </list>
        /// </summary>
        public static string Response(string status, long? amount = null, string reason = null, List<string> tags = null)
        {
            Dictionary<string, object> rawResponse = new Dictionary<string, object>
            {
                {"authorization", new Dictionary<string, object>
                    {
                        {"status", status},
                        {"amount", amount},
                        {"reason", reason},
                        {"tags", tags}
                    }
                }
            };
            Dictionary<string, object> response = Api.CastJsonToApiFormat(rawResponse);
            return JsonConvert.SerializeObject(response);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingPurchase", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string holderName = json.holderName;
            string cardID = json.cardId;
            string cardEnding = json.cardEnding;
            string purpose = json.purpose;
            long amount = json.amount;
            int? tax = json.tax;
            long issuerAmount = json.issuerAmount;
            string issuerCurrencyCode = json.issuerCurrencyCode;
            string issuerCurrencySymbol = json.issuerCurrencySymbol;
            long? merchantAmount = json.merchantAmount;
            string merchantCurrencyCode = json.merchantCurrencyCode;
            string merchantCurrencySymbol = json.merchantCurrencySymbol;
            string merchantCategoryCode = json.merchantCategoryCode;
            string merchantCountryCode = json.merchantCountryCode;
            string acquirerID = json.acquirerId;
            string merchantID = json.merchantId;
            string merchantName = json.merchantName;
            Dictionary<string, object> metadata = json.metadata?.ToObject<Dictionary<string, object>>();
            int? merchantFee = json.merchantFee;
            string walletID = json.walletId;
            string methodCode = json.methodCode;
            float? score = json.score.ToObject<float?>();
            string endToEndID = json.endToEndId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string zipCode = json.zipCode;
            List<string> issuingTransactionIds = json.issuingTransactionIds?.ToObject<List<string>>();
            string status = json.status;
            string createdString = json.created;
            DateTime? created = Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = Checks.CheckNullableDateTime(updatedString);
            bool? isPartialAllowed = json.isPartialAllowed;
            List<string> cardTags = json.cardTags?.ToObject<List<string>>();
            List<string> holderTags = json.holderTags?.ToObject<List<string>>();

            return new IssuingPurchase(
                id: id, holderName: holderName, cardID: cardID, cardEnding: cardEnding,
                purpose: purpose, amount: amount, tax: tax, issuerAmount: issuerAmount,
                issuerCurrencyCode: issuerCurrencyCode, issuerCurrencySymbol: issuerCurrencySymbol,
                merchantAmount: merchantAmount, merchantCurrencyCode: merchantCurrencyCode,
                merchantCurrencySymbol: merchantCurrencySymbol, merchantCategoryCode: merchantCategoryCode,
                merchantCountryCode: merchantCountryCode, acquirerID: acquirerID,
                merchantID: merchantID, merchantName: merchantName, metadata: metadata, merchantFee: merchantFee,
                walletID: walletID, methodCode: methodCode, score: score, endToEndID: endToEndID,
                tags: tags, zipCode: zipCode, issuingTransactionIds: issuingTransactionIds,
                status: status, created: created, updated: updated, isPartialAllowed: isPartialAllowed,
                cardTags: cardTags, holderTags: holderTags
            ); ; ;
        }
    }
}
 