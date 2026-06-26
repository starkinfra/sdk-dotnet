using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class BusinessAttachmentLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<BusinessAttachment.Log> logs = BusinessAttachment.Log.Query(
                limit: 5
            ).ToList();
            Assert.True(logs.Count <= 101);
            Assert.True(logs.First().ID != logs.Last().ID);
            foreach (BusinessAttachment.Log log in logs)
            {
                Assert.NotNull(log.ID);
                Assert.NotNull(log.Type);
            }
            BusinessAttachment.Log getLog = BusinessAttachment.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BusinessAttachment.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BusinessAttachment.Log.Page(limit: 2, cursor: cursor);
                foreach (BusinessAttachment.Log entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 4);
        }
    }
}
