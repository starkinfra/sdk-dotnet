using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixPullSubscription object
    /// <br/>
    /// A PixPullSubscription is a recurring Pix debit authorization. It defines the frequency, amount, and required
    /// payer authorizations for a series of Pix debits to be pulled from the sender by the receiver.
    /// Each cycle of an active subscription is triggered by a PixPullRequest.
    /// When you initialize a PixPullSubscription, the entity will not be automatically created in the Stark Infra API.
    /// The 'create' function sends the objects to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>BacenID [string]: central bank's unique subscription id. ex: "RR2017032900000000000000000A"</item>
    ///     <item>ExternalID [string]: unique id of the subscription on your system. ex: "my-external-id"</item>
    ///     <item>InstallmentStart [DateTime]: datetime when the recurring debits start. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Interval [string]: frequency of the recurring debits. ex: "week", "month", "quarter", "semester", "year"</item>
    ///     <item>ReceiverName [string]: receiver full name. ex: "Anthony Edward Stark"</item>
    ///     <item>ReceiverTaxID [string]: receiver tax id (CPF/CNPJ). ex: "01234567890"</item>
    ///     <item>SenderAccountNumber [string]: sender bank account number. ex: "876543-2"</item>
    ///     <item>SenderBankCode [string]: sender bank code. ex: "20018183"</item>
    ///     <item>SenderBranchCode [string]: sender bank account branch code. ex: "1357-9"</item>
    ///     <item>SenderTaxID [string]: sender tax id (CPF/CNPJ). ex: "01234567890"</item>
    ///     <item>Type [string]: subscription type. Options: "push", "qrcode", "qrcodeAndPayment", "paymentAndOrQrcode"</item>
    ///     <item>Amount [long]: amount in cents to be pulled on each cycle. Required if the subscription has a fixed amount. ex: 11234 (= R$ 112.34)</item>
    ///     <item>AmountMinLimit [long]: minimum amount in cents allowed per cycle. Required if the subscription has a variable amount. ex: 100 (= R$ 1.00)</item>
    ///     <item>Description [string]: free text description of the subscription. ex: "Monthly subscription"</item>
    ///     <item>Due [DateTime]: subscription due datetime. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>InstallmentEnd [DateTime]: datetime when the recurring debits end. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>ReceiverBankCode [string]: receiver bank code. ex: "20018183"</item>
    ///     <item>ReferenceCode [string]: reference code of the subscription. ex: "ref-1234"</item>
    ///     <item>PullRetryLimit [long]: maximum number of retries allowed per cycle. ex: 3</item>
    ///     <item>SenderCityCode [string]: sender city code (IBGE). ex: "1100015"</item>
    ///     <item>SenderFinalName [string]: sender final beneficiary full name. ex: "Anthony Edward Stark"</item>
    ///     <item>SenderFinalTaxID [string]: sender final beneficiary tax id (CPF/CNPJ). ex: "01234567890"</item>
    ///     <item>Tags [list of strings]: list of strings for reference when searching for PixPullSubscriptions. ex: new List<string>{ "employees", "monthly" }</item>
    ///     <item>Status [string]: current PixPullSubscription status. Options: "created", "pending", "failed", "denied", "approved", "active", "expired", "canceled"</item>
    ///     <item>Flow [string]: direction of money flow. Options: "in", "out"</item>
    ///     <item>Created [DateTime]: creation datetime for the PixPullSubscription. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the PixPullSubscription. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixPullSubscription : Utils.Resource
    {
        public string BacenID { get; }
        public string ExternalID { get; }
        public DateTime? InstallmentStart { get; }
        public string Interval { get; }
        public string ReceiverName { get; }
        public string ReceiverTaxID { get; }
        public string SenderAccountNumber { get; }
        public string SenderBankCode { get; }
        public string SenderBranchCode { get; }
        public string SenderTaxID { get; }
        public string Type { get; }
        public long? Amount { get; }
        public long? AmountMinLimit { get; }
        public string Description { get; }
        public DateTime? Due { get; }
        public DateTime? InstallmentEnd { get; }
        public string ReceiverBankCode { get; }
        public string ReferenceCode { get; }
        public long? PullRetryLimit { get; }
        public string SenderCityCode { get; }
        public string SenderFinalName { get; }
        public string SenderFinalTaxID { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public string Flow { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixPullSubscription object
        /// <br/>
        /// A PixPullSubscription is a recurring Pix debit authorization.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>bacenID [string]: central bank's unique subscription id. ex: "RR2017032900000000000000000A"</item>
        ///     <item>externalID [string]: unique id of the subscription on your system. ex: "my-external-id"</item>
        ///     <item>installmentStart [DateTime]: datetime when the recurring debits start. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>interval [string]: frequency of the recurring debits. ex: "week", "month", "quarter", "semester", "year"</item>
        ///     <item>receiverName [string]: receiver full name. ex: "Anthony Edward Stark"</item>
        ///     <item>receiverTaxID [string]: receiver tax id (CPF/CNPJ). ex: "01234567890"</item>
        ///     <item>senderAccountNumber [string]: sender bank account number. ex: "876543-2"</item>
        ///     <item>senderBankCode [string]: sender bank code. ex: "20018183"</item>
        ///     <item>senderBranchCode [string]: sender bank account branch code. ex: "1357-9"</item>
        ///     <item>senderTaxID [string]: sender tax id (CPF/CNPJ). ex: "01234567890"</item>
        ///     <item>type [string]: subscription type. Options: "push", "qrcode", "qrcodeAndPayment", "paymentAndOrQrcode"</item>
        /// </list>
        /// <br/>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>amount [long, default null]: amount in cents to be pulled on each cycle. Required if the subscription has a fixed amount. ex: 11234 (= R$ 112.34)</item>
        ///     <item>amountMinLimit [long, default null]: minimum amount in cents allowed per cycle. Required if the subscription has a variable amount. ex: 100 (= R$ 1.00)</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>description [string, default null]: free text description of the subscription. ex: "Monthly subscription"</item>
        ///     <item>due [DateTime, default null]: subscription due datetime. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>installmentEnd [DateTime, default null]: datetime when the recurring debits end. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>receiverBankCode [string, default null]: receiver bank code. ex: "20018183"</item>
        ///     <item>referenceCode [string, default null]: reference code of the subscription. ex: "ref-1234"</item>
        ///     <item>pullRetryLimit [long, default null]: maximum number of retries allowed per cycle. ex: 3</item>
        ///     <item>senderCityCode [string, default null]: sender city code (IBGE). ex: "1100015"</item>
        ///     <item>senderFinalName [string, default null]: sender final beneficiary full name. ex: "Anthony Edward Stark"</item>
        ///     <item>senderFinalTaxID [string, default null]: sender final beneficiary tax id (CPF/CNPJ). ex: "01234567890"</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for PixPullSubscriptions. ex: new List<string>{ "employees", "monthly" }</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixPullSubscription is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current PixPullSubscription status. Options: "created", "pending", "failed", "denied", "approved", "active", "expired", "canceled"</item>
        ///     <item>flow [string]: direction of money flow. Options: "in", "out"</item>
        ///     <item>created [DateTime]: creation datetime for the PixPullSubscription. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the PixPullSubscription. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixPullSubscription(string bacenID, string externalID, DateTime? installmentStart, string interval,
            string receiverName, string receiverTaxID, string senderAccountNumber, string senderBankCode,
            string senderBranchCode, string senderTaxID, string type, long? amount = null, long? amountMinLimit = null,
            string description = null, DateTime? due = null, DateTime? installmentEnd = null,
            string receiverBankCode = null, string referenceCode = null, long? pullRetryLimit = null,
            string senderCityCode = null, string senderFinalName = null, string senderFinalTaxID = null,
            List<string> tags = null, string id = null, string status = null, string flow = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            BacenID = bacenID;
            ExternalID = externalID;
            InstallmentStart = installmentStart;
            Interval = interval;
            ReceiverName = receiverName;
            ReceiverTaxID = receiverTaxID;
            SenderAccountNumber = senderAccountNumber;
            SenderBankCode = senderBankCode;
            SenderBranchCode = senderBranchCode;
            SenderTaxID = senderTaxID;
            Type = type;
            Amount = amount;
            AmountMinLimit = amountMinLimit;
            Description = description;
            Due = due;
            InstallmentEnd = installmentEnd;
            ReceiverBankCode = receiverBankCode;
            ReferenceCode = referenceCode;
            PullRetryLimit = pullRetryLimit;
            SenderCityCode = senderCityCode;
            SenderFinalName = senderFinalName;
            SenderFinalTaxID = senderFinalTaxID;
            Tags = tags;
            Status = status;
            Flow = flow;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create PixPullSubscriptions
        /// <br/>
        /// Send a list of PixPullSubscription objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>subscriptions [list of PixPullSubscription objects]: list of PixPullSubscription objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixPullSubscription objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixPullSubscription> Create(List<PixPullSubscription> subscriptions, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: subscriptions,
                user: user
            ).ToList().ConvertAll(o => (PixPullSubscription)o);
        }

        /// <summary>
        /// Retrieve a specific PixPullSubscription by its id
        /// <br/>
        /// Receive a single PixPullSubscription object previously created in the Stark Infra API by its id
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
        ///     <item>PixPullSubscription object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static PixPullSubscription Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixPullSubscription;
        }

        /// <summary>
        /// Retrieve PixPullSubscriptions
        /// <br/>
        /// Receive an IEnumerable of PixPullSubscription objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "active", "created" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixPullSubscription objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixPullSubscription> Query(int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> status = null, List<string> tags = null,
            List<string> ids = null, User user = null)
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
                    { "ids", ids }
                },
                user: user
            ).Cast<PixPullSubscription>();
        }

        /// <summary>
        /// Retrieve paged PixPullSubscriptions
        /// <br/>
        /// Receive a list of up to 100 PixPullSubscription objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "active", "created" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixPullSubscription objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixPullSubscription objects</item>
        /// </list>
        /// </summary>
        public static (List<PixPullSubscription> page, string pageCursor) Page(string cursor = null,
            int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> tags = null, List<string> ids = null, User user = null)
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
                    { "ids", ids }
                },
                user: user
            );
            List<PixPullSubscription> subscriptions = new List<PixPullSubscription>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                subscriptions.Add(subResource as PixPullSubscription);
            }
            return (subscriptions, pageCursor);
        }

        /// <summary>
        /// Update a PixPullSubscription
        /// <br/>
        /// Update a PixPullSubscription by passing its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: PixPullSubscription unique id. ex: "5656565656565656"</item>
        ///     <item>patchData [Dictionary<string, object>]: dictionary containing the attributes to be updated. ex: new Dictionary<string, object>{ { "status", "approved" }, { "senderCityCode", "3550308" } }
        ///         <list>
        ///             <item>Parameters (required):</item>
        ///             <item>status [string]: New status of the Pix Subscription.</item>
        ///             <item>Parameters (conditionally required):</item>
        ///             <item>senderCityCode [string]: IBGE Code of the payer's city. Required if you are confirming the subscription.</item>
        ///             <item>reason [string]: Reason why the Pix Subscription is being patched. Options: "accountClosed", "accountBlocked", "invalidBranchCode", "notRecognizedBySender", "userRejected", "notOffered"</item>
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
        ///     <item>target PixPullSubscription with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixPullSubscription Update(string id, Dictionary<string, object> patchData, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as PixPullSubscription;
        }

        /// <summary>
        /// Cancel a PixPullSubscription
        /// <br/>
        /// Cancel a PixPullSubscription by passing its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: PixPullSubscription unique id. ex: "5656565656565656"</item>
        ///     <item>reason [string]: reason why the PixPullSubscription is being canceled. Options for the receiver: "accountClosed", "receiverOrganizationClosed", "receiverInternalError", "fraud", "receiverUserRequested". Options for the sender: "accountClosed", "senderDeceased", "fraud", "senderUserRequested"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled PixPullSubscription object</item>
        /// </list>
        /// </summary>
        public static PixPullSubscription Cancel(string id, string reason, User user = null)
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
            return StarkCore.Utils.Api.FromApiJson(resourceMaker, json) as PixPullSubscription;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixPullSubscription", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string bacenID = json.bacenId;
            string externalID = json.externalId;
            string installmentStartString = json.installmentStart;
            DateTime? installmentStart = string.IsNullOrEmpty(installmentStartString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(installmentStartString);
            string interval = json.interval;
            string receiverName = json.receiverName;
            string receiverTaxID = json.receiverTaxId;
            string senderAccountNumber = json.senderAccountNumber;
            string senderBankCode = json.senderBankCode;
            string senderBranchCode = json.senderBranchCode;
            string senderTaxID = json.senderTaxId;
            string type = json.type;
            long? amount = json.amount;
            long? amountMinLimit = json.amountMinLimit;
            string description = json.description;
            string dueString = json.due;
            DateTime? due = string.IsNullOrEmpty(dueString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(dueString);
            string installmentEndString = json.installmentEnd;
            DateTime? installmentEnd = string.IsNullOrEmpty(installmentEndString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(installmentEndString);
            string receiverBankCode = json.receiverBankCode;
            string referenceCode = json.referenceCode;
            long? pullRetryLimit = json.pullRetryLimit;
            string senderCityCode = json.senderCityCode;
            string senderFinalName = json.senderFinalName;
            string senderFinalTaxID = json.senderFinalTaxId;
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            string id = json.id;
            string status = json.status;
            string flow = json.flow;
            string createdString = json.created;
            DateTime? created = string.IsNullOrEmpty(createdString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = string.IsNullOrEmpty(updatedString) ? (DateTime?)null : StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixPullSubscription(
                bacenID: bacenID, externalID: externalID, installmentStart: installmentStart,
                interval: interval, receiverName: receiverName, receiverTaxID: receiverTaxID,
                senderAccountNumber: senderAccountNumber, senderBankCode: senderBankCode,
                senderBranchCode: senderBranchCode, senderTaxID: senderTaxID, type: type,
                amount: amount, amountMinLimit: amountMinLimit, description: description,
                due: due, installmentEnd: installmentEnd, receiverBankCode: receiverBankCode,
                referenceCode: referenceCode, pullRetryLimit: pullRetryLimit,
                senderCityCode: senderCityCode, senderFinalName: senderFinalName,
                senderFinalTaxID: senderFinalTaxID, tags: tags, id: id, status: status, flow: flow,
                created: created, updated: updated
            );
        }
    }
}
