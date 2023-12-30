using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixStatement object
    /// <br/>
    /// The PixStatement object stores information about all the transactions that
    /// happened on a specific day at your settlement account according to the Central Bank.
    /// It must be created by the user before it can be accessed.
    /// This feature is only available for direct participants.
    /// <br/>
    /// When you initialize a PixStatement, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>After [DateTime]: transactions that happened at this date are stored in the PixStatement, must be the same as before. ex: DateTime(2022, 01, 01)</item>
    ///     <item>Before [DateTime]: transactions that happened at this date are stored in the PixStatement, must be the same as after. ex: DateTime(2022, 01, 01)</item>
    ///     <item>Type [string]: types of entities to include in statement. Options: new List<string>{ "interchange", "interchangeTotal", "transaction" }</item>
    ///     <item>ID [string]: unique id returned when the PixStatement is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current PixStatement status. ex: "success" or "failed"</item>
    ///     <item>TransactionCount [integer]: number of transactions that happened during the day that the PixStatement was requested. ex: 11</item>
    ///     <item>Created [DateTime]: creation DateTime for the PixStatement. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the PixStatement. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixStatement : Resource
    {
        public DateTime? After { get; }
        public DateTime? Before { get; }
        public string Type { get; }
        public string Status { get; }
        public long? TransactionCount { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixStatement object
        /// <br/>
        /// The PixStatement object stores information about all the transactions that
        /// happened on a specific day at your settlement account according to the Central Bank.
        /// It must be created by the user before it can be accessed.
        /// This feature is only available for direct participants.
        /// <br/>
        /// When you initialize a PixStatement, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>after [DateTime]: transactions that happened at this date are stored in the PixStatement, must be the same as before. ex: (2022-01-01)</item>
        ///     <item>before [DateTime]: transactions that happened at this date are stored in the PixStatement, must be the same as after. ex: (2022-01-01)</item>
        ///     <item>type [string]: types of entities to include in statement. Options: new List<string>{ "interchange", "interchangeTotal", "transaction" }</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixStatement is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current PixStatement status. ex: "success" or "failed"</item>
        ///     <item>transactionCount [integer]: number of transactions that happened during the day that the PixStatement was requested. ex: 11</item>
        ///     <item>created [DateTime]: creation DateTime for the PixStatement. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the PixStatement. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixStatement(DateTime? after, DateTime? before, string type, string id = null, string status = null, 
            long? transactionCount = null, DateTime? created = null, DateTime? updated = null) : base(id)
        {
            After = after;
            Before = before;
            Type = type;
            Status = status;
            TransactionCount = transactionCount;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create a PixStatement
        /// <br/>
        /// Send one PixStatement object for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>statement [PixStatement object]: PixStatement object to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixStatement objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixStatement Create(PixStatement statement, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: statement,
                user: user
            ) as PixStatement;
        }
        
        /// <summary>
        /// Create PixStatement
        /// <br/>
        /// Send a PixStatement dictionary for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>statement [dictionary]: Dictionary representing the PixStatement to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixStatement object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixStatement Create(Dictionary<string, object> statement, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: statement,
                user: user
            ) as PixStatement;
        }

        /// <summary>
        /// Retrieve a PixStatement object
        /// <br/>
        /// Retrieve the PixStatement object linked to your Workspace in the Stark Infra API by its id.
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
        ///     <item>PixStatement object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static PixStatement Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixStatement;
        }
        
        /// <summary>
        /// Retrieve PixStatement objects
        /// <br/>
        /// Receive an IEnumerable of PixStatement objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixStatement objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixStatement> Query(int? limit = null, List<string> ids = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "ids", ids }
                },
                user: user
            ).Cast<PixStatement>();
        }
        
        /// <summary>
        /// Retrieve paged PixStatement objects
        /// <br/>
        /// Receive a list of up to 100 PixStatement objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixStatement objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixStatement objects</item>
        /// </list>
        /// </summary>
        public static (List<PixStatement> page, string pageCursor) Page(string cursor = null, int? limit = null, List<string> ids = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "ids", ids }
                },
                user: user
            );
            List<PixStatement> statements = new List<PixStatement>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                statements.Add(subResource as PixStatement);
            }
            return (statements, pageCursor);
        }

        /// <summary>
        /// Retrieve a .csv PixStatement by its id
        /// <br/>
        /// Retrieve a specific PixStatement by its ID in a .csv file.
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
        ///     <item>.zip file containing a PixStatement in .csv format</item>
        /// </list>
        /// </summary>
        public static byte[] Csv(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                subResourceName: "csv",
                id: id,
                user: user
            );
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixStatement", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string afterString = json.after;
            string beforeString = json.before;
            DateTime after = StarkCore.Utils.Checks.CheckDateTime(afterString);
            DateTime before = StarkCore.Utils.Checks.CheckDateTime(beforeString);
            string type = json.type;
            string id = json.id;
            string status = json.status;
            long transactionCount = json.transactionCount;
            string createdString = json.created;
            string updatedString = json.updated;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixStatement(
                after: after, before: before, type: type, id: id, status: status, transactionCount: transactionCount,
                created: created, updated: updated
            );
        }
    }
}
