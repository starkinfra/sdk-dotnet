using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// AiTranscript object
    /// <br/>
    /// An AiTranscript is the text of an audio file you upload, from any speaker, cloned or not.
    /// <br/>
    /// When you initialize an AiTranscript, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the object
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Audio [string]: base64-encoded audio to transcribe. Up to 10000000 characters. The format is read from the file's own header. Write-only: the API never returns it</item>
    ///     <item>ID [string]: unique id returned when the AiTranscript is created. ex: "5656565656565656"</item>
    ///     <item>Text [string]: transcribed text. ex: "This is a short recording."</item>
    ///     <item>Status [string]: current status of the transcript. Options: "processing", "success", "failed"</item>
    ///     <item>Errors [list of strings]: reasons the transcription failed. Empty when it worked</item>
    ///     <item>Created [DateTime]: creation datetime for the AiTranscript. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the AiTranscript. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class AiTranscript : Resource
    {
        public string Audio { get; }
        public string Text { get; }
        public string Status { get; }
        public List<string> Errors { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// AiTranscript object
        /// <br/>
        /// An AiTranscript is the text of an audio file you upload, from any speaker, cloned or not.
        /// <br/>
        /// When you initialize an AiTranscript, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the object
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>audio [string]: base64-encoded audio to transcribe. Up to 10000000 characters. The format is read from the file's own header</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the AiTranscript is created. ex: "5656565656565656"</item>
        ///     <item>text [string]: transcribed text. ex: "This is a short recording."</item>
        ///     <item>status [string]: current status of the transcript. Options: "processing", "success", "failed"</item>
        ///     <item>errors [list of strings]: reasons the transcription failed. Empty when it worked</item>
        ///     <item>created [DateTime]: creation datetime for the AiTranscript. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the AiTranscript. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public AiTranscript(string audio = null, string id = null, string text = null, string status = null,
            List<string> errors = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Audio = audio;
            Text = text;
            Status = status;
            Errors = errors;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create an AiTranscript object
        /// <br/>
        /// Send an AiTranscript object for creation in the Stark Infra API. The audio is transcribed during the call.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>transcript [AiTranscript object]: AiTranscript object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>AiTranscript object with updated attributes</item>
        /// </list>
        /// </summary>
        public static AiTranscript Create(AiTranscript transcript, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: transcript,
                user: user
            ) as AiTranscript;
        }

        /// <summary>
        /// Retrieve AiTranscripts
        /// <br/>
        /// Receive an IEnumerable of AiTranscript objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of AiTranscript objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<AiTranscript> Query(int? limit = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit }
                },
                user: user
            ).Cast<AiTranscript>();
        }

        /// <summary>
        /// Retrieve paged AiTranscript objects
        /// <br/>
        /// Receive a list of up to 100 AiTranscript objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of AiTranscript objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of AiTranscript objects, null on the last page</item>
        /// </list>
        /// </summary>
        public static (List<AiTranscript> page, string pageCursor) Page(string cursor = null, int? limit = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit }
                },
                user: user
            );
            return (page.Cast<AiTranscript>().ToList(), pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "AiTranscript", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string text = json.text;
            string status = json.status;
            List<string> errors = json.errors?.ToObject<List<string>>();
            string created = json.created;
            string updated = json.updated;

            return new AiTranscript(
                id: id, text: text, status: status, errors: errors,
                created: StarkCore.Utils.Checks.CheckNullableDateTime(created),
                updated: StarkCore.Utils.Checks.CheckNullableDateTime(updated)
            );
        }
    }
}
