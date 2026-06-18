using Xunit;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingDesignTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingDesign> designs = IssuingDesign.Query(limit: 10).ToList();
            Assert.True(designs.Count <= 4);
            foreach (IssuingDesign design in designs) {
                TestUtils.Log(design);
                Assert.NotNull(design.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingDesign> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingDesign.Page(limit: 2, cursor: cursor);
                foreach (IssuingDesign entity in page)
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
            List<IssuingDesign> designs = IssuingDesign.Query(limit: 1).ToList();
            Assert.True(designs.Count <= 4);
            foreach (IssuingDesign design in designs)
            {
                Assert.NotNull(design.ID);
                IssuingDesign getDesign = IssuingDesign.Get(design.ID);
                Assert.Equal(getDesign.ID, design.ID);
                TestUtils.Log(getDesign);
            }
        }
        
        [Fact]
        public void QueryPdfGet()
        {
            List<IssuingDesign> designs = IssuingDesign.Query(limit: 1).ToList();
            Assert.True(designs.Count <= 4);
            foreach (IssuingDesign design in designs)
            {
                byte[] pdf = IssuingDesign.Pdf(id: design.ID);
                Assert.True(pdf.Length > 0);
                System.IO.File.WriteAllBytes("issuingdesign.pdf", pdf);
            }
        }

        [Fact]
        public void QueryType()
        {
            List<string> validTypes = new List<string> { "card", "envelope" };
            List<IssuingDesign> designs = IssuingDesign.Query(limit: 4).ToList();
            Assert.True(designs.Count <= 4);
            foreach (IssuingDesign design in designs)
            {
                TestUtils.Log(design);
                Assert.NotNull(design.ID);
                Assert.NotNull(design.Type);
                Assert.Contains(design.Type, validTypes);
            }
        }

        [Fact]
        public void GetAndVerifyType()
        {
            List<string> validTypes = new List<string> { "card", "envelope" };
            List<IssuingDesign> designs = IssuingDesign.Query(limit: 1).ToList();
            Assert.True(designs.Count <= 4);
            foreach (IssuingDesign design in designs)
            {
                IssuingDesign getDesign = IssuingDesign.Get(design.ID);
                Assert.Equal(getDesign.ID, design.ID);
                Assert.NotNull(getDesign.Type);
                Assert.Contains(getDesign.Type, validTypes);
                TestUtils.Log(getDesign);
            }
        }
    }
}
