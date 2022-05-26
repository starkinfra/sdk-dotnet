using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class IssuingPurchaseTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<IssuingPurchase> purchases = IssuingPurchase.Query(limit: 1).ToList();
            IssuingPurchase purchase = purchases.First();
            IssuingPurchase getPurchase = IssuingPurchase.Get(purchase.ID);
            Assert.NotNull(getPurchase);
            TestUtils.Log(getPurchase);
        }

        [Fact]
        public void Query()
        {
            List<IssuingPurchase> purchases = IssuingPurchase.Query(limit: 3, status: "canceled").ToList();
            Assert.True(purchases.Count <= 3);
            foreach (IssuingPurchase purchase in purchases)
            {
                TestUtils.Log(purchase);
                Assert.NotNull(purchase.ID);
                Assert.Equal("canceled", purchase.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingPurchase> page;
            string cursor = null;
            for (int i = 0; i < 3; i++)
            {
                (page, cursor) = IssuingPurchase.Page(limit: 1, cursor: cursor);
                foreach (IssuingPurchase entity in page)
                {
                    TestUtils.Log(entity);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 3);
        }
    }
}
