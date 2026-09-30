using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    public class AiTranscriptFixture
    {
        public readonly AiTranscript Transcript;

        public AiTranscriptFixture()
        {
            TestUser.SetDefaultProject();
            string audio = AiFixture.SpeechAudio();
            if (audio == null)
            {
                return;
            }
            Transcript = AiTranscript.Create(new AiTranscript(audio: audio));
        }
    }

    [Collection("AiApi")]
    public class AiTranscriptTest : IClassFixture<AiTranscriptFixture>
    {
        private readonly AiTranscript transcript;

        public AiTranscriptTest(AiTranscriptFixture fixture)
        {
            TestUser.SetDefaultProject();
            transcript = fixture.Transcript;
        }

        [Fact]
        public void CreateReturnsTheText()
        {
            if (transcript == null)
            {
                Console.Error.WriteLine("AiTranscript.Create was not exercised: the workspace has no finished speech to take an audio from");
                return;
            }
            Assert.NotNull(transcript.ID);
            Assert.Equal("success", transcript.Status);
            Assert.NotNull(transcript.Text);
            Assert.NotNull(transcript.Created);
        }

        [Fact]
        public void QueryListsTheCreatedTranscriptWithoutTheAudio()
        {
            if (transcript == null)
            {
                Console.Error.WriteLine("AiTranscript.Query was not exercised: the workspace has no finished speech to take an audio from");
                return;
            }
            Dictionary<string, AiTranscript> listed = AiTranscript.Query().ToDictionary(entity => entity.ID);
            Assert.Contains(transcript.ID, listed.Keys);
            Assert.Null(listed[transcript.ID].Audio);
        }

        [Fact]
        public void QueryStopsAtTheLimit()
        {
            int found = AiTranscript.Query(limit: 1).Count();
            Assert.True(found <= 1);
            if (transcript != null)
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
                (List<AiTranscript> transcripts, string next) = AiTranscript.Page(cursor: cursor, limit: 50);
                Assert.True(transcripts.Count <= 50);
                cursor = next;
            } while (cursor != null);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(101)]
        public void PageWithAnInvalidLimitRaisesInputErrors(int limit)
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiTranscript.Page(limit: limit));
        }

        [Fact]
        public void QueryWithLimitZeroRaisesInputErrors()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() => AiTranscript.Query(limit: 0).ToList());
        }
    }
}
