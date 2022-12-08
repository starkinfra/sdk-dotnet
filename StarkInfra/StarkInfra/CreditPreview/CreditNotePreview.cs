using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    public partial class CreditPreview
    {
        /// <summary>
        /// CreditNotePreview object
        /// <br/>
        /// A CreditNotePreview is used to preview a CCB contract between the borrower and lender with a specific table type.
        /// <br/>
        /// When you initialize a CreditNotePreview, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>type [string]: table type that defines the amortization system. Options: "sac", "price", "american", "bullet", "custom"</item>
        ///     <item>nominalAmount [integer]: amount in cents transferred to the credit receiver, before deductions. ex: 11234 (= R$ 112.34)</item>
        ///     <item>scheduled [DateTime]: date of transfer execution. ex: DateTime(2020, 3, 10)</item>
        ///     <item>taxID [string]: credit receiver's tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
        ///     <item>invoices [list of CreditNote.Invoice objects]: list of Invoice objects to be created and sent to the credit receiver. ex: new List<Invoice> { Invoice(), Invoice() }</item>
        ///     <item>nominalInterest [float]: yearly nominal interest rate of the CreditNote, in percentage. ex: 12.5</item>
        ///     <item>initialDue [DateTime]: date of the first invoice. ex: DateTime(2020, 3, 10)</item>
        ///     <item>count [integer]: quantity of invoices for payment. ex: 12</item>
        ///     <item>initialAmount [integer]: value of the first invoice in cents. ex: 1234 (= R$12.34)</item>
        ///     <item>interval [string]: interval between invoices. Options: "year", "month"</item>
        ///     <item>rebateAmount [integer, default null]: credit analysis fee deducted from lent amount. ex: 11234 (= R$ 112.34)</item>
        ///     <item>amount [integer]: CreditNote value in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>interest [float]: yearly effective interest rate of the CreditNote, in percentage. ex: 12.5</item>
        ///     <item>taxAmount [integer]: tax amount included in the CreditNote. ex: 100</item>
        /// </list>
        /// </summary>
        public class CreditNotePreview : SubResource
        {
            public string Type { get; }
            public int NominalAmount { get; }
            public DateTime? Scheduled { get; }
            public string TaxID { get; }
            public List<Invoice> Invoices { get; }
            public float? NominalInterest { get; }
            public DateTime? InitialDue { get; }
            public int? Count { get; }
            public int? InitialAmount { get; }
            public string Interval { get; }
            public int? RebateAmount { get; }
            public int? Amount { get; }
            public float? Interest { get; }
            public int? TaxAmount { get; }

            /// <summary>
            /// CreditNotePreview object
            /// <br/>
            /// A CreditNotePreview is used to preview a CCB contract between the borrower and lender with a specific table type.
            /// <br/>
            /// When you initialize a CreditNotePreview, the entity will not be automatically
            /// created in the Stark Infra API. The 'create' function sends the objects
            /// to the Stark Infra API and returns the created object.
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>type [string]: table type that defines the amortization system. Options: "sac", "price", "american", "bullet", "custom"</item>
            ///     <item>nominalAmount [integer]: amount in cents transferred to the credit receiver, before deductions. ex: 11234 (= R$ 112.34)</item>
            ///     <item>scheduled [DateTime]: date of transfer execution. ex: DateTime(2020, 3, 10)</item>
            ///     <item>taxID [string]: credit receiver's tax ID (CPF or CNPJ). ex: "20.018.183/0001-80"</item>
            /// </list>
            /// Parameters (conditionally required):
            /// <list>
            ///     <item>invoices [list of CreditNote.Invoice objects]: list of Invoice objects to be created and sent to the credit receiver. ex: new List<string> { Invoice(), Invoice() }</item>
            ///     <item>nominalInterest [float]: yearly nominal interest rate of the CreditNote, in percentage. ex: 12.5</item>
            ///     <item>initialDue [DateTime]: date of the first invoice. ex: DateTime(2020, 3, 10)</item>
            ///     <item>count [integer]: quantity of invoices for payment. ex: 12</item>
            ///     <item>initialAmount [integer]: value of the first invoice in cents. ex: 1234 (= R$12.34)</item>
            ///     <item>interval [string]: interval between invoices. Options: "year", "month"</item>
            /// </list>
            /// Parameters (optional):
            /// <list>
            ///     <item>rebateAmount [integer, default null]: credit analysis fee deducted from lent amount. ex: 11234 (= R$ 112.34)</item>
            /// </list>
            /// Attributes (return-only):
            /// <list>
            ///     <item>amount [integer]: CreditNote value in cents. ex: 1234 (= R$ 12.34)</item>
            ///     <item>interest [float]: yearly effective interest rate of the CreditNote, in percentage. ex: 12.5</item>
            ///     <item>taxAmount [integer]: tax amount included in the CreditNote. ex: 100</item>
            /// </list>
            /// </summary>
            public CreditNotePreview(
                string type, int nominalAmount, DateTime? scheduled, string taxID, 
                List<Invoice> invoices = null, float? nominalInterest = null, 
                DateTime? initialDue = null, int? count = null, int? initialAmount = null, 
                string interval = null, int? rebateAmount = null, int? amount = null,
                float? interest = null, int? taxAmount = null
            )
            {
                Type = type;
                NominalAmount = nominalAmount;
                Scheduled = scheduled;
                TaxID = taxID;
                Invoices = invoices;
                NominalInterest = nominalInterest;
                InitialDue = initialDue;
                Count = count;
                InitialAmount = initialAmount;
                Interval = interval;
                RebateAmount = rebateAmount;
                Amount = amount;
                Interest = interest;
                TaxAmount = taxAmount;
            }

            internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "CreditNotePreview", resourceMaker: ResourceMaker);
            }

            internal static Utils.SubResource ResourceMaker(dynamic json)
            {
                string type = json.type;
                int nominalAmount = json.nominalAmount;
                string scheduledString = json.scheduled;
                DateTime? scheduled = Checks.CheckDateTime(scheduledString);
                string taxID = json.taxId;
                List<Invoice> invoices = ParseInvoice(json.invoices);
                float nominalInterest = json.nominalInterest;
                DateTime initialDue = json.initialDue;
                int count = json.count;
                int initialAmount = json.initialAmount;
                string interval = json.interval;
                int? rebateAmount = json.rebateAmount;
                int amount = json.amount;
                float interest = json.interest;
                int taxAmount = json.taxAmount;
                

                return new CreditNotePreview(
                    type: type, nominalAmount: nominalAmount, scheduled: scheduled, taxID: taxID,
                    invoices: invoices, nominalInterest: nominalInterest, initialDue: initialDue,
                    count: count, initialAmount: initialAmount, interval: interval,
                    rebateAmount: rebateAmount, amount: amount, interest: interest,
                    taxAmount: taxAmount
                );
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
        }
    }
}
