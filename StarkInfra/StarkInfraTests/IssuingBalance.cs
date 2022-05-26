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
            IssuingBalance issuingBalance = IssuingBalance.Get();
            Assert.NotNull(issuingBalance.ID);
            Assert.True(issuingBalance.Amount >= 0);
            Assert.Equal(3, issuingBalance.Currency.Length);
            Assert.True(issuingBalance.Updated <= DateTime.UtcNow);
            TestUtils.Log(issuingBalance);
        }
    }
}
