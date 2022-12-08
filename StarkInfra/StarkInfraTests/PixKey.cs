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
            List<PixKey> keys = PixKey.Query(limit: 101).ToList();
            Assert.True(keys.Count <= 101);
            Assert.True(keys.First().ID != keys.Last().ID);
            foreach (PixKey key in keys)
            {
                TestUtils.Log(key);
                Assert.NotNull(key.ID);
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
            List<PixKey> keys = PixKey.Query(limit: 2, status: "registered").ToList();
            Assert.Equal(2, keys.Count);
            Assert.True(keys.First().ID != keys.Last().ID);
            string expectedStatus = "reconciliation";
            foreach (PixKey key in keys)
            {
                TestUtils.Log(key);
                Assert.NotNull(key.ID);
                Dictionary<string, object> patchData = new Dictionary<string, object> {
                    { "name", "John Snow" }
                };
                PixKey updatedPixKey = PixKey.Update(id: key.ID, reason: expectedStatus, patchData);
                TestUtils.Log(updatedPixKey);
                Assert.Equal("John Snow", updatedPixKey.Name);
            }
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            PixKey key = PixKey.Create(Example());
            TestUtils.Log(key);
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "endToEndID", EndToEndID.Create(bankCode: Environment.GetEnvironmentVariable("SANDBOX_BANKCODE")) }
            };
            System.Threading.Thread.Sleep(4000);
            PixKey getPixKey = PixKey.Get(id: key.ID, payerID: key.TaxID, parameters: parameters);
            Assert.Equal(getPixKey.ID, key.ID);
            PixKey canceledKey = PixKey.Cancel(id: key.ID);
            Assert.Equal(canceledKey.ID, key.ID);
            TestUtils.Log(key);
        }

        internal static PixKey Example()
        {
            return new PixKey(
                accountCreated: new DateTime(2022, 03, 01),
                accountNumber: TestUtils.RandomNumberString(10000, 99999),
                accountType: "savings",
                branchCode: TestUtils.RandomNumberString(1000, 9999),
                name: "Jamie Lannster",
                taxID: "012.345.678-90",
                id: TestUtils.RandomPhoneNumber()
            );
        }
    }
}
