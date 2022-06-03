using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingHolder object
    /// <br/>
    /// The IssuingHolder describes a card holder that may group several cards.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: card holder name.</item>
    ///     <item>TaxID [string]: card holder tax ID</item>
    ///     <item>ExternalID [string] card holder external ID</item>
    ///     <item>Rules [list of IssuingRule objects, default []]: [EXPANDABLE] list of holder spending rules</item>
    ///     <item>Tags [list of strings, default []]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID [string]: unique id returned when IssuingHolder is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current IssuingHolder status. ex: "active", "blocked" or "canceled"</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingHolder : Resource
    {
        public string Name { get; }
        public string TaxID { get; }
        public string ExternalID { get; }
        public string Status { get; }
        public List<IssuingRule> Rules { get; }
        public List<string> Tags { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingHolder object
        /// <br/>
        /// The IssuingHolder describes a card holder that may group several cards.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: card holder name</item>
        ///     <item>taxId [string]: card holder tax ID</item>
        ///     <item>externalId [string] card holder external ID</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>rules [list of Rules, default []]: [EXPANDABLE] list of holder spending rules</item>
        ///     <item>tags [list of strings, default []]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingHolder is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current IssuingHolder status. ex: "active", "blocked" or "canceled"</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingHolder(string name, string taxId, string externalId, List<IssuingRule> rules = null, List<string> tags = null, 
            string id = null, string status = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Name = name;
            TaxID = taxId;
            ExternalID = externalId;
            Status = status;
            Rules = rules;
            Tags = tags;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create IssuingHolders
        /// <br/>
        /// Send a list of IssuingHolder objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holders [list of IssuingHolder objects]: list of IssuingHolder objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingHolder objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingHolder> Create(List<IssuingHolder> holders, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holders,
                query: parameters,
                user: user
            ).ToList().ConvertAll(o => (IssuingHolder)o);
        }

        /// <summary>
        /// Create IssuingHolders
        /// <br/>
        /// Send a list of IssuingHolder objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holders [list of dictionaries]: list of dictionaries representing the IssuingHolders to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingHolder objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingHolder> Create(List<Dictionary<string, object>> holders, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holders,
                query: parameters,
                user: user
            ).ToList().ConvertAll(o => (IssuingHolder)o);
        }

        /// <summary>
        /// Retrieve a specific IssuingHolder
        /// <br/>
        /// Receive a single IssuingHolder object previously created in the Stark Infra API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingHolder object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingHolder Get(string id, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                query: parameters,
                user: user
            ) as IssuingHolder;
        }

        /// <summary>
        /// Retrieve IssuingHolders
        /// <br/>
        /// Receive an IEnumerable of IssuingHolder objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "canceled".</item>
        ///     <item>after [DateTime or string, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: List<string>{ "5656565656565656", "4545454545454545"}</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingHolder objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingHolder> Query(string status = null, DateTime? after = null, DateTime? before = null, List<string> tags = null,
            List<string> ids = null, int? limit = null, List<string> expand = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "status", status },
                    { "after", after },
                    { "before", before },
                    { "tags", tags },
                    { "ids", ids },
                    { "limit", limit },
                    { "expand", expand }
                },
                user: user
            ).Cast<IssuingHolder>();
        }

        /// <summary>
        /// Retrieve paged IssuingHolders
        /// <br/>
        /// Receive a list of up to 100 IssuingHolder objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "canceled"</item>
        ///     <item>after [DateTime or string, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{"tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{"rules"}</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingHolder objects with updated attributes and cursor to retrieve the next page of IssuingHolder objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingHolder> page, string pageCursor) Page(string cursor = null, string status = null, DateTime? after = null, DateTime? before = null, 
            List<string> tags = null, List<string> ids = null, int? limit = null, List<string> expand = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "status", status },
                    { "after", after },
                    { "before", before },
                    { "tags", tags },
                    { "ids", ids },
                    { "limit", limit },
                    { "expand", expand }
                },
                user: user
            );
            List<IssuingHolder> holders = new List<IssuingHolder>();
            foreach (SubResource subResource in page)
            {
                holders.Add(subResource as IssuingHolder);
            }
            return (holders, pageCursor);
        }

        /// <summary>
        /// Update IssuingHolder entity
        /// <br/>
        /// Update an IssuingHolder by passing id.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, defautl null]: you may cancel the IssuingHolder by passing "canceled" in the status. ex: "canceled"</item>
        ///     <item>name [string, default null]: card holder name</item>
        ///     <item>rules [list of IssuingRule, default null]: list of IssuingRule with "amount": int, "currencyCode": string, "id": string, "interval": string, "name": string pairs.</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target IssuingHolder with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingHolder Update(string id, Dictionary<string, object> patchData, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as IssuingHolder;
        }

        /// <summary>
        /// Cancel an IssuingHolder entity
        /// <br/>
        /// Cancel an IssuingHolder entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: IssuingHolder unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled IssuingHolder object</item>
        /// </list>
        /// </summary>
        public static IssuingHolder Cancel(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingHolder;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingHolder", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string name = json.name;
            string taxId = json.taxId;
            string externalId = json.externalId;
            List<IssuingRule> rules = IssuingRule.ParseRules(json.rules);
            List<string> tags = json.tags.ToObject<List<string>>();
            string status = json.status;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new IssuingHolder(
                id: id, name: name, taxId: taxId, externalId: externalId, status: status,
                rules: rules, tags: tags, updated: updated, created: created
            );
        }
    }
}
