using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingWithdrawal object
    /// <br/>
    /// The IssuingWithdrawal objects created in your Workspace return cash from your Issuing balance to your Banking balance.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long]: IssuingWithdrawal value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
    ///     <item>ExternalID [string] IssuingWithdrawal external ID. ex: "12345"</item>
    ///     <item>Description [string]: IssuingWithdrawal description. ex: "sending money back"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "tony", "stark" }</item>
    ///     <item>ID[string]: unique id returned when IssuingWithdrawal is created. ex: "5656565656565656"</item>
    ///     <item>TransactionID [string]: Stark Bank ledger transaction ids linked to this IssuingWithdrawal</item>
    ///     <item>IssuingTransactionID [string]: issuing ledger transaction ids linked to this IssuingWithdrawal</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the IssuingWithdrawal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the IssuingWithdrawal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingWithdrawal : Resource
    {
        public long Amount { get; }
        public string Description { get; }
        public string TransactionID { get; }
        public string IssuingTransactionID { get; }
        public string ExternalID { get; }
        public List<string> Tags { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingWithdrawal object
        /// <br/>
        /// The IssuingWithdrawal objects created in your Workspace return cash from your Issuing balance to your Banking balance.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [integer]: IssuingWithdrawal value in cents. Minimum = 0 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
        ///     <item>externalID [string] IssuingWithdrawal external ID. ex: "12345"</item>
        ///     <item>description [string]: IssuingWithdrawal description. ex: "sending money back"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "tony", "stark" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingWithdrawal is created. ex: "5656565656565656"</item>
        ///     <item>transactionID [string]: Stark Bank ledger transaction ids linked to this IssuingWithdrawal</item>
        ///     <item>issuingTransactionID [string]: issuing ledger transaction ids linked to this IssuingWithdrawal</item>
        ///     <item>updated [DateTime]: latest update DateTime for the IssuingWithdrawal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the IssuingWithdrawal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingWithdrawal(long amount, string externalID, string description, List<string> tags = null, string id = null,
            string transactionID = null, string issuingTransactionID = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Amount = amount;
            Description = description;
            TransactionID = transactionID;
            IssuingTransactionID = issuingTransactionID;
            ExternalID = externalID;
            Tags = tags;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create an IssuingWithdrawal
        /// <br/>
        /// Send an IssuingWithdrawal object for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>withdrawal [IssuingWithdrawal object]: IssuingWithdrawal object to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingWithdrawal object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingWithdrawal Create(IssuingWithdrawal withdrawal, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: withdrawal,
                user: user
            ) as IssuingWithdrawal;
        }
        
        /// <summary>
        /// Create IssuingWithdrawal
        /// <br/>
        /// Send an IssuingWithdrawal dictionary for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>withdrawal [dictionary]: Dictionary representing the IssuingWithdrawal to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingWithdrawal object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingWithdrawal Create(Dictionary<string, object> withdrawal, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: withdrawal,
                user: user
            ) as IssuingWithdrawal;
        }

        /// <summary>
        /// Retrieve a specific IssuingWithdrawal by its id
        /// <br/>
        /// Receive a single IssuingWithdrawal object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingWithdrawal object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IssuingWithdrawal Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingWithdrawal;
        }

        /// <summary>
        /// Retrieve IssuingWithdrawal objects
        /// <br/>
        /// Receive an IEnumerable of IssuingWithdrawal objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>externalIds [list of strings, default null]: external IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings. User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingWithdrawal objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingWithdrawal> Query(int? limit = null, string externalID = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit" , limit },
                    { "externalID" , externalID },
                    { "after" , after },
                    { "before" , before },
                    { "tags" , tags }
                },
                user: user
            ).Cast<IssuingWithdrawal>();
        }

        /// <summary>
        /// Retrieve paged IssuingWithdrawal objects
        /// <br/>
        /// Receive a list of up to 100 IssuingWithdrawal objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>externalIds [list of strings, default null]: external IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingWithdrawal objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingWithdrawal objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingWithdrawal> page, string pageCursor) Page(string cursor = null, int? limit = null, string externalID = null,
            DateTime? after = null, DateTime? before = null, List<string> tags = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit" , limit },
                    { "externalID" , externalID },
                    { "after" , after },
                    { "before" , before },
                    { "tags" , tags }
                },
                user: user
            );
            List<IssuingWithdrawal> withdrawals = new List<IssuingWithdrawal>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                withdrawals.Add(subResource as IssuingWithdrawal);
            }
            return (withdrawals, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingWithdrawal", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string description = json.description;
            string transactionID = json.transactionId;
            string issuingTransactionID = json.issuingTransactionId;
            string externalID = json.externalId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new IssuingWithdrawal(
                id: id, amount: amount, description: description, transactionID: transactionID, tags: tags, externalID: externalID,
                issuingTransactionID: issuingTransactionID, updated: updated, created: created
            );
        }
    }
}
