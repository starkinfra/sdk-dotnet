using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;


namespace StarkInfraTests
{
    public class PixInfractionLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<PixInfraction.Log> logs = PixInfraction.Log.Query(
                limit: 10,
                types: new List<string> { "canceled" }
            ).ToList();
            Assert.Equal(10, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (PixInfraction.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("canceled", log.Type);
            }
            PixInfraction.Log getLog = PixInfraction.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixInfraction.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixInfraction.Log.Page(limit: 5, cursor: cursor);
                foreach (PixInfraction.Log entity in page)
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
