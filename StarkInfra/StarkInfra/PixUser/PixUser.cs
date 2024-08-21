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

