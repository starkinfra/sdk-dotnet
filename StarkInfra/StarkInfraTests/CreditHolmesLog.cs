using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;


namespace StarkInfraTests
{
    public class CreditHolmesLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<CreditHolmes.Log> logs = CreditHolmes.Log.Query(
                limit: 1,
                types: new List<string> { "created" }
            ).ToList();
            Assert.Equal(1, logs.Count);
            foreach (CreditHolmes.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.Equal("created", log.Type);
            }
            CreditHolmes.Log getLog = CreditHolmes.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
            TestUtils.Log(getLog);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CreditHolmes.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CreditHolmes.Log.Page(limit: 2, cursor: cursor);
                foreach (CreditHolmes.Log entity in page)
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
