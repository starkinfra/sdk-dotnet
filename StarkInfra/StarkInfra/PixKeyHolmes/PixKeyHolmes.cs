using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixKeyHolmes object
    /// <br/>
    /// A PixKeyHolmes is used to investigate the registration status of a Pix Key
    /// in the Central Bank's DICT. You open one per key you want to check; the API
    /// resolves it asynchronously and reports back whether the key is registered.
    /// <br/>
    /// When you initialize a PixKeyHolmes, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>KeyID [string]: Pix Key to be investigated. ex: "+5511989898989", "11.222.333/0001-00", "valid@sandbox.com"</item>
    ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for PixKeyHolmes. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID [string]: unique id returned when the PixKeyHolmes is created. ex: "5656565656565656"</item>
    ///     <item>Result [string]: investigation outcome once the case is solved. Options: "registered", "unregistered"</item>
    ///     <item>Status [string]: current status of the PixKeyHolmes. ex: "created", "solving", "solved", "failed"</item>
    ///     <item>Created [DateTime]: creation DateTime for the PixKeyHolmes. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the PixKeyHolmes. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixKeyHolmes : Resource
    {
        public string KeyID { get; }
        public List<string> Tags { get; }
        public string Result { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixKeyHolmes object
        /// <br/>
        /// A PixKeyHolmes is used to investigate the registration status of a Pix Key
        /// in the Central Bank's DICT.
        /// When you initialize a PixKeyHolmes, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>keyID [string]: Pix Key to be investigated. ex: "+5511989898989", "11.222.333/0001-00", "valid@sandbox.com"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for PixKeyHolmes. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when PixKeyHolmes is created. ex: "5656565656565656"</item>
        ///     <item>result [string]: investigation outcome once the case is solved. Options: "registered", "unregistered"</item>
        ///     <item>status [string]: current status of the PixKeyHolmes. ex: "created", "solving", "solved", "failed"</item>
        ///     <item>created [DateTime]: creation DateTime for the PixKeyHolmes. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the PixKeyHolmes. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixKeyHolmes(
            string keyID, List<string> tags = null, string id = null, string result = null,
            string status = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            KeyID = keyID;
            Tags = tags;
            Result = result;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create PixKeyHolmes objects
        /// <br/>
        /// Send a list of PixKeyHolmes objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holmes [list of PixKeyHolmes objects]: list of PixKeyHolmes objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixKeyHolmes objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixKeyHolmes> Create(List<PixKeyHolmes> holmes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holmes,
                user: user
            ).ToList().ConvertAll(o => (PixKeyHolmes)o);
        }

        /// <summary>
        /// Retrieve PixKeyHolmes objects
        /// <br/>
        /// Receive an IEnumerable of PixKeyHolmes objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. The live API accepts only "solved" or "solving". ex: new List<string>{ "solved", "solving" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixKeyHolmes objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixKeyHolmes> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
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
            ).Cast<PixKeyHolmes>();
        }

        /// <summary>
        /// Retrieve paged PixKeyHolmes objects
        /// <br/>
        /// Receive a list of up to 100 PixKeyHolmes objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. The live API accepts only "solved" or "solving". ex: new List<string>{ "solved", "solving" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixKeyHolmes objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixKeyHolmes objects</item>
        /// </list>
        /// </summary>
        public static (List<PixKeyHolmes> page, string pageCursor) Page(string cursor = null, int? limit = null,
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
            List<PixKeyHolmes> holmes = new List<PixKeyHolmes>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                holmes.Add(subResource as PixKeyHolmes);
            }
            return (holmes, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixKeyHolmes", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string keyID = json.keyId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string result = json.result;
            string status = json.status;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixKeyHolmes(
                keyID: keyID, tags: tags, id: id, result: result,
                status: status, created: created, updated: updated
            );
        }
    }
}
