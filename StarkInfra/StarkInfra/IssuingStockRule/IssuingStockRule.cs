using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingStockRule object
    /// <br/>
    /// The IssuingStockRule object is a notification rule attached to an IssuingStock. When the linked
    /// stock balance reaches the minimumBalance, the recipients listed in emails and phones are notified.
    /// <br/>
    /// When you initialize an IssuingStockRule, the entity will not be automatically created in the
    /// Stark Infra API. The 'create' function sends the objects to the Stark Infra API and returns the
    /// list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>MinimumBalance [integer]: stock balance threshold that triggers a notification. ex: 10000</item>
    ///     <item>StockID [string]: IssuingStock unique id the rule is linked to. ex: "5136459887542272"</item>
    ///     <item>Tags [list of strings]: list of strings for tagging. ex: new List<string>{ "card", "corporate" }</item>
    ///     <item>Emails [list of strings]: emails notified when the stock reaches the minimum balance. ex: new List<string>{ "john.doe@enterprise.com" }</item>
    ///     <item>Phones [list of strings]: phones notified when the stock reaches the minimum balance. ex: new List<string>{ "+55 (11) 91234 5678" }</item>
    ///     <item>ID [string]: unique id returned when IssuingStockRule is created. ex: "5664445921492992"</item>
    ///     <item>Status [string]: current IssuingStockRule status. ex: "active" or "canceled"</item>
    ///     <item>Created [DateTime]: creation DateTime for the IssuingStockRule. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the IssuingStockRule. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingStockRule : Resource
    {
        public int MinimumBalance { get; }
        public string StockID { get; }
        public List<string> Tags { get; }
        public List<string> Emails { get; }
        public List<string> Phones { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// IssuingStockRule object
        /// <br/>
        /// The IssuingStockRule object is a notification rule attached to an IssuingStock. When the linked
        /// stock balance reaches the minimumBalance, the recipients listed in emails and phones are notified.
        /// <br/>
        /// When you initialize an IssuingStockRule, the entity will not be automatically created in the
        /// Stark Infra API. The 'create' function sends the objects to the Stark Infra API and returns the
        /// list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>minimumBalance [integer]: stock balance threshold that triggers a notification. ex: 10000</item>
        ///     <item>stockID [string]: IssuingStock unique id the rule is linked to. ex: "5136459887542272"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "card", "corporate" }</item>
        ///     <item>emails [list of strings, default null]: emails notified when the stock reaches the minimum balance. ex: new List<string>{ "john.doe@enterprise.com" }</item>
        ///     <item>phones [list of strings, default null]: phones notified when the stock reaches the minimum balance. ex: new List<string>{ "+55 (11) 91234 5678" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingStockRule is created. ex: "5664445921492992"</item>
        ///     <item>status [string]: current IssuingStockRule status. ex: "active" or "canceled"</item>
        ///     <item>created [DateTime]: creation DateTime for the IssuingStockRule. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the IssuingStockRule. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingStockRule(int minimumBalance, string stockID, List<string> tags = null, List<string> emails = null,
            List<string> phones = null, string id = null, string status = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            MinimumBalance = minimumBalance;
            StockID = stockID;
            Tags = tags;
            Emails = emails;
            Phones = phones;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create IssuingStockRule objects
        /// <br/>
        /// Send a list of IssuingStockRule objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>rules [list of IssuingStockRule objects]: list of IssuingStockRule objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingStockRule objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingStockRule> Create(List<IssuingStockRule> rules, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: rules,
                user: user
            ).ToList().ConvertAll(o => (IssuingStockRule)o);
        }

        /// <summary>
        /// Create IssuingStockRule objects
        /// <br/>
        /// Send a list of IssuingStockRule dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>rules [list of dictionaries]: list of dictionaries representing the IssuingStockRule objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingStockRule objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingStockRule> Create(List<Dictionary<string, object>> rules, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: rules,
                user: user
            ).ToList().ConvertAll(o => (IssuingStockRule)o);
        }

        /// <summary>
        /// Retrieve a specific IssuingStockRule by its id
        /// <br/>
        /// Receive a single IssuingStockRule object previously created in the Stark Infra API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5664445921492992"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingStockRule object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IssuingStockRule Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingStockRule;
        }

        /// <summary>
        /// Retrieve IssuingStockRule objects
        /// <br/>
        /// Receive an IEnumerable of IssuingStockRule objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "active", "canceled" }</item>
        ///     <item>stockIds [list of strings, default null]: list of stockIds to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingStockRule objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingStockRule> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
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
            ).Cast<IssuingStockRule>();
        }

        /// <summary>
        /// Retrieve paged IssuingStockRule objects
        /// <br/>
        /// Receive a list of up to 100 IssuingStockRule objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "active", "canceled" }</item>
        ///     <item>stockIds [list of strings, default null]: list of stockIds to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingStockRule objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingStockRule objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingStockRule> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> status = null, List<string> stockIds = null, List<string> ids = null, List<string> tags = null,
            User user = null)
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
                    { "stockIds", stockIds },
                    { "ids", ids },
                    { "tags", tags },
                },
                user: user
            );
            List<IssuingStockRule> rules = new List<IssuingStockRule>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                rules.Add(subResource as IssuingStockRule);
            }
            return (rules, pageCursor);
        }

        /// <summary>
        /// Update IssuingStockRule entity
        /// <br/>
        /// Update an IssuingStockRule by passing id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: IssuingStockRule id. ex: "5664445921492992"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>minimumBalance [integer, default null]: stock balance threshold that triggers a notification. ex: 20000</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "card", "corporate" }</item>
        ///     <item>emails [list of strings, default null]: emails notified when the stock reaches the minimum balance. ex: new List<string>{ "john.doe@enterprise.com" }</item>
        ///     <item>phones [list of strings, default null]: phones notified when the stock reaches the minimum balance. ex: new List<string>{ "+55 (11) 91234 5678" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target IssuingStockRule with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingStockRule Update(string id, int? minimumBalance = null, List<string> tags = null,
            List<string> emails = null, List<string> phones = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "minimumBalance", minimumBalance },
                    { "tags", tags },
                    { "emails", emails },
                    { "phones", phones },
                },
                user: user
            ) as IssuingStockRule;
        }

        /// <summary>
        /// Cancel an IssuingStockRule entity
        /// <br/>
        /// Cancel an IssuingStockRule entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: IssuingStockRule unique id. ex: "5664445921492992"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled IssuingStockRule object</item>
        /// </list>
        /// </summary>
        public static IssuingStockRule Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingStockRule;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingStockRule", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            int minimumBalance = json.minimumBalance;
            string stockID = json.stockId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            List<string> emails = json.emails?.ToObject<List<string>>();
            List<string> phones = json.phones?.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new IssuingStockRule(
                minimumBalance: minimumBalance, stockID: stockID, tags: tags, emails: emails,
                phones: phones, id: id, status: status, created: created, updated: updated
            );
        }
    }
}
