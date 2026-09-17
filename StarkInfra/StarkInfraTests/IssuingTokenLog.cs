using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingTokenLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<IssuingToken.Log> logs = IssuingToken.Log.Query(
                limit: 2,
                types: new List<string> { "blocked" }
            ).ToList();
            Assert.Equal(2, logs.Count);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (IssuingToken.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("blocked", log.Type);
                foreach (Dictionary<string, object> error in log.Errors)
                {
                    Assert.True(error.ContainsKey("code"));
                    Assert.True(error.ContainsKey("message"));
                }
            }
            IssuingToken.Log getLog = IssuingToken.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingToken.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingToken.Log.Page(limit: 2, cursor: cursor);
                foreach (IssuingToken.Log entity in page)
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
