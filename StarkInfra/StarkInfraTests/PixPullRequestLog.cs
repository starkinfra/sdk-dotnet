using Xunit;
using StarkInfra;
using StarkInfra.Utils;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixPullRequestLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<PixPullRequest.Log> logs = PixPullRequest.Log.Query(limit: 10).ToList();
            Assert.True(logs.Count <= 10);
            foreach (PixPullRequest.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<PixPullRequest.Log> logs = new List<PixPullRequest.Log>();
            List<PixPullRequest.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixPullRequest.Log.Page(limit: 5, cursor: cursor);
                foreach (PixPullRequest.Log entity in page)
                {
                    Assert.DoesNotContain(entity, logs);
                    logs.Add(entity);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(logs.Count <= 10);
        }

        [Fact]
        public void QueryParams()
        {
            List<PixPullRequest.Log> logs = PixPullRequest.Log.Query(
                limit: 10,
                after: new DateTime(2026, 1, 1),
                before: new DateTime(2026, 4, 30),
                types: new List<string> { "failed" },
                requestIds: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(logs.Count == 0);
        }

        [Fact]
        public void Get()
        {
            List<PixPullRequest.Log> logs = PixPullRequest.Log.Query(limit: 1).ToList();
            if (logs.Count == 0) return;
            PixPullRequest.Log fetched = PixPullRequest.Log.Get(logs[0].ID);
            Assert.Equal(fetched.ID, logs[0].ID);
            TestUtils.Log(fetched);
        }
    }
}
