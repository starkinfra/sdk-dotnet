using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// DynamicBrcode object
    /// <br/>
    /// BR Codes store information represented by Pix QR Codes, which are used to
    /// send or receive Pix transactions in a convenient way.
    /// DynamicBrcodes represent charges with information that can change at any time,
    /// since all data needed for the payment is requested dynamically to an URL stored
    /// in the BR Code. Stark Infra will receive the GET request and forward it to your
    /// registered endpoint with a GET request containing the UUID of the BR Code for
    /// identification.
    /// <br/>
    /// When you initialize a DynamicBrcode, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: receiver's name. ex: "Tony Stark"</item>
    ///     <item>City [string]: receiver's city name. ex: "Rio de Janeiro"</item>
    ///     <item>ExternalID [string]: string that must be unique among all your DynamicBrcodes. Duplicated external ids will cause failures. ex: "my-internal-id-123456"</item>
    ///     <item>Type [string, default "instant"]: type of the DynamicBrcode. Options: "instant", "due", "subscription", "subscriptionAndInstant" or "dueAndOrSubscription"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID [string]: id returned on creation, this is the BR Code. ex: "00020126360014br.gov.bcb.pix0114+552840092118152040000530398654040.095802BR5915Jamie Lannister6009Sao Paulo620705038566304FC6C"</item>
    ///     <item>Uuid [string]: unique uuid returned when the DynamicBrcode is created. ex: "4e2eab725ddd495f9c98ffd97440702d"</item>
    ///     <item>Url [string]: url link to the BR Code image. ex: "https://brcode-h.development.starkinfra.com/dynamic-qrcode/901e71f2447c43c886f58366a5432c4b.png"</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the DynamicBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the DynamicBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class DynamicBrcode : Resource
    {
        public string Name { get; }
        public string City { get; }
        public string ExternalID { get; }
        public string Type { get; }
        public List<string> Tags { get; }
        public string Uuid { get; }
        public string Url { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// DynamicBrcode object
        /// <br/>
        /// BR Codes store information represented by Pix QR Codes, which are used to
        /// send or receive Pix transactions in a convenient way.
        /// DynamicBrcodes represent charges with information that can change at any time,
        /// since all data needed for the payment is requested dynamically to an URL stored
        /// in the BR Code. Stark Infra will receive the GET request and forward it to your
        /// registered endpoint with a GET request containing the UUID of the BR Code for
        /// identification.
        /// <br/>
        /// When you initialize a DynamicBrcode, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: receiver's name. ex: "Tony Stark"</item>
        ///     <item>city [string]: receiver's city name. ex: "Rio de Janeiro"</item>
        ///     <item>externalID [string]: string that must be unique among all your DynamicBrcodes. Duplicated external ids will cause failures. ex: "my-internal-id-123456"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>type [string, default "instant"]: type of the DynamicBrcode. Options: "instant", "due", "subscription", "subscriptionAndInstant" or "dueAndOrSubscription"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: id returned on creation, this is the BR Code. ex: "00020126360014br.gov.bcb.pix0114+552840092118152040000530398654040.095802BR5915Jamie Lannister6009Sao Paulo620705038566304FC6C"</item>
        ///     <item>uuid [string]: unique uuid returned when the DynamicBrcode is created. ex: "4e2eab725ddd495f9c98ffd97440702d"</item>
        ///     <item>url [string]: url link to the BR Code image. ex: "https://brcode-h.development.starkinfra.com/dynamic-qrcode/901e71f2447c43c886f58366a5432c4b.png"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the DynamicBrcode. ex: DateTime(2020, 3, 10)</item>
        ///     <item>created [DateTime]: creation DateTime for the DynamicBrcode. ex: DateTime(2020, 3, 10)</item>
        /// </list>
        /// </summary>
        public DynamicBrcode( 
            string name, string city, string externalID, string type=null, 
            List<string> tags=null, string id=null, string uuid=null, 
            string url=null, DateTime? updated=null, DateTime? created=null
        ) : base(id)
        {
            Name = name;
            City = city;
            ExternalID = externalID;
            Type = type;
            Tags = tags;
            Uuid = uuid;
            Url = url;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create DynamicBrcode objects
        /// <br/>
        /// Send a list of DynamicBrcode objects for creation at the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>brcodes [list of DynamicBrcode objects]: list of DynamicBrcode objects to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DynamicBrcode objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<DynamicBrcode> Create(List<DynamicBrcode> brcodes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: brcodes,
                user: user
            ).ToList().ConvertAll(o => (DynamicBrcode)o);
        }

        /// <summary>
        /// Create DynamicBrcode objects
        /// <br/>
        /// Send a list of DynamicBrcode objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>brcodes [list of Dictionaries]: list of dictionaries representing the DynamicBrcode objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DynamicBrcode objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<DynamicBrcode> Create(List<Dictionary<string, object>> brcodes, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: brcodes,
                user: user
            ).ToList().ConvertAll(o => (DynamicBrcode)o);
        }

        /// <summary>
        /// Retrieve a specific DynamicBrcode by its id
        /// <br/>
        /// Receive a single DynamicBrcode object previously created in the Stark Infra API by passing its uuid
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>uuid [string]: object's unique uuid. ex: "97756273400d42ce9086404fe10ea0d6"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>DynamicBrcode object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static DynamicBrcode Get(string uuid, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: uuid,
                user: user
            ) as DynamicBrcode;
        }

        /// <summary>
        /// Retrieve DynamicBrcode objects
        /// <br/>
        /// Receive an IEnumerable of DynamicBrcode objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>externalIds [list of strings, default null]: list of externalIds to filter retrieved objects. ex: new List<string>{ "my_external_id1", "my_external_id2" }</item>
        ///     <item>uuids [list of strings, default null]: list of uuids to filter retrieved objects. ex: new List<string>{ "97756273400d42ce9086404fe10ea0d6", "e3da0b6d56fa4045b9b295b2be82436e" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of DynamicBrcode objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<DynamicBrcode> Query(
            int? limit = null, object after = null, object before = null, List<string> externalIds = null,
            List<string> uuids = null, List<string> tags = null, User user = null
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
                    { "externalIds", externalIds },
                    { "uuids", uuids },
                    { "tags", tags },
                },
                user: user
            ).Cast<DynamicBrcode>();
        }

        /// <summary>
        /// Retrieve paged DynamicBrcode objects
        /// <br/>
        /// Receive a list of up to 100 DynamicBrcode objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>externalIds [list of strings, default null]: list of externalIds to filter retrieved objects. ex: new List<string>{ "my_external_id1", "my_external_id2" }</item>
        ///     <item>uuids [list of strings, default null]: list of uuids to filter retrieved objects. ex: new List<string>{ "97756273400d42ce9086404fe10ea0d6", "e3da0b6d56fa4045b9b295b2be82436e" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DynamicBrcode objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of DynamicBrcode objects</item>
        /// </list>
        /// </summary>
        public static (List<DynamicBrcode> page, string pageCursor) Page(
            string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> externalIds = null, List<string> uuids = null,
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
                    { "externalIds", externalIds },
                    { "uuids", uuids },
                    { "tags", tags },
                },
                user: user
            );
            List<DynamicBrcode> DynamicBrcode = new List<DynamicBrcode>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                DynamicBrcode.Add(subResource as DynamicBrcode);
            }
            return (DynamicBrcode, pageCursor);
        }

        /// <summary>
        /// Helps you respond to a due DynamicBrcode Read
        /// <br/>
        /// When a Due DynamicBrcode is read by your user, a GET request containing the Brcode's 
        /// UUID will be made to your registered URL to retrieve additional information needed 
        /// to complete the transaction.
        /// The get request must be answered in the following format, within 5 seconds, and with 
        /// an HTTP status code 200.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>version [integer]: integer that represents how many times the BR Code was updated.</item>
        ///     <item>created [DateTime]: creation DateTime in ISO format of the DynamicBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>due [DateTime]: requested payment due DateTime in ISO format. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>expiration [integer]: time in seconds counted from the creation DateTime until the DynamicBrcode expires. After expiration, the BR Code cannot be paid anymore. ex: 86400</item>
        ///     <item>keyID [string]: receiver's PixKey id. Can be a taxID (CPF/CNPJ), a phone number, an email or an alphanumeric sequence (EVP). ex: "+5511989898989"</item>
        ///     <item>status [string]: BR Code status. Options: "created", "overdue", "paid", "canceled" or "expired"</item>
        ///     <item>reconciliationID [string]: id to be used for conciliation of the resulting Pix transaction. This id must have from to 26 to 35 alphanumeric characters ex: "cd65c78aeb6543eaaa0170f68bd741ee"</item>
        ///     <item>nominalAmount [integer]: positive integer that represents the amount in cents of the resulting Pix transaction. ex: 1234 (= R$ 12.34)</item>
        ///     <item>senderName [string]: sender's full name. ex: "Anthony Edward Stark"</item>
        ///     <item>senderTaxID [string]: sender's CPF (11 digits formatted or unformatted) or CNPJ (14 digits formatted or unformatted). ex: "01.001.001/0001-01"</item>
        ///     <item>receiverName [string]: receiver's full name. ex: "Jamie Lannister"</item>
        ///     <item>receiverTaxID [string]: receiver's CPF (11 digits formatted or unformatted) or CNPJ (14 digits formatted or unformatted). ex: "012.345.678-90"</item>
        ///     <item>receiverStreetLine [string]: receiver's main address. ex: "Av. Paulista, 200"</item>
        ///     <item>receiverCity [string]: receiver's address city name. ex: "Sao Paulo"</item>
        ///     <item>receiverStateCode [string]: receiver's address state code. ex: "SP"</item>
        ///     <item>receiverZipCode [string]: receiver's address zip code. ex: "01234-567"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>fine [float, default 2.0]: Percentage charged if the sender pays after the due DateTime.</item>
        ///     <item>interest [float, default 1.0]: Interest percentage charged if the sender pays after the due DateTime.</item>
        ///     <item>discounts [list of Discount objects, default null]: list of Discount objects with "percentage":float and "due":DateTime.</item>
        ///     <item>description [string, default null]: additional information to be shown to the sender at the moment of payment.</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Dumped JSON string that must be returned to us</item>
        /// </list>
        /// </summary>
        public static string ResponseDue(
            int version, DateTime? created, DateTime due, int expiration, string keyID, string status,
            string reconciliationID, int nominalAmount, string senderName, string senderTaxID, 
            string receiverName, string receiverTaxID, string receiverStreetLine, string receiverCity, 
            string receiverStateCode, string receiverZipCode, float? fine = null, float? interest = null,
            List<Discount> discounts = null, string description = null
        )
        {
            Dictionary<string, object> rawResponse = new Dictionary<string, object> {
                { "version", version },
                { "created", created },
                { "due", due },
                { "expiration", expiration },
                { "keyID", keyID },
                { "status", status },
                { "reconciliationID", reconciliationID },
                { "nominalAmount", nominalAmount },
                { "senderName", senderName },
                { "senderTaxID", senderTaxID },
                { "receiverName", receiverName },
                { "receiverTaxID", receiverTaxID },
                { "receiverStreetLine", receiverStreetLine },
                { "receiverCity", receiverCity },
                { "receiverStateCode", receiverStateCode },
                { "receiverZipCode", receiverZipCode },
                { "fine", fine },
                { "interest", interest },
                { "discounts", ParseDiscounts(discounts) },
                { "description", description },
            };
            Dictionary<string, object> response = StarkCore.Utils.Api.CastJsonToApiFormat(rawResponse);
            return JsonConvert.SerializeObject(response);
        }

        internal static List<Dictionary<string, object>> ParseDiscounts(List<Discount> discounts)
        {
            List<Dictionary<string, object>> discountsDictionary = new List<Dictionary<string, object>> { };
            foreach (Discount discount in discounts)
            {
                discountsDictionary.Add(JObject.FromObject(discount).ToObject<Dictionary<string, object>>());
            }
            return discountsDictionary;
        }

        /// <summary>
        /// Helps you respond to an instant DynamicBrcode Read
        /// <br/>
        /// When an instant DynamicBrcode is read by your user, a GET request containing the BR Code's UUID will be made
        /// to your registered URL to retrieve additional information needed to complete the transaction.
        /// The get request must be answered in the following format within 5 seconds and with an HTTP status code 200.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>version [integer]: integer that represents how many times the BR Code was updated.</item>
        ///     <item>created [DateTime]: creation DateTime of the DynamicBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>keyID [string]: receiver's PixKey id. Can be a taxID (CPF/CNPJ), a phone number, an email or an alphanumeric sequence (EVP). ex: "+5511989898989"</item>
        ///     <item>status [string]: BR Code's status. Options: "created", "overdue", "paid", "canceled" or "expired"</item>
        ///     <item>reconciliationID [string]: id to be used for conciliation of the resulting Pix transaction. ex: "cd65c78aeb6543eaaa0170f68bd741ee"</item>
        ///     <item>amount [integer]: positive integer that represents the amount in cents of the resulting Pix transaction. ex: 1234 (= R$ 12.34)</item>
        /// </list>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>cashierType [string, default null]: cashier's type. Required if the cashAmount is different from 0. Options: "merchant", "participant" and "other"</item>
        ///     <item>cashierBankCode [string, default null]: cashier's bank code. Required if the cashAmount is different from 0. ex: "20018183"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>cashAmount [integer, default 0]: amount to be withdrawn from the cashier in cents. ex: 1000 (= R$ 10.00)</item>
        ///     <item>expiration [integer, default 86400 (1 day)]: time in seconds counted from the creation DateTime until the DynamicBrcode expires. After expiration, the BR Code cannot be paid anymore. Default value: 86400 (1 day)</item>
        ///     <item>senderName [string, default null]: sender's full name. ex: "Anthony Edward Stark"</item>
        ///     <item>senderTaxID [string, default null]: sender's CPF (11 digits formatted or unformatted) or CNPJ (14 digits formatted or unformatted). ex: "01.001.001/0001-01"</item>
        ///     <item>amountType [string, default "fixed"]: amount type of the Brcode. If the amount type is "custom" the Brcode's amount can be changed by the sender at the moment of payment. Options: "fixed"or "custom"</item>
        ///     <item>description [string, default null]: additional information to be shown to the sender at the moment of payment.</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Dumped JSON string that must be returned to us on the IssuingPurchase request</item>
        /// </list>
        /// </summary>
        public static string ResponseInstant(
            int version, DateTime? created, string keyID, string status, string reconciliationID, int amount,
            string cashierType = null, string cashierBankCode = null, int? cashAmount = null, int? expiration = null,
            string senderName = null, string senderTaxID = null, string amountType =  null, string description = null
        )
        {
            Dictionary<string, object> rawResponse = new Dictionary<string, object> {
                { "version", version },
                { "created", created },
                { "keyID", keyID },
                { "status", status },
                { "reconciliationID", reconciliationID },
                { "amount", amount },
                { "cashierType", cashierType },
                { "cashierBankCode", cashierBankCode },
                { "cashAmount", cashAmount },
                { "expiration", expiration },
                { "senderName", senderName },
                { "senderTaxID", senderTaxID },
                { "amountType", amountType },
                { "description", description },
            };
            Dictionary<string, object> response = StarkCore.Utils.Api.CastJsonToApiFormat(rawResponse);
            return JsonConvert.SerializeObject(response);
        }

        /// <summary>
        /// Verify a DynamicBrcode Read
        /// <br/>
        /// When a DynamicBrcode is read by your user, a GET request will be made to your registered URL to
        /// retrieve additional information needed to complete the transaction.
        /// Use this method to verify the authenticity of a GET request received at your registered endpoint.
        /// If the provided digital signature does not check out with the StarkInfra public key,
        /// a Error.InvalidSignatureException will be raised.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>uuid [string]: unique uuid of the DynamicBrcode, passed as a path variable in the DynamicBrcode Read request. ex: "4e2eab725ddd495f9c98ffd97440702d"</item>
        ///     <item>signature [string]: base-64 digital signature received at response header "Digital-Signature"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>verified Brcode's uuid.</item>
        /// </list>
        /// </summary>
        public static string Verify(string uuid, string signature, User user = null)
        {
            return Parse.Verify(
                content: uuid,
                signature: signature,
                user: user
            );
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "DynamicBrcode", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string name = json.name;
            string city = json.city;
            string externalID = json.externalId;
            string type = json.type;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string uuid = json.uuid;
            string url = json.url;
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);

            return new DynamicBrcode(
                name: name, city: city, externalID: externalID, type: type, 
                tags: tags, id: id, uuid: uuid, url: url, updated: updated, 
                created: created
            );
        }
    }
}
