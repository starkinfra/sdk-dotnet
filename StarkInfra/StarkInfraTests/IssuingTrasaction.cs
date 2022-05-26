using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class IssuingTransactionTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<IssuingTransaction> transactions = IssuingTransaction.Query(limit: 1).ToList();
            IssuingTransaction transaction = transactions.First();
            IssuingTransaction getTransaction = IssuingTransaction.Get(transaction.ID);
            Assert.NotNull(getTransaction);
            TestUtils.Log(getTransaction);
        }

        [Fact]
        public void Query()
        {
            List<IssuingTransaction> transactions = IssuingTransaction.Query(limit: 2).ToList();
            Assert.True(transactions.Count <= 2);
            Assert.True(transactions.First().ID != transactions.Last().ID);
            foreach (IssuingTransaction transaction in transactions)
            {
                TestUtils.Log(transaction);
                Assert.NotNull(transaction.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingTransaction> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingTransaction.Page(limit: 2, cursor: cursor);
                foreach (IssuingTransaction entity in page)
                {
                    TestUtils.Log(entity);
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
