using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixFraud object
    /// <br/>
    /// Pix Frauds can be created by either participant or automatically when a Pix Infraction is accepted.
    /// <br/>
    /// When you initialize a PixFraud, the entity will not be automatically
    /// created in the Stark Infra API.The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>bacenId [string]: unique ID from BACEN. ex: "ccf9bd9c-e99d-999e-bab9-b999ca999f99"</item>
    ///     <item>keyId [string]: the key ID associated with the PixFraud. ex: ""</item>
    ///     <item>externalId [string]: external ID for the PixFraud. ex: "my_external_id"</item>
    ///     <item>status [string]: current status of the PixFraud. ex: "created"</item>
    ///     <item>tags [List&lt;string&gt;]: list of tags associated with the PixFraud. ex: []</item>
    ///     <item>taxId [string]: tax ID associated with the PixFraud. ex: "012.345.678-90"</item>
    ///     <item>type [string]: type of fraud. ex: "mule"</item>
    ///     <item>created [DateTime]: creation DateTime for the PixFraud. ex: "2022-05-26T19:16:12.202908+00:00"</item>
    ///     <item>updated [DateTime]: latest update DateTime for the PixFraud. ex: "2022-05-27T14:28:49.607934+00:00"</item>
    ///     <item>id [string]: unique ID returned when the PixFraud is created. ex: "5521738276274176"</item>
    /// </list>
    /// </summary>
    public partial class PixFraud : Resource
    {
        public string BacenId { get; set; }
        public string KeyId { get; set; }
        public string ExternalId { get; set; }
        public string Status { get; set; }
        public List<string> Tags { get; set; }
        public string TaxId { get; set; }
        public string Type { get; set; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixFraud object
        /// <br/>
        /// Pix Frauds can be created by either participant or automatically when a Pix Infraction is accepted.
        /// <br/>
        /// When you initialize a PixFraud, the entity will not be automatically
        /// created in the Stark Infra API.The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>taxId [string]: User CPF (11 digits formatted or unformatted) or CNPJ (14 digits formatted or unformatted). Example: taxId="012.345.678-90"</item>
        ///     <item>type [string]: type of fraud report. Options: "identity", "mule", "scam" and "other"</item>
        ///     <item>externalId [string]: Unique external id to avoid duplicated fraud requests. Example: "my_external_id"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>keyId [string, default null]: Pix key associated with the fraud. It can be a taxId (CPF/CNPJ), a phone number, an email or an alphanumeric sequence (EVP). Example: keyId="+5511989898989"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>bacenId [string]: unique ID from BACEN. ex: "ccf9bd9c-e99d-999e-bab9-b999ca999f99"</item>
        ///     <item>keyId [string]: the key ID associated with the PixFraud. ex: ""</item>
        ///     <item>externalId [string]: external ID for the PixFraud. ex: "my_external_id"</item>
        ///     <item>status [string]: current status of the PixFraud. ex: "created"</item>
        ///     <item>tags [List&lt;string&gt;]: list of tags associated with the PixFraud. ex: []</item>
        ///     <item>taxId [string]: tax ID associated with the PixFraud. ex: "012.345.678-90"</item>
        ///     <item>type [string]: type of fraud. ex: "mule"</item>
        ///     <item>created [DateTime]: creation DateTime for the PixFraud. ex: "2022-05-26T19:16:12.202908+00:00"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the PixFraud. ex: "2022-05-27T14:28:49.607934+00:00"</item>
        ///     <item>id [string]: unique ID returned when the PixFraud is created. ex: "5521738276274176"</item>
        /// </list>
        /// </summary>
        public PixFraud(string taxId, string type, string externalId, string keyId = null, List<string> tags = null,
                        string bacenId = null, string status = null, DateTime? created = null,
                        DateTime? updated = null, string id = null) : base(id)
        {
            BacenId = bacenId;
            KeyId = keyId;
            ExternalId = externalId;
            Status = status;
            Tags = tags ?? new List<string>();
            TaxId = taxId;
            Type = type;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create PixFraud objects
        /// <br/>
        /// Create PixFraud objects in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>frauds [list of PixFraud object]: list of PixFraud objects to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>List of PixFraud object with updated attributes.</item>
        /// </list>
        /// </summary>
        public static List<PixFraud> Create(List<PixFraud> frauds, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: frauds,
                user: user
            ).ToList().ConvertAll(o => (PixFraud)o);
        }

        /// <summary>
        /// Create PixFraud objects
        /// <br/>
        /// Send PixFraud dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>frauds [list of dictionaries]: list of PixFraud dictionaries to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>List of PixFraud object with updated attributes.</item>
        /// </list>
        /// </summary>
        public static List<PixFraud> Create(List<Dictionary<string, object>> frauds, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: frauds,
                user: user
            ).ToList().ConvertAll(o => (PixFraud)o);
        }

        /// <summary>
        /// Retrieve a PixFraud object
        /// <br/>
        /// Retrieve a PixFraud object linked to your Workspace in the Stark Infra API using its id.
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
        ///     <item>PixFraud object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static PixFraud Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixFraud;
        }

        /// <summary>
        /// Retrieve PixFraud objects
        /// <br/>
        /// Receive a IEnumerable of PixFraud objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created after a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created before a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: List of strings to filter Pix Frauds by the specific status. Options: '"created", "failed", "registered" and "canceled"</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>type [list of strings, default null]: Filter Pix Frauds by their type. Options: "identity", "mule", "scam" and "other"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixFraud objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixFraud> Query(
            int? limit = null, DateTime? after = null, DateTime? before = null,
            string status = null, List<string> ids = null, string type = null,
            List<string> tags = null, User user = null
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
                    { "ids", ids },
                    { "type", type },
                    { "tags", tags }
                },
                user: user
            ).Cast<PixFraud>();
        }

        /// <summary>
        /// Retrieve paged PixFraud objects
        /// <br/>
        /// Receive a list of up to 100 PixFraud objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created after a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created before a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. Options: "created", "failed", "delivered", "closed", "canceled".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>type [list of strings, default null]: filter for the type of retrieved PixFrauds. Options: "fraud", "reversal", "reversalChargeback"</item>
        ///     <item>flow [string, default null]: direction of the PixFraud flow. Options: "out" if you created the PixFraud, "in" if you received the PixFraud.</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixFraud objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixFraud objects</item>
        /// </list>
        /// </summary>
        public static (List<PixFraud> page, string pageCursor) Page(
            string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, string status = null, List<string> ids = null,
            string type = null, List<string> tags = null,
            User user = null
        )
        {
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
                    { "ids", ids },
                    { "type", type },
                    { "tags", tags }
                },
                user: user
            );
            List<PixFraud> frauds = new List<PixFraud>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                frauds.Add(subResource as PixFraud);
            }
            return (frauds, pageCursor);
        }

        /// <summary>
        /// Cancel a PixFraud entity
        /// <br/>
        /// Cancel a PixFraud entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: PixFraud unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Organization/Project object, default null]: Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled PixFraud object</item>
        /// </list>
        /// </summary>
        public static PixFraud Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixFraud;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixFraud", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string bacenId = json.bacenId;
            string type = json.type;
            string keyId = json.keyId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string externalId = json.externalId;
            string status = json.status;
            string taxId = json.taxId;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixFraud(
                bacenId: bacenId, type: type, keyId: keyId,
                tags: tags, id: id, externalId: externalId,
                taxId: taxId, status: status, updated: updated,
                created: created
            );
        }
    }
}
