using StarkInfra;
using Xunit;
using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingCardTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingCard> cards = IssuingCard.Query(limit: 4, status: new List<string> { "active" }, expand: new List<string> { "rules" } ).ToList();
            Assert.True(cards.Count <= 4);
            foreach (IssuingCard card in cards)
            {
                TestUtils.Log(card);
                Assert.NotNull(card.ID);
                Assert.Equal("active", card.Status);

                foreach(IssuingRule rule in card.Rules)
                {
                    TestUtils.Log(rule);
                }
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingCard> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingCard.Page(limit: 2, cursor: cursor);
                foreach (IssuingCard entity in page)
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
        public void Update()
        {
            List<IssuingCard> cards = IssuingCard.Query(limit: 2, status: new List<string> { "active" }).ToList();
            Assert.True(2 >= cards.Count);
            Dictionary<string, object> patchData = new Dictionary<string, object>
            {
                {"status", "blocked"}
            };
            foreach (IssuingCard card in cards)
            {
                TestUtils.Log(card);
                Assert.NotNull(card.ID);
                Assert.Equal("active", card.Status);
                IssuingCard updatedCard = IssuingCard.Update(id: card.ID, patchData: patchData);
                TestUtils.Log(updatedCard);
                Assert.Equal("blocked", updatedCard.Status);
            }
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"expand", new List<string> {"rules"}}
            };
            List<IssuingCard> cards = IssuingCard.Create(new List<IssuingCard>() { Example() }, parameters: parameters);
            IssuingCard card = cards.First();
            TestUtils.Log(card);
            Assert.NotNull(card.ID);
            IssuingCard getCard = IssuingCard.Get(card.ID, parameters: parameters);
            Assert.Equal(getCard.ID, card.ID);
            TestUtils.Log(getCard);
            IssuingCard canceledCard = IssuingCard.Cancel(id: card.ID);
            Assert.Equal(canceledCard.ID, card.ID);
            Assert.Equal(canceledCard.Status, "canceled");
            TestUtils.Log(canceledCard);
        }
        
        internal static IssuingCard Example()
        {
            IssuingHolder holder = IssuingHolder.Query(limit: 1, status: "blocked").ToList().First();

            string generatedHolderName = holder.Name;
            string generatedHolderTaxID = holder.TaxID;
            string generatedHolderExternalID = Convert.ToString(new Random().Next(1, 999999999));

            return new IssuingCard(
                city: "Sao Paulo",
                displayName: "ANTHONY STARK",
                productID: "52233227",
                district: "Bela Vista",
                holderExternalID: generatedHolderExternalID,
                holderName: generatedHolderName,
                holderTaxID: "586.589.770-56",
                rules: new List<StarkInfra.IssuingRule>{
                    new StarkInfra.IssuingRule(
                        name: "general",
                        interval: "week",
                        amount: 100000,
                        currencyCode: "USD"
                    )
                },
                stateCode: "SP",
                streetLine1: "Av. Paulista, 200",
                streetLine2: "Apto. 123",
                tags: new List<string> {
                    "travel",
                    "food"
                },
                zipCode: "01311-200"
            );
        }
    }
}
