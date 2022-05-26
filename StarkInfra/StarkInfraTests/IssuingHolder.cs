using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class IssuingHolderTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingHolder> holders = IssuingHolder.Query(limit: 3, status: "active").ToList();
            Assert.True(holders.Count <= 3);
            Assert.True(holders.First().ID != holders.Last().ID);
            foreach (IssuingHolder holder in holders)
            {
                TestUtils.Log(holder);
                Assert.NotNull(holder.ID);
                Assert.Equal("active", holder.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingHolder> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingHolder.Page(limit: 2, cursor: cursor);
                foreach (IssuingHolder entity in page)
                {
                    TestUtils.Log(entity);
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
            List<IssuingHolder> holders = IssuingHolder.Query(limit: 2, status: "active").ToList();
            Assert.Equal(2, holders.Count);
            Assert.True(holders.First().ID != holders.Last().ID);
            Dictionary<string, object> patchData = new Dictionary<string, object> {
                { "status", "blocked" }
            };
            foreach (IssuingHolder holder in holders)
            {
                TestUtils.Log(holder);
                Assert.NotNull(holder.ID);
                Assert.Equal("active", holder.Status);
                IssuingHolder updatedHolder = IssuingHolder.Update(id: holder.ID, patchData: patchData);
                TestUtils.Log(updatedHolder);
                Assert.Equal("blocked", updatedHolder.Status);
            }
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"expand", new List<string> {"rules"}}
            };
            List<IssuingHolder> holders = IssuingHolder.Create(new List<IssuingHolder>() { Example() }, parameters: parameters);
            IssuingHolder holder = holders.First();
            TestUtils.Log(holder);
            Assert.NotNull(holder.ID);
            IssuingHolder getHolder = IssuingHolder.Get(id: holder.ID, parameters: parameters);
            Assert.Equal(getHolder.ID, holder.ID);
            IssuingHolder canceledgHolder = IssuingHolder.Cancel(id: holder.ID);
            Assert.Equal(canceledgHolder.ID, holder.ID);
            Assert.Equal(canceledgHolder.Status, "canceled");
            TestUtils.Log(holder);
        }

        internal static IssuingHolder Example()
        {
            return new IssuingHolder(
                name: "Iron Bank S.A.",
                externalId: Guid.NewGuid().ToString(),
                taxId: "012.345.678-90",
                tags: new List<string> { "Traveler Employee" },
                rules: new List<StarkInfra.IssuingRule> {
                    new StarkInfra.IssuingRule(
                        name: "general",
                        interval: "week",
                        amount: 100000,
                        currencyCode: "USD"
                    )
                }
            );
        }
    }
}
