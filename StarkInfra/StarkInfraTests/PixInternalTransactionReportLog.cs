using Xunit;
using System;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixInternalTransactionReportLogTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryAndGet()
        {
            List<PixInternalTransactionReport.Log> logs = PixInternalTransactionReport.Log.Query(limit: 10).ToList();
            Assert.True(logs.Count <= 10);
            foreach (PixInternalTransactionReport.Log log in logs)
            {
                Assert.NotNull(log.ID);
                Assert.False(string.IsNullOrEmpty(log.Type));
                Assert.NotNull(log.Created);
                Assert.IsType<DateTime>(log.Created.Value);
            }
            PixInternalTransactionReport.Log getLog = PixInternalTransactionReport.Log.Get(id: logs.First().ID);
            Assert.Equal(getLog.ID, logs.First().ID);
        }

        [Fact]
        public void ReportDeserializesToPixInternalTransactionReport()
        {
            List<PixInternalTransactionReport.Log> logs = PixInternalTransactionReport.Log.Query(limit: 1).ToList();
            Assert.NotEmpty(logs);
            PixInternalTransactionReport.Log log = logs.First();
            Assert.NotNull(log.Report);
            Assert.IsType<PixInternalTransactionReport>(log.Report);
            Assert.NotNull(log.Report.ID);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixInternalTransactionReport.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixInternalTransactionReport.Log.Page(limit: 5, cursor: cursor);
                foreach (PixInternalTransactionReport.Log entity in page)
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

        [Fact]
        public void QueryParams()
        {
            List<PixInternalTransactionReport.Log> logs = PixInternalTransactionReport.Log.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                types: new List<string> { "success" },
                reportIds: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(logs.Count == 0);
        }

        [Fact]
        public void PageParams()
        {
            List<PixInternalTransactionReport.Log> page;
            string cursor = null;
            (page, cursor) = PixInternalTransactionReport.Log.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                types: new List<string> { "success" },
                reportIds: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }
    }
}
