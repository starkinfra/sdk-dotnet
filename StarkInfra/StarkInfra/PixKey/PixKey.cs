using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixKey object
    /// <br/>
    /// PixKeys link bank account information to key ids.
    /// Key ids are a convenient way to search and pass bank account information.
    /// When you initialize a Pix Key, the entity will not be automatically
    /// created in the Stark Infra API.The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>AccountCreated [DateTime or string]: opening Date or DateTime for the linked account. ex: "2022-01-01T12:00:00:00".</item>
    ///     <item>AccountNumber [string]: number of the linked account. ex: "76543".</item>
    ///     <item>AccountType [string]: type of the linked account. Options: "checking", "savings", "salary" or "payment".</item>
    ///     <item>BranchCode [string]: branch code of the linked account. ex: 1234".</item>
    ///     <item>Name [string]: holder's name of the linked account. ex: "Jamie Lannister".</item>
    ///     <item>TaxID [string]: holder's taxId (CPF/CNPJ) of the linked account. ex: "012.345.678-90".</item>
    ///     <item>ID [string, default null]: id of the registered PixKey. Allowed types are: CPF, CNPJ, phone number or email. If this parameter is not passed, an EVP will be created. ex: "+5511989898989";</item>
    ///     <item>Tags [list of strings, default null]: list of strings for reference when searching for PixKeys. ex: new List<string>{ "employees", "monthly" }</item>
    ///     <item>Owned [DateTime]: datetime when the key was owned by the holder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>OwnerType [string]: type of the owner of the PixKey. Options: "business" or "individual".</item>
    ///     <item>Status [string]: current PixKey status. Options: "created", "registered", "canceled", "failed"</item>
    ///     <item>BankCode [string]: bankCode of the account linked to the Pix Key. ex: "20018183".</item>
    ///     <item>BankName [string]: name of the bank that holds the account linked to the PixKey. ex: "StarkBank"</item>
    ///     <item>Type [string]: type of the PixKey. Options: "cpf", "cnpj", "phone", "email" and "evp",</item>
    ///     <item>Created [DateTime]: creation datetime for the PixKey. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixKey : Resource
    {
        public DateTime? AccountCreated { get; }
        public string AccountNumber { get; }
        public string AccountType { get; }
        public string BranchCode { get; }
        public string Name { get; }
        public string TaxID { get; }
        public List<string> Tags { get; }
        public DateTime? Owned { get; }
        public string OwnerType { get; }
        public string Status { get; }
        public string BankCode { get; }
        public string BankName { get; }
        public string Type { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// PixKey object
        /// <br/>
        /// PixKeys link bank account information to key ids.
        /// Key ids are a convenient way to search and pass bank account information.
        /// When you initialize a Pix Key, the entity will not be automatically
        /// created in the Stark Infra API.The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>accountCreated [DateTime or string]: opening Date or DateTime for the linked account. ex: "2022-01-01T12:00:00:00".</item>
        ///     <item>accountNumber [string]: number of the linked account. ex: "76543".</item>
        ///     <item>accountType [string]: type of the linked account. Options: "checking", "savings", "salary" or "payment".</item>
        ///     <item>branchCode [string]: branch code of the linked account. ex: 1234".</item>
        ///     <item>name [string]: holder's name of the linked account. ex: "Jamie Lannister".</item>
        ///     <item>taxId [string]: holder's taxId (CPF/CNPJ) of the linked account. ex: "012.345.678-90".</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>id [string, default null]: id of the registered PixKey. Allowed types are: CPF, CNPJ, phone number or email. If this parameter is not passed, an EVP will be created. ex: "+5511989898989";</item>
        ///     <item>tags [list of strings, default null]: list of strings for reference when searching for PixKeys. ex: new List<string>{ "employees", "monthly" }</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>owned [DateTime]: datetime when the key was owned by the holder. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>ownerType [string]: type of the owner of the PixKey. Options: "business" or "individual".</item>
        ///     <item>status [string]: current PixKey status. Options: "created", "registered", "canceled", "failed"</item>
        ///     <item>bankCode [string]: bankCode of the account linked to the Pix Key. ex: "20018183".</item>
        ///     <item>bankName [string]: name of the bank that holds the account linked to the PixKey. ex: "StarkBank"</item>
        ///     <item>type [string]: type of the PixKey. Options: "cpf", "cnpj", "phone", "email" and "evp",</item>
        ///     <item>created [DateTime]: creation datetime for the PixKey. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixKey(DateTime? accountCreated, string accountNumber, string accountType, string branchCode, string name,
            string taxId, string id = null, List<string> tags = null, DateTime? owned = null, string ownerType = null, 
            string status = null, string bankCode = null, string bankName = null, string type = null, DateTime? created = null
        ) : base(id)
        {
            AccountCreated = accountCreated;
            AccountNumber = accountNumber;
            AccountType = accountType;
            BranchCode = branchCode;
            Name = name;
            TaxID = taxId;
            Tags = tags;
            Owned = owned;
            OwnerType = ownerType;
            Status = status;
            BankCode = bankCode;
            BankName = bankName;
            Type = type;
            Created = created;
        }

        /// <summary>
        /// Create a PixKey
        /// <br/>
        /// Send a PixKey object for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>key [PixKey Object]: PixKey object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixKey object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixKey Create(PixKey key, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: key,
                user: user
            ) as PixKey;
        }

		/// <summary>
        /// Create a PixKey
        /// <br/>
        /// Send a PixKey dictionary for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>key [dictionary]: PixKey dictionary to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixKey object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixKey Create(Dictionary<string, object> key, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: key,
                user: user
            ) as PixKey;
        }

        /// <summary>
        /// Retrieve a specific PixKey
        /// <br/>
        /// Retrieve the PixKey object linked to your Workspace in the Stark Infra API by its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        ///     <item>payerId [string]: tax id (CPF/CNPJ) of the individual or business requesting the PixKey information. This id is used by the Central Bank to limit request rates. ex: "20.018.183/0001-80".</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: Dictionary of parameters</item>
        ///     <list>
        ///         <item>endToEndId [string, default null]: central bank's unique transaction id. If the request results in the creation of a PixRequest, the same endToEndId should be used. If this parameter is not passed, one endToEndId will be automatically created. Example: "E00002649202201172211u34srod19le"</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixKey object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static PixKey Get(string id, string payerId, Dictionary<string, object> parameters = null, User user = null)
        {
            parameters.Add("payerId", payerId);
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                query: parameters,
                user: user
            ) as PixKey;
        }

        /// <summary>
        /// Retrieve PixKeys
        /// <br/>
        /// Receive a IEnumerable of PixKeys objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created after a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created before a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. Options: "created", "registered", "canceled", "failed".</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>type [list of strings, default null]: filter for the type of retrieved PixKeys. Options: "cpf", "cnpj", "phone", "email", "evp".</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixKey objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixKey> Query(int? limit = null, DateTime? after = null, DateTime? before = null, string status = null, 
            List<string> tags = null, List<string> ids = null, string type = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "status", status },
                    { "tags", tags },
                    { "ids", ids },
                    { "type", type },
                    { "user", user }
                },
                user: user
            ).Cast<PixKey>();
        }

        /// <summary>
        /// Retrieve paged PixKeys
        /// <br/>
        /// Receive a list of up to 100 PixKey objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your keys.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created after a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created before a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. Options: "created", "registered", "canceled", "failed".</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>type [list of strings, default null]: filter for the type of retrieved PixKeys. Options: "cpf", "cnpj", "phone", "email", "evp".</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>cursor to retrieve the next page of PixKey objects</item>
        ///     <item>list of PixKey objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static (List<PixKey> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null, 
            string status = null, List<string> tags = null, List<string> ids = null, string type = null, User user = null)
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
                    { "type", type },
                    { "user", user }
                },
                user: user
            );
            List<PixKey> keys = new List<PixKey>();
            foreach (SubResource subResource in page)
            {
                keys.Add(subResource as PixKey);
            }
            return (keys, pageCursor);
        }

        /// <summary>
        /// Update PixKey entity
        /// <br/>
        /// Update a PixKey parameters by passing id.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: object unique id. ex: "5656565656565656"</item>
        ///     <item>reason [string]: reason why the PixKey is being patched. Options: "branchTransfer", "reconciliation" or "userRequested".</item>
        ///     <item>patchData [dictionary]: Dictionary of optional parameters</item>
        ///     <list>
        ///         <item>accountCreated [DateTime or string, default null]: opening Date or DateTime for the account to be linked. ex: "2022-01-01.</item>
        ///         <item>accountNumber[string, default null]: number of the account to be linked.ex: "76543".</item>
        ///         <item>accountType[string, default null]: type of the account to be linked.Options: "checking", "savings", "salary" or "payment".</item>
        ///         <item>branchCode[string, default null]: branch code of the account to be linked.ex: 1234".</item>
        ///         <item>name[string, default null]: holder's name of the account to be linked. ex: "Jamie Lannister".</item>
        ///     </list>
        ///     <item>user[Organization / Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixKey with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixKey Update(string id, string reason, Dictionary<string, object> patchData, User user = null)
        {
            patchData.Add("reason", reason);
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as PixKey;
        }

        /// <summary>
        /// Cancel a PixKey entity
        /// <br/>
        /// Cancel a PixKey entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: PixKey unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled PixKey object</item>
        /// </list>
        /// </summary>
        public static PixKey Cancel(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixKey;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixKey", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string accountCreatedString = json.accountCreated;
            DateTime? accountCreated = Checks.CheckDateTime(accountCreatedString);
            string accountNumber = json.accountNumber;
            string accountType = json.accountType;
            string branchCode = json.branchCode;
            string name = json.name;
            string taxId = json.taxId;
            List<string> tags = json.tags.ToObject<List<string>>();
            string ownedString = json.owned;
            DateTime? owned = null;
            if (ownedString != null) owned = Checks.CheckDateTime(ownedString);
            string ownerType = json.ownerType;
            string status = json.status;
            string bankCode = json.bankCode;
            string bankName = json.bankName;
            string type = json.type;
            string createdString = json.created;
            DateTime? created = null;
            if (ownedString != null) created = Checks.CheckDateTime(createdString);

            return new PixKey(
                id: id, accountCreated: accountCreated, accountNumber: accountNumber, accountType: accountType,
                branchCode: branchCode, name: name, taxId: taxId, tags: tags, owned: owned, ownerType: ownerType,
                status: status, bankCode: bankCode, bankName: bankName, type: type, created: created
            );
        }
    }
}
