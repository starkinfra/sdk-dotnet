using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixRequestLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<PixRequest.Log> logs = PixRequest.Log.Query(
                limit: 2,
                types: new List<string> { "denied" }
            ).ToList();
            Assert.Equal(2, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (PixRequest.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("denied", log.Type);
            }
            PixRequest.Log getLog = PixRequest.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixRequest.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixRequest.Log.Page(limit: 5, cursor: cursor);
                foreach (PixRequest.Log entity in page)
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
