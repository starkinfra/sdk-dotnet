using System;
using StarkInfra;
using StarkInfra.Utils;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace StarkInfraTests
{
    public class PixChargebackTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<PixChargeback> chargebacks = PixChargeback.Query(limit: 101, status: "canceled").ToList();
            Assert.True(chargebacks.Count <= 101);
            Assert.True(chargebacks.First().ID != chargebacks.Last().ID);
            foreach (PixChargeback chargeback in chargebacks)
            {
                TestUtils.Log(chargeback);
                Assert.NotNull(chargeback.ID);
                Assert.Equal("canceled", chargeback.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixChargeback> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixChargeback.Page(limit: 5, cursor: cursor);
                foreach (PixChargeback entity in page)
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
        }

        [Fact]
        public void Update()
        {
            List<PixChargeback> chargebacks = PixChargeback.Query(limit: 2, status: "delivered").ToList();
            Assert.Equal(2, chargebacks.Count);
            Assert.True(chargebacks.First().ID != chargebacks.Last().ID);
            string expected = "delivered";
            string expectedResult = "canceled";
            Dictionary<string, object> patchData = new Dictionary<string, object> {
                { "rejectionReason", "noBalance" }
            };
            foreach (PixChargeback chargeback in chargebacks)
            {
                TestUtils.Log(chargeback);
                Assert.NotNull(chargeback.ID);
                Assert.Equal(expected, chargeback.Status);
                PixChargeback updatedPixChargeback = PixChargeback.Update(id: chargeback.ID, result: "rejected", patchData);
                TestUtils.Log(updatedPixChargeback);
                Assert.Equal(expectedResult, updatedPixChargeback.Status);
            }
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            List<PixChargeback> chargeback = PixChargeback.Create(new List<PixChargeback> {Example()});
            TestUtils.Log(chargeback);
            PixChargeback getPixChargeback = PixChargeback.Get(id: chargeback.First().ID);
            Assert.Equal(getPixChargeback.ID, chargeback.First().ID);
            PixChargeback cancelPixChargeback = PixChargeback.Cancel(id: getPixChargeback.ID);
            Assert.Equal(cancelPixChargeback.ID, getPixChargeback.ID);
            TestUtils.Log(chargeback);
        }

        internal static PixChargeback Example()
        {
            List<PixRequest> request = PixRequest.Query(limit: 1, status: new List<string> { "success" }).ToList();
            return new PixChargeback(
                amount: 100,
                referenceID: request[0].EndToEndID,
                reason: "fraud",
                tags: new List<string> { "teste sdk" }
            );
        }
    }
}
