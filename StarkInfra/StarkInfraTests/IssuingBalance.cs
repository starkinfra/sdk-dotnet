using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;


namespace StarkInfraTests
{
    public class IssuingBalanceTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            IssuingBalance balance = IssuingBalance.Get();
            Assert.NotNull(balance.ID);
            Assert.True(balance.Amount >= 0);
            Assert.Equal(3, balance.Currency.Length);
            Assert.True(balance.Updated <= DateTime.UtcNow);
            TestUtils.Log(balance);
        }
    }
}
