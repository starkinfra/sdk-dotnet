using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixInternalTransactionReport object
    /// <br/>
    /// Transactions that happen internally — outside of the SPI — must be reported to
    /// the Central Bank so they are reflected in the participant's statements. A
    /// PixInternalTransactionReport is the report you create for each such transaction.
    /// <br/>
    /// When you initialize a PixInternalTransactionReport, the entity will not be
    /// automatically created in the Stark Infra API. The 'create' function sends the
    /// objects to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///    <item>Amount [long]: amount in cents of the reported transaction. ex: 1234 (= R$ 12.34)</item>
    ///    <item>Created [DateTime]: datetime when the reported transaction occurred. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///    <item>EndToEndID [string]: central bank's unique transaction id. ex: "E20018183202201201213u34sav898j"</item>
    ///    <item>Method [string]: execution method of the reported transaction. ex: "manual", "dict", "dynamicQrcode"</item>
    ///    <item>ReferenceType [string]: type of the reported transaction. ex: "request" or "reversal"</item>
    ///    <item>SenderAccountNumber [string]: sender's bank account number. ex: "876543-2"</item>
    ///    <item>SenderBranchCode [string]: sender's branch code. ex: "1357-9"</item>
    ///    <item>SenderAccountType [string]: sender's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
    ///    <item>SenderBankCode [string]: sender's participant code (ISPB). ex: "00000665"</item>
    ///    <item>SenderTaxID [string]: sender's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///    <item>ReceiverAccountNumber [string]: receiver's bank account number. ex: "876543-2"</item>
    ///    <item>ReceiverBranchCode [string]: receiver's branch code. ex: "1357-9"</item>
    ///    <item>ReceiverAccountType [string]: receiver's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
    ///    <item>ReceiverBankCode [string]: receiver's participant code (ISPB). ex: "20018183"</item>
    ///    <item>ReceiverTaxID [string]: receiver's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///    <item>ReceiverKeyID [string, default null]: receiver's Pix key. ex: "+5511989898989"</item>
    ///    <item>ReturnID [string, default null]: central bank's unique reversal id. Required when ReferenceType is "reversal". ex: "D20018183202202030109X3OoBHG74wo"</item>
    ///    <item>ID [string]: unique id returned when the PixInternalTransactionReport is created. ex: "5656565656565656"</item>
    ///    <item>Status [string]: current PixInternalTransactionReport status. ex: "created", "failed", "sent", "success"</item>
    ///    <item>Updated [DateTime]: latest update DateTime for the PixInternalTransactionReport. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixInternalTransactionReport : Resource
    {
        public long Amount { get; }
        public DateTime Created { get; }
        public string EndToEndID { get; }
        public string Method { get; }
        public string ReferenceType { get; }
        public string SenderAccountNumber { get; }
        public string SenderBranchCode { get; }
        public string SenderAccountType { get; }
        public string SenderBankCode { get; }
        public string SenderTaxID { get; }
        public string ReceiverAccountNumber { get; }
        public string ReceiverBranchCode { get; }
        public string ReceiverAccountType { get; }
        public string ReceiverBankCode { get; }
        public string ReceiverTaxID { get; }
        public string ReceiverKeyID { get; }
        public string ReturnID { get; }
        public string Status { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// PixInternalTransactionReport object
        /// <br/>
        /// Transactions that happen internally — outside of the SPI — must be reported to
        /// the Central Bank so they are reflected in the participant's statements.
        /// <br/>
        /// When you initialize a PixInternalTransactionReport, the entity will not be
        /// automatically created in the Stark Infra API. The 'create' function sends the
        /// objects to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///    <item>amount [long]: amount in cents of the reported transaction. ex: 1234 (= R$ 12.34)</item>
        ///    <item>created [DateTime]: datetime when the reported transaction occurred. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///    <item>endToEndID [string]: central bank's unique transaction id. ex: "E20018183202201201213u34sav898j"</item>
        ///    <item>method [string]: execution method of the reported transaction. ex: "manual", "dict", "dynamicQrcode"</item>
        ///    <item>referenceType [string]: type of the reported transaction. ex: "request" or "reversal"</item>
        ///    <item>senderAccountNumber [string]: sender's bank account number. ex: "876543-2"</item>
        ///    <item>senderBranchCode [string]: sender's branch code. ex: "1357-9"</item>
        ///    <item>senderAccountType [string]: sender's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
        ///    <item>senderBankCode [string]: sender's participant code (ISPB). ex: "00000665"</item>
        ///    <item>senderTaxID [string]: sender's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///    <item>receiverAccountNumber [string]: receiver's bank account number. ex: "876543-2"</item>
        ///    <item>receiverBranchCode [string]: receiver's branch code. ex: "1357-9"</item>
        ///    <item>receiverAccountType [string]: receiver's bank account type. ex: "checking", "savings", "salary" or "payment"</item>
        ///    <item>receiverBankCode [string]: receiver's participant code (ISPB). ex: "20018183"</item>
        ///    <item>receiverTaxID [string]: receiver's tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///    <item>receiverKeyID [string, default null]: receiver's Pix key. ex: "+5511989898989"</item>
        ///    <item>returnID [string, default null]: central bank's unique reversal id. Required when referenceType is "reversal". ex: "D20018183202202030109X3OoBHG74wo"</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///    <item>id [string]: unique id returned when the PixInternalTransactionReport is created. ex: "5656565656565656"</item>
        ///    <item>status [string]: current PixInternalTransactionReport status. ex: "created", "failed", "sent", "success"</item>
        ///    <item>updated [DateTime]: latest update DateTime for the PixInternalTransactionReport. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixInternalTransactionReport(long amount, DateTime created, string endToEndID, string method,
            string referenceType, string senderAccountNumber, string senderBranchCode, string senderAccountType,
            string senderBankCode, string senderTaxID, string receiverAccountNumber, string receiverBranchCode,
            string receiverAccountType, string receiverBankCode, string receiverTaxID, string receiverKeyID = null,
            string returnID = null, string id = null, string status = null, DateTime? updated = null) : base(id)
        {
            Amount = amount;
            Created = created;
            EndToEndID = endToEndID;
            Method = method;
            ReferenceType = referenceType;
            SenderAccountNumber = senderAccountNumber;
            SenderBranchCode = senderBranchCode;
            SenderAccountType = senderAccountType;
            SenderBankCode = senderBankCode;
            SenderTaxID = senderTaxID;
            ReceiverAccountNumber = receiverAccountNumber;
            ReceiverBranchCode = receiverBranchCode;
            ReceiverAccountType = receiverAccountType;
            ReceiverBankCode = receiverBankCode;
            ReceiverTaxID = receiverTaxID;
            ReceiverKeyID = receiverKeyID;
            ReturnID = returnID;
            Status = status;
            Updated = updated;
        }

        /// <summary>
        /// Create PixInternalTransactionReport objects
        /// <br/>
        /// Send a list of PixInternalTransactionReport objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>reports [list of PixInternalTransactionReport objects]: list of PixInternalTransactionReport objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixInternalTransactionReport objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixInternalTransactionReport> Create(List<PixInternalTransactionReport> reports, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: reports,
                user: user
            ).ToList().ConvertAll(o => (PixInternalTransactionReport)o);
        }

        /// <summary>
        /// Create PixInternalTransactionReport objects
        /// <br/>
        /// Send a list of dictionaries representing PixInternalTransactionReport objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>reports [list of dictionaries]: list of dictionaries representing the PixInternalTransactionReport objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixInternalTransactionReport objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PixInternalTransactionReport> Create(List<Dictionary<string, object>> reports, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: reports,
                user: user
            ).ToList().ConvertAll(o => (PixInternalTransactionReport)o);
        }

        /// <summary>
        /// Retrieve a specific PixInternalTransactionReport by its id
        /// <br/>
        /// Receive a single PixInternalTransactionReport object previously created in the Stark Infra API by passing its id
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
        ///     <item>PixInternalTransactionReport object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixInternalTransactionReport Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixInternalTransactionReport;
        }

        /// <summary>
        /// Retrieve PixInternalTransactionReport objects
        /// <br/>
        /// Receive an IEnumerable of PixInternalTransactionReport objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "success", "failed" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixInternalTransactionReport objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixInternalTransactionReport> Query(int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> status = null, List<string> ids = null, User user = null)
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
                    { "ids", ids }
                },
                user: user
            ).Cast<PixInternalTransactionReport>();
        }

        /// <summary>
        /// Retrieve paged PixInternalTransactionReport objects
        /// <br/>
        /// Receive a list of up to 100 PixInternalTransactionReport objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "success", "failed" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixInternalTransactionReport objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixInternalTransactionReport objects</item>
        /// </list>
        /// </summary>
        public static (List<PixInternalTransactionReport> page, string pageCursor) Page(string cursor = null,
            int? limit = null, DateTime? after = null, DateTime? before = null, List<string> status = null,
            List<string> ids = null, User user = null)
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
                    { "ids", ids }
                },
                user: user
            );
            List<PixInternalTransactionReport> reports = new List<PixInternalTransactionReport>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                reports.Add(subResource as PixInternalTransactionReport);
            }
            return (reports, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixInternalTransactionReport", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string endToEndID = json.endToEndId;
            string method = json.method;
            string referenceType = json.referenceType;
            string senderAccountNumber = json.senderAccountNumber;
            string senderBranchCode = json.senderBranchCode;
            string senderAccountType = json.senderAccountType;
            string senderBankCode = json.senderBankCode;
            string senderTaxID = json.senderTaxId;
            string receiverAccountNumber = json.receiverAccountNumber;
            string receiverBranchCode = json.receiverBranchCode;
            string receiverAccountType = json.receiverAccountType;
            string receiverBankCode = json.receiverBankCode;
            string receiverTaxID = json.receiverTaxId;
            string receiverKeyID = json.receiverKeyId;
            string returnID = json.returnId;
            string id = json.id;
            string status = json.status;
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixInternalTransactionReport(
                amount: amount, created: created, endToEndID: endToEndID, method: method,
                referenceType: referenceType, senderAccountNumber: senderAccountNumber,
                senderBranchCode: senderBranchCode, senderAccountType: senderAccountType,
                senderBankCode: senderBankCode, senderTaxID: senderTaxID,
                receiverAccountNumber: receiverAccountNumber, receiverBranchCode: receiverBranchCode,
                receiverAccountType: receiverAccountType, receiverBankCode: receiverBankCode,
                receiverTaxID: receiverTaxID, receiverKeyID: receiverKeyID, returnID: returnID,
                id: id, status: status, updated: updated
            );
        }
    }
}
