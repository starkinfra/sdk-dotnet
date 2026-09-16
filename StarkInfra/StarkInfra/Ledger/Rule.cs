using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    public partial class Ledger
    {
        /// <summary>
        /// Ledger.Rule object
        /// <br/>
        /// The Ledger.Rule object modifies the behavior of Ledger objects when passed as an argument upon their creation or update.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>Key [string]: Rule to be customized, describes what Ledger behavior will be altered. ex: "minimumBalance", "maximumBalance"</item>
        ///     <item>Value [long integer]: Value of the rule. ex: 1000</item>
        /// </list>
        /// </summary>
        public class Rule : StarkCore.Utils.SubResource
        {
            public string Key { get; }
            public long Value { get; }

            /// <summary>
            /// Ledger.Rule object
            /// <br/>
            /// The Ledger.Rule object modifies the behavior of Ledger objects when passed as an argument upon their creation or update.
            /// <br/>
            /// Parameters (required):
            /// <list>
            ///     <item>key [string]: Rule to be customized, describes what Ledger behavior will be altered. ex: "minimumBalance", "maximumBalance"</item>
            ///     <item>value [long integer]: Value of the rule. ex: 1000</item>
            /// </list>
            /// </summary>
            public Rule(string key, long value)
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
                long value = json.value;

                return new Rule(key: key, value: value);
            }
        }
    }
}
