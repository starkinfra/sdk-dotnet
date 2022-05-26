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
    /// The PixDomain object displays the QR Code domain certificate information of Pix participants.
    /// All certificates must be registered with the Central Bank.
    /// <br/>
    /// Properties:
    /// <list>
    ///    <item>Certificates [list of PixDomain.Certificate objects]: list of objects cantaining certificate information of Pix participants.</item>
    ///    <item>Name [string]: current active domain (URL) of the Pix participant.</item>
    /// </list>
    /// </summary>
    public partial class PixDomain : SubResource
    {
        public List<Certificate> Certificates { get; }
        public string Name { get; }

        /// <summary>
        /// PixDomain object
        /// <br/>
        /// The PixDomain object displays the QR Code domain certificate information of Pix participants.
        /// All certificates must be registered with the Central Bank.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///    <item>Certificates [list of PixDomain.Certificate objects]: list of objects cantaining certificate information of Pix participants.</item>
        ///    <item>name [string]: current active domain (URL) of the Pix participant.</item>
        /// </list>
        /// </summary>
        public PixDomain(List<Certificate> certificates = null, string name = null)
        {
            Certificates = certificates;
            Name = name;
        }

        /// <summary>
        /// Retrieve PixDomains
        /// <br/>
        /// Receive an IEnumerable of PixDomain objects previously created in the Stark Infra API
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixDomain objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixDomain> Query(User user = null)
        {

            (string resourceName, Api.ResourceMaker resourceMaker) = SubResource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {},
                user: user
            ).Cast<PixDomain>();
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) SubResource()
        {
            return (resourceName: "PixDomain", resourceMaker: ResourceMaker);
        }

        internal static SubResource ResourceMaker(dynamic json)
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
