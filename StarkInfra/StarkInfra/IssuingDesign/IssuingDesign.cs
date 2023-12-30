using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingDesign object
    /// <br/>
    /// The IssuingDesign object displays information on the card and card package designs available to your Workspace.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when IssuingDesign is created. ex: "5656565656565656"</item>
    ///     <item>Name [string]: card or package design name. ex: "stark-plastic-dark-001"</item>
    ///     <item>EmbosserIds [list of strings] list of embosser unique ids. ex: new List<string> { "5136459887542272", "5136459887542273" }</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingDesign. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingDesign. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingDesign : Resource
    {
        public string Name { get; }
        public List<string> EmbosserIds { get; }
        public DateTime? Updated { get;  }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingDesign object
        /// <br/>
        /// The IssuingDesign object displays information on the card and card package designs available to your Workspace.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingDesign is created. ex: "5656565656565656"</item>
        ///     <item>name [string]: card or package design name. ex: "stark-plastic-dark-001" </item>
        ///     <item>embosserIds [list of strings]: list of embosser unique ids. ex: new List<string> { "5136459887542272", "5136459887542273" }</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingDesign. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingDesign. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingDesign(
            string id = null, string name = null, List<string> embosserIds = null, 
            DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Name = name;
            EmbosserIds = embosserIds;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Retrieve a specific IssuingDesign object
        /// <br/>
        /// Receive a single IssuingDesign object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingDesign object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingDesign Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingDesign;
        }

        /// <summary>
        /// Retrieve IssuingDesign objects
        /// <br/>
        /// Receive an IEnumerable of IssuingDesign objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingDesign objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingDesign> Query(List<string> ids = null, int? limit = null,
            User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "ids", ids },
                    { "limit", limit },
                },
                user: user
            ).Cast<IssuingDesign>();
        }

        /// <summary>
        /// Retrieve paged IssuingDesign objects
        /// <br/>
        /// Receive a list of up to 100 IssuingDesign objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string> { "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingDesign objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingDesign objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingDesign> page, string pageCursor) Page(string cursor = null, List<string> ids = null, int? limit = null, 
            User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "ids", ids },
                    { "limit", limit },
                },
                user: user
            );
            List<IssuingDesign> designs = new List<IssuingDesign>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                designs.Add(subResource as IssuingDesign);
            }
            return (designs, pageCursor);
        }
        
        /// <summary>
        /// Retrieve a specific IssuingDesign pdf file
        /// <br/>
        /// Receive a single IssuingDesign pdf file generated in the Stark Infra API by its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingDesign .pdf file</item>
        /// </list>
        /// </summary>
        public static byte[] Pdf(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                subResourceName: "pdf",
                id: id,
                user: user
            );
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingDesign", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string name = json.name;
            List<string> embosserIds = json.embosserIds.ToObject<List<string>>();;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new IssuingDesign(
                id: id, name: name, embosserIds: embosserIds, updated: updated, 
                created: created
            );
        }
    }
}
