using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class BusinessAccountRequestLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<BusinessAccountRequest.Log> logs = BusinessAccountRequest.Log.Query(
                limit: 5
            ).ToList();
            Assert.True(logs.Count <= 5);
            foreach (BusinessAccountRequest.Log log in logs)
            {
                Assert.NotNull(log.ID);
                Assert.IsType<BusinessAccountRequest>(log.Request);
            }
            if (logs.Count > 0)
            {
                BusinessAccountRequest.Log getLog = BusinessAccountRequest.Log.Get(id: logs.First().ID);
                Assert.Equal(getLog.ID, logs.First().ID);
            }
        }

        [Fact]
        public void QueryParams()
        {
            List<BusinessAccountRequest.Log> logs = BusinessAccountRequest.Log.Query(
                limit: 10,
                after: DateTime.Today.AddDays(-100),
                before: DateTime.Today,
                types: new List<string> { "created" },
                accountRequestIds: new List<string> { "1", "2", "3" }
            ).ToList();
            Assert.Empty(logs);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BusinessAccountRequest.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BusinessAccountRequest.Log.Page(limit: 2, cursor: cursor);
                foreach (BusinessAccountRequest.Log entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
        }
    }
}
