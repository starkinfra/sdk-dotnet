using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class CreditHolmesTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<CreditHolmes> holmes = CreditHolmes.Create(new List<CreditHolmes>() { Example() });
            CreditHolmes sherlock = holmes.First();
            Assert.NotNull(sherlock.ID);
            CreditHolmes getCreditHolmes = CreditHolmes.Get(id: sherlock.ID);
            Assert.Equal(getCreditHolmes.ID, sherlock.ID);
            TestUtils.Log(sherlock);
        }

        [Fact]
        public void Query()
        {
            List<CreditHolmes> holmes = CreditHolmes.Query(limit: 5).ToList();
            Assert.True(holmes.Count <= 101);
            Assert.True(holmes.First().ID != holmes.Last().ID);
            foreach (CreditHolmes sherlock in holmes)
            {
                TestUtils.Log(sherlock);
                Assert.NotNull(sherlock.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CreditHolmes> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CreditHolmes.Page(limit: 1, cursor: cursor);
                foreach (CreditHolmes entity in page)
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

        internal static CreditHolmes Example() => new CreditHolmes(
            taxID: "012.345.678-90",
            competence: "2022-03"
        );
    }
}
