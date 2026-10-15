using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    [CollectionDefinition("AiApi")]
    public class AiApiCollection : ICollectionFixture<AiFixture>
    {
    }

    public class AiFixture : IDisposable
    {
        private AiKnowledgeBase knowledgeBase;
        private AiAgent agent;
        private AiChat chat;
        private List<AiMessage> posted;

        public AiKnowledgeBase KnowledgeBase
        {
            get
            {
                TestUser.SetDefaultProject();
                return knowledgeBase ?? (knowledgeBase = AiKnowledgeBase.Create(AiKnowledgeBaseTest.Example()));
            }
        }

        public AiAgent Agent
        {
            get
            {
                TestUser.SetDefaultProject();
                return agent ?? (agent = AiAgent.Create(AiFixture.ExampleAgent(knowledgeBaseIds: new List<string> { KnowledgeBase.ID })));
            }
        }

        public AiChat Chat
        {
            get
            {
                TestUser.SetDefaultProject();
                return chat ?? (chat = AiChat.Create(new AiChat(
                    agentID: Agent.ID,
                    title: Name("sdk-dotnet-chat"),
                    tags: new List<string> { "sdk-dotnet", "test" },
                    context: new Dictionary<string, object> { { "customer_id", "123" } }
                )));
            }
        }

        public List<AiMessage> Posted
        {
            get
            {
                TestUser.SetDefaultProject();
                return posted ?? (posted = AiMessage.Create(
                    new AiMessage(chatID: Chat.ID, text: "Say hello and mention order 123."),
                    expand: new List<string> { "chatName" }
                ));
            }
        }

        public static AiAgent ExampleAgent(List<string> knowledgeBaseIds = null)
        {
            return new AiAgent(
                name: Name("sdk-dotnet-agent"),
                model: "bender-1.0",
                systemPrompt: "Answer in one short sentence.",
                knowledgeBaseIds: knowledgeBaseIds,
                metadataSchema: new Dictionary<string, object> {
                    { "order_id", new Dictionary<string, object> { { "type", "string" }, { "description", "Order the customer mentions" } } }
                }
            );
        }

        public static string Name(string prefix)
        {
            return prefix + "-" + Guid.NewGuid().ToString("N").Substring(0, 12);
        }

        public static AiVoice ReadyVoice()
        {
            return AiVoice.Query().FirstOrDefault(voice => voice.Status == "success");
        }

        public static string SpeechAudio()
        {
            AiSpeech finished = AiSpeech.Query().FirstOrDefault(speech => speech.Status == "success");
            if (finished == null)
            {
                return null;
            }
            return AiSpeech.Get(finished.ID).Audio;
        }

        public void Dispose()
        {
            if (chat == null && agent == null && knowledgeBase == null)
            {
                return;
            }
            TestUser.SetDefaultProject();
            if (chat != null)
            {
                AiChat.Delete(new List<string> { chat.ID });
            }
            if (agent != null)
            {
                AiAgent.Delete(new List<string> { agent.ID });
            }
            if (knowledgeBase != null)
            {
                AiKnowledgeBase.Delete(new List<string> { knowledgeBase.ID });
            }
        }
    }
}
