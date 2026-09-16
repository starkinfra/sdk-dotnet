using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// BusinessAccountRequest object
    /// <br/>
    /// You can create a business account request to request an account for a specific company, opening the
    /// account with identity verification by webview for each of its owners.
    /// <br/>
    /// When you initialize a BusinessAccountRequest, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Address [BusinessAccountRequest.Address object]: company's structured address. ex: new Address(street: "Av. Faria Lima", number: "2000", neighborhood: "Itaim Bibi", city: "Sao Paulo", state: "SP", zipCode: "04538-132")</item>
    ///     <item>Revenue [long]: company's annual revenue in cents. ex: 100000000 (= R$ 1,000,000.00)</item>
    ///     <item>Name [string]: company's legal name (minimum 5 characters). ex: "Stark Bank S.A."</item>
    ///     <item>TaxID [string]: company's tax ID (CNPJ). ex: "20.018.183/0001-80"</item>
    ///     <item>Owners [list of BusinessAccountRequest.Owner objects]: list of 1 to 10 company owners. ex: new List&lt;Owner&gt;{ new Owner(taxID: "012.345.678-90", name: "Jamie Lannister", role: "partner") }</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for BusinessAccountRequests. ex: new List&lt;string&gt;{ "employees", "monthly" }</item>
    ///     <item>ID [string]: unique id returned when the BusinessAccountRequest is created. ex: "5656565656565656"</item>
    ///     <item>AccountType [string]: type of the account. ex: "business"</item>
    ///     <item>Flags [list of dictionaries]: flags that motivated the decision, populated when the request is denied. Each flag has a code and a message. ex: new List&lt;Dictionary&lt;string, object&gt;&gt;{ new Dictionary&lt;string, object&gt;{ {"code", "failedIdentityProof"}, {"message", "O representante: 012.345.678-90 falhou na verificação de identidade."} } }</item>
    ///     <item>Status [string]: current status of the BusinessAccountRequest. Options: "created", "processing", "approved", "denied", "failed"</item>
    ///     <item>Created [DateTime]: creation DateTime for the BusinessAccountRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the BusinessAccountRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class BusinessAccountRequest : Resource
    {
        public Address Address { get; }
        public long Revenue { get; }
        public string Name { get; }
        public string TaxID { get; }
        public List<Owner> Owners { get; }
        public List<string> Tags { get; }
        public string AccountType { get; }
        public List<Dictionary<string, object>> Flags { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// BusinessAccountRequest object
        /// <br/>
        /// You can create a business account request to request an account for a specific company, opening the
        /// account with identity verification by webview for each of its owners.
        /// <br/>
        /// When you initialize a BusinessAccountRequest, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>address [BusinessAccountRequest.Address object]: company's structured address. ex: new Address(street: "Av. Faria Lima", number: "2000", neighborhood: "Itaim Bibi", city: "Sao Paulo", state: "SP", zipCode: "04538-132")</item>
        ///     <item>revenue [long]: company's annual revenue in cents. ex: 100000000 (= R$ 1,000,000.00)</item>
        ///     <item>name [string]: company's legal name (minimum 5 characters). ex: "Stark Bank S.A."</item>
        ///     <item>taxID [string]: company's tax ID (CNPJ). ex: "20.018.183/0001-80"</item>
        ///     <item>owners [list of BusinessAccountRequest.Owner objects]: list of 1 to 10 company owners. ex: new List&lt;Owner&gt;{ new Owner(taxID: "012.345.678-90", name: "Jamie Lannister", role: "partner") }</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for BusinessAccountRequests. ex: new List&lt;string&gt;{ "employees", "monthly" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the BusinessAccountRequest is created. ex: "5656565656565656"</item>
        ///     <item>accountType [string]: type of the account. ex: "business"</item>
        ///     <item>flags [list of dictionaries]: flags that motivated the decision, populated when the request is denied. Each flag has a code and a message.</item>
        ///     <item>status [string]: current status of the BusinessAccountRequest. Options: "created", "processing", "approved", "denied", "failed"</item>
        ///     <item>created [DateTime]: creation DateTime for the BusinessAccountRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the BusinessAccountRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public BusinessAccountRequest(
            Address address, long revenue, string name, string taxID, List<Owner> owners,
            List<string> tags = null, string id = null, string accountType = null,
            List<Dictionary<string, object>> flags = null, string status = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Address = address;
            Revenue = revenue;
            Name = name;
            TaxID = taxID;
            Owners = owners;
            Tags = tags;
            AccountType = accountType;
            Flags = flags;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create BusinessAccountRequests
        /// <br/>
        /// Send a list of BusinessAccountRequest objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of BusinessAccountRequest objects]: list of BusinessAccountRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BusinessAccountRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BusinessAccountRequest> Create(List<BusinessAccountRequest> requests, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (BusinessAccountRequest)o);
        }

        /// <summary>
        /// Create BusinessAccountRequests
        /// <br/>
        /// Send a list of BusinessAccountRequest dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of Dictionaries]: list of dictionaries representing the BusinessAccountRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BusinessAccountRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BusinessAccountRequest> Create(List<Dictionary<string, object>> requests, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (BusinessAccountRequest)o);
        }

        /// <summary>
        /// Retrieve a specific BusinessAccountRequest
        /// <br/>
        /// Receive a single BusinessAccountRequest object previously created in the Stark Infra API by its id
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
        ///     <item>BusinessAccountRequest object with updated attributes</item>
        /// </list>
        /// </summary>
        public static BusinessAccountRequest Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as BusinessAccountRequest;
        }

        /// <summary>
        /// Retrieve BusinessAccountRequests
        /// <br/>
        /// Receive an IEnumerable of BusinessAccountRequest objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List&lt;string&gt;{ "created", "processing" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List&lt;string&gt;{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of BusinessAccountRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<BusinessAccountRequest> Query(
            int? limit = null, DateTime? after = null, DateTime? before = null, List<string> status = null,
            List<string> tags = null, List<string> ids = null, User user = null
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
            ).Cast<BusinessAccountRequest>();
        }

        /// <summary>
        /// Retrieve paged BusinessAccountRequests
        /// <br/>
        /// Receive a list of up to 100 BusinessAccountRequest objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List&lt;string&gt;{ "created", "processing" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List&lt;string&gt;{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List&lt;string&gt;{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BusinessAccountRequest objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of BusinessAccountRequest objects</item>
        /// </list>
        /// </summary>
        public static (List<BusinessAccountRequest> page, string pageCursor) Page(
            string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> tags = null, List<string> ids = null, User user = null
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
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            );
            List<BusinessAccountRequest> requests = new List<BusinessAccountRequest>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                requests.Add(subResource as BusinessAccountRequest);
            }
            return (requests, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "BusinessAccountRequest", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            Address address = ParseAddress(json.address);
            long revenue = json.revenue;
            string name = json.name;
            string taxID = json.taxId;
            List<Owner> owners = ParseOwners(json.owners);
            List<string> tags = json.tags?.ToObject<List<string>>();
            string accountType = json.accountType;
            List<Dictionary<string, object>> flags = json.flags?.ToObject<List<Dictionary<string, object>>>();
            string status = json.status;
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);

            return new BusinessAccountRequest(
                id: id, address: address, revenue: revenue, name: name, taxID: taxID, owners: owners,
                tags: tags, accountType: accountType, flags: flags, status: status, created: created,
                updated: updated
            );
        }

        private static Address ParseAddress(dynamic json)
        {
            if (json == null)
            {
                return null;
            }
            return (Address)Address.ResourceMaker(json);
        }

        private static List<Owner> ParseOwners(dynamic json)
        {
            List<Owner> owners = new List<Owner>();
            if (json == null)
            {
                return owners;
            }
            foreach (dynamic owner in json)
            {
                owners.Add((Owner)Owner.ResourceMaker(owner));
            }
            return owners;
        }
    }
}
