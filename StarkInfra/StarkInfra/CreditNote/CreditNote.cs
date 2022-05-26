using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditNote object
    /// <br/>
    /// CreditNotes are used to generate CCB contracts between you and your customers.
    /// When you initialize a CreditNote, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>TemplateID [string]: ID of the contract template on which the credit note will be based. ex: templateId="0123456789101112"</item>
    ///     <item>Name [string]: credit receiver's full name. ex: name="Edward Stark"</item>
    ///     <item>TaxID [string]: credit receiver's tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
    ///     <item>NominalAmount [integer]: amount in cents transferred to the credit receiver, before deductions. ex: nominalAmount=11234 (= R$ 112.34)</item>
    ///     <item>Scheduled [DateTime or string, default now]: date of payment execution. ex: scheduled=datetime(2020, 3, 10)</item>
    ///     <item>Invoices [list of CreditNote.Invoice objects]: list of Invoice objects or dictionaries to be created and sent to the credit receiver. ex: new List<CreditNote.Invoice>{ new CreditNote.InvoiceInvoice() }</item>
    ///     <item>Payment [CreditNote.Transfer object]: Transfer object or dictionary to be created and sent to the credit receiver. ex: payment=Transfer()</item>
    ///     <item>Signers [list of Signer objects]: list of Signer objects containing signer's information</item>
    ///     <item>ExternalID [string]: a string that must be unique among all your CreditNotes, used to avoid resource duplication. ex: "my-internal-id-123456"</item>
    ///     <item>PaymentType [string]: payment type, inferred from the payment parameter if it is not a dictionary. ex: "transfer"</item>
    ///     <item>RebateAmount [integer, default null]: credit analysis fee deducted from lent amount. ex: rebateAmount=11234 (= R$ 112.34)</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for CreditNotes. ex: tags=new List<string>{ "employees", "monthly" }</item>
    ///     <item>ID [string]: unique id returned when the CreditNote is created. ex: "5656565656565656"</item>
    ///     <item>Amount [integer]: CreditNote value in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>Expiration [integer]: time interval in seconds between due date and expiration date. ex: 123456789</item>
    ///     <item>DocumentID [string]: ID of the signed document to execute this CreditNote. ex: "4545454545454545"</item>
    ///     <item>Status [string]: current status of the CreditNote. ex: "canceled", "created", "expired", "failed", "processing", "signed", "success"</item>
    ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this CreditNote. ex: ["19827356981273"]</item>
    ///     <item>WorkspaceId [string]: ID of the Workspace that generated this CreditNote. ex: "4545454545454545"</item>
    ///     <item>TaxAmount [integer]: tax amount included in the CreditNote. ex: 100</item>
    ///     <item>Interest [float]: yearly effective interest rate of the credit note, in percentage. ex: 12.5
    ///     <item>Created [DateTime]: creation datetime for the CreditNote. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the CreditNote. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     
    /// </list>
    /// </summary>
    public partial class CreditNote : Resource
    {
        public string TemplateID { get; }
        public string Name { get; }
        public string TaxID { get; }
        public long NominalAmount { get; }
        public DateTime? Scheduled { get; }
        public List<Invoice> Invoices { get; }
        public Resource Payment { get; }
        public List<Signer> Signers { get; }
        public string ExternalID { get; }
        public string PaymentType { get; }
        public long? RebateAmount { get; }
        public List<string> Tags { get; }
        public long? Amount { get; }
        public string Expiration { get; }
        public string DocumentID { get; }
        public string Status { get; }
        public List<string> TransactionIds { get; }
        public string WorkspaceId { get; }
        public long? TaxAmount { get; }
        public float? Interest { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// CreditNote object
        /// <br/>
        /// The CreditNote object displays the information of the cards created in your Workspace.
        /// Sensitive information will only be returned when the "expand" parameter is used, to avoid security concerns.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>templateId [string]: ID of the contract template on which the credit note will be based. ex: templateId="0123456789101112"</item>
        ///     <item>name [string]: credit receiver's full name. ex: name="Edward Stark"</item>
        ///     <item>taxId [string]: credit receiver's tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
        ///     <item>nominalAmount [integer]: amount in cents paid to the credit receiver, before deductions. ex: nominalAmount=11234 (= R$ 112.34)</item>
        ///     <item>scheduled [DateTime or string, default now]: date of payment execution. ex: scheduled=datetime(2020, 3, 10)</item>
        ///     <item>invoices [list of Invoice objects]: list of Invoice objects to be created and sent to the credit receiver. ex: new List<Invoice>{ new Invoice(), new Invoice() }</item>
        ///     <item>payment [Payment object]: Payment object to be created and sent to the credit receiver. ex: payment=Payment()</item>
        ///     <item>signers [List of Signer objects]: signer's name, e-mail and delivery method for the contract. ex: signers= new List<Signer>{ new Signer(name: "Tony Stark", contact: "tony@starkindustries.com", method: "link")}</item>
        ///     <item>externalId [string]: a string that must be unique among all your CreditNotes, used to avoid resource duplication. ex: "my-internal-id-123456"</item>
        ///</list>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>PaymentType [string]: payment type, inferred from the payment parameter if it is not a dictionary. ex: "transfer"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>rebateAmount [integer, default null]: credit analysis fee deducted from lent amount. ex: rebateAmount=11234 (= R$ 112.34)</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for CreditNotes. ex: tags= new List<string>{ "employees", "monthly" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>ID [string]: unique id returned when the CreditNote is created. ex: "5656565656565656"</item>
        ///     <item>Amount [integer]: CreditNote value in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>Expiration [integer]: time interval in seconds between due date and expiration date. ex: 123456789</item>
        ///     <item>DocumentID [string]: ID of the signed document to execute this CreditNote. ex: "4545454545454545"</item>
        ///     <item>Status [string]: current status of the CreditNote. ex: "canceled", "created", "expired", "failed", "processing", "signed", "success"</item>
        ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this CreditNote. ex: ["19827356981273"]</item>
        ///     <item>WorkspaceId [string]: ID of the Workspace that generated this CreditNote. ex: "4545454545454545"</item>
        ///     <item>TaxAmount [integer]: tax amount included in the CreditNote. ex: 100</item>
        ///     <item>Interest [float]: yearly effective interest rate of the credit note, in percentage. ex: 12.5</item>
        ///     <item>Created [DateTime]: creation datetime for the CreditNote. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>Updated [DateTime]: latest update datetime for the CreditNote. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public CreditNote(string templateId, string name, string taxId, long nominalAmount, DateTime? scheduled, List<Invoice> invoices, Resource payment,
            List<Signer> signers, string externalId, string paymentType, long? rebateAmount = null, List<string> tags = null, string id = null,
            long? amount = null, string expiration = null, string documentId = null, string status = null, List<string> transactionIds = null, string workspaceId = null,
            long? taxAmount = null, float? interest = null, DateTime? created = null, DateTime? updated = null)  : base(id)
        {
            TemplateID = templateId;
            Name = name;
            TaxID = taxId;
            NominalAmount = nominalAmount;
            Scheduled = scheduled;
            Invoices = invoices;
            Payment = payment;
            Signers = signers;
            ExternalID = externalId;
            PaymentType = paymentType;
            RebateAmount = rebateAmount;
            Tags = tags;
            Amount = amount;
            Expiration = expiration;
            DocumentID = documentId;
            Status = status;
            TransactionIds = transactionIds;
            WorkspaceId = workspaceId;
            TaxAmount = taxAmount;
            Interest = interest;
            Created = created;
            Updated = updated;
        }

        private static string GetType(Utils.Resource payment)
        {
            if (payment.GetType() == typeof(Transfer))
            {
                return "transfer";
            }
            throw new Exception("if no type is specified, payment must a Transfer");
        }

        /// <summary>
        /// Create CreditNotes
        /// <br/>
        /// Send a list of CreditNote objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>notes [list of CreditNote objects]: list of CreditNote objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CreditNote objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<CreditNote> Create(List<CreditNote> notes, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: notes,
                user: user
            ).ToList().ConvertAll(o => (CreditNote)o);
        }

        /// <summary>
        /// Create CreditNotes
        /// <br/>
        /// Send a list of creditNote dicitonaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>notes [list of dictionaries]: list of Dictionaries representing the CreditNotes to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CreditNote objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<CreditNote> Create(List<Dictionary<string, object>> notes, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: notes,
                user: user
            ).ToList().ConvertAll(o => (CreditNote)o);
        }

        /// <summary>
        /// Retrieve a specific CreditNote
        /// <br/>
        /// Receive a single CreditNote object previously created in the Stark Infra API by passing its id
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
        ///     <item>CreditNote object with updated attributes</item>
        /// </list>
        /// </summary>
        public static CreditNote Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as CreditNote;
        }

        /// <summary>
        /// Retrieve CreditNotes
        /// <br/>
        /// Receive an IEnumerable of CreditNote objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Unlimited if null. ex: 35D20018183202202030109X3OoBhfkg7h</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "canceled", "created", "expired", "failed", "processing", "signed", "success"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime or string, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CreditNote objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CreditNote> Query(int? limit = null, string status = null, List<string> tags = null, List<string> ids = null,
            DateTime? after = null, DateTime? before = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    {"limit", limit },
                    {"tags", tags },
                    {"ids", ids },
                    {"status", status },
                    {"after", after },
                    {"before", before },
                    {"user", user }
                },
                user: user
            ).Cast<CreditNote>();
        }

        /// <summary>
        /// Retrieve paged CreditNotes
        /// <br/>
        /// Receive a list of up to 100 CreditNote objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Unlimited if null. ex: 35D20018183202202030109X3OoBhfkg7h</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "canceled", "created", "expired", "failed", "processing", "signed", "success"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>after [DateTime or string, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CreditNote objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of CreditNote objects</item>
        /// </list>
        /// </summary>
        public static (List<CreditNote> page, string pageCursor) Page(string cursor = null, int? limit = null, string status = null, List<string> tags = null,
            List<string> ids = null, DateTime? after = null, DateTime? before = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "tags", tags },
                    { "ids", ids },
                    { "status", status },
                    { "after", after },
                    { "before", before }
                },
                user: user
            );
            List<CreditNote> notes = new List<CreditNote>();
            foreach (SubResource subResource in page)
            {
                notes.Add(subResource as CreditNote);
            }
            return (notes, pageCursor);
        }

        /// <summary>
        /// Cancel a CreditNote entity
        /// <br/>
        /// Cancel a CreditNote entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: CreditNote unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Organization/Project object, default null]: Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled CreditNote object</item>
        /// </list>
        /// </summary>
        public static CreditNote Cancel(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as CreditNote;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CreditNote", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string templateId = json.templateId;
            string name = json.name;
            string taxId = json.taxId;
            long nominalAmount = json.nominalAmount;
            string scheduledString = json.scheduled;
            DateTime scheduled = Checks.CheckDateTime(scheduledString);
            string externalId = json.externalId;
            string paymentType = json.paymentType;
            long? rebateAmount = json.rebateAmount;
            List<string> tags = json.tags.ToObject<List<string>>();
            long? amount = json.amount;
            string expiration = json.expiration;
            string documentId = json.documentId;
            string status = json.status;
            List<string> transactionIds = json.transactionIds.ToObject<List<string>>();
            string workspaceId = json.workspaceId;
            long? taxAmount = json.taxAmount;
            float? interest = json.interest;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            List<Signer> signers = ParseSigners(json.signers);
            Resource payment = ParsePayment(json: json.payment, paymentType: json.paymentType.ToObject<string>());
            List<Invoice> invoices = ParseInvoice(json.invoices);

            return new CreditNote(
                id: id, templateId: templateId, name: name, taxId: taxId, nominalAmount: nominalAmount, scheduled: scheduled, invoices: invoices,
                payment: payment, signers: signers, externalId: externalId, paymentType: paymentType, rebateAmount: rebateAmount, tags: tags,
                amount: amount, expiration: expiration, documentId: documentId, status: status, transactionIds: transactionIds, workspaceId: workspaceId,
                taxAmount: taxAmount, interest: interest, created: created, updated: updated
            );
        }

        private static Resource ParsePayment(dynamic json, string paymentType)
        {
            if (paymentType == "transfer")
            {
                return Transfer.ResourceMaker(json);
            }
            return null;
        }

        private static List<Invoice> ParseInvoice(dynamic json)
        {
            List<Invoice> invoices = new List<Invoice>();

            foreach (dynamic invoice in json)
            {
                invoices.Add(Invoice.ResourceMaker(invoice));
            }
            return invoices;
        }

        private static List<Signer> ParseSigners(dynamic json)
        {
            List<Signer> signers = new List<Signer>();

            foreach (dynamic signer in json)
            {
                signers.Add(Signer.ResourceMaker(signer));
            }
            return signers;
        }
    }
}
