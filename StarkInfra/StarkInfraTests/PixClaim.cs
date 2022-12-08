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
            List<PixClaim> claims = PixClaim.Query(limit: 101, status: "canceled").ToList();
            Assert.True(claims.Count <= 101);
            Assert.True(claims.First().ID != claims.Last().ID);
            foreach (PixClaim claim in claims)
            {
                TestUtils.Log(claim);
                Assert.NotNull(claim.ID);
                Assert.Equal("canceled", claim.Status);
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
            List<PixClaim> claims = PixClaim.Query(limit: 2, status: "delivered").ToList();
            Assert.Equal(2, claims.Count);
            Assert.True(claims.First().ID != claims.Last().ID);
            
            foreach (PixClaim claim in claims)
            {
                Dictionary<string, object> patchData = new Dictionary<string, object> {
                    { "reason", "fraud" }
                };
                TestUtils.Log(claim);
                Assert.NotNull(claim.ID);
                Assert.Equal("delivered", claim.Status);
                PixClaim updatedPixClaim = PixClaim.Update(id: claim.ID, status: "canceled", patchData: patchData);
                TestUtils.Log(updatedPixClaim);
                Assert.Equal(updatedPixClaim.ID, claim.ID);
            }
        }

        [Fact]
        public void CreateGet()
        {
            PixClaim claim = PixClaim.Create(Example());
            TestUtils.Log(claim);
            TestUtils.Log(claim);
            PixClaim getPixClaim = PixClaim.Get(id: claim.ID);
            Assert.Equal(getPixClaim.ID, claim.ID);
            TestUtils.Log(claim);
        }

        internal static PixClaim Example()
        {
            return new PixClaim(
                accountCreated: new DateTime(2022, 02, 01),
                accountNumber: "5692908409716736",
                accountType: "checking",
                branchCode: "0000",
                keyID: TestUtils.RandomPhoneNumber(),
                name: "testKey",
                taxID: "012.345.678-90",
                tags: new List<string> { "teste sdk" }
            );
        }
    }
}
