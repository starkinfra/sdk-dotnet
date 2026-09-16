using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class LedgerTransactionTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            Ledger ledger = Ledger.Create(new List<Ledger>() { LedgerTest.Example() }).First();
            List<LedgerTransaction> transactions = LedgerTransaction.Create(
                new List<LedgerTransaction>() { Example(ledgerID: ledger.ID) }
            );
            LedgerTransaction transaction = transactions.First();
            Assert.NotNull(transaction.ID);
            LedgerTransaction getTransaction = LedgerTransaction.Get(id: transaction.ID);
            Assert.Equal(getTransaction.ID, transaction.ID);
        }

        [Fact]
        public void CreateOutputFields()
        {
            Ledger ledger = Ledger.Create(new List<Ledger>() { LedgerTest.Example() }).First();
            List<LedgerTransaction> transactions = LedgerTransaction.Create(
                new List<LedgerTransaction>() { Example(ledgerID: ledger.ID) }
            );
            LedgerTransaction transaction = transactions.First();
            Assert.NotNull(transaction.ID);
            Assert.NotNull(transaction.Balance);
        }

        [Fact]
        public void QueryByLedgerID()
        {
            Ledger ledger = Ledger.Create(new List<Ledger>() { LedgerTest.Example() }).First();
            LedgerTransaction.Create(new List<LedgerTransaction>() { Example(ledgerID: ledger.ID) });

            List<LedgerTransaction> transactions = LedgerTransaction.Query(ledgerID: ledger.ID, limit: 10).ToList();
            foreach (LedgerTransaction transaction in transactions)
            {
                Assert.NotNull(transaction.ID);
                Assert.Equal(ledger.ID, transaction.LedgerID);
            }
        }

        [Fact]
        public void QueryParams()
        {
            List<LedgerTransaction> transactions = LedgerTransaction.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                ids: new List<string> { "1", "2" },
                externalIds: new List<string> { "1", "2" },
                tags: new List<string> { "iron", "bank" }
            ).ToList();
            Assert.True(transactions.Count == 0);
        }

        [Fact]
        public void Page()
        {
            Ledger ledger = Ledger.Create(new List<Ledger>() { LedgerTest.Example() }).First();
            for (int i = 0; i < 4; i++)
            {
                LedgerTransaction.Create(new List<LedgerTransaction>() { Example(ledgerID: ledger.ID) });
            }

            List<string> ids = new List<string>();
            List<LedgerTransaction> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = LedgerTransaction.Page(ledgerID: ledger.ID, limit: 2, cursor: cursor);
                foreach (LedgerTransaction entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count <= 4);
        }

        [Fact]
        public void PageParams()
        {
            List<LedgerTransaction> page;
            string cursor = null;
            (page, cursor) = LedgerTransaction.Page(
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

        internal static LedgerTransaction Example(string ledgerID)
        {
            return new LedgerTransaction(
                amount: new Random().Next(1000, 9999),
                ledgerID: ledgerID,
                externalID: Convert.ToString(new Random().Next(1, 999999999)),
                source: "bank-transfer/" + Convert.ToString(new Random().Next(1, 999999)),
                tags: new List<string> { "transfer/123", "savings" }
            );
        }
    }
}
