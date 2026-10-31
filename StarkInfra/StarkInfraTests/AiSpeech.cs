using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    public class AiSpeechFixture
    {
        public readonly AiSpeech Speech;

        public AiSpeechFixture()
        {
            TestUser.SetDefaultProject();
            AiVoice ready = AiFixture.ReadyVoice();
            if (ready == null)
            {
                return;
            }
            Speech = AiSpeech.Create(new AiSpeech(voiceID: ready.ID, text: "Short test."));
        }
    }

    [Collection("AiApi")]
    public class AiSpeechTest : IClassFixture<AiSpeechFixture>
    {
        private readonly AiSpeech speech;

        public AiSpeechTest(AiSpeechFixture fixture)
        {
            TestUser.SetDefaultProject();
            speech = fixture.Speech;
        }

        [Fact]
        public void CreateReturnsTheSynthesizedAudio()
        {
            if (speech == null)
            {
                Console.Error.WriteLine("AiSpeech.Create was not exercised: the workspace has no ready voice");
                return;
            }
            Assert.NotNull(speech.ID);
            Assert.Equal("success", speech.Status);
            Assert.Equal("Short test.", speech.Text);
            Assert.False(string.IsNullOrEmpty(speech.Audio));
            Assert.NotNull(speech.Created);
        }

        [Fact]
        public void GetReturnsTheAudioAndExpandsTheVoiceName()
        {
            if (speech == null)
            {
                Console.Error.WriteLine("AiSpeech.Get was not exercised: the workspace has no ready voice");
                return;
            }
            AiSpeech fetched = AiSpeech.Get(speech.ID);
            Assert.Equal(speech.ID, fetched.ID);
            Assert.False(string.IsNullOrEmpty(fetched.Audio));

            AiSpeech expanded = AiSpeech.Get(speech.ID, expand: new List<string> { "voiceName" });
            Assert.False(string.IsNullOrEmpty(expanded.VoiceName));
        }

        [Fact]
        public void QueryLeavesTheAudioOutAndStopsAtTheLimit()
        {
            foreach (AiSpeech listed in AiSpeech.Query())
            {
                Assert.NotNull(listed.ID);
                Assert.Null(listed.Audio);
                Assert.NotNull(listed.Created);
            }
            Assert.True(AiSpeech.Query(limit: 1).Count() <= 1);
        }

        [Fact]
        public void PageFollowsTheCursorUntilItIsNull()
        {
            string cursor = null;
            do
            {
                (List<AiSpeech> speeches, string next) = AiSpeech.Page(cursor: cursor, limit: 50);
                Assert.True(speeches.Count <= 50);
                cursor = next;
            } while (cursor != null);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(101)]
        public void PageWithAnInvalidLimitRaisesInputErrors(int limit)
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiSpeech.Page(limit: limit));
        }

        [Fact]
        public void QueryWithLimitZeroRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiSpeech.Query(limit: 0).ToList());
        }

        [Fact]
        public void GetUnknownIdRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiSpeech.Get("0000000000000000"));
        }
    }
}
