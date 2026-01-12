using Xunit;
using StarkInfra;
using StarkInfra.Utils;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixDisputeTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void QueryGet()
        {
            List<PixDispute> disputes = PixDispute.Query(
                limit: 3,
                after: new DateTime(2019, 04, 01),
                before: new DateTime(2030, 04, 30)
            ).ToList();

            foreach (PixDispute dispute in disputes)
            {
                Assert.NotNull(dispute.ID);
                PixDispute getDispute = PixDispute.Get(id: dispute.ID);
                Assert.Equal(dispute.ID, getDispute.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixDispute> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixDispute.Page(
                    limit: 2,
                    after: new DateTime(2019, 04, 01),
                    before: new DateTime(2030, 04, 30),
                    cursor: cursor
                );
                foreach (PixDispute dispute in page)
                {
                    if (ids.Contains(dispute.ID))
                    {
                        throw new Exception("repeated id");
                    }
                    ids.Add(dispute.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 4);
        }

        [Fact]
        public void Cancel()
        {
            List<PixDispute> disputes = PixDispute.Query(
                limit: 1,
                status: new List<string> { "created", "delivered" }
            ).ToList();

            if (disputes.Count == 0)
            {
                throw new Exception("No disputes found");
            }

            PixDispute dispute = disputes.First();
            PixDispute canceledDispute = PixDispute.Cancel(id: dispute.ID);
            Assert.NotNull(canceledDispute.ID);
            Assert.Equal(dispute.ID, canceledDispute.ID);
        }

        [Fact]
        public void LogQueryAndGet()
        {
            List<PixDispute.Log> logs = PixDispute.Log.Query(
                limit: 2,
                after: new DateTime(2019, 04, 01),
                before: new DateTime(2030, 04, 30)
            ).ToList();

            foreach (PixDispute.Log log in logs)
            {
                Assert.NotNull(log.ID);
                Assert.NotNull(log.Dispute.ID);
                PixDispute.Log getLog = PixDispute.Log.Get(id: log.ID);
                Assert.Equal(log.ID, getLog.ID);
            }
        }

        [Fact]
        public void LogPage()
        {
            List<string> ids = new List<string>();
            List<PixDispute.Log> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixDispute.Log.Page(
                    limit: 2,
                    after: new DateTime(2019, 04, 01),
                    before: new DateTime(2030, 04, 30),
                    cursor: cursor
                );
                foreach (PixDispute.Log log in page)
                {
                    if (ids.Contains(log.ID))
                    {
                        throw new Exception("repeated id");
                    }
                    ids.Add(log.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 4);
        }

        [Fact]
        public void PixDisputeEventParse()
        {
            string content = "{\"event\": {\"created\": \"2025-12-19T19:20:08.687079+00:00\", \"id\": \"4543235613523968\", \"log\": {\"created\": \"2025-12-19T19:20:08.107566+00:00\", \"dispute\": {\"bacenId\": \"42e3c802-22c0-4862-b352-cedc912c07a1\", \"created\": \"2025-12-19T19:16:04.867430+00:00\", \"description\": \"\", \"flow\": \"in\", \"id\": \"4652621482688512\", \"maxHopCount\": 5, \"maxHopInterval\": 86400, \"maxTransactionCount\": 500, \"method\": \"scam\", \"minTransactionAmount\": 20000, \"operatorEmail\": \"fraud@company.com\", \"operatorPhone\": \"+5511989898989\", \"referenceId\": \"E20018183202512191914WcfANNEIYnt\", \"status\": \"analysed\", \"tags\": [], \"transactions\": [{\"amount\": 20000, \"endToEndId\": \"E20018183202512191914WcfANNEIYnt\", \"nominalAmount\": 20000, \"receiverAccountCreated\": \"\", \"receiverBankCode\": \"39908427\", \"receiverId\": \"1\", \"receiverTaxIdCreated\": \"\", \"receiverType\": \"business\", \"senderAccountCreated\": \"\", \"senderBankCode\": \"20018183\", \"senderId\": \"2\", \"senderTaxIdCreated\": \"\", \"senderType\": \"business\", \"settled\": \"2025-12-19T19:14:25.760000+00:00\"}], \"updated\": \"2025-12-19T19:20:08.107585+00:00\"}, \"errors\": [], \"id\": \"6007878011846656\", \"type\": \"analysed\"}, \"subscription\": \"pix-dispute\", \"workspaceId\": \"5560467233701888\"}}";
            string validSignature = "MEYCIQCPgzyktxttTM9ooQaXq37NvFjL2cF/nQMfl1rvUcsLAQIhAKLbphPa5311mHvXlz6Rtkk+LPhctxgGYOnxAdhdldls";

            Event parsedEvent = Event.Parse(content, validSignature);
            PixDispute.Log log = parsedEvent.Log as PixDispute.Log;

            Assert.NotNull(log);
            Assert.NotNull(log.Dispute);
        }
    }
}

