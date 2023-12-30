using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;
using Newtonsoft.Json;

namespace StarkInfra
{
    /// <summary>
    /// PixRequest object
    /// <br/>
    /// PixRequests are used to receive or send instant payments to accounts
    /// hosted in any Pix participant.
    /// <br/>
    /// When you initialize a PixRequest, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///    <item>Amount [integer]: amount in cents to be transferred. ex: 11234 (= R$ 112.34)</item>
    ///    <item>ExternalID [string]: string that must be unique among all your PixRequests. Duplicated external IDs will cause failures. By default, this parameter will block any PixRequests that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
    ///    <item>SenderName [string]: sender's full name. ex: "Anthony Edward Stark"</item>
    ///    <item>SenderTaxID [string]: sender's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///    <item>SenderBranchCode [string]: sender's bank account branch code. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
    ///    <item>SenderAccountNumber [string]: sender's bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
    ///    <item>SenderAccountType [string]: sender's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
    ///    <item>ReceiverName [string]: receiver's full name. ex: "Anthony Edward Stark"</item>
    ///    <item>ReceiverTaxID [string]: receiver's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///    <item>ReceiverBankCode [string]: receiver's bank institution code in Brazil. ex: "20018183" or "341"</item>
    ///    <item>ReceiverAccountNumber [string]: receiver's bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
    ///    <item>ReceiverBranchCode [string]: receiver's bank account branch code. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
    ///    <item>ReceiverAccountType [string]: receiver's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
    ///    <item>EndToEndID [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
    ///    <item>CashierType [string, default null]: Cashier's type. Options: merchant, other, participant</item>
    ///    <item>CashierBankCode [string, default null]: Cashier's bank code. ex: "20018183"</item>
    ///    <item>CashAmount [integer, default null]: Amount to be withdrawal from the cashier in cents. ex: 1000 (= R$ 10.00)</item>
    ///    <item>ReceiverKeyID [string, default null]: receiver's dict key. ex: "20.018.183/0001-80"</item>
    ///    <item>Description [string, default null]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
    ///    <item>ReconciliationID [string, default null]: Reconciliation ID linked to this payment. ex: "b77f5236-7ab9-4487-9f95-66ee6eaf1781"</item>
    ///    <item>InitiatorTaxID [string, default null]: Payment initiator's tax id (CPF/CNPJ). ex: "01234567890" or "20.018.183/0001-80"</item>
    ///    <item>Tags [list of strings, default null]: list of strings for reference when searching for PixRequests. ex: new List<string>{ "employees", "monthly" }</item>
    ///    <item>Method [string, default null]: execution method of creation of the Pix. ex: 'manual', 'payerQrcode', 'dynamicQrcode’.</item>
    ///    <item>ID [string]: unique id returned when the PixRequest is created. ex: "5656565656565656"</item>
    ///    <item>Fee [integer]: fee charged when PixRequest is paid. ex: 200 (= R$ 2.00)</item>
    ///    <item>Status [string]: current PixRequest status. ex: "created", "processing", "success", "failed"</item>
    ///    <item>Flow [string]: direction of money flow. ex: "in" or "out"</item>
    ///    <item>SenderBankCode [string]: sender's bank institution code in Brazil. ex: "20018183" or "341"</item>
    ///    <item>Created DateTime]: creation DateTime for the PixRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///    <item>Updated [DateTime]: latest update DateTime for the PixRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixRequest : Utils.Resource
    {
        public long Amount { get; }
        public string ExternalID { get; }
        public string SenderName { get; }
        public string SenderTaxID { get; }
        public string SenderBranchCode { get; }
        public string SenderAccountNumber { get; }
        public string SenderAccountType { get; }
        public string ReceiverName { get; }
        public string ReceiverTaxID { get; }
        public string ReceiverBankCode { get; }
        public string ReceiverAccountNumber { get; }
        public string ReceiverBranchCode { get; }
        public string ReceiverAccountType { get; }
        public string EndToEndID { get; }
        public string CashierType { get; }
        public string CashierBankCode { get; }
        public long? CashAmount { get; }
        public string ReceiverKeyID { get; }
        public string Description { get; }
        public string ReconciliationID { get; }
        public string InitiatorTaxID { get; }
        public List<string> Tags { get; }
        public string Method { get; }
        public long? Fee { get; }
        public string Status { get; }
        public string Flow { get; }
        public string SenderBankCode { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixRequest object
        /// <br/>
        /// PixRequests are used to receive or send instant payments to accounts
        /// hosted in any Pix participant.
        /// <br/>
        /// When you initialize a PixRequest, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///    <item>amount [integer]: amount in cents to be transferred. ex: 11234 (= R$ 112.34)</item>
        ///    <item>externalID [string]: string that must be unique among all your PixRequests. Duplicated external IDs will cause failures. By default, this parameter will block any PixRequests that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
        ///    <item>senderName [string]: sender's full name. ex: "Anthony Edward Stark"</item>
        ///    <item>senderTaxID [string]: sender's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///    <item>senderBranchCode [string]: sender's bank account branch code. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
        ///    <item>senderAccountNumber [string]: sender's bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
        ///    <item>senderAccountType [string]: sender's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
        ///    <item>receiverName [string]: receiver's full name. ex: "Anthony Edward Stark"</item>
        ///    <item>receiverTaxID [string]: receiver's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///    <item>receiverBankCode [string]: receiver's bank institution code in Brazil. ex: "20018183" or "341"</item>
        ///    <item>receiverAccountNumber [string]: receiver's bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
        ///    <item>receiverBranchCode [string]: receiver's bank account branch code. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
        ///    <item>receiverAccountType [string]: receiver's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
        ///    <item>endToEndID [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
        /// </list>
        /// Parameters (conditionally required):
        /// <list>
        ///    <item>cashierType [string, default null]: Cashier's type. Options: merchant, other, participant</item>
        ///    <item>cashierBankCode [string, default null]: cashier's bank code. ex: "20018183"</item>
        /// </item>
        /// Parameters (optional):
        /// <list>
        ///    <item>cashAmount [integer, default null]: Amount to be withdrawal from the cashier in cents. ex: 1000 (= R$ 10.00)</item>
        ///    <item>receiverKeyID [string, default null]: receiver's dict key. ex: "20.018.183/0001-80"</item>
        ///    <item>description [string, default null]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
        ///    <item>reconciliationID [string, default null]: Reconciliation ID linked to this payment. ex: "b77f5236-7ab9-4487-9f95-66ee6eaf1781"</item>
        ///    <item>initiatorTaxID [string, default null]: Payment initiator's tax id (CPF/CNPJ). ex: "01234567890" or "20.018.183/0001-80"</item>
        ///    <item>tags [list of strings, default null]: list of strings for reference when searching for PixRequests. ex: new List<string>{ "employees", "monthly" }</item>
        ///    <item>method [string, default null]: execution method of creation of the Pix. ex: 'manual', 'payerQrcode', 'dynamicQrcode’.</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///    <item>id [string]: unique id returned when the PixRequest is created. ex: "5656565656565656"</item>
        ///    <item>fee [integer]: fee charged when PixRequest is paid. ex: 200 (= R$ 2.00)</item>
        ///    <item>status [string]: current PixRequest status. ex: "created", "processing", "success", "failed"</item>
        ///    <item>flow [string]: direction of money flow. ex: "in" or "out"</item>
        ///    <item>senderBankCode [string]: sender's bank institution code in Brazil. ex: "20018183" or "341"</item>
        ///    <item>created [DateTime]: creation DateTime for the PixRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///    <item>updated [DateTime]: latest update DateTime for the PixRequest. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixRequest(long amount, string externalID, string senderName, string senderTaxID, 
            string senderBranchCode, string senderAccountNumber, string senderAccountType,
            string receiverName, string receiverTaxID, string receiverBankCode, 
            string receiverAccountNumber, string receiverBranchCode, string receiverAccountType, 
            string endToEndID, string cashierType = null, string cashierBankCode = null, 
            long? cashAmount = null, string receiverKeyID = null, string description = null, 
            string reconciliationID = null, string initiatorTaxID = null,  
            List<string> tags = null, string method = null, string id = null, 
            long? fee = null, string status = null, string flow = null, 
            string senderBankCode = null, DateTime? created = null, DateTime? updated = null 
        ) : base(id)
        {
            Amount = amount;
            ExternalID = externalID;
            SenderName = senderName;
            SenderTaxID = senderTaxID;
            SenderBranchCode = senderBranchCode;
            SenderAccountNumber = senderAccountNumber;
            SenderAccountType = senderAccountType;
            ReceiverName = receiverName;
            ReceiverTaxID = receiverTaxID;
            ReceiverBankCode = receiverBankCode;
            ReceiverAccountNumber = receiverAccountNumber;
            ReceiverBranchCode = receiverBranchCode;
            ReceiverAccountType = receiverAccountType;
            EndToEndID = endToEndID;
            ReceiverKeyID = receiverKeyID;
            Description = description;
            ReconciliationID = reconciliationID;
            InitiatorTaxID = initiatorTaxID;
            CashAmount = cashAmount;
            CashierBankCode = cashierBankCode;
            CashierType = cashierType;
            Tags = tags;
            Method = method;
            Fee = fee;
            Status = status;
            Flow = flow;
            SenderBankCode = senderBankCode;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create PixRequest objects
        /// <br/>
        /// Send a list of PixRequest objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of PixRequest objects]: list of PixRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixRequest> Create(List<PixRequest> requests, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (PixRequest)o);
        }

        /// <summary>
        /// Create PixRequest objects
        /// <br/>
        /// Send a list of PixRequest objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>requests [list of dictionaries]: list of dictionaries representing the PixRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixRequest> Create(List<Dictionary<string, object>> requests, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: requests,
                user: user
            ).ToList().ConvertAll(o => (PixRequest)o);
        }

        /// <summary>
        /// Retrieve a specific PixRequest by its id
        /// <br/>
        /// Receive a single PixRequest object previously created in the Stark Infra API by passing its id
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
        ///     <item>PixRequest object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixRequest Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixRequest;
        }

        /// <summary>
        /// Retrieve PixRequest objects
        /// <br/>
        /// Receive an IEnumerable of PixRequest objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>endToEndIds [list of strings, default null]: central bank's unique transaction IDs. ex: new List<string>{ "E79457883202101262140HHX553UPqeq", "E79457883202101262140HHX553UPxzx" }</item>
        ///     <item>externalIds [list of strings, default null]: url safe strings that must be unique among all your PixRequests. Duplicated external IDs will cause failures. By default, this parameter will block any PixRequests that repeats amount and receiver information on the same date. ex: new List<string>{ "my-internal-id-123456", "my-internal-id-654321" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixRequest> Query(int? limit = null, DateTime? after = null, DateTime? before = null, 
            List<string> status = null, List<string> ids = null, List<string> tags = null, List<string> endToEndIds = null, 
            List<string> externalIds = null, User user = null)
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
                    { "ids", ids },
                    { "tags", tags },
                    { "endToEndIds", endToEndIds },
                    { "externalIds", externalIds }
                },
                user: user
            ).Cast<PixRequest>();
        }

        /// <summary>
        /// Retrieve paged PixRequest objects
        /// <br/>
        /// Receive a list of up to 100 PixRequest objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>endToEndIds [list of strings, default null]: central bank's unique transaction IDs. ex: new List<string>{ "E79457883202101262140HHX553UPqeq", "E79457883202101262140HHX553UPxzx" }</item>
        ///     <item>externalIds [list of strings, default null]: url safe strings that must be unique among all your PixRequests. Duplicated external IDs will cause failures. By default, this parameter will block any PixRequests that repeats amount and receiver information on the same date. ex: new List<string>{ "my-internal-id-123456", "my-internal-id-654321" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixRequest objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixRequest objects</item>
        /// </list>
        /// </summary>
        public static (List<PixRequest> page, string pageCursor) Page(string cursor = null, 
            int? limit = null, DateTime? after = null, DateTime? before = null, List<string> status = null, 
            List<string> ids = null, List<string> tags = null, List<string> endToEndIds = null, 
            List<string> externalIds = null, User user = null)
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
                    { "ids", ids },
                    { "tags", tags },
                    { "endToEndIds", endToEndIds },
                    { "externalIds", externalIds }
                },
                user: user
            );
            List<PixRequest> requests = new List<PixRequest>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                requests.Add(subResource as PixRequest);
            }
            return (requests, pageCursor);
        }
        
        /// <summary>
        /// Create single verified PixRequest object from a content string
        /// <br/>
        /// Create a single PixRequest object from a content string received from a handler listening at
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
        ///     <item>Parsed PixRequest object</item>
        /// </list>
        /// </summary>
        public static PixRequest Parse(string content, string signature, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Parse.ParseAndVerify(
                content: content,
                signature: signature,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                user: user
            ) as PixRequest;
        }

        /// <summary>
        /// Helps you respond to a PixRequest authorization
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>status [string]: response to the authorization. Options: "approved" or "denied"</item>
        ///</list>
        /// Parameters (conditionally required):
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
            return (resourceName: "PixRequest", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string externalID = json.externalId is null ? "" : json.externalId;
            string senderName = json.senderName;
            string senderTaxID = json.senderTaxId;
            string senderBranchCode = json.senderBranchCode;
            string senderAccountNumber = json.senderAccountNumber;
            string senderAccountType = json.senderAccountType;
            string receiverName = json.receiverName;
            string receiverTaxID = json.receiverTaxId;
            string receiverBankCode = json.receiverBankCode;
            string receiverAccountNumber = json.receiverAccountNumber;
            string receiverBranchCode = json.receiverBranchCode;
            string receiverAccountType = json.receiverAccountType;
            string endToEndID = json.endToEndId;
            string cashierType = json.cashierType;
            string cashierBankCode = json.cashierBankCode;
            long cashAmount = json.cashAmount;
            string receiverKeyID = json.receiverKeyId;
            string description = json.description is null ? "" : json.description;
            string reconciliationID = json.reconciliationId;
            string initiatorTaxID = json.initiatorTaxId;
            List<string> tags = json.tags is null ? new List<string> { } : json.tags.ToObject<List<string>>();
            string method = json.method;
            string id = json.id;
            long fee = json.fee is null ? 0 : json.fee;
            string status = json.status;
            string flow = json.flow ;
            string senderBankCode = json.senderBankCode;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixRequest(
                amount: amount, externalID: externalID, senderName: senderName, 
                senderTaxID: senderTaxID, senderBranchCode: senderBranchCode, 
                senderAccountNumber: senderAccountNumber, senderAccountType: senderAccountType, 
                receiverName: receiverName, receiverTaxID: receiverTaxID, 
                receiverBankCode: receiverBankCode, receiverAccountNumber: receiverAccountNumber, 
                receiverBranchCode: receiverBranchCode, receiverAccountType: receiverAccountType, 
                endToEndID: endToEndID, cashierType: cashierType, cashierBankCode: cashierBankCode, 
                cashAmount: cashAmount, receiverKeyID: receiverKeyID, description: description, 
                reconciliationID: reconciliationID, initiatorTaxID: initiatorTaxID, tags: tags, 
                method: method, id: id, fee: fee, status: status, flow: flow, senderBankCode: senderBankCode, 
                created: created, updated: updated
            );
        }
    }
}
