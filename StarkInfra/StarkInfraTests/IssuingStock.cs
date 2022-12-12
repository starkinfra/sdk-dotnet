using StarkInfra;
using Xunit;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingStockTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingStock> stocks = IssuingStock.Query(limit: 4).ToList();
            Assert.True(stocks.Count <= 4);
            foreach (IssuingStock stock in stocks)
            {
                TestUtils.Log(stock);
                Assert.NotNull(stock.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingStock> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingStock.Page(limit: 2, cursor: cursor);
                foreach (IssuingStock entity in page)
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
            Assert.True(ids.Count == 2);
        }
        
        [Fact]
        public void QueryGet()
        {
            List<IssuingStock> stocks = IssuingStock.Query(limit: 1).ToList();
            Assert.True(stocks.Count <= 1);
            foreach (IssuingStock toGetStock in stocks)
            {
                IssuingStock stock = IssuingStock.Get(toGetStock.ID);
                TestUtils.Log(stock);
                Assert.NotNull(stock.ID);
            }
        }
    }
}
