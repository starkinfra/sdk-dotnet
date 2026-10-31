using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// AiChat object
    /// <br/>
    /// An AiChat is one conversation thread with an AiAgent and holds the history. Each turn is an AiMessage.
    /// <br/>
    /// When you initialize an AiChat, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the object
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>AgentID [string]: id of the AiAgent that will answer in this chat. ex: "5656565656565656"</item>
    ///     <item>Title [string, default null]: title of the conversation. Up to 100 characters. When omitted, the first message posted to the chat generates one. ex: "Order status"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for AiChats. ex: new List&lt;string&gt;{ "customer-123", "whatsapp" }</item>
    ///     <item>Context [Dictionary object, default null]: free-form object the agent receives with every turn of the chat. The keys are yours and are sent exactly as written. ex: new Dictionary&lt;string, object&gt;{ { "customer_id", "123" } }</item>
    ///     <item>ID [string]: unique id returned when the AiChat is created. ex: "5656565656565656"</item>
    ///     <item>AgentName [string]: name of the agent. Only present when requested with expand: ["agentName"]</item>
    ///     <item>Updated [DateTime]: latest update datetime for the AiChat. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class AiChat : Resource
    {
        public string AgentID { get; }
        public string Title { get; }
        public List<string> Tags { get; }
        public Dictionary<string, object> Context { get; }
        public string AgentName { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// AiChat object
        /// <br/>
        /// An AiChat is one conversation thread with an AiAgent and holds the history. Each turn is an AiMessage.
        /// <br/>
        /// When you initialize an AiChat, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the object
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>agentID [string]: id of the AiAgent that will answer in this chat. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>title [string, default null]: title of the conversation. Up to 100 characters. When omitted, the first message posted to the chat generates one. ex: "Order status"</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for AiChats. ex: new List&lt;string&gt;{ "customer-123", "whatsapp" }</item>
        ///     <item>context [Dictionary object, default null]: free-form object the agent receives with every turn of the chat. The keys are yours and are sent exactly as written. ex: new Dictionary&lt;string, object&gt;{ { "customer_id", "123" } }</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the AiChat is created. ex: "5656565656565656"</item>
        ///     <item>agentName [string]: name of the agent. Only present when requested with expand: ["agentName"]</item>
        ///     <item>updated [DateTime]: latest update datetime for the AiChat. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public AiChat(string agentID, string title = null, List<string> tags = null, Dictionary<string, object> context = null,
            string id = null, string agentName = null, DateTime? updated = null
        ) : base(id)
        {
            AgentID = agentID;
            Title = title;
            Tags = tags;
            Context = context;
            AgentName = agentName;
            Updated = updated;
        }

        /// <summary>
        /// Create an AiChat object
        /// <br/>
        /// Send an AiChat object for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>chat [AiChat object]: AiChat object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>AiChat object with updated attributes</item>
        /// </list>
        /// </summary>
        public static AiChat Create(AiChat chat, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: chat,
                user: user
            ) as AiChat;
        }

        /// <summary>
        /// Retrieve a specific AiChat
        /// <br/>
        /// Receive a single AiChat object previously created in the Stark Infra API by its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "agentName"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>AiChat object that corresponds to the given id</item>
        /// </list>
        /// </summary>
        public static AiChat Get(string id, List<string> expand = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                query: new Dictionary<string, object> {
                    { "expand", expand }
                },
                user: user
            ) as AiChat;
        }

        /// <summary>
        /// Retrieve AiChats
        /// <br/>
        /// Receive an IEnumerable of AiChat objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "agentName"</item>
        ///     <item>tags [list of strings, default null]: list of strings to filter retrieved objects. ex: new List&lt;string&gt;{ "customer-123", "whatsapp" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of AiChat objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<AiChat> Query(int? limit = null, List<string> expand = null, List<string> tags = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "expand", expand },
                    { "tags", tags }
                },
                user: user
            ).Cast<AiChat>();
        }

        /// <summary>
        /// Retrieve paged AiChat objects
        /// <br/>
        /// Receive a list of up to 100 AiChat objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "agentName"</item>
        ///     <item>tags [list of strings, default null]: list of strings to filter retrieved objects. ex: new List&lt;string&gt;{ "customer-123", "whatsapp" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of AiChat objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of AiChat objects, null on the last page</item>
        /// </list>
        /// </summary>
        public static (List<AiChat> page, string pageCursor) Page(string cursor = null, int? limit = null, List<string> expand = null,
            List<string> tags = null, User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "expand", expand },
                    { "tags", tags }
                },
                user: user
            );
            return (page.Cast<AiChat>().ToList(), pageCursor);
        }

        /// <summary>
        /// Update an AiChat entity
        /// <br/>
        /// Update an AiChat's parameters by passing its id. The API keeps what you do not send;
        /// clear a field with "" for the title, an empty list for the tags or an empty dictionary for the context.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: AiChat unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>title [string, default null]: new title for the conversation. Up to 100 characters</item>
        ///     <item>agentID [string, default null]: id of the AiAgent that should answer from now on</item>
        ///     <item>tags [list of strings, default null]: new list of strings. Replaces the current list</item>
        ///     <item>context [Dictionary object, default null]: new context. Replaces the current object. The keys are sent exactly as written</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target AiChat with updated attributes</item>
        /// </list>
        /// </summary>
        public static AiChat Update(string id, string title = null, string agentID = null, List<string> tags = null,
            Dictionary<string, object> context = null, User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "agentId", agentID },
                    { "title", title },
                    { "tags", tags },
                    { "context", context }
                },
                user: user
            ) as AiChat;
        }

        /// <summary>
        /// Delete AiChats
        /// <br/>
        /// Delete up to 100 AiChats at once, with their messages.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>ids [list of strings]: ids of the AiChats to be deleted. Up to 100 ids. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of deleted AiChat objects</item>
        /// </list>
        /// </summary>
        public static List<AiChat> Delete(List<string> ids, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            StarkCore.Utils.Response response = Rest.DeleteRaw(
                payload: null,
                path: StarkCore.Utils.Api.Endpoint(resourceName),
                query: new Dictionary<string, object> { { "ids", ids } },
                user: user
            );
            return response.Json()[StarkCore.Utils.Api.LastNamePlural(resourceName)].Select(json => resourceMaker(json) as AiChat).ToList();
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "AiChat", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string agentID = json.agentId;
            string title = json.title;
            List<string> tags = json.tags?.ToObject<List<string>>();
            Dictionary<string, object> context = json.context?.ToObject<Dictionary<string, object>>();
            string id = json.id;
            string agentName = json.agentName;
            string updated = json.updated;

            return new AiChat(
                agentID: agentID, title: title, tags: tags, context: context, id: id, agentName: agentName,
                updated: StarkCore.Utils.Checks.CheckNullableDateTime(updated)
            );
        }
    }
}
