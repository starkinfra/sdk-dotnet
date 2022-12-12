using Xunit;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingStockLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<IssuingStock.Log> logs = IssuingStock.Log.Query(limit: 10).ToList();
            Assert.Equal(10, logs.Count);
            foreach (IssuingStock.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
            }
            IssuingStock.Log getLog = IssuingStock.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingStock.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingStock.Log.Page(limit: 2, cursor: cursor);
                foreach (IssuingStock.Log entity in page)
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
    }
}
