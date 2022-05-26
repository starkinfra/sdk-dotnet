using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingBin object
    /// <br/>
    /// The IssuingBin object displays the informations of BINs registered to your Workspace.
    /// They represent a group of cards that begin with the same numbers (BIN) and offer the same product to end customers.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID[string]: unique BIN number registered within the card network. ex: "53810200"
    ///     <item>Network [string]: card network flag. ex: "mastercard"
    ///     <item>Settlement [string]: settlement type. ex: "credit"
    ///     <item>Category [string]: purchase category. ex: "prepaid"
    ///     <item>Client [string]: client type. ex: "business"
    ///     <item>Updated [DateTime]: latest update datetime for the Bin. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)
    ///     <item>Created [DateTime]: creation datetime for the Bin. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)
    /// </list>
    /// </summary>
    public partial class IssuingBin : Resource
    {
        public string Network { get; }
        public string Settlement { get; }
        public string Category { get; }
        public string Client { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingBin object
        /// <br/>
        /// The IssuingBin object displays the information of BINs registered to your Workspace.
        /// They represent a group of cards that begin with the same numbers (BIN) and offer the same product to end customers.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>id [string]: unique BIN number registered within the card network. ex: "53810200"
        ///     <item>network [string]: card network flag. ex: "mastercard"
        ///     <item>settlement [string]: settlement type. ex: "credit"
        ///     <item>category [string]: purchase category. ex: "prepaid"
        ///     <item>client [string]: client type. ex: "business"
        ///     <item>updated [DateTime]: latest update datetime for the Bin. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)
        ///     <item>created [DateTime]: creation datetime for the Bin. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)
        /// </list>
        /// </summary>
        public IssuingBin(string id, string network, string settlement, string category, string client,
            DateTime? updated, DateTime? created
        ) : base(id)
        {
            Network = network;
            Settlement = settlement;
            Category = category;
            Client = client;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Retrieve IssuingBins
        /// <br/>
        /// Receive a generator of IssuingBin objects previously registered in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, unlimited if null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingBin objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingBin> Query(int? limit = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit }
                },
                user: user
            ).Cast<IssuingBin>();
        }

        /// <summary>
        /// Retrieve paged IssuingBins
        /// <br/>
        /// Receive a list of up to 100 IssuingBin objects previously registered in the Stark Infra API and the cursor to the next page.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingBin objects with updated attributes and cursor to retrieve the next page of IssuingBin objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingBin> page, string pageCursor) Page(string cursor = null, int? limit = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit }
                },
                user: user
            );
            List<IssuingBin> bins = new List<IssuingBin>();
            foreach (SubResource subResource in page)
            {
                bins.Add(subResource as IssuingBin);
            }
            return (bins, pageCursor);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingBin", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string network = json.network;
            string settlement = json.settlement;
            string category = json.category;
            string client = json.client;
            string updatedString = json.updated;
            string createdString = json.created;
            DateTime updated = Checks.CheckDateTime(updatedString);
            DateTime created = Checks.CheckDateTime(createdString);

            return new IssuingBin(
                id: id, network: network, settlement: settlement, category: category,
                client: client, updated: updated, created: created
            );
        }
    } 
}
