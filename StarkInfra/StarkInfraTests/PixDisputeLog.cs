using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixDisputeLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<PixDispute.Log> logs = PixDispute.Log.Query(limit: 10).ToList();
            Assert.True(logs.Count == 10);
            foreach (PixDispute.Log log in logs)
            {
                Assert.NotNull(log.ID);
            }
            PixDispute.Log getLog = PixDispute.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixDispute.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixDispute.Log.Page(limit: 5, cursor: cursor);
                foreach (PixDispute.Log entity in page)
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

