using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// BusinessAttachment object
    /// <br/>
    /// Business attachments are files containing documents of a business
    /// to be used in a matching validation. When created, they must be attached to a business
    /// identity to be used for its validation.
    /// <br/>
    /// When you initialize a BusinessAttachment, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: name of the BusinessAttachment. ex: "articles-of-incorporation.pdf"</item>
    ///     <item>Content [string]: Base64 data url of the file. ex: data:application/pdf;base64,JVBERi0xLjQ...</item>
    ///     <item>BusinessIdentityID [string]: Unique id of BusinessIdentity. ex: "5656565656565656"</item>
    ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for BusinessAttachments. ex: List<string>{ "employees", "monthly" }</item>
    ///     <item>Id [string]: unique id returned when the BusinessAttachment is created. ex: "5656565656565656"</item>
    ///     <item>AttachmentID [string]: unique id of the attached file. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current status of the BusinessAttachment. Options: "created", "canceled", "approved", "denied"</item>
    ///     <item>Created [DateTime]: creation DateTime for the BusinessAttachment. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the BusinessAttachment. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class BusinessAttachment : Resource
    {
        public string Name { get; }
        public string Content { get; }
        public string BusinessIdentityID { get; }
        public List<string> Tags { get; }
        public string AttachmentID { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// BusinessAttachment object
        /// <br/>
        /// Business attachments are files containing documents of a business
        /// to be used in a matching validation. When created, they must be attached to a business
        /// identity to be used for its validation.
        /// <br/>
        /// When you initialize a BusinessAttachment, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: name of the BusinessAttachment. ex: "articles-of-incorporation.pdf"</item>
        ///     <item>content [string]: Base64 data url of the file. ex: data:application/pdf;base64,JVBERi0xLjQ...</item>
        ///     <item>businessIdentityID [string]: Unique id of BusinessIdentity. ex: "5656565656565656"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default []]: list of strings for reference when searching for BusinessAttachment. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the BusinessAttachment is created. ex: "5656565656565656"</item>
        ///     <item>attachmentID [string]: unique id of the attached file. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the BusinessAttachment. Options: "created", "canceled", "approved", "denied"</item>
        ///     <item>created [DateTime]: creation DateTime for the BusinessAttachment. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the BusinessAttachment. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public BusinessAttachment(
            string name, string content, string businessIdentityID, List<string> tags = null,
            string id = null, string attachmentID = null, string status = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Name = name;
            Content = content;
            BusinessIdentityID = businessIdentityID;
            Tags = tags;
            AttachmentID = attachmentID;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// BusinessAttachment object
        /// <br/>
        /// Business attachments are files containing documents of a business
        /// to be used in a matching validation. When created, they must be attached to a business
        /// identity to be used for its validation.
        /// <br/>
        /// When you initialize a BusinessAttachment, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: name of the BusinessAttachment. ex: "articles-of-incorporation.pdf"</item>
        ///     <item>content [byte[]]: raw bytes of the file. ex: System.IO.File.ReadAllBytes("articles-of-incorporation.pdf")</item>
        ///     <item>contentType [string]: content MIME type. This parameter is required as input only. ex: "image/png" or "application/pdf"</item>
        ///     <item>businessIdentityID [string]: Unique id of BusinessIdentity. ex: "5656565656565656"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default []]: list of strings for reference when searching for BusinessAttachment. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the BusinessAttachment is created. ex: "5656565656565656"</item>
        ///     <item>attachmentID [string]: unique id of the attached file. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the BusinessAttachment. Options: "created", "canceled", "approved", "denied"</item>
        ///     <item>created [DateTime]: creation DateTime for the BusinessAttachment. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the BusinessAttachment. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public BusinessAttachment(
            string name, byte[] content, string contentType, string businessIdentityID, List<string> tags = null,
            string id = null, string attachmentID = null, string status = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Name = name;
            Content = "data:" + contentType + ";base64," + Convert.ToBase64String(content);
            BusinessIdentityID = businessIdentityID;
            Tags = tags;
            AttachmentID = attachmentID;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create BusinessAttachment objects
        /// <br/>
        /// Send a list of BusinessAttachment objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>attachments [list of BusinessAttachment objects]: list of BusinessAttachment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BusinessAttachment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BusinessAttachment> Create(List<BusinessAttachment> attachments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: attachments,
                user: user
            ).ToList().ConvertAll(o => (BusinessAttachment)o);
        }

        /// <summary>
        /// Create BusinessAttachment objects
        /// <br/>
        /// Send a list of BusinessAttachment dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>attachments [list of Dictionaries]: list of dictionaries representing the BusinessAttachment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BusinessAttachment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BusinessAttachment> Create(List<Dictionary<string, object>> attachments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: attachments,
                user: user
            ).ToList().ConvertAll(o => (BusinessAttachment)o);
        }

        /// <summary>
        /// Retrieve a specific BusinessAttachment by its id
        /// <br/>
        /// Receive a single BusinessAttachment object previously created in the Stark Infra API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "content" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>BusinessAttachment object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static BusinessAttachment Get(string id, List<string> expand = null, User user = null)
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
            ) as BusinessAttachment;
        }

        /// <summary>
        /// Retrieve BusinessAttachment objects
        /// <br/>
        /// Receive an IEnumerable of BusinessAttachment objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "created", "canceled", "approved", "denied"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of BusinessAttachment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<BusinessAttachment> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> tags = null, List<string> ids = null, User user = null
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
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<BusinessAttachment>();
        }

        /// <summary>
        /// Retrieve paged BusinessAttachment objects
        /// <br/>
        /// Receive a list of up to 100 BusinessAttachment objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "created", "canceled", "approved", "denied"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BusinessAttachment objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of BusinessAttachment objects</item>
        /// </list>
        /// </summary>
        public static (List<BusinessAttachment> page, string pageCursor) Page(string cursor = null, int? limit = null,
            DateTime? after = null, DateTime? before = null, List<string> status = null, List<string> tags = null,
            List<string> ids = null, User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
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
            List<BusinessAttachment> attachments = new List<BusinessAttachment>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                attachments.Add(subResource as BusinessAttachment);
            }
            return (attachments, pageCursor);
        }

        /// <summary>
        /// Cancel a BusinessAttachment entity
        /// <br/>
        /// Cancel a BusinessAttachment entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: BusinessAttachment unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled BusinessAttachment object</item>
        /// </list>
        /// </summary>
        public static BusinessAttachment Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as BusinessAttachment;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "BusinessAttachment", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string content = json.content;
            string businessIdentityID = json.businessIdentityId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string attachmentID = json.attachmentId;
            string status = json.status;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new BusinessAttachment(
                name: name, content: content, businessIdentityID: businessIdentityID,
                tags: tags, id: id, attachmentID: attachmentID, status: status,
                created: created, updated: updated
            );
        }
    }
}
