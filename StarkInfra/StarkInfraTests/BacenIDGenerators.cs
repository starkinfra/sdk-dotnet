using Xunit;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using StarkInfra.Utils;


namespace StarkInfraTests
{
    public class BacenIDGeneratorsTest
    {
        private const string BankCode = "20018183";

        [Fact]
        public void PixSubscriptionBacenIDFormat()
        {
            string id = PixSubscriptionBacenID.Create(bankCode: BankCode, prefix: "RR");
            Assert.Equal(29, id.Length);
            Assert.Matches("^RR" + BankCode + @"\d{8}[a-zA-Z0-9]{11}$", id);
            Assert.Equal(DateTime.UtcNow.ToString("yyyyMMdd"), id.Substring(10, 8));
            Assert.NotEqual(id, PixSubscriptionBacenID.Create(bankCode: BankCode, prefix: "RR"));
        }

        [Fact]
        public void PixSubscriptionBacenIDIsUniqueInQuickSequence()
        {
            HashSet<string> ids = new HashSet<string>();
            for (int i = 0; i < 50; i++)
            {
                ids.Add(PixSubscriptionBacenID.Create(bankCode: BankCode, prefix: "RR"));
            }
            Assert.Equal(50, ids.Count);
        }

        [Fact]
        public void EndToEndIDFormat()
        {
            string id = StarkInfra.Utils.EndToEndID.Create(bankCode: BankCode);
            Assert.Equal(32, id.Length);
            Assert.Matches("^E" + BankCode + @"\d{12}[a-zA-Z0-9]{11}$", id);
        }

        [Fact]
        public void ReturnIDFormat()
        {
            string id = StarkInfra.Utils.ReturnID.Create(bankCode: BankCode);
            Assert.Equal(32, id.Length);
            Assert.Matches("^D" + BankCode + @"\d{12}[a-zA-Z0-9]{11}$", id);
        }

        [Fact]
        public void EndToEndIDAndReturnIDUseUtc()
        {
            foreach (string id in new[] { StarkInfra.Utils.EndToEndID.Create(bankCode: BankCode), StarkInfra.Utils.ReturnID.Create(bankCode: BankCode) })
            {
                DateTime generated = DateTime.ParseExact(
                    id.Substring(9, 12),
                    "yyyyMMddHHmm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
                );
                Assert.True(Math.Abs((DateTime.UtcNow - generated).TotalMinutes) <= 2);
            }
        }
    }
}
