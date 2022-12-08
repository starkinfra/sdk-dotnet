using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixChargeback object
    /// <br/>
    /// A Pix Chargeback can be created when fraud is detected on a transaction or a system malfunction
    /// results in an erroneous transaction.
    /// It notifies another participant of your request to reverse the payment  they have received.
    /// <br/>
    /// When you initialize a PixChargeback, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long]: amount in cents to be reversed. ex: 11234 (= R$ 112.34)</item>
    ///     <item>ReferenceID [string]: endToEndID or returnID of the transaction to be reversed. ex: "E20018183202201201450u34sDGd19lz"</item>
    ///     <item>Reason [string]: reason why the reversal was requested. Options: "fraud", "flaw", "reversalChargeback"</item>
    ///     <item>Description [string, default null]: description for the PixChargeback.</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID [string]: unique id returned when the PixChargeback is created. ex: "5656565656565656"</item>
    ///     <item>Analysis [string]: analysis that led to the result.</item>
    ///     <item>SenderBankCode [string]: bankCode of the Pix participant that created the PixChargeback. ex: "20018183"</item>
    ///     <item>ReceiverBankCode [string]: bankCode of the Pix participant that received the PixChargeback. ex: "20018183"</item>
    ///     <item>RejectionReason [string]: reason for the rejection of the PixChargeback. Options: "noBalance", "accountClosed", "unableToReverse"</item>
    ///     <item>ReversalReferenceID [string]: returnID or endToEndID of the reversal transaction. ex: "D20018183202202030109X3OoBHG74wo".</item>
    ///     <item>Result [string]: result after the analysis of the PixChargeback by the receiving party. Options: "rejected", "accepted", "partiallyAccepted"</item>
    ///     <item>Flow [string]: direction of the Pix Chargeback. Options: "in" for received chargebacks, "out" for chargebacks you requested</item>
    ///     <item>Status [string]: current PixChargeback status. Options: "created", "failed", "delivered", "closed", "canceled".</item>
    ///     <item>Created [DateTime]: creation DateTime for the PixChargeback. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the PixChargeback. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixChargeback : Resource
    {
        public long Amount { get; }
        public string ReferenceID { get; }
        public string Reason { get; }
        public string Description { get; }
        public List<string> Tags { get; }
        public string Analysis { get; }
        public string SenderBankCode { get; }
        public string ReceiverBankCode { get; }
        public string RejectionReason { get; }
        public string ReversalReferenceID { get; }
        public string Result { get; }
        public string Flow { get; }
        public string Status { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixChargeback object
        /// <br/>
        /// A Pix chargeback can be created when fraud is detected on a transaction or a system malfunction
        /// results in an erroneous transaction.
        /// It notifies another participant of your request to reverse the payment they have received.
        /// <br/>
        /// When you initialize a PixChargeback, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long]: amount in cents to be reversed. ex: 11234 (= R$ 112.34)</item>
        ///     <item>referenceID [string]: endToEndID or returnID of the transaction to be reversed. ex: "E20018183202201201450u34sDGd19lz"</item>
        ///     <item>reason [string]: reason why the reversal was requested. Options: "fraud", "flaw", "reversalChargeback"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>description [string, default null]: description for the PixChargeback.</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixChargeback is created. ex: "5656565656565656"</item>
        ///     <item>analysis [string]: analysis that led to the result.</item>
        ///     <item>senderBankCode [string]: bankCode of the Pix participant that created the PixChargeback. ex: "20018183"</item>
        ///     <item>receiverBankCode [string]: bankCode of the Pix participant that received the PixChargeback. ex: "20018183"</item>
        ///     <item>rejectionReason [string]: reason for the rejection of the PixChargeback. Options: "noBalance", "accountClosed", "unableToReverse"</item>
        ///     <item>reversalReferenceID [string]: returnID or endToEndID of the reversal transaction. ex: "D20018183202202030109X3OoBHG74wo".</item>
        ///     <item>result [string]: result after the analysis of the PixChargeback by the receiving party. Options: "rejected", "accepted", "partiallyAccepted"</item>
        ///     <item>flow [string]: direction of the Pix Chargeback. Options: "in" for received chargebacks, "out" for chargebacks you requested</item>
        ///     <item>status [string]: current PixChargeback status. Options: "created", "failed", "delivered", "closed", "canceled".</item>
        ///     <item>created [DateTime]: creation DateTime for the PixChargeback. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the PixChargeback. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixChargeback(
            long amount, string referenceID, string reason, string description = null, 
            List<string> tags = null, string id = null, string analysis = null, 
            string senderBankCode = null, string receiverBankCode = null, 
            string rejectionReason = null, string reversalReferenceID = null, 
            string result = null, string flow = null, string status = null, 
            DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Amount = amount;
            ReferenceID = referenceID;
            Reason = reason;
            Description = description;
            Tags = tags;
            Analysis = analysis;
            SenderBankCode = senderBankCode;
            ReceiverBankCode = receiverBankCode;
            RejectionReason = rejectionReason;
            ReversalReferenceID = reversalReferenceID;
            Result = result;
            Flow = flow;
            Status = status;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create PixChargeback objects
        /// <br/>
        /// Create PixChargeback objects in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>chargebacks [list of PixChargeback objects]: list of PixChargeback objects to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixChargeback objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixChargeback> Create(List<PixChargeback> chargebacks, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: chargebacks,
                user: user
            ).ToList().ConvertAll(o => (PixChargeback)o);
        }

        /// <summary>
        /// Create PixChargeback objects
        /// <br/>
        /// Create PixChargeback objects in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>chargebacks [list of Dictionaries]: list of Dictionaries representing the PixChargeback to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixChargeback objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixChargeback> Create(List<Dictionary<string, object>> chargebacks, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: chargebacks,
                user: user
            ).ToList().ConvertAll(o => (PixChargeback)o);
        }

        /// <summary>
        /// Retrieve a PixChargeback object
        /// <br/>
        /// Retrieve a PixChargeback object linked to your Workspace in the Stark Infra API using its id.
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
        ///     <item>PixChargeback object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static PixChargeback Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixChargeback;
        }

        /// <summary>
        /// Retrieve PixChargeback objects
        /// <br/>
        /// Receive an IEnumerable of PixChargeback objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. Options: "created", "failed", "delivered", "closed", "canceled"</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>flow [string, default null]: direction of the Pix Chargeback. Options: "in" for received chargebacks, "out" for chargebacks you requested</item>
        ///     <item>tags [list of strings, default null]: filter for tags of retrieved objects. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixChargeback objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixChargeback> Query(
            int? limit = null, DateTime? after = null, DateTime? before = null, 
            string status = null, List<string> ids = null, string flow = null, 
            List<string> tags = null, User user = null
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
                    { "ids", ids },
                    { "flow", flow },
                    { "tags", tags }
                },
                user: user
            ).Cast<PixChargeback>();
        }

        /// <summary>
        /// Retrieve paged PixChargeback objects
        /// <br/>
        /// Receive a list of up to 100 PixChargeback objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. Options: "created", "failed", "delivered", "closed", "canceled"</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>flow [string, default null]: direction of the Pix Chargeback. Options: "in" for received chargebacks, "out" for chargebacks you requested</item>
        ///     <item>tags [list of strings, default null]: filter for tags of retrieved objects. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixChargeback objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixChargeback objects</item>
        /// </list>
        /// </summary>
        public static (List<PixChargeback> page, string pageCursor) Page(
            string cursor = null, int? limit = null, DateTime? after = null, 
            DateTime? before = null, string status = null, List<string> ids = null, 
            string flow = null, List<string> tags = null, User user = null
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
                    { "status", status },
                    { "ids", ids },
                    { "flow", flow },
                    { "tags", tags }
                },
                user: user
            );
            List<PixChargeback> chargebacks = new List<PixChargeback>();
            foreach (SubResource subResource in page)
            {
                chargebacks.Add(subResource as PixChargeback);
            }
            return (chargebacks, pageCursor);
        }

        /// <summary>
        /// Update PixChargeback entity
        /// <br/>
        /// Update a PixChargeback by passing id.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        ///     <item>result [string]: result after the analysis of the PixChargeback. Options: "rejected", "accepted", "partiallyAccepted".</item>
        ///     <item>patchData [dictionary]: Dictionary of optional and conditionally required parameters</item>
        ///     <list>
        ///         <item>rejectionReason[string, default null]: if the PixChargeback is rejected a rejectionReason is required. Options: "noBalance", "accountClosed", "unableToReverse"</item>
        ///         <item>reversalReferenceID[string, default null]: if the PixChargeback is accepted a reversalReferenceID is required. ex: "D20018183202201201450u34sDGd19lz"</item>
        ///     </list>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>patchData [dictionary]: Dictionary of optional and conditionally required parameters</item>
        ///     <list>
        ///         <item>analysis[string, default null]: description of the analysis that led to the result.</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings. User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixChargeback object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixChargeback Update(string id, string result, Dictionary<string, object> patchData, User user = null)
        {
            patchData.Add("result", result);
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as PixChargeback;
        }

        /// <summary>
        /// Cancel a PixChargeback entity
        /// <br/>
        /// Cancel a PixChargeback entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Organization/Project object, default null]: Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Canceled PixChargeback object</item>
        /// </list>
        /// </summary>
        public static PixChargeback Cancel(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixChargeback;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixChargeback", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string referenceID = json.referenceId;
            string reason = json.reason;
            string description = json.description;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string analysis = json.analysis;
            string senderBankCode = json.senderBankCode;
            string receiverBankCode = json.receiverBankCode;
            string rejectionReason = json.rejectionReason;
            string reversalReferenceID = json.reversalReferenceId;
            string result = json.result;
            string flow = json.flow;
            string status = json.status;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new PixChargeback(
                id: id, amount: amount, referenceID: referenceID, reason: reason, description: description,
                tags: tags, analysis: analysis, senderBankCode: senderBankCode, receiverBankCode: receiverBankCode,
                rejectionReason: rejectionReason, reversalReferenceID: reversalReferenceID, result: result, 
                status: status, flow: flow, updated: updated, created: created
            );
        }
    }
}
