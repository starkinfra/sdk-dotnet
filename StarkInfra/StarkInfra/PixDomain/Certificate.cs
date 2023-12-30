using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixDomain.Certificate object
    /// <br/>
    /// The Certificate object displays the certificate information from a specific domain.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>content [string]: certificate of a Pix participant in PEM format.</item>
    /// </list>
    /// </summary>
    public partial class Certificate : StarkCore.Utils.SubResource
    {
        public string Content { get; }

        /// <summary>
        /// PixDomain.Certificate object
        /// <br/>
        /// The Certificate object displays the certificate information from a specific domain.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>content [string]: certificate of the Pix participant in PEM format.</item>
        /// </list>
        /// </summary>
        public Certificate(string content)
        {
            Content = content;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Certificate", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            string content = json.content;

            return new Certificate(content: content);
        }
    }
}
