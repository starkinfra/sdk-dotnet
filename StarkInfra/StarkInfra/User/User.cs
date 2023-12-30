using StarkInfra.Utils;
using StarkCore;


namespace StarkInfra
{
    public abstract class User : StarkCore.User
    {
        public User (string environment, string id, string privateKey) : base(environment, id, privateKey) { }
    }
}
