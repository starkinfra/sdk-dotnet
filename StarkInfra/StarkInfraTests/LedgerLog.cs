using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class LedgerLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<Ledger.Log> logs = Ledger.Log.Query(limit: 10).ToList();
            Assert.True(logs.Count <= 10);
            foreach (Ledger.Log log in logs)
            {
                Assert.NotNull(log.ID);
                Assert.False(string.IsNullOrEmpty(log.Type));
                Assert.NotNull(log.Created);
                Assert.IsType<DateTime>(log.Created.Value);
            }
            Ledger.Log getLog = Ledger.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
        }

        [Fact]
        public void LedgerDeserializesToLedger()
        {
            List<Ledger.Log> logs = Ledger.Log.Query(limit: 1).ToList();
            Assert.NotEmpty(logs);
            Ledger.Log log = logs.First();
            Assert.NotNull(log.Ledger);
            Assert.IsType<Ledger>(log.Ledger);
            Assert.NotNull(log.Ledger.ID);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Ledger.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Ledger.Log.Page(limit: 5, cursor: cursor);
                foreach (Ledger.Log entity in page)
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
            List<Ledger.Log> logs = Ledger.Log.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                ledgerID: "1",
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(logs.Count == 0);
        }

        [Fact]
        public void PageParams()
        {
            List<Ledger.Log> page;
            string cursor = null;
            (page, cursor) = Ledger.Log.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                ledgerID: "1",
                ids: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }
    }
}
