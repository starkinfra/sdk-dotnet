using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    [Collection("AiApi")]
    public class AiMessageTest
    {
        private readonly AiFixture fixture;

        public AiMessageTest(AiFixture fixture)
        {
            TestUser.SetDefaultProject();
            this.fixture = fixture;
        }

        [Fact]
        public void CreateReturnsTheUserMessageAndTheAnswer()
        {
            List<AiMessage> posted = fixture.Posted;
            Assert.Equal(new List<string> { "user", "system" }, posted.Select(message => message.Sender).ToList());
            foreach (AiMessage message in posted)
            {
                Assert.Equal(fixture.Chat.ID, message.ChatID);
                Assert.NotNull(message.Created);
                Assert.False(string.IsNullOrEmpty(message.ChatName));
            }
        }

        [Fact]
        public void TheAnswerCarriesAMetadataObject()
        {
            Assert.NotNull(fixture.Posted[1].Metadata);
        }

        [Fact]
        public void QueryReturnsTheWholeHistory()
        {
            List<string> expected = fixture.Posted.Select(message => message.ID).OrderBy(id => id, StringComparer.Ordinal).ToList();
            List<string> found = AiMessage.Query(chatID: fixture.Chat.ID).Select(message => message.ID).OrderBy(id => id, StringComparer.Ordinal).ToList();
            Assert.Equal(expected, found);
        }

        [Fact]
        public void QueryWithLimitStopsAtTheLimit()
        {
            Assert.Equal(2, fixture.Posted.Count);
            Assert.Single(AiMessage.Query(chatID: fixture.Chat.ID, limit: 1).ToList());
        }

        [Fact]
        public void QueryWithoutChatIDListsTheMessagesOfEveryChat()
        {
            List<string> ofTheChat = fixture.Posted.Select(message => message.ID).ToList();
            List<AiMessage> found = AiMessage.Query(limit: 10).ToList();
            Assert.NotEmpty(found);
            Assert.All(found, message => Assert.False(string.IsNullOrEmpty(message.ID)));
            Assert.All(found, message => Assert.False(string.IsNullOrEmpty(message.ChatID)));

            List<string> scoped = AiMessage.Query(chatID: fixture.Chat.ID).Select(message => message.ID).ToList();
            Assert.All(ofTheChat, id => Assert.Contains(id, scoped));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void QueryWithAnInvalidLimitRaisesInputErrors(int limit)
        {
            string chatID = fixture.Chat.ID;
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiMessage.Query(chatID: chatID, limit: limit).ToList());
        }

        [Fact]
        public void PageWithALimitAboveTheMaximumRaisesInputErrors()
        {
            string chatID = fixture.Chat.ID;
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiMessage.Page(chatID: chatID, limit: 101));
        }

        [Fact]
        public void QueryWithAnUnknownChatIDRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiMessage.Query(chatID: "0000000000000000").ToList());
        }

        [Fact]
        public void PageReturnsACursorThatLeadsToADifferentSecondPage()
        {
            string chatID = fixture.Chat.ID;
            Assert.Equal(2, fixture.Posted.Count);
            (List<AiMessage> first, string cursor) = AiMessage.Page(chatID: chatID, limit: 1);
            Assert.Single(first);
            Assert.NotNull(cursor);
            (List<AiMessage> second, string _) = AiMessage.Page(chatID: chatID, cursor: cursor, limit: 1);
            Assert.Single(second);
            Assert.NotEqual(first[0].ID, second[0].ID);
        }

        [Fact]
        public void CreateInAnUnknownChatRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiMessage.Create(new AiMessage(chatID: "0000000000000000", text: "hi")));
        }
    }
}
