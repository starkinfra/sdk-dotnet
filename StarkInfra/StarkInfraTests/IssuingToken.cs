using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingTokenTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 1).ToList();
            IssuingToken token = tokens.First();
            IssuingToken getToken = IssuingToken.Get(token.ID);
            Assert.NotNull(getToken);
            Assert.Equal(getToken.ID, token.ID);
            TestUtils.Log(getToken);
        }

        [Fact]
        public void Query()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 101).ToList();
            Assert.True(tokens.Count <= 101);
            foreach (IssuingToken token in tokens)
            {
                TestUtils.Log(token);
                Assert.NotNull(token.ID);
            }
        }

        [Fact]
        public void QueryIds()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 10).ToList();
            List<string> tokensIdsExpected = new List<string>();
            foreach (IssuingToken token in tokens)
            {
                Assert.NotNull(token.ID);
                tokensIdsExpected.Add(token.ID);
            }

            List<IssuingToken> tokensResult = IssuingToken.Query(limit: 10, ids: tokensIdsExpected).ToList();
            List<string> tokensIdsResult = new List<string>();
            foreach (IssuingToken token in tokensResult)
            {
                Assert.NotNull(token.ID);
                tokensIdsResult.Add(token.ID);
            }

            tokensIdsExpected.Sort();
            tokensIdsResult.Sort();
            Assert.Equal(tokensIdsExpected, tokensIdsResult);
        }

        [Fact]
        public void QueryParams()
        {
            List<IssuingToken> tokens = IssuingToken.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "active" },
                cardIds: new List<string> { "1", "2" },
                tags: new List<string> { "travel", "food" },
                ids: new List<string> { "1", "2" },
                externalIds: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(tokens.Count == 0);
        }

        [Fact]
        public void Page()
        {
            List<IssuingToken> tokens = new List<IssuingToken>();
            List<IssuingToken> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingToken.Page(limit: 5, cursor: cursor);
                foreach (IssuingToken entity in page)
                {
                    Assert.DoesNotContain(entity, tokens);
                    tokens.Add(entity);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(tokens.Count <= 10);
        }

        [Fact]
        public void PageParams()
        {
            List<IssuingToken> page;
            string cursor = null;
            (page, cursor) = IssuingToken.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "active" },
                cardIds: new List<string> { "1", "2" },
                tags: new List<string> { "travel", "food" },
                ids: new List<string> { "1", "2" },
                externalIds: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }

        [Fact]
        public void GetAndUpdate()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 1).ToList();
            IssuingToken token = tokens.First();
            Assert.NotNull(token.ID);
            IssuingToken updatedToken = IssuingToken.Update(id: token.ID, status: "blocked");
            Assert.Equal("blocked", updatedToken.Status);
            TestUtils.Log(updatedToken);
        }

        [Fact]
        public void GetAndCancel()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 1).ToList();
            IssuingToken token = tokens.First();
            Assert.NotNull(token.ID);
            IssuingToken getToken = IssuingToken.Get(token.ID);
            Assert.Equal(getToken.ID, token.ID);
            IssuingToken canceledToken = IssuingToken.Cancel(id: token.ID);
            Assert.Equal(canceledToken.ID, token.ID);
            TestUtils.Log(canceledToken);
        }

        [Fact]
        public void QueryExposesUrl()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 1).ToList();
            foreach (IssuingToken token in tokens)
            {
                Assert.NotNull(token.ID);
                string url = token.Url;
                TestUtils.Log(url);
            }
        }
    }
}
