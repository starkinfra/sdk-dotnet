using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using StarkInfra;
using Xunit;

namespace StarkInfraTests
{
	public class PixUserTest
	{
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            PixUser pixUser = PixUser.Get(id: "01234567890", keyId: "+5511946823840");
            Assert.NotNull(pixUser.Statistics);
        }
    }
}