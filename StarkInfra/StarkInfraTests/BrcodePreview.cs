using System;
using StarkInfra;
using Xunit;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class BrcodePreviewTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Create()
        {
            string payerId = "20018183000180";
            List<StaticBrcode> staticBrcodes = StaticBrcode.Query(limit: 2).ToList();
            List<DynamicBrcode> dynamicBrcodes = DynamicBrcode.Query(limit: 2).ToList();

            List<string> allIds = staticBrcodes.Select(b => b.ID).Concat(dynamicBrcodes.Select(b => b.ID)).ToList();

            List<BrcodePreview> brcodes = allIds
                .Select(id => new BrcodePreview(id: id, payerId: payerId))
                .ToList();

            List<BrcodePreview> previews = BrcodePreview.Create(brcodes);
            Assert.True(previews.Count == 4);

            int index = 0;
            foreach (BrcodePreview preview in previews)
            {
                Assert.NotNull(preview.ID);
                Assert.NotNull(preview.EndToEndID);
                Assert.Equal(preview.ID, brcodes[index].ID);
                index++;
            }
        }

        [Fact]
        public void CreatePreviewFromInstantBrcode()
        {
            string type = "instant";
            DynamicBrcode createdDynamicBrcode = DynamicBrcodeTest.CreateDynamicBrcodeByType(type);
            BrcodePreview preview = CreateBrcodePreviewById(createdDynamicBrcode.ID);

            Assert.Equal(createdDynamicBrcode.ID, preview.ID);
            Assert.Null(preview.Due);
            Assert.Null(preview.Subscription);
        }

        [Fact]
        public void CreatePreviewFromDueBrcode()
        {
            string type = "due";
            DynamicBrcode createdDynamicBrcode = DynamicBrcodeTest.CreateDynamicBrcodeByType(type);
            BrcodePreview preview = CreateBrcodePreviewById(createdDynamicBrcode.ID);

            Assert.Equal(createdDynamicBrcode.ID, preview.ID);
            Assert.NotNull(preview.Due);
            Assert.Null(preview.Subscription);
        }

        [Fact]
        public void CreatePreviewFromSubscriptionBrcode()
        {
            string type = "subscription";
            DynamicBrcode createdDynamicBrcode = DynamicBrcodeTest.CreateDynamicBrcodeByType(type);
            BrcodePreview preview = CreateBrcodePreviewById(createdDynamicBrcode.ID);

            Assert.Equal(createdDynamicBrcode.ID, preview.ID);
            Assert.Equal("", preview.PayerId);
            Assert.Equal("qrcode", preview.Subscription.Type);
        }

        [Fact]
        public void CreatePreviewFromSubscriptionAndInstantBrcode()
        {
            string type = "subscriptionAndInstant";
            DynamicBrcode createdDynamicBrcode = DynamicBrcodeTest.CreateDynamicBrcodeByType(type);
            BrcodePreview preview = CreateBrcodePreviewById(createdDynamicBrcode.ID);

            Assert.Equal(createdDynamicBrcode.ID, preview.ID);
            Assert.NotEqual("", preview.PayerId);
            Assert.Equal("qrcodeAndPayment", preview.Subscription.Type);
        }

        [Fact]
        public void CreatePreviewFromDueAndOrSubscriptionBrcode()
        {
            string type = "dueAndOrSubscription";
            DynamicBrcode createdDynamicBrcode = DynamicBrcodeTest.CreateDynamicBrcodeByType(type);
            BrcodePreview preview = CreateBrcodePreviewById(createdDynamicBrcode.ID);

            Assert.Equal(createdDynamicBrcode.ID, preview.ID);
            Assert.NotEqual("", preview.PayerId);
            Assert.Equal("paymentAndOrQrcode", preview.Subscription.Type);
        }

        internal static BrcodePreview CreateBrcodePreviewById(string id)
        {
            List<BrcodePreview> previews = BrcodePreview.Create(new List<BrcodePreview>
            {
                new BrcodePreview(id: id, payerId: "20018183000180")
            });

            return previews[0];
        }
    }
}
