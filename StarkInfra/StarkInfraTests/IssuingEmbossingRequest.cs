using StarkInfra;
using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingEmbossingRequestTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingEmbossingRequest> requests = IssuingEmbossingRequest.Query(before: new DateTime(2022, 03, 03)).ToList();
            foreach (IssuingEmbossingRequest request in requests)
            {
                TestUtils.Log(request);
                Assert.NotNull(request.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingEmbossingRequest> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingEmbossingRequest.Page(limit: 2, cursor: cursor);
                foreach (IssuingEmbossingRequest entity in page)
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
            Assert.True(ids.Count == 4);
        }
        
        [Fact]
        public void QueryGet()
        {
            List<IssuingEmbossingRequest> requests = IssuingEmbossingRequest.Query(limit: 4).ToList();
            Assert.True(requests.Count <= 4);
            foreach (IssuingEmbossingRequest request in requests)
            {
                IssuingEmbossingRequest getRequest = IssuingEmbossingRequest.Get(request.ID);
                Assert.NotNull(request.ID);
                Assert.Equal(getRequest.ID, request.ID);
                TestUtils.Log(getRequest);
            }
        }
        
        [Fact]
        public void Create()
        {
            List<IssuingEmbossingRequest> requests = IssuingEmbossingRequest.Create(new List<IssuingEmbossingRequest>() { Example() });
            IssuingEmbossingRequest request = requests.First();
            TestUtils.Log(request);
            Assert.NotNull(request.ID);
        }
        
        internal static IssuingEmbossingRequest Example()
        {
            IssuingHolder holder = IssuingHolder.Query(limit: 1).ToList().First();

            string generatedHolderName = holder.Name;
            string generatedHolderTaxID = holder.TaxID;
            string generatedHolderExternalID = Convert.ToString(new Random().Next(1, 999999999));
            
            IssuingCard issuingcard = new IssuingCard(
                city: "Sao Paulo",
                displayName: "ANTHONY STARK",
                district: "Bela Vista",
                holderExternalID: generatedHolderExternalID,
                holderName: generatedHolderName,
                holderTaxID: generatedHolderTaxID,
                rules: new List<IssuingRule>{
                    new IssuingRule(
                        name: "general",
                        interval: "week",
                        amount: 100000,
                        currencyCode: "USD"
                    )
                },
                productID: "52233227",
                type: "physical",
                stateCode: "SP",
                streetLine1: "Av. Paulista, 200",
                streetLine2: "Apto. 123",
                tags: new List<string> {
                    "travel",
                    "food"
                },
                zipCode: "01311-200"
            );
            
            List<IssuingCard> cards = IssuingCard.Create(new List<IssuingCard>() { issuingcard });
            IssuingCard card = cards.First();
            string cardID = card.ID;
            
            return new IssuingEmbossingRequest(
                cardID: cardID, 
                cardDesignID: "5648359658356736", 
                displayName1: "teste", 
                envelopeDesignID: "5747368922185728", 
                shippingCity: "Sao Paulo", 
                shippingCountryCode: "BRA", 
                shippingDistrict: "Bela Vista", 
                shippingService: "loggi", 
                shippingStateCode: "SP", 
                shippingStreetLine1: "teste", 
                shippingStreetLine2: "teste", 
                shippingTrackingNumber: "teste", 
                shippingZipCode: "12345-678",
                embosserID: "5746980898734080"
            );
        }
    }
}
