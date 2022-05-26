using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class WebhookTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<Webhook> webhooks = Webhook.Query(limit: 101).ToList();
            Assert.True(webhooks.Count <= 101);
            Assert.True(webhooks.First().ID != webhooks.Last().ID);
            foreach (Webhook webhook in webhooks)
            {
                TestUtils.Log(webhook);
                Assert.NotNull(webhook.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Webhook> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Webhook.Page(limit: 5, cursor: cursor);
                foreach (Webhook entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
        }

        [Fact]
        public void CreateGet()
        {
            Webhook webhook = Webhook.Create(
                url: "https://webhook.site/6cd3c98d-60ca-4f4a-97c6-a7d0365b15B7",
                subscription: new List<string> {
                    "credit-note",
                    "pix-request.in",
                    "pix-request.out",
                    "pix-reversal.in",
                    "pix-reversal.out",
                });
            TestUtils.Log(webhook);
            Webhook getWebhook = Webhook.Get(id: webhook.ID);
            Assert.Equal(getWebhook.ID, webhook.ID);
            Webhook deleteWebhook = Webhook.Delete(id: webhook.ID);
            Assert.Equal(deleteWebhook.ID, webhook.ID);
            TestUtils.Log(webhook);

        }

    }
}
