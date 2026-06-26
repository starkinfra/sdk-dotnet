using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixPullRequest object
    /// <br/>
    /// A PixPullRequest is a command sent to the payer's bank to trigger the automatic debit linked to an active PixPullSubscription.
    /// When you initialize a PixPullRequest, the entity will not be automatically created in the Stark Infra API.
    /// The 'create' function sends the objects to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long]: amount in cents to be pulled. ex: 11234 (= R$ 112.34)</item>
    ///     <item>Due [DateTime]: settlement datetime for the debit. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>EndToEndID [string]: central bank's unique transaction id. ex: "E79457883202101262140HHX553UPqeq"</item>
    ///     <item>ReceiverAccountNumber [string]: receiver bank account number. ex: "876543-2"</item>
    ///     <item>ReceiverAccountType [string]: receiver bank account type. ex: "checking", "savings", "salary" or "payment"</item>
    ///     <item>ReceiverBankCode [string]: receiver bank code. ex: "20018183"</item>
    ///     <item>ReconciliationID [string]: reconciliation id linked to this payment. ex: "b77f5236-7ab9-4487-9f95-66ee6eaf1781"</item>
    ///     <item>SubscriptionID [string]: id of the PixPullSubscription that triggered this PixPullRequest. ex: "5656565656565656"</item>
    ///     <item>AttemptType [string]: pull attempt type. ex: "default", "retry"</item>
    ///     <item>Description [string]: free text description of the payment. ex: "Payment for service rendered"</item>
    ///     <item>ReceiverBranchCode [string]: receiver bank account branch code. ex: "1357-9"</item>
    ///     <item>Tags [list of strings]: list of strings for reference when searching for PixPullRequests. ex: new List<string>{ "employees", "monthly" }</item>
    ///     <item>Status [string]: current PixPullRequest status. Options: "created", "processing", "scheduled", "denied", "success", "canceled", "expired"</item>
    ///     <item>Flow [string]: direction of money flow. Options: "in", "out"</item>
    ///     <item>ReceiverName [string]: receiver full name. ex: "Anthony Edward Stark"</item>
    ///     <item>ReceiverTaxID [string]: receiver tax id (CPF/CNPJ). ex: "01234567890"</item>
    ///     <item>SenderBankCode [string]: sender bank code. ex: "20018183"</item>
    ///     <item>SenderFinalName [string]: sender final beneficiary full name. ex: "Anthony Edward Stark"</item>
    ///     <item>SenderTaxID [string]: sender tax id (CPF/CNPJ). ex: "01234567890"</item>
    ///     <item>SubscriptionBacenID [string]: central bank's unique subscription id. ex: "RR2017032900000000000000000A"</item>
    ///     <item>Created [DateTime]: creation datetime for the PixPullRequest. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the PixPullRequest. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixPullRequest : Utils.Resource
    {
        public long? Amount { get; }
        public DateTime? Due { get; }
        public string EndToEndID { get; }
        public string ReceiverAccountNumber { get; }
        public string ReceiverAccountType { get; }
        public string ReceiverBankCode { get; }
        public string ReconciliationID { get; }
        public string SubscriptionID { get; }
        public string AttemptType { get; }
        public string Description { get; }
        public string ReceiverBranchCode { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public string Flow { get; }
        public string ReceiverName { get; }
        public string ReceiverTaxID { get; }
        public string SenderBankCode { get; }
        public string SenderFinalName { get; }
        public string SenderTaxID { get; }
        public string SubscriptionBacenID { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixPullRequest object
        /// <br/>
        /// A PixPullRequest is a command sent to the payer's bank to trigger the automatic debit linked to an active PixPullSubscription.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long]: amount in cents to be pulled. ex: 11234 (= R$ 112.34)</item>
        ///     <item>due [DateTime]: settlement datetime for the debit. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>endToEndID [string]: central bank's unique transaction id. ex: "E79457883202101262140HHX553UPqeq"</item>
        ///     <item>receiverAccountNumber [string]: receiver bank account number. ex: "876543-2"</item>
        ///     <item>receiverAccountType [string]: receiver bank account type. ex: "checking", "savings", "salary" or "payment"</item>
        ///     <item>receiverBankCode [string]: receiver bank code. ex: "20018183"</item>
        ///     <item>reconciliationID [string]: reconciliation id linked to this payment. ex: "b77f5236-7ab9-4487-9f95-66ee6eaf1781"</item>
        ///     <item>subscriptionID [string]: id of the PixPullSubscription that triggered this PixPullRequest. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>attemptType [string, default null]: pull attempt type. ex: "default", "retry"</item>
        ///     <item>description [string, default null]: free text description of the payment. ex: "Payment for service rendered"</item>
        ///     <item>receiverBranchCode [string, default null]: receiver bank account branch code. ex: "1357-9"</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for PixPullRequests. ex: new List<string>{ "employees", "monthly" }</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixPullRequest is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current PixPullRequest status. Options: "created", "processing", "scheduled", "denied", "success", "canceled", "expired"</item>
        ///     <item>flow [string]: direction of money flow. Options: "in", "out"</item>
        ///     <item>receiverName [string]: receiver full name. ex: "Anthony Edward Stark"</item>
        ///     <item>receiverTaxID [string]: receiver tax id (CPF/CNPJ). ex: "01234567890"</item>
        ///     <item>senderBankCode [string]: sender bank code. ex: "20018183"</item>
        ///     <item>senderFinalName [string]: sender final beneficiary full name. ex: "Anthony Edward Stark"</item>
        ///     <item>senderTaxID [string]: sender tax id (CPF/CNPJ). ex: "01234567890"</item>
        ///     <item>subscriptionBacenID [string]: central bank's unique subscription id. ex: "RR2017032900000000000000000A"</item>
        ///     <item>created [DateTime]: creation datetime for the PixPullRequest. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the PixPullRequest. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixPullRequest(long? amount, DateTime? due, string endToEndID, string receiverAccountNumber,
            string receiverAccountType, string receiverBankCode, string reconciliationID, string subscriptionID,
            string attemptType = null, string description = null, string receiverBranchCode = null,
            List<string> tags = null, string id = null, string status = null, string flow = null,
            string receiverName = null, string receiverTaxID = null, string senderBankCode = null,
            string senderFinalName = null, string senderTaxID = null, string subscriptionBacenID = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Amount = amount;
            Due = due;
            EndToEndID = endToEndID;
            ReceiverAccountNumber = receiverAccountNumber;
            ReceiverAccountType = receiverAccountType;
            ReceiverBankCode = receiverBankCode;
            ReconciliationID = reconciliationID;
            SubscriptionID = subscriptionID;
            AttemptType = attemptType;
            Description = description;
            ReceiverBranchCode = receiverBranchCode;
            Tags = tags;
            Status = status;
            Flow = flow;
            ReceiverName = receiverName;
            ReceiverTaxID = receiverTaxID;
            SenderBankCode = senderBankCode;
            SenderFinalName = senderFinalName;
            SenderTaxID = senderTaxID;
            SubscriptionBacenID = subscriptionBacenID;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create PixPullRequests
        /// <br/>
        /// Send a list of PixPullRequest objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of PixPullRequest objects]: list of PixPullRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixPullRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixPullRequest> Create(List<PixPullRequest> requests, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (PixPullRequest)o);
        }

        /// <summary>
        /// Retrieve a specific PixPullRequest by its id
        /// <br/>
        /// Receive a single PixPullRequest object previously created in the Stark Infra API by its id
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
        ///     <item>PixPullRequest object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static PixPullRequest Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixPullRequest;
        }

        /// <summary>
        /// Retrieve PixPullRequests
        /// <br/>
        /// Receive an IEnumerable of PixPullRequest objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created", "success" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>flows [list of strings, default null]: direction of money flow to filter retrieved objects. Options: "in", "out"</item>
        ///     <item>subscriptionIds [list of strings, default null]: list of PixPullSubscription ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixPullRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixPullRequest> Query(int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> status = null, List<string> tags = null,
            List<string> ids = null, List<string> flows = null, List<string> subscriptionIds = null, User user = null)
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
                    { "flows", flows },
                    { "subscriptionIds", subscriptionIds }
                },
                user: user
            ).Cast<PixPullRequest>();
        }

        /// <summary>
        /// Retrieve paged PixPullRequests
        /// <br/>
        /// Receive a list of up to 100 PixPullRequest objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created", "success" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>flows [list of strings, default null]: direction of money flow to filter retrieved objects. Options: "in", "out"</item>
        ///     <item>subscriptionIds [list of strings, default null]: list of PixPullSubscription ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixPullRequest objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixPullRequest objects</item>
        /// </list>
        /// </summary>
        public static (List<PixPullRequest> page, string pageCursor) Page(string cursor = null,
            int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> tags = null, List<string> ids = null,
            List<string> flows = null, List<string> subscriptionIds = null, User user = null)
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
                    { "ids", ids },
                    { "flows", flows },
                    { "subscriptionIds", subscriptionIds }
                },
                user: user
            );
            List<PixPullRequest> requests = new List<PixPullRequest>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                requests.Add(subResource as PixPullRequest);
            }
            return (requests, pageCursor);
        }

        /// <summary>
        /// Update a PixPullRequest
        /// <br/>
        /// Update a PixPullRequest by passing its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: PixPullRequest unique id. ex: "5656565656565656"</item>
        ///     <item>patchData [Dictionary<string, object>]: dictionary containing the attributes to be updated. ex: new Dictionary<string, object>{ { "status", "approved" }, { "senderCityCode", "3550308" } }
        ///         <list>
        ///             <item>Parameters (required):</item>
        ///             <item>status [string]: New status of the Pix Pull Request.</item>
        ///             <item>Parameters (conditionally required):</item>
        ///             <item>reason [string]: Reason why the Pix Pull Request is being denied. Options: "senderAccountClosed", "senderAccountBlocked", "amountNotAllowed"</item>
        ///         </list>
        ///     </item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target PixPullRequest with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixPullRequest Update(string id, Dictionary<string, object> patchData, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as PixPullRequest;
        }

        /// <summary>
        /// Cancel a PixPullRequest
        /// <br/>
        /// Cancel a PixPullRequest by passing its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: PixPullRequest unique id. ex: "5656565656565656"</item>
        ///     <item>reason [string]: reason why the PixPullRequest is being canceled. Options for the receiver: "accountClosed", "receiverOrganizationClosed", "receiverInternalError", "fraud", "receiverUserRequested". Options for the sender: "accountClosed", "senderDeceased", "fraud", "senderUserRequested"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled PixPullRequest object</item>
        /// </list>
        /// </summary>
        public static PixPullRequest Cancel(string id, string reason, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            string path = $"/{StarkCore.Utils.Api.Endpoint(resourceName)}/{id}";
            Dictionary<string, object> query = new Dictionary<string, object> { { "reason", reason } };
            StarkCore.Utils.Response response = Rest.DeleteRaw(
                payload: null,
                path: path,
                query: query,
                user: user
            );
            dynamic json = response.Json()[StarkCore.Utils.Api.LastName(resourceName)];
            return StarkCore.Utils.Api.FromApiJson(resourceMaker, json) as PixPullRequest;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixPullRequest", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            long? amount = json.amount;
            string dueString = json.due;
            DateTime? due = string.IsNullOrEmpty(dueString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(dueString);
            string endToEndID = json.endToEndId;
            string receiverAccountNumber = json.receiverAccountNumber;
            string receiverAccountType = json.receiverAccountType;
            string receiverBankCode = json.receiverBankCode;
            string reconciliationID = json.reconciliationId;
            string subscriptionID = json.subscriptionId;
            string attemptType = json.attemptType;
            string description = json.description;
            string receiverBranchCode = json.receiverBranchCode;
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string flow = json.flow;
            string receiverName = json.receiverName;
            string receiverTaxID = json.receiverTaxId;
            string senderBankCode = json.senderBankCode;
            string senderFinalName = json.senderFinalName;
            string senderTaxID = json.senderTaxId;
            string subscriptionBacenID = json.subscriptionBacenId;
            string createdString = json.created;
            DateTime? created = string.IsNullOrEmpty(createdString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = string.IsNullOrEmpty(updatedString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixPullRequest(
                amount: amount, due: due, endToEndID: endToEndID,
                receiverAccountNumber: receiverAccountNumber, receiverAccountType: receiverAccountType,
                receiverBankCode: receiverBankCode, reconciliationID: reconciliationID,
                subscriptionID: subscriptionID, attemptType: attemptType, description: description,
                receiverBranchCode: receiverBranchCode, tags: tags, id: id, status: status, flow: flow,
                receiverName: receiverName, receiverTaxID: receiverTaxID, senderBankCode: senderBankCode,
                senderFinalName: senderFinalName, senderTaxID: senderTaxID,
                subscriptionBacenID: subscriptionBacenID, created: created, updated: updated
            );
        }
    }
}
