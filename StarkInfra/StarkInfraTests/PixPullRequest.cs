using Xunit;
using StarkInfra;
using StarkInfra.Utils;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixPullRequestTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<PixPullRequest> requests = PixPullRequest.Create(new List<PixPullRequest>() { Example() });
            PixPullRequest request = requests.First();
            Assert.NotNull(request.ID);
            PixPullRequest fetched = PixPullRequest.Get(id: request.ID);
            Assert.Equal(fetched.ID, request.ID);
        }

        [Fact]
        public void Query()
        {
            List<PixPullRequest> requests = PixPullRequest.Query(limit: 10).ToList();
            Assert.True(requests.Count <= 10);
            foreach (PixPullRequest request in requests)
            {
                Assert.NotNull(request.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<PixPullRequest> requests = new List<PixPullRequest>();
            List<PixPullRequest> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixPullRequest.Page(limit: 5, cursor: cursor);
                foreach (PixPullRequest entity in page)
                {
                    Assert.DoesNotContain(entity, requests);
                    requests.Add(entity);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(requests.Count <= 10);
        }

        [Fact]
        public void QueryParams()
        {
            List<PixPullRequest> requests = PixPullRequest.Query(
                limit: 10,
                after: new DateTime(2026, 1, 1),
                before: new DateTime(2026, 4, 30),
                status: new List<string> { "created" },
                tags: new List<string> { "test" },
                flows: new List<string> { "out" },
                subscriptionIds: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(requests.Count == 0);
        }

        public readonly string Content = "{\"event\": {\"created\": \"2026-03-17T22:17:48.687366+00:00\", \"id\": \"5980132964564992\", \"log\": {\"created\": \"2026-03-17T22:17:44.741312+00:00\", \"description\": \"The Pix Pull Request was created in Stark Infra.\", \"errors\": [], \"id\": \"4777799707525120\", \"reason\": \"\", \"request\": {\"amount\": 79562, \"attemptType\": \"default\", \"created\": \"2026-03-17T22:17:44.727124+00:00\", \"description\": \"Monthly fare\", \"due\": \"2026-03-18T19:17:44.382949+00:00\", \"endToEndId\": \"E32160637202617031917FXbuEOeqxTE\", \"flow\": \"out\", \"id\": \"5859939668983808\", \"receiverAccountNumber\": \"00000000\", \"receiverAccountType\": \"payment\", \"receiverBankCode\": \"32160637\", \"receiverBranchCode\": \"\", \"receiverName\": \"Stark Bank\", \"receiverTaxId\": \"39.908.427/0001-28\", \"reconciliationId\": \"20260317191744.382994-03001917VKqeyyGMWvK\", \"senderBankCode\": null, \"senderFinalName\": \"STARK SCD S.A.\", \"senderFinalTaxId\": \"39.908.427/0001-28\", \"senderTaxId\": \"99.999.919/9999-79\", \"status\": \"created\", \"subscriptionBacenId\": \"RR321606372026170319175775651\", \"subscriptionId\": \"6366699370577920\", \"tags\": [], \"updated\": \"2026-03-17T22:17:45.560279+00:00\"}, \"type\": \"created\"}, \"subscription\": \"pix-pull-request\", \"workspaceId\": \"4828094443552768\"}}";
        public readonly string GoodSignature = "MEUCIQDPci6mVcRQUqQazbol04cYvz8Ffuhh0birk4+8jSUH4AIgKlLhIH5zKzu+4jQlyabvSJin+8+5kJKiJpoqSQPCITg=";
        public readonly string BadSignature = "MEUCIQDPci6mVcRQUqQazbol04cYvz8Ffuhh0bIrk4+8jSUH4AIgKlLhIH5zKzu+4jQlyabvSJin+8+5kJKiJpoqSQPCITg=";

        [Fact]
        public void ParseWithRightSignature()
        {
            Event parsed = Event.Parse(Content, GoodSignature);
            Assert.NotNull(parsed.ID);
            Assert.NotNull(parsed.Log);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try
            {
                Event parsed = Event.Parse(Content, BadSignature);
            }
            catch (StarkCore.Error.InvalidSignatureError)
            {
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void ParseWithMalformedSignature()
        {
            try
            {
                Event parsed = Event.Parse(Content, "something is definitely wrong");
            }
            catch (StarkCore.Error.InvalidSignatureError)
            {
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void Update()
        {
            PixPullRequest target = PixPullRequest.Query(limit: 1, status: new List<string> { "created" }).FirstOrDefault();
            if (target == null) return;
            try
            {
                PixPullRequest updated = PixPullRequest.Update(
                    id: target.ID,
                    patchData: new Dictionary<string, object> {
                        { "status", "scheduled" }
                    }
                );
                Assert.NotNull(updated.ID);
            }
            catch (Exception e) when (e.Message.Contains("invalidAction") || e.Message.Contains("invalidStatusPatch"))
            {
            }
        }

        [Fact]
        public void Cancel()
        {
            PixPullRequest target = PixPullRequest.Query(limit: 1, status: new List<string> { "created" }).FirstOrDefault();
            if (target == null) return;
            try
            {
                PixPullRequest canceled = PixPullRequest.Cancel(target.ID, "senderUserRequested");
                Assert.NotNull(canceled.ID);
            }
            catch (Exception e) when (e.Message.Contains("invalidAction") || e.Message.Contains("invalidCancellation"))
            {
            }
        }

        internal static PixPullRequest Example()
        {
            Random rand = new Random();
            string bankCode = Environment.GetEnvironmentVariable("SANDBOX_BANK_CODE");
            string endToEndID = EndToEndID.Create(bankCode: bankCode);
            string subscriptionID = PixPullSubscription.Query(limit: 1, status: new List<string> { "active" }).FirstOrDefault()?.ID
                ?? PixPullSubscription.Create(new List<PixPullSubscription> { PixPullSubscriptionTest.Example() }).First().ID;
            return new PixPullRequest(
                amount: 52064,
                due: DateTime.UtcNow.AddDays(2),
                endToEndID: endToEndID,
                receiverAccountNumber: "876543-2",
                receiverAccountType: "checking",
                receiverBankCode: bankCode,
                reconciliationID: "recon-" + rand.Next(1, 0xffffff),
                subscriptionID: subscriptionID,
                attemptType: "default",
                tags: new List<string> { "test", "pix-pull" }
            );
        }
    }
}
