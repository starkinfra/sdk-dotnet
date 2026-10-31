using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// AiSpeech object
    /// <br/>
    /// An AiSpeech is one text read out loud by an AiVoice. The speech is synthesized when it is created and comes
    /// back as a base64 MP3 in the audio attribute.
    /// <br/>
    /// When you initialize an AiSpeech, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the object
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>VoiceID [string]: id of the AiVoice that should read the text. Only a voice in "success" can speak. ex: "5656565656565656"</item>
    ///     <item>Text [string]: text to read out loud. Between 1 and 100000 characters. ex: "Hello, how can I help you?"</item>
    ///     <item>ID [string]: unique id returned when the AiSpeech is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current status of the speech. Options: "processing", "success", "failed"</item>
    ///     <item>Audio [string]: base64-encoded MP3 of the speech. Left out of query and page results; get returns it</item>
    ///     <item>VoiceName [string]: name of the voice. Only present when requested with expand: ["voiceName"]</item>
    ///     <item>Errors [list of strings]: reasons the synthesis failed. Empty when it worked</item>
    ///     <item>Created [DateTime]: creation datetime for the AiSpeech. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the AiSpeech. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class AiSpeech : Resource
    {
        public string VoiceID { get; }
        public string Text { get; }
        public string Status { get; }
        public string Audio { get; }
        public string VoiceName { get; }
        public List<string> Errors { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// AiSpeech object
        /// <br/>
        /// An AiSpeech is one text read out loud by an AiVoice. The speech is synthesized when it is created and comes
        /// back as a base64 MP3 in the audio attribute.
        /// <br/>
        /// When you initialize an AiSpeech, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the object
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>voiceID [string]: id of the AiVoice that should read the text. Only a voice in "success" can speak. ex: "5656565656565656"</item>
        ///     <item>text [string]: text to read out loud. Between 1 and 100000 characters. ex: "Hello, how can I help you?"</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the AiSpeech is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the speech. Options: "processing", "success", "failed"</item>
        ///     <item>audio [string]: base64-encoded MP3 of the speech. Left out of query and page results; get returns it</item>
        ///     <item>voiceName [string]: name of the voice. Only present when requested with expand: ["voiceName"]</item>
        ///     <item>errors [list of strings]: reasons the synthesis failed. Empty when it worked</item>
        ///     <item>created [DateTime]: creation datetime for the AiSpeech. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the AiSpeech. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public AiSpeech(string voiceID, string text, string id = null, string status = null, string audio = null,
            string voiceName = null, List<string> errors = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            VoiceID = voiceID;
            Text = text;
            Status = status;
            Audio = audio;
            VoiceName = voiceName;
            Errors = errors;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create an AiSpeech object
        /// <br/>
        /// Send an AiSpeech object for creation in the Stark Infra API. The audio is synthesized during the call.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>speech [AiSpeech object]: AiSpeech object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>AiSpeech object with updated attributes</item>
        /// </list>
        /// </summary>
        public static AiSpeech Create(AiSpeech speech, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: speech,
                user: user
            ) as AiSpeech;
        }

        /// <summary>
        /// Retrieve a specific AiSpeech
        /// <br/>
        /// Receive a single AiSpeech object previously created in the Stark Infra API by its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "voiceName"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>AiSpeech object that corresponds to the given id</item>
        /// </list>
        /// </summary>
        public static AiSpeech Get(string id, List<string> expand = null, User user = null)
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
            ) as AiSpeech;
        }

        /// <summary>
        /// Retrieve AiSpeeches
        /// <br/>
        /// Receive an IEnumerable of AiSpeech objects previously created in the Stark Infra API, following the cursor
        /// until the list ends or the limit is reached. The audio is left out of the results.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "voiceName"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of AiSpeech objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<AiSpeech> Query(int? limit = null, List<string> expand = null, User user = null)
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
            ).Cast<AiSpeech>();
        }

        /// <summary>
        /// Retrieve paged AiSpeech objects
        /// <br/>
        /// Receive a list of up to 100 AiSpeech objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests. The audio is left out of the results.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>expand [list of strings, default null]: extra attributes to compute. Options: "voiceName"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of AiSpeech objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of AiSpeech objects, null on the last page</item>
        /// </list>
        /// </summary>
        public static (List<AiSpeech> page, string pageCursor) Page(string cursor = null, int? limit = null, List<string> expand = null,
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
            return (page.Cast<AiSpeech>().ToList(), pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "AiSpeech", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string voiceID = json.voiceId;
            string text = json.text;
            string id = json.id;
            string status = json.status;
            string audio = json.audio;
            string voiceName = json.voiceName;
            List<string> errors = json.errors?.ToObject<List<string>>();
            string created = json.created;
            string updated = json.updated;

            return new AiSpeech(
                voiceID: voiceID, text: text, id: id, status: status, audio: audio, voiceName: voiceName, errors: errors,
                created: StarkCore.Utils.Checks.CheckNullableDateTime(created),
                updated: StarkCore.Utils.Checks.CheckNullableDateTime(updated)
            );
        }
    }
}
