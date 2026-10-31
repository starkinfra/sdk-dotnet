using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// AiKnowledgeBase object
    /// <br/>
    /// An AiKnowledgeBase turns a website into material an AiAgent can read. You give it a root URL;
    /// Stark Infra crawls the page, follows its links, converts everything to Markdown and indexes it for retrieval.
    /// <br/>
    /// When you initialize an AiKnowledgeBase, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the object
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: name of the knowledge base. Between 1 and 100 characters. ex: "Product Documentation"</item>
    ///     <item>RootUrl [string]: absolute http or https URL the crawl starts from. ex: "https://docs.starkinfra.com"</item>
    ///     <item>IsRecursive [bool?, default null]: whether the crawl may follow links into other subdomains of the root URL's registered domain. The API defaults to true. ex: false</item>
    ///     <item>Tags [list of strings, default null]: list of up to 100 strings for reference when searching for AiKnowledgeBases. ex: new List&lt;string&gt;{ "support", "public" }</item>
    ///     <item>ID [string]: unique id returned when the AiKnowledgeBase is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current status of the knowledge base. Options: "processing", "success", "failed". An agent retrieves from a base only once it reaches "success"</item>
    ///     <item>Created [DateTime]: creation datetime for the AiKnowledgeBase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the AiKnowledgeBase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class AiKnowledgeBase : Resource
    {
        public string Name { get; }
        public string RootUrl { get; }
        public bool? IsRecursive { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// AiKnowledgeBase object
        /// <br/>
        /// An AiKnowledgeBase turns a website into material an AiAgent can read. You give it a root URL;
        /// Stark Infra crawls the page, follows its links, converts everything to Markdown and indexes it for retrieval.
        /// <br/>
        /// When you initialize an AiKnowledgeBase, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the object
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: name of the knowledge base. Between 1 and 100 characters. ex: "Product Documentation"</item>
        ///     <item>rootUrl [string]: absolute http or https URL the crawl starts from. ex: "https://docs.starkinfra.com"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>isRecursive [bool?, default null]: whether the crawl may follow links into other subdomains of the root URL's registered domain. The API defaults to true. ex: false</item>
        ///     <item>tags [list of strings, default null]: list of up to 100 strings for reference when searching for AiKnowledgeBases. ex: new List&lt;string&gt;{ "support", "public" }</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the AiKnowledgeBase is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the knowledge base. Options: "processing", "success", "failed". An agent retrieves from a base only once it reaches "success"</item>
        ///     <item>created [DateTime]: creation datetime for the AiKnowledgeBase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the AiKnowledgeBase. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public AiKnowledgeBase(string name, string rootUrl, bool? isRecursive = null, List<string> tags = null,
            string id = null, string status = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Name = name;
            RootUrl = rootUrl;
            IsRecursive = isRecursive;
            Tags = tags;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create an AiKnowledgeBase object
        /// <br/>
        /// Send an AiKnowledgeBase object for creation in the Stark Infra API and start crawling it.
        /// The call returns immediately with the knowledge base in "processing" status.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>knowledgeBase [AiKnowledgeBase object]: AiKnowledgeBase object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>AiKnowledgeBase object with updated attributes</item>
        /// </list>
        /// </summary>
        public static AiKnowledgeBase Create(AiKnowledgeBase knowledgeBase, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            StarkCore.Utils.Response response = Rest.PostRaw(
                payload: knowledgeBase.ToJson(),
                path: StarkCore.Utils.Api.Endpoint(resourceName),
                query: null,
                user: user
            );
            return resourceMaker(response.Json()["knowledgeBase"]) as AiKnowledgeBase;
        }

        /// <summary>
        /// Retrieve a specific AiKnowledgeBase
        /// <br/>
        /// Receive a single AiKnowledgeBase object previously created in the Stark Infra API by its id.
        /// This is the call to poll while the crawl runs.
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
        ///     <item>AiKnowledgeBase object that corresponds to the given id</item>
        /// </list>
        /// </summary>
        public static AiKnowledgeBase Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            StarkCore.Utils.Response response = Rest.GetRaw(
                path: StarkCore.Utils.Api.Endpoint(resourceName) + "/" + id,
                query: null,
                user: user
            );
            return resourceMaker(response.Json()["knowledgeBase"]) as AiKnowledgeBase;
        }

        /// <summary>
        /// Retrieve AiKnowledgeBases
        /// <br/>
        /// Receive an IEnumerable of AiKnowledgeBase objects previously created in the Stark Infra API, following the cursor
        /// until the list ends or the limit is reached.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>name [string, default null]: case-insensitive substring of the name to filter retrieved objects. The filter is applied to each page, so a page may come back short or empty while the cursor is followed. ex: "docs"</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. Options: "processing", "success", "failed"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of AiKnowledgeBase objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<AiKnowledgeBase> Query(int? limit = null, List<string> ids = null, string name = null,
            string status = null, User user = null
        ) {
            int remaining = limit ?? 0;
            string cursor = null;
            do
            {
                (List<AiKnowledgeBase> knowledgeBases, string nextCursor) = Page(
                    cursor: cursor,
                    limit: limit,
                    ids: ids,
                    name: name,
                    status: status,
                    user: user
                );
                foreach (AiKnowledgeBase knowledgeBase in knowledgeBases)
                {
                    yield return knowledgeBase;
                }
                remaining -= knowledgeBases.Count;
                cursor = nextCursor;
            } while (!string.IsNullOrEmpty(cursor) && (!limit.HasValue || remaining > 0));
        }

        /// <summary>
        /// Retrieve paged AiKnowledgeBase objects
        /// <br/>
        /// Receive a list of up to 100 AiKnowledgeBase objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. When sent, every matching object is returned at once and no cursor comes back. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>name [string, default null]: case-insensitive substring of the name to filter retrieved objects. The filter is applied to each page, so a page may hold fewer than limit objects, or none, while a cursor is still returned. ex: "docs"</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. Options: "processing", "success", "failed"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of AiKnowledgeBase objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of AiKnowledgeBase objects, null on the last page</item>
        /// </list>
        /// </summary>
        public static (List<AiKnowledgeBase> page, string pageCursor) Page(string cursor = null, int? limit = null,
            List<string> ids = null, string name = null, string status = null, User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            StarkCore.Utils.Response response = Rest.GetRaw(
                path: StarkCore.Utils.Api.Endpoint(resourceName),
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "ids", ids },
                    { "name", name },
                    { "status", status }
                },
                user: user
            );
            Newtonsoft.Json.Linq.JObject content = response.Json();
            List<AiKnowledgeBase> knowledgeBases = content["knowledgeBases"]
                .Select(json => resourceMaker(json) as AiKnowledgeBase)
                .ToList();
            return (knowledgeBases, (string)content["cursor"]);
        }

        /// <summary>
        /// Update an AiKnowledgeBase entity
        /// <br/>
        /// Rename a knowledge base, retag it or change whether its crawl is recursive. The root URL cannot be changed.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: AiKnowledgeBase unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>name [string, default null]: new name of the knowledge base. Between 1 and 100 characters</item>
        ///     <item>isRecursive [bool?, default null]: whether the next crawl may follow links into other subdomains of the root URL's registered domain</item>
        ///     <item>tags [list of strings, default null]: new list of up to 100 strings. Replaces the current list</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target AiKnowledgeBase with updated attributes</item>
        /// </list>
        /// </summary>
        public static AiKnowledgeBase Update(string id, string name = null, bool? isRecursive = null, List<string> tags = null,
            User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            StarkCore.Utils.Response response = Rest.PatchRaw(
                payload: new Dictionary<string, object> {
                    { "name", name },
                    { "isRecursive", isRecursive },
                    { "tags", tags }
                },
                path: StarkCore.Utils.Api.Endpoint(resourceName) + "/" + id,
                query: null,
                user: user
            );
            return resourceMaker(response.Json()["knowledgeBase"]) as AiKnowledgeBase;
        }

        /// <summary>
        /// List the pages of an AiKnowledgeBase
        /// <br/>
        /// Receive every page the crawler has seen, grouped by host. While a crawl is running this is the live
        /// picture, merged with the last finished one.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: AiKnowledgeBase unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>JObject mapping each host to its list of pages, each with originalUrl, storageUrl and status</item>
        /// </list>
        /// </summary>
        public static JObject Hosts(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker _) = Resource();
            StarkCore.Utils.Response response = Rest.GetRaw(
                path: StarkCore.Utils.Api.Endpoint(resourceName) + "/" + id + "/hosts",
                query: null,
                user: user
            );
            return (JObject)response.Json()["hosts"];
        }

        /// <summary>
        /// Delete AiKnowledgeBases
        /// <br/>
        /// Delete up to 100 AiKnowledgeBases at once. Agents still referencing a deleted base simply retrieve nothing from it.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>ids [list of strings]: ids of the AiKnowledgeBases to be deleted. Up to 100 ids. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of deleted AiKnowledgeBase objects</item>
        /// </list>
        /// </summary>
        public static List<AiKnowledgeBase> Delete(List<string> ids, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            StarkCore.Utils.Response response = Rest.DeleteRaw(
                payload: null,
                path: StarkCore.Utils.Api.Endpoint(resourceName),
                query: new Dictionary<string, object> { { "ids", ids } },
                user: user
            );
            return response.Json()["knowledgeBases"].Select(json => resourceMaker(json) as AiKnowledgeBase).ToList();
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "AiKnowledgeBase", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string rootUrl = json.rootUrl;
            bool? isRecursive = json.isRecursive;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string created = json.created;
            string updated = json.updated;

            return new AiKnowledgeBase(
                name: name, rootUrl: rootUrl, isRecursive: isRecursive, tags: tags, id: id, status: status,
                created: StarkCore.Utils.Checks.CheckNullableDateTime(created),
                updated: StarkCore.Utils.Checks.CheckNullableDateTime(updated)
            );
        }
    }
}
