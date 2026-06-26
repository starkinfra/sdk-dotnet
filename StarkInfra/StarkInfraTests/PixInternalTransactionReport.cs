using Xunit;
using StarkInfra;
using StarkInfra.Utils;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixInternalTransactionReportTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<PixInternalTransactionReport> reports = PixInternalTransactionReport.Create(
                new List<PixInternalTransactionReport>() { Example() }
            );
            PixInternalTransactionReport report = reports.First();
            Assert.NotNull(reports.First().ID);
            PixInternalTransactionReport getReport = PixInternalTransactionReport.Get(id: report.ID);
            Assert.Equal(getReport.ID, report.ID);
        }

        [Fact]
        public void CreateGetReversal()
        {
            List<PixInternalTransactionReport> reports = PixInternalTransactionReport.Create(
                new List<PixInternalTransactionReport>() { ReversalExample() }
            );
            PixInternalTransactionReport report = reports.First();
            Assert.NotNull(reports.First().ID);
            PixInternalTransactionReport getReport = PixInternalTransactionReport.Get(id: report.ID);
            Assert.Equal(getReport.ID, report.ID);
        }

        [Fact]
        public void CreateOutputFields()
        {
            List<PixInternalTransactionReport> reports = PixInternalTransactionReport.Create(
                new List<PixInternalTransactionReport>() { Example() }
            );
            PixInternalTransactionReport report = reports.First();
            Assert.NotNull(report.ID);
            Assert.False(string.IsNullOrEmpty(report.Status));
            Assert.NotNull(report.Updated);
            Assert.IsType<DateTime>(report.Created);
            Assert.IsType<DateTime>(report.Updated.Value);
        }

        [Fact]
        public void Query()
        {
            List<PixInternalTransactionReport> reports = PixInternalTransactionReport.Query(limit: 10).ToList();
            Assert.True(reports.Count <= 10);
            foreach (PixInternalTransactionReport report in reports)
            {
                Assert.NotNull(report.ID);
            }
        }

        [Fact]
        public void QueryIds()
        {
            List<PixInternalTransactionReport> reports = PixInternalTransactionReport.Query(limit: 10).ToList();
            List<string> reportIdsExpected = new List<string>();
            foreach (PixInternalTransactionReport report in reports)
            {
                Assert.NotNull(report.ID);
                reportIdsExpected.Add(report.ID);
            }

            List<PixInternalTransactionReport> reportsResult = PixInternalTransactionReport.Query(
                limit: 10, ids: reportIdsExpected
            ).ToList();
            List<string> reportIdsResult = new List<string>();
            foreach (PixInternalTransactionReport report in reportsResult)
            {
                Assert.NotNull(report.ID);
                reportIdsResult.Add(report.ID);
            }

            reportIdsExpected.Sort();
            reportIdsResult.Sort();
            Assert.Equal(reportIdsExpected, reportIdsResult);
        }

        [Fact]
        public void QueryParams()
        {
            List<PixInternalTransactionReport> reports = PixInternalTransactionReport.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "success" },
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(reports.Count == 0);
        }

        [Fact]
        public void Page()
        {
            (List<PixInternalTransactionReport> page, string cursor) = PixInternalTransactionReport.Page(limit: 5);
            Assert.True(page.Count > 0);
            Assert.NotNull(cursor);
            foreach (PixInternalTransactionReport entity in page)
            {
                Assert.NotNull(entity.ID);
            }
        }

        [Fact]
        public void PageParams()
        {
            List<PixInternalTransactionReport> page;
            string cursor = null;
            (page, cursor) = PixInternalTransactionReport.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "success" },
                ids: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }

        internal static PixInternalTransactionReport Example()
        {
            string senderBankCode = Environment.GetEnvironmentVariable("SANDBOX_BANKCODE");
            return new PixInternalTransactionReport(
                amount: new Random().Next(1, 1000),
                created: DateTime.Now,
                endToEndID: EndToEndID.Create(bankCode: senderBankCode),
                method: "manual",
                referenceType: "request",
                senderAccountNumber: "00000-0",
                senderBranchCode: "0000",
                senderAccountType: "checking",
                senderBankCode: senderBankCode,
                senderTaxID: "01234567890",
                receiverAccountNumber: "00000-1",
                receiverBranchCode: "0001",
                receiverAccountType: "checking",
                receiverBankCode: "20018183",
                receiverTaxID: "01234567890"
            );
        }

        internal static PixInternalTransactionReport ReversalExample()
        {
            string senderBankCode = Environment.GetEnvironmentVariable("SANDBOX_BANKCODE");
            const string alphanumeric = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            char[] suffix = new char[11];
            for (int i = 0; i < suffix.Length; i++)
            {
                suffix[i] = alphanumeric[random.Next(alphanumeric.Length)];
            }
            string returnID = "D" + "00000665" + DateTime.Now.ToString(@"yyyyMMddHHmm") + new string(suffix);
            return new PixInternalTransactionReport(
                amount: random.Next(1, 1000),
                created: DateTime.Now,
                endToEndID: EndToEndID.Create(bankCode: senderBankCode),
                method: "dict",
                referenceType: "reversal",
                senderAccountNumber: "00000-0",
                senderBranchCode: "0000",
                senderAccountType: "checking",
                senderBankCode: senderBankCode,
                senderTaxID: "01234567890",
                receiverAccountNumber: "00000-1",
                receiverBranchCode: "0001",
                receiverAccountType: "checking",
                receiverBankCode: "20018183",
                receiverTaxID: "01234567890",
                returnID: returnID
            );
        }
    }
}
