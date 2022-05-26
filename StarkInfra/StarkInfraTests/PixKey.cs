using System;
using StarkInfra;
using StarkInfra.Utils;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class PixKeyTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<PixKey> pixKeys = PixKey.Query(limit: 101).ToList();
            Assert.True(pixKeys.Count <= 101);
            Assert.True(pixKeys.First().ID != pixKeys.Last().ID);
            foreach (PixKey pixKey in pixKeys)
            {
                TestUtils.Log(pixKey);
                Assert.NotNull(pixKey.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixKey> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixKey.Page(limit: 5, cursor: cursor);
                foreach (PixKey entity in page)
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

        [Fact]
        public void Update()
        {
            List<PixKey> pixKeys = PixKey.Query(limit: 2, status: "registered").ToList();
            Assert.Equal(2, pixKeys.Count);
            Assert.True(pixKeys.First().ID != pixKeys.Last().ID);
            string expectedStatus = "reconciliation";
            foreach (PixKey pixKey in pixKeys)
            {
                TestUtils.Log(pixKey);
                Assert.NotNull(pixKey.ID);
                Dictionary<string, object> patchData = new Dictionary<string, object> {
                    { "name", "John Snow" }
                };
                PixKey updatedPixKey = PixKey.Update(id: pixKey.ID, reason: expectedStatus, patchData);
                TestUtils.Log(updatedPixKey);
                Assert.Equal("John Snow", updatedPixKey.Name);
            }
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            PixKey pixKey = PixKey.Create(Example());
            TestUtils.Log(pixKey);
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "endToEndId", EndToEndId.Create(bankCode: Environment.GetEnvironmentVariable("SANDBOX_BANKCODE")) }
            };
            PixKey getPixKey = PixKey.Get(id: pixKey.ID, payerId: pixKey.TaxID, parameters: parameters);
            Assert.Equal(getPixKey.ID, pixKey.ID);
            PixKey canceledKey = PixKey.Cancel(id: pixKey.ID);
            Assert.Equal(canceledKey.ID, pixKey.ID);
            TestUtils.Log(pixKey);
        }

        internal static PixKey Example()
        {
            return new PixKey(
                accountCreated: new DateTime(2022, 03, 01),
                accountNumber: TestUtils.RandomNumberString(10000, 99999),
                accountType: "savings",
                branchCode: TestUtils.RandomNumberString(1000, 9999),
                name: "Jamie Lannster",
                taxId: "012.345.678-90",
                id: TestUtils.RandomPhoneNumber()
            );
        }
    }
}
