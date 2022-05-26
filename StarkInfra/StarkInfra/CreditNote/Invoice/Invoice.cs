using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// Invoice object
    /// <br/>
    /// Invoice issued after the contract is signed, to be paid by the credit receiver.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [integer]: Invoice value in cents. Minimum = 1 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
    ///     <item>Due [DateTime or string, default now + 2 days]: Invoice due date in UTC ISO format. ex: "2020-10-28T17:59:26.249976+00:00" for immediate invoices and "2020-10-28" for scheduled invoices</item>
    ///     <item>Expiration [integer, default 5097600 (59 days)]: time interval in seconds between due date and expiration date. ex: 123456789</item>
    ///     <item>Fine [float, default 2.0]: Invoice fine for overdue payment in %. ex: 2.5</item>
    ///     <item>Interest [float, default 1.0]: Invoice monthly interest for overdue payment in %. ex: 5.2</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging</item>
    ///     <item>Descriptions [list of Description objects, default null]: list of Description objects or dictionaries with "key":string and (optional) "value":string pairs</item>
    ///     <item>Name [string]: payer name. ex: "Iron Bank S.A."</item>
    ///     <item>TaxID [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Pdf [string]: public Invoice PDF URL. ex: "https://invoice.starkbank.com/pdf/d454fa4e524441c1b0c1a729457ed9d8"</item>
    ///     <item>Link [string]: public Invoice webpage URL. ex: "https://my-workspace.sandbox.starkbank.com/invoicelink/d454fa4e524441c1b0c1a729457ed9d8"</item>
    ///     <item>NominalAmount [integer]: Invoice emission value in cents (will change if invoice is updated, but not if it's paid). ex: 400000</item>
    ///     <item>FineAmount [integer]: Invoice fine value calculated over nominalAmount. ex: 20000</item>
    ///     <item>InterestAmount [integer]: Invoice interest value calculated over nominalAmount. ex: 10000</item>
    ///     <item>DiscountAmount [integer]: Invoice discount value calculated over nominalAmount. ex: 3000</item>
    ///     <item>ID [string]: unique id returned when Invoice is created. ex: "5656565656565656"</item>
    ///     <item>Brcode [string]: BR Code for the Invoice payment. ex: "00020101021226800014br.gov.bcb.pix2558invoice.starkbank.com/f5333103-3279-4db2-8389-5efe335ba93d5204000053039865802BR5913Arya Stark6009Sao Paulo6220051656565656565656566304A9A0"</item>
    ///     <item>Status [string]: current Invoice status. ex: "registered" or "paid"</item>
    ///     <item>Fee [integer]: fee charged by this Invoice. ex: 200 (= R$ 2.00)</item>
    ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this Invoice (if there are more than one, all but the first are reversals or failed reversal chargebacks). ex: ["19827356981273"]</item>
    ///     <item>Discounts [list of Discount objects]: list of Discount objects or dictionaries with "percentage":float and "due":datetime.datetime or string pairs</item>
    ///     <item>Created [DateTime]: creation datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class Invoice : Resource
    {
        public long Amount { get; }
        public DateTime? Due { get; }
        public string Expiration { get; }
        public float? Fine { get; }
        public float? Interest { get; }
        public List<Discount> Discounts { get; }
        public List<string> Tags { get; }
        public List<Description> Descriptions { get; }
        public string Name { get; }
        public string TaxID { get; }
        public string Pdf { get; }
        public string Link { get; }
        public long? NominalAmount { get; }
        public long? FineAmount { get; }
        public long? InterestAmount { get; }
        public long? DiscountAmount { get; }
        public string Brcode { get; }
        public string Status { get; }
        public long? Fee { get; }
        public string TransactionIds { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// Invoice object
        /// <br/>
        /// Invoice issued after the contract is signed, to be paid by the credit receiver.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [integer]: Invoice value in cents. Minimum = 1 (any value will be accepted). ex: 1234 (= R$ 12.34)</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>due [DateTime or string, default now + 2 days]: Invoice due date in UTC ISO format. ex: "2020-10-28T17:59:26.249976+00:00" for immediate invoices and "2020-10-28" for scheduled invoices</item>
        ///     <item>expiration [integer, default 5097600 (59 days)]: time interval in seconds between due date and expiration date. ex: 123456789</item>
        ///     <item>fine [float, default 2.0]: Invoice fine for overdue payment in %. ex: 2.5</item>
        ///     <item>interest [float, default 1.0]: Invoice monthly interest for overdue payment in %. ex: 5.2</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging</item>
        ///     <item>descriptions [list of CreditNote.Invoice.Description objects, default null]: list of Description objects or dictionaries with "key":string and (optional) "value":string pairs</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>name [string]: payer name. ex: "Iron Bank S.A."</item>
        ///     <item>taxId [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>pdf [string]: public Invoice PDF URL. ex: "https://invoice.starkbank.com/pdf/d454fa4e524441c1b0c1a729457ed9d8"</item>
        ///     <item>link [string]: public Invoice webpage URL. ex: "https://my-workspace.sandbox.starkbank.com/invoicelink/d454fa4e524441c1b0c1a729457ed9d8"</item>
        ///     <item>nominalAmount [integer]: Invoice emission value in cents (will change if invoice is updated, but not if it's paid). ex: 400000</item>
        ///     <item>fineAmount [integer]: Invoice fine value calculated over nominalAmount. ex: 20000</item>
        ///     <item>interestAmount [integer]: Invoice interest value calculated over nominalAmount. ex: 10000</item>
        ///     <item>discountAmount [integer]: Invoice discount value calculated over nominalAmount. ex: 3000</item>
        ///     <item>id [string]: unique id returned when Invoice is created. ex: "5656565656565656"</item>
        ///     <item>brcode [string]: BR Code for the Invoice payment. ex: "00020101021226800014br.gov.bcb.pix2558invoice.starkbank.com/f5333103-3279-4db2-8389-5efe335ba93d5204000053039865802BR5913Arya Stark6009Sao Paulo6220051656565656565656566304A9A0"</item>
        ///     <item>status [string]: current Invoice status. ex: "registered" or "paid"</item>
        ///     <item>fee [integer]: fee charged by this Invoice. ex: 200 (= R$ 2.00)</item>
        ///     <item>transactionIds [list of strings]: ledger transaction ids linked to this Invoice (if there are more than one, all but the first are reversals or failed reversal chargebacks). ex: ["19827356981273"]</item>
        ///     <item>discounts [list of CreditNote.Invoice.Discount objects]: list of Discount objects or dictionaries with "percentage":float and "due":datetime.datetime or string pairs</item>
        ///     <item>created [DateTime]: creation datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the Invoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Invoice(long amount, DateTime? due = null, string expiration = null, float? fine = null, float? interest = null,
                List<string> tags = null, List<Description> descriptions = null, string name = null, string taxId = null,
                string pdf = null, string link = null, long? nominalAmount = null, long? fineAmount = null, long? interestAmount = null,
                long? discountAmount = null, string id = null, string brcode = null, string status = null, long? fee = null, string transactionIds = null,
                List<Discount> discounts = null, DateTime? created = null, DateTime? updated = null) : base(id)
        {
            Amount = amount;
            Due = due;
            Expiration = expiration;
            Fine = fine;
            Interest = interest;
            Discounts = discounts;
            Tags = tags;
            Descriptions = descriptions;
            Name = name;
            TaxID = taxId;
            Pdf = pdf;
            Link = link;
            NominalAmount = nominalAmount;
            FineAmount = fineAmount;
            InterestAmount = interestAmount;
            DiscountAmount = discountAmount;
            Brcode = brcode;
            Status = status;
            Fee = fee;
            TransactionIds = transactionIds;
            Created = created;
            Updated = updated;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Invoice", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string dueString = json.due;
            DateTime? due = Checks.CheckDateTime(dueString);
            string expiration = json.expiration;
            float? fine = json.fine;
            float? interest = json.interest;
            List<Discount> discounts = ParseDiscount(json.discounts);
            List<string> tags = json.tags.ToObject<List<string>>();
            List<Description> descriptions = ParseDescription(json.descriptions);
            string name = json.name;
            string taxId = json.taxId;
            string pdf = json.pdf;
            string link = json.link;
            long? nominalAmount = json.nominalAmount;
            long? fineAmount = json.fineAmount;
            long? interestAmount = json.interestAmount;
            long? discountAmount = json.discountAmount;
            string brcode = json.brcode;
            string status = json.status;
            long? fee = json.fee;
            string transactionIds = json.transactionIds;
            DateTime? created = json.created;
            if(created != null) created = Checks.CheckDateTime(json.created);
            DateTime? updated = json.created;
            if (updated != null) updated = Checks.CheckDateTime(json.updated);

            return new Invoice(
                id: id, amount: amount, due: due, expiration: expiration, fine: fine, interest: interest, discounts: discounts, tags: tags,
                descriptions: descriptions, name: name, taxId: taxId, pdf: pdf, link: link, nominalAmount: nominalAmount, fineAmount: fineAmount,
                interestAmount: interestAmount, discountAmount: discountAmount, brcode: brcode, status: status, fee: fee, transactionIds: transactionIds,
                created: created, updated: updated
            );
        }

        private static List<Discount> ParseDiscount(dynamic json)
        {
            List<Discount> discounts = new List<Discount>();

            foreach (dynamic discount in json)
            {
                discounts.Add(Discount.ResourceMaker(discount));
            }
            return discounts;
        }

        private static List<Description> ParseDescription(dynamic json)
        {
            List<Description> descriptions = new List<Description>();

            foreach (dynamic description in json)
            {
                descriptions.Add(Description.ResourceMaker(description));
            }
            return descriptions;
        }
    }
}
