using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixReversalTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<PixReversal> pixReversals = PixReversal.Create(new List<PixReversal>() { Example() });
            PixReversal pixReversal = pixReversals.First();
            Assert.NotNull(pixReversals.First().ID);
            PixReversal getPixReversal = PixReversal.Get(id: pixReversal.ID);
            Assert.Equal(getPixReversal.ID, pixReversal.ID);
            TestUtils.Log(pixReversal);
        }

        [Fact]
        public void Query()
        {
            List<PixReversal> pixReversals = PixReversal.Query(limit: 101, status: "success").ToList();
            Assert.True(pixReversals.Count <= 101);
            Assert.True(pixReversals.First().ID != pixReversals.Last().ID);
            foreach (PixReversal pixReversal in pixReversals)
            {
                TestUtils.Log(pixReversal);
                Assert.NotNull(pixReversal.ID);
                Assert.Equal("success", pixReversal.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixReversal> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixReversal.Page(limit: 5, cursor: cursor);
                foreach (PixReversal entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 10);
        }

        [Fact]
        public void QueryIds()
        {
            List<PixReversal> pixReversals = PixReversal.Query(limit: 10).ToList();
            List<String> pixReversalsIdsExpected = new List<string>();
            Assert.Equal(10, pixReversals.Count);
            Assert.True(pixReversals.First().ID != pixReversals.Last().ID);
            foreach (PixReversal transaction in pixReversals)
            {
                Assert.NotNull(transaction.ID);
                pixReversalsIdsExpected.Add(transaction.ID);
            }

            List<PixReversal> pixReversalsResult = PixReversal.Query(limit:10, ids: pixReversalsIdsExpected).ToList();
            List<String> pixReversalsIdsResult = new List<string>();
            Assert.Equal(10, pixReversals.Count);
            Assert.True(pixReversals.First().ID != pixReversals.Last().ID);
            foreach (PixReversal transaction in pixReversalsResult)
            {
                Assert.NotNull(transaction.ID);
                pixReversalsIdsResult.Add(transaction.ID);
            }

            pixReversalsIdsExpected.Sort();
            pixReversalsIdsResult.Sort();
            Assert.Equal(pixReversalsIdsExpected, pixReversalsIdsResult);
        }
        
        [Fact]
        public void QueryParams()
        {
            List<PixReversal> pixReversals = PixReversal.Query(
                fields: new List<string> {"amount", "id"},
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: "success",
                tags: new List<string> {"iron", "bank"},
                ids: new List<string> {"1", "2"},
                externalIds: new List<string> {"1", "2"},
                returnIds: new List<string> { "98236508236008632", "2352352352353" }
            ).ToList();
            Assert.True(pixReversals.Count == 0);
        }
        
        [Fact]
        public void PageParams()
        {
            List<PixReversal> page;
            (page, _) = PixReversal.Page(
                cursor: null,
                fields: new List<string> {"amount", "id"},
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: "success",
                tags: new List<string> {"iron", "bank"},
                ids: new List<string> {"1", "2"},
                externalIds: new List<string> {"1", "2"},
                returnIds: new List<string> { "98236508236008632", "2352352352353" }
            );
            Assert.True(page.Count == 0);
        }
        
        public readonly string Content = "";
        public readonly string GoodSignature = "";
        public readonly string BadSignature = "";

        [Fact]
        public void ParseWithRightSignature()
        {
            PixReversal parsedPixReversal = PixReversal.Parse(Content, GoodSignature);
            Assert.NotNull(parsedPixReversal.ID);
            // Assert.NotNull(parsedPixReversal.Log);
            TestUtils.Log(parsedPixReversal);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try {
                PixReversal parsedPixReversal = PixReversal.Parse(Content, BadSignature);
            } catch (StarkInfra.Error.InvalidSignatureError e) {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void ParseWithMalformedSignature()
        {
            try
            {
                PixReversal parsedPixReversal = PixReversal.Parse(Content, BadSignature);
            }
            catch (StarkInfra.Error.InvalidSignatureError e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }
        
        internal static PixReversal Example(bool schedule = true)
        {
            return new PixReversal(
                amount: new Random().Next(1, 5),
                externalId: Convert.ToString(new Random().Next(1, 999999999)),
                endToEndId: TestUtils.GetEndToEndId(),
                reason: "fraud"
            );
        }
    }
}
