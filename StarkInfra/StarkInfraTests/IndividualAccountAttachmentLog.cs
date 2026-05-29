using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IndividualAccountAttachmentLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        // [M9] Log is read-only, exposed under <resource>.Log, provides Query + Get.
        // The Log's Attachment field is the parent type, not a string id.
        [Fact]
        public void QueryAndGet()
        {
            List<IndividualAccountAttachment.Log> logs =
                IndividualAccountAttachment.Log.Query(limit: 10).ToList();
            Assert.True(logs.Count <= 10);
            foreach (IndividualAccountAttachment.Log log in logs)
            {
                TestUtils.Log(log);
                Assert.NotNull(log.ID);
                Assert.NotNull(log.Attachment);
                Assert.NotNull(log.Attachment.ID);
            }

            if (logs.Count > 0)
            {
                IndividualAccountAttachment.Log getLog =
                    IndividualAccountAttachment.Log.Get(id: logs.First().ID);
                Assert.Equal(getLog.ID, logs.First().ID);
                TestUtils.Log(getLog);
            }
        }

        // [M9] Log.Page returns (items, cursor).
        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IndividualAccountAttachment.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IndividualAccountAttachment.Log.Page(limit: 5, cursor: cursor);
                foreach (IndividualAccountAttachment.Log entity in page)
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

        // [M10] Log.Query / Log.Page accept limit, after, before, types, attachmentIds —
        // the parent-id filter is attachmentIds (plural), NOT accountRequestIds.
        // types filter uses a valid enum value ("created"), never "failed".
        [Fact]
        public void QueryParams()
        {
            List<IndividualAccountAttachment.Log> logs = IndividualAccountAttachment.Log.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                types: new List<string> { "created" },
                attachmentIds: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(logs.Count == 0);
        }

        // [M10] Log.Page accepts the same filters as Log.Query plus cursor.
        [Fact]
        public void PageParams()
        {
            List<IndividualAccountAttachment.Log> page;
            string cursor = null;
            (page, cursor) = IndividualAccountAttachment.Log.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                types: new List<string> { "created" },
                attachmentIds: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }
    }
}
