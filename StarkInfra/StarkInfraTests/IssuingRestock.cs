using StarkInfra;
using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingRestockTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingRestock> restocks = IssuingRestock.Query(limit: 4).ToList();
            Assert.True(restocks.Count <= 4);
            foreach (IssuingRestock restock in restocks)
            {
                TestUtils.Log(restock);
                Assert.NotNull(restock.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingRestock> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingRestock.Page(limit: 2, cursor: cursor);
                foreach (IssuingRestock entity in page)
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
        public void QueryGet()
        {
            List<IssuingRestock> restocks = IssuingRestock.Query(limit: 4).ToList();
            Assert.True(restocks.Count <= 4);
            foreach (IssuingRestock toGetRestock in restocks)
            {
                IssuingRestock restock = IssuingRestock.Get(toGetRestock.ID);
                TestUtils.Log(restock);
                Assert.NotNull(restock.ID);
            }
        }

        [Fact]
        public void Create()
        {
            List<IssuingRestock> restocks = IssuingRestock.Create(Example());
            IssuingRestock restock = restocks.First();
            TestUtils.Log(restock);
            Assert.NotNull(restock.ID);
        }
        
        internal static List<IssuingRestock> Example()
        {
            IssuingStock stock = IssuingStock.Query(limit: 1).ToList().First();

            int generatedStockCount = new Random().Next(100, 1000);
            string generatedStockID = stock.ID;

            return new List<IssuingRestock>{
                new IssuingRestock(
                    count: generatedStockCount,
                    stockID: generatedStockID
                )
            };
        }
    }
}
