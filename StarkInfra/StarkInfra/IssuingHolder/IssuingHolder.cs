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
    /// When you initialize a IssuingHolder, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: card holder name. ex: "Jaime Lannister"</item>
    ///     <item>TaxID [string]: card holder's tax ID. ex: "012.345.678-90"</item>
    ///     <item>ExternalID [string] card holder's external ID. ex: "my-external-id"</item>
    ///     <item>Rules [list of IssuingRule objects, default null]: [EXPANDABLE] list of holder spending rules</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID [string]: unique id returned when IssuingHolder is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current IssuingHolder status. ex: "active", "blocked" or "canceled"</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the IssuingHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the IssuingHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingHolder : Resource
    {
        public string Name { get; }
        public string TaxID { get; }
        public string ExternalID { get; }
        public List<IssuingRule> Rules { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingHolder object
        /// <br/>
        /// The IssuingHolder describes a card holder that may group several cards.
        /// <br/>
        /// When you initialize a IssuingHolder, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: card holder's name. ex: "Jaime Lannister</item>
        ///     <item>taxID [string]: card holder's tax ID. ex: "012.345.678-90"</item>
        ///     <item>externalID [string] card holder external ID. ex: "my-external-id"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>rules [list of Rules, default null]: [EXPANDABLE] list of holder spending rules</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingHolder is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current IssuingHolder status. ex: "active", "blocked" or "canceled"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the IssuingHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the IssuingHolder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingHolder(string name, string taxID, string externalID, List<IssuingRule> rules = null, List<string> tags = null, 
            string id = null, string status = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Name = name;
            TaxID = taxID;
            ExternalID = externalID;
            Rules = rules;
            Tags = tags;
            Status = status;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create IssuingHolder objects
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
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holders,
                query: parameters,
                user: user
            ).ToList().ConvertAll(o => (IssuingHolder)o);
        }

        /// <summary>
        /// Create IssuingHolder objects
        /// <br/>
        /// Send a list of IssuingHolder objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holders [list of Dictionaries]: list of dictionaries representing the IssuingHolder objects to be created in the API</item>
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
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holders,
                query: parameters,
                user: user
            ).ToList().ConvertAll(o => (IssuingHolder)o);
        }

        /// <summary>
        /// Retrieve a specific IssuingHolder by its id
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
        ///     <item>IssuingHolder object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IssuingHolder Get(string id, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                query: parameters,
                user: user
            ) as IssuingHolder;
        }

        /// <summary>
        /// Retrieve IssuingHolder objects
        /// <br/>
        /// Receive an IEnumerable of IssuingHolder objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: List<string>{ "5656565656565656", "4545454545454545"}</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "canceled".</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "rules" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingHolder objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingHolder> Query(int? limit = null, List<string> ids = null, DateTime? after = null, 
            DateTime? before = null, string status = null, List<string> tags = null,List<string> expand = null, 
            User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "ids", ids },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "expand", expand }
                },
                user: user
            ).Cast<IssuingHolder>();
        }

        /// <summary>
        /// Retrieve paged IssuingHolder objects
        /// <br/>
        /// Receive a list of up to 100 IssuingHolder objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "canceled"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{"tony", "stark" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{"rules"}</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingHolder objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingHolder objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingHolder> page, string pageCursor) Page(string cursor = null, int? limit = null, 
            List<string> ids = null, DateTime? after = null, DateTime? before = null, string status = null, 
            List<string> tags = null, List<string> expand = null, User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "ids", ids },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "expand", expand }
                },
                user: user
            );
            List<IssuingHolder> holders = new List<IssuingHolder>();
            foreach (StarkCore.Utils.SubResource subResource in page)
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
        ///     <item>id[string]: IssuingHolder id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, default null]: You may block the IssuingHolder by passing 'blocked' in the status. ex: "blocked"</item>
        ///     <item>name [string, default null]: card holder name. ex: "Jaime Lannister"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        ///     <item>rules [list of dictionaries, default null]: list of dictionaries with "amount": int, "currencyCode": string, "id": string, "interval": string, "name": string pairs.</item>
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
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
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
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingHolder;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingHolder", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string taxID = json.taxId;
            string externalID = json.externalId;
            List<IssuingRule> rules = IssuingRule.ParseRules(json.rules);
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);

            return new IssuingHolder(
                name: name, taxID: taxID, externalID: externalID, rules: rules, 
                tags: tags, id: id, status: status, updated: updated, created: created             
            );
        }
    }
}
