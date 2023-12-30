using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingProduct object
    /// <br/>
    /// The IssuingProduct object displays information of available card products  registered to your Workspace.
    /// They represent a group of cards that begin with the same numbers (id) and offer the same product to end customers.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID[string]: unique card product number (BIN) registered within the card network. ex: "53810200"</item>
    ///     <item>Network [string]: card network flag. ex: "mastercard"</item>
    ///     <item>FundingType [string]: type of funding used for payment. ex: "credit", "debit"</item>
    ///     <item>HolderType [string]: holder type. ex: "business", "individual"</item>
    ///     <item>Code [string]: internal code from card flag informing the product. ex: "MRW", "MCO", "MWB", "MCS"</item>
    ///     <item>Created [DateTime]: creation DateTime for the IssuingProduct. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingProduct : Resource
    {
        public string Network { get; }
        public string Settlement { get; }
        public string Category { get; }
        public string Client { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingProduct object
        /// <br/>
        /// The IssuingProduct object displays the information of BINs registered to your Workspace.
        /// They represent a group of cards that begin with the same numbers (BIN) and offer the same product to end customers.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>id[string]: unique card product number (BIN) registered within the card network. ex: "53810200"</item>
        ///     <item>network [string]: card network flag. ex: "mastercard"</item>
        ///     <item>fundingType [string]: type of funding used for payment. ex: "credit", "debit"</item>
        ///     <item>holderType [string]: holder type. ex: "business", "individual"</item>
        ///     <item>code [string]: internal code from card flag informing the product. ex: "MRW", "MCO", "MWB", "MCS"</item>
        ///     <item>created [DateTime]: creation DateTime for the IssuingProduct. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingProduct(string id, string network, string settlement, string category, string client,
            DateTime? created
        ) : base(id)
        {
            Network = network;
            Settlement = settlement;
            Category = category;
            Client = client;
            Created = created;
        }

        /// <summary>
        /// Retrieve IssuingProduct objects
        /// <br/>
        /// Receive an IEnumerable of IssuingProduct objects previously registered in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingProduct objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingProduct> Query(int? limit = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit }
                },
                user: user
            ).Cast<IssuingProduct>();
        }

        /// <summary>
        /// Retrieve paged IssuingProduct objects
        /// <br/>
        /// Receive a list of up to 100 IssuingProduct objects previously registered in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingProduct objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingProduct objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingProduct> page, string pageCursor) Page(string cursor = null, int? limit = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit }
                },
                user: user
            );
            List<IssuingProduct> products = new List<IssuingProduct>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                products.Add(subResource as IssuingProduct);
            }
            return (products, pageCursor);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingProduct", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string network = json.network;
            string fundingType = json.fundingType;
            string holderType = json.holderType;
            string code = json.code;
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckDateTime(createdString);

            return new IssuingProduct(
                id: id, network: network, settlement: fundingType, category: holderType, 
                client: code, created: created
            );
        }
    } 
}
