using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// AiVoice object
    /// <br/>
    /// An AiVoice is a voice cloned from a recording you upload. Once cloned, it can read any text out loud through
    /// an AiSpeech, and it can be attached to an AiAgent so every reply carries a speech ready to be synthesized.
    /// Cloning is asynchronous: the voice is created in "processing" status and moves to "success" when it is ready
    /// to speak, or to "failed" when the recording could not be cloned.
    /// <br/>
    /// When you initialize an AiVoice, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the object
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Audio [string]: base64-encoded recording of the speaker. MP3, WAV, OGG, FLAC and WebM are accepted. Up to 10000000 characters. Write-only: the API never returns it</item>
    ///     <item>Name [string, default null]: name of the voice. Up to 100 characters. Defaults to the voice's own id. ex: "Helena"</item>
    ///     <item>Description [string, default null]: free-text description of the voice. Up to 1000 characters. ex: "Calm voice"</item>
    ///     <item>Language [string, default null]: language the voice speaks. Options: "portuguese", "english". The API defaults to "portuguese"</item>
    ///     <item>Gender [string, default null]: gender of the voice. Options: "male", "female", "neutral"</item>
    ///     <item>ID [string]: unique id returned when the AiVoice is created. This is the voiceID you send to other AI resources. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current status of the voice. Options: "processing", "success", "failed". Only a voice in "success" can speak</item>
    ///     <item>Errors [list of strings]: reasons the cloning failed. Empty while the voice is healthy</item>
    ///     <item>Created [DateTime]: creation datetime for the AiVoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the AiVoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class AiVoice : Resource
    {
        public string Audio { get; }
        public string Name { get; }
        public string Description { get; }
        public string Language { get; }
        public string Gender { get; }
        public string Status { get; }
        public List<string> Errors { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// AiVoice object
        /// <br/>
        /// An AiVoice is a voice cloned from a recording you upload. Once cloned, it can read any text out loud through
        /// an AiSpeech, and it can be attached to an AiAgent so every reply carries a speech ready to be synthesized.
        /// <br/>
        /// When you initialize an AiVoice, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the object
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>audio [string]: base64-encoded recording of the speaker. MP3, WAV, OGG, FLAC and WebM are accepted. Up to 10000000 characters</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>name [string, default null]: name of the voice. Up to 100 characters. Defaults to the voice's own id. ex: "Helena"</item>
        ///     <item>description [string, default null]: free-text description of the voice. Up to 1000 characters. ex: "Calm voice"</item>
        ///     <item>language [string, default null]: language the voice speaks. Options: "portuguese", "english". The API defaults to "portuguese"</item>
        ///     <item>gender [string, default null]: gender of the voice. Options: "male", "female", "neutral"</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the AiVoice is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the voice. Options: "processing", "success", "failed". Only a voice in "success" can speak</item>
        ///     <item>errors [list of strings]: reasons the cloning failed. Empty while the voice is healthy</item>
        ///     <item>created [DateTime]: creation datetime for the AiVoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the AiVoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public AiVoice(string audio = null, string name = null, string description = null, string language = null, string gender = null,
            string id = null, string status = null, List<string> errors = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Audio = audio;
            Name = name;
            Description = description;
            Language = language;
            Gender = gender;
            Status = status;
            Errors = errors;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create an AiVoice object
        /// <br/>
        /// Send an AiVoice object for creation in the Stark Infra API and start cloning it.
        /// The call returns immediately with the voice in "processing" status.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>voice [AiVoice object]: AiVoice object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>AiVoice object with updated attributes</item>
        /// </list>
        /// </summary>
        public static AiVoice Create(AiVoice voice, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: voice,
                user: user
            ) as AiVoice;
        }

        /// <summary>
        /// Retrieve AiVoices
        /// <br/>
        /// Receive an IEnumerable of AiVoice objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of AiVoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<AiVoice> Query(int? limit = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit }
                },
                user: user
            ).Cast<AiVoice>();
        }

        /// <summary>
        /// Retrieve paged AiVoice objects
        /// <br/>
        /// Receive a list of up to 100 AiVoice objects previously created in the Stark Infra API and the cursor to the next page.
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
        ///     <item>list of AiVoice objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of AiVoice objects, null on the last page</item>
        /// </list>
        /// </summary>
        public static (List<AiVoice> page, string pageCursor) Page(string cursor = null, int? limit = null, User user = null)
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
            return (page.Cast<AiVoice>().ToList(), pageCursor);
        }

        /// <summary>
        /// Delete AiVoices
        /// <br/>
        /// Delete up to 100 AiVoices at once.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>ids [list of strings]: ids of the AiVoices to be deleted. Up to 100 ids. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of deleted AiVoice objects</item>
        /// </list>
        /// </summary>
        public static List<AiVoice> Delete(List<string> ids, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            StarkCore.Utils.Response response = Rest.DeleteRaw(
                payload: null,
                path: StarkCore.Utils.Api.Endpoint(resourceName),
                query: new Dictionary<string, object> { { "ids", ids } },
                user: user
            );
            return response.Json()[StarkCore.Utils.Api.LastNamePlural(resourceName)].Select(json => resourceMaker(json) as AiVoice).ToList();
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "AiVoice", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string description = json.description;
            string language = json.language;
            string gender = json.gender;
            string id = json.id;
            string status = json.status;
            List<string> errors = json.errors?.ToObject<List<string>>();
            string created = json.created;
            string updated = json.updated;

            return new AiVoice(
                name: name, description: description, language: language, gender: gender, id: id, status: status,
                errors: errors, created: StarkCore.Utils.Checks.CheckNullableDateTime(created),
                updated: StarkCore.Utils.Checks.CheckNullableDateTime(updated)
            );
        }
    }
}
