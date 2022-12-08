using System;
using StarkInfra;
using Xunit;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class IssuingWithdrawalTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            IssuingWithdrawal withdrawal = IssuingWithdrawal.Create(Example());
            Assert.NotNull(withdrawal.ID);
            IssuingWithdrawal getIssuingWithdrawal = IssuingWithdrawal.Get(id: withdrawal.ID);
            Assert.Equal(getIssuingWithdrawal.ID, withdrawal.ID);
            TestUtils.Log(withdrawal);
        }

        [Fact]
        public void Query()
        {
            List<IssuingWithdrawal> withdrawals = IssuingWithdrawal.Query(limit: 3).ToList();
            Assert.True(withdrawals.Count <= 3);
            Assert.True(withdrawals.First().ID != withdrawals.Last().ID);
            foreach (IssuingWithdrawal withdrawal in withdrawals)
            {
                TestUtils.Log(withdrawal);
                Assert.NotNull(withdrawal.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingWithdrawal> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingWithdrawal.Page(limit: 2, cursor: cursor);
                foreach (IssuingWithdrawal entity in page)
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

        internal static IssuingWithdrawal Example()
        {
            return new IssuingWithdrawal(
                amount: 10000,
                externalID : Guid.NewGuid().ToString(),
                description : "Sending back"
            );
        }
    }
}
