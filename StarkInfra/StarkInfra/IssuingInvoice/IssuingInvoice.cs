using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingInvoice object
    /// <br/>
    /// The IssuingInvoice objects created in your Workspace load your Issuing balance when paid.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long]: IssuingInvoice value in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>TaxID [string, default sub-issuer tax ID]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Name [string, default sub-issuer name]: payer name. ex: "Iron Infra S.A."</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID[string]: unique id returned when IssuingInvoice is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current IssuingHolder status. ex: "created", "expired", "overdue" and "paid"</item>
    ///     <item>IssuingTransactionID [string]: ledger transaction ids linked to this IssuingInvoice. ex: "issuing-invoice/5656565656565656"</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingInvoice : Resource
    {
        public long Amount { get; }
        public string TaxID { get; }
        public string Name { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public string IssuingTransactionID { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingInvoice object
        /// <br/>
        /// The IssuingInvoice objects created in your Workspace load your Issuing balance when paid.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long]: IssuingInvoice value in cents. ex: 1234 (= R$ 12.34)</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>taxId [string, default sub-issuer tax ID]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>name [string, default sub-issuer name]: payer name. ex: "Iron Infra S.A."</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingInvoice is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current IssuingHolder status. ex: "created", "expired", "overdue" and "paid"</item>
        ///     <item>issuingTransactionId [string]: ledger transaction ids linked to this IssuingInvoice. ex: "issuing-invoice/5656565656565656"</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingInvoice(long amount, string taxId = null, string name = null, List<string> tags = null, string id = null, string status = null, 
            string issuingTransactionId = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Amount = amount;
            TaxID = taxId;
            Name = name;
            Tags = tags;
            Status = status;
            IssuingTransactionID = issuingTransactionId;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create IssuingInvoices
        /// <br/>
        /// Send a list of IssuingInvoice objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>IssuingInvoices [list of IssuingInvoice objects]: list of IssuingInvoice objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingInvoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingInvoice Create(IssuingInvoice IssuingInvoice, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: IssuingInvoice,
                user: user
            ) as IssuingInvoice;
        }

        /// <summary>
        /// Create IssuingInvoices
        /// <br/>
        /// Send a list of IssuingInvoice objects for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>invoices [list of dictionaries]: list of dictionaries representing the IssuingInvoices to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingInvoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<IssuingInvoice> Create(List<Dictionary<string, object>> invoices, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: invoices,
                user: user
            ).ToList().ConvertAll(o => (IssuingInvoice)o);
        }

        /// <summary>
        /// Retrieve a specific IssuingInvoice
        /// <br/>
        /// Receive a single IssuingInvoice object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingInvoice object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingInvoice Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingInvoice;
        }

        /// <summary>
        /// Retrieve IssuingInvoices
        /// <br/>
        /// Receive an IEnumerable of IssuingInvoice objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "expired", "overdue" and "paid"</item>
        ///     <item>after [DateTime or string, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingInvoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingInvoice> Query(string status = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, int? limit = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "status", status },
                    { "after", after },
                    { "before", before },
                    { "tags", tags },
                    { "limit", limit }
                },
                user: user
            ).Cast<IssuingInvoice>();
        }

        /// <summary>
        /// Retrieve paged IssuingInvoices
        /// <br/>
        /// Receive a list of up to 100 IssuingInvoice objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "expired", "overdue" and "paid"</item>
        ///     <item>after [DateTime or string, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime or string, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingInvoice objects with updated attributes and cursor to retrieve the next page of IssuingInvoice objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingInvoice> page, string pageCursor) Page(string cursor = null, string status = null, DateTime? after = null,
            DateTime? before = null, List<string> tags = null, int? limit = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "status", status },
                    { "after", after },
                    { "before", before },
                    { "tags", tags },
                    { "limit", limit }
                },
                user: user
            );
            List<IssuingInvoice> issuingInvoices = new List<IssuingInvoice>();
            foreach (SubResource subResource in page)
            {
                issuingInvoices.Add(subResource as IssuingInvoice);
            }
            return (issuingInvoices, pageCursor);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingInvoice", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string taxId = json.taxId;
            string name = json.name;
            List<string> tags = json.tags.ToObject<List<string>>();
            string status = json.status;
            string issuingTransactionId = json.issuingTransactionId;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new IssuingInvoice(
                id: id, amount: amount, taxId: taxId, name: name, tags: tags, status: status,
                issuingTransactionId: issuingTransactionId, updated: updated, created: created
            );
        }
    }
}
