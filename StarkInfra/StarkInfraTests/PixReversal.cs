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
            List<PixReversal> reversals = PixReversal.Create(new List<PixReversal>() { Example() });
            PixReversal reversal = reversals.First();
            Assert.NotNull(reversals.First().ID);
            PixReversal getPixReversal = PixReversal.Get(id: reversal.ID);
            Assert.Equal(getPixReversal.ID, reversal.ID);
            TestUtils.Log(reversal);
        }

        [Fact]
        public void Query()
        {
            List<PixReversal> reversals = PixReversal.Query(limit: 101, status: new List<string> { "success" }).ToList();
            Assert.True(reversals.Count <= 101);
            Assert.True(reversals.First().ID != reversals.Last().ID);
            foreach (PixReversal reversal in reversals)
            {
                TestUtils.Log(reversal);
                Assert.NotNull(reversal.ID);
                Assert.Equal("success", reversal.Status);
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
            List<PixReversal> reversals = PixReversal.Query(limit: 10).ToList();
            List<String> reversalsIdsExpected = new List<string>();
            Assert.Equal(10, reversals.Count);
            Assert.True(reversals.First().ID != reversals.Last().ID);
            foreach (PixReversal transaction in reversals)
            {
                Assert.NotNull(transaction.ID);
                reversalsIdsExpected.Add(transaction.ID);
            }

            List<PixReversal> reversalsResult = PixReversal.Query(limit:10, ids: reversalsIdsExpected).ToList();
            List<String> reversalsIdsResult = new List<string>();
            Assert.Equal(10, reversals.Count);
            Assert.True(reversals.First().ID != reversals.Last().ID);
            foreach (PixReversal transaction in reversalsResult)
            {
                Assert.NotNull(transaction.ID);
                reversalsIdsResult.Add(transaction.ID);
            }

            reversalsIdsExpected.Sort();
            reversalsIdsResult.Sort();
            Assert.Equal(reversalsIdsExpected, reversalsIdsResult);
        }
        
        [Fact]
        public void QueryParams()
        {
            List<PixReversal> reversals = PixReversal.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "success" },
                tags: new List<string> {"iron", "bank"},
                ids: new List<string> {"1", "2"},
                externalIds: new List<string> {"1", "2"},
                returnIds: new List<string> { "98236508236008632", "2352352352353" }
            ).ToList();
            Assert.True(reversals.Count == 0);
        }
        
        [Fact]
        public void PageParams()
        {
            List<PixReversal> page;
            (page, _) = PixReversal.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "success" },
                tags: new List<string> {"iron", "bank"},
                ids: new List<string> {"1", "2"},
                externalIds: new List<string> {"1", "2"},
                returnIds: new List<string> { "98236508236008632", "2352352352353" }
            );
            Assert.True(page.Count == 0);
        }
        
        public readonly string Content = "{\"status\": \"processing\", \"returnId\": \"D34052649202212081809BSc6b12oLsF\", \"amount\": 10, \"updated\": \"2022-12-08T18:09:38.344943+00:00\", \"tags\": [\"lannister\", \"chargeback\"], \"reason\": \"fraud\", \"created\": \"2022-12-08T18:09:38.344936+00:00\", \"flow\": \"in\", \"id\": \"5685338043318272\", \"endToEndId\": \"E35547753202201201450oo8srGorhf1\"}";
        public readonly string GoodSignature = "MEQCIFiONlW6TV4+U3XWfACP2IttNrxPi8E++FCuXEsf1NjuAiAD2wktgT1tTzxcz+MMJWDPuw3PZjp2kJG+Wf9yF1lcGg==";
        public readonly string BadSignature = "MEQCIFiONlW6TV4+U3XWdACP2IttNrxPi8E++FCuXEsf1NjuAiAD2wktgT1tTzxcz+MMJWDPuw3PZjp2kJG+Wf9yF1lcGg==";

        [Fact]
        public void ParseWithRightSignature()
        {
            PixReversal parsedPixReversal = PixReversal.Parse(Content, GoodSignature);
            Assert.NotNull(parsedPixReversal.ID);
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

        [Fact]
        public void SendResponse()
        {
            string response = PixReversal.Response(status: "accepted");
            TestUtils.Log(response);
        }

        internal static PixReversal Example(bool schedule = true)
        {
            return new PixReversal(
                amount: new Random().Next(1, 5),
                externalID: Convert.ToString(new Random().Next(1, 999999999)),
                endToEndID: TestUtils.GetEndToEndID(),
                reason: "fraud"
            );
        }
    }
}
