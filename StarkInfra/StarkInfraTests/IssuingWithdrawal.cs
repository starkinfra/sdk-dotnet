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
            IssuingWithdrawal issuingWithdrawal = IssuingWithdrawal.Create(Example());
            Assert.NotNull(issuingWithdrawal.ID);
            IssuingWithdrawal getIssuingWithdrawal = IssuingWithdrawal.Get(id: issuingWithdrawal.ID);
            Assert.Equal(getIssuingWithdrawal.ID, issuingWithdrawal.ID);
            TestUtils.Log(issuingWithdrawal);
        }

        [Fact]
        public void Query()
        {
            List<IssuingWithdrawal> issuingWithdrawals = IssuingWithdrawal.Query(limit: 3).ToList();
            Assert.True(issuingWithdrawals.Count <= 3);
            Assert.True(issuingWithdrawals.First().ID != issuingWithdrawals.Last().ID);
            foreach (IssuingWithdrawal issuingWithdrawal in issuingWithdrawals)
            {
                TestUtils.Log(issuingWithdrawal);
                Assert.NotNull(issuingWithdrawal.ID);
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
                externalId : Guid.NewGuid().ToString(),
                description : "Sending back"
            );
        }
    }
}
