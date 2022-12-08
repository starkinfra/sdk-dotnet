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

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
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
