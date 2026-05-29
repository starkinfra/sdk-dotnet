using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IndividualAccountRequest object
    /// <br/>
    /// Request to open a Stark Infra account for an individual. The caller submits the individual's
    /// identifying data and income, and the API runs the approval flow asynchronously.
    /// <br/>
    /// When you initialize an IndividualAccountRequest, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: full legal name of the individual. ex: "Tony Stark"</item>
    ///     <item>TaxID [string]: Brazilian CPF. ex: "012.345.678-90" or "01234567890"</item>
    ///     <item>Address [Address]: structured residential address.</item>
    ///     <item>Income [integer]: monthly income in cents. ex: 1000000 (= R$ 10,000.00)</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for IndividualAccountRequests. ex: new List<string>{ "employees", "monthly" }</item>
    ///     <item>ID [string]: unique id returned when the IndividualAccountRequest is created. ex: "5189530608992256"</item>
    ///     <item>Status [string]: current IndividualAccountRequest status. Options: "approved", "created", "denied", "processing", "updated"</item>
    ///     <item>AccountType [string]: account-request kind. Always "individual" for this resource.</item>
    ///     <item>Flags [list of strings]: server-side review flags.</item>
    ///     <item>Created [DateTime]: creation DateTime for the IndividualAccountRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the IndividualAccountRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IndividualAccountRequest : Resource
    {
        public string Name { get; }
        public string TaxID { get; }
        public Address Address { get; }
        public long Income { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public string AccountType { get; }
        public List<string> Flags { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// IndividualAccountRequest object
        /// <br/>
        /// Request to open a Stark Infra account for an individual.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: full legal name of the individual. ex: "Tony Stark"</item>
        ///     <item>taxID [string]: Brazilian CPF. ex: "012.345.678-90" or "01234567890"</item>
        ///     <item>address [Address]: structured residential address.</item>
        ///     <item>income [integer]: monthly income in cents. Must be >= 0. ex: 1000000 (= R$ 10,000.00)</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for IndividualAccountRequests. ex: new List<string>{ "employees", "monthly" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the IndividualAccountRequest is created. ex: "5189530608992256"</item>
        ///     <item>status [string]: current IndividualAccountRequest status. Options: "approved", "created", "denied", "processing", "updated"</item>
        ///     <item>accountType [string]: account-request kind. Always "individual" for this resource.</item>
        ///     <item>flags [list of strings]: server-side review flags.</item>
        ///     <item>created [DateTime]: creation DateTime for the IndividualAccountRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the IndividualAccountRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IndividualAccountRequest(
            string name, string taxID, Address address, long income, List<string> tags = null,
            string id = null, string status = null, string accountType = null, List<string> flags = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Name = name;
            TaxID = taxID;
            Address = address;
            Income = income;
            Tags = tags;
            Status = status;
            AccountType = accountType;
            Flags = flags;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create IndividualAccountRequest objects
        /// <br/>
        /// Send a list of IndividualAccountRequest objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of IndividualAccountRequest objects]: list of IndividualAccountRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualAccountRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IndividualAccountRequest> Create(List<IndividualAccountRequest> requests, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (IndividualAccountRequest)o);
        }

        /// <summary>
        /// Create IndividualAccountRequest objects
        /// <br/>
        /// Send a list of IndividualAccountRequest dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of Dictionaries]: list of dictionaries representing the IndividualAccountRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualAccountRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IndividualAccountRequest> Create(List<Dictionary<string, object>> requests, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (IndividualAccountRequest)o);
        }

        /// <summary>
        /// Retrieve a specific IndividualAccountRequest by its id
        /// <br/>
        /// Receive a single IndividualAccountRequest object previously created in the Stark Infra API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5189530608992256"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IndividualAccountRequest object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IndividualAccountRequest Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IndividualAccountRequest;
        }

        /// <summary>
        /// Retrieve IndividualAccountRequest objects
        /// <br/>
        /// Receive an IEnumerable of IndividualAccountRequest objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "employees", "monthly" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5189530608992256", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IndividualAccountRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IndividualAccountRequest> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
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
            ).Cast<IndividualAccountRequest>();
        }

        /// <summary>
        /// Retrieve paged IndividualAccountRequest objects
        /// <br/>
        /// Receive a list of up to 100 IndividualAccountRequest objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "employees", "monthly" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5189530608992256", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualAccountRequest objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IndividualAccountRequest objects</item>
        /// </list>
        /// </summary>
        public static (List<IndividualAccountRequest> page, string pageCursor) Page(string cursor = null, int? limit = null,
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
            List<IndividualAccountRequest> requests = new List<IndividualAccountRequest>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                requests.Add(subResource as IndividualAccountRequest);
            }
            return (requests, pageCursor);
        }

        /// <summary>
        /// Update an IndividualAccountRequest entity
        /// <br/>
        /// Update an IndividualAccountRequest by passing its id and the fields to be updated.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: IndividualAccountRequest unique id. ex: "5189530608992256"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>name [string, default null]: full legal name of the individual. ex: "Tony Stark"</item>
        ///     <item>taxID [string, default null]: Brazilian CPF. ex: "012.345.678-90"</item>
        ///     <item>address [Address, default null]: structured residential address. Replaces the address as a whole object.</item>
        ///     <item>income [integer, default null]: monthly income in cents. ex: 1000000</item>
        ///     <item>status [string, default null]: manual state transition. ex: "processing"</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference. ex: new List<string>{ "employees", "monthly" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>updated IndividualAccountRequest object</item>
        /// </list>
        /// </summary>
        public static IndividualAccountRequest Update(string id, string name = null, string taxID = null, Address address = null,
            long? income = null, string status = null, List<string> tags = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "name", name },
                    { "taxId", taxID },
                    { "address", address },
                    { "income", income },
                    { "status", status },
                    { "tags", tags }
                },
                user: user
            ) as IndividualAccountRequest;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IndividualAccountRequest", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string taxID = json.taxId;
            Address address = Address.Parse(json.address);
            long income = json.income;
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string accountType = json.accountType;
            List<string> flags = json.flags is null ? new List<string> { } : json.flags.ToObject<List<string>>();
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new IndividualAccountRequest(
                name: name, taxID: taxID, address: address, income: income, tags: tags,
                id: id, status: status, accountType: accountType, flags: flags,
                created: created, updated: updated
            );
        }
    }
}
