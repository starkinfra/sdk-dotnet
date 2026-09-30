using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    public class AiKnowledgeBaseFixture : IDisposable
    {
        public readonly AiKnowledgeBase KnowledgeBase;

        public AiKnowledgeBaseFixture()
        {
            TestUser.SetDefaultProject();
            KnowledgeBase = AiKnowledgeBase.Create(AiKnowledgeBaseTest.Example());
        }

        public void Dispose()
        {
            AiKnowledgeBase.Delete(new List<string> { KnowledgeBase.ID });
        }
    }

    [Collection("AiApi")]
    public class AiKnowledgeBaseTest : IClassFixture<AiKnowledgeBaseFixture>
    {
        private readonly AiKnowledgeBase knowledgeBase;

        public AiKnowledgeBaseTest(AiKnowledgeBaseFixture fixture)
        {
            TestUser.SetDefaultProject();
            knowledgeBase = fixture.KnowledgeBase;
        }

        [Fact]
        public void CreateReturnsProcessingKnowledgeBase()
        {
            Assert.NotNull(knowledgeBase.ID);
            Assert.Equal("processing", knowledgeBase.Status);
            Assert.Equal("https://docs.starkinfra.com", knowledgeBase.RootUrl);
            Assert.False(knowledgeBase.IsRecursive);
            Assert.Equal(new List<string> { "sdk-dotnet", "test" }, knowledgeBase.Tags);
            Assert.NotNull(knowledgeBase.Created);
        }

        [Fact]
        public void Get()
        {
            AiKnowledgeBase fetched = AiKnowledgeBase.Get(knowledgeBase.ID);
            Assert.Equal(knowledgeBase.ID, fetched.ID);
            Assert.Equal(knowledgeBase.Name, fetched.Name);
        }

        [Fact]
        public void HostsReturnsJsonObject()
        {
            Assert.NotNull(AiKnowledgeBase.Hosts(knowledgeBase.ID));
        }

        [Fact]
        public void QueryFiltersByIds()
        {
            List<AiKnowledgeBase> found = AiKnowledgeBase.Query(ids: new List<string> { knowledgeBase.ID }).ToList();
            Assert.Equal(new List<string> { knowledgeBase.ID }, found.Select(entity => entity.ID).ToList());
        }

        [Fact]
        public void QueryFiltersByNameAndStatus()
        {
            AiKnowledgeBase current = AiKnowledgeBase.Get(knowledgeBase.ID);
            List<AiKnowledgeBase> found = AiKnowledgeBase.Query(name: current.Name, status: current.Status).ToList();
            Assert.Contains(knowledgeBase.ID, found.Select(entity => entity.ID));
        }

        [Fact]
        public void QueryWithLimitStopsAtTheLimit()
        {
            Assert.Single(AiKnowledgeBase.Query(limit: 1).ToList());
        }

        [Fact]
        public void PageFollowsTheCursorUntilItIsNull()
        {
            List<AiKnowledgeBase> found = new List<AiKnowledgeBase>();
            string cursor = null;
            do
            {
                (List<AiKnowledgeBase> bases, string next) = AiKnowledgeBase.Page(cursor: cursor, limit: 1);
                Assert.True(bases.Count <= 1);
                found.AddRange(bases);
                cursor = next;
            } while (cursor != null);

            Assert.Contains(knowledgeBase.ID, found.Select(entity => entity.ID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(101)]
        public void PageWithAnInvalidLimitRaisesInputErrors(int limit)
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiKnowledgeBase.Page(limit: limit));
        }

        [Fact]
        public void QueryWithLimitZeroRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiKnowledgeBase.Query(limit: 0).ToList());
        }

        [Fact]
        public void QueryWithoutMatchIsEmpty()
        {
            List<AiKnowledgeBase> found = AiKnowledgeBase.Query(name: "no-knowledge-base-has-this-name").ToList();
            Assert.Empty(found);
        }

        [Fact]
        public void UpdateChangesNameAndTagsOnly()
        {
            try
            {
                AiKnowledgeBase updated = AiKnowledgeBase.Update(
                    id: knowledgeBase.ID,
                    name: "renamed-by-sdk",
                    tags: new List<string> { "renamed" }
                );
                Assert.Equal("renamed-by-sdk", updated.Name);
                Assert.Equal(new List<string> { "renamed" }, updated.Tags);
                Assert.Equal(knowledgeBase.RootUrl, updated.RootUrl);
            }
            finally
            {
                AiKnowledgeBase.Update(id: knowledgeBase.ID, name: knowledgeBase.Name, tags: knowledgeBase.Tags);
            }
        }

        [Fact]
        public void CreateWithInvalidRootUrlRaisesInputErrors()
        {
            AiKnowledgeBase invalid = new AiKnowledgeBase(name: "invalid", rootUrl: "not-a-url");
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiKnowledgeBase.Create(invalid));
        }

        [Fact]
        public void GetUnknownIdRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiKnowledgeBase.Get("0000000000000000"));
        }

        public static AiKnowledgeBase Example()
        {
            return new AiKnowledgeBase(
                name: "sdk-dotnet-" + Guid.NewGuid().ToString("N").Substring(0, 12),
                rootUrl: "https://docs.starkinfra.com",
                isRecursive: false,
                tags: new List<string> { "sdk-dotnet", "test" }
            );
        }
    }
}
