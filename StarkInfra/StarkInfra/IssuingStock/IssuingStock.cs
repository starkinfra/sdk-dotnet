using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingStock object
    /// <br/>
    /// The IssuingStock object represents the current stock of a certain IssuingDesign linked to an Embosser available to your workspace.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when IssuingStock is created. ex: "5656565656565656"</item>
    ///     <item>Balance [integer]: [EXPANDABLE] current stock balance. ex: 1000</item>
    ///     <item>DesignID [string]: IssuingDesign unique id ex: "5136459887542272"</item>
    ///     <item>EmbosserID [string] list of embosser unique ids. ex: "5656565656565656" </item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingStock. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingStock. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingStock : Resource
    {
        public int? Balance { get; }
        public string DesignID { get; }
        public string EmbosserID { get; }
        public DateTime? Updated { get;  }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingStock object
        /// <br/>
        /// The IssuingStock object represents the current stock of a certain IssuingDesign linked to an Embosser available to your workspace.
        /// Balance information will only be returned when the "expand" parameter is used.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingStock is created. ex: "5656565656565656"</item>
        ///     <item>balance [integer]: [EXPANDABLE] current stock balance. ex: 1000</item>
        ///     <item>designID [string]: IssuingDesign unique id ex: "5136459887542272"</item>
        ///     <item>embosserID [string] list of embosser unique ids. ex: "5656565656565656" </item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingStock. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingStock. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingStock(string id = null, int? balance = null, string designID = null, string embosserID = null, 
            DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Balance = balance;
            DesignID = designID;
            EmbosserID = embosserID;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Retrieve IssuingStock objects
        /// <br/>
        /// Receive an IEnumerable of IssuingStock objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>designIds [list of strings, default null]: IssuingDesign unique ids. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>embosserIds [list of strings, default null]: Embosser unique ids. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information ex: new List<string>{ "balance" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingStock objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingStock> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> designIds = null, List<string> embosserIds = null, List<string> ids = null, List<string> expand = null,
            User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", after },
                    { "before", before },
                    { "designIds", designIds },
                    { "embosserIds", embosserIds },
                    { "ids", ids },
                    { "expand", expand },
                },
                user: user
            ).Cast<IssuingStock>();
        }

        /// <summary>
        /// Retrieve paged IssuingStock object
        /// <br/>
        /// Receive a list of up to 100 IssuingStock objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>designIds [list of strings, default null]: IssuingDesign unique ids. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>embosserIds [list of strings, default null]: Embosser unique ids. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>expand [list of strings, default null]: fields to expand information ex: new List<string>{ "balance" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingStock objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingStock objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingStock> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> designIds = null, List<string> embosserIds = null, List<string> ids = null, List<string> expand = null,
            User user = null)
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
                    { "designIds", designIds },
                    { "embosserIds", embosserIds },
                    { "ids", ids },
                    { "expand", expand },
                },
                user: user
            );
            List<IssuingStock> stocks = new List<IssuingStock>();
            foreach (SubResource subResource in page)
            {
                stocks.Add(subResource as IssuingStock);
            }
            return (stocks, pageCursor);
        }

        /// <summary>
        /// Retrieve a specific IssuingStock object
        /// <br/>
        /// Receive a single IssuingStock object previously created in the Stark Infra API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>parameters [dictionary]: dictionary of optional parameters</item>
        ///     <list>
        ///         <item>expand [list of strings, default null]: fields to expand information. ex: new List<string>{ "balance" }</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingStock object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingStock Get(string id, Dictionary<string, object> parameters = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                query: parameters,
                user: user
            ) as IssuingStock;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingStock", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            int? balance = json.balance;
            string designID = json.designId;
            string embosserID = json.embosserId;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new IssuingStock(
                id: id, balance: balance, designID: designID, embosserID: embosserID, 
                updated: updated, created: created
            );
        }
    }
}
