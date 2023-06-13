using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IndividualDocument object
    /// <br/>
    /// Individual documents are images containing either side of a document or a selfie
    /// to be used in a matching validation. When created, they must be attached to an individual
    /// identity to be used for its validation.
    /// <br/>
    /// When you initialize a IndividualDocument, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Type [string]: type of the IndividualDocument. Options: "drivers-license-front", "drivers-license-back", "identity-front", "identity-back" or "selfie"
    ///     <item>Content [string]: Base64 data url of the picture. ex: data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAASABIAAD...</item>
    ///     <item>contentType [string]: content MIME type. This parameter is required as input only. ex: "image/png" or "image/jpeg"<item>
    ///     <item>IdentityID [string]: Unique id of IndividualIdentity. ex: "5656565656565656"</item>
    ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for IndividualDocuments. ex: List<string>{ "employees", "monthly" }</item>
    ///     <item>Id [string]: unique id returned when the IndividualDocument is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current status of the IndividualDocument. Options: "created", "canceled", "processing", "failed", "success"</item>
    ///     <item>Created [DateTime]: creation DateTime for the IndividualDocument. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IndividualDocument : Resource
    {
        public string Type { get; }
        public string Content { get; }
        public string ContentType { get; }
        public string IdentityID { get; }
        public List<string> Tags { get; }
        public string Id { get; }
        public string Status { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IndividualDocument object
        /// <br/>
        /// Individual documents are images containing either side of a document or a selfie
        /// to be used in a matching validation. When created, they must be attached to an individual
        /// identity to be used for its validation.
        /// <br/>
        /// When you initialize a IndividualDocument, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>type [string]: type of the IndividualDocument. Options: "drivers-license-front", "drivers-license-back", "identity-front", "identity-back" or "selfie"</item>
        ///     <item>content [string]: Base64 data url of the picture. ex: data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAASABIAAD...</item>
        ///     <item>contentType [string]: content MIME type. This parameter is required as input only. ex: "image/png" or "image/jpeg"<item>
        ///     <item>identityID [string]: Unique id of IndividualIdentity. ex: "5656565656565656"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default []]: list of strings for reference when searching for IndividualDocument. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the IndividualDocument is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the IndividualDocument. Options: "created", "canceled", "processing", "failed", "success"</item>
        ///     <item>created [DateTime]: creation DateTime for the IndividualDocument. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IndividualDocument(
            string type, string content, string identityID, string contentType = null, List<string> tags = null, 
            string id = null, string status = null, DateTime? created = null
        ) : base(id)
        {
            Type = type;
            Content = content;
            ContentType = contentType;
            IdentityID = identityID;
            Tags = tags;
            Status = status;
            Created = created;
        }

        /// <summary>
        /// IndividualDocument object
        /// <br/>
        /// Individual documents are images containing either side of a document or a selfie
        /// to be used in a matching validation. When created, they must be attached to an individual
        /// identity to be used for its validation.
        /// <br/>
        /// When you initialize a IndividualDocument, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>type [string]: type of the IndividualDocument. Options: "drivers-license-front", "drivers-license-back", "identity-front", "identity-back" or "selfie"</item>
        ///     <item>content [byte[]]: Base64 data url of the picture. ex: data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAASABIAAD...</item>
        ///     <item>contentType [string]: content MIME type. This parameter is required as input only. ex: "image/png" or "image/jpeg"</item>
        ///     <item>identityID [string]: Unique id of IndividualIdentity. ex: "5656565656565656"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default []]: list of strings for reference when searching for IndividualDocument. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the IndividualDocument is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the IndividualDocument. Options: "created", "canceled", "processing", "failed", "success"</item>
        ///     <item>created [DateTime]: creation DateTime for the IndividualDocument. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IndividualDocument(
            string type, byte[] content, string contentType, string identityID, List<string> tags = null, 
            string id = null, string status = null, DateTime? created = null
        ) : base(id)
        {
            Type = type;
            Content = "data:" + contentType + ";base64," + Convert.ToBase64String(content);
            IdentityID = identityID;
            Tags = tags;
            Status = status;
            Created = created;
        }

        /// <summary>
        /// Create IndividualDocument objects
        /// <br/>
        /// Send a list of IndividualDocument objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>documents [list of IndividualDocument objects]: list of IndividualDocument objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualDocument objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IndividualDocument> Create(List<IndividualDocument> documents, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: documents,
                user: user
            ).ToList().ConvertAll(o => (IndividualDocument)o);
        }

        /// <summary>
        /// Create IndividualDocument objects
        /// <br/>
        /// Send a list of IndividualDocument dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>documents [list of Dictionaries]: list of dictionaries representing the IndividualDocument objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualDocument objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IndividualDocument> Create(List<Dictionary<string, object>> documents, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: documents,
                user: user
            ).ToList().ConvertAll(o => (IndividualDocument)o);
        }

        /// <summary>
        /// Retrieve a specific IndividualDocument by its id
        /// <br/>
        /// Receive a single IndividualDocument object previously created in the Stark Infra API by passing its id
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
        ///     <item>IndividualDocument object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IndividualDocument Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IndividualDocument;
        }

        /// <summary>
        /// Retrieve IndividualDocument objects
        /// <br/>
        /// Receive an IEnumerable of IndividualDocument objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default None]: filter for status of retrieved objects. Options: ["created", "canceled", "processing", "failed", "success"]</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IndividualDocument objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IndividualDocument> Query(int? limit = null, DateTime? after = null, DateTime? before = null, 
            List<string> status = null, List<string> tags = null, List<string> ids = null, User user = null
        )
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<IndividualDocument>();
        }

        /// <summary>
        /// Retrieve paged IndividualDocument objects
        /// <br/>
        /// Receive a list of up to 100 IndividualDocument objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default None]: filter for status of retrieved objects. Options: ["created", "canceled", "processing", "failed", "success"]</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualDocument objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IndividualDocument objects</item>
        /// </list>
        /// </summary>
        public static (List<IndividualDocument> page, string pageCursor) Page(string cursor = null, int? limit = null, 
            DateTime? after = null, DateTime? before = null, List<string> status = null, List<string> tags = null,
            List<string> ids = null, User user = null
        ) {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            );
            List<IndividualDocument> documents = new List<IndividualDocument>();
            foreach (SubResource subResource in page)
            {
                documents.Add(subResource as IndividualDocument);
            }
            return (documents, pageCursor);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IndividualDocument", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string type = json.type;
            string content = json.content;
            string identityID = json.identityId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);

            return new IndividualDocument(
                type: type, content: content, identityID: identityID, 
                tags: tags, id: id, status: status, created: created
            );
        }
    }
}
