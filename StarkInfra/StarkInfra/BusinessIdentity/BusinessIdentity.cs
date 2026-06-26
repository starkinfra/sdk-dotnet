using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// BusinessIdentity object
    /// <br/>
    /// A BusinessIdentity represents a business to be validated. It can have several business attachments attached
    /// to it, which are used to validate the identity of the business. Once a business identity is created, business
    /// attachments must be attached to it using the created method of the business attachment resource. When all the required
    /// business attachments are attached to a business identity it can be sent to validation by patching its status to
    /// processing.
    /// <br/>
    /// When you initialize a BusinessIdentity, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>TaxID [string]: business's tax ID (CNPJ). ex: "20.018.183/0001-80"</item>
    ///     <item>Tags [list of strings, default []]: list of strings for reference when searching for BusinessIdentities. ex: List<string>{ "employees", "monthly" }</item>
    ///     <item>Id [string]: unique id returned when the BusinessIdentity is created. ex: "5656565656565656"</item>
    ///     <item>Name [string]: business's full name. ex: "Stark Bank S.A."</item>
    ///     <item>TaxIDStatus [string]: tax ID status of the BusinessIdentity. ex: "valid", "invalid"</item>
    ///     <item>InsightTaxID [string]: tax ID returned by the insight provider. ex: "20.018.183/0001-80"</item>
    ///     <item>InsightDocumentType [string]: document type returned by the insight provider. ex: "cnpj"</item>
    ///     <item>NumPages [integer]: number of pages of the documents attached to the BusinessIdentity. ex: 5</item>
    ///     <item>Representatives [string]: JSON string with the representatives of the BusinessIdentity.</item>
    ///     <item>Attachments [list of strings]: list of BusinessAttachment ids attached to the BusinessIdentity. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
    ///     <item>Rules [string]: JSON string with the rules of the BusinessIdentity.</item>
    ///     <item>Status [string]: current status of the BusinessIdentity. ex: "created", "pending", "canceled", "processing", "success", "failed"</item>
    ///     <item>Created [DateTime]: creation DateTime for the BusinessIdentity. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the BusinessIdentity. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class BusinessIdentity : Resource
    {
        public string TaxID { get; }
        public List<string> Tags { get; }
        public string Name { get; }
        public string TaxIDStatus { get; }
        public string InsightTaxID { get; }
        public string InsightDocumentType { get; }
        public int? NumPages { get; }
        public string Representatives { get; }
        public List<string> Attachments { get; }
        public string Rules { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// BusinessIdentity object
        /// <br/>
        /// A BusinessIdentity represents a business to be validated. It can have several business attachments attached
        /// to it, which are used to validate the identity of the business. Once a business identity is created, business
        /// attachments must be attached to it using the created method of the business attachment resource. When all the required
        /// business attachments are attached to a business identity it can be sent to validation by patching its status to
        /// processing.
        /// <br/>
        /// When you initialize a BusinessIdentity, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>taxID [string]: business's tax ID (CNPJ). ex: "20.018.183/0001-80"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default []]: list of strings for reference when searching for BusinessIdentities. ex: List<string>{ "employees", "monthly" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when BusinessIdentity is created. ex: "5656565656565656"</item>
        ///     <item>name [string]: business's full name. ex: "Stark Bank S.A."</item>
        ///     <item>taxIDStatus [string]: tax ID status of the BusinessIdentity. ex: "valid", "invalid"</item>
        ///     <item>insightTaxID [string]: tax ID returned by the insight provider. ex: "20.018.183/0001-80"</item>
        ///     <item>insightDocumentType [string]: document type returned by the insight provider. ex: "cnpj"</item>
        ///     <item>numPages [integer]: number of pages of the documents attached to the BusinessIdentity. ex: 5</item>
        ///     <item>representatives [string]: JSON string with the representatives of the BusinessIdentity.</item>
        ///     <item>attachments [list of strings]: list of BusinessAttachment ids attached to the BusinessIdentity. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>rules [string]: JSON string with the rules of the BusinessIdentity.</item>
        ///     <item>status [string]: current status of the BusinessIdentity. ex: "created", "pending", "canceled", "processing", "success", "failed"</item>
        ///     <item>created [DateTime]: creation DateTime for the BusinessIdentity. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the BusinessIdentity. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public BusinessIdentity(
            string taxID = null, List<string> tags = null, string id = null, string name = null,
            string taxIDStatus = null, string insightTaxID = null, string insightDocumentType = null,
            int? numPages = null, string representatives = null, List<string> attachments = null,
            string rules = null, string status = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            TaxID = taxID;
            Tags = tags;
            Name = name;
            TaxIDStatus = taxIDStatus;
            InsightTaxID = insightTaxID;
            InsightDocumentType = insightDocumentType;
            NumPages = numPages;
            Representatives = representatives;
            Attachments = attachments;
            Rules = rules;
            Status = status;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create BusinessIdentity objects
        /// <br/>
        /// Send a list of BusinessIdentity objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>identities [list of BusinessIdentity objects]: list of BusinessIdentity objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BusinessIdentity objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BusinessIdentity> Create(List<BusinessIdentity> identities, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: identities,
                user: user
            ).ToList().ConvertAll(o => (BusinessIdentity)o);
        }

        /// <summary>
        /// Create BusinessIdentity objects
        /// <br/>
        /// Send a list of BusinessIdentity dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>identities [list of Dictionaries]: list of dictionaries representing the BusinessIdentity objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BusinessIdentity objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<BusinessIdentity> Create(List<Dictionary<string, object>> identities, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: identities,
                user: user
            ).ToList().ConvertAll(o => (BusinessIdentity)o);
        }

        /// <summary>
        /// Retrieve a specific BusinessIdentity by its id
        /// <br/>
        /// Receive a single BusinessIdentity object previously created in the Stark Infra API by passing its id
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
        ///     <item>BusinessIdentity object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static BusinessIdentity Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as BusinessIdentity;
        }

        /// <summary>
        /// Retrieve BusinessIdentity objects
        /// <br/>
        /// Receive an IEnumerable of BusinessIdentity objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "created", "pending", "canceled", "processing", "success", "failed"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>taxIDs [list of strings, default null]: list of tax IDs to filter retrieved objects. ex: new List<string>{ "20.018.183/0001-80" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of BusinessIdentity objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<BusinessIdentity> Query(
            int? limit = null, DateTime? after = null, DateTime? before = null, List<string> status = null,
            List<string> tags = null, List<string> ids = null, List<string> taxIDs = null, User user = null
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
                    { "ids", ids },
                    { "taxIds", taxIDs }
                },
                user: user
            ).Cast<BusinessIdentity>();
        }

        /// <summary>
        /// Retrieve paged BusinessIdentity objects
        /// <br/>
        /// Receive a list of up to 100 BusinessIdentity objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "created", "pending", "canceled", "processing", "success", "failed"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>taxIDs [list of strings, default null]: list of tax IDs to filter retrieved objects. ex: new List<string>{ "20.018.183/0001-80" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of BusinessIdentity objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of BusinessIdentity objects</item>
        /// </list>
        /// </summary>
        public static (List<BusinessIdentity> page, string pageCursor) Page(
            string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> tags = null, List<string> ids = null,
            List<string> taxIDs = null, User user = null
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
                    { "ids", ids },
                    { "taxIds", taxIDs }
                },
                user: user
            );
            List<BusinessIdentity> identities = new List<BusinessIdentity>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                identities.Add(subResource as BusinessIdentity);
            }
            return (identities, pageCursor);
        }

        /// <summary>
        /// Update BusinessIdentity entity
        /// <br/>
        /// Update a BusinessIdentity by passing its id.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id [string]: BusinessIdentity id. ex: "5656565656565656"</item>
        ///     <item>patchData [Dictionary of string, object]: Dictionary of properties to patch</item>
        ///         <list>
        ///             <item>status [string, default null]: You may send the BusinessIdentity to validation by passing "processing" in the status</item>
        ///             <item>tags [list of strings, default null]: list of strings for reference when searching for BusinessIdentities. ex: List<string>{ "employees", "monthly" }</item>
        ///         </list>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target BusinessIdentity with updated attributes</item>
        /// </list>
        /// </summary>
        public static BusinessIdentity Update(string id, Dictionary<string, object> patchData, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as BusinessIdentity;
        }

        /// <summary>
        /// Update BusinessIdentity entity
        /// <br/>
        /// Update a BusinessIdentity by passing its id.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id [string]: BusinessIdentity id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, default null]: You may send the BusinessIdentity to validation by passing "processing" in the status</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for BusinessIdentities. ex: List<string>{ "employees", "monthly" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target BusinessIdentity with updated attributes</item>
        /// </list>
        /// </summary>
        public static BusinessIdentity Update(string id, string status = null, List<string> tags = null, User user = null)
        {
            Dictionary<string, object> patchData = new Dictionary<string, object>();
            if (status != null) patchData.Add("status", status);
            if (tags != null) patchData.Add("tags", tags);

            return Update(id, patchData, user);
        }

        /// <summary>
        /// Cancel a BusinessIdentity entity
        /// <br/>
        /// Cancel a BusinessIdentity entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: BusinessIdentity unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled BusinessIdentity object</item>
        /// </list>
        /// </summary>
        public static BusinessIdentity Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as BusinessIdentity;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "BusinessIdentity", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string taxID = json.taxId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string name = json.name;
            string taxIDStatus = json.taxIdStatus;
            string insightTaxID = json.insightTaxId;
            string insightDocumentType = json.insightDocumentType;
            int? numPages = json.numPages;
            string representatives = json.representatives?.ToString();
            List<string> attachments = json.attachments?.ToObject<List<string>>();
            string rules = json.rules?.ToString();
            string status = json.status;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new BusinessIdentity(
                taxID: taxID, tags: tags, id: id, name: name, taxIDStatus: taxIDStatus,
                insightTaxID: insightTaxID, insightDocumentType: insightDocumentType, numPages: numPages,
                representatives: representatives, attachments: attachments, rules: rules, status: status,
                created: created, updated: updated
            );
        }
    }
}
