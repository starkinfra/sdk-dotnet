using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixFraud object
    /// <br/>
    /// PixFraud objects are used to report a PixKey or taxId when a fraud
    /// has been confirmed.
    /// <br/>
    /// When you initialize a PixFraud, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ExternalID [string]: endToEndID or returnID of the transaction being reported. ex: "my_external_id"</item>
    ///     <item>Type [string]: type of PixFraud. Options: "identity", "mule", "scam", "other"</item>
    ///     <item>TaxID [string]: user tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>KeyID [string, default null]: marked PixKey id. ex: "+5511989898989"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "fraudulent" }</item>
    ///     <item>ID [string]: unique id returned when the PixFraud is created. ex: "5656565656565656"</item>
    ///     <item>BacenID [string]: unique transaction id returned from Central Bank. ex: "ccf9bd9c-e99d-999e-bab9-b999ca999f99"</item>
    ///     <item>Status [string]: current PixFraud status. Options: "created", "failed", "registered", "canceled".</item>
    ///     <item>Created [DateTime]: creation DateTime for the PixFraud. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the PixFraud. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixFraud : Resource
    {
        public string ExternalID { get; }
        public string Type { get; }
        public string TaxID { get; }
        public string KeyID { get; }
        public List<string> Tags { get; }
        public string BacenID { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixFraud object
        /// <br/>
        /// PixFraud objects are used to report a PixKey or taxId when a fraud
        /// has been confirmed.
        /// <br/>
        /// When you initialize a PixFraud, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>externalID [string]: endToEndID or returnID of the transaction being reported. ex: "my_external_id"</item>
        ///     <item>type [string]: type of PixFraud. Options: "identity", "mule", "scam", "other"</item>
        ///     <item>taxID [string]: user tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>keyID [string, default null]: marked PixKey id. ex: "+5511989898989"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "fraudulent" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixFraud is created. ex: "5656565656565656"</item>
        ///     <item>bacenID [string]: unique transaction id returned from Central Bank. ex: "ccf9bd9c-e99d-999e-bab9-b999ca999f99"</item>
        ///     <item>status [string]: current PixFraud status. Options: "created", "failed", "registered", "canceled".</item>
        ///     <item>created [DateTime]: creation DateTime for the PixFraud. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the PixFraud. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixFraud(
            string externalID, string type, string taxID, string keyID = null, List<string> tags = null,
            string id = null, string bacenID = null, string status = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            ExternalID = externalID;
            Type = type;
            TaxID = taxID;
            KeyID = keyID;
            Tags = tags;
            BacenID = bacenID;
            Status = status;
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
        ///     <item>frauds [list of PixFraud objects]: list of PixFraud objects to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixFraud objects with updated attributes</item>
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
        /// Send a list of PixFraud dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>frauds [list of dictionaries]: list of dictionaries representing the PixFraud objects to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixFraud objects with updated attributes</item>
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
        /// Retrieve the PixFraud object linked to your Workspace in the Stark Infra API using its id.
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
        /// Receive an IEnumerable of PixFraud objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created after a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created before a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. Options: "created", "failed", "registered", "canceled".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "fraudulent" }</item>
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
            List<string> status = null, List<string> ids = null, List<string> tags = null,
            User user = null
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
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. Options: "created", "failed", "registered", "canceled".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "fraudulent" }</item>
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
            DateTime? before = null, List<string> status = null, List<string> ids = null,
            List<string> tags = null, User user = null
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
        /// Delete a PixFraud entity
        /// <br/>
        /// Delete a PixFraud entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: PixFraud unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted PixFraud object</item>
        /// </list>
        /// </summary>
        public static PixFraud Delete(string id, User user = null)
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
            string externalID = json.externalId;
            string type = json.type;
            string taxID = json.taxId;
            string keyID = json.keyId;
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            string id = json.id;
            string bacenID = json.bacenId;
            string status = json.status;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixFraud(
                externalID: externalID, type: type, taxID: taxID, keyID: keyID, tags: tags,
                id: id, bacenID: bacenID, status: status, created: created, updated: updated
            );
        }
    }
}
