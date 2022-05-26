using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditNot.Invoice.Description object
    /// <br/>
    /// Description object for the invoice 
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Key [string]: Description for the value. ex: "Taxes"</item>
    ///     <item>Value [string]: amount related to the described key. ex: "R$100,00"</item>
    /// </list>
    /// </summary>
    public partial class Description : SubResource
    {
        public string Key { get; }
        public string Value { get; }

        /// <summary>
        /// CreditNot.Invoice.Description object
        /// <br/>
        /// Description object for the invoice 
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>key [string]: Description for the value. ex: "Taxes"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>value [string]: amount related to the described key. ex: "R$100,00"</item>
        /// </list>
        /// </summary>
        public Description(string key, string value)
        {
            Key = key;
            Value = value;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Description", resourceMaker: ResourceMaker);
        }

        internal static SubResource ResourceMaker(dynamic json)
        {
            string key = json.key;
            string value = json.value;

            return new Description(key: key, value: value);
        }
    }
}
