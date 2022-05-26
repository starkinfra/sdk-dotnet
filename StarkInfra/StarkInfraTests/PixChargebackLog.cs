using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;


namespace StarkInfraTests
{
    public class PixChargebackLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<PixChargeback.Log> logs = PixChargeback.Log.Query(
                limit: 10,
                types: new List<string> { "canceled" }
                ).ToList();
            Assert.Equal(10, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (PixChargeback.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("canceled", log.Type);
            }
            PixChargeback.Log getLog = PixChargeback.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixChargeback.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixChargeback.Log.Page(limit: 5, cursor: cursor);
                foreach (PixChargeback.Log entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 10);
        }
    }
}
