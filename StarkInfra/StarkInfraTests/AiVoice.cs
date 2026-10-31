using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    public class AiVoiceFixture : IDisposable
    {
        public readonly string Audio;
        public readonly AiVoice Voice;

        public AiVoiceFixture()
        {
            TestUser.SetDefaultProject();
            Audio = AiFixture.SpeechAudio();
            if (Audio == null)
            {
                return;
            }
            Voice = AiVoice.Create(new AiVoice(audio: Audio, name: "sdk-dotnet test"));
        }

        public void Dispose()
        {
            if (Voice == null)
            {
                return;
            }
            AiVoice.Delete(new List<string> { Voice.ID });
        }
    }

    [Collection("AiApi")]
    public class AiVoiceTest : IClassFixture<AiVoiceFixture>
    {
        private readonly AiVoiceFixture fixture;

        public AiVoiceTest(AiVoiceFixture fixture)
        {
            TestUser.SetDefaultProject();
            this.fixture = fixture;
        }

        [Fact]
        public void CreateReturnsAProcessingVoice()
        {
            if (fixture.Voice == null)
            {
                Console.Error.WriteLine("AiVoice.Create was not exercised: the workspace has no finished speech to take an audio from");
                return;
            }
            Assert.NotNull(fixture.Voice.ID);
            Assert.Equal("processing", fixture.Voice.Status);
            Assert.Equal("sdk-dotnet test", fixture.Voice.Name);
            Assert.NotNull(fixture.Voice.Created);
            Assert.Null(fixture.Voice.Audio);
        }

        [Fact]
        public void QueryListsTheCreatedVoiceWithoutTheAudio()
        {
            if (fixture.Voice == null)
            {
                Console.Error.WriteLine("AiVoice.Query was not exercised: the workspace has no finished speech to take an audio from");
                return;
            }
            Dictionary<string, AiVoice> listed = AiVoice.Query().ToDictionary(voice => voice.ID);
            Assert.Contains(fixture.Voice.ID, listed.Keys);
            Assert.Null(listed[fixture.Voice.ID].Audio);
            Assert.NotNull(listed[fixture.Voice.ID].Errors);
        }

        [Fact]
        public void QueryStopsAtTheLimit()
        {
            int found = AiVoice.Query(limit: 1).Count();
            Assert.True(found <= 1);
            if (fixture.Voice != null)
            {
                Assert.Equal(1, found);
            }
        }

        [Fact]
        public void PageFollowsTheCursorUntilItIsNull()
        {
            string cursor = null;
            do
            {
                (List<AiVoice> voices, string next) = AiVoice.Page(cursor: cursor, limit: 50);
                Assert.True(voices.Count <= 50);
                cursor = next;
            } while (cursor != null);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(101)]
        public void PageWithAnInvalidLimitRaisesInputErrors(int limit)
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiVoice.Page(limit: limit));
        }

        [Fact]
        public void QueryWithLimitZeroRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiVoice.Query(limit: 0).ToList());
        }

        [Fact]
        public void DeleteReturnsTheDeletedVoice()
        {
            if (fixture.Audio == null)
            {
                Console.Error.WriteLine("AiVoice.Delete was not exercised: the workspace has no finished speech to take an audio from");
                return;
            }
            AiVoice extra = AiVoice.Create(new AiVoice(audio: fixture.Audio, name: "sdk-dotnet delete test"));
            List<AiVoice> deleted = AiVoice.Delete(new List<string> { extra.ID });
            Assert.Equal(new List<string> { extra.ID }, deleted.Select(voice => voice.ID).ToList());
        }
    }
}
