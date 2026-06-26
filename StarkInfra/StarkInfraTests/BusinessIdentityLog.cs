using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class BusinessIdentityLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<BusinessIdentity.Log> logs = BusinessIdentity.Log.Query(
                limit: 5
            ).ToList();
            Assert.True(logs.Count <= 101);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (BusinessIdentity.Log log in logs)
            {
                Assert.NotNull(log.ID);
                Assert.NotNull(log.Type);
            }
            BusinessIdentity.Log getLog = BusinessIdentity.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BusinessIdentity.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BusinessIdentity.Log.Page(limit: 2, cursor: cursor);
                foreach (BusinessIdentity.Log entity in page)
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
