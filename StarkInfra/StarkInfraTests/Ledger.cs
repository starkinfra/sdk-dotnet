using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class LedgerTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<Ledger> ledgers = Ledger.Create(new List<Ledger>() { Example() });
            Ledger ledger = ledgers.First();
            Assert.NotNull(ledger.ID);
            Ledger getLedger = Ledger.Get(id: ledger.ID);
            Assert.Equal(getLedger.ID, ledger.ID);
        }

        [Fact]
        public void CreateOutputFields()
        {
            List<Ledger> ledgers = Ledger.Create(new List<Ledger>() { Example() });
            Ledger ledger = ledgers.First();
            Assert.NotNull(ledger.ID);
            Assert.NotNull(ledger.Created);
            Assert.NotNull(ledger.Updated);
            Assert.IsType<DateTime>(ledger.Created.Value);
            Assert.IsType<DateTime>(ledger.Updated.Value);
        }

        [Fact]
        public void Query()
        {
            List<Ledger> ledgers = Ledger.Query(limit: 10).ToList();
            foreach (Ledger ledger in ledgers)
            {
                Assert.NotNull(ledger.ID);
            }
            Assert.True(ledgers.Count <= 10);
        }

        [Fact]
        public void QueryIds()
        {
            List<Ledger> ledgers = Ledger.Query(limit: 10).ToList();
            List<string> ledgerIdsExpected = new List<string>();
            foreach (Ledger ledger in ledgers)
            {
                Assert.NotNull(ledger.ID);
                ledgerIdsExpected.Add(ledger.ID);
            }

            List<Ledger> ledgersResult = Ledger.Query(limit: 10, ids: ledgerIdsExpected).ToList();
            List<string> ledgerIdsResult = new List<string>();
            foreach (Ledger ledger in ledgersResult)
            {
                Assert.NotNull(ledger.ID);
                ledgerIdsResult.Add(ledger.ID);
            }

            ledgerIdsExpected.Sort();
            ledgerIdsResult.Sort();
            Assert.Equal(ledgerIdsExpected, ledgerIdsResult);
        }

        [Fact]
        public void QueryParams()
        {
            List<Ledger> ledgers = Ledger.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                ids: new List<string> { "1", "2" },
                externalIds: new List<string> { "1", "2" },
                tags: new List<string> { "iron", "bank" }
            ).ToList();
            Assert.True(ledgers.Count == 0);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Ledger> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Ledger.Page(limit: 5, cursor: cursor);
                foreach (Ledger entity in page)
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
        public void PageParams()
        {
            List<Ledger> page;
            string cursor = null;
            (page, cursor) = Ledger.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                ids: new List<string> { "1", "2" },
                externalIds: new List<string> { "1", "2" },
                tags: new List<string> { "iron", "bank" }
            );
            Assert.True(page.Count == 0);
        }

        [Fact]
        public void Update()
        {
            List<Ledger> ledgers = Ledger.Query(limit: 1).ToList();
            Assert.NotEmpty(ledgers);
            Ledger ledger = ledgers.First();
            Ledger updatedLedger = Ledger.Update(
                id: ledger.ID,
                rules: new List<Ledger.Rule> { new Ledger.Rule(key: "minimumBalance", value: 0) }
            );
            Assert.NotNull(updatedLedger.ID);
        }

        internal static Ledger Example()
        {
            return new Ledger(
                externalID: Convert.ToString(new Random().Next(1, 999999999)),
                tags: new List<string> { "savings account", "spending counter" },
                metadata: new Dictionary<string, object> { { "accountId", "123" } },
                rules: new List<Ledger.Rule> { new Ledger.Rule(key: "minimumBalance", value: 0) }
            );
        }
    }
}
