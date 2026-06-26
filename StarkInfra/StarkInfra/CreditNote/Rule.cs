using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditNote.Rule object
    /// <br/>
    /// The CreditNote.Rule object modifies the behavior of CreditNote objects when passed as an argument upon their creation.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Key [string]: Rule to be customized, describes what CreditNote behavior will be altered. ex: "invoiceCreationMode"</item>
    ///     <item>Value [string]: Value of the rule. ex: "scheduled", "instant", "never"</item>
    /// </list>
    /// </summary>
    public partial class Rule : StarkCore.Utils.SubResource
    {
        public string Key { get; }
        public string Value { get; }

        /// <summary>
        /// CreditNote.Rule object
        /// <br/>
        /// The CreditNote.Rule object modifies the behavior of CreditNote objects when passed as an argument upon their creation.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>key [string]: Rule to be customized, describes what CreditNote behavior will be altered. ex: "invoiceCreationMode"</item>
        ///     <item>value [string]: Value of the rule. ex: "scheduled", "instant", "never"</item>
        /// </list>
        /// </summary>
        public Rule(string key, string value)
        {
            Key = key;
            Value = value;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Rule", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            string key = json.key;
            string value = json.value;

            return new Rule(key: key, value: value);
        }
    }
}
