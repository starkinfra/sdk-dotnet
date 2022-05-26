using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixReversal object
    /// <br/>
    /// When you initialize a PixReversal, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [integer]: amount in cents to be reversed from PixRequest. ex: 1234 (= R$ 12.34)</item>
    ///     <item>ExternalID [string]: url safe string that must be unique among all your PixReversals. Duplicated external IDs will cause failures. By default, this parameter will block any PixReversal that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
    ///     <item>EndToEndID [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
    ///     <item>Reason [string]: reason why the PixRequest is being reversed. Options are "bankError", "fraud", "chashierError", "customerRequest"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for PixReversals. ex: ["employees", "monthly"]</item>
    ///     <item>ID [string]: unique id returned when the PixReversal is created. ex: "5656565656565656".</item>
    ///     <item>ReturnID [string]: central bank's unique reversal transaction ID. ex: "D20018183202202030109X3OoBHG74wo".</item>
    ///     <item>BankCode [string]: code of the bank institution in Brazil. ex: "20018183" or "341"</item>
    ///     <item>Fee [string]: fee charged by this PixReversal. ex: 200 (= R$ 2.00)</item>
    ///     <item>Status [string]: current PixReversal status. ex: "registered" or "paid"</item>
    ///     <item>Flow [string]: direction of money flow. ex: "in" or "out"</item>
    ///     <item>Created [DateTime]: creation datetime for the PixReversal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the PixReversal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixReversal : Utils.Resource
    {
        public long Amount { get; }
        public string ExternalID { get; }
        public string EndToEndID { get; }
        public string Reason { get; }
        public List<string> Tags { get; }
        public string ReturnID { get; }
        public string BankCode { get; }
        public long? Fee { get; }
        public string Status { get; }
        public string Flow { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixReversal object
        /// <br/>
        /// When you initialize a PixReversal, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [integer]: amount in cents to be reversed from PixRequest. ex: 1234 (= R$ 12.34)</item>
        ///     <item>externalId [string]: url safe string that must be unique among all your PixReversals. Duplicated external IDs will cause failures. By default, this parameter will block any PixReversal that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
        ///     <item>endToEndId [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
        ///     <item>reason [string]: reason why the PixRequest is being reversed. Options are "bankError", "fraud", "chashierError", "customerRequest"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for PixReversals. ex: ["employees", "monthly"]</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixReversal is created. ex: "5656565656565656".</item>
        ///     <item>returnId [string]: central bank's unique reversal transaction ID. ex: "D20018183202202030109X3OoBHG74wo".</item>
        ///     <item>bankCode [string]: code of the bank institution in Brazil. ex: "20018183" or "341"</item>
        ///     <item>fee [string]: fee charged by this PixReversal. ex: 200 (= R$ 2.00)</item>
        ///     <item>status [string]: current PixReversal status. ex: "registered" or "paid"</item>
        ///     <item>flow [string]: direction of money flow. ex: "in" or "out"</item>
        ///     <item>created [DateTime]: creation datetime for the PixReversal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the PixReversal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixReversal(long amount, string externalId, string endToEndId, string reason, List<string> tags = null,  
            string id = null, string returnId = null, string bankCode = null, long? fee = null, string status = null, 
            string flow = null, DateTime? created = null, DateTime? updated = null) : base(id)
        {
            Amount = amount;
            ExternalID = externalId;
            EndToEndID = endToEndId;
            Reason = reason;
            Tags = tags;
            ReturnID = returnId;
            BankCode = bankCode;
            Fee = fee;
            Status = status;
            Flow = flow ;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create PixReversals
        /// <br/>
        /// Send a list of PixReversal objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>reversals [list of PixReversal objects]: list of PixReversal objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixReversal objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixReversal> Create(List<PixReversal> reversals, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: reversals,
                user: user
            ).ToList().ConvertAll(o => (PixReversal)o);
        }

        /// <summary>
        /// Create PixReversals
        /// <br/>
        /// Send a list of PixReversal objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>reversals [list of dictionaries]: list of dictionaries representing the PixReversals to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixReversal objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixReversal> Create(List<Dictionary<string, object>> reversals, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: reversals,
                user: user
            ).ToList().ConvertAll(o => (PixReversal)o);
        }

        /// <summary>
        /// Retrieve a specific PixReversal
        /// <br/>
        /// Receive a single PixReversal object previously created in the Stark Infra API by passing its id
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
        ///     <item>PixReversal object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixReversal Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixReversal;
        }

        /// <summary>
        /// Retrieve PixReversals
        /// <br/>
        /// Receive an IEnumerable of PixReversal objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>fields [list of strings, default null]: parameters to be retrieved from PixReversal objects. ex: ["amount", "id"]</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>endToEndIds [list of strings, default null]: central bank's unique transaction IDs. ex: ["D20018183202202030109X3OoBHG74wo", "D20018183202202030109X3OoBhfkg7h"]</item>
        ///     <item>externalIds [list of strings, default null]: url safe strings that must be unique among all your PixReversals. Duplicated external IDs will cause failures. By default, this parameter will block any PixReversals that repeats amount and receiver information on the same date. ex: ["my-internal-id-123456", "my-internal-id-654321"]</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixReversal objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixReversal> Query(List<string>  fields = null, int? limit = null, 
            DateTime? after = null, DateTime? before = null, string status = null, List<string> tags = null, 
            List<string> ids = null, List<string> returnIds = null, List<string> externalIds = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "fields", fields },
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids },
                    { "returnIds", returnIds },
                    { "externalIds", externalIds }
                },
                user: user
            ).Cast<PixReversal>();
        }

        /// <summary>
        /// Retrieve paged PixReversals
        /// <br/>
        /// Receive a list of up to 100 PixReversal objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>fields [list of strings, default null]: parameters to be retrieved from PixReversal objects. ex: ["amount", "id"]</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>returnIds [list of strings, default null]: central bank's unique transaction IDs. ex: ["D20018183202202030109X3OoBHG74wo", "D20018183202202030109X3OoBhfkg7h"]</item>
        ///     <item>externalIds [list of strings, default null]: url safe strings that must be unique among all your PixReversals. Duplicated external IDs will cause failures. By default, this parameter will block any PixReversals that repeats amount and receiver information on the same date. ex: ["my-internal-id-123456", "my-internal-id-654321"]</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixReversal objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixReversal objects</item>
        /// </list>
        /// </summary>
        public static (List<PixReversal> page, string pageCursor) Page(string cursor = null, List<string>  fields = null, 
            int? limit = null, DateTime? after = null, DateTime? before = null, string status = null, 
            List<string> tags = null, List<string> ids = null, List<string> returnIds = null, 
            List<string> externalIds = null, User user = null)
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
                    { "tags", tags },
                    { "ids", ids },
                    { "returnIds", returnIds },
                    { "externalIds", externalIds }
                },
                user: user
            );
            List<PixReversal> reversals = new List<PixReversal>();
            foreach (SubResource subResource in page)
            {
                reversals.Add(subResource as PixReversal);
            }
            return (reversals, pageCursor);
        }
        
        /// <summary>
        ///  Create single verified PixReversal object from a content string
        /// <br/>
        /// Create a single PixReversal object from a content string received from a handler listening at
        /// the request url. If the provided digital signature does not check out with the StarkInfra
        /// public key, a starkinfra.error.InvalidSignatureError will be raised.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>content [string]: response content from request received at user endpoint (not parsed)</item>
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
        ///     <item>Parsed PixReversal object</item>
        /// </list>
        /// </summary>
        public static PixReversal Parse(string content, string signature, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Parse.ParseAndVerify(
                content: content,
                signature: signature,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                user: user
            ) as PixReversal;
        }
        
        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixReversal", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string externalId = json.externalId;
            string endToEndId = json.endToEndId;
            string reason = json.reason;
            List<string> tags = json.tags.ToObject<List<string>>();
            string id = json.id;
            string returnId = json.returnId;
            string bankCode = json.bankCode;
            long fee = json.fee;
            string status = json.status;
            string flow = json.flow ;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new PixReversal(
                amount: amount, externalId: externalId, endToEndId: endToEndId, reason: reason, tags: tags, id: id,
                returnId: returnId, bankCode: bankCode, fee: fee, status: status, flow: flow , created: created, 
                updated: updated );
        }
    }
}
