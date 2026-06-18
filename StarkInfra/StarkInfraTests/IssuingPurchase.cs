using Xunit;
using System;
using StarkInfra;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class IssuingPurchaseTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<IssuingPurchase> purchases = IssuingPurchase.Query(limit: 1).ToList();
            IssuingPurchase purchase = purchases.First();
            IssuingPurchase getPurchase = IssuingPurchase.Get(purchase.ID);
            Assert.NotNull(getPurchase);
            TestUtils.Log(getPurchase);
        }

        [Fact]
        public void Query()
        {
            List<IssuingPurchase> purchases = IssuingPurchase.Query(limit: 3, status: "canceled").ToList();
            Assert.True(purchases.Count <= 3);
            foreach (IssuingPurchase purchase in purchases)
            {
                TestUtils.Log(purchase);
                Assert.NotNull(purchase.ID);
                Assert.Equal("canceled", purchase.Status);
            }
        }

        [Fact]
        public void QueryFieldGaps()
        {
            List<IssuingPurchase> purchases = IssuingPurchase.Query(limit: 1).ToList();
            Assert.True(purchases.Count <= 1);
            foreach (IssuingPurchase purchase in purchases)
            {
                TestUtils.Log(purchase);
                Assert.NotNull(purchase.ID);
                string productID = purchase.ProductID;
                string merchantCategoryType = purchase.MerchantCategoryType;
                int? installmentCount = purchase.InstallmentCount;
                string description = purchase.Description;
                string holderID = purchase.HolderID;
                TestUtils.Log(productID);
                TestUtils.Log(merchantCategoryType);
                TestUtils.Log(installmentCount);
                TestUtils.Log(description);
                TestUtils.Log(holderID);
            }
        }

        [Fact]
        public void ConstructorExposesNewFields()
        {
            IssuingPurchase purchase = new IssuingPurchase(
                productID: "53810200",
                installmentCount: 12,
                merchantCategoryType: "food",
                description: "Office Supplies",
                holderID: "5917814565109760"
            );
            Assert.Equal("53810200", purchase.ProductID);
            Assert.Equal(12, purchase.InstallmentCount);
            Assert.Equal("food", purchase.MerchantCategoryType);
            Assert.Equal("Office Supplies", purchase.Description);
            Assert.Equal("5917814565109760", purchase.HolderID);
            TestUtils.Log(purchase);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingPurchase> page;
            string cursor = null;
            for (int i = 0; i < 3; i++)
            {
                (page, cursor) = IssuingPurchase.Page(limit: 1, cursor: cursor);
                foreach (IssuingPurchase entity in page)
                {
                    TestUtils.Log(entity);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 3);
        }

        public readonly string Content = "{\"acquirerId\": \"236090\", \"amount\": 100, \"cardId\": \"5671893688385536\", \"cardTags\": [], \"endToEndId\": \"2fa7ef9f-b889-4bae-ac02-16749c04a3b6\", \"holderId\": \"5917814565109760\", \"holderTags\": [], \"isPartialAllowed\": false, \"issuerAmount\": 100, \"issuerCurrencyCode\": \"BRL\", \"merchantAmount\": 100, \"merchantCategoryCode\": \"bookStores\", \"merchantCountryCode\": \"BRA\", \"merchantCurrencyCode\": \"BRL\", \"merchantFee\": 0, \"merchantId\": \"204933612653639\", \"merchantName\": \"COMPANY 123\", \"methodCode\": \"token\", \"purpose\": \"purchase\", \"score\": null, \"tax\": 0, \"walletId\": \"\"}";
        public readonly string GoodSignature = "MEUCIBxymWEpit50lDqFKFHYOgyyqvE5kiHERi0ZM6cJpcvmAiEA2wwIkxcsuexh9BjcyAbZxprpRUyjcZJ2vBAjdd7o28Q=";
        public readonly string BadSignature = "MEUCIQDOpo1j+V40DNZK2URL2786UQK/8mDXon9ayEd8U0/l7AIgYXtIZJBTs8zCRR3vmted6Ehz/qfw1GRut/eYyvf1yOk=";

        [Fact]
        public void ParseWithRightSignature()
        {
            IssuingPurchase parsedIssuingPurchase = IssuingPurchase.Parse(Content, GoodSignature);
            TestUtils.Log(parsedIssuingPurchase);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try {
                IssuingPurchase parsedIssuingPurchase = IssuingPurchase.Parse(Content, BadSignature);
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
                IssuingPurchase parsedIssuingPurchase = IssuingPurchase.Parse(Content, "Something is definitely wrong");
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
            string response = IssuingPurchase.Response(status: "accepted");
            TestUtils.Log(response);
        }
    }
}
