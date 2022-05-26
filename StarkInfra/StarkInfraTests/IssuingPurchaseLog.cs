using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;


namespace StarkInfraTests
{
    public class IssuingPurchaseLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<IssuingPurchase.Log> logs = IssuingPurchase.Log.Query(limit: 2).ToList();
            Assert.Equal(2, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (IssuingPurchase.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
            }
            IssuingPurchase.Log getLog = IssuingPurchase.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingPurchase.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingPurchase.Log.Page(limit: 2, cursor: cursor);
                foreach (IssuingPurchase.Log entity in page)
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
