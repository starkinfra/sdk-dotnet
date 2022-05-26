using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class PixClaimTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<PixClaim> pixClaims = PixClaim.Query(limit: 101, status: "canceled").ToList();
            Assert.True(pixClaims.Count <= 101);
            Assert.True(pixClaims.First().ID != pixClaims.Last().ID);
            foreach (PixClaim pixClaim in pixClaims)
            {
                TestUtils.Log(pixClaim);
                Assert.NotNull(pixClaim.ID);
                Assert.Equal("canceled", pixClaim.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixClaim> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixClaim.Page(limit: 5, cursor: cursor);
                foreach (PixClaim entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.Equal(10, ids.Count);
        }

        [Fact]
        public void Update()
        {
            List<PixClaim> pixClaims = PixClaim.Query(limit: 2, status: "delivered").ToList();
            Assert.Equal(2, pixClaims.Count);
            Assert.True(pixClaims.First().ID != pixClaims.Last().ID);
            Dictionary<string, object> patchData = new Dictionary<string, object> {
                { "reason", "userRequested" }
            };
            foreach (PixClaim pixClaim in pixClaims)
            {
                TestUtils.Log(pixClaim);
                Assert.NotNull(pixClaim.ID);
                Assert.Equal("delivered", pixClaim.Status);
                PixClaim updatedPixClaim = PixClaim.Update(id: pixClaim.ID, status: "canceled", patchData: patchData);
                TestUtils.Log(updatedPixClaim);
                Assert.Equal(updatedPixClaim.ID, pixClaim.ID);
            }
        }

        [Fact]
        public void CreateGet()
        {
            PixClaim pixClaim = PixClaim.Create(Example());
            TestUtils.Log(pixClaim);
            TestUtils.Log(pixClaim);
            PixClaim getPixClaim = PixClaim.Get(id: pixClaim.ID);
            Assert.Equal(getPixClaim.ID, pixClaim.ID);
            TestUtils.Log(pixClaim);
        }

        internal static PixClaim Example()
        {
            return new PixClaim(
                accountCreated: new DateTime(2022, 02, 01),
                accountNumber: "5692908409716736",
                accountType: "checking",
                branchCode: "0000",
                keyId: TestUtils.RandomPhoneNumber(),
                name: "testKey",
                taxId: "012.345.678-90"
            );
        }
    }
}
