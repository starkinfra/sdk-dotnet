using Xunit;
using StarkInfra;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IndividualAccountAttachmentTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        // [M1] create accepts a List<IndividualAccountAttachment> and returns the same shape
        // with server-assigned id/status/created populated.
        // [M2] the constructor encodes content + contentType into a data: URL client-side.
        // [M4] get(id) returns a single IndividualAccountAttachment by id.
        // [M8] exposed under IndividualAccountAttachment (not accountRequestAttachment).
        // Self-contained: creates a FRESH parent request, then attaches to THAT.
        [Fact]
        public void CreateGet()
        {
            IndividualAccountRequest parent = CreateParent();

            List<IndividualAccountAttachment> attachments = IndividualAccountAttachment.Create(
                new List<IndividualAccountAttachment>() { Example(parent.ID) });
            IndividualAccountAttachment attachment = attachments.First();
            Assert.NotNull(attachment.ID);

            IndividualAccountAttachment getAttachment = IndividualAccountAttachment.Get(id: attachment.ID);
            Assert.Equal(getAttachment.ID, attachment.ID);
            TestUtils.Log(getAttachment);
        }

        // [M3] contentType is input-only: it is never deserialized back onto a response object.
        // Passing contentType to a deserialized response object is a programming error, so a
        // fetched/created attachment must not expose a populated ContentType.
        [Fact]
        public void ContentTypeIsInputOnly()
        {
            IndividualAccountRequest parent = CreateParent();

            List<IndividualAccountAttachment> attachments = IndividualAccountAttachment.Create(
                new List<IndividualAccountAttachment>() { Example(parent.ID) });
            IndividualAccountAttachment attachment = attachments.First();
            Assert.NotNull(attachment.ID);
            // The response object carries no standalone contentType wire field.
            Assert.Null(attachment.ContentType);
        }

        // [M5] query returns an iterable of IndividualAccountAttachment accepting
        // limit, after, before, status, tags, ids.
        [Fact]
        public void Query()
        {
            List<IndividualAccountAttachment> attachments =
                IndividualAccountAttachment.Query(limit: 101).ToList();
            Assert.True(attachments.Count <= 101);
            foreach (IndividualAccountAttachment attachment in attachments)
            {
                TestUtils.Log(attachment);
                Assert.NotNull(attachment.ID);
            }
        }

        // [M5] query honours the ids filter as a round-trip.
        [Fact]
        public void QueryIds()
        {
            List<IndividualAccountAttachment> attachments =
                IndividualAccountAttachment.Query(limit: 10).ToList();
            List<string> idsExpected = new List<string>();
            foreach (IndividualAccountAttachment attachment in attachments)
            {
                Assert.NotNull(attachment.ID);
                idsExpected.Add(attachment.ID);
            }

            List<IndividualAccountAttachment> result =
                IndividualAccountAttachment.Query(limit: 10, ids: idsExpected).ToList();
            List<string> idsResult = new List<string>();
            foreach (IndividualAccountAttachment attachment in result)
            {
                Assert.NotNull(attachment.ID);
                idsResult.Add(attachment.ID);
            }

            idsExpected.Sort();
            idsResult.Sort();
            Assert.Equal(idsExpected, idsResult);
        }

        // [M5] every documented query filter param serializes without throwing.
        [Fact]
        public void QueryParams()
        {
            List<IndividualAccountAttachment> attachments = IndividualAccountAttachment.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "created" },
                tags: new List<string> { "employees" },
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(attachments.Count == 0);
        }

        // [M6] page returns (items, cursor) and accepts the same params as query plus cursor.
        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IndividualAccountAttachment> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IndividualAccountAttachment.Page(limit: 5, cursor: cursor);
                foreach (IndividualAccountAttachment entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count <= 10);
        }

        // [M6] every documented page filter param serializes without throwing.
        [Fact]
        public void PageParams()
        {
            List<IndividualAccountAttachment> page;
            string cursor = null;
            (page, cursor) = IndividualAccountAttachment.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "created" },
                tags: new List<string> { "employees" },
                ids: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }

        // [M7] cancel(id) maps to DELETE and returns status = deleted. cancel is IDEMPOTENT:
        // a second cancel on an already-deleted attachment succeeds with no error.
        // Self-contained: fresh parent + fresh attachment, then cancel THAT (twice).
        [Fact]
        public void GetAndCancel()
        {
            IndividualAccountRequest parent = CreateParent();
            List<IndividualAccountAttachment> attachments = IndividualAccountAttachment.Create(
                new List<IndividualAccountAttachment>() { Example(parent.ID) });
            IndividualAccountAttachment attachment = attachments.First();
            Assert.NotNull(attachment.ID);

            IndividualAccountAttachment canceled = IndividualAccountAttachment.Cancel(id: attachment.ID);
            Assert.Equal(canceled.ID, attachment.ID);
            Assert.Equal("deleted", canceled.Status);
            TestUtils.Log(canceled);

            // Idempotent: a second cancel succeeds without raising.
            IndividualAccountAttachment canceledAgain = IndividualAccountAttachment.Cancel(id: attachment.ID);
            Assert.Equal(canceledAgain.ID, attachment.ID);
        }

        // ===== Error-path tests ([M13] — assert the mapped exception TYPE is raised, never a code string) =====

        // [E] / [M11] type not in the documented enum -> InputErrors. The trigger uses an out-of-enum
        // value ("not-a-real-type"); note "selfie" is also NOT a valid type for this resource.
        [Fact]
        public void CreateWithInvalidTypeRaises()
        {
            IndividualAccountRequest parent = CreateParent();
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountAttachment.Create(new List<IndividualAccountAttachment>() {
                    new IndividualAccountAttachment(
                        type: "not-a-real-type",
                        content: SampleImageBytes(),
                        contentType: "image/png",
                        accountRequestID: parent.ID
                    )
                })
            );
        }

        // [E] content missing or empty -> InputErrors.
        [Fact]
        public void CreateWithEmptyContentRaises()
        {
            IndividualAccountRequest parent = CreateParent();
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountAttachment.Create(new List<IndividualAccountAttachment>() {
                    new IndividualAccountAttachment(
                        type: "identity-front",
                        content: Encoding.UTF8.GetBytes(""),
                        contentType: "image/png",
                        accountRequestID: parent.ID
                    )
                })
            );
        }

        // [E] contentType missing when content provided -> InputErrors.
        // [M2] without a MIME type the SDK cannot build the data: URL, but it must still produce a
        // serializable payload (not crash before the API call); the API then rejects it.
        [Fact]
        public void CreateWithMissingContentTypeRaises()
        {
            IndividualAccountRequest parent = CreateParent();
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountAttachment.Create(new List<IndividualAccountAttachment>() {
                    new IndividualAccountAttachment(
                        type: "identity-front",
                        content: SampleImageBytes(),
                        contentType: null,
                        accountRequestID: parent.ID
                    )
                })
            );
        }

        // [E] accountRequestId not found -> InputErrors.
        [Fact]
        public void CreateWithUnknownAccountRequestIdRaises()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountAttachment.Create(new List<IndividualAccountAttachment>() {
                    new IndividualAccountAttachment(
                        type: "identity-front",
                        content: SampleImageBytes(),
                        contentType: "image/png",
                        accountRequestID: "0"
                    )
                })
            );
        }

        // [E] unknown id (get) -> InputErrors.
        [Fact]
        public void GetUnknownIdRaises()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountAttachment.Get(id: "0")
            );
        }

        // Creates a FRESH parent IndividualAccountRequest so each attachment test owns its own
        // parent (avoids the "attachment already sent" sandbox condition).
        internal static IndividualAccountRequest CreateParent()
        {
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Create(
                new List<IndividualAccountRequest>() { IndividualAccountRequestTest.Example() });
            return requests.First();
        }

        // Valid-for-create attachment bound to the given parent id.
        internal static IndividualAccountAttachment Example(string accountRequestID)
        {
            return new IndividualAccountAttachment(
                type: "identity-front",
                content: SampleImageBytes(),
                contentType: "image/png",
                accountRequestID: accountRequestID,
                tags: new List<string> { "employees" }
            );
        }

        // Real PNG bytes; the API validates binary image content (not just the MIME type).
        // Read from the source tree via relative path, matching IndividualDocument.cs.
        internal static byte[] SampleImageBytes()
        {
            return System.IO.File.ReadAllBytes("../../../identity/identity-front-face.png");
        }
    }
}
