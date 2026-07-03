using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingTokenDesignTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingTokenDesign> designs = IssuingTokenDesign.Query(limit: 10).ToList();
            Assert.True(designs.Count <= 10);
            foreach (IssuingTokenDesign design in designs)
            {
                TestUtils.Log(design);
                Assert.NotNull(design.ID);
                Assert.NotNull(design.Name);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingTokenDesign> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingTokenDesign.Page(limit: 2, cursor: cursor);
                foreach (IssuingTokenDesign entity in page)
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
            Assert.True(ids.Count <= 4);
        }

        [Fact]
        public void QueryGet()
        {
            List<IssuingTokenDesign> designs = IssuingTokenDesign.Query(limit: 1).ToList();
            Assert.True(designs.Count <= 1);
            foreach (IssuingTokenDesign design in designs)
            {
                Assert.NotNull(design.ID);
                IssuingTokenDesign getDesign = IssuingTokenDesign.Get(design.ID);
                Assert.Equal(getDesign.ID, design.ID);
                TestUtils.Log(getDesign);
            }
        }

        [Fact]
        public void QueryIds()
        {
            List<IssuingTokenDesign> designs = IssuingTokenDesign.Query(limit: 2).ToList();
            List<string> designIdsExpected = new List<string>();
            foreach (IssuingTokenDesign design in designs)
            {
                Assert.NotNull(design.ID);
                designIdsExpected.Add(design.ID);
            }

            List<IssuingTokenDesign> designsResult = IssuingTokenDesign.Query(limit: 2, ids: designIdsExpected).ToList();
            List<string> designIdsResult = new List<string>();
            foreach (IssuingTokenDesign design in designsResult)
            {
                Assert.NotNull(design.ID);
                designIdsResult.Add(design.ID);
            }

            designIdsExpected.Sort();
            designIdsResult.Sort();
            Assert.Equal(designIdsExpected, designIdsResult);
        }

        [Fact]
        public void QueryParams()
        {
            List<IssuingTokenDesign> designs = IssuingTokenDesign.Query(
                limit: 10,
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(designs.Count == 0);
        }

        [Fact]
        public void PageParams()
        {
            List<IssuingTokenDesign> page;
            string cursor = null;
            (page, cursor) = IssuingTokenDesign.Page(
                cursor: null,
                limit: 10,
                ids: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }

        [Fact]
        public void QueryPdfGet()
        {
            List<IssuingTokenDesign> designs = IssuingTokenDesign.Query(limit: 1).ToList();
            Assert.True(designs.Count <= 1);
            foreach (IssuingTokenDesign design in designs)
            {
                byte[] pdf = IssuingTokenDesign.Pdf(id: design.ID);
                Assert.True(pdf.Length > 0);
                System.IO.File.WriteAllBytes("issuingtokendesign.pdf", pdf);
            }
        }

        [Fact]
        public void QueryGetDatetimeFields()
        {
            List<IssuingTokenDesign> designs = IssuingTokenDesign.Query(limit: 1).ToList();
            Assert.True(designs.Count <= 1);
            foreach (IssuingTokenDesign design in designs)
            {
                IssuingTokenDesign getDesign = IssuingTokenDesign.Get(design.ID);
                Assert.NotNull(getDesign.ID);
                Assert.NotNull(getDesign.Name);
                Assert.IsType<DateTime>(getDesign.Created);
                Assert.IsType<DateTime>(getDesign.Updated);
                TestUtils.Log(getDesign);
            }
        }
    }
}
