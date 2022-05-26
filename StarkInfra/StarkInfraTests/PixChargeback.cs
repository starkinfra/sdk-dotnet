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
            List<PixChargeback> pixChargebacks = PixChargeback.Query(limit: 101, status: "canceled").ToList();
            Assert.True(pixChargebacks.Count <= 101);
            Assert.True(pixChargebacks.First().ID != pixChargebacks.Last().ID);
            foreach (PixChargeback pixChargeback in pixChargebacks)
            {
                TestUtils.Log(pixChargeback);
                Assert.NotNull(pixChargeback.ID);
                Assert.Equal("canceled", pixChargeback.Status);
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
            List<PixChargeback> pixChargebacks = PixChargeback.Query(limit: 2, status: "delivered").ToList();
            Assert.Equal(2, pixChargebacks.Count); 
            Assert.True(pixChargebacks.First().ID != pixChargebacks.Last().ID);
            string expected = "delivered";
            string expectedResult = "canceled";
            Dictionary<string, object> patchData = new Dictionary<string, object> {
                { "rejectionReason", "noBalance" }
            };
            foreach (PixChargeback pixChargeback in pixChargebacks)
            {
                TestUtils.Log(pixChargeback);
                Assert.NotNull(pixChargeback.ID);
                Assert.Equal(expected, pixChargeback.Status);
                PixChargeback updatedPixChargeback = PixChargeback.Update(id: pixChargeback.ID, result: "rejected", patchData);
                TestUtils.Log(updatedPixChargeback);
                Assert.Equal(expectedResult, updatedPixChargeback.Status);
            }
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            List<PixChargeback> pixChargeback = PixChargeback.Create(new List<PixChargeback> {Example()});
            TestUtils.Log(pixChargeback);
            PixChargeback getPixChargeback = PixChargeback.Get(id: pixChargeback.First().ID);
            Assert.Equal(getPixChargeback.ID, pixChargeback.First().ID);
            PixChargeback cancelPixChargeback = PixChargeback.Cancel(id: getPixChargeback.ID);
            Assert.Equal(cancelPixChargeback.ID, getPixChargeback.ID);
            TestUtils.Log(pixChargeback);
        }

        internal static PixChargeback Example()
        {
            List<PixRequest> request = PixRequest.Query(limit: 1).ToList();
            return new PixChargeback(
                amount : 100,
                referenceId : request[0].EndToEndID,
                reason : "fraud"
            );
        }
    }
}
