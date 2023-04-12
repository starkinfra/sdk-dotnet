using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IndividualIdentity object
    /// <br/>
    /// An IndividualDocument represents an individual to be validated. It can have several individual documents attached
    /// to it, which are used to validate the identity of the individual. Once an individual identity is created, individual
    /// documents must be attached to it using the created method of the individual document resource. When all the required
    /// individual documents are attached to an individual identity it can be sent to validation by patching its status to 
    /// processing.
    /// <br/>
    /// When you initialize a IndividualIdentity, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: individual's full name. ex: "Edward Stark". ex: "Edward Stark"</item>
    ///     <item>TaxID [string]: individual's tax ID (CPF). ex: "594.739.480-42"</item>
    ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for IndividualIdentities. ex: List<string>{ "employees", "monthly" }</item>
    ///     <item>Id [string]: unique id returned when the IndividualIdentity is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current status of the IndividualIdentity. ex: "created", "canceled", "processing", "failed", "success"</item>
    ///     <item>Created [DateTime]: creation DateTime for the IndividualIdentity. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IndividualIdentity : Resource
    {
        public string Name { get; }
        public string TaxID { get; }
        public List<string> Tags { get; }
        public string Id { get; }
        public string Status { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IndividualIdentity object
        /// <br/>
        /// An IndividualDocument represents an individual to be validated. It can have several individual documents attached
        /// to it, which are used to validate the identity of the individual. Once an individual identity is created, individual
        /// documents must be attached to it using the created method of the individual document resource. When all the required
        /// individual documents are attached to an individual identity it can be sent to validation by patching its status to 
        /// processing.
        /// <br/>
        /// When you initialize a IndividualIdentity, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: individual's full name. ex: "Edward Stark". ex: "Edward Stark"</item>
        ///     <item>taxID [string]: individual's tax ID (CPF). ex: "594.739.480-42"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default []]: list of strings for reference when searching for IndividualIdentities. ex: List<string>{ "employees", "monthly" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IndividualIdentity is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current status of the IndividualIdentity. ex: "created", "canceled", "processing", "failed", "success"</item>
        ///     <item>created [DateTime]: creation DateTime for the IndividualIdentity. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IndividualIdentity(
            string name = null, string taxID = null, List<string> tags = null, string id = null, 
            string status = null, DateTime? created = null
        ) : base(id)
        {
            Name = name;
            TaxID = taxID;
            Tags = tags;
            Id = id;
            Status = status;
            Created = created;
        }

        /// <summary>
        /// Create IndividualIdentity objects
        /// <br/>
        /// Send a list of IndividualIdentity objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>identities [list of IndividualIdentity objects]: list of IndividualIdentity objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualIdentity objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IndividualIdentity> Create(List<IndividualIdentity> identities, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: identities,
                user: user
            ).ToList().ConvertAll(o => (IndividualIdentity)o);
        }

        /// <summary>
        /// Create IndividualIdentity objects
        /// <br/>
        /// Send a list of IndividualIdentity dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>identities [list of Dictionaries]: list of dictionaries representing the IndividualIdentity objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualIdentity objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IndividualIdentity> Create(List<Dictionary<string, object>> identities, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: identities,
                user: user
            ).ToList().ConvertAll(o => (IndividualIdentity)o);
        }

        /// <summary>
        /// Retrieve a specific IndividualIdentity by its id
        /// <br/>
        /// Receive a single IndividualIdentity object previously created in the Stark Infra API by passing its id
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
        ///     <item>IndividualIdentity object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IndividualIdentity Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IndividualIdentity;
        }

        /// <summary>
        /// Retrieve IndividualIdentity objects
        /// <br/>
        /// Receive an IEnumerable of IndividualIdentity objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "created", "canceled", "processing", "failed", "success"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IndividualIdentity objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IndividualIdentity> Query(
            int? limit = null, DateTime? after = null, DateTime? before = null, List<string> status = null, 
            List<string> tags = null,List<string> ids = null, User user = null
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
            ).Cast<IndividualIdentity>();
        }

        /// <summary>
        /// Retrieve paged IndividualIdentity objects
        /// <br/>
        /// Receive a list of up to 100 IndividualIdentity objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "created", "canceled", "processing", "failed", "success"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IndividualIdentity objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IndividualIdentity objects</item>
        /// </list>
        /// </summary>
        public static (List<IndividualIdentity> page, string pageCursor) Page(
            string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null, 
            List<string> status = null, List<string> tags = null,List<string> ids = null, User user = null
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
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            );
            List<IndividualIdentity> identities = new List<IndividualIdentity>();
            foreach (SubResource subResource in page)
            {
                identities.Add(subResource as IndividualIdentity);
            }
            return (identities, pageCursor);
        }

        /// <summary>
        /// Update IndividualIdentity entity
        /// <br/>
        /// Update an IndividualIdentity by passing its id.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: IndividualIdentity id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, default null]: You may block the IndividualIdentity by passing "blocked" or activate by passing "active" in the status</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target IndividualIdentity with updated attributes</item>
        /// </list>
        /// </summary>
        public static IndividualIdentity Update(string id, string status, User user = null)
        {
            Dictionary<string, object> patchData = new Dictionary<string, object>();
            if (status != null) patchData.Add("status", status);
            
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as IndividualIdentity;
        }

        /// <summary>
        /// Cancel an IndividualIdentity entity
        /// <br/>
        /// Cancel an IndividualIdentity entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: IndividualIdentity unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled IndividualIdentity object</item>
        /// </list>
        /// </summary>
        public static IndividualIdentity Cancel(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IndividualIdentity;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IndividualIdentity", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string taxID = json.taxID;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);

            return new IndividualIdentity(
                name: name, taxID: taxID, tags: tags, id: id, status: status, 
                created: created
            );
        }
    }
}
