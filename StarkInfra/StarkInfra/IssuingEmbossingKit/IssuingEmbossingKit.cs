using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingEmbossingKit object
    /// <br/>
    /// The IssuingEmbossingKit object displays information on the embossing kits available to your Workspace.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when IssuingEmbossingKit is created. ex: "5656565656565656"</item>
    ///     <item>Name [string]: embossing kit name. ex: "stark-plastic-dark-001"</item>
    ///     <item>Designs [list of IssuingDesign objects]: list of IssuingDesign objects. ex: new List<IssuingDesign> {IssuingDesign(), IssuingDesign() }</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingEmbossingKit. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingEmbossingKit. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingEmbossingKit : Resource
    {
        public string Name { get; }
        public List<IssuingDesign> Designs { get; }
        public DateTime? Updated { get;  }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingEmbossingKit object
        /// <br/>
        /// The IssuingEmbossingKit object displays information on the embossing kits available to your Workspace.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingEmbossingKit is created. ex: "5656565656565656"</item>
        ///     <item>name [string]: embossing kit name. ex: "stark-plastic-dark-001" </item>
        ///     <item>designs [list of IssuingDesign objects]: list of IssuingDesign objects. ex: new List<IssuingDesign> {IssuingDesign(), IssuingDesign() }</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingEmbossingKit. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingEmbossingKit. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public IssuingEmbossingKit(
            string id = null, string name = null, List<IssuingDesign> designs = null, 
            DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Name = name;
            Designs = designs;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Retrieve a specific IssuingEmbossingKit object
        /// <br/>
        /// Receive a single IssuingEmbossingKit object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingEmbossingKit object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingEmbossingKit Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingEmbossingKit;
        }

        /// <summary>
        /// Retrieve IssuingEmbossingKit objects
        /// <br/>
        /// Receive an IEnumerable of IssuingEmbossingKit objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default None]: filter for status of retrieved objects. Options: ["created", "canceled", "processing", "failed", "success"]</item>
        ///     <item>designIds [list of string, default None]: list of designIds to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]<item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingEmbossingKit objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingEmbossingKit> Query(int? limit = null, List<String> designIds = null, 
            DateTime? after = null, DateTime? before = null, List<string> status = null, List<string> ids = null, User user = null
        )
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
                    { "designIds", designIds },
                    { "ids", ids }
                },
                user: user
            ).Cast<IssuingEmbossingKit>();
        }

        /// <summary>
        /// Retrieve paged IssuingEmbossingKit objects
        /// <br/>
        /// Receive a list of up to 100 IssuingEmbossingKit objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default None]: filter for status of retrieved objects. Options: ["created", "canceled", "processing", "failed", "success"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string> { "5656565656565656", "4545454545454545" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingEmbossingKit objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingEmbossingKit objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingEmbossingKit> page, string pageCursor) Page(string cursor = null, int? limit = null, List<String> designIds = null,
            DateTime? after = null, DateTime? before = null, List<string> status = null, List<string> ids = null, User user = null
        )
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
                    { "designIds", designIds },
                    { "ids", ids },
                },
                user: user
            );
            List<IssuingEmbossingKit> kits = new List<IssuingEmbossingKit>();
            foreach (SubResource subResource in page)
            {
                kits.Add(subResource as IssuingEmbossingKit);
            }
            return (kits, pageCursor);
        }
        
        /// <summary>
        /// Retrieve a specific IssuingEmbossingKit pdf file
        /// <br/>
        /// Receive a single IssuingEmbossingKit pdf file generated in the Stark Infra API by its id.
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
        ///     <item>IssuingEmbossingKit .pdf file</item>
        /// </list>
        /// </summary>
        public static byte[] Pdf(string id, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                subResourceName: "pdf",
                id: id,
                user: user
            );
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingEmbossingKit", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {

            string id = json.id;
            string name = json.name; 
            List<IssuingDesign> designs = ParseIssuingDesign(json.designs);
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new IssuingEmbossingKit(
                id: id, name: name, designs: designs, updated: updated, 
                created: created
            );
        }

        private static List<IssuingDesign> ParseIssuingDesign(dynamic json)
        {
            if (json == null)
            {
                return null;
            }

            List<IssuingDesign> designs = new List<IssuingDesign>();

            foreach (dynamic design in json)
            {
                designs.Add(IssuingDesign.ResourceMaker(design));
            }
            return designs;
        }
    }
}
