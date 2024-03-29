﻿using System;
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
    /// When you initialize a IssuingInvoice, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Amount [long]: IssuingInvoice value in cents. ex: 1234 (= R$ 12.34)</item>
    ///     <item>TaxID [string, default sub-issuer tax ID]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Name [string, default sub-issuer name]: payer name. ex: "Iron Infra S.A."</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID[string]: unique id returned when IssuingInvoice is created. ex: "5656565656565656"</item>
    ///     <item>Brcode [string]: BR Code for the Invoice payment. ex: "00020101021226930014br.gov.bcb.pix2571brcode-h.development.starkinfra.com/v2/d7f6546e194d4c64a153e8f79f1c41ac5204000053039865802BR5925Stark Bank S.A. - Institu6009Sao Paulo62070503***63042109"</item>
    ///     <item>Due [DateTime]: Invoice due and expiration date in UTC ISO format. ex: DateTime(2020, 10, 28)</item>
    ///     <item>Link [string]: public Invoice webpage URL. ex: "https://starkbank-card-issuer.development.starkbank.com/invoicelink/d7f6546e194d4c64a153e8f79f1c41ac"</item>
    ///     <item>Status [string]: current IssuingInvoice status status. Options: "created", "expired", "overdue" and "paid"</item>
    ///     <item>IssuingTransactionID [string]: ledger transaction ids linked to this IssuingInvoice. ex: "issuing-invoice/5656565656565656"</item>
    ///     <item>Updated [DateTime]: latest update DateTime for the IssuingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation DateTime for the IssuingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingInvoice : Resource
    {
        public long Amount { get; }
        public string TaxID { get; }
        public string Name { get; }
        public List<string> Tags { get; }
        public string Brcode { get; }
        public DateTime? Due { get; }
        public string Link { get; }
        public string Status { get; }
        public string IssuingTransactionID { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingInvoice object
        /// <br/>
        /// The IssuingInvoice objects created in your Workspace load your Issuing balance when paid.
        /// <br/>
        /// When you initialize a IssuingInvoice, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>amount [long]: IssuingInvoice value in cents. ex: 1234 (= R$ 12.34)</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>taxID [string, default sub-issuer tax ID]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>name [string, default sub-issuer name]: payer name. ex: "Iron Infra S.A."</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingInvoice is created. ex: "5656565656565656"</item>
        ///     <item>brcode [string]: BR Code for the Invoice payment. ex: "00020101021226930014br.gov.bcb.pix2571brcode-h.development.starkinfra.com/v2/d7f6546e194d4c64a153e8f79f1c41ac5204000053039865802BR5925Stark Bank S.A. - Institu6009Sao Paulo62070503***63042109"</item>
        ///     <item>due [DateTime]: Invoice due and expiration date in UTC ISO format. ex: DateTime(2020, 10, 28)</item>
        ///     <item>link [string]: public Invoice webpage URL. ex: "https://starkbank-card-issuer.development.starkbank.com/invoicelink/d7f6546e194d4c64a153e8f79f1c41ac"</item>
        ///     <item>status [string]: current IssuingInvoice status status. ex: "created", "expired", "overdue" and "paid"</item>
        ///     <item>issuingTransactionID [string]: ledger transaction ids linked to this IssuingInvoice. ex: "issuing-invoice/5656565656565656"</item>
        ///     <item>updated [DateTime]: latest update DateTime for the IssuingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation DateTime for the IssuingInvoice. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingInvoice(long amount, string taxID = null, string name = null, 
            List<string> tags = null, string id = null, string brcode = null, 
            DateTime? due = null, string link = null, string status = null, string issuingTransactionID = null,
            DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Amount = amount;
            TaxID = taxID;
            Name = name;
            Tags = tags;
            Brcode = brcode;
            Due = due;
            Link = link;
            Status = status;
            IssuingTransactionID = issuingTransactionID;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create an IssuingInvoice
        /// <br/>
        /// Send an IssuingInvoice object for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>invoice [IssuingInvoice object]: IssuingInvoice object to be created in the API.</item>
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
        public static IssuingInvoice Create(IssuingInvoice invoice, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: invoice,
                user: user
            ) as IssuingInvoice;
        }

        /// <summary>
        /// Create an IssuingInvoice
        /// <br/>
        /// Send an IssuingInvoice object for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>invoice [Dictionaries]: Dictionary representing the IssuingInvoice objects to be created in the API</item>
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
        public static IssuingInvoice Create(Dictionary<string, object> invoice, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: invoice,
                user: user
            ) as IssuingInvoice;
        }

        /// <summary>
        /// Retrieve a specific IssuingInvoice by its id
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
        ///     <item>IssuingInvoice object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IssuingInvoice Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingInvoice;
        }

        /// <summary>
        /// Retrieve IssuingInvoice objects
        /// <br/>
        /// Receive an IEnumerable of IssuingInvoice objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "expired", "overdue" and "paid"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingInvoice objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingInvoice> Query(int? limit = null, DateTime? after = null, 
            DateTime? before = null, string status = null, List<string> tags = null, 
            User user = null
        ) {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags }
                },
                user: user
            ).Cast<IssuingInvoice>();
        }

        /// <summary>
        /// Retrieve paged IssuingInvoice objects
        /// <br/>
        /// Receive a list of up to 100 IssuingInvoice objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "expired", "overdue" and "paid"</item>
        ///     <item>tags [list of strings, default null]: list of tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingInvoice objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingInvoice objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingInvoice> page, string pageCursor) Page(string cursor = null, int? limit = null, 
            DateTime? after = null, DateTime? before = null, string status = null, List<string> tags = null, 
            User user = null
        ) {
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
                    { "tags", tags }
                },
                user: user
            );
            List<IssuingInvoice> issuingInvoices = new List<IssuingInvoice>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                issuingInvoices.Add(subResource as IssuingInvoice);
            }
            return (issuingInvoices, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingInvoice", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            long amount = json.amount;
            string taxID = json.taxId;
            string name = json.name;
            List<string> tags = json.tags.ToObject<List<string>>();
            string id = json.id;
            string brcode = json.brcode;
            string dueString = json.due;
            DateTime? due = StarkCore.Utils.Checks.CheckNullableDateTime(dueString);
            string link = json.link;
            string status = json.status;
            string issuingTransactionID = json.issuingTransactionId;
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);

            return new IssuingInvoice(
                amount: amount, taxID: taxID, name: name, tags: tags, id: id, brcode: brcode, 
                due: due, link: link, status: status, issuingTransactionID: issuingTransactionID, 
                updated: updated, created: created           
            );
        }
    }
}
