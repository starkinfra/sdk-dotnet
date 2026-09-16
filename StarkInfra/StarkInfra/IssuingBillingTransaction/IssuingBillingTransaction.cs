using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingBillingTransaction object
    /// <br/>
    /// The IssuingBillingTransaction objects created in your Workspace to represent each balance shift due to issuing usage.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when IssuingBillingTransaction is created. ex: "5656565656565656"</item>
    ///     <item>Amount [long]: transaction amount, in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>InvoiceID [string]: parent billing invoice id. May be null. ex: "5656565656565656"</item>
    ///     <item>Installment [long]: installment number of the transaction. ex: 1</item>
    ///     <item>InstallmentCount [long]: total installment count of the transaction. ex: 12</item>
    ///     <item>Balance [long]: remaining balance after the transaction, in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>HolderName [string]: card holder name. ex: "Tony Stark"</item>
    ///     <item>Source [string]: transaction source. ex: "issuing-purchase"</item>
    ///     <item>ExternalID [string]: external transaction id. ex: "my-external-id-123456"</item>
    ///     <item>Description [string]: transaction description. ex: "Issuing purchase at Iron Bank"</item>
    ///     <item>CardEnding [string]: last 4 digits of the card number. ex: "1234"</item>
    ///     <item>Tax [double]: IOF amount in cents applied to the transaction</item>
    ///     <item>Rate [double]: Conversion rate applied to international transactions</item>
    ///     <item>MerchantAmount [long]: merchant amount, in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>MerchantCurrencyCode [string]: merchant currency code (ISO 4217). ex: "USD"</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingBillingTransaction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingBillingTransaction : Resource
    {
        public long? Amount { get; }
        public string InvoiceID { get; }
        public long? Installment { get; }
        public long? InstallmentCount { get; }
        public long? Balance { get; }
        public string HolderName { get; }
        public string Source { get; }
        public string ExternalID { get; }
        public string Description { get; }
        public string CardEnding { get; }
        public double? Tax { get; }
        public double? Rate { get; }
        public long? MerchantAmount { get; }
        public string MerchantCurrencyCode { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingBillingTransaction object
        /// <br/>
        /// The IssuingBillingTransaction objects created in your Workspace to represent each balance shift due to issuing usage.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingBillingTransaction is created. ex: "5656565656565656"</item>
        ///     <item>amount [long]: transaction amount, in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>invoiceId [string]: parent billing invoice id. May be null. ex: "5656565656565656"</item>
        ///     <item>installment [long]: installment number of the transaction. ex: 1</item>
        ///     <item>installmentCount [long]: total installment count of the transaction. ex: 12</item>
        ///     <item>balance [long]: remaining balance after the transaction, in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>holderName [string]: card holder name. ex: "Tony Stark"</item>
        ///     <item>source [string]: transaction source. ex: "issuing-purchase"</item>
        ///     <item>externalId [string]: external transaction id. ex: "my-external-id-123456"</item>
        ///     <item>description [string]: transaction description. ex: "Issuing purchase at Iron Bank"</item>
        ///     <item>cardEnding [string]: last 4 digits of the card number. ex: "1234"</item>
        ///     <item>tax [double]: IOF amount in cents applied to the transaction</item>
        ///     <item>rate [double]: Conversion rate applied to international transactions</item>
        ///     <item>merchantAmount [long]: merchant amount, in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>merchantCurrencyCode [string]: merchant currency code (ISO 4217). ex: "USD"</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingBillingTransaction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingBillingTransaction(
            long? amount = null, string invoiceID = null, long? installment = null,
            long? installmentCount = null, long? balance = null, string holderName = null, string source = null,
            string externalID = null, string description = null, string cardEnding = null, double? tax = null,
            double? rate = null, long? merchantAmount = null, string merchantCurrencyCode = null,
            DateTime? created = null, string id = null
        ) : base(id)
        {
            Amount = amount;
            InvoiceID = invoiceID;
            Installment = installment;
            InstallmentCount = installmentCount;
            Balance = balance;
            HolderName = holderName;
            Source = source;
            ExternalID = externalID;
            Description = description;
            CardEnding = cardEnding;
            Tax = tax;
            Rate = rate;
            MerchantAmount = merchantAmount;
            MerchantCurrencyCode = merchantCurrencyCode;
            Created = created;
        }

        /// <summary>
        /// Retrieve IssuingBillingTransaction objects
        /// <br/>
        /// Receive an IEnumerable of IssuingBillingTransaction objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>invoiceID [string, default null]: filter for transactions of a specific billing invoice. ex: "5656565656565656"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingBillingTransaction objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingBillingTransaction> Query(int? limit = null, DateTime? after = null,
            DateTime? before = null, string invoiceID = null, List<string> tags = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "invoiceId", invoiceID },
                    { "tags", tags }
                },
                user: user
            ).Cast<IssuingBillingTransaction>();
        }

        /// <summary>
        /// Retrieve paged IssuingBillingTransaction objects
        /// <br/>
        /// Receive a list of up to 100 IssuingBillingTransaction objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>invoiceID [string, default null]: filter for transactions of a specific billing invoice. ex: "5656565656565656"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingBillingTransaction objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingBillingTransaction objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingBillingTransaction> page, string pageCursor) Page(string cursor = null,
            int? limit = null, DateTime? after = null, DateTime? before = null, string invoiceID = null,
            List<string> tags = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "invoiceId", invoiceID },
                    { "tags", tags }
                },
                user: user
            );
            List<IssuingBillingTransaction> transactions = new List<IssuingBillingTransaction>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                transactions.Add(subResource as IssuingBillingTransaction);
            }
            return (transactions, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingBillingTransaction", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long? amount = json.amount;
            string invoiceID = json.invoiceId;
            long? installment = json.installment;
            long? installmentCount = json.installmentCount;
            long? balance = json.balance;
            string holderName = json.holderName;
            string source = json.source;
            string externalID = json.externalId;
            string description = json.description;
            string cardEnding = json.cardEnding;
            double? tax = json.tax;
            double? rate = json.rate;
            long? merchantAmount = json.merchantAmount;
            string merchantCurrencyCode = json.merchantCurrencyCode;
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);

            return new IssuingBillingTransaction(
                id: id, amount: amount, invoiceID: invoiceID, installment: installment,
                installmentCount: installmentCount, balance: balance, holderName: holderName, source: source,
                externalID: externalID, description: description, cardEnding: cardEnding, tax: tax, rate: rate,
                merchantAmount: merchantAmount, merchantCurrencyCode: merchantCurrencyCode, created: created
            );
        }
    }
}
