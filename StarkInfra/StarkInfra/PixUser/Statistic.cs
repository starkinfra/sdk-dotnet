using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;
using StarkCore.Utils;

namespace StarkInfra
{
    public partial class Statistic : StarkCore.Utils.SubResource
    {

        public DateTime? After { get; }
        public string Source { get; }
        public string Type { get; }
        public int Value { get; }
        public DateTime? Updated { get; }

        public Statistic(DateTime? after, string source, string type, int value, DateTime? updated)
        {

            After = after;
            Source = source;
            Type = type;
            Value = value;
            Updated = updated;

        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Statistic", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            DateTime? after = json.after;
            string source = json.source;
            string type = json.type;
            int value = json.value;
            DateTime? updated = json.updated;

            return new Statistic(after: after, source: source, type: type, value: value, updated: updated);
        }
    }
}
