using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditPreview object
    /// <br/>
    /// A CreditPreview is used to get information from a credit before taking it.
    /// This resource can be used to preview CreditNotes
    /// <br/>
    /// When you initialize a CreditPreview, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Credit [CreditNotePreview object]: CreditNotePreview object or dictionary containing credit information.</item>
    ///     <item>Type [string]: Credit type, inferred from the payment parameter if it is not a dictionary. ex: "credit-note"</item>
    /// </list>
    /// </summary>
    public partial class CreditPreview : SubResource
    {
        public string Type { get; }
        public SubResource Credit { get; }

        /// <summary>
        /// CreditPreview object
        /// <br/>
        /// A CreditPreview is used to get information from a credit before taking it.
        /// This resource can be used to preview CreditNotes
        /// <br/>
        /// When you initialize a CreditPreview, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>credit [CreditNotePreview]: CreditNotePreview object or dictionary containing credit information.</item>
        ///     <item>type [string]: Credit type, inferred from the payment parameter if it is not a dictionary. ex: "credit-note"</item>
        /// </list>
        /// </summary>
        public CreditPreview(SubResource credit, string type)
        {
            Credit = credit;
            Type = type;
        }

        /// <summary>
        /// Create CreditPreview objects
        /// <br/>
        /// Send a list of CreditPreview objects for processing in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>previews [list of CreditPreview objects]: list of CreditPreview objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CreditPreview objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<CreditPreview> Create(List<CreditPreview> previews, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: previews,
                user: user
            ).ToList().ConvertAll(o => (CreditPreview)o);
        }

        /// <summary>
        /// Create CreditPreview objects
        /// <br/>
        /// Send a list of CreditPreview dictionaries for processing in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>previews [list of Dictionaries]: list of Dictionaries representing the CreditPreview objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of CreditPreview objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<CreditPreview> Create(List<Dictionary<string, object>> previews, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: previews,
                user: user
            ).ToList().ConvertAll(o => (CreditPreview)o);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CreditPreview", resourceMaker: ResourceMaker);
        }

        internal static SubResource ResourceMaker(dynamic json)
        {
            string type = json.type;
            SubResource payment = ParseCredit(json: json.credit, type: json.type.ToObject<string>());

            return new CreditPreview(
                credit: payment, type: type
            );
        }

        private static SubResource ParseCredit(dynamic json, string type)
        {
            if (type == "credit-note") return CreditNotePreview.ResourceMaker(json);
            return null;
        }
    }
}
