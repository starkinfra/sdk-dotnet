using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditNote.Signer object
    /// <br/>
    /// CreditNote signer's information.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: signer's name. ex: "Tony Stark"</item>
    ///     <item>Contact [string]: contact for the contract signature request. ex: "tony@starkindustries.com"</item>
    ///     <item>Method [string]: delivery method for the contract. ex: "link"</item>
    /// </list>
    /// </summary>
    public partial class Signer : SubResource
    {
        public string Name { get; }
        public string Contact { get; }
        public string Method { get; }

        /// <summary>
        /// Signer object
        /// <br/>
        /// CreditNote signer's information.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>name [string]: signer name. ex: "Tony Stark"</item>
        ///     <item>contact [string]: contact for the contract signature request. ex: "tony@starkindustries.com"</item>
        ///     <item>method [string]: delivery method for the contract. ex: "link"</item>
        /// </list>
        /// </summary>
        public Signer(string name, string contact, string method)
        {
            Name = name;
            Contact = contact;
            Method = method;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Signer", resourceMaker: ResourceMaker);
        }

        internal static SubResource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string contact = json.contact;
            string method = json.method;

            return new Signer(name: name, contact: contact, method: method);
        }
    }
}
