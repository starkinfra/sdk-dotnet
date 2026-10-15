using System.Collections.Generic;
using System.Linq;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    [Collection("AiApi")]
    public class AiAgentTest
    {
        private readonly AiFixture fixture;

        public AiAgentTest(AiFixture fixture)
        {
            TestUser.SetDefaultProject();
            this.fixture = fixture;
        }

        [Fact]
        public void CreateKeepsTheSchemaKeysAsWritten()
        {
            AiAgent agent = fixture.Agent;
            Assert.NotNull(agent.ID);
            Assert.Equal("bender-1.0", agent.Model);
            Assert.Equal(new List<string> { fixture.KnowledgeBase.ID }, agent.KnowledgeBaseIds);
            Assert.Equal(new List<string> { "order_id" }, agent.MetadataSchema.Keys.ToList());
            Assert.NotNull(agent.Created);
        }

        [Fact]
        public void GetExpandsTheKnowledgeBases()
        {
            AiAgent plain = AiAgent.Get(fixture.Agent.ID);
            Assert.Equal(fixture.Agent.ID, plain.ID);
            Assert.Null(plain.KnowledgeBases);

            AiAgent expanded = AiAgent.Get(fixture.Agent.ID, expand: new List<string> { "knowledgeBases" });
            Assert.Equal(new List<string> { fixture.KnowledgeBase.ID }, expanded.KnowledgeBases.Select(entity => entity.ID).ToList());
            Assert.IsType<AiKnowledgeBase>(expanded.KnowledgeBases[0]);
        }

        [Fact]
        public void QueryWithLimitStopsAtTheLimit()
        {
            Assert.NotNull(fixture.Agent.ID);
            Assert.Single(AiAgent.Query(limit: 1).ToList());
        }

        [Fact]
        public void PageFollowsTheCursorUntilItIsNull()
        {
            string id = fixture.Agent.ID;
            List<AiAgent> found = new List<AiAgent>();
            string cursor = null;
            do
            {
                (List<AiAgent> agents, string next) = AiAgent.Page(cursor: cursor, limit: 1);
                Assert.True(agents.Count <= 1);
                found.AddRange(agents);
                cursor = next;
            } while (cursor != null);

            Assert.Contains(id, found.Select(entity => entity.ID));
            Assert.Equal(found.Count, found.Select(entity => entity.ID).Distinct().Count());
        }

        [Fact]
        public void UpdateKeepsWhatItWasNotAskedToChange()
        {
            AiAgent agent = fixture.Agent;
            try
            {
                AiAgent renamed = AiAgent.Update(id: agent.ID, name: "renamed-by-sdk");
                Assert.Equal("renamed-by-sdk", renamed.Name);
                Assert.Equal(new List<string> { fixture.KnowledgeBase.ID }, renamed.KnowledgeBaseIds);
                Assert.Equal(new List<string> { "order_id" }, renamed.MetadataSchema.Keys.ToList());
            }
            finally
            {
                AiAgent.Update(id: agent.ID, name: agent.Name);
            }
        }

        [Fact]
        public void UpdateWithAnEmptyListClearsTheKnowledgeBases()
        {
            AiAgent agent = AiAgent.Create(AiFixture.ExampleAgent(knowledgeBaseIds: new List<string> { fixture.KnowledgeBase.ID }));
            try
            {
                AiAgent cleared = AiAgent.Update(id: agent.ID, knowledgeBaseIds: new List<string>());
                Assert.Empty(cleared.KnowledgeBaseIds);
            }
            finally
            {
                AiAgent.Delete(new List<string> { agent.ID });
            }
        }

        [Fact]
        public void UpdateWithEmptyValuesClearsThePromptAndTheVoice()
        {
            AiAgent agent = AiAgent.Create(AiFixture.ExampleAgent());
            try
            {
                AiAgent cleared = AiAgent.Update(id: agent.ID, systemPrompt: "", voiceID: "");
                Assert.Equal("", cleared.SystemPrompt);
                Assert.Equal("", cleared.VoiceID);
            }
            finally
            {
                AiAgent.Delete(new List<string> { agent.ID });
            }
        }

        [Fact]
        public void DeleteReturnsTheDeletedAgents()
        {
            AiAgent agent = AiAgent.Create(AiFixture.ExampleAgent());
            List<AiAgent> deleted = AiAgent.Delete(new List<string> { agent.ID });
            Assert.Equal(new List<string> { agent.ID }, deleted.Select(entity => entity.ID).ToList());
        }

        [Fact]
        public void CreateWithInvalidModelRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiAgent.Create(new AiAgent(name: "invalid", model: "gpt")));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(101)]
        public void PageWithAnInvalidLimitRaisesInputErrors(int limit)
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiAgent.Page(limit: limit));
        }

        [Fact]
        public void QueryWithLimitZeroRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiAgent.Query(limit: 0).ToList());
        }

        [Fact]
        public void GetUnknownIdRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiAgent.Get("0000000000000000"));
        }
    }
}
