using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditSigner object
    /// <br/>
    /// CreditNote signer's information.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: signer's name. ex: "Tony Stark"</item>
    ///     <item>Contact [string]: signer's contact information. ex: "tony@starkindustries.com"</item>
    ///     <item>Method [string]: delivery method for the contract. ex: "link"</item>
    ///     <item>ID [string]: unique id returned when the CreditSigner is created. ex: "5656565656565656"</item>
    /// </list>
    /// </summary>
    public class CreditSigner : Resource
    {
        public string Name { get; }
        public string Contact { get; }
        public string Method { get; }

        /// <summary>
        /// CreditSigner object
        /// <br/>
        /// CreditNote signer's information.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: signer's name. ex: "Tony Stark"</item>
        ///     <item>contact [string]: signer's contact information. ex: "tony@starkindustries.com"</item>
        ///     <item>method [string]: delivery method for the contract. ex: "link"</item>
        /// </list>
        /// Attributes(return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the CreditSigner is created. ex: "5656565656565656"</item>
        /// </list>
        /// </summary>
        public CreditSigner( 
            string name, string contact, string method, string id = null
        ) : base(id)
        {
            Name = name;
            Contact = contact;
            Method = method;
        }

        /// <summary>
        /// Resend token to signer
        /// <br/>
        /// Resend token to a specific signer.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: CreditSigner unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>CreditSigner object with updated attributes</item>
        /// </list>
        /// </summary>
        public static CreditSigner ResendToken(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> { { "isSent", false } },
                user: user
            ) as CreditSigner;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CreditSigner", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string contact = json.contact;
            string method = json.method;
            string id = json.id;

            return new CreditSigner(
                name: name, contact: contact, method: method, id: id
            );
        }
    }
}
