using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// LedgerTransaction object
    /// <br/>
    /// LedgerTransactions are used to track the balance of a given amount by inserting LedgerTransactions to them.
    /// They can represent a bank account, a digital wallet, an inventory product, etc.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long integer]: amount of the transaction. ex: 11234</item>
    ///     <item>LedgerID [string]: id of the Ledger containing the transaction. ex: "5656565656565656"</item>
    ///     <item>ExternalID [string]: string that must be unique among all your LedgerTransactions in a single Ledger. ex: "my-internal-id-123456"</item>
    ///     <item>Source [string]: source of the LedgerTransaction. ex: "bank-transfer/123"</item>
    ///     <item>Fee [long integer, default null]: fee applied to the LedgerTransaction. ex: 100</item>
    ///     <item>Rules [list of Ledger.Rule objects, default null]: list of Rule objects linked to the LedgerTransaction. Rules are used to overwrite the Ledger's rules for this transaction. ex: new List&lt;Ledger.Rule&gt;{ new Ledger.Rule(key: "minimumBalance", value: 0) }</item>
    ///     <item>Metadata [Dictionary object, default null]: dictionary object used to store additional information about the LedgerTransaction object. ex: new Dictionary&lt;string, object&gt;{ { "orderId", "123" }, { "orderType", "purchase" } }</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for LedgerTransactions. ex: new List&lt;string&gt;{ "transfer/123", "savings" }</item>
    ///     <item>Created [DateTime, default null]: datetime to backdate the transaction, used to import existing transaction history. Cannot be in the future; when creating multiple transactions in one request, their created values must be in chronological order. Defaults to the current datetime when omitted.</item>
    ///     <item>ID [string]: unique id returned when the LedgerTransaction is created. ex: "5656565656565656"</item>
    ///     <item>Balance [long integer]: Ledger's balance after the transaction. ex: 11234</item>
    /// </list>
    /// </summary>
    public partial class LedgerTransaction : Resource
    {
        public long Amount { get; }
        public string LedgerID { get; }
        public string ExternalID { get; }
        public string Source { get; }
        public long? Fee { get; }
        public List<Ledger.Rule> Rules { get; }
        public Dictionary<string, object> Metadata { get; }
        public List<string> Tags { get; }
        public DateTime? Created { get; }
        public long? Balance { get; }

        /// <summary>
        /// LedgerTransaction object
        /// <br/>
        /// LedgerTransactions are used to track the balance of a given amount by inserting LedgerTransactions to them.
        /// They can represent a bank account, a digital wallet, an inventory product, etc.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long integer]: amount of the transaction. ex: 11234</item>
        ///     <item>ledgerID [string]: id of the Ledger containing the transaction. ex: "5656565656565656"</item>
        ///     <item>externalID [string]: string that must be unique among all your LedgerTransactions in a single Ledger. ex: "my-internal-id-123456"</item>
        ///     <item>source [string]: source of the LedgerTransaction. ex: "bank-transfer/123"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>fee [long integer, default null]: fee applied to the LedgerTransaction. ex: 100</item>
        ///     <item>rules [list of Ledger.Rule objects, default null]: list of Rule objects linked to the LedgerTransaction. Rules are used to overwrite the Ledger's rules for this transaction. ex: new List&lt;Ledger.Rule&gt;{ new Ledger.Rule(key: "minimumBalance", value: 0) }</item>
        ///     <item>metadata [Dictionary object, default null]: dictionary object used to store additional information about the LedgerTransaction object. ex: new Dictionary&lt;string, object&gt;{ { "orderId", "123" }, { "orderType", "purchase" } }</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for LedgerTransactions. ex: new List&lt;string&gt;{ "transfer/123", "savings" }</item>
        ///     <item>created [DateTime, default null]: datetime to backdate the transaction, used to import existing transaction history. Cannot be in the future; when creating multiple transactions in one request, their created values must be in chronological order. Defaults to the current datetime when omitted.</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the LedgerTransaction is created. ex: "5656565656565656"</item>
        ///     <item>balance [long integer]: Ledger's balance after the transaction. ex: 11234</item>
        /// </list>
        /// </summary>
        public LedgerTransaction(
            long amount, string ledgerID, string externalID, string source, long? fee = null,
            List<Ledger.Rule> rules = null, Dictionary<string, object> metadata = null,
            List<string> tags = null, DateTime? created = null, string id = null, long? balance = null
        ) : base(id)
        {
            Amount = amount;
            LedgerID = ledgerID;
            ExternalID = externalID;
            Source = source;
            Fee = fee;
            Rules = rules;
            Metadata = metadata;
            Tags = tags;
            Created = created;
            Balance = balance;
        }

        /// <summary>
        /// Create LedgerTransactions
        /// <br/>
        /// Send a list of LedgerTransaction objects for creation at the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>transactions [list of LedgerTransaction objects]: list of LedgerTransaction objects to be created in the Stark Infra API. You can send up to 500 objects in a single request, targeting different ledgers if needed; each is applied to its Ledger in the order sent, and the resulting balance is returned for each one.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of LedgerTransaction objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<LedgerTransaction> Create(List<LedgerTransaction> transactions, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: transactions,
                user: user
            ).ToList().ConvertAll(o => (LedgerTransaction)o);
        }

        /// <summary>
        /// Create LedgerTransactions
        /// <br/>
        /// Send a list of LedgerTransaction dictionaries for creation at the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>transactions [list of dictionaries]: list of dictionaries representing the LedgerTransaction objects to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of LedgerTransaction objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<LedgerTransaction> Create(List<Dictionary<string, object>> transactions, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: transactions,
                user: user
            ).ToList().ConvertAll(o => (LedgerTransaction)o);
        }

        /// <summary>
        /// Retrieve a specific LedgerTransaction
        /// <br/>
        /// Receive a single LedgerTransaction object previously created in the Stark Infra API by its id
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
        ///     <item>LedgerTransaction object with updated attributes</item>
        /// </list>
        /// </summary>
        public static LedgerTransaction Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as LedgerTransaction;
        }

        /// <summary>
        /// Retrieve LedgerTransactions
        /// <br/>
        /// Receive an IEnumerable of LedgerTransaction objects previously created in the Stark Infra API.
        /// Either ledgerID or ids must be provided. If both are sent, the query will be filtered by both.
        /// <br/>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>ledgerID [string, default null]: id of the Ledger containing the transaction. Either ledgerID or ids must be provided. If both are sent, the query will be filtered by both. ex: "5656565656565656"</item>
        ///     <item>ids [list of strings, default null]: list of LedgerTransaction ids to filter retrieved objects. Either ledgerID or ids must be provided. If both are sent, the query will be filtered by both. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>flow [string, default null]: direction of the transaction. ex: "in" or "out"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List&lt;string&gt;{ "transfer/123", "savings" }</item>
        ///     <item>externalIds [list of strings, default null]: list of LedgerTransaction external ids to filter retrieved objects. ex: new List&lt;string&gt;{ "my-internal-id-123456", "my-internal-id-654321" }</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>limit [integer, default 100, maximum 1000]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of LedgerTransaction objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<LedgerTransaction> Query(
            string ledgerID = null, string flow = null, List<string> tags = null, List<string> externalIds = null,
            DateTime? after = null, DateTime? before = null, List<string> ids = null, int? limit = null, User user = null
        )
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "ledgerId", ledgerID },
                    { "flow", flow },
                    { "tags", tags },
                    { "externalIds", externalIds },
                    { "after", after },
                    { "before", before },
                    { "ids", ids },
                    { "limit", limit }
                },
                user: user
            ).Cast<LedgerTransaction>();
        }

        /// <summary>
        /// Retrieve paged LedgerTransactions
        /// <br/>
        /// Receive a list of LedgerTransaction objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// Either ledgerID or ids must be provided. If both are sent, the query will be filtered by both.
        /// <br/>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>ledgerID [string, default null]: id of the Ledger containing the transaction. Either ledgerID or ids must be provided. If both are sent, the query will be filtered by both. ex: "5656565656565656"</item>
        ///     <item>ids [list of strings, default null]: list of LedgerTransaction ids to filter retrieved objects. Either ledgerID or ids must be provided. If both are sent, the query will be filtered by both. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>flow [string, default null]: direction of the transaction. ex: "in" or "out"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List&lt;string&gt;{ "transfer/123", "savings" }</item>
        ///     <item>externalIds [list of strings, default null]: list of LedgerTransaction external ids to filter retrieved objects. ex: new List&lt;string&gt;{ "my-internal-id-123456", "my-internal-id-654321" }</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>limit [integer, default 100, maximum 1000]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of LedgerTransaction objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of LedgerTransaction objects</item>
        /// </list>
        /// </summary>
        public static (List<LedgerTransaction> page, string pageCursor) Page(
            string ledgerID = null, string flow = null, List<string> tags = null, List<string> externalIds = null,
            DateTime? after = null, DateTime? before = null, List<string> ids = null, int? limit = null,
            string cursor = null, User user = null
        )
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "ledgerId", ledgerID },
                    { "flow", flow },
                    { "tags", tags },
                    { "externalIds", externalIds },
                    { "after", after },
                    { "before", before },
                    { "ids", ids },
                    { "limit", limit },
                    { "cursor", cursor }
                },
                user: user
            );
            List<LedgerTransaction> transactions = new List<LedgerTransaction>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                transactions.Add(subResource as LedgerTransaction);
            }
            return (transactions, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "LedgerTransaction", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string ledgerID = json.ledgerId;
            string externalID = json.externalId;
            string source = json.source;
            string id = json.id;
            long? balance = json.balance;
            long? fee = json.fee;
            List<Ledger.Rule> rules = json.rules is null ? new List<Ledger.Rule> { } : ParseRules(json.rules);
            Dictionary<string, object> metadata = json.metadata?.ToObject<Dictionary<string, object>>();
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            string createdString = json.created;
            DateTime? created = string.IsNullOrEmpty(createdString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(createdString);

            return new LedgerTransaction(
                amount: amount, ledgerID: ledgerID, externalID: externalID, source: source, id: id,
                balance: balance, fee: fee, rules: rules, metadata: metadata, tags: tags, created: created
            );
        }

        private static List<Ledger.Rule> ParseRules(dynamic json)
        {
            List<Ledger.Rule> rules = new List<Ledger.Rule>();
            foreach (dynamic rule in json)
            {
                rules.Add(Ledger.Rule.ResourceMaker(rule));
            }
            return rules;
        }
    }
}
