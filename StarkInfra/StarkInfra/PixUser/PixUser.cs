using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;
using static StarkCore.Utils.Api;
using StarkCore;

namespace StarkInfra
{
    public partial class PixUser : Resource
    {

        public List<Statistic> Statistics { get; }

        public PixUser(List<Statistic> statistics, string id = null) : base(id)
        {

            Statistics = statistics;

        }

        /// <summary>
        /// Retrieve a PixUser object
        /// <br/>
        /// Retrieve a user's aggregated Pix statistics (Pix keys, Pix requests, frauds and infractions) by their tax id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: user's tax ID (CPF/CNPJ). ex: "012.345.678-90"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>keyId [string, default null]: PixKey id to scope the statistics to a specific key.</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>PixUser object with a list of Statistic objects. Each Statistic has a `source` ("pix-key", "pix-fraud", "pix-request", "pix-infraction") and a `type` whose valid values depend on the source: "registered"/"unique" (pix-key); "settled" (pix-request); "identity"/"mule"/"scam"/"other"/"unknown"/"amount"/"unique" (pix-fraud); "open"/"denied"/"unique" (pix-infraction).</item>
        /// </list>
        /// </summary>
        public static PixUser Get(string id, string keyId = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                query: new Dictionary<string, object> {
                    { "keyId" , keyId }
                },
                user: user
            ) as PixUser;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixUser", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            string id = json.id;
            List<Statistic> statistics = ParsePixUser(json.statistics);

            return new PixUser(id: id, statistics: statistics);
        }

        private static List<Statistic> ParsePixUser(dynamic json)
        {
            List<Statistic> statistics = new List<Statistic>();

            foreach (dynamic statistic in json)
            {
                statistics.Add(Statistic.ResourceMaker(statistic));
            }
            return statistics;
        }

    }
}

