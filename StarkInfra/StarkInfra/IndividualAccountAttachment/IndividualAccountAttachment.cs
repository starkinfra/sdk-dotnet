using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IndividualAccountAttachment object
    /// <br/>
    /// Supporting document (identity document, driver's license) attached to an IndividualAccountRequest
    /// for the account-approval flow. The caller uploads the raw image bytes and a MIME content type;
    /// the SDK encodes them as a data: URL before sending.
    /// <br/>
    /// When you initialize an IndividualAccountAttachment, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Type [string]: type of the IndividualAccountAttachment. Options: "drivers-license-front", "drivers-license-back", "identity-front", "identity-back"</item>
    ///     <item>Content [string]: data: URL of the picture. ex: "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAASABIAAD..."</item>
    ///     <item>ContentType [string]: input-only MIME type, consumed client-side to build the Content data: URL. Never populated on a response object.</item>
    ///     <item>AccountRequestID [string]: id of the parent IndividualAccountRequest. ex: "5189530608992256"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for IndividualAccountAttachments.</item>
    ///     <item>ID [string]: unique id returned when the IndividualAccountAttachment is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current status of the IndividualAccountAttachment. Options: "created", "success", "failed", "deleted"</item>
    ///     <item>Created [DateTime]: creation DateTime for the IndividualAccountAttachment. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IndividualAccountAttachment : Resource
    {
        public string Type { get; }
        public string Content { get; }
        public string ContentType { get; }
        public string AccountRequestID { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IndividualAccountAttachment object
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>type [string]: type of the IndividualAccountAttachment. Options: "drivers-license-front", "drivers-license-back", "identity-front", "identity-back"</item>
        ///     <item>content [byte[]]: raw image bytes. Encoded client-side into a data: URL.</item>
        ///     <item>contentType [string]: content MIME type. This parameter is required as input only and is never sent as its own wire field. ex: "image/png" or "image/jpeg"</item>
        ///     <item>accountRequestID [string]: id of the parent IndividualAccountRequest. ex: "5189530608992256"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for IndividualAccountAttachments. ex: new List<string>{ "employees" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the IndividualAccountAttachment is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the IndividualAccountAttachment. Options: "created", "success", "failed", "deleted"</item>
        ///     <item>created [DateTime]: creation DateTime for the IndividualAccountAttachment. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IndividualAccountAttachment(
            string type, byte[] content, string contentType, string accountRequestID, List<string> tags = null,
            string id = null, string status = null, DateTime? created = null
        ) : base(id)
        {
            Type = type;
            Content = "data:" + contentType + ";base64," + Convert.ToBase64String(content);
            AccountRequestID = accountRequestID;
            Tags = tags;
            Status = status;
            Created = created;
        }

        /// <summary>
        /// IndividualAccountAttachment object
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>type [string]: type of the IndividualAccountAttachment. Options: "drivers-license-front", "drivers-license-back", "identity-front", "identity-back"</item>
        ///     <item>content [string]: pre-built data: URL of the picture. ex: "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAASABIAAD..."</item>
        ///     <item>accountRequestID [string]: id of the parent IndividualAccountRequest. ex: "5189530608992256"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for IndividualAccountAttachments. ex: new List<string>{ "employees" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the IndividualAccountAttachment is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the IndividualAccountAttachment. Options: "created", "success", "failed", "deleted"</item>
        ///     <item>created [DateTime]: creation DateTime for the IndividualAccountAttachment. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IndividualAccountAttachment(
            string type, string content, string accountRequestID, List<string> tags = null,
            string id = null, string status = null, DateTime? created = null
        ) : base(id)
        {
            Type = type;
            Content = content;
            AccountRequestID = accountRequestID;
            Tags = tags;
            Status = status;
            Created = created;
        }

        /// <summary>
        /// Create IndividualAccountAttachment objects
        /// <br/>
        /// Send a list of IndividualAccountAttachment objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>attachments [list of IndividualAccountAttachment objects]: list of IndividualAccountAttachment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualAccountAttachment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IndividualAccountAttachment> Create(List<IndividualAccountAttachment> attachments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: attachments,
                user: user
            ).ToList().ConvertAll(o => (IndividualAccountAttachment)o);
        }

        /// <summary>
        /// Create IndividualAccountAttachment objects
        /// <br/>
        /// Send a list of IndividualAccountAttachment dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>attachments [list of Dictionaries]: list of dictionaries representing the IndividualAccountAttachment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualAccountAttachment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IndividualAccountAttachment> Create(List<Dictionary<string, object>> attachments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: attachments,
                user: user
            ).ToList().ConvertAll(o => (IndividualAccountAttachment)o);
        }

        /// <summary>
        /// Retrieve a specific IndividualAccountAttachment by its id
        /// <br/>
        /// Receive a single IndividualAccountAttachment object previously created in the Stark Infra API by passing its id
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
        ///     <item>IndividualAccountAttachment object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IndividualAccountAttachment Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IndividualAccountAttachment;
        }

        /// <summary>
        /// Retrieve IndividualAccountAttachment objects
        /// <br/>
        /// Receive an IEnumerable of IndividualAccountAttachment objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "employees" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IndividualAccountAttachment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IndividualAccountAttachment> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> tags = null, List<string> ids = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<IndividualAccountAttachment>();
        }

        /// <summary>
        /// Retrieve paged IndividualAccountAttachment objects
        /// <br/>
        /// Receive a list of up to 100 IndividualAccountAttachment objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "employees" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualAccountAttachment objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IndividualAccountAttachment objects</item>
        /// </list>
        /// </summary>
        public static (List<IndividualAccountAttachment> page, string pageCursor) Page(string cursor = null, int? limit = null,
            DateTime? after = null, DateTime? before = null, List<string> status = null, List<string> tags = null,
            List<string> ids = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            );
            List<IndividualAccountAttachment> attachments = new List<IndividualAccountAttachment>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                attachments.Add(subResource as IndividualAccountAttachment);
            }
            return (attachments, pageCursor);
        }

        /// <summary>
        /// Cancel an IndividualAccountAttachment entity
        /// <br/>
        /// Cancel an IndividualAccountAttachment entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: IndividualAccountAttachment unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled IndividualAccountAttachment object with status "deleted"</item>
        /// </list>
        /// </summary>
        public static IndividualAccountAttachment Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IndividualAccountAttachment;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IndividualAccountAttachment", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string type = json.type;
            string content = json.content;
            string accountRequestID = json.accountRequestId;
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);

            return new IndividualAccountAttachment(
                type: type, content: content, accountRequestID: accountRequestID, tags: tags,
                id: id, status: status, created: created
            );
        }
    }
}
