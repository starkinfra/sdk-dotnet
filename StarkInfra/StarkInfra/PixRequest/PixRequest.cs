using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixRequest object
    /// <br/>
    /// When you initialize a PixRequest, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///    <item>amount [integer]: amount in cents to be transferred. ex: 11234 (= R$ 112.34)</item>
    ///    <item>externalId [string]: url safe string that must be unique among all your PixRequests. Duplicated external IDs will cause failures. By default, this parameter will block any PixRequests that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
    ///    <item>senderName [string]: sender's full name. ex: "Anthony Edward Stark"</item>
    ///    <item>senderTaxId [string]: sender's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///    <item>senderBranchCode [string]: sender's bank account branch code. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
    ///    <item>senderAccountNumber [string]: sender's bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
    ///    <item>senderAccountType [string, default "checking"]: sender's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
    ///    <item>receiverName [string]: receiver's full name. ex: "Anthony Edward Stark"</item>
    ///    <item>receiverTaxId [string]: receiver's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///    <item>receiverBankCode [string]: receiver's bank institution code in Brazil. ex: "20018183" or "341"</item>
    ///    <item>receiverAccountNumber [string]: receiver's bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
    ///    <item>receiverBranchCode [string]: receiver's bank account branch code. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
    ///    <item>receiverAccountType [string]: receiver's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
    ///    <item>endToEndId [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
    ///    <item>receiverKeyId [string, default null]: receiver's dict key. ex: "20.018.183/0001-80"</item>
    ///    <item>description [string, default null]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
    ///    <item>reconciliationId [string, default null]: Reconciliation ID linked to this payment. ex: "b77f5236-7ab9-4487-9f95-66ee6eaf1781"</item>
    ///    <item>initiatorTaxId [string, default null]: Payment initiator's tax id (CPF/CNPJ). ex: "01234567890" or "20.018.183/0001-80"</item>
    ///    <item>cashAmount [integer, default null]: Amount to be withdrawal from the cashier in cents. ex: 1000 (= R$ 10.00)</item>
    ///    <item>cashierBankCode [string, default null]: Cashier's bank code. ex: "00000000"</item>
    ///    <item>cashierType [string, default null]: Cashier's type. ex: [merchant, other, participant]</item>
    ///    <item>tags [list of strings, default null]: list of strings for reference when searching for PixRequests. ex: ["employees", "monthly"]</item>
    ///    <item>method [string, default null]: execution  method for thr creation of the PIX. ex: "manual", "payerQrcode", "dynamicQrcode".</item>
    ///    <item>id [string, default null]: unique id returned when the PixRequest is created. ex: "5656565656565656"</item>
    ///    <item>fee [integer, default null]: fee charged when PixRequest is paid. ex: 200 (= R$ 2.00)</item>
    ///    <item>status [string, default null]: current PixRequest status. ex: "registered" or "paid"</item>
    ///    <item>flow [string, default null]: direction of money flow. ex: "in" or "out"</item>
    ///    <item>senderBankCode [string, default null]: sender's bank institution code in Brazil. If an ISPB (8 digits) is informed. ex: "20018183" or "341"</item>
    ///    <item>created DateTime, default null]: creation datetime for the PixRequest. ex: datetime.datetime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///    <item>updated [DateTime, default null]: latest update datetime for the PixRequest. ex: datetime.datetime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixRequest : Utils.Resource
    {
        public long Amount { get; }
        public string ExternalId { get; }
        public string SenderName { get; }
        public string SenderTaxId { get; }
        public string SenderBranchCode { get; }
        public string SenderAccountNumber { get; }
        public string SenderAccountType { get; }
        public string ReceiverName { get; }
        public string ReceiverTaxId { get; }
        public string ReceiverBankCode { get; }
        public string ReceiverAccountNumber { get; }
        public string ReceiverBranchCode { get; }
        public string ReceiverAccountType { get; }
        public string EndToEndId { get; }
        public string ReceiverKeyId { get; }
        public string Description { get; }
        public string ReconciliationId { get; }
        public string InitiatorTaxId { get; }
        public long? CashAmount { get; }
        public string CashierBankCode { get; }
        public string CashierType { get; }
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
        /// When you initialize a PixRequest, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///    <item>amount [integer]: amount in cents to be transferred. ex: 11234 (= R$ 112.34)</item>
        ///    <item>externalId [string]: url safe string that must be unique among all your PixRequests. Duplicated external IDs will cause failures. By default, this parameter will block any PixRequests that repeats amount and receiver information on the same date. ex: "my-internal-id-123456"</item>
        ///    <item>senderName [string]: sender's full name. ex: "Anthony Edward Stark"</item>
        ///    <item>senderTaxId [string]: sender's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///    <item>senderBranchCode [string]: sender's bank account branch code. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
        ///    <item>senderAccountNumber [string]: sender's bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
        ///    <item>senderAccountType [string, default "checking"]: sender's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
        ///    <item>receiverName [string]: receiver's full name. ex: "Anthony Edward Stark"</item>
        ///    <item>receiverTaxId [string]: receiver's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///    <item>receiverBankCode [string]: receiver's bank institution code in Brazil. ex: "20018183" or "341"</item>
        ///    <item>receiverAccountNumber [string]: receiver's bank account number. Use '-' before the verifier digit. ex: "876543-2"</item>
        ///    <item>receiverBranchCode [string]: receiver's bank account branch code. Use '-' in case there is a verifier digit. ex: "1357-9"</item>
        ///    <item>receiverAccountType [string]: receiver's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
        ///    <item>endToEndId [string]: central bank's unique transaction ID. ex: "E79457883202101262140HHX553UPqeq"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///    <item>receiverKeyId [string, default null]: receiver's dict key. ex: "20.018.183/0001-80"</item>
        ///    <item>description [string, default null]: optional description to override default description to be shown in the bank statement. ex: "Payment for service #1234"</item>
        ///    <item>reconciliationId [string, default null]: Reconciliation ID linked to this payment. ex: "b77f5236-7ab9-4487-9f95-66ee6eaf1781"</item>
        ///    <item>initiatorTaxId [string, default null]: Payment initiator's tax id (CPF/CNPJ). ex: "01234567890" or "20.018.183/0001-80"</item>
        ///    <item>cashAmount [integer, default null]: Amount to be withdrawal from the cashier in cents. ex: 1000 (= R$ 10.00)</item>
        ///    <item>cashierBankCode [string, default null]: Cashier's bank code. ex: "00000000"</item>
        ///    <item>cashierType [string, default null]: Cashier's type. ex: [merchant, other, participant]</item>
        ///    <item>tags [list of strings, default null]: list of strings for reference when searching for PixRequests. ex: ["employees", "monthly"]</item>
        ///    <item>method [string, default null]: execution  method for thr creation of the PIX. ex: "manual", "payerQrcode", "dynamicQrcode".</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///    <item>id [string, default null]: unique id returned when the PixRequest is created. ex: "5656565656565656"</item>
        ///    <item>fee [integer, default null]: fee charged when PixRequest is paid. ex: 200 (= R$ 2.00)</item>
        ///    <item>status [string, default null]: current PixRequest status. ex: "registered" or "paid"</item>
        ///    <item>flow [string, default null]: direction of money flow. ex: "in" or "out"</item>
        ///    <item>senderBankCode [string, default null]: sender's bank institution code in Brazil. If an ISPB (8 digits) is informed. ex: "20018183" or "341"</item>
        ///    <item>created [DateTime, default null]: creation datetime for the PixRequest. ex: datetime.datetime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///    <item>updated [DateTime, default null]: latest update datetime for the PixRequest. ex: datetime.datetime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixRequest(long amount, string externalId, string senderName, string senderTaxId, 
            string senderBranchCode, string senderAccountNumber, string senderAccountType,
            string receiverName, string receiverTaxId, string receiverBankCode, 
            string receiverAccountNumber, string receiverBranchCode, string receiverAccountType, 
            string endToEndId, string receiverKeyId = null, string description = null, 
            string reconciliationId = null, string initiatorTaxId = null, long? cashAmount = null, 
            string cashierBankCode = null, string cashierType = null, List<string> tags = null, 
            string method = null, string id = null,  long? fee = null, string status = null, string flow = null, 
            string senderBankCode = null, DateTime? created = null, DateTime? updated = null 
                ) : base(id)
        {
            Amount = amount;
            ExternalId = externalId;
            SenderName = senderName;
            SenderTaxId = senderTaxId;
            SenderBranchCode = senderBranchCode;
            SenderAccountNumber = senderAccountNumber;
            SenderAccountType = senderAccountType;
            ReceiverName = receiverName;
            ReceiverTaxId = receiverTaxId;
            ReceiverBankCode = receiverBankCode;
            ReceiverAccountNumber = receiverAccountNumber;
            ReceiverBranchCode = receiverBranchCode;
            ReceiverAccountType = receiverAccountType;
            EndToEndId = endToEndId;
            ReceiverKeyId = receiverKeyId;
            Description = description;
            ReconciliationId = reconciliationId;
            InitiatorTaxId = initiatorTaxId;
            CashAmount = cashAmount;
            CashierBankCode = cashierBankCode;
            CashierType = cashierType;
            Tags = tags;
            Method = method;
            Fee = fee;
            Status = status;
            Flow = flow ;
            SenderBankCode = senderBankCode;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Create PixRequests
        /// <br/>
        /// Send a list of PixRequest objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>pixRequests [list of PixRequest objects]: list of PixRequest objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixRequest> Create(List<PixRequest> pixRequests, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: pixRequests,
                user: user
            ).ToList().ConvertAll(o => (PixRequest)o);
        }

        /// <summary>
        /// Create PixRequests
        /// <br/>
        /// Send a list of PixRequest objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>pixRequests [list of Dictionaries]: list of Dictionaries representing the PixRequests to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixRequest> Create(List<Dictionary<string, object>> pixRequests, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: pixRequests,
                user: user
            ).ToList().ConvertAll(o => (PixRequest)o);
        }

        /// <summary>
        /// Retrieve a specific PixRequest
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixRequest object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixRequest Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixRequest;
        }

        /// <summary>
        /// Retrieve PixRequests
        /// <br/>
        /// Receive an IEnumerable of PixRequest objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>fields [list of strings, default null]: parameters to be retrieved from PixRequest objects. ex: ["amount", "id"]</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: datetime.date(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: datetime.date(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>endToEndIds [list of strings, default null]: central bank's unique transaction IDs. ex: ["E79457883202101262140HHX553UPqeq", "E79457883202101262140HHX553UPxzx"]</item>
        ///     <item>externalIds [list of strings, default null]: url safe strings that must be unique among all your PixRequests. Duplicated external IDs will cause failures. By default, this parameter will block any PixRequests that repeats amount and receiver information on the same date. ex: ["my-internal-id-123456", "my-internal-id-654321"]</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixRequest objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixRequest> Query(List<string>  fields = null, int? limit = null, 
            DateTime? after = null, DateTime? before = null, string status = null, List<string> tags = null, 
            List<string> ids = null, List<string> endToEndIds = null, List<string> externalIds = null, User user = null)
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
                    { "endToEndIds", endToEndIds },
                    { "externalIds", externalIds },
                    { "user", user }
                },
                user: user
            ).Cast<PixRequest>();
        }

        /// <summary>
        /// Retrieve paged PixRequests
        /// <br/>
        /// Receive a list of up to 100 PixRequest objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>fields [list of strings, default null]: parameters to be retrieved from PixRequest objects. ex: ["amount", "id"]</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created or updated only after specified date. ex: datetime.date(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created or updated only before specified date. ex: datetime.date(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "success" or "failed"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>endToEndIds [list of strings, default null]: central bank's unique transaction IDs. ex: ["E79457883202101262140HHX553UPqeq", "E79457883202101262140HHX553UPxzx"]</item>
        ///     <item>externalIds [list of strings, default null]: url safe strings that must be unique among all your PixRequests. Duplicated external IDs will cause failures. By default, this parameter will block any PixRequests that repeats amount and receiver information on the same date. ex: ["my-internal-id-123456", "my-internal-id-654321"]</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixRequest objects with updated attributes and cursor to retrieve the next page of PixRequest objects</item>
        /// </list>
        /// </summary>
        public static (List<PixRequest> page, string pageCursor) Page(string cursor = null, List<string>  fields = null, 
            int? limit = null, DateTime? after = null, DateTime? before = null, string status = null, 
            List<string> tags = null, List<string> ids = null, List<string> endToEndIds = null, 
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
                    { "endToEndIds", endToEndIds },
                    { "externalIds", externalIds },
                    { "user", user }
                },
                user: user
            );
            List<PixRequest> pixRequests = new List<PixRequest>();
            foreach (SubResource subResource in page)
            {
                pixRequests.Add(subResource as PixRequest);
            }
            return (pixRequests, pageCursor);
        }
        
        /// <summary>
        ///  Create single verified PixRequest object from a content string
        /// <br/>
        /// Create a single PixRequest object from a content string received from a handler listening at
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Parsed PixRequest object</item>
        /// </list>
        /// </summary>
        public static PixRequest Parse(string content, string signature, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Parse.ParseAndVerify(
                content: content,
                signature: signature,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                user: user
            ) as PixRequest;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixRequest", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string externalId = json.externalId;
            string senderName = json.senderName;
            string senderTaxId = json.senderTaxId;
            string senderBranchCode = json.senderBranchCode;
            string senderAccountNumber = json.senderAccountNumber;
            string senderAccountType = json.senderAccountType;
            string receiverName = json.receiverName;
            string receiverTaxId = json.receiverTaxId;
            string receiverBankCode = json.receiverBankCode;
            string receiverAccountNumber = json.receiverAccountNumber;
            string receiverBranchCode = json.receiverBranchCode;
            string receiverAccountType = json.receiverAccountType;
            string endToEndId = json.endToEndId;
            string receiverKeyId = json.receiverKeyId;
            string description = json.description;
            string reconciliationId = json.reconciliationId;
            string initiatorTaxId = json.initiatorTaxId;
            long cashAmount = json.cashAmount;
            string cashierBankCode = json.cashierBankCode;
            string cashierType = json.cashierType;
            List<string> tags = json.tags.ToObject<List<string>>();
            string method = json.method;
            string id = json.id;
            long fee = json.fee;
            string status = json.status;
            string flow = json.flow ;
            string senderBankCode = json.senderBankCode;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new PixRequest(
                amount: amount, externalId: externalId, senderName: senderName, senderTaxId: senderTaxId, 
                senderBranchCode: senderBranchCode, senderAccountNumber: senderAccountNumber, 
                senderAccountType: senderAccountType, receiverName: receiverName, receiverTaxId: receiverTaxId, 
                receiverBankCode: receiverBankCode, receiverAccountNumber: receiverAccountNumber, 
                receiverBranchCode: receiverBranchCode, receiverAccountType: receiverAccountType, 
                endToEndId: endToEndId, receiverKeyId: receiverKeyId, description: description, 
                reconciliationId: reconciliationId, initiatorTaxId: initiatorTaxId, cashAmount: cashAmount, 
                cashierBankCode: cashierBankCode, cashierType: cashierType, tags: tags, method: method, id: id, 
                fee: fee, status: status, flow: flow, senderBankCode: senderBankCode, created: created, updated: updated
            );
        }
    }
}
