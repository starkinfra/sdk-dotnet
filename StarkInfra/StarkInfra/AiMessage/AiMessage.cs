using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// AiMessage object
    /// <br/>
    /// An AiMessage is a single turn of an AiChat. You post what the user said and the same call returns the user's
    /// message and the agent's answer.
    /// <br/>
    /// When you initialize an AiMessage, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the object
    /// to the Stark Infra API and returns the user's message and the agent's answer.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ChatID [string]: id of the AiChat to post to. ex: "5656565656565656"</item>
    ///     <item>Text [string]: content of the message. Between 1 and 50000 characters. ex: "What is the status of my order?"</item>
    ///     <item>Model [string, default null]: AI model to use for this turn only. Options: "bender-1.0", "prime-1.0". The API defaults to the agent's own model</item>
    ///     <item>ID [string]: unique id of the AiMessage. ex: "5656565656565656"</item>
    ///     <item>Sender [string]: who wrote the message. Options: "user", "system". The agent's answers are sent by "system"</item>
    ///     <item>Speech [string]: version of the text written to be heard rather than read, ready to be sent to AiSpeech. Only filled when the agent has a voice</item>
    ///     <item>Metadata [Dictionary object]: structured data the agent extracted, shaped by the agent's metadataSchema. The keys are the agent's, exactly as it declared them</item>
    ///     <item>ChatName [string]: title of the chat. Only present when create is called with expand: ["chatName"]</item>
    ///     <item>Created [DateTime]: creation datetime for the AiMessage. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class AiMessage : Resource
    {
        public string ChatID { get; }
        public string Text { get; }
        public string Model { get; }
        public string Sender { get; }
        public string Speech { get; }
        public Dictionary<string, object> Metadata { get; }
        public string ChatName { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// AiMessage object
        /// <br/>
        /// An AiMessage is a single turn of an AiChat. You post what the user said and the same call returns the user's
        /// message and the agent's answer.
        /// <br/>
        /// When you initialize an AiMessage, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the object
        /// to the Stark Infra API and returns the user's message and the agent's answer.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>chatID [string]: id of the AiChat to post to. ex: "5656565656565656"</item>
        ///     <item>text [string]: content of the user's message. Between 1 and 50000 characters. ex: "What is the status of my order?"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>model [string, default null]: AI model to use for this turn only. Options: "bender-1.0", "prime-1.0". The API defaults to the agent's own model</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id of the AiMessage. ex: "5656565656565656"</item>
        ///     <item>sender [string]: who wrote the message. Options: "user", "system". The agent's answers are sent by "system"</item>
        ///     <item>speech [string]: version of the text written to be heard rather than read, ready to be sent to AiSpeech. Only filled when the agent has a voice</item>
        ///     <item>metadata [Dictionary object]: structured data the agent extracted, shaped by the agent's metadataSchema. The keys are the agent's, exactly as it declared them</item>
        ///     <item>chatName [string]: title of the chat. Only present when create is called with expand: ["chatName"]</item>
        ///     <item>created [DateTime]: creation datetime for the AiMessage. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public AiMessage(string chatID, string text, string model = null, string id = null, string sender = null, string speech = null,
            Dictionary<string, object> metadata = null, string chatName = null, DateTime? created = null
        ) : base(id)
        {
            ChatID = chatID;
            Text = text;
            Model = model;
            Sender = sender;
            Speech = speech;
            Metadata = metadata;
            ChatName = chatName;
            Created = created;
        }

        /// <summary>
        /// Create an AiMessage object
        /// <br/>
        /// Post the user's message to an AiChat. The call waits for the agent, which takes a few seconds, and returns both messages.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>message [AiMessage object]: AiMessage object with chatID and text, to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "chatName", which returns the chat title on every message, useful on the first turn, when the title is generated</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list with the user's AiMessage and the agent's AiMessage</item>
        /// </list>
        /// </summary>
        public static List<AiMessage> Create(AiMessage message, List<string> expand = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            StarkCore.Utils.Response response = Rest.PostRaw(
                payload: new Dictionary<string, object> {
                    { "chatId", message.ChatID },
                    { "text", message.Text },
                    { "model", message.Model }
                },
                path: StarkCore.Utils.Api.Endpoint(resourceName),
                query: new Dictionary<string, object> { { "expand", expand } },
                user: user
            );
            Newtonsoft.Json.Linq.JObject content = response.Json();
            string chatName = (string)content["chatName"];
            return content["messages"].Select(json => {
                json["chatName"] = chatName;
                return resourceMaker(json) as AiMessage;
            }).ToList();
        }

        /// <summary>
        /// Retrieve AiMessages
        /// <br/>
        /// Receive an IEnumerable of AiMessage objects, following the cursor until the history ends.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>chatID [string, default null]: id of the AiChat whose messages you want. When omitted, the messages of every chat of the workspace come back. ex: "5656565656565656"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of AiMessage objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<AiMessage> Query(int? limit = null, string chatID = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "chatId", chatID }
                },
                user: user
            ).Cast<AiMessage>();
        }

        /// <summary>
        /// Retrieve paged AiMessage objects
        /// <br/>
        /// Receive a list of up to 100 AiMessage objects and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>chatID [string, default null]: id of the AiChat whose messages you want. When omitted, the messages of every chat of the workspace come back. ex: "5656565656565656"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of AiMessage objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of AiMessage objects, null on the last page</item>
        /// </list>
        /// </summary>
        public static (List<AiMessage> page, string pageCursor) Page(string cursor = null, int? limit = null, string chatID = null,
            User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "chatId", chatID }
                },
                user: user
            );
            return (page.Cast<AiMessage>().ToList(), pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "AiMessage", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string chatID = json.chatId;
            string text = json.text;
            string model = json.model;
            string id = json.id;
            string sender = json.sender;
            string speech = json.speech;
            Dictionary<string, object> metadata = json.metadata?.ToObject<Dictionary<string, object>>();
            string chatName = json.chatName;
            string created = json.created;

            return new AiMessage(
                chatID: chatID, text: text, model: model, id: id, sender: sender, speech: speech, metadata: metadata,
                chatName: chatName, created: StarkCore.Utils.Checks.CheckNullableDateTime(created)
            );
        }
    }
}
