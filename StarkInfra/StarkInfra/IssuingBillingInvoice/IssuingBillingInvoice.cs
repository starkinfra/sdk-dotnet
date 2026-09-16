using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingBillingInvoice object
    /// <br/>
    /// The IssuingBillingInvoice object displays the invoices created in your Workspace to collect the amount due from issuing usage.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when IssuingBillingInvoice is created. ex: "5656565656565656"</item>
    ///     <item>TaxID [string]: payer tax id. ex: "012.345.678-90"</item>
    ///     <item>Name [string]: payer name. ex: "Iron Bank S.A."</item>
    ///     <item>Fine [double]: Fine percentage applied when paid after the due date. ex: 2.0</item>
    ///     <item>Interest [double]: Monthly interest percentage applied when paid after the due date. ex: 1.0</item>
    ///     <item>Amount [long]: invoice amount, in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>NominalAmount [long]: nominal amount of the invoice, in cents. ex: 1200 (= R$ 12.00)</item>
    ///     <item>Status [string]: current invoice status. ex: "created", "paid", "overdue"</item>
    ///     <item>Brcode [string]: BR Code for the invoice payment. ex: "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A"</item>
    ///     <item>Link [string]: public invoice webpage URL. ex: "https://starkbank-card-issuer.sandbox.starkbank.com/billinginvoicelink/97de4d51e8984c459639a645ce920abb"</item>
    ///     <item>Due [DateTime]: invoice due datetime. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Start [DateTime]: billing cycle start datetime. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>End [DateTime]: billing cycle end datetime. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingBillingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingBillingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingBillingInvoice : Resource
    {
        public string TaxID { get; }
        public string Name { get; }
        public double? Fine { get; }
        public double? Interest { get; }
        public long? Amount { get; }
        public long? NominalAmount { get; }
        public string Status { get; }
        public string Brcode { get; }
        public string Link { get; }
        public DateTime? Due { get; }
        public DateTime? Start { get; }
        public DateTime? End { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// IssuingBillingInvoice object
        /// <br/>
        /// The IssuingBillingInvoice object displays the invoices created in your Workspace to collect the amount due from issuing usage.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingBillingInvoice is created. ex: "5656565656565656"</item>
        ///     <item>taxId [string]: payer tax id. ex: "012.345.678-90"</item>
        ///     <item>name [string]: payer name. ex: "Iron Bank S.A."</item>
        ///     <item>fine [double]: Fine percentage applied when paid after the due date. ex: 2.0</item>
        ///     <item>interest [double]: Monthly interest percentage applied when paid after the due date. ex: 1.0</item>
        ///     <item>amount [long]: invoice amount, in cents. ex: 1234 (= R$ 12.34)</item>
        ///     <item>nominalAmount [long]: nominal amount of the invoice, in cents. ex: 1200 (= R$ 12.00)</item>
        ///     <item>status [string]: current invoice status. ex: "created", "paid", "overdue"</item>
        ///     <item>brcode [string]: BR Code for the invoice payment. ex: "00020126580014br.gov.bcb.pix..."</item>
        ///     <item>link [string]: public invoice webpage URL. ex: "https://starkbank-card-issuer.sandbox.starkbank.com/billinginvoicelink/97de4d51e8984c459639a645ce920abb"</item>
        ///     <item>due [DateTime]: invoice due datetime. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>start [DateTime]: billing cycle start datetime. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>end [DateTime]: billing cycle end datetime. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingBillingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingBillingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingBillingInvoice(
            string id = null, string taxID = null, string name = null, double? fine = null, double? interest = null,
            long? amount = null, long? nominalAmount = null, string status = null, string brcode = null,
            string link = null, DateTime? due = null, DateTime? start = null, DateTime? end = null,
            DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            TaxID = taxID;
            Name = name;
            Fine = fine;
            Interest = interest;
            Amount = amount;
            NominalAmount = nominalAmount;
            Status = status;
            Brcode = brcode;
            Link = link;
            Due = due;
            Start = start;
            End = end;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Retrieve a specific IssuingBillingInvoice object
        /// <br/>
        /// Receive a single IssuingBillingInvoice object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingBillingInvoice object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingBillingInvoice Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingBillingInvoice;
        }

        /// <summary>
        /// Retrieve IssuingBillingInvoice objects
        /// <br/>
        /// Receive an IEnumerable of IssuingBillingInvoice objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created", "paid" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingBillingInvoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingBillingInvoice> Query(int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> status = null, List<string> tags = null, List<string> ids = null,
            User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            ).Cast<IssuingBillingInvoice>();
        }

        /// <summary>
        /// Retrieve paged IssuingBillingInvoice objects
        /// <br/>
        /// Receive a list of up to 100 IssuingBillingInvoice objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: new DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "created", "paid" }</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingBillingInvoice objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingBillingInvoice objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingBillingInvoice> page, string pageCursor) Page(string cursor = null, int? limit = null,
            DateTime? after = null, DateTime? before = null, List<string> status = null, List<string> tags = null,
            List<string> ids = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids }
                },
                user: user
            );
            List<IssuingBillingInvoice> invoices = new List<IssuingBillingInvoice>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                invoices.Add(subResource as IssuingBillingInvoice);
            }
            return (invoices, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingBillingInvoice", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string taxID = json.taxId;
            string name = json.name;
            double? fine = json.fine;
            double? interest = json.interest;
            long? amount = json.amount;
            long? nominalAmount = json.nominalAmount;
            string status = json.status;
            string brcode = json.brcode;
            string link = json.link;
            string dueString = json.due;
            DateTime? due = StarkCore.Utils.Checks.CheckDateTime(dueString);
            string startString = json.start;
            DateTime? start = StarkCore.Utils.Checks.CheckDateTime(startString);
            string endString = json.end;
            DateTime? end = StarkCore.Utils.Checks.CheckDateTime(endString);
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new IssuingBillingInvoice(
                id: id, taxID: taxID, name: name, fine: fine, interest: interest, amount: amount,
                nominalAmount: nominalAmount, status: status, brcode: brcode, link: link, due: due,
                start: start, end: end, created: created, updated: updated
            );
        }
    }
}
