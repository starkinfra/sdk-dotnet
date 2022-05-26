using Xunit;
using StarkInfra;
using StarkInfra.Utils;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixRequestTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<PixRequest> pixRequests = PixRequest.Create(new List<PixRequest>() { Example() });
            PixRequest pixRequest = pixRequests.First();
            Assert.NotNull(pixRequests.First().ID);
            PixRequest getPixRequest = PixRequest.Get(id: pixRequest.ID);
            Assert.Equal(getPixRequest.ID, pixRequest.ID);
            TestUtils.Log(pixRequest);
        }

        [Fact]
        public void Query()
        {
            List<PixRequest> pixRequests = PixRequest.Query(limit: 101, status: "success").ToList();
            Assert.True(pixRequests.Count <= 101);
            Assert.True(pixRequests.First().ID != pixRequests.Last().ID);
            foreach (PixRequest pixRequest in pixRequests)
            {
                TestUtils.Log(pixRequest);
                Assert.NotNull(pixRequest.ID);
                Assert.Equal("success", pixRequest.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixRequest> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixRequest.Page(limit: 5, cursor: cursor);
                foreach (PixRequest entity in page)
                {
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
        public void QueryIds()
        {
            List<PixRequest> pixRequests = PixRequest.Query(limit: 10).ToList();
            List<string> pixRequestsIdsExpected = new List<string>();
            Assert.Equal(10, pixRequests.Count);
            Assert.True(pixRequests.First().ID != pixRequests.Last().ID);
            foreach (PixRequest transaction in pixRequests)
            {
                Assert.NotNull(transaction.ID);
                pixRequestsIdsExpected.Add(transaction.ID);
            }

            List<PixRequest> pixRequestsResult = PixRequest.Query(limit:10, ids: pixRequestsIdsExpected).ToList();
            List<string> pixRequestsIdsResult = new List<string>();
            Assert.Equal(10, pixRequests.Count);
            Assert.True(pixRequests.First().ID != pixRequests.Last().ID);
            foreach (PixRequest transaction in pixRequestsResult)
            {
                Assert.NotNull(transaction.ID);
                pixRequestsIdsResult.Add(transaction.ID);
            }

            pixRequestsIdsExpected.Sort();
            pixRequestsIdsResult.Sort();
            Assert.Equal(pixRequestsIdsExpected, pixRequestsIdsResult);
        }
        
        [Fact]
        public void QueryParams()
        {
            List<PixRequest> pixRequests = PixRequest.Query(
                fields: new List<string> {"amount", "id"},
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: "success",
                tags: new List<string> {"iron", "bank"},
                ids: new List<string> {"1", "2"},
                externalIds: new List<string> {"1", "2"},
                endToEndIds: new List<string> { "98236508236008632", "2352352352353" }
            ).ToList();
            Assert.True(pixRequests.Count == 0);
        }
        
        [Fact]
        public void PageParams()
        {
            List<PixRequest> page;
            string cursor = null;
            (page, cursor) = PixRequest.Page(
                cursor: null,
                fields: new List<string> {"amount", "id"},
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: "success",
                tags: new List<string> {"iron", "bank"},
                ids: new List<string> {"1", "2"},
                externalIds: new List<string> {"1", "2"},
                endToEndIds: new List<string> { "98236508236008632", "2352352352353" }
            );
            Assert.True(page.Count == 0);
        }

        public readonly string Content = "{\"receiverBranchCode\": \"0001\", \"cashierBankCode\": \"\", \"senderTaxId\": \"20.018.183/0001-80\", \"senderName\": \"Stark Bank S.A. - Instituicao de Pagamento\", \"id\": \"4508348862955520\", \"senderAccountType\": \"payment\", \"fee\": 0, \"receiverName\": \"Cora\", \"cashierType\": \"\", \"externalId\": \"\", \"method\": \"manual\", \"status\": \"processing\", \"updated\": \"2022-02-16T17:23:53.980250+00:00\", \"description\": \"\", \"tags\": [], \"receiverKeyId\": \"\", \"cashAmount\": 0, \"senderBankCode\": \"20018183\", \"senderBranchCode\": \"0001\", \"bankCode\": \"34052649\", \"senderAccountNumber\": \"5647143184367616\", \"receiverAccountNumber\": \"5692908409716736\", \"initiatorTaxId\": \"\", \"receiverTaxId\": \"34.052.649/0001-78\", \"created\": \"2022-02-16T17:23:53.980238+00:00\", \"flow\": \"in\", \"endToEndId\": \"E20018183202202161723Y4cqxlfLFcm\", \"amount\": 1, \"receiverAccountType\": \"checking\", \"reconciliationId\": \"\", \"receiverBankCode\": \"34052649\"}";
        public readonly string GoodSignature = "MEUCIQC7FVhXdripx/aXg5yNLxmNoZlehpyvX3QYDXJ8o02X2QIgVwKfJKuIS5RDq50NC/+55h/7VccDkV1vm8Q/7jNu0VM=";
        public readonly string BadSignature = "MEUCIQDOpo1j+V40DNZK2URL2786UQK/8mDXon9ayEd8U0/l7AIgYXtIZJBTs8zCRR3vmted6Ehz/qfw1GRut/eYyvf1yOk=";

        [Fact]
        public void ParseWithRightSignature()
        {
            PixRequest parsedPixRequest = PixRequest.Parse(Content, GoodSignature);
            Assert.NotNull(parsedPixRequest.ID);
            TestUtils.Log(parsedPixRequest);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try {
                PixRequest parsedPixRequest = PixRequest.Parse(Content, BadSignature);
            } catch (StarkInfra.Error.InvalidSignatureError e) {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void ParseWithMalformedSignature()
        {
            try
            {
                PixRequest parsedPixRequest = PixRequest.Parse(Content, BadSignature);
            }
            catch (StarkInfra.Error.InvalidSignatureError e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }
        
        internal static PixRequest Example(bool schedule = true)
        {
            return new PixRequest(
                amount: new Random().Next(1, 1000),
                externalId: Convert.ToString(new Random().Next(1, 999999999)),
                senderName: "Arya",
                senderTaxId: "01234567890",
                senderBranchCode: "0000",
                senderAccountNumber: "00000-0",
                senderAccountType: "checking",
                receiverName: "maria",
                receiverTaxId: "01234567890",
                receiverBankCode: "0001",
                receiverAccountNumber: "00000-1",
                receiverBranchCode: "0001",
                receiverAccountType: "checking",
                endToEndId: EndToEndId.Create(bankCode: Environment.GetEnvironmentVariable("SANDBOX_BANKCODE"))
            );
        }
    }
}
