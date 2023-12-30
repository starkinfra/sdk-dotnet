using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;
using Newtonsoft.Json.Linq;


namespace StarkInfra
{
    /// <summary>
    /// PixDomain object
    /// <br/>
    /// The PixDomain object displays the domain name and the QR Code domain 
    /// certificate of Pix participants.
    /// <br/>
    /// Properties:
    /// <list>
    ///    <item>Certificates [list of PixDomain.Certificate objects]: certificate information of the Pix participant.</item>
    ///    <item>Name [string]: current active domain (URL) of the Pix participant.</item>
    /// </list>
    /// </summary>
    public partial class PixDomain : StarkCore.Utils.SubResource
    {
        public List<Certificate> Certificates { get; }
        public string Name { get; }

        /// <summary>
        /// PixDomain object
        /// <br/>
        /// The PixDomain object displays the domain name and the QR Code domain 
        /// certificate of Pix participants.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///    <item>Certificates [list of PixDomain.Certificate objects]: certificate information of the Pix participant.</item>
        ///    <item>name [string]: current active domain (URL) of the Pix participant.</item>
        /// </list>
        /// </summary>
        public PixDomain(List<Certificate> certificates = null, string name = null)
        {
            Certificates = certificates;
            Name = name;
        }

        /// <summary>
        /// Retrieve PixDomain objects
        /// <br/>
        /// Receive an IEnumerable of PixDomain objects of Pix participants able to issue BR Codes
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixDomain objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixDomain> Query(User user = null)
        {

            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = SubResource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {},
                user: user
            ).Cast<PixDomain>();
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) SubResource()
        {
            return (resourceName: "PixDomain", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            List<Certificate> certificates = ParseCertificates(json.certificates);
            string name = json.name;

            return new PixDomain(certificates: certificates, name: name);
        }

        private static List<Certificate> ParseCertificates(dynamic json)
        {
            List<Certificate> certificates = new List<Certificate>();

            foreach (dynamic certificate in json)
            {
                certificates.Add(Certificate.ResourceMaker(certificate));
            }
            return certificates;
        }
    }
}
