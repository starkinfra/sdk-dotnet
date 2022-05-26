using StarkInfra;
using Xunit;
using System.Collections.Generic;
using System;

namespace StarkInfraTests
{
    public class IssuingAuthorizationTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        string content = "{\"acquirerId\": \"236090\", \"amount\": 100, \"cardId\": \"5671893688385536\", \"cardTags\": [], \"endToEndId\": \"2fa7ef9f-b889-4bae-ac02-16749c04a3b6\", \"holderId\": \"5917814565109760\", \"holderTags\": [], \"isPartialAllowed\": false, \"issuerAmount\": 100, \"issuerCurrencyCode\": \"BRL\", \"merchantAmount\": 100, \"merchantCategoryCode\": \"bookStores\", \"merchantCountryCode\": \"BRA\", \"merchantCurrencyCode\": \"BRL\", \"merchantFee\": 0, \"merchantId\": \"204933612653639\", \"merchantName\": \"COMPANY 123\", \"methodCode\": \"token\", \"purpose\": \"purchase\", \"score\": null, \"tax\": 0, \"walletId\": \"\"}";
        string validSignature = "MEUCIBxymWEpit50lDqFKFHYOgyyqvE5kiHERi0ZM6cJpcvmAiEA2wwIkxcsuexh9BjcyAbZxprpRUyjcZJ2vBAjdd7o28Q=";
        string invalidSignature = "MEUCIQDOpo1j+V40DNZK2URL2786UQK/8mDXon9ayEd8U0/l7AIgYXtIZJBTs8zCRR3vmted6Ehz/qfw1GRut/eYyvf1yOk=";

        [Fact]
        public void test_success()
        {
            IssuingAuthorization authorization = IssuingAuthorization.ParseContent(content, validSignature);
            TestUtils.Log(authorization);
        }

        [Fact]
        public void test_failed()
        {
            try
            {
                IssuingAuthorization authorization = IssuingAuthorization.ParseContent(content, invalidSignature);
            }
            catch (StarkInfra.Error.InvalidSignatureError e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void sendResponse()
        {
            string response = IssuingAuthorization.Response(status: "accepted", amount: 10000, tags: new List<string> { "my-purchase-id/123", "123" });
            TestUtils.Log(response);
        }
    }
}
