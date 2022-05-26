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
            PixBalance pixBalance = PixBalance.Get();
            Assert.NotNull(pixBalance.ID);
            Assert.True(pixBalance.Amount >= 0);
            Assert.Equal(3, pixBalance.Currency.Length);
            Assert.True(pixBalance.Updated <= DateTime.UtcNow);
            TestUtils.Log(pixBalance);
        }
    }
}
