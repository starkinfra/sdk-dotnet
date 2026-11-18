using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixKeyHolmesLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<PixKeyHolmes.Log> logs = PixKeyHolmes.Log.Query(limit: 5).ToList();
            Assert.NotEmpty(logs);
            foreach (PixKeyHolmes.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.NotNull(log.Holmes);
                Assert.NotNull(log.Holmes.ID);
                Assert.Contains(log.Type, new List<string> { "solving", "solved" });
                Assert.NotNull(log.Errors);
                Assert.NotEqual(default(DateTime), log.Created);
            }
            PixKeyHolmes.Log getLog = PixKeyHolmes.Log.Get(id: logs.First().ID);
            Assert.Equal(logs.First().ID, getLog.ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixKeyHolmes.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixKeyHolmes.Log.Page(limit: 5, cursor: cursor);
                foreach (PixKeyHolmes.Log entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count <= 10);
        }
    }
}
