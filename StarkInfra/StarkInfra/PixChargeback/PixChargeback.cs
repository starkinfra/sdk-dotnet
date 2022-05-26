using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixChargeback object
    /// <br/>
    /// Pix Chargebacks can be created when fraud is detected on a transaction or a system malfunction
    /// results in an erroneous transaction.
    /// It notifies another participant of your request to reverse the payment they have received.
    /// When you initialize a PixChargeback, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long]: amount in cents to be reversed. ex: 11234 (= R$ 112.34)</item>
    ///     <item>ReferenceID [string]: endToEndId or returnId of the transaction to be reversed. ex: "E20018183202201201450u34sDGd19lz"</item>
    ///     <item>Reason [string]: reason why the reversal was requested. Options: "fraud", "flaw", "reversalChargeback"</item>
    ///     <item>Description [string, default null]: description for the PixChargeback.</item>
    ///     <item>Analysis [string]: analysis that led to the result.</item>
    ///     <item>BacenID [string]: central bank's unique UUID that identifies the PixChargeback.</item>
    ///     <item>SenderBankCode [string]: bankCode of the Pix participant that created the PixChargeback. ex: "20018183"</item>
    ///     <item>ReceiverBankCode [string]: bankCode of the Pix participant that received the PixChargeback. ex: "20018183"</item>
    ///     <item>RejectionReason [string]: reason for the rejection of the PixChargeback. Options: "noBalance", "accountClosed", "unableToReverse"</item>
    ///     <item>ReversalReferenceID [string]: return id of the reversal transaction. ex: "D20018183202202030109X3OoBHG74wo".</item>
    ///     <item>ID [string]: unique id returned when the PixChargeback is created. ex: "5656565656565656"</item>
    ///     <item>Result [string]: result after the analysis of the PixChargeback by the receiving party. Options: "rejected", "accepted", "partiallyAccepted"</item>
    ///     <item>Status [string]: current PixChargeback status. Options: "created", "failed", "delivered", "closed", "canceled".</item>
    ///     <item>Created [DateTime]: creation datetime for the PixChargeback. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the PixChargeback. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixChargeback : Resource
    {
        public long Amount { get; }
        public string ReferenceID { get; }
        public string Reason { get; }
        public string Description { get; }
        public string Analysis { get; }
        public string BacenID { get; }
        public string SenderBankCode { get; }
        public string ReceiverBankCode { get; }
        public string RejectionReason { get; }
        public string ReversalReferenceID { get; }
        public string Result { get; }
        public string Status { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// PixChargeback object
        /// <br/>
        /// A Pix chargeback can be created when fraud is detected on a transaction or a system malfunction
        /// results in an erroneous transaction.
        /// It notifies another participant of your request to reverse the payment they have received.
        /// When you initialize a PixChargeback, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long]: amount in cents to be reversed. ex: 11234 (= R$ 112.34)</item>
        ///     <item>referenceId [string]: endToEndId or returnId of the transaction to be reversed. ex: "E20018183202201201450u34sDGd19lz"</item>
        ///     <item>reason [string]: reason why the reversal was requested. Options: "fraud", "flaw", "reversalChargeback"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>description [string, default null]: description for the PixChargeback.</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>analysis [string]: analysis that led to the result.</item>
        ///     <item>bacenId [string]: central bank's unique UUID that identifies the PixChargeback.</item>
        ///     <item>senderBankCode [string]: bankCode of the Pix participant that created the PixChargeback. ex: "20018183"</item>
        ///     <item>receiverBankCode [string]: bankCode of the Pix participant that received the PixChargeback. ex: "20018183"</item>
        ///     <item>rejectionReason [string]: reason for the rejection of the PixChargeback. Options: "noBalance", "accountClosed", "unableToReverse"</item>
        ///     <item>reversalReferenceId [string]: return id of the reversal transaction. ex: "D20018183202202030109X3OoBHG74wo".</item>
        ///     <item>id [string]: unique id returned when the PixChargeback is created. ex: "5656565656565656"</item>
        ///     <item>result [string]: result after the analysis of the PixChargeback by the receiving party. Options: "rejected", "accepted", "partiallyAccepted"</item>
        ///     <item>status [string]: current PixChargeback status. Options: "created", "failed", "delivered", "closed", "canceled".</item>
        ///     <item>created [DateTime]: creation datetime for the PixChargeback. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the PixChargeback. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixChargeback(long amount, string referenceId, string reason, string description = null, string analysis = null, string bacenId = null,
            string senderBankCode = null, string receiverBankCode = null, string rejectionReason = null, string reversalReferenceId = null,
            string result = null, string status = null, DateTime? updated = null, DateTime? created = null, string id = null
        ) : base(id)
        {
            Amount = amount;
            ReferenceID = referenceId;
            Reason = reason;
            Description = description;
            Analysis = analysis;
            BacenID = bacenId;
            SenderBankCode = senderBankCode;
            ReceiverBankCode = receiverBankCode;
            RejectionReason = rejectionReason;
            ReversalReferenceID = reversalReferenceId;
            Result = result;
            Status = status;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create PixChargebacks
        /// <br/>
        /// Send a list of PixChargeback objects for creation in the Stark Infra API
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
        /// Create PixChargebacks
        /// <br/>
        /// Send a list of PixChargeback dictionaries objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>chargebacks [list of dictionaries]: list of Dictionaries representing the PixChargeback to be created in the API</item>
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
        /// Retrieve a specific PixChargeback
        /// <br/>
        /// Receive a single PixChargeback object previously created in the Stark Infra API by passing its id
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
        ///     <item>PixChargeback object with updated attributes</item>
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
        /// Retrieve PixChargebacks
        /// <br/>
        /// Receive an IEnumerable of PixChargeback objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "failed", "delivered", "closed", "canceled"</item>
        ///     <item>after [DateTime or string, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545"]</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Unlimited if null. ex: 35D20018183202202030109X3OoBhfkg7h</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixChargeback objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixChargeback> Query(string status = null, DateTime? after = null, DateTime? before = null, 
            List<string> ids = null, int? limit = null,User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "status", status },
                    { "after", after },
                    { "before", before },
                    { "ids", ids },
                    { "limit", limit },
                    { "user", user }
                },
                user: user
            ).Cast<PixChargeback>();
        }

        /// <summary>
        /// Retrieve paged PixChargebacks
        /// <br/>
        /// Receive a list of up to 100 PixChargeback objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "failed", "delivered", "closed", "canceled"</item>
        ///     <item>after [DateTime or string, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Unlimited if null. ex: 35D20018183202202030109X3OoBhfkg7h</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixChargeback objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixChargeback objects</item>
        /// </list>
        /// </summary>
        public static (List<PixChargeback> page, string pageCursor) Page(string cursor = null, string status = null, DateTime? after = null, DateTime? before = null, 
            List<string> ids = null, int? limit = null,User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "status", status },
                    { "after", after },
                    { "before", before },
                    { "ids", ids },
                    { "limit", limit },
                    { "user", user }
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
        ///     <item>patchData [dictionary]: Dictionary of optional and conditionaly required parameters</item>
        ///     <list>
        ///         <item>rejectionReason[string, default null]: if the PixChargeback is rejected a rejectionReason is required. Options: "noBalance", "accountClosed", "unableToReverse"</item>
        ///         <item>reversalReferenceId[string, default null]: if the PixChargeback is accepted a reversalReferenceId is required. ex: "D20018183202201201450u34sDGd19lz"</item>
        ///         <item>analysis[string, default null]: description of the analysis that led to the result.</item>
        ///     </list>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings. User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target PixChargeback with updated attributes</item>
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
        ///     <item>id[string]: PixChargeback unique id. ex: "5656565656565656"</item>
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
            string referenceId = json.referenceId;
            string reason = json.reason;
            string description = json.description;
            string analysis = json.analysis;
            string bacenId = json.bacenId;
            string senderBankCode = json.senderBankCode;
            string receiverBankCode = json.receiverBankCode;
            string rejectionReason = json.rejectionReason;
            string reversalReferenceId = json.reversalReferenceId;
            string result = json.result;
            string status = json.status;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new PixChargeback(
                id: id, amount: amount, referenceId: referenceId, reason: reason, description: description,
                analysis: analysis, bacenId: bacenId, senderBankCode: senderBankCode, receiverBankCode: receiverBankCode,
                rejectionReason: rejectionReason, reversalReferenceId: reversalReferenceId, result: result, status: status,
                updated: updated, created: created
            );
        }
    }
}
