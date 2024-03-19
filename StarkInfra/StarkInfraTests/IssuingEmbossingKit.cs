using StarkInfra;
using Xunit;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingEmbossingKitTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingEmbossingKit> kits = IssuingEmbossingKit.Query(limit: 10).ToList();
            Assert.True(kits.Count <= 4);
            foreach (IssuingEmbossingKit kit in kits) {
                TestUtils.Log(kit);
                Assert.NotNull(kit.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingEmbossingKit> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingEmbossingKit.Page(limit: 2, cursor: cursor);
                foreach (IssuingEmbossingKit entity in page)
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
            Assert.True(ids.Count == 2);
        }
        
        [Fact]
        public void QueryGet()
        {
            List<IssuingEmbossingKit> kits = IssuingEmbossingKit.Query(limit: 1).ToList();
            Assert.True(kits.Count <= 4);
            foreach (IssuingEmbossingKit kit in kits)
            {
                Assert.NotNull(kit.ID);
                IssuingEmbossingKit getDesign = IssuingEmbossingKit.Get(kit.ID);
                Assert.Equal(getDesign.ID, kit.ID);
                TestUtils.Log(getDesign);
            }
        }
    }
}
