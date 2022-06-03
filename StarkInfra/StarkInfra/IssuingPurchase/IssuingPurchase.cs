using System;
using System.Collections.Generic;
using System.Linq;
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
    ///     <item>merchantFee [integer]: fee charged by the merchant to cover specific costs, such as ATM withdrawal logistics, etc. ex: 200 (= R$ 2.00)</item>
    ///     <item>WalletID [string]: virtual wallet ID. ex: "5656565656565656"</item>
    ///     <item>MethodCode [string]: method code. ex: "chip", "token", "server", "manual", "magstripe" or "contactless"</item>
    ///     <item>Score [float]: internal score calculated for the authenticity of the purchase. null in case of insufficient data. ex: 7.6</item>
    ///     <item>IssuingTransactionIds [string]: ledger transaction ids linked to this Purchase</item>
    ///     <item>EndToEndID [string]: Unique id used to identify the transaction through all of its life cycle, even before the purchase is denied or accepted and gets its usual id. Example: endToEndId="679cd385-642b-49d0-96b7-89491e1249a5"</item>
    ///     <item>Status [string]: current IssuingCard status. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
    ///     <item>Tags [list of strings]: list of strings for tagging returned by the sub-issuer during the authorization. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingPurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingPurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingPurchase : Resource
    {
        public string HolderName { get; }
        public string CardID { get; }
        public string CardEnding { get; }
        public long? Amount { get; }
        public string Tax { get; }
        public long? IssuerAmount { get; }
        public string IssuerCurrencyCode { get; }
        public string IssuerCurrencySymbol { get; }
        public string MerchantAmount { get; }
        public string MerchantCurrencyCode { get; }
        public string MerchantCurrencySymbol { get; }
        public string MerchantCategoryCode { get; }
        public string MerchantCountryCode { get; }
        public string AcquirerID { get; }
        public string MerchantID { get; }
        public string MerchantName { get; }
        public int? MerchantFee { get; }
        public string WalletID { get; }
        public string MethodCode { get; }
        public float? Score { get; }
        public string IssuingTransactionID { get; }
        public string EndToEndID { get; }
        public string Status { get; }
        public List<string> Tags { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingPurchase object
        /// <br/>
        /// Displays the IssuingPurchase objects created in your Workspace.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingPurchase is created. ex: "5656565656565656"</item>
        ///     <item>holderName [string]: card holder name. ex: "Tony Stark"</item>
        ///     <item>cardId [string]: unique id returned when IssuingCard is created. ex: "5656565656565656"</item>
        ///     <item>cardEnding [string]: last 4 digits of the card number. ex: "1234"</item>
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
        ///     <item>acquirerId [string]: acquirer ID. ex: "5656565656565656"</item>
        ///     <item>merchantId [string]: merchant ID. ex: "5656565656565656"</item>
        ///     <item>merchantName [string]: merchant name. ex: "Google Cloud Platform"</item>
        ///     <item>merchantFee [integer]: fee charged by the merchant to cover specific costs, such as ATM withdrawal logistics, etc. ex: 200 (= R$ 2.00)</item>
        ///     <item>walletId [string]: virtual wallet ID. ex: "5656565656565656"</item>
        ///     <item>methodCode [string]: method code. ex: "chip", "token", "server", "manual", "magstripe" or "contactless"</item>
        ///     <item>score [float]: internal score calculated for the authenticity of the purchase. null in case of insufficient data. ex: 7.6</item>
        ///     <item>issuingTransactionIds [string]: ledger transaction ids linked to this Purchase</item>
        ///     <item>endToEndId [string]: Unique id used to identify the transaction through all of its life cycle, even before the purchase is denied or accepted and gets its usual id. Example: endToEndId="679cd385-642b-49d0-96b7-89491e1249a5"</item>
        ///     <item>status [string]: current IssuingCard status. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>tags [list of strings]: list of strings for tagging returned by the sub-issuer during the authorization. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingPurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingPurchase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingPurchase(string id = null, string holderName = null, string cardId = null, string cardEnding = null, long? amount = null,
            string tax = null, long? issuerAmount = null, string issuerCurrencyCode = null, string issuerCurrencySymbol = null, 
            string merchantAmount = null, string merchantCurrencyCode = null, string merchantCurrencySymbol = null, string merchantCategoryCode = null, 
            string merchantCountryCode = null, string acquirerId = null, string merchantId = null, string merchantName = null, int? merchantFee = null,
            string walletId = null, string methodCode = null, float? score = null, string issuingTransactionId = null, string endToEndId = null,
            string status = null, List<string> tags = null, DateTime? updated = null, DateTime? created = null) : base(id)
        {
            HolderName = holderName;
            CardID = cardId;
            CardEnding = cardEnding;
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
            AcquirerID = acquirerId;
            MerchantID = merchantId;
            MerchantName = merchantName;
            MerchantFee = merchantFee;
            WalletID = walletId;
            MethodCode = methodCode;
            Score = score;
            IssuingTransactionID = issuingTransactionId;
            EndToEndID = endToEndId;
            Status = status;
            Tags = tags;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Retrieve a specific IssuingPurchase
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
        ///     <item>IssuingPurchase object with updated attributes</item>
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
        /// Retrieve IssuingPurchases
        /// <br/>
        /// Receive an IEnumerable of IssuingPurchase objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>endToEndIds [list of strings, default null]: Unique id used to identify the transaction through all of its life cycle, even before the purchase is denied or accepted and gets its usual id. List<string>{ ""679cd385-642b-49d0-96b7-89491e1249a5"" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>cardIds [list of strings, default null]: card  IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>after [DateTime or string, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>ids [list of strings, default null]: purchase IDs</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingPurchase objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingPurchase> Query(List<string> endToEndIds = null, List<string> holderIds = null,
            List<string> cardIds = null, string status = null, DateTime? after = null, DateTime? before = null, 
            List<string> ids = null, int? limit = 1, List<string> tags = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "endToEndIds" , endToEndIds },
                    { "holderIds" , holderIds },
                    { "cardIds" , cardIds },
                    { "status" , status },
                    { "after" , after },
                    { "before" , before },
                    { "ids" , ids },
                    { "limit" , limit },
                    { "tags", tags }
                },
                user: user
            ).Cast<IssuingPurchase>();
        }

        /// <summary>
        /// Retrieve paged IssuingPurchases
        /// <br/>
        /// Receive a list of up to 100 IssuingPurchase objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>endToEndIds [list of strings, default null]: Unique id used to identify the transaction through all of its life cycle, even before the purchase is denied or accepted and gets its usual id. List<string>{ ""679cd385-642b-49d0-96b7-89491e1249a5"" }</item>
        ///     <item>holderIds [list of strings, default null]: card holder IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>cardIds [list of strings, default null]: card  IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>after [DateTime or string, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>ids [list of strings, default null]: purchase IDs</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingPurchase objects with updated attributes and cursor to retrieve the next page of IssuingPurchase objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingPurchase> page, string pageCursor) Page(
            string cursor = null, List<string> endToEndIds = null, List<string> holderIds = null,
            List<string> cardIds = null, string status = null, DateTime? after = null, DateTime? before = null, 
            List<string> ids = null, int? limit = 1, List<string> tags = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "endToEndIds" , endToEndIds },
                    { "holderIds" , holderIds },
                    { "cardIds" , cardIds },
                    { "status" , status },
                    { "after" , after },
                    { "before" , before },
                    { "ids" , ids },
                    { "limit" , limit },
                    { "tags", tags }
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

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingPurchase", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string holderName = json.holderName;
            string cardId = json.cardId;
            string cardEnding = json.cardEnding;
            long amount = json.amount;
            string tax = json.tax;
            long issuerAmount = json.issuerAmount;
            string issuerCurrencyCode = json.issuerCurrencyCode;
            string issuerCurrencySymbol = json.issuerCurrencySymbol;
            string merchantAmount = json.merchantAmount;
            string merchantCurrencyCode = json.merchantCurrencyCode;
            string merchantCurrencySymbol = json.merchantCurrencySymbol;
            string merchantCategoryCode = json.merchantCategoryCode;
            string merchantCountryCode = json.merchantCountryCode;
            string acquirerId = json.acquirerId;
            string merchantId = json.merchantId;
            string merchantName = json.merchantName;
            int? merchantFee = json.merchantFee;
            string walletId = json.walletId;
            string methodCode = json.methodCode;
            float? score = json.score.ToObject<float?>();
            string issuingTransactionId = json.issuingTransactionId;
            string endToEndId = json.endToEndId;
            string status = json.status;
            List<string> tags = json.tags.ToObject<List<string>>();
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new IssuingPurchase(
                id: id, holderName: holderName, cardId: cardId, cardEnding: cardEnding, amount: amount,
                tax: tax, issuerAmount: issuerAmount, issuerCurrencyCode: issuerCurrencyCode,
                issuerCurrencySymbol: issuerCurrencySymbol, merchantAmount: merchantAmount,
                merchantCurrencyCode: merchantCurrencyCode, merchantCurrencySymbol: merchantCurrencySymbol,
                merchantCategoryCode: merchantCategoryCode, merchantCountryCode: merchantCountryCode, merchantFee: merchantFee,
                acquirerId: acquirerId, merchantId: merchantId, merchantName: merchantName, walletId: walletId,
                methodCode: methodCode, score: score, issuingTransactionId: issuingTransactionId,
                endToEndId: endToEndId, status: status, tags: tags, created: created, updated: updated
            );
        }
    }
}
