using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixKeyHolmesTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Create()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Create(new List<PixKeyHolmes>() { Example() });
            PixKeyHolmes sherlock = holmes.First();
            Assert.NotNull(sherlock.ID);
            TestUtils.Log(sherlock);
        }

        [Fact]
        public void CreateOutputFields()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Create(new List<PixKeyHolmes>() { Example() });
            PixKeyHolmes sherlock = holmes.First();
            Assert.NotNull(sherlock.ID);
            Assert.False(string.IsNullOrEmpty(sherlock.Status));
            Assert.NotNull(sherlock.Created);
            Assert.NotNull(sherlock.Updated);
            Assert.IsType<DateTime>(sherlock.Created.Value);
            Assert.IsType<DateTime>(sherlock.Updated.Value);
            TestUtils.Log(sherlock);
        }

        [Fact]
        public void Query()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Query(limit: 10).ToList();
            foreach (PixKeyHolmes sherlock in holmes)
            {
                TestUtils.Log(sherlock);
                Assert.NotNull(sherlock.ID);
            }
            Assert.True(holmes.Count <= 10);
        }

        [Fact]
        public void QueryIds()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Query(limit: 10).ToList();
            List<string> holmesIdsExpected = new List<string>();
            foreach (PixKeyHolmes sherlock in holmes)
            {
                Assert.NotNull(sherlock.ID);
                holmesIdsExpected.Add(sherlock.ID);
            }

            List<PixKeyHolmes> holmesResult = PixKeyHolmes.Query(limit: 10, ids: holmesIdsExpected).ToList();
            List<string> holmesIdsResult = new List<string>();
            foreach (PixKeyHolmes sherlock in holmesResult)
            {
                Assert.NotNull(sherlock.ID);
                holmesIdsResult.Add(sherlock.ID);
            }

            holmesIdsExpected.Sort();
            holmesIdsResult.Sort();
            Assert.Equal(holmesIdsExpected, holmesIdsResult);
        }

        [Fact]
        public void QueryParams()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "solved" },
                tags: new List<string> { "iron", "bank" },
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(holmes.Count == 0);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixKeyHolmes> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixKeyHolmes.Page(limit: 5, cursor: cursor);
                foreach (PixKeyHolmes entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count <= 10);
        }

        [Fact]
        public void PageParams()
        {
            List<PixKeyHolmes> page;
            string cursor = null;
            (page, cursor) = PixKeyHolmes.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "solved" },
                tags: new List<string> { "iron", "bank" },
                ids: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }

        [Fact]
        public void ExposesOnlyCreateQueryPage()
        {
            Type type = typeof(PixKeyHolmes);
            Assert.Null(type.GetMethod("Get", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.Null(type.GetMethod("Cancel", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.Null(type.GetMethod("Delete", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.NotNull(type.GetMethod("Create", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.NotNull(type.GetMethod("Query", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.NotNull(type.GetMethod("Page", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
        }

        [Fact]
        public void TagsDefaultWhenOmitted()
        {
            PixKeyHolmes sherlock = new PixKeyHolmes(keyID: "valid@sandbox.com");
            Assert.Null(sherlock.Tags);
            Assert.Equal("valid@sandbox.com", sherlock.KeyID);
        }

        internal static PixKeyHolmes Example()
        {
            return new PixKeyHolmes(
                keyID: "valid@sandbox.com",
                tags: new List<string> { "travel", "food" }
            );
        }
    }
}
