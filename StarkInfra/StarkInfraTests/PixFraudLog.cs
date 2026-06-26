using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixFraudLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<PixFraud.Log> logs = PixFraud.Log.Query(limit: 10).ToList();
            Assert.True(logs.Count <= 10);
            foreach (PixFraud.Log log in logs)
            {
                Assert.NotNull(log.ID);
                Assert.False(string.IsNullOrEmpty(log.Type));
                Assert.NotNull(log.Created);
                Assert.IsType<DateTime>(log.Created.Value);
            }
            PixFraud.Log getLog = PixFraud.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
        }

        [Fact]
        public void FraudDeserializesToPixFraud()
        {
            List<PixFraud.Log> logs = PixFraud.Log.Query(limit: 1).ToList();
            Assert.NotEmpty(logs);
            PixFraud.Log log = logs.First();
            Assert.NotNull(log.Fraud);
            Assert.IsType<PixFraud>(log.Fraud);
            Assert.NotNull(log.Fraud.ID);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixFraud.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixFraud.Log.Page(limit: 5, cursor: cursor);
                foreach (PixFraud.Log entity in page)
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

        [Fact]
        public void QueryParams()
        {
            List<PixFraud.Log> logs = PixFraud.Log.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                types: new List<string> { "registered" },
                fraudIds: new List<string> { "1", "2" },
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(logs.Count == 0);
        }

        [Fact]
        public void PageParams()
        {
            List<PixFraud.Log> page;
            string cursor = null;
            (page, cursor) = PixFraud.Log.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                types: new List<string> { "registered" },
                fraudIds: new List<string> { "1", "2" },
                ids: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }
    }
}
