using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;
using Newtonsoft.Json.Linq;

namespace StarkInfra
{
    /// <summary>
    /// PixDispute object
    /// <br/>
    /// Pix disputes can be created when a fraud is detected creating a chain of transactions
    /// in order to reverse the funds to the origin.
    /// <br/>
    /// When you initialize a PixDispute, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///    <item>ReferenceID [string]: endToEndId of the transaction being reported. ex: "E20018183202201201450u34sDGd19lz"</item>
    ///    <item>Method [string]: method used to perform the fraudulent action. Options: "scam", "unauthorized", "coercion", "invasion", "other"</item>
    ///    <item>Description [string]: description including any details that can help with the dispute investigation. The description parameter is required when method is "other".</item>
    ///    <item>OperatorEmail [string]: contact email of the operator responsible for the dispute.</item>
    ///    <item>OperatorPhone [string]: contact phone number of the operator responsible for the dispute.</item>
    ///    <item>Tags [list of strings]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///    <item>MinTransactionAmount [long]: minimum transaction amount to be considered for the graph creation.</item>
    ///    <item>MaxTransactionCount [long]: maximum number of transactions to be considered for the graph creation.</item>
    ///    <item>MaxHopInterval [long]: mean time between transactions to be considered for the graph creation.</item>
    ///    <item>MaxHopCount [long]: depth to be considered for the graph creation.</item>
    ///    <item>ID [string]: unique id returned when the PixDispute is created. ex: "5656565656565656"</item>
    ///    <item>BacenID [string]: Central Bank's unique dispute id. ex: "817fc523-9e9d-40ab-9e53-dacb71454a05"</item>
    ///    <item>Flow [string]: indicates the flow of the Pix Dispute. Options: "in" if you received the PixDispute, "out" if you created the PixDispute.</item>
    ///    <item>Status [string]: current PixDispute status. Options: "created", "delivered", "analysed", "processing", "closed", "failed", "canceled".</item>
    ///    <item>Transactions [list of PixDispute.Transaction objects]: list of transactions related to the dispute.</item>
    ///    <item>Created [DateTime]: creation datetime for the PixDispute. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///    <item>Updated [DateTime]: latest update datetime for the PixDispute. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixDispute : Utils.Resource
    {
        public string ReferenceID { get; }
        public string Method { get; }
        public string Description { get; }
        public string OperatorEmail { get; }
        public string OperatorPhone { get; }
        public List<string> Tags { get; }
        public long? MinTransactionAmount { get; }
        public long? MaxTransactionCount { get; }
        public long? MaxHopInterval { get; }
        public long? MaxHopCount { get; }
        public string BacenID { get; }
        public string Flow { get; }
        public string Status { get; }
        public List<Transaction> Transactions { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixDispute object
        /// <br/>
        /// When you initialize a PixDispute, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///    <item>referenceID [string]: endToEndId of the transaction being reported. ex: "E20018183202201201450u34sDGd19lz"</item>
        ///    <item>method [string]: method used to perform the fraudulent action. Options: "scam", "unauthorized", "coercion", "invasion", "other"</item>
        ///    <item>operatorEmail [string]: contact email of the operator responsible for the dispute.</item>
        ///    <item>operatorPhone [string]: contact phone number of the operator responsible for the dispute.</item>
        /// </list>
        /// Parameters (conditionally required):
        /// <list>
        ///    <item>description [string, default null]: description including any details that can help with the dispute investigation. The description parameter is required when method is "other".</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///    <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        ///    <item>minTransactionAmount [long]: minimum transaction amount to be considered for the graph creation.</item>
        ///    <item>maxTransactionCount [long]: maximum number of transactions to be considered for the graph creation.</item>
        ///    <item>maxHopInterval [long]: mean time between transactions to be considered for the graph creation.</item>
        ///    <item>maxHopCount [long]: depth to be considered for the graph creation.</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///    <item>id [string]: unique id returned when the PixDispute is created. ex: "5656565656565656"</item>
        ///    <item>bacenID [string]: Central Bank's unique dispute id. ex: "817fc523-9e9d-40ab-9e53-dacb71454a05"</item>
        ///    <item>flow [string]: indicates the flow of the Pix Dispute. Options: "in" if you received the PixDispute, "out" if you created the PixDispute.</item>
        ///    <item>status [string]: current PixDispute status. Options: "created", "delivered", "analysed", "processing", "closed", "failed", "canceled".</item>
        ///    <item>transactions [list of PixDispute.Transaction objects]: list of transactions related to the dispute.</item>
        ///    <item>created [DateTime]: creation datetime for the PixDispute. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///    <item>updated [DateTime]: latest update datetime for the PixDispute. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixDispute(string referenceID, string method, string operatorEmail, string operatorPhone,
            string description = null, List<string> tags = null, long? minTransactionAmount = null,
            long? maxTransactionCount = null, long? maxHopInterval = null, long? maxHopCount = null,
            string id = null, string bacenID = null, string flow = null, string status = null,
            List<Transaction> transactions = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            ReferenceID = referenceID;
            Method = method;
            Description = description;
            OperatorEmail = operatorEmail;
            OperatorPhone = operatorPhone;
            Tags = tags;
            MinTransactionAmount = minTransactionAmount;
            MaxTransactionCount = maxTransactionCount;
            MaxHopInterval = maxHopInterval;
            MaxHopCount = maxHopCount;
            BacenID = bacenID;
            Flow = flow;
            Status = status;
            Transactions = transactions;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create PixDispute objects
        /// <br/>
        /// Send a list of PixDispute objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>disputes [list of PixDispute objects]: list of PixDispute objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixDispute objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixDispute> Create(List<PixDispute> disputes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: disputes,
                user: user
            ).ToList().ConvertAll(o => (PixDispute)o);
        }

        /// <summary>
        /// Create PixDispute objects
        /// <br/>
        /// Send a list of PixDispute objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>disputes [list of dictionaries]: list of dictionaries representing the PixDispute objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixDispute objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixDispute> Create(List<Dictionary<string, object>> disputes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: disputes,
                user: user
            ).ToList().ConvertAll(o => (PixDispute)o);
        }

        /// <summary>
        /// Retrieve a specific PixDispute by its id
        /// <br/>
        /// Receive a single PixDispute object previously created in the Stark Infra API by passing its id
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
        ///     <item>PixDispute object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixDispute Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixDispute;
        }

        /// <summary>
        /// Retrieve PixDispute objects
        /// <br/>
        /// Receive an IEnumerable of PixDispute objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. Options: "created", "delivered", "analysed", "processing", "closed", "failed", "canceled".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixDispute objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixDispute> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> ids = null, List<string> tags = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "ids", ids },
                    { "tags", tags }
                },
                user: user
            ).Cast<PixDispute>();
        }

        /// <summary>
        /// Retrieve paged PixDispute objects
        /// <br/>
        /// Receive a list of up to 100 PixDispute objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your disputes.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. Options: "created", "delivered", "analysed", "processing", "closed", "failed", "canceled".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixDispute objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixDispute objects</item>
        /// </list>
        /// </summary>
        public static (List<PixDispute> page, string pageCursor) Page(string cursor = null,
            int? limit = null, DateTime? after = null, DateTime? before = null, List<string> status = null,
            List<string> ids = null, List<string> tags = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "ids", ids },
                    { "tags", tags }
                },
                user: user
            );
            List<PixDispute> disputes = new List<PixDispute>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                disputes.Add(subResource as PixDispute);
            }
            return (disputes, pageCursor);
        }

        /// <summary>
        /// Cancel a PixDispute
        /// <br/>
        /// Cancel a PixDispute entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: PixDispute unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled PixDispute object</item>
        /// </list>
        /// </summary>
        public static PixDispute Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixDispute;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixDispute", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string referenceID = json.referenceId;
            string method = json.method;
            string description = json.description;
            string operatorEmail = json.operatorEmail;
            string operatorPhone = json.operatorPhone;
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            long? minTransactionAmount = json.minTransactionAmount;
            long? maxTransactionCount = json.maxTransactionCount;
            long? maxHopInterval = json.maxHopInterval;
            long? maxHopCount = json.maxHopCount;
            string id = json.id;
            string bacenID = json.bacenId;
            string flow = json.flow;
            string status = json.status;
            List<Transaction> transactions = ParseTransactions(json.transactions);
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixDispute(
                referenceID: referenceID, method: method, operatorEmail: operatorEmail,
                operatorPhone: operatorPhone, description: description, tags: tags,
                minTransactionAmount: minTransactionAmount, maxTransactionCount: maxTransactionCount,
                maxHopInterval: maxHopInterval, maxHopCount: maxHopCount, id: id,
                bacenID: bacenID, flow: flow, status: status, transactions: transactions,
                created: created, updated: updated
            );
        }

        private static List<Transaction> ParseTransactions(dynamic json)
        {
            List<Transaction> transactions = new List<Transaction>();
            foreach (dynamic transactionJson in json)
            {
                transactions.Add(Transaction.ResourceMaker(transactionJson));
            }
            return transactions;
        }
    }
}
