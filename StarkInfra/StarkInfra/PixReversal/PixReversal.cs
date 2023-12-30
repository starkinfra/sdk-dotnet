using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using StarkInfra.Utils;
using Newtonsoft.Json;

namespace StarkInfra
{
    /// <summary>
    /// PixReversal object
    /// <br/>
    /// PixReversals are instant payments used to revert PixRequests. You can only revert inbound PixRequests.
    /// <br/>
    /// When you initialize a PixReversal, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [integer]: amount in cents to be reversed from PixRequest. ex: 1234 (= R$ 12.34)</item>
    ///     <item>ExternalID [string]: string that must be unique among all your PixReversals. Duplicated external IDs will cause failures. By default, this parameter will block any PixReversal that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
    ///     <item>EndToEndID [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
    ///     <item>Reason [string]: reason why the PixRequest is being reversed. Options are "bankError", "fraud", "chashierError", "customerRequest"</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for PixReversals. ex: new List<string>{ "employees", "monthly" }</item>
    ///     <item>ID [string]: unique id returned when the PixReversal is created. ex: "5656565656565656".</item>
    ///     <item>ReturnID [string]: central bank's unique reversal transaction ID. ex: "D20018183202202030109X3OoBHG74wo".</item>
    ///     <item>Fee [string]: fee charged by this PixReversal. ex: 200 (= R$ 2.00)</item>
    ///     <item>Status [string]: current PixReversal status. ex: "created", "processing", "success", “failed"</item>
    ///     <item>Flow [string]: direction of money flow. ex: "in" or "out"</item>
    ///     <item>Created [DateTime]: creation DateTime for the PixReversal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the PixReversal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
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
        public long? Fee { get; }
        public string Status { get; }
        public string Flow { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixReversal object
        /// <br/>
        /// PixReversals are instant payments used to revert PixRequests. You can only revert inbound PixRequests.
        /// <br/>
        /// When you initialize a PixReversal, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [integer]: amount in cents to be reversed from PixRequest. ex: 1234 (= R$ 12.34)</item>
        ///     <item>externalID [string]: string that must be unique among all your PixReversals. Duplicated external IDs will cause failures. By default, this parameter will block any PixReversal that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
        ///     <item>endToEndID [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
        ///     <item>reason [string]: reason why the PixRequest is being reversed. Options are "bankError", "fraud", "chashierError", "customerRequest"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for PixReversals. ex: new List<string>{ "employees", "monthly" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixReversal is created. ex: "5656565656565656".</item>
        ///     <item>returnID [string]: central bank's unique reversal transaction ID. ex: "D20018183202202030109X3OoBHG74wo".</item>
        ///     <item>fee [string]: fee charged by this PixReversal. ex: 200 (= R$ 2.00)</item>
        ///     <item>status [string]: current PixReversal status. ex: "registered" or "paid"</item>
        ///     <item>flow [string]: direction of money flow. ex: "in" or "out"</item>
        ///     <item>created [DateTime]: creation DateTime for the PixReversal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the PixReversal. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixReversal(long amount, string externalID, string endToEndID, string reason, List<string> tags = null,  
            string id = null, string returnID = null, long? fee = null, string status = null, string flow = null, 
            DateTime? created = null, DateTime? updated = null) : base(id)
        {
            Amount = amount;
            ExternalID = externalID;
            EndToEndID = endToEndID;
            Reason = reason;
            Tags = tags;
            ReturnID = returnID;
            Fee = fee;
            Status = status;
            Flow = flow ;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create PixReversal objects
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
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: reversals,
                user: user
            ).ToList().ConvertAll(o => (PixReversal)o);
        }

        /// <summary>
        /// Create PixReversal objects
        /// <br/>
        /// Send a list of PixReversal objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>reversals [list of Dictionaries]: list of dictionaries representing the PixReversal objects to be created in the API</item>
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
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: reversals,
                user: user
            ).ToList().ConvertAll(o => (PixReversal)o);
        }

        /// <summary>
        /// Retrieve a specific PixReversal by its id
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
        ///     <item>PixReversal object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static PixReversal Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixReversal;
        }

        /// <summary>
        /// Retrieve PixReversal objects
        /// <br/>
        /// Receive an IEnumerable of PixReversal objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>returnIds [list of strings, default null]: central bank's unique reversal transaction IDs. ex: new List<string>{ "D20018183202202030109X3OoBHG74wo", "D20018183202202030109X3OoBhfkg7h" }</item>
        ///     <item>externalIds [list of strings, default null]: url safe strings that must be unique among all your PixReversals. Duplicated external IDs will cause failures. By default, this parameter will block any PixReversals that repeats amount and receiver information on the same date. ex: new List<string>{ "my-internal-id-123456", "my-internal-id-654321" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixReversal objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixReversal> Query(
            int? limit = null, DateTime? after = null, DateTime? before = null, 
            List<string> status = null, List<string> ids = null, List<string> returnIds = null, 
            List<string> externalIds = null, List<string> tags = null,  User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "ids", ids },
                    { "returnIds", returnIds },
                    { "externalIds", externalIds },
                    { "tags", tags }
                },
                user: user
            ).Cast<PixReversal>();
        }

        /// <summary>
        /// Retrieve paged PixReversal objects
        /// <br/>
        /// Receive a list of up to 100 PixReversal objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>returnIds [list of strings, default null]: central bank's unique reversal transaction IDs. ex: new List<string>{ "D20018183202202030109X3OoBHG74wo", "D20018183202202030109X3OoBhfkg7h" }</item>
        ///     <item>externalIds [list of strings, default null]: url safe strings that must be unique among all your PixReversals. Duplicated external IDs will cause failures. By default, this parameter will block any PixReversals that repeats amount and receiver information on the same date. ex: new List<string>{ "my-internal-id-123456", "my-internal-id-654321" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixReversal objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixReversal objects</item>
        /// </list>
        /// </summary>
        public static (List<PixReversal> page, string pageCursor) Page(
            string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null, 
            List<string> status = null, List<string> tags = null, List<string> ids = null, 
            List<string> returnIds = null, List<string> externalIds = null, User user = null
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
                    { "ids", ids },
                    { "returnIds", returnIds },
                    { "externalIds", externalIds },
                    { "tags", tags }
                },
                user: user
            );
            List<PixReversal> reversals = new List<PixReversal>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                reversals.Add(subResource as PixReversal);
            }
            return (reversals, pageCursor);
        }
        
        /// <summary>
        /// Create single verified PixReversal object from a content string
        /// <br/>
        /// Create a single PixReversal object from a content string received from a handler listening at
        /// the request url. If the provided digital signature does not check out with the StarkInfra
        /// public key, a Error.InvalidSignatureError will be raised.
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
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Parse.ParseAndVerify(
                content: content,
                signature: signature,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                user: user
            ) as PixReversal;
        }

        /// <summary>
        /// Helps you respond to a PixReversal authorization
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>status [string]: response to the authorization. Options: "approved" or "denied"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>reason [string, default null]: denial reason. Options: "invalidAccountNumber", "blockedAccount", "accountClosed", "invalidAccountType", "invalidTransactionType", "taxIDMismatch", "invalidTaxID", "orderRejected", "reversalTimeExpired", "settlementFailed"</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Dumped JSON string that must be returned to us</item>
        /// </list>
        /// </summary>
        public static string Response(string status, string reason = null)
        {
            Dictionary<string, object> rawResponse = new Dictionary<string, object>
            {
                {"authorization", new Dictionary<string, object>
                    {
                        {"status", status},
                        {"reason", reason},
                    }
                }
            };
            Dictionary<string, object> response = StarkCore.Utils.Api.CastJsonToApiFormat(rawResponse);
            return JsonConvert.SerializeObject(response);
        }
        
        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixReversal", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string externalID = json.externalId is null ? "" : json.externalId;
            string endToEndID = json.endToEndId;
            string reason = json.reason;
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            string id = json.id;
            string returnID = json.returnId;
            long fee = json.fee is null ? 0 : json.fee;
            string status = json.status;
            string flow = json.flow;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixReversal(
                amount: amount, externalID: externalID, endToEndID: endToEndID, 
                reason: reason, tags: tags, id: id, returnID: returnID, fee: fee, 
                status: status, flow: flow , created: created, updated: updated 
            );
        }
    }
}
