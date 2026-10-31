using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// AiAgent object
    /// <br/>
    /// An AiAgent is the configuration of an assistant: the model, the instructions, the knowledge it may consult and
    /// the voice it speaks with. The agent never changes during a conversation; the conversation lives in an AiChat
    /// and each turn is an AiMessage.
    /// <br/>
    /// When you initialize an AiAgent, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the object
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: name of the agent. Between 1 and 100 characters. ex: "Support assistant"</item>
    ///     <item>Model [string]: AI model the agent runs on. Options: "bender-1.0" for everyday conversations, "prime-1.0" for harder reasoning</item>
    ///     <item>SystemPrompt [string, default null]: instructions that define the agent's persona, tone and domain behavior. Up to 100000 characters. The API falls back to its default assistant prompt when omitted</item>
    ///     <item>VoiceID [string, default null]: id of the AiVoice the agent speaks with. When set, every reply also carries a speech string ready to be sent to AiSpeech. The API does not check that the voice exists</item>
    ///     <item>KnowledgeBaseIds [list of strings, default null]: ids of up to 100 AiKnowledgeBases the agent retrieves from before answering. The API does not check that they exist</item>
    ///     <item>MetadataSchema [Dictionary object, default null]: flat dictionary whose keys are the fields the agent must extract on every reply. Each field takes a "type" (string, integer, number, boolean or array), an optional "description" of up to 2000 characters, an optional "enum" of up to 20 strings for string fields. The keys are yours and are sent exactly as written. ex: new Dictionary&lt;string, object&gt;{ { "order_id", new Dictionary&lt;string, object&gt;{ { "type", "string" }, { "description", "Order the customer mentions" } } } }</item>
    ///     <item>ID [string]: unique id returned when the AiAgent is created. ex: "5656565656565656"</item>
    ///     <item>KnowledgeBases [list of AiKnowledgeBase objects]: the knowledge bases themselves. Only present when requested with expand: ["knowledgeBases"]</item>
    ///     <item>Created [DateTime]: creation datetime for the AiAgent. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the AiAgent. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class AiAgent : Resource
    {
        public string Name { get; }
        public string Model { get; }
        public string SystemPrompt { get; }
        public string VoiceID { get; }
        public List<string> KnowledgeBaseIds { get; }
        public Dictionary<string, object> MetadataSchema { get; }
        public List<AiKnowledgeBase> KnowledgeBases { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// AiAgent object
        /// <br/>
        /// An AiAgent is the configuration of an assistant: the model, the instructions, the knowledge it may consult and
        /// the voice it speaks with. The agent never changes during a conversation; the conversation lives in an AiChat
        /// and each turn is an AiMessage.
        /// <br/>
        /// When you initialize an AiAgent, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the object
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: name of the agent. Between 1 and 100 characters. ex: "Support assistant"</item>
        ///     <item>model [string]: AI model the agent runs on. Options: "bender-1.0" for everyday conversations, "prime-1.0" for harder reasoning</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>systemPrompt [string, default null]: instructions that define the agent's persona, tone and domain behavior. Up to 100000 characters. The API falls back to its default assistant prompt when omitted</item>
        ///     <item>voiceID [string, default null]: id of the AiVoice the agent speaks with. When set, every reply also carries a speech string ready to be sent to AiSpeech. The API does not check that the voice exists</item>
        ///     <item>knowledgeBaseIds [list of strings, default null]: ids of up to 100 AiKnowledgeBases the agent retrieves from before answering. The API does not check that they exist</item>
        ///     <item>metadataSchema [Dictionary object, default null]: flat dictionary whose keys are the fields the agent must extract on every reply. Each field takes a "type" (string, integer, number, boolean or array), an optional "description" of up to 2000 characters, an optional "enum" of up to 20 strings for string fields. The keys are yours and are sent exactly as written</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the AiAgent is created. ex: "5656565656565656"</item>
        ///     <item>knowledgeBases [list of AiKnowledgeBase objects]: the knowledge bases themselves. Only present when requested with expand: ["knowledgeBases"]</item>
        ///     <item>created [DateTime]: creation datetime for the AiAgent. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the AiAgent. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public AiAgent(string name, string model, string systemPrompt = null, string voiceID = null, List<string> knowledgeBaseIds = null,
            Dictionary<string, object> metadataSchema = null, string id = null, List<AiKnowledgeBase> knowledgeBases = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Name = name;
            Model = model;
            SystemPrompt = systemPrompt;
            VoiceID = voiceID;
            KnowledgeBaseIds = knowledgeBaseIds;
            MetadataSchema = metadataSchema;
            KnowledgeBases = knowledgeBases;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create an AiAgent object
        /// <br/>
        /// Send an AiAgent object for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>agent [AiAgent object]: AiAgent object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>AiAgent object with updated attributes</item>
        /// </list>
        /// </summary>
        public static AiAgent Create(AiAgent agent, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: agent,
                user: user
            ) as AiAgent;
        }

        /// <summary>
        /// Retrieve a specific AiAgent
        /// <br/>
        /// Receive a single AiAgent object previously created in the Stark Infra API by its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "knowledgeBases"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>AiAgent object that corresponds to the given id</item>
        /// </list>
        /// </summary>
        public static AiAgent Get(string id, List<string> expand = null, User user = null)
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
            ) as AiAgent;
        }

        /// <summary>
        /// Retrieve AiAgents
        /// <br/>
        /// Receive an IEnumerable of AiAgent objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "knowledgeBases"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of AiAgent objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<AiAgent> Query(int? limit = null, List<string> expand = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "expand", expand }
                },
                user: user
            ).Cast<AiAgent>();
        }

        /// <summary>
        /// Retrieve paged AiAgent objects
        /// <br/>
        /// Receive a list of up to 100 AiAgent objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "knowledgeBases"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of AiAgent objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of AiAgent objects, null on the last page</item>
        /// </list>
        /// </summary>
        public static (List<AiAgent> page, string pageCursor) Page(string cursor = null, int? limit = null, List<string> expand = null,
            User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "expand", expand }
                },
                user: user
            );
            return (page.Cast<AiAgent>().ToList(), pageCursor);
        }

        /// <summary>
        /// Update an AiAgent entity
        /// <br/>
        /// Update an AiAgent's parameters by passing its id. The API keeps what you do not send;
        /// clear a field with "" for the system prompt and the voice, an empty list for the knowledge bases or an
        /// empty dictionary for the metadata schema.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: AiAgent unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>name [string, default null]: new name for the agent. Between 1 and 100 characters</item>
        ///     <item>model [string, default null]: new AI model. Options: "bender-1.0", "prime-1.0"</item>
        ///     <item>systemPrompt [string, default null]: new instructions for the agent. Up to 100000 characters</item>
        ///     <item>voiceID [string, default null]: new AiVoice id</item>
        ///     <item>knowledgeBaseIds [list of strings, default null]: the AiKnowledgeBase ids the agent should end up with. Replaces the current list</item>
        ///     <item>metadataSchema [Dictionary object, default null]: new schema of the structured data the agent must extract. The keys are sent exactly as written</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target AiAgent with updated attributes</item>
        /// </list>
        /// </summary>
        public static AiAgent Update(string id, string name = null, string model = null, string systemPrompt = null, string voiceID = null,
            List<string> knowledgeBaseIds = null, Dictionary<string, object> metadataSchema = null, User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "name", name },
                    { "model", model },
                    { "systemPrompt", systemPrompt },
                    { "voiceId", voiceID },
                    { "knowledgeBaseIds", knowledgeBaseIds },
                    { "metadataSchema", metadataSchema }
                },
                user: user
            ) as AiAgent;
        }

        /// <summary>
        /// Delete AiAgents
        /// <br/>
        /// Delete up to 100 AiAgents at once.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>ids [list of strings]: ids of the AiAgents to be deleted. Up to 100 ids. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of deleted AiAgent objects</item>
        /// </list>
        /// </summary>
        public static List<AiAgent> Delete(List<string> ids, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            StarkCore.Utils.Response response = Rest.DeleteRaw(
                payload: null,
                path: StarkCore.Utils.Api.Endpoint(resourceName),
                query: new Dictionary<string, object> { { "ids", ids } },
                user: user
            );
            return response.Json()[StarkCore.Utils.Api.LastNamePlural(resourceName)].Select(json => resourceMaker(json) as AiAgent).ToList();
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "AiAgent", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string model = json.model;
            string systemPrompt = json.systemPrompt;
            string voiceID = json.voiceId;
            List<string> knowledgeBaseIds = json.knowledgeBaseIds?.ToObject<List<string>>();
            Dictionary<string, object> metadataSchema = json.metadataSchema?.ToObject<Dictionary<string, object>>();
            string id = json.id;
            List<AiKnowledgeBase> knowledgeBases = null;
            if (json.knowledgeBases != null)
            {
                knowledgeBases = new List<AiKnowledgeBase>();
                foreach (dynamic knowledgeBase in json.knowledgeBases)
                {
                    knowledgeBases.Add(AiKnowledgeBase.ResourceMaker(knowledgeBase) as AiKnowledgeBase);
                }
            }
            string created = json.created;
            string updated = json.updated;

            return new AiAgent(
                name: name, model: model, systemPrompt: systemPrompt, voiceID: voiceID, knowledgeBaseIds: knowledgeBaseIds,
                metadataSchema: metadataSchema, id: id, knowledgeBases: knowledgeBases,
                created: StarkCore.Utils.Checks.CheckNullableDateTime(created),
                updated: StarkCore.Utils.Checks.CheckNullableDateTime(updated)
            );
        }
    }
}
