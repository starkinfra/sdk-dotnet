using Xunit;
using StarkInfra;
using StarkInfra.Utils;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixPullSubscriptionTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<PixPullSubscription> subscriptions = PixPullSubscription.Create(new List<PixPullSubscription>() { Example() });
            PixPullSubscription subscription = subscriptions.First();
            Assert.NotNull(subscription.ID);
            PixPullSubscription getSubscription = PixPullSubscription.Get(id: subscription.ID);
            Assert.Equal(getSubscription.ID, subscription.ID);
        }

        [Fact]
        public void Query()
        {
            List<PixPullSubscription> subscriptions = PixPullSubscription.Query(limit: 10).ToList();
            Assert.True(subscriptions.Count <= 10);
            foreach (PixPullSubscription subscription in subscriptions)
            {
                Assert.NotNull(subscription.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<PixPullSubscription> subscriptions = new List<PixPullSubscription>();
            List<PixPullSubscription> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixPullSubscription.Page(limit: 5, cursor: cursor);
                foreach (PixPullSubscription entity in page)
                {
                    Assert.DoesNotContain(entity, subscriptions);
                    subscriptions.Add(entity);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(subscriptions.Count <= 10);
        }

        [Fact]
        public void QueryParams()
        {
            List<PixPullSubscription> subscriptions = PixPullSubscription.Query(
                limit: 10,
                after: new DateTime(2026, 1, 1),
                before: new DateTime(2026, 4, 30),
                status: new List<string> { "active" },
                tags: new List<string> { "test" },
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(subscriptions.Count == 0);
        }

        public readonly string Content = "{\"event\": {\"created\": \"2026-03-17T20:24:02.006080+00:00\", \"id\": \"5739991880695808\", \"log\": {\"created\": \"2026-03-17T20:23:58.050406+00:00\", \"errors\": [], \"id\": \"5340798381981696\", \"reason\": \"\", \"subscription\": {\"amount\": 52064, \"amountMinLimit\": 0, \"bacenId\": \"RR321606372026170317231564231\", \"created\": \"2026-03-17T20:23:57.255567+00:00\", \"description\": \"A Lannister always pays his debts\", \"due\": \"2026-04-17T02:59:59.999000+00:00\", \"externalId\": \"606512134\", \"flow\": \"out\", \"id\": \"5656970050666496\", \"installmentEnd\": \"\", \"installmentStart\": \"2026-03-18T02:59:59.999999+00:00\", \"interval\": \"month\", \"pullRetryLimit\": 3, \"receiverBankCode\": \"32160637\", \"receiverName\": \"Stark Bank\", \"receiverTaxId\": \"39.908.427/0001-28\", \"referenceCode\": \"36135971\", \"senderAccountNumber\": \"55213\", \"senderBankCode\": null, \"senderBranchCode\": \"356\", \"senderCityCode\": \"\", \"senderFinalName\": \"STARK SCD S.A.\", \"senderFinalTaxId\": \"39.908.427/0001-28\", \"senderTaxId\": \"99.999.919/9999-79\", \"status\": \"created\", \"tags\": [], \"type\": \"push\", \"updated\": \"2026-03-17T20:23:58.050421+00:00\"}, \"type\": \"delivering\"}, \"subscription\": \"pix-pull-subscription\", \"workspaceId\": \"4828094443552768\"}}";
        public readonly string GoodSignature = "MEUCIQCCZWR4+JYoDNENLnRbSCGGZf+atOaG4q8jWB3ADgc+DQIgIZ1LuXLZ06pke2qzaMNTlDLwcriuH+S3ve1aTQeqNK0=";
        public readonly string BadSignature = "MEUCIQCCZWR4+JYoDNENLnRbSCGGZf+atOaG4q8jWB3ADgc+DQIgIZ1LuXLZ06pke2qzaMNTlDLwcriuH+S3ve1aTQEqNK0=";

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
            PixPullSubscription target = PixPullSubscription.Query(limit: 1, status: new List<string> { "created" }).FirstOrDefault();
            if (target == null) return;
            try
            {
                PixPullSubscription updated = PixPullSubscription.Update(
                    id: target.ID,
                    patchData: new Dictionary<string, object> {
                        { "status", "approved" },
                        { "senderCityCode", "1100015" }
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
            PixPullSubscription target = PixPullSubscription.Query(limit: 1, status: new List<string> { "active" }).FirstOrDefault();
            if (target == null) return;
            try
            {
                PixPullSubscription canceled = PixPullSubscription.Cancel(target.ID, "receiverUserRequested");
                Assert.NotNull(canceled.ID);
            }
            catch (Exception e) when (e.Message.Contains("invalidAction") || e.Message.Contains("invalidCancellation"))
            {
            }
        }

        internal static PixPullSubscription Example()
        {
            Random rand = new Random();
            string bankCode = Environment.GetEnvironmentVariable("SANDBOX_BANK_CODE");
            string bacenID = BacenID.Create(bankCode: bankCode);
            return new PixPullSubscription(
                bacenID: bacenID,
                externalID: Convert.ToString(rand.Next(1, 999999999)),
                installmentStart: DateTime.UtcNow.AddDays(1),
                interval: "month",
                receiverName: "Stark Bank",
                receiverTaxID: "39.908.427/0001-28",
                senderAccountNumber: "876543-2",
                senderBankCode: bankCode,
                senderBranchCode: "1357-9",
                senderTaxID: "01234567890",
                type: "push",
                amount: 52064,
                referenceCode: "ref-" + rand.Next(1, 999999999).ToString(),
                receiverBankCode: bankCode,
                pullRetryLimit: 3,
                senderFinalName: "STARK SCD S.A.",
                senderFinalTaxID: "39908427000128",
                description: "A Lannister always pays his debts",
                tags: new List<string> { "test", "pix-pull" }
            );
        }
    }
}
