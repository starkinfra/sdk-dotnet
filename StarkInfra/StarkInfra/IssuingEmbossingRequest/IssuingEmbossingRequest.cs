using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingEmbossingRequest object
    /// <br/>
    /// The IssuingEmbossingRequest object displays the information of embossing requests in your Workspace.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>CardID [string]: id of the IssuingCard to be embossed. ex "5656565656565656"</item>
    ///     <item>CardDesignID [string]: card IssuingDesign id. ex "5656565656565656"</item>
    ///     <item>EnvelopeDesignID [string]: envelope IssuingDesign id. ex "5656565656565656"</item>
    ///     <item>DisplayName1 [string]: card displayed name. ex: "ANTHONY STARK"</item>
    ///     <item>ShippingCity [string]: shipping city. ex: "NEW YORK"</item>
    ///     <item>ShippingCountryCode [string]: shipping country code. ex: "US"</item>
    ///     <item>ShippingDistrict [string]: shipping district. ex: "NY"</item>
    ///     <item>ShippingStateCode [string]: shipping state code. ex: "NY"</item>
    ///     <item>ShippingStreetLine1 [string]: shipping main address. ex: "AVENUE OF THE AMERICAS"</item>
    ///     <item>ShippingStreetLine2 [string]: shipping address complement. ex: "Apt. 6"</item>
    ///     <item>ShippingService [string]: shipping service. ex: "loggi"</item>
    ///     <item>ShippingTrackingNumber [string]: shipping tracking number. ex: "5656565656565656"</item>
    ///     <item>ShippingZipCode [string]: shipping zip code. ex: "12345-678"</item>
    ///     <item>EmbosserID [string, default null]: id of the card embosser. ex: "5656565656565656"</item>
    ///     <item>DisplayName2 [string, default null]: card displayed name. ex: "IT Services"</item>
    ///     <item>DisplayName3 [string, default null]: card displayed name. ex: "StarkBank S.A."</item>
    ///     <item>ShippingPhone [string, default null]: shipping phone. ex: "+5511999999999"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string> {"card", "corporate"}</item>
    ///     <item>ID [string]: unique id returned when IssuingEmbossingRequest is created. ex: "5656565656565656"</item>
    ///     <item>Fee [integer]: fee charged when IssuingEmbossingRequest is created. ex: 1000</item>
    ///     <item>Status [string]: status of the IssuingEmbossingRequest. ex: "created", "processing", "success", "failed"</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingEmbossingRequests. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingEmbossingRequests. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingEmbossingRequest : Resource
    {
        public string CardID { get; }
        public string CardDesignID { get; }
        public string EnvelopeDesignID { get; }
        public string DisplayName1 { get; }
        public string ShippingCity { get; }
        public string ShippingCountryCode { get; }
        public string ShippingDistrict { get; }
        public string ShippingStateCode { get; }
        public string ShippingStreetLine1 { get; }
        public string ShippingStreetLine2 { get; }
        public string ShippingService { get; }
        public string ShippingTrackingNumber { get; }
        public string ShippingZipCode { get; }
        public string EmbosserID { get; }
        public string DisplayName2 { get; }
        public string DisplayName3 { get; }
        public string ShippingPhone { get; }
        public List<String> Tags { get; }
        public int? Fee { get; }
        public string Status { get; }
        public DateTime? Updated  { get; }
        public DateTime? Created  { get; }

        /// <summary>
        /// IssuingEmbossingRequest object
        /// <br/>
        /// The IssuingEmbossingRequest object displays the information of embossing requests in your Workspace.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>cardID [string]: id of the IssuingCard to be embossed. ex "5656565656565656"</item>
        ///     <item>cardDesignID [string]: card IssuingDesign id. ex "5656565656565656"</item>
        ///     <item>envelopeDesignID [string]: envelope IssuingDesign id. ex "5656565656565656"</item>
        ///     <item>displayName1 [string]: card displayed name. ex: "ANTHONY STARK"</item>
        ///     <item>shippingCity [string]: shipping city. ex: "NEW YORK"</item>
        ///     <item>shippingCountryCode [string]: shipping country code. ex: "US"</item>
        ///     <item>shippingDistrict [string]: shipping district. ex: "NY"</item>
        ///     <item>shippingStateCode [string]: shipping state code. ex: "NY"</item>
        ///     <item>shippingStreetLine1 [string]: shipping main address. ex: "AVENUE OF THE AMERICAS"</item>
        ///     <item>shippingStreetLine2 [string]: shipping address complement. ex: "Apt. 6"</item>
        ///     <item>shippingService [string]: shipping service. ex: "loggi"</item>
        ///     <item>shippingTrackingNumber [string]: shipping tracking number. ex: "5656565656565656"</item>
        ///     <item>shippingZipCode [string]: shipping zip code. ex: "12345-678"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>embosserID [string, default null]: id of the card embosser. ex: "5656565656565656"</item>
        ///     <item>displayName2 [string, default null]: card displayed name. ex: "IT Services"</item>
        ///     <item>displayName3 [string, default null]: card displayed name. ex: "StarkBank S.A."</item>
        ///     <item>shippingPhone [string, default null]: shipping phone. ex: "+5511999999999"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string> { "card", "corporate" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingEmbossingRequest is created. ex: "5656565656565656"</item>
        ///     <item>fee [integer]: fee charged when IssuingEmbossingRequest is created. ex: 1000</item>
        ///     <item>status [string]: status of the IssuingEmbossingRequest. ex: "created", "processing", "success", "failed"</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingEmbossingRequests. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingEmbossingRequests. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingEmbossingRequest(
            string cardID = null, string cardDesignID = null, string envelopeDesignID = null,
            string displayName1 = null, string shippingCity = null, string shippingCountryCode = null, 
            string shippingDistrict = null, string shippingStateCode = null, string shippingStreetLine1 = null,
            string shippingStreetLine2 = null, string shippingService = null, string shippingTrackingNumber = null, 
            string shippingZipCode = null, string embosserID = null, string displayName2 = null, string displayName3 = null, 
            string shippingPhone = null, List<String> tags = null, string id = null, int? fee = null, string status = null, 
            DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            CardID = cardID;
            CardDesignID = cardDesignID;
            EnvelopeDesignID = envelopeDesignID;
            DisplayName1 = displayName1;
            ShippingCity = shippingCity;
            ShippingCountryCode = shippingCountryCode;
            ShippingDistrict = shippingDistrict;
            ShippingStateCode = shippingStateCode;
            ShippingStreetLine1 = shippingStreetLine1;
            ShippingStreetLine2 = shippingStreetLine2;
            ShippingService = shippingService;
            ShippingTrackingNumber = shippingTrackingNumber;
            ShippingZipCode = shippingZipCode;
            EmbosserID = embosserID;
            DisplayName2 = displayName2;
            DisplayName3 = displayName3;
            ShippingPhone = shippingPhone;
            Tags = tags;
            Fee = fee;
            Status = status;
            Updated = updated;
            Created = created;
        }
        
        /// <summary>
        /// Create IssuingEmbossingRequests
        /// <br/>
        /// Send a list of IssuingEmbossingRequests objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of IssuingEmbossingRequest objects]: list of IssuingEmbossingRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingEmbossingRequests objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingEmbossingRequest> Create(List<IssuingEmbossingRequest> requests, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (IssuingEmbossingRequest)o);
        }

        /// <summary>
        /// Create IssuingEmbossingRequests
        /// <br/>
        /// Send a list of IssuingEmbossingRequests dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of dictionaries]: list of Dictionaries representing the IssuingEmbossingRequests to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingEmbossingRequests objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingEmbossingRequest> Create(List<Dictionary<string, object>> requests, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (IssuingEmbossingRequest)o);
        }
        
        /// <summary>
        /// Retrieve IssuingEmbossingRequests
        /// <br/>
        /// Receive an IEnumerable of IssuingEmbossingRequest objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created", "processing", "success", "failed" }</item>
        ///     <item>cardIds [list of strings, default null]: list of cardIds to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingEmbossingRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingEmbossingRequest> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> cardIds = null, List<string> ids = null, List<string> tags = null,
            User user = null)
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
                    { "cardIds", cardIds },
                    { "ids", ids },
                    { "tags", tags },
                },
                user: user
            ).Cast<IssuingEmbossingRequest>();
        }
        
        /// <summary>
        /// Retrieve paged IssuingEmbossingRequest
        /// <br/>
        /// Receive a list of up to 100 IssuingEmbossingRequest objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created", "processing", "success", "failed" }</item>
        ///     <item>cardIds [list of strings, default null]: list of cardIds to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingEmbossingRequest objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingEmbossingRequest objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingEmbossingRequest> page, string pageCursor) Page(string cursor = null, int? limit = null, 
            DateTime? after = null, DateTime? before = null, List<string> status = null, List<string> cardIds = null,
            List<string> ids = null, List<string> tags = null,
            User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "cardIds", cardIds },
                    { "ids", ids },
                    { "tags", tags },
                },
                user: user
            );
            List<IssuingEmbossingRequest> requests = new List<IssuingEmbossingRequest>();
            foreach (SubResource subResource in page)
            {
                requests.Add(subResource as IssuingEmbossingRequest);
            }
            return (requests, pageCursor);
        }

        /// <summary>
        /// Retrieve a specific IssuingEmbossingRequest
        /// <br/>
        /// Receive a single IssuingEmbossingRequest object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingEmbossingRequest object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingEmbossingRequest Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingEmbossingRequest;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingEmbossingRequest", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string cardID = json.cardId;
            string cardDesignID = json.cardDesignId;
            string envelopeDesignID = json.envelopeDesignId;
            string displayName1 = json.displayName1;
            string shippingCity = json.shippingCity;
            string shippingCountryCode = json.shippingCountryCode;
            string shippingDistrict = json.shippingDistrict;
            string shippingStateCode = json.shippingStateCode;
            string shippingStreetLine1 = json.shippingStreetLine1; 
            string shippingStreetLine2 = json.shippingStreetLine2; 
            string shippingService = json.shippingService;
            string shippingTrackingNumber = json.shippingTrackingNumber;
            string shippingZipCode = json.shippingZipCode; 
            string embosserID = json.embosserId;
            string displayName2 = json.displayName2; 
            string displayName3 = json.displayName3;
            string shippingPhone = json.shippingPhone;
            List<String> tags = json.tags.ToObject<List<string>>();
            int? fee = json.fee;
            string status = json.status;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new IssuingEmbossingRequest(
                id: id, cardID: cardID, cardDesignID: cardDesignID, envelopeDesignID: envelopeDesignID, displayName1: displayName1,
                shippingCity: shippingCity, shippingCountryCode: shippingCountryCode, shippingDistrict: shippingDistrict,
                shippingStateCode: shippingStateCode, shippingStreetLine1: shippingStreetLine1, shippingStreetLine2: shippingStreetLine2,
                shippingService: shippingService, shippingTrackingNumber: shippingTrackingNumber, shippingZipCode: shippingZipCode,
                embosserID: embosserID, displayName2: displayName2, displayName3: displayName3, shippingPhone: shippingPhone,
                tags: tags, fee: fee, status: status, updated: updated, created: created
            );
        }
    }
}
