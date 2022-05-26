using System.Linq;
using System.Reflection;
using System.Collections.Generic;


namespace StarkInfra.Utils
{
    public abstract class Resource : SubResource
    {
        public string ID { get; }

        public Resource(string id)
        {
            ID = id;
        }
    }
}
