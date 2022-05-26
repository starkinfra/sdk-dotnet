using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class PixDomainTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<PixDomain> domains = PixDomain.Query().ToList();
            Assert.NotNull(domains);
            foreach (PixDomain domain in domains)
            {
                Assert.NotNull(domain.Certificates);
            }
        }
    }
}
