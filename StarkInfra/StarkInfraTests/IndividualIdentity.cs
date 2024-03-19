using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IndividualIdentityTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<IndividualIdentity> identities = IndividualIdentity.Create(new List<IndividualIdentity>() { Example() });
            IndividualIdentity identity = identities.First();
            Assert.NotNull(identity.ID);
            IndividualIdentity getIndividualIdentity = IndividualIdentity.Get(id: identity.ID);
            Assert.Equal(getIndividualIdentity.ID, identity.ID);
            TestUtils.Log(identity);
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            List<IndividualIdentity> identities = IndividualIdentity.Create(new List<IndividualIdentity>() { Example() });
            IndividualIdentity identity = identities.First();
            TestUtils.Log(identity);
            IndividualIdentity getIndividualIdentity = IndividualIdentity.Get(id: identity.ID);
            Assert.Equal(getIndividualIdentity.ID, identity.ID);
            IndividualIdentity cancelIndividualIdentity = IndividualIdentity.Cancel(id: identity.ID);
            Assert.Equal(cancelIndividualIdentity.ID, identity.ID);
            TestUtils.Log(identity);
        }

        [Fact]
        public void Query()
        {
            List<IndividualIdentity> identities = IndividualIdentity.Query(limit: 5, status: new List<string> { "canceled" }).ToList();
            Assert.True(identities.Count <= 101);
            Assert.True(identities.First().ID != identities.Last().ID);
            foreach (IndividualIdentity identity in identities)
            {
                TestUtils.Log(identity);
                Assert.NotNull(identity.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IndividualIdentity> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IndividualIdentity.Page(limit: 1, cursor: cursor);
                foreach (IndividualIdentity entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }

                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 2);
        }

        internal static IndividualIdentity Example() => new IndividualIdentity(
            name: "Jamie Lannister",
            taxID: "012.345.678-90"
        );
    }
}
