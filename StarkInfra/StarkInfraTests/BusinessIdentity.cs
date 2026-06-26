using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class BusinessIdentityTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<BusinessIdentity> identities = BusinessIdentity.Create(new List<BusinessIdentity>() { Example() });
            BusinessIdentity identity = identities.First();
            Assert.NotNull(identity.ID);
            BusinessIdentity getBusinessIdentity = BusinessIdentity.Get(id: identity.ID);
            Assert.Equal(getBusinessIdentity.ID, identity.ID);
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            List<BusinessIdentity> identities = BusinessIdentity.Create(new List<BusinessIdentity>() { Example() });
            BusinessIdentity identity = identities.First();
            BusinessIdentity getBusinessIdentity = BusinessIdentity.Get(id: identity.ID);
            Assert.Equal(getBusinessIdentity.ID, identity.ID);
            BusinessIdentity cancelBusinessIdentity = BusinessIdentity.Cancel(id: identity.ID);
            Assert.Equal(cancelBusinessIdentity.ID, identity.ID);
        }

        [Fact]
        public void Query()
        {
            List<BusinessIdentity> identities = BusinessIdentity.Query(limit: 5).ToList();
            Assert.True(identities.Count <= 101);
            Assert.True(identities.First().ID != identities.Last().ID);
            foreach (BusinessIdentity identity in identities)
            {
                Assert.NotNull(identity.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BusinessIdentity> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BusinessIdentity.Page(limit: 2, cursor: cursor);
                foreach (BusinessIdentity entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }

                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 4);
        }

        [Fact]
        public void Update()
        {
            List<BusinessIdentity> identities = BusinessIdentity.Create(new List<BusinessIdentity>() { Example() });
            BusinessIdentity identity = identities.First();
            Assert.NotNull(identity.ID);

            BusinessIdentity updatedBusinessIdentity = BusinessIdentity.Update(
                id: identity.ID,
                tags: new List<string> { "updated" }
            );

            Assert.NotNull(updatedBusinessIdentity.ID);
            Assert.Contains("updated", updatedBusinessIdentity.Tags);
        }

        internal static BusinessIdentity Example() => new BusinessIdentity(
            taxID: "20.018.183/0001-80",
            tags: new List<string> { "test", "testing" }
        );
    }
}
