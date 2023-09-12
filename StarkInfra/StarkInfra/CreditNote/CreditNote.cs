using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditNote object
    /// <br/>
    /// CreditNotes are used to generate CCB contracts between you and your customers.
    /// <br/>
    /// When you initialize a CreditNote, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>TemplateID [string]: ID of the contract template on which the CreditNote will be based. ex: "0123456789101112"</item>
    ///     <item>Name [string]: credit receiver's full name. ex: "Edward Stark"</item>
    ///     <item>TaxID [string]: credit receiver's tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
    ///     <item>Scheduled [DateTime]: date of payment execution. ex: DateTime(2020, 3, 10)</item>
    ///     <item>Invoices [list of CreditNote.Invoice objects]: list of Invoice objects or dictionaries to be created and sent to the credit receiver. ex: new List<CreditNote.Invoice>{ new CreditNote.InvoiceInvoice() }</item>
    ///     <item>Payment [CreditNote.Transfer object]: Payment entity to be created and sent to the credit receiver. ex: Transfer()</item>
    ///     <item>Signers [list of CreditSigner objects]: list of CreditSigner objects containing signer's name, contact and delivery method for the signature request. ex: new List<CreditSigner>{ new CreditSigner(name: "Tony Stark", contact: "tony@starkindustries.com", method: "link")}</item>
    ///     <item>ExternalID [string]: a string that must be unique among all your CreditNotes, used to avoid resource duplication. ex: "my-internal-id-123456"</item>
    ///     <item>StreetLine1 [string]: credit receiver main address. ex: "Av. Paulista, 200"</item>
    ///     <item>StreetLine2 [string]: credit receiver address complement. ex: "Apto. 123"</item>
    ///     <item>District [string]: credit receiver address district / neighbourhood. ex: "Bela Vista"</item>
    ///     <item>City [string]: credit receiver address city. ex: "Rio de Janeiro"</item>
    ///     <item>StateCode [string]: credit receiver address state. ex: "GO"</item>
    ///     <item>ZipCode [string]: credit receiver address zip code. ex: "01311-200"</item>
    ///     <item>Amount [integer]: amount in cents transferred to the credit receiver, before deductions. The amount parameter is required when nominalAmount is not sent. ex: 1234 (= R$ 12.34)</item>
    ///     <item>NominalAmount [integer]: CreditNote value in cents. The nominalAmount parameter is required when amount is not sent. ex: 1234 (= R$ 12.34)</item>
    ///     <item>PaymentType [string]: payment type, inferred from the payment parameter if it is not a dictionary. ex: "transfer"</item>
    ///     <item>RebateAmount [integer, default null]: credit analysis fee deducted from lent amount. ex: 11234  R$ 112.34)</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for CreditNotes. ex: new List<string>{ "employees", "monthly" }</item>
    ///     <item>Expiration [integer, default 604800 (7 days)]: time interval in seconds between scheduled date and expiration. ex: 123456789</item>
    ///     <item>ID [string]: unique id returned when the CreditNote is created. ex: "5656565656565656"</item>
    ///     <item>DocumentID [string]: ID of the signed document to execute this CreditNote. ex: "4545454545454545"</item>
    ///     <item>Status [string]: current status of the CreditNote. ex: "canceled", "created", "expired", "failed", "processing", "signed", "success"</item>
    ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this CreditNote. ex: new List<string>{ "19827356981273" }</item>
    ///     <item>WorkspaceID [string]: ID of the Workspace that generated this CreditNote. ex: "4545454545454545"</item>
    ///     <item>TaxAmount [integer]: tax amount included in the CreditNote. ex: 100</item>
    ///     <item>NominalInterest [float]: yearly nominal interest rate of the CreditNote, in percentage. ex: 11.5</item>
    ///     <item>Interest [float]: yearly effective interest rate of the CreditNote, in percentage. ex: 12.5</item>
    ///     <item>Created [DateTime]: creation DateTime for the CreditNote. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the CreditNote. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class CreditNote : Resource
    {
        public string TemplateID { get; }
        public string Name { get; }
        public string TaxID { get; }
        public DateTime? Scheduled { get; }
        public List<Invoice> Invoices { get; }
        public Resource Payment { get; }
        public List<CreditSigner> Signers { get; }
        public string ExternalID { get; }
        public string StreetLine1 { get; }
        public string StreetLine2 { get; }
        public string District { get; }
        public string City { get; }
        public string StateCode { get; }
        public string ZipCode { get; }
        public string PaymentType { get; }
        public long? NominalAmount { get; }
        public long? RebateAmount { get; }
        public long? Amount { get; }
        public List<string> Tags { get; }
        public string Expiration { get; }
        public string DocumentID { get; }
        public string Status { get; }
        public List<string> TransactionIds { get; }
        public string WorkspaceID { get; }
        public long? TaxAmount { get; }
        public float? Interest { get; }
        public float? NominalInterest { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// CreditNote object
        /// <br/>
        /// CreditNotes are used to generate CCB contracts between you and your customers.
        /// <br/>
        /// When you initialize a CreditNote, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>templateID [string]: ID of the contract template on which the CreditNote will be based. ex: "0123456789101112"</item>
        ///     <item>name [string]: credit receiver's full name. ex: "Edward Stark"</item>
        ///     <item>taxID [string]: credit receiver's tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
        ///     <item>scheduled [DateTime]: date of payment execution. ex: DateTime(2020, 3, 10)</item>
        ///     <item>invoices [list of CreditNote.Invoice objects]: list of Invoice objects to be created and sent to the credit receiver. ex: new List<Invoice>{ new Invoice(), new Invoice() }</item>
        ///     <item>payment [CreditNote.Transfer object]: Payment entity to be created and sent to the credit receiver. ex: Transfer()</item>
        ///     <item>signers [List of CreditSigner objects]: signer's name, e-mail and delivery method for the contract. ex: new List<CreditSigner>{ new CreditSigner(name: "Tony Stark", contact: "tony@starkindustries.com", method: "link")}</item>
        ///     <item>externalID [string]: a string that must be unique among all your CreditNotes, used to avoid resource duplication. ex: "my-internal-id-123456"</item>
        ///     <item>streetLine1 [string]: credit receiver main address. ex: "Av. Paulista, 200"</item>
        ///     <item>streetLine2 [string]: credit receiver address complement. ex: "Apto. 123"</item>
        ///     <item>district [string]: credit receiver address district / neighbourhood. ex: "Bela Vista"</item>
        ///     <item>city [string]: credit receiver address city. ex: "Rio de Janeiro"</item>
        ///     <item>stateCode [string]: credit receiver address state. ex: "GO"</item>
        ///     <item>zipCode [string]: credit receiver address zip code. ex: "01311-200"</item>
        /// </list>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>paymentType [string]: payment type, inferred from the payment parameter if it is not a dictionary. ex: "transfer"</item>
        ///     <item>nominalAmount [integer]: CreditNote value in cents. The nominalAmount parameter is required when amount is not sent. ex: 1234 (= R$ 12.34)</item>
        ///     <item>amount [integer]: amount in cents transferred to the credit receiver, before deductions. The amount parameter is required when nominalAmount is not sent. ex: 1234 (= R$ 12.34)</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>rebateAmount [integer, default null]: credit analysis fee deducted from lent amount. ex: 11234 (= R$ 112.34)</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for CreditNotes. ex: new List<string>{ "employees", "monthly" }</item>
        ///     <item>expiration [integer, default 604800 (7 days)]: time interval in seconds between scheduled date and expiration. ex: 123456789</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the CreditNote is created. ex: "5656565656565656"</item>
        ///     <item>documentID [string]: ID of the signed document to execute this CreditNote. ex: "4545454545454545"</item>
        ///     <item>status [string]: current status of the CreditNote. ex: "canceled", "created", "expired", "failed", "processing", "signed", "success"</item>
        ///     <item>transactionIds [list of strings]: ledger transaction ids linked to this CreditNote. ex: new List<string>{ "19827356981273" }</item>
        ///     <item>workspaceID [string]: ID of the Workspace that generated this CreditNote. ex: "4545454545454545"</item>
        ///     <item>taxAmount [integer]: tax amount included in the CreditNote. ex: 100</item>
        ///     <item>nominalInterest [float]: yearly nominal interest rate of the CreditNote, in percentage. ex: 11.5</item>
        ///     <item>interest [float]: yearly effective interest rate of the CreditNote, in percentage. ex: 12.5</item>
        ///     <item>created [DateTime]: creation DateTime for the CreditNote. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update DateTime for the CreditNote. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public CreditNote(
            string templateID, string name, string taxID, DateTime? scheduled, 
            List<Invoice> invoices, Resource payment, List<CreditSigner> signers, 
            string externalID, string streetLine1, string streetLine2, string district, 
            string city, string stateCode, string zipCode, long? nominalAmount = null, string paymentType = null, 
            long? amount = null, long? rebateAmount = null, List<string> tags = null, string expiration = null, 
            string id = null, string documentID = null, string status = null, 
            List<string> transactionIds = null, string workspaceID = null, long? taxAmount = null, 
            float? nominalInterest = null, float? interest = null, DateTime? created = null, 
            DateTime? updated = null
        )  : base(id)
        {
            TemplateID = templateID;
            Name = name;
            TaxID = taxID;
            Scheduled = scheduled;
            Invoices = invoices;
            Payment = payment;
            Signers = signers;
            ExternalID = externalID;
            StreetLine1 = streetLine1;
            StreetLine2 = streetLine2;
            District = district;
            City = city;
            StateCode = stateCode;
            ZipCode = zipCode;
            NominalAmount = nominalAmount;
            Amount = amount;
            RebateAmount = rebateAmount;
            Tags = tags;
            Expiration = expiration;
            DocumentID = documentID;
            Status = status;
            TransactionIds = transactionIds;
            WorkspaceID = workspaceID;
            TaxAmount = taxAmount;
            NominalInterest = nominalInterest;
            Interest = interest;
            Created = created;
            Updated = updated;

            if (PaymentType == null)
            {
                PaymentType = GetType(Payment);
            }
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
        /// Create CreditNote objects
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
        /// Create CreditNote objects
        /// <br/>
        /// Send a list of CreditNote objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>notes [list of Dictionaries]: list of Dictionaries representing the CreditNote objects to be created in the API</item>
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
        ///     <item>id [string]: CreditNote unique id. ex: "5656565656565656"</item>
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
        /// Retrieve CreditNote objects
        /// <br/>
        /// Receive an IEnumerable of CreditNote objects previously created in the Stark Infra API
        /// <br/>
        /// Use this function instead of page if you want to stream the objects without worrying about cursors and pagination.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: new List<string>{ "canceled", "created", "expired", "failed", "processing", "signed", "success" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of CreditNote objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<CreditNote> Query(
            int? limit = null, DateTime? after = null, DateTime? before = null, 
            List<string> status = null, List<string> tags = null, List<string> ids = null,
            User user = null
        )
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    {"limit", limit },
                    {"after", after },
                    {"before", before },
                    {"status", status },
                    {"tags", tags },
                    {"ids", ids }
                },
                user: user
            ).Cast<CreditNote>();
        }

        /// <summary>
        /// Retrieve paged CreditNote objects
        /// <br/>
        /// Receive a list of up to 100 CreditNote objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of string, default null]: filter for status of retrieved objects. ex: new List<string>{ "canceled", "created", "expired", "failed", "processing", "signed", "success" }</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
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
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
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
            string templateID = json.templateId;
            string name = json.name;
            string taxID = json.taxId;
            string scheduledString = json.scheduled;
            DateTime scheduled = Checks.CheckDateTime(scheduledString);
            string externalID = json.externalId;
            string streetLine1 = json.streetLine1;
            string streetLine2 = json.streetLine2;
            string district = json.district;
            string city = json.city;
            string stateCode = json.stateCode;
            string zipCode = json.zipCode;
            string paymentType = json.paymentType;
            long? nominalAmount = json.nominalAmount;
            long? amount = json.amount;
            long? rebateAmount = json.rebateAmount;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string expiration = json.expiration;
            string documentID = json.documentId;
            string status = json.status;
            List<string> transactionIds = json.transactionIds.ToObject<List<string>>();
            string workspaceID = json.workspaceId;
            long? taxAmount = json.taxAmount;
            float? interest = json.interest;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            List<CreditSigner> signers = ParseSigners(json.signers);
            Resource payment = ParsePayment(json: json.payment, paymentType: json.paymentType.ToObject<string>());
            List<Invoice> invoices = ParseInvoice(json.invoices);

            return new CreditNote(
                id: id, templateID: templateID, name: name, taxID: taxID, scheduled: scheduled, invoices: invoices, payment: payment, 
                signers: signers, externalID: externalID, streetLine1: streetLine1, streetLine2: streetLine2, district: district, 
                city: city, stateCode: stateCode, zipCode: zipCode, paymentType: paymentType, nominalAmount: nominalAmount, amount: amount,
                rebateAmount: rebateAmount, tags: tags, expiration: expiration, documentID: documentID, status: status, transactionIds: transactionIds,
                workspaceID: workspaceID, taxAmount: taxAmount, interest: interest, created: created, updated: updated
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

        private static List<CreditSigner> ParseSigners(dynamic json)
        {
            List<CreditSigner> signers = new List<CreditSigner>();

            foreach (dynamic signer in json)
            {
                signers.Add(CreditSigner.ResourceMaker(signer));
            }
            return signers;
        }
    }
}
