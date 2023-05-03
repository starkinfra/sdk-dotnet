using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class StaticBrcodeTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<StaticBrcode> brcode = StaticBrcode.Create( new List<StaticBrcode> { Example() } );
            Assert.NotNull(brcode[0].Uuid);
            StaticBrcode getStaticBrcode = StaticBrcode.Get(uuid: brcode[0].Uuid);
            Assert.Equal(getStaticBrcode.Uuid, brcode[0].Uuid);
            TestUtils.Log(getStaticBrcode);
        }

        [Fact]
        public void Query()
        {
            List<StaticBrcode> brcodes = StaticBrcode.Query(limit: 3).ToList();
            Assert.True(brcodes.Count <= 3);
            Assert.True(brcodes.First().Uuid != brcodes.Last().Uuid);
            foreach (StaticBrcode brcode in brcodes)
            {
                TestUtils.Log(brcode);
                Assert.NotNull(brcode.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<StaticBrcode> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = StaticBrcode.Page(limit: 5, cursor: cursor);
                foreach (StaticBrcode entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.Uuid, ids);
                    ids.Add(entity.Uuid);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 10);
        }

        internal static StaticBrcode Example()
        {
            return new StaticBrcode(
                name: "Jamie Lannister",
                keyID: "+5511989898989",
                city: "Rio de Janeiro",
                cashierBankCode: "20018183",
                description: "A StaticBrcode"
            );
        }
    }
}

