using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class DynamicBrcodeTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<DynamicBrcode> brcodes = DynamicBrcode.Create(new List<DynamicBrcode> { Example() });
            Assert.NotNull(brcodes[0].Uuid);
            DynamicBrcode getDynamicBrcode = DynamicBrcode.Get(uuid: brcodes[0].Uuid);
            Assert.Equal(getDynamicBrcode.Uuid, brcodes[0].Uuid);
            foreach(DynamicBrcode brcode in brcodes)
            {
                TestUtils.Log(brcode);
            }
        }

        [Fact]
        public void Query()
        {
            List<DynamicBrcode> brcodes = DynamicBrcode.Query(limit: 3).ToList();
            Assert.True(brcodes.Count <= 3);
            Assert.True(brcodes.First().ID != brcodes.Last().ID);
            foreach (DynamicBrcode brcode in brcodes)
            {
                TestUtils.Log(brcode);
                Assert.NotNull(brcode.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<DynamicBrcode> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = DynamicBrcode.Page(limit: 5, cursor: cursor);
                foreach (DynamicBrcode entity in page)
                {
                    TestUtils.Log(entity);
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 10);
        }
        
        public readonly string Content = "21f174ab942843eb90837a5c3135dfd6";
        public readonly string GoodSignature = "MEYCIQC+Ks0M54DPLEbHIi0JrMiWbBFMRETe/U2vy3gTiid3rAIhANMmOaxT03nx2bsdo+vg6EMhWGzdphh90uBH9PY2gJdd";
        public readonly string BadSignature = "MEUCIQDOpo1j+V40DNZK2URL2786UQK/8mDXon9ayEd8U0/l7AIgYXtIZJBTs8zCRR3vmted6Ehz/qfw1GRut/eYyvf1yOk=";

        [Fact]
        public void VerifyWithRightSignature()
        {
            string parsedDynamicBrcode = DynamicBrcode.Verify(Content, GoodSignature);
            TestUtils.Log(parsedDynamicBrcode);
        }

        [Fact]
        public void VerifyWithWrongSignature()
        {
            try {
                string parsedDynamicBrcode = DynamicBrcode.Verify(Content, BadSignature);
            } catch (StarkInfra.Error.InvalidSignatureError e) {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void VerifyWithMalformedSignature()
        {
            try
            {
                string parsedDynamicBrcode = DynamicBrcode.Verify(Content, BadSignature);
            }
            catch (StarkInfra.Error.InvalidSignatureError e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void SendResponseDue()
        {
            string response = DynamicBrcode.ResponseDue(
                version: 1,
                created: new DateTime(2022, 07, 15),
                due: new DateTime(2022, 07, 15),
                expiration: 100000,
                keyID: "+5511989898989",
                status: "paid",
                reconciliationID: "b77f5236-7ab9-4487-9f95-66ee6eaf1781",
                nominalAmount: 100,
                senderName: "Anthony Edward Stark",
                senderTaxID: "012.345.678-90",
                receiverName: "Jamie Lannister",
                receiverStreetLine: "Av. Paulista, 200",
                receiverCity: "Sao Paulo",
                receiverStateCode: "SP",
                receiverZipCode: "01234-567",
                receiverTaxID: "20.018.183/0001-8",
                fine: 200,
                interest: 1,
                discounts: new List<Discount> {
                    new Discount (
                        percentage: 5,
                        due: new DateTime(2022, 07, 18)
                    ),
                    new Discount (
                        percentage: 1,
                        due: new DateTime(2022, 07, 20)
                    )
                },
                description: "teste SDK"
            );
            TestUtils.Log(response);
        }

        [Fact]
        public void SendResponseInstant()
        {
            string response = DynamicBrcode.ResponseInstant(
                version: 1,
                created: new DateTime(2022, 07, 18),
                keyID: "+5511989898989",
                status: "paid",
                reconciliationID: "b77f5236-7ab9-4487-9f95-66ee6eaf1781",
                amount: 100
            );
            TestUtils.Log(response);
        }

        internal static DynamicBrcode Example()
        {
            return new DynamicBrcode(
                name: "Jamie Lannister",
                city: "Rio de Janeiro",
                externalID: Convert.ToString(new Random().Next(1, 999999999)),
                type: "instant",
                tags: new List<string>
                {
                    "tags teste"
                }
            );
        }
    }
}
