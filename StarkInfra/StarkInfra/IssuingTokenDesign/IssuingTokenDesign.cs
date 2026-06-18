using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingTokenDesign object
    /// <br/>
    /// The IssuingTokenDesign object displays the information of the token designs created in your Workspace.
    /// This resource represents the existent designs for the cards which will be tokenized.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when IssuingTokenDesign is created. ex: "5656565656565656"</item>
    ///     <item>Name [string]: design name. ex: "Stark Bank - White Metal"</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingTokenDesign. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingTokenDesign. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingTokenDesign : Resource
    {
        public string Name { get; }
        public DateTime? Created { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// IssuingTokenDesign object
        /// <br/>
        /// The IssuingTokenDesign object displays the information of the token designs created in your Workspace.
        /// This resource represents the existent designs for the cards which will be tokenized.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingTokenDesign is created. ex: "5656565656565656"</item>
        ///     <item>name [string]: design name. ex: "Stark Bank - White Metal"</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingTokenDesign. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingTokenDesign. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingTokenDesign(
            string id = null, string name = null, DateTime? created = null, DateTime? updated = null
        ) : base(id)
        {
            Name = name;
            Created = created;
            Updated = updated;
        }

        /// <summary>
        /// Retrieve a specific IssuingTokenDesign object
        /// <br/>
        /// Receive a single IssuingTokenDesign object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingTokenDesign object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingTokenDesign Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingTokenDesign;
        }

        /// <summary>
        /// Retrieve IssuingTokenDesign objects
        /// <br/>
        /// Receive an IEnumerable of IssuingTokenDesign objects previously created in the Stark Infra API
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
        ///     <item>IEnumerable of IssuingTokenDesign objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingTokenDesign> Query(int? limit = null, List<string> ids = null,
            User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "ids", ids },
                },
                user: user
            ).Cast<IssuingTokenDesign>();
        }

        /// <summary>
        /// Retrieve paged IssuingTokenDesign objects
        /// <br/>
        /// Receive a list of up to 100 IssuingTokenDesign objects previously created in the Stark Infra API and the cursor to the next page.
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
        ///     <item>list of IssuingTokenDesign objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingTokenDesign objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingTokenDesign> page, string pageCursor) Page(string cursor = null, int? limit = null,
            List<string> ids = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "ids", ids },
                },
                user: user
            );
            List<IssuingTokenDesign> designs = new List<IssuingTokenDesign>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                designs.Add(subResource as IssuingTokenDesign);
            }
            return (designs, pageCursor);
        }

        /// <summary>
        /// Retrieve a specific IssuingTokenDesign pdf file
        /// <br/>
        /// Receive a single IssuingTokenDesign pdf file generated in the Stark Infra API by its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingTokenDesign .pdf file</item>
        /// </list>
        /// </summary>
        public static byte[] Pdf(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                subResourceName: "pdf",
                id: id,
                user: user
            );
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingTokenDesign", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string name = json.name;
            string createdString = json.created;
            DateTime created = StarkCore.Utils.Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new IssuingTokenDesign(
                id: id, name: name, created: created, updated: updated
            );
        }
    }
}
