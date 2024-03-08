using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditHolmes object
    /// <br/>
    /// CreditHolmes are used to obtain debt information on your customers.
    /// Before you create a CreditHolmes, make sure you have your customer's express
    /// authorization to verify their information in the Central Bank's SCR.
    /// <br/>
    /// When you initialize a CreditHolmes, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>TaxID [string]: customer's tax ID (CPF or CNPJ) for whom the credit operations will be verified. ex: "20.018.183/0001-80"</item>
    ///     <item>Competence [string, default 'two months before current date']: competence month of the operation verification, format: "YYYY-MM". ex: "2021-04"</item>
    ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for CreditHolmes. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>Id [string]: unique id returned when the CreditHolmes is created. ex: "5656565656565656"</item>
    ///     <item>Result [dictionary, default empty dictionary]: result of the investigation after the case is solved.</item>
    ///     <item>Status [string]: current status of the CreditHolmes. ex: "created", "failed", "success"</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the CreditHolmes. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the CreditHolmes. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class CreditHolmes : Resource
    {
        public string TaxID { get; }
        public string Competence { get; }
        public List<string> Tags { get; }
        public string Id { get; }
        public Dictionary<string, object> Result { get; }
        public string Status { get; }
        public DateTime? Updated { get;  }
        public DateTime? Created { get; }

        /// <summary>
        /// CreditHolmes object
        /// <br/>
        /// CreditHolmes are used to obtain debt information on your customers.
        /// Before you create a CreditHolmes, make sure you have your customer's express
        /// authorization to verify their information in the Central Bank's SCR.
        /// When you initialize a CreditHolmes, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// When you initialize a CreditHolmes, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>taxID [string]: customer's tax ID (CPF or CNPJ) for whom the credit operations will be verified. ex: "20.018.183/0001-80"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>competence [string, default 'two months before current date']: competence month of the operation verification, format: "YYYY-MM". ex: "2021-04"
        ///     <item>tags [list of strings, default []]: list of strings for reference when searching for CreditHolmes. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when CreditHolmes is created. ex: "5656565656565656"</item>
        ///     <item>result [dictionary, default empty dictionary]: result of the investigation after the case is solved.</item>
        ///     <item>status [string]: current status of the CreditHolmes. ex: "created", "failed", "success"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the CreditHolmes. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the CreditHolmes. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public CreditHolmes(
            string taxID, string competence = null, List<string> tags = null, string id = null, Dictionary<string, object> result = null, 
            string status = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            TaxID = taxID;
            Competence = competence;
            Tags = tags;
            Id = id;
            Result = result;
            Status = status;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create CreditHolmes objects
        /// <br/>
        /// Send a list of CreditHolmes objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holmes [list of CreditHolmes objects]: list of CreditHolmes objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CreditHolmes objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<CreditHolmes> Create(List<CreditHolmes> holmes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holmes,
                user: user
            ).ToList().ConvertAll(o => (CreditHolmes)o);
        }

        /// <summary>
        /// Create CreditHolmes objects
        /// <br/>
        /// Send a list of CreditHolmes dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>holmes [list of Dictionaries]: list of dictionaries representing the CreditHolmes objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CreditHolmes objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<CreditHolmes> Create(List<Dictionary<string, object>> holmes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: holmes,
                user: user
            ).ToList().ConvertAll(o => (CreditHolmes)o);
        }

        /// <summary>
        /// Retrieve a specific CreditHolmes by its id
        /// <br/>
        /// Receive a single CreditHolmes object previously created in the Stark Infra API by passing its id
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
        ///     <item>CreditHolmes object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static CreditHolmes Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as CreditHolmes;
        }

        /// <summary>
        /// Retrieve CreditHolmes objects
        /// <br/>
        /// Receive an IEnumerable of CreditHolmes objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "expired" or "canceled"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CreditHolmes objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CreditHolmes> Query(int? limit = null, DateTime? after = null, DateTime? before = null, 
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
            ).Cast<CreditHolmes>();
        }

        /// <summary>
        /// Retrieve paged CreditHolmes objects
        /// <br/>
        /// Receive a list of up to 100 CreditHolmes objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "active", "blocked", "expired" or "canceled"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CreditHolmes objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of CreditHolmes objects</item>
        /// </list>
        /// </summary>
        public static (List<CreditHolmes> page, string pageCursor) Page(string cursor = null, int? limit = null, 
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
            List<CreditHolmes> holmes = new List<CreditHolmes>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                holmes.Add(subResource as CreditHolmes);
            }
            return (holmes, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CreditHolmes", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string taxID = json.taxId;
            string competence = json.competence;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            Dictionary<string, object> result = json.result?.ToObject<Dictionary<string, object>>();
            string status = json.status;
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);

            return new CreditHolmes(
                taxID: taxID, competence: competence, tags: tags, id: id, result: result, 
                status: status, updated: updated, created: created
            );
        }
    }
}
