﻿using System;
using System.Globalization;
using StarkInfra.Utils;
using Xunit;


namespace StarkInfraTests
{
    public class EndToEndIDTest
    {
        [Fact]
        public void CreateUsesUtc()
        {
            string endToEndID = EndToEndID.Create(bankCode: "32160637");
            Assert.Equal(32, endToEndID.Length);
            Assert.Matches(@"^E32160637\d{12}[a-zA-Z0-9]{11}$", endToEndID);

            DateTime generated = DateTime.ParseExact(
                endToEndID.Substring(9, 12),
                "yyyyMMddHHmm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
            );
            Assert.True(Math.Abs((DateTime.UtcNow - generated).TotalMinutes) <= 2);
        }
    }
}
