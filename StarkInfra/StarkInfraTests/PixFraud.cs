using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixFraudTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<PixFraud> frauds = PixFraud.Create(new List<PixFraud>() { Example() });
            PixFraud fraud = frauds.First();
            Assert.NotNull(frauds.First().ID);
            PixFraud getPixFraud = PixFraud.Get(id: fraud.ID);
            Assert.Equal(getPixFraud.ID, fraud.ID);
        }

        [Fact]
        public void CreateOutputFields()
        {
            List<PixFraud> frauds = PixFraud.Create(new List<PixFraud>() { Example() });
            PixFraud fraud = frauds.First();
            Assert.NotNull(fraud.ID);
            Assert.NotNull(fraud.BacenID);
            Assert.False(string.IsNullOrEmpty(fraud.Status));
            Assert.NotNull(fraud.Created);
            Assert.NotNull(fraud.Updated);
            Assert.IsType<DateTime>(fraud.Created.Value);
            Assert.IsType<DateTime>(fraud.Updated.Value);
        }

        [Fact]
        public void Query()
        {
            List<PixFraud> frauds = PixFraud.Query(limit: 10).ToList();
            foreach (PixFraud fraud in frauds)
            {
                Assert.NotNull(fraud.ID);
            }
            Assert.True(frauds.Count <= 10);
        }

        [Fact]
        public void QueryIds()
        {
            List<PixFraud> frauds = PixFraud.Query(limit: 10).ToList();
            List<string> fraudIdsExpected = new List<string>();
            foreach (PixFraud fraud in frauds)
            {
                Assert.NotNull(fraud.ID);
                fraudIdsExpected.Add(fraud.ID);
            }

            List<PixFraud> fraudsResult = PixFraud.Query(limit: 10, ids: fraudIdsExpected).ToList();
            List<string> fraudIdsResult = new List<string>();
            foreach (PixFraud fraud in fraudsResult)
            {
                Assert.NotNull(fraud.ID);
                fraudIdsResult.Add(fraud.ID);
            }

            fraudIdsExpected.Sort();
            fraudIdsResult.Sort();
            Assert.Equal(fraudIdsExpected, fraudIdsResult);
        }

        [Fact]
        public void QueryParams()
        {
            List<PixFraud> frauds = PixFraud.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "registered" },
                ids: new List<string> { "1", "2" },
                tags: new List<string> { "iron", "bank" }
            ).ToList();
            Assert.True(frauds.Count == 0);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixFraud> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixFraud.Page(limit: 5, cursor: cursor);
                foreach (PixFraud entity in page)
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
            List<PixFraud> page;
            string cursor = null;
            (page, cursor) = PixFraud.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "registered" },
                ids: new List<string> { "1", "2" },
                tags: new List<string> { "iron", "bank" }
            );
            Assert.True(page.Count == 0);
        }


        [Fact]
        public void TypeIsPopulated()
        {
            PixFraud fraud = Example();
            Assert.False(string.IsNullOrEmpty(fraud.Type));
        }

        internal static PixFraud Example()
        {
            return new PixFraud(
                externalID: Convert.ToString(new Random().Next(1, 999999999)),
                type: "scam",
                taxID: "01234567890",
                tags: new List<string> { "fraudulent" }
            );
        }
    }
}
