using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingTransaction object
    /// <br/>
    /// The IssuingTransaction object displays the information of the cards created in your Workspace.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when IssuingTransaction is created. ex: "5656565656565656"</item>
    ///     <item>Amount [long]: IssuingTransaction value in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Balance [integer]: balance amount of the Workspace at the instant of the Transaction in cents. ex: 200 (= R$ 2.00)</item>
    ///     <item>Description [string]: IssuingTransaction description. ex: "Buying food"</item>
    ///     <item>Source [string]: source of the transaction. ex: "issuing-purchase/5656565656565656"</item>
    ///     <item>Tags [list of strings]: list of strings inherited from the source resource. ex: new List<string>{ "tony", "stark" }</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingTransaction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingTransaction : Resource
    {
        public long Amount { get; }
        public long Balance { get; }
        public string Description { get; }
        public string Source { get; }
        public List<string> Tags { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingTransaction object
        /// <br/>
        /// Displays the IssuingTransaction objects created in your Workspace.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingTransaction is created. ex: "5656565656565656"</item>
        ///     <item>amount [long]: IssuingTransaction value in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>balance [integer]: balance amount of the Workspace at the instant of the Transaction in cents. ex: 200 (= R$ 2.00)</item>
        ///     <item>description [string]: IssuingTransaction description. ex: "Buying food"</item>
        ///     <item>source [string]: source of the transaction. ex: "issuing-purchase/5656565656565656"</item>
        ///     <item>tags [list of strings]: list of strings inherited from the source resource. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingTransaction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingTransaction(long amount, long balance, string description, string source, List<string> tags, DateTime? created = null, string id = null) : base(id)
        {
            Amount = amount;
            Balance = balance;
            Description = description;
            Source = source;
            Tags = tags;
            Created = created;
        }

        /// <summary>
        /// Retrieve a specific IssuingTransaction
        /// <br/>
        /// Receive a single IssuingTransaction object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingTransaction object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingTransaction Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingTransaction;
        }

        /// <summary>
        /// Retrieve IssuingTransactions
        /// <br/>
        /// Receive an IEnumerable of IssuingTransaction objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>externalIds [list of strings, default null]: external IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime or string, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>ids [list of strings, default null]: purchase IDs</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingTransaction objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingTransaction> Query(List<string> tags = null, List<string> externalIds = null,
            DateTime? after = null, DateTime? before = null, string status = null, List<string> ids = null, int? limit = 1, 
            User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "tags" , tags },
                    { "externalIds" , externalIds },
                    { "after" , after },
                    { "before" , before },
                    { "status" , status },
                    { "ids" , ids },
                    { "limit" , limit }
                },
                user: user
            ).Cast<IssuingTransaction>();
        }

        /// <summary>
        /// Retrieve paged IssuingTransactions
        /// <br/>
        /// Receive a list of up to 100 IssuingTransaction objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>externalIds [list of strings, default null]: external IDs. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime or string, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "approved", "canceled", "denied", "confirmed" or "voided"</item>
        ///     <item>ids [list of strings, default null]: purchase IDs</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingTransaction objects with updated attributes and cursor to retrieve the next page of IssuingTransaction objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingTransaction> page, string pageCursor) Page(string cursor = null, List<string> tags = null, 
            List<string> externalIds = null, DateTime? after = null, DateTime? before = null, string status = null,
            int? limit = 1, List<string> ids = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "tags" , tags },
                    { "externalIds" , externalIds },
                    { "after" , after },
                    { "before" , before },
                    { "status" , status },
                    { "ids" , ids },
                    { "limit" , limit }
                },
                user: user
            );
            List<IssuingTransaction> transactions = new List<IssuingTransaction>();
            foreach (SubResource subResource in page)
            {
                transactions.Add(subResource as IssuingTransaction);
            }
            return (transactions, pageCursor);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingTransaction", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            long balance = json.balance;
            string description = json.description;
            string source = json.source;
            List<string> tags = json.tags.ToObject<List<string>>();
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);

            return new IssuingTransaction(
                id: id, amount: amount, balance: balance, description: description, source: source, 
                tags: tags, created: created
            );
        }
    }
}
