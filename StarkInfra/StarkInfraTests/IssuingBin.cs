using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class IssuingBinTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingBin> IssuingBins = IssuingBin.Query(limit: 101).ToList();
            Assert.True(IssuingBins.Count <= 101);
            Assert.True(IssuingBins.First().ID != IssuingBins.Last().ID);
            foreach (IssuingBin IssuingBin in IssuingBins)
            {
                Assert.NotNull(IssuingBin.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingBin> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingBin.Page(limit: 1, cursor: cursor);
                foreach (IssuingBin entity in page)
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

            Assert.Equal(2, ids.Count);
        }
    }
}
