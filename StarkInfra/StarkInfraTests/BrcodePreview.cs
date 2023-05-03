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
        public void Query()
        {
            List<StaticBrcode> staticBrcodes = StaticBrcode.Query(limit: 3).ToList();
            List<DynamicBrcode> dynamicBrcodes = DynamicBrcode.Query(limit: 3).ToList();
            List<BrcodePreview> brcodes = new List<BrcodePreview>
            {
                new BrcodePreview(
                        id: staticBrcodes[0].ID,
                        payerID: "20.018.183/0001-80"
                    ),
                new BrcodePreview(
                        id: staticBrcodes[1].ID,
                        payerID: "20.018.183/0001-80"
                    ),
                new BrcodePreview(
                        id: dynamicBrcodes[0].ID,
                        payerID: "20.018.183/0001-80"
                    ),
                new BrcodePreview(
                        id: dynamicBrcodes[1].ID,
                        payerID: "20.018.183/0001-80"
                    )
            };

            List<BrcodePreview> previews = BrcodePreview.Create(brcodes);
            Assert.True(previews.Count <= 4);

            int index = 0;
            foreach (BrcodePreview preview in previews)
            {
                TestUtils.Log(preview);
                Assert.NotNull(preview.ID);
                Assert.Equal(preview.ID, brcodes[index].ID);
                index++;
            }
        }
    }
}
