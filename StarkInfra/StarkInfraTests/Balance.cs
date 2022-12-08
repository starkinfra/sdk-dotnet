using System;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    public class BalanceTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            PixBalance balance = PixBalance.Get();
            Assert.NotNull(balance.ID);
            Assert.True(balance.Amount >= 0);
            Assert.Equal(3, balance.Currency.Length);
            Assert.True(balance.Updated <= DateTime.UtcNow);
            TestUtils.Log(balance);
        }
    }
}
