using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixClaim object
    /// <br/>
    /// A Pix Claim is a request to transfer a Pix Key from an account hosted at 
    /// another Pix participant to an account under your bank code. Pix Claims must 
    /// always be requested by the claimer. 
    /// <br/>
    /// When you initialize a PixClaim, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>AccountCreated [DateTime]: opening Date or DateTime for the account claiming the PixKey. ex: new Datetime(2022, 01, 01).</item>
    ///     <item>AccountNumber [string]: number of the account claiming the PixKey. ex: "76543".</item>
    ///     <item>AccountType [string]: type of the account claiming the PixKey. Options: "checking", "savings", "salary" or "payment".</item>
    ///     <item>BranchCode [string]: branch code of the account claiming the PixKey. ex: "1234".</item>
    ///     <item>Name [string]: holder's name of the account claiming the PixKey. ex: "Jamie Lannister".</item>
    ///     <item>TaxID [string]: holder's taxID of the account claiming the PixKey (CPF/CNPJ). ex: "012.345.678-90".</item>
    ///     <item>KeyID [string]: id of the registered PixKey to be claimed. Allowed keyTypes are CPF, CNPJ, phone number or email. ex: "+5511989898989".</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
    ///     <item>ID [string]: unique id returned when the PixClaim is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current PixClaim status. Options: "created", "failed", "delivered", "confirmed", "success", "canceled"</item>
    ///     <item>Type [string]: type of PixClaim. Options: "ownership", "portability".</item>
    ///     <item>KeyType [string]: keyType of the claimed PixKey. Options: "CPF", "CNPJ", "phone" or "email"</item>
    ///     <item>Flow [string]: direction of the PixClaim. Options: "in" if you received the PixClaim or "out" if you created the PixClaim.</item>
    ///     <item>ClaimerBankCode [string]: bankCode of the Pix participant that created the PixClaim. ex: "20018183".</item>
    ///     <item>ClaimedBankCode [string]: bankCode of the account donating the PixKey. ex: "20018183"</item>
    ///     <item>Created [DateTime]: creation DateTime for the PixClaim. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: update DateTime for the PixClaim. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixClaim : Resource
    {
        public DateTime AccountCreated { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public string BranchCode { get; }
        public string Name { get; }
        public string TaxID { get; }
        public string KeyID { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public string Type { get; }
        public string KeyType { get; }
        public string Flow { get; }
        public string ClaimerBankCode { get; }
        public string ClaimedBankCode { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// PixClaim object
        /// <br/>
        /// A Pix Claim is a request to transfer a Pix Key from an account hosted at 
        /// another Pix participant to an account under your bank code. Pix Claims must 
        /// always be requested by the claimer. 
        /// <br/>
        /// When you initialize a PixClaim, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>accountCreated [DateTime]: opening Date or DateTime for the account claiming the PixKey. ex: new Datetime(2022, 01, 01).</item>
        ///     <item>accountNumber [string]: number of the account claiming the PixKey. ex: "76543".</item>
        ///     <item>accountType [string]: type of the account claiming the PixKey. Options: "checking", "savings", "salary" or "payment".</item>
        ///     <item>branchCode [string]: branch code of the account claiming the PixKey. ex: 1234".</item>
        ///     <item>name [string]: holder's name of the account claiming the PixKey. ex: "Jamie Lannister".</item>
        ///     <item>taxID [string]: holder's taxID of the account claiming the PixKey (CPF/CNPJ). ex: "012.345.678-90".</item>
        ///     <item>keyID [string]: id of the registered PixKey to be claimed. Allowed keyTypes are CPF, CNPJ, phone number or email. ex: "+5511989898989".</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixClaim is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current PixClaim status. Options: "created", "failed", "delivered", "confirmed", "success", "canceled"</item>
        ///     <item>type [string]: type of PixClaim. Options: "ownership", "portability".</item>
        ///     <item>keyType [string]: keyType of the claimed PixKey. Options: "CPF", "CNPJ", "phone" or "email"</item>
        ///     <item>flow [string]: direction of the PixClaim. Options: "in" if you received the PixClaim or "out" if you created the PixClaim.</item>
        ///     <item>claimerBankCode [string]: bankCode of the Pix participant that created the PixClaim. ex: "20018183".</item>
        ///     <item>claimedBankCode [string]: bankCode of the account donating the PixKey. ex: "20018183"</item>
        ///     <item>created [DateTime]: creation DateTime for the PixClaim. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: update DateTime for the PixClaim. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixClaim(
            DateTime accountCreated, string accountNumber, string accountType, 
            string branchCode, string name, string taxID, string keyID, List<string> tags = null,
            string status = null, string type = null, string keyType = null, 
            string flow = null, string claimerBankCode = null, string claimedBankCode = null,
            DateTime? updated = null, DateTime? created = null, string id = null
        ) : base(id)
        {
            AccountCreated = accountCreated;
            AccountNumber = accountNumber;
            AccountType = accountType;
            BranchCode = branchCode;
            Name = name;
            TaxID = taxID;
            KeyID = keyID;
            Tags = tags;
            Status = status;
            Type = type;
            KeyType = keyType;
            Flow = flow;
            ClaimerBankCode = claimerBankCode;
            ClaimedBankCode = claimedBankCode;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create a PixClaim object
        /// <br/>
        /// Create a Pix Claim to request the transfer of a Pix Key from an account 
        /// hosted at another Pix participant to an account under your bank code.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>claim [PixClaim object]: PixClaim object to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixClaim object with updated attributes.</item>
        /// </list>
        /// </summary>
        public static PixClaim Create(PixClaim pixClaim, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: pixClaim,
                user: user
            ) as PixClaim;
        }
        
        /// <summary>
        /// Create a PixClaim
        /// <br/>
        /// Create a Pix Claim to request the transfer of a Pix Key from an account 
        /// hosted at another Pix participant to an account under your bank code.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>claim [dictionary]: dictionary representing PixClaim to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixClaim object with updated attributes.</item>
        /// </list>
        /// </summary>
        public static PixClaim Create(Dictionary<string, object> claim, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: claim,
                user: user
            ) as PixClaim;
        }

        /// <summary>
        /// Retrieve a PixClaim object
        /// <br/>
        /// Retrieve a PixClaim object linked to your Workspace in the Stark Infra API by its id.
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
        ///     <item>PixClaim object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static PixClaim Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixClaim;
        }

        /// <summary>
        /// Retrieve PixClaim objects
        /// <br/>
        /// Receive a IEnumerable of PixClaims objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created after a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created before a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. Options: new List<string>{ "created", "failed", "delivered", "confirmed", "success", "canceled" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>type [strings, default null]: filter for the type of retrieved PixClaims. Options: "ownership" or "portability".</item>
        ///     <item>keyType [string, default null]: filter for the PixKey type of retrieved PixClaims. Options: "cpf", "cnpj", "phone", "email" and "evp",</item>
        ///     <item>keyID [string, default null]: filter PixClaims linked to a specific PixKey id. ex: "+5511989898989".</item>
        ///     <item>flow [string, default null]: direction of the Pix Claim. Options: "in" if you received the PixClaim or "out" if you created the PixClaim.</item>
        ///     <item>tags [list of strings, default null]: list of strings to filter retrieved objects. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if starkinfra.user was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixClaim objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixClaim> Query(
            int? limit = null, DateTime? after = null, DateTime? before = null, 
            string status = null, List<string> ids = null, string type = null, 
            string keyType = null, string keyID = null, string flow = null, 
            List<string> tags = null, User user = null
        )
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
                    { "type", type },
                    { "keyType", keyType },
                    { "keyID", keyID },
                    { "flow", flow },
                    { "tags", tags }
                },
                user: user
            ).Cast<PixClaim>();
        }

        /// <summary>
        /// Retrieve paged PixClaim objects
        /// <br/>
        /// Receive a list of up to 100 PixClaims objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null]: date filter for objects created after a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created before a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. Options: "created", "failed", "delivered", "confirmed", "success", "canceled".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>type [strings, default null]: filter for the type of retrieved PixClaims. Options: "ownership" or "portability".</item>
        ///     <item>keyType [string, default null]: filter for the PixKey type of retrieved PixClaims. Options: "cpf", "cnpj", "phone", "email" and "evp",</item>
        ///     <item>keyID [string, default null]: filter PixClaims linked to a specific PixKey id. ex: "+5511989898989".</item>
        ///     <item>flow [string, default null]:direction of the Pix Claim. Options: "in" if you received the PixClaim or "out" if you created the PixClaim.</item>
        ///     <item>tags [list of strings, default null]: list of strings to filter retrieved objects. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixClaim objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixClaim objects</item>
        /// </list>
        /// </summary>
        public static (List<PixClaim> page, string pageCursor) Page(
            string cursor = null, int? limit = null, DateTime? after = null, 
            DateTime? before = null, string status = null, List<string> ids = null, 
            string type = null, string keyType = null, string keyID = null,
            string flow = null, List<string> tags = null, User user = null
        )
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
                    { "type", type },
                    { "keyType", keyType },
                    { "keyID", keyID },
                    { "flow", flow },
                    { "tags", tags }
                },
                user: user
            );
            List<PixClaim> claims = new List<PixClaim>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                claims.Add(subResource as PixClaim);
            }
            return (claims, pageCursor);
        }

        /// <summary>
        /// Update PixClaim entity
        /// <br/>
        /// Update a PixClaim by passing id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: PixClaim id. ex: “5656565656565656”</item>
        ///     <item>status [string]: patched status for Pix Claim. Options: "confirmed" and "canceled"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>patchData [dictionary]: Dictionary of parameters</item>
        ///     <list>
        ///         <item>reason [string, default: "userRequested"]: reason why the PixClaim is being patched. Options: "fraud", "userRequested", "accountClosure".</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixClaim with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixClaim Update(string id, string status, Dictionary<string, object> patchData = null, User user = null)
        {
            if (patchData == null)
            {
                patchData = new Dictionary<string, object> { };
            }
            patchData.Add("status", status);
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as PixClaim;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixClaim", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            DateTime accountCreated = json.accountCreated;
            string accountNumber = json.accountNumber;
            string accountType = json.accountType;
            string branchCode = json.branchCode;
            string name = json.name;
            string taxID = json.taxId;
            string keyID = json.keyId;
            List<string> tags = json.tags?.ToObject<List<string>>();;
            string id = json.id;
            string status = json.status;
            string type = json.type;
            string keyType = json.keyType;
            string flow = json.flow;
            string claimerBankCode = json.claimerBankCode;
            string claimedBankCode = json.claimedBankCode;
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new PixClaim(
                accountCreated: accountCreated, accountNumber: accountNumber, 
                accountType: accountType, branchCode: branchCode, name: name, 
                taxID: taxID, keyID: keyID, tags: tags, id: id, status: status, 
                type: type, keyType: keyType, flow: flow, claimerBankCode: claimerBankCode,
                claimedBankCode: claimedBankCode, updated: updated, created: created
            );
        }
    }
}
