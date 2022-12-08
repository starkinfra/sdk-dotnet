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
            List<PixRequest> requests = PixRequest.Create(new List<PixRequest>() { Example() });
            PixRequest request = requests.First();
            Assert.NotNull(requests.First().ID);
            PixRequest getPixRequest = PixRequest.Get(id: request.ID);
            Assert.Equal(getPixRequest.ID, request.ID);
            TestUtils.Log(request);
        }

        [Fact]
        public void Query()
        {
            List<PixRequest> requests = PixRequest.Query(limit: 101, status: new List<string> { "success" }).ToList();
            Assert.True(requests.Count <= 101);
            Assert.True(requests.First().ID != requests.Last().ID);
            foreach (PixRequest request in requests)
            {
                TestUtils.Log(request);
                Assert.NotNull(request.ID);
                Assert.Equal("success", request.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<PixRequest> requests = new List<PixRequest>();
            List<PixRequest> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixRequest.Page(limit: 5, cursor: cursor);
                foreach (PixRequest entity in page)
                {
                    Assert.DoesNotContain(entity, requests);
                    requests.Add(entity);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(requests.Count == 10);
        }

        [Fact]
        public void QueryIds()
        {
            List<PixRequest> requests = PixRequest.Query(limit: 10).ToList();
            List<string> requestsIdsExpected = new List<string>();
            Assert.Equal(10, requests.Count);
            Assert.True(requests.First().ID != requests.Last().ID);
            foreach (PixRequest transaction in requests)
            {
                Assert.NotNull(transaction.ID);
                requestsIdsExpected.Add(transaction.ID);
            }

            List<PixRequest> requestsResult = PixRequest.Query(limit:10, ids: requestsIdsExpected).ToList();
            List<string> requestsIdsResult = new List<string>();
            Assert.Equal(10, requests.Count);
            Assert.True(requests.First().ID != requests.Last().ID);
            foreach (PixRequest transaction in requestsResult)
            {
                Assert.NotNull(transaction.ID);
                requestsIdsResult.Add(transaction.ID);
            }

            requestsIdsExpected.Sort();
            requestsIdsResult.Sort();
            Assert.Equal(requestsIdsExpected, requestsIdsResult);
        }
        
        [Fact]
        public void QueryParams()
        {
            List<PixRequest> requests = PixRequest.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "success" },
                tags: new List<string> {"iron", "bank"},
                ids: new List<string> {"1", "2"},
                externalIds: new List<string> {"1", "2"},
                endToEndIds: new List<string> { "98236508236008632", "2352352352353" }
            ).ToList();
            Assert.True(requests.Count == 0);
        }
        
        [Fact]
        public void PageParams()
        {
            List<PixRequest> page;
            string cursor = null;
            (page, cursor) = PixRequest.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "success" },
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

        [Fact]
        public void SendResponse()
        {
            string response = PixRequest.Response(status: "accepted");
            TestUtils.Log(response);
        }

        internal static PixRequest Example()
        {
            return new PixRequest(
                amount: new Random().Next(1, 1000),
                externalID: Convert.ToString(new Random().Next(1, 999999999)),
                senderName: "Arya",
                senderTaxID: "01234567890",
                senderBranchCode: "0000",
                senderAccountNumber: "00000-0",
                senderAccountType: "checking",
                receiverName: "maria",
                receiverTaxID: "01234567890",
                receiverBankCode: "20018183",
                receiverBranchCode: "0001",
                receiverAccountNumber: "00000-1",
                receiverAccountType: "checking",
                endToEndID: "E35547753205" + Convert.ToString(new Random().Next(111111111, 999999999)) + "u34sDGd19l2"
            );
        }
    }
}
