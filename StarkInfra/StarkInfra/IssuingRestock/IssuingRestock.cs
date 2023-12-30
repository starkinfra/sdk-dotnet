using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingRestock object
    /// <br/>
    /// The IssuingRestock object displays the information of the restock orders created in your Workspace. 
    /// This resource place a restock order for a specific IssuingStock object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Count [integer]: number of restocks to be restocked. ex: 100</item>
    ///     <item>StockID [string]: IssuingStock unique id ex: "5136459887542272"</item>
    ///     <item>Tags [list of strings] list of strings for tagging. ex: new List<string> { "card", "corporate" } </item>
    ///     <item>ID [string]: unique id returned when IssuingRestock is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current IssuingRestock status. ex: "created", "processing", "confirmed"</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingRestock. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingRestock. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingRestock : Resource
    {
        public int? Count { get; }
        public string StockID { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public DateTime? Updated { get;  }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingRestock object
        /// <br/>
        /// The IssuingRestock object displays the information of the restock orders created in your Workspace. 
        /// This resource place a restock order for a specific IssuingStock object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>count [integer]: number of restocks to be restocked. ex: 100</item>
        ///     <item>stockID [string]: IssuingStock unique id ex: "5136459887542272"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string> { "card", "corporate" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingRestock is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current IssuingRestock status. ex: "created", "processing", "confirmed"</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingRestock. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingRestock. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingRestock(int count, string stockID, List<string> tags = null, string id = null, 
            string status = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Count = count;
            StockID = stockID;
            Tags = tags;
            Status = status;
            Updated = updated;
            Created = created;
        }
        
        /// <summary>
        /// Create IssuingRestock objects
        /// <br/>
        /// Send a list of IssuingRestocks objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>restocks [list of IssuingRestock objects]: list of IssuingRestock objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingRestock objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingRestock> Create(List<IssuingRestock> restocks, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: restocks,
                user: user
            ).ToList().ConvertAll(o => (IssuingRestock)o);
        }
        
        /// <summary>
        /// Create IssuingRestock objects
        /// <br/>
        /// Send a list of IssuingRestocks dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of dictionaries]: list of dictionaries representing the IssuingRestocks to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingRestock objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingRestock> Create(List<Dictionary<string, object>> requests, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (IssuingRestock)o);
        }

        /// <summary>
        /// Retrieve IssuingRestock objects
        /// <br/>
        /// Receive an IEnumerable of IssuingRestock objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created", "processing", "confirmed" }</item>
        ///     <item>stockIds [list of strings, default null]: list of stockIds to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingRestock objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingRestock> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> stockIds = null, List<string> ids = null, List<string> tags = null,
            User user = null)
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
                    { "stockIds", stockIds },
                    { "ids", ids },
                    { "tags", tags },
                },
                user: user
            ).Cast<IssuingRestock>();
        }

        /// <summary>
        /// Retrieve paged IssuingRestock objects
        /// <br/>
        /// Receive a list of up to 100 IssuingRestock objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created", "processing", "success", "failed" }</item>
        ///     <item>stockIds [list of strings, default null]: list of stockIds to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingRestock objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingRestock objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingRestock> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> status = null, List<string> stockIds = null, List<string> ids = null, List<string> tags = null,
            User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "ids", ids },
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "stockIds", stockIds },
                    { "tags", tags },
                },
                user: user
            );
            List<IssuingRestock> restocks = new List<IssuingRestock>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                restocks.Add(subResource as IssuingRestock);
            }
            return (restocks, pageCursor);
        }

        /// <summary>
        /// Retrieve a specific IssuingRestock object
        /// <br/>
        /// Receive a single IssuingRestock object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingRestock object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingRestock Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingRestock;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingRestock", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            int count = json.count;
            string stockID = json.stockId;
            List<string> tags = json.tags.ToObject<List<string>>();
            string status = json.status;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new IssuingRestock(
                id: id, count: count, stockID: stockID, tags: tags, status: status,
                updated: updated, created: created
            );
        }
    }
}
