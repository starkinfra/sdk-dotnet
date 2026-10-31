using System.Collections.Generic;
using System.Linq;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    [Collection("AiApi")]
    public class AiChatTest
    {
        private readonly AiFixture fixture;

        public AiChatTest(AiFixture fixture)
        {
            TestUser.SetDefaultProject();
            this.fixture = fixture;
        }

        [Fact]
        public void CreateReturnsTheChatWithTagsAndContext()
        {
            AiChat chat = fixture.Chat;
            Assert.NotNull(chat.ID);
            Assert.Equal(fixture.Agent.ID, chat.AgentID);
            Assert.StartsWith("sdk-dotnet-chat-", chat.Title);
            Assert.Equal(new List<string> { "sdk-dotnet", "test" }, chat.Tags);
            Assert.Equal("123", chat.Context["customer_id"]);
            Assert.Null(chat.AgentName);
        }

        [Fact]
        public void GetExpandsTheAgentName()
        {
            AiChat fetched = AiChat.Get(fixture.Chat.ID, expand: new List<string> { "agentName" });
            Assert.Equal(fixture.Chat.ID, fetched.ID);
            Assert.Equal(fixture.Agent.Name, fetched.AgentName);
        }

        [Fact]
        public void QueryFiltersByTagsAndStopsAtTheLimit()
        {
            string id = fixture.Chat.ID;
            List<AiChat> tagged = AiChat.Query(tags: new List<string> { "sdk-dotnet" }).ToList();
            Assert.Contains(id, tagged.Select(entity => entity.ID));
            Assert.Single(AiChat.Query(limit: 1).ToList());
        }

        [Fact]
        public void PageFollowsTheCursorUntilItIsNull()
        {
            string id = fixture.Chat.ID;
            List<AiChat> found = new List<AiChat>();
            string cursor = null;
            do
            {
                (List<AiChat> chats, string next) = AiChat.Page(cursor: cursor, limit: 1);
                Assert.True(chats.Count <= 1);
                found.AddRange(chats);
                cursor = next;
            } while (cursor != null);

            Assert.Contains(id, found.Select(entity => entity.ID));
            Assert.Equal(found.Count, found.Select(entity => entity.ID).Distinct().Count());
        }

        [Fact]
        public void UpdateReplacesTagsAndContextAndKeepsTheRest()
        {
            AiChat chat = AiChat.Create(new AiChat(agentID: fixture.Agent.ID, title: AiFixture.Name("sdk-dotnet-chat")));
            try
            {
                Assert.Empty(chat.Tags);
                Assert.Empty(chat.Context);

                AiChat tagged = AiChat.Update(
                    id: chat.ID,
                    tags: new List<string> { "customer-123", "whatsapp" },
                    context: new Dictionary<string, object> { { "customer_id", "123" } }
                );
                Assert.Equal(chat.Title, tagged.Title);
                Assert.Equal(new List<string> { "customer-123", "whatsapp" }, tagged.Tags);
                Assert.Equal("123", tagged.Context["customer_id"]);

                AiChat renamed = AiChat.Update(id: chat.ID, title: "renamed-by-sdk");
                Assert.Equal("renamed-by-sdk", renamed.Title);
                Assert.Equal(new List<string> { "customer-123", "whatsapp" }, renamed.Tags);
                Assert.Equal("123", renamed.Context["customer_id"]);

                AiChat cleared = AiChat.Update(id: chat.ID, tags: new List<string>(), context: new Dictionary<string, object>());
                Assert.Empty(cleared.Tags);
                Assert.Empty(cleared.Context);
            }
            finally
            {
                AiChat.Delete(new List<string> { chat.ID });
            }
        }

        [Fact]
        public void CreateWithUnknownAgentRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiChat.Create(new AiChat(agentID: "0000000000000000")));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(101)]
        public void PageWithAnInvalidLimitRaisesInputErrors(int limit)
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiChat.Page(limit: limit));
        }

        [Fact]
        public void GetUnknownIdRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiChat.Get("0000000000000000"));
        }
    }
}
