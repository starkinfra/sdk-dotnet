using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// StaticBrcode object
    /// <br/>
    /// A StaticBrcode stores account information in the form of a PixKey and can be used to create 
    /// Pix transactions easily.
    /// <br/>
    /// When you initialize a StaticBrcode, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: receiver's name. ex: "Tony Stark"</item>
    ///     <item>KeyID [string]: receiver's PixKey id. ex: "+5541999999999"</item>
    ///     <item>City [string, default São Paulo]: receiver's city name. ex: "Rio de Janeiro"</item>
    ///     <item>Amount [integer, default 0]: positive integer that represents the amount in cents of the resulting Pix transaction. If the amount is zero, the sender can choose any amount in the moment of payment. ex: 1234 (= R$ 12.34)</item>
    ///     <item>CashierBankCode [string, default ""]: Cashier's bank code. ex: "20018183".</item>
    ///     <item>Description [string, default ""]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
    ///     <item>ReconciliationID [string, default ""]: id to be used for conciliation of the resulting Pix transaction. This id must have up to 25 alphanumeric digits ex: "ah27s53agj6493hjds6836v49"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID [string]: id returned on creation, this is the BR code. ex: "00020126360014br.gov.bcb.pix0114+552840092118152040000530398654040.095802BR5915Jamie Lannister6009Sao Paulo620705038566304FC6C"</item>
    ///     <item>Uuid [string]: unique uuid returned when a StaticBrcode is created. ex: "97756273400d42ce9086404fe10ea0d6"</item>
    ///     <item>Url [string]: url link to the BR code image. ex: "https://brcode-h.development.starkinfra.com/static-qrcode/97756273400d42ce9086404fe10ea0d6.png"</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the StaticBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the StaticBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class StaticBrcode : Resource
    {
        public string Name { get; }
        public string KeyID { get; }
        public string City { get; }
        public string CashierBankCode { get; }
        public string Description { get; }
        public long? Amount { get; }
        public string ReconciliationID { get; }
        public List<string> Tags { get; }
        public string Uuid { get; }
        public string Url { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// StaticBrcode object
        /// <br/>
        /// A StaticBrcode stores account information in the form of a PixKey and can be used to create 
        /// Pix transactions easily.
        /// <br/>
        /// When you initialize a StaticBrcode, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: receiver's name. ex: "Tony Stark"</item>
        ///     <item>keyID [string]: receiver's PixKey id. ex: "+5541999999999"</item>
        ///     <item>city [string]: receiver's city name. ex: "Rio de Janeiro"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>amount [integer, default 0]: positive integer that represents the amount in cents of the resulting Pix transaction. If the amount is zero, the sender can choose any amount in the moment of payment. ex: 1234 (= R$ 12.34)</item>
        ///     <item>reconciliationID [string, default ""]: id to be used for conciliation of the resulting Pix transaction. This id must have up to 25 alphanumeric digits ex: "ah27s53agj6493hjds6836v49"</item>
        ///     <item>cashierBankCode [string, default ""]: Cashier's bank code. ex: "20018183".</item>
        ///     <item>description [string, default ""]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: id returned on creation, this is the BR code. ex: "00020126360014br.gov.bcb.pix0114+552840092118152040000530398654040.095802BR5915Jamie Lannister6009Sao Paulo620705038566304FC6C"</item>
        ///     <item>uuid [string]: unique uuid returned when a StaticBrcode is created. ex: "97756273400d42ce9086404fe10ea0d6"</item>
        ///     <item>url [string]: url link to the BR code image. ex: "https://brcode-h.development.starkinfra.com/static-qrcode/97756273400d42ce9086404fe10ea0d6.png"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the StaticBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the StaticBrcode. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public StaticBrcode(
            string name, string keyID, string city, long? amount = null, 
            string reconciliationID = null, string cashierBankCode = null,
            string description = null, List<string> tags = null, string id = null, 
            string uuid = null, string url = null, DateTime? updated = null, 
            DateTime? created = null
        ) : base(id)
        {
            Name = name;
            KeyID = keyID;
            City = city;
            Amount = amount;
            ReconciliationID = reconciliationID;
            CashierBankCode = cashierBankCode;
            Description = description;
            Tags = tags;
            Uuid = uuid;
            Url = url;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create StaticBrcode objects
        /// <br/>
        /// Send a list of StaticBrcode objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>brcodes [list of StaticBrcode objects]: list of StaticBrcode objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of StaticBrcode objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<StaticBrcode> Create(List<StaticBrcode> brcodes, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: brcodes,
                user: user
            ).ToList().ConvertAll(o => (StaticBrcode)o);
        }

        /// <summary>
        /// Create StaticBrcode objects
        /// <br/>
        /// Send a list of StaticBrcode objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>brcodes [list of Dictionaries]: list of dictionaries representing the StaticBrcode objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of StaticBrcode objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<StaticBrcode> Create(List<Dictionary<string, object>> brcodes, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: brcodes,
                user: user
            ).ToList().ConvertAll(o => (StaticBrcode)o);
        }

        /// <summary>
        /// Retrieve a specific StaticBrcode by its uuid
        /// <br/>
        /// Receive a single StaticBrcode object previously created in the Stark Infra API by its uuid
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
        ///     <item>StaticBrcode object that corresponds to the given uuid.</item>
        /// </list>
        /// </summary>
        public static StaticBrcode Get(string uuid, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: uuid,
                user: user
            ) as StaticBrcode;
        }

        /// <summary>
        /// Retrieve StaticBrcode objects
        /// <br/>
        /// Receive an IEnumerable of StaticBrcode objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>uuids [list of strings, default null]: list of uuids to filter retrieved objects. ex: new List<string>{ "97756273400d42ce9086404fe10ea0d6", "e3da0b6d56fa4045b9b295b2be82436e" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of StaticBrcode objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<StaticBrcode> Query(
            int? limit = null, object after = null, object before = null, 
            List<string> uuids = null, List<string> tags = null, User user = null
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
                    { "uuids", uuids },
                    { "tags", tags },
                },
                user: user
            ).Cast<StaticBrcode>();
        }

        /// <summary>
        /// Retrieve paged StaticBrcode objects
        /// <br/>
        /// Receive a list of up to 100 StaticBrcode objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>uuids [list of strings, default null]: list of uuids to filter retrieved objects. ex: new List<string>{ "97756273400d42ce9086404fe10ea0d6", "e3da0b6d56fa4045b9b295b2be82436e" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of StaticBrcode objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of StaticBrcode objects</item>
        /// </list>
        /// </summary>
        public static (List<StaticBrcode> page, string pageCursor) Page(
            string cursor = null, List<string> uuids = null, DateTime? after = null,
            DateTime? before = null, List<string> tags = null, int? limit = null, 
            User user = null
        )
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
                    { "uuids", uuids },
                    { "tags", tags },
                },
                user: user
            );
            List<StaticBrcode> staticBrcode = new List<StaticBrcode>();
            foreach (SubResource subResource in page)
            {
                staticBrcode.Add(subResource as StaticBrcode);
            }
            return (staticBrcode, pageCursor);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "StaticBrcode", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string keyID = json.keyId;
            string city = json.city;
            long? amount = json.amount;
            string reconciliationID = json.reconciliationId;
            string cashierBankCode = json.cashierBankCode;
            string description = json.description;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string id = json.id;
            string uuid = json.uuid;
            string url = json.url;
            string createdString = json.created;
            DateTime? created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = Checks.CheckDateTime(updatedString);

            return new StaticBrcode( 
                name: name, keyID: keyID, city: city, amount: amount, 
                reconciliationID: reconciliationID, cashierBankCode: cashierBankCode, 
                description: description,tags: tags, id: id, uuid: uuid, url: url, 
                created: created, updated: updated
            );
        }
    }
}
