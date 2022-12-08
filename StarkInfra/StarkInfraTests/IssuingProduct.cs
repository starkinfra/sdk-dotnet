using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class IssuingProductTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingProduct> products = IssuingProduct.Query(limit: 101).ToList();
            Assert.True(products.Count <= 101);
            Assert.True(products.First().ID != products.Last().ID);
            foreach (IssuingProduct product in products)
            {
                Assert.NotNull(product.ID);
                TestUtils.Log(product);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingProduct> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingProduct.Page(limit: 1, cursor: cursor);
                foreach (IssuingProduct entity in page)
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

            Assert.Equal(2, ids.Count);
        }
    }
}
