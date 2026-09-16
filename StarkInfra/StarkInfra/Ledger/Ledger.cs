using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// Ledger object
    /// <br/>
    /// Ledgers are used to track the balance of a given amount by inserting LedgerTransactions to them.
    /// They can represent a bank account, a digital wallet, an inventory product, etc.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ExternalID [string]: string that must be unique among all your Ledgers. ex: "my-internal-id-123456"</item>
    ///     <item>Rules [list of Ledger.Rule objects, default null]: list of Rule objects linked to the Ledger. Rules are used to limit the balance of the Ledger. ex: new List&lt;Ledger.Rule&gt;{ new Ledger.Rule(key: "minimumBalance", value: 0) }</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for Ledgers. ex: new List&lt;string&gt;{ "account/123", "savings" }</item>
    ///     <item>Metadata [Dictionary object, default null]: dictionary object used to store additional information about the Ledger object. ex: new Dictionary&lt;string, object&gt;{ { "accountId", "123" }, { "accountType", "savings" } }</item>
    ///     <item>ID [string]: unique id returned when the Ledger is created. ex: "5656565656565656"</item>
    ///     <item>Created [DateTime]: creation datetime for the Ledger. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the Ledger. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class Ledger : Resource
    {
        public string ExternalID { get; }
        public List<Rule> Rules { get; }
        public List<string> Tags { get; }
        public Dictionary<string, object> Metadata { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// Ledger object
        /// <br/>
        /// Ledgers are used to track the balance of a given amount by inserting LedgerTransactions to them.
        /// They can represent a bank account, a digital wallet, an inventory product, etc.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>externalID [string]: string that must be unique among all your Ledgers. ex: "my-internal-id-123456"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>rules [list of Ledger.Rule objects, default null]: list of Rule objects linked to the Ledger. Rules are used to limit the balance of the Ledger. ex: new List&lt;Ledger.Rule&gt;{ new Ledger.Rule(key: "minimumBalance", value: 0) }</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for Ledgers. ex: new List&lt;string&gt;{ "account/123", "savings" }</item>
        ///     <item>metadata [Dictionary object, default null]: dictionary object used to store additional information about the Ledger object. ex: new Dictionary&lt;string, object&gt;{ { "accountId", "123" }, { "accountType", "savings" } }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the Ledger is created. ex: "5656565656565656"</item>
        ///     <item>created [DateTime]: creation datetime for the Ledger. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the Ledger. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Ledger(
            string externalID, List<Rule> rules = null, List<string> tags = null,
            Dictionary<string, object> metadata = null, string id = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            ExternalID = externalID;
            Rules = rules;
            Tags = tags;
            Metadata = metadata;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create Ledgers
        /// <br/>
        /// Send a list of Ledger objects for creation at the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>ledgers [list of Ledger objects]: list of Ledger objects to be created in the Stark Infra API. You can send up to 100 Ledger objects in a single request.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Ledger objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Ledger> Create(List<Ledger> ledgers, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: ledgers,
                user: user
            ).ToList().ConvertAll(o => (Ledger)o);
        }

        /// <summary>
        /// Create Ledgers
        /// <br/>
        /// Send a list of Ledger dictionaries for creation at the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>ledgers [list of dictionaries]: list of dictionaries representing the Ledger objects to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Ledger objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Ledger> Create(List<Dictionary<string, object>> ledgers, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: ledgers,
                user: user
            ).ToList().ConvertAll(o => (Ledger)o);
        }

        /// <summary>
        /// Retrieve a specific Ledger
        /// <br/>
        /// Receive a single Ledger object previously created in the Stark Infra API by its id
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
        ///     <item>Ledger object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Ledger Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Ledger;
        }

        /// <summary>
        /// Retrieve Ledgers
        /// <br/>
        /// Receive an IEnumerable of Ledger objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>ids [list of strings, default null]: list of Ledger ids to filter retrieved objects. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>externalIds [list of strings, default null]: list of Ledger external ids to filter retrieved objects. ex: new List&lt;string&gt;{ "my-internal-id-123456", "my-internal-id-654321" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List&lt;string&gt;{ "account/123", "savings" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Ledger objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Ledger> Query(
            int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> ids = null, List<string> externalIds = null, List<string> tags = null, User user = null
        )
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "ids", ids },
                    { "externalIds", externalIds },
                    { "tags", tags }
                },
                user: user
            ).Cast<Ledger>();
        }

        /// <summary>
        /// Retrieve paged Ledgers
        /// <br/>
        /// Receive a list of up to 100 Ledger objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>ids [list of strings, default null]: list of Ledger ids to filter retrieved objects. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>externalIds [list of strings, default null]: list of Ledger external ids to filter retrieved objects. ex: new List&lt;string&gt;{ "my-internal-id-123456", "my-internal-id-654321" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List&lt;string&gt;{ "account/123", "savings" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Ledger objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of Ledger objects</item>
        /// </list>
        /// </summary>
        public static (List<Ledger> page, string pageCursor) Page(
            string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> ids = null, List<string> externalIds = null, List<string> tags = null, User user = null
        )
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
                    { "ids", ids },
                    { "externalIds", externalIds },
                    { "tags", tags }
                },
                user: user
            );
            List<Ledger> ledgers = new List<Ledger>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                ledgers.Add(subResource as Ledger);
            }
            return (ledgers, pageCursor);
        }

        /// <summary>
        /// Update Ledger
        /// <br/>
        /// Update a Ledger by passing id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: Ledger id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>rules [list of Ledger.Rule objects, default null]: list of Rule objects linked to the Ledger. Rules are used to limit the balance of the Ledger. ex: new List&lt;Ledger.Rule&gt;{ new Ledger.Rule(key: "minimumBalance", value: 0) }</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for Ledgers. ex: new List&lt;string&gt;{ "account/123", "savings" }</item>
        ///     <item>metadata [Dictionary object, default null]: dictionary object used to store additional information about the Ledger object. ex: new Dictionary&lt;string, object&gt;{ { "accountId", "123" }, { "accountType", "savings" } }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target Ledger object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Ledger Update(
            string id, List<Rule> rules = null, List<string> tags = null,
            Dictionary<string, object> metadata = null, User user = null
        )
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "rules", rules },
                    { "tags", tags },
                    { "metadata", metadata }
                },
                user: user
            ) as Ledger;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Ledger", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string externalID = json.externalId;
            List<Rule> rules = json.rules is null ? new List<Rule> { } : ParseRules(json.rules);
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            Dictionary<string, object> metadata = json.metadata?.ToObject<Dictionary<string, object>>();
            string id = json.id;
            string createdString = json.created;
            DateTime? created = string.IsNullOrEmpty(createdString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = string.IsNullOrEmpty(updatedString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new Ledger(
                externalID: externalID, rules: rules, tags: tags, metadata: metadata,
                id: id, created: created, updated: updated
            );
        }

        private static List<Rule> ParseRules(dynamic json)
        {
            List<Rule> rules = new List<Rule>();
            foreach (dynamic rule in json)
            {
                rules.Add(Rule.ResourceMaker(rule));
            }
            return rules;
        }
    }
}
