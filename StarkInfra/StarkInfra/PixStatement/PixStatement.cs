using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixStatement object
    /// <br/>
    /// When you initialize a PixStatement it will not be created in the Stark Infra API.
    /// The 'create' function sends the objects to the Stark Infra API and returns the list of
    /// created objects. The PixStatement object stores information about all the transactions
    /// that happened on a specific day at the workspace. It must be created by the user before
    /// it can be accessed by the user. This feature is only available for direct participants.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>after [DateTime]: transactions that happened at this date are stored in the PixStatement, must be the same as before. ex: (2022-01-01)</item>
    ///     <item>before [DateTime]: transactions that happened at this date are stored in the PixStatement, must be the same as after. ex: (2022-01-01)</item>
    ///     <item>type [string]: types of entities to include in statement. Options: ["interchange", "interchangeTotal", "transaction"]</item>
    ///     <item>id [string, default null]: unique id returned when the PixStatement is created. ex: "5656565656565656"</item>
    ///     <item>status [string, default null]: current PixStatement status. ex: "success" or "failed"</item>
    ///     <item>transaction_count [integer, default null]: number of transactions that happened during the day that the PixStatement was requested. ex 11</item>
    ///     <item>created [DateTime, default null]: creation datetime for the PixStatement. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>updated [DateTime, default null]: latest update datetime for the PixStatement. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixStatement : Utils.Resource
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
        /// When you initialize a PixStatement it will not be created in the Stark Infra API.
        /// The 'create' function sends the objects to the Stark Infra API and returns the list of
        /// created objects. The PixStatement object stores information about all the transactions
        /// that happened on a specific day at the workspace. It must be created by the user before
        /// it can be accessed by the user. This feature is only available for direct participants.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>after [DateTime]: transactions that happened at this date are stored in the PixStatement, must be the same as before. ex: (2022-01-01)</item>
        ///     <item>before [DateTime]: transactions that happened at this date are stored in the PixStatement, must be the same as after. ex: (2022-01-01)</item>
        ///     <item>type [string]: types of entities to include in statement. Options: ["interchange", "interchangeTotal", "transaction"]</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string, default null]: unique id returned when the PixStatement is created. ex: "5656565656565656"</item>
        ///     <item>status [string, default null]: current PixStatement status. ex: "success" or "failed"</item>
        ///     <item>transaction_count [integer, default null]: number of transactions that happened during the day that the PixStatement was requested. ex 11</item>
        ///     <item>created [DateTime, default null]: creation datetime for the PixStatement. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime, default null]: latest update datetime for the PixStatement. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
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
        ///     <item>pixStatements [list of Dictionaries]: list of Dictionaries representing the PixStatements to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixStatement objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixStatement Create(PixStatement pixStatement, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: pixStatement,
                user: user
            ) as PixStatement;
        }

        /// <summary>
        /// Retrieve a specific PixStatement
        /// <br/>
        /// Receive a single PixStatement object previously created in the Stark Infra API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixStatement object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixStatement Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixStatement;
        }
        
        /// <summary>
        /// Retrieve PixStatements
        /// <br/>
        /// Receive an IEnumerable of PixStatement objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixStatement objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixStatement> Query(int? limit = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
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
        /// Retrieve paged PixStatements
        /// <br/>
        /// Receive a list of up to 100 PixStatement objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixStatement objects with updated attributes and cursor to retrieve the next page of PixStatement objects</item>
        /// </list>
        /// </summary>
        public static (List<PixStatement> page, string pageCursor) Page(string cursor = null, int? limit = null, List<string> ids = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "ids", ids }
                },
                user: user
            );
            List<PixStatement> pixStatements = new List<PixStatement>();
            foreach (SubResource subResource in page)
            {
                pixStatements.Add(subResource as PixStatement);
            }
            return (pixStatements, pageCursor);
        }

        /// <summary>
        /// Retrieve a specific PixStatement csv file
        /// <br/>
        /// Receive a single PixStatement csv receipt file generated in the Stark Infra API by passing its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixStatement csv zipped file file</item>
        /// </list>
        /// </summary>
        public static byte[] Csv(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                subResourceName: "csv",
                id: id,
                user: user
            );
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixStatement", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string afterString = json.after;
            string beforeString = json.before;
            DateTime after = Checks.CheckDateTime(afterString);
            DateTime before = Checks.CheckDateTime(beforeString);
            string type = json.type;
            string id = json.id;
            string status = json.status;
            long transactionCount = json.transactionCount;
            string createdString = json.created;
            string updatedString = json.updated;
            DateTime created = Checks.CheckDateTime(createdString);
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new PixStatement(
                after: after, before: before, type: type, id: id, status: status, transactionCount: transactionCount,
                created: created, updated: updated
            );
        }
    }
}
