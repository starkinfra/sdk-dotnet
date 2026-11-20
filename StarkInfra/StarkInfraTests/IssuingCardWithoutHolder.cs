using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    // The offline tests check the JSON the SDK builds for the API (the HTTP call is a private static field of the core and
    // has no seam to replace). The sandbox tests run against the real API and cancel every card they create.
    public class IssuingCardWithoutHolderTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void HolderFieldsAreNotSentWhenOmitted()
        {
            IssuingCard card = new IssuingCard(productID: "123", type: "physical");

            Dictionary<string, object> sent = StarkCore.Utils.Api.ApiJson(card);

            Assert.False(sent.ContainsKey("holderName"));
            Assert.False(sent.ContainsKey("holderTaxID"));
            Assert.False(sent.ContainsKey("holderTaxId"));
            Assert.False(sent.ContainsKey("holderExternalID"));
            Assert.False(sent.ContainsKey("holderExternalId"));
            Assert.Equal(2, sent.Count);
            Assert.Equal("physical", sent["type"]);
        }

        [Fact]
        public void PositionalHolderArgumentsKeepTheirPositions()
        {
            IssuingCard card = new IssuingCard("Tony Stark", "012.345.678-90", "my-entity/123", "ANTHONY STARK");

            Assert.Equal("Tony Stark", card.HolderName);
            Assert.Equal("012.345.678-90", card.HolderTaxID);
            Assert.Equal("my-entity/123", card.HolderExternalID);
            Assert.Equal("ANTHONY STARK", card.DisplayName);
        }

        // ---- sandbox ----

        private static IssuingProduct FirstProduct()
        {
            return IssuingProduct.Query(limit: 1).FirstOrDefault() ?? throw new Exception("no issuing product in the sandbox Project");
        }

        // A holder can only be linked to a product of its own type: business holders have a CNPJ (14 digits) and individual
        // holders a CPF (11 digits). Existing holders are used so no personal data is created by the test.
        private static IssuingHolder FindExistingHolderForProduct(IssuingProduct product)
        {
            int digits = product.HolderType == "business" ? 14 : product.HolderType == "individual" ? 11 : throw new Exception("unknown holder type of the sandbox product");
            return IssuingHolder.Query(status: "active", limit: 100)
                .FirstOrDefault(holder => holder.TaxID.Count(char.IsDigit) == digits);
        }

        private static List<IssuingCard> CardsWithoutHolder(int n, string type, string productID)
        {
            return Enumerable.Range(0, n).Select(_ => new IssuingCard(productID: productID, type: type, tags: new List<string> { "sdk-test" })).ToList();
        }

        // A failed cancel must not hide the failure of the test (a leftover card is also reported by the sandbox-test plan).
        private static void CancelAll(IEnumerable<IssuingCard> cards)
        {
            foreach (IssuingCard card in cards)
            {
                try
                {
                    IssuingCard.Cancel(card.ID);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("could not cancel card " + card.ID + " (" + exception.GetType().Name + ")");
                }
            }
        }

        private static Dictionary<string, object> HolderFields(IssuingHolder holder)
        {
            return new Dictionary<string, object>
            {
                { "holderName", holder.Name },
                { "holderTaxID", holder.TaxID },
                { "holderExternalID", holder.ExternalID }
            };
        }

        // The API rejection reaches the caller untouched, with the error code of the API.
        private static void AssertRejected(string code, Action call)
        {
            StarkCore.Error.InputErrors errors = Assert.Throws<StarkCore.Error.InputErrors>(call);
            Assert.Contains(code, errors.Errors.Select(error => error.Code));
        }

        // Creates pending physical cards without holder, runs the test with them and always cancels them.
        private static void WithPendingCards(int n, Action<List<IssuingCard>, IssuingHolder> test)
        {
            IssuingProduct product = FirstProduct();
            IssuingHolder holder = FindExistingHolderForProduct(product);
            Assert.True(holder != null, "the sandbox needs an existing active holder compatible with the product");
            List<IssuingCard> cards = IssuingCard.Create(CardsWithoutHolder(n, "physical", product.ID));
            try
            {
                test(cards, holder);
            }
            finally
            {
                CancelAll(cards);
            }
        }

        [Fact]
        public void PhysicalCardWithoutHolderIsCreatedPending()
        {
            List<IssuingCard> cards = IssuingCard.Create(CardsWithoutHolder(1, "physical", FirstProduct().ID));
            try
            {
                Assert.Single(cards);
                Assert.Equal("pending", cards[0].Status);
                Assert.Equal("physical", cards[0].Type);
                Assert.True(string.IsNullOrEmpty(cards[0].HolderID));
                Assert.True(string.IsNullOrEmpty(cards[0].HolderName));
            }
            finally
            {
                CancelAll(cards);
            }
        }

        [Fact]
        public void ApiRejectsVirtualCardWithoutHolder()
        {
            List<IssuingCard> created = new List<IssuingCard>();
            try
            {
                AssertRejected("invalidHolder", () => created = IssuingCard.Create(CardsWithoutHolder(1, "virtual", FirstProduct().ID)));
            }
            finally
            {
                CancelAll(created);
            }
        }

        [Fact]
        public void ApiRejectsIncompleteHolderFieldsOnCreate()
        {
            List<IssuingCard> created = new List<IssuingCard>();
            try
            {
                IssuingCard card = new IssuingCard(holderName: "Tony Stark", productID: FirstProduct().ID, type: "physical", tags: new List<string> { "sdk-test" });
                AssertRejected("invalidHolder", () => created = IssuingCard.Create(new List<IssuingCard> { card }));
            }
            finally
            {
                CancelAll(created);
            }
        }

        [Fact]
        public void HolderIsAssignedToAPendingCard()
        {
            WithPendingCards(1, (cards, holder) =>
            {
                IssuingCard card = IssuingCard.Update(cards[0].ID, HolderFields(holder));

                Assert.Equal("pending", card.Status);
                Assert.Equal(holder.ID, card.HolderID);
            });
        }

        [Fact]
        public void HolderIsAssignedAndCardActivatedInTheSameCall()
        {
            WithPendingCards(1, (cards, holder) =>
            {
                Dictionary<string, object> patch = HolderFields(holder);
                patch["status"] = "active";
                patch["pin"] = "1234";

                IssuingCard card = IssuingCard.Update(cards[0].ID, patch);

                Assert.Equal("active", card.Status);
                Assert.Equal(holder.ID, card.HolderID);
            });
        }

        [Fact]
        public void ApiRejectsIncompleteHolderFieldsOnUpdate()
        {
            WithPendingCards(1, (cards, holder) =>
                AssertRejected("invalidHolder", () => IssuingCard.Update(cards[0].ID, new Dictionary<string, object> { { "holderName", holder.Name } })));
        }

        [Fact]
        public void ApiRejectsActivationWithoutHolder()
        {
            WithPendingCards(1, (cards, holder) =>
                AssertRejected("invalidHolder", () => IssuingCard.Update(cards[0].ID, new Dictionary<string, object> { { "status", "active" }, { "pin", "1234" } })));
        }

        [Fact]
        public void HolderIsAssignedToSeveralCards()
        {
            WithPendingCards(2, (cards, holder) =>
            {
                List<Dictionary<string, object>> items = cards.Select(card =>
                {
                    Dictionary<string, object> item = HolderFields(holder);
                    item["id"] = card.ID;
                    return item;
                }).ToList();

                List<IssuingCard> updated = IssuingCard.UpdateMulti(items);

                Assert.Equal(cards.Select(card => card.ID).OrderBy(id => id), updated.Select(card => card.ID).OrderBy(id => id));
                Assert.All(updated, card => Assert.Equal(holder.ID, card.HolderID));
            });
        }

        [Fact]
        public void ApiRejectsTheWholeListWhenOneItemIsInvalid()
        {
            WithPendingCards(2, (cards, holder) =>
            {
                Dictionary<string, object> valid = HolderFields(holder);
                valid["id"] = cards[0].ID;
                Dictionary<string, object> invalid = HolderFields(holder);
                invalid["id"] = "0000000000000001";

                AssertRejected("invalidCard", () => IssuingCard.UpdateMulti(new List<Dictionary<string, object>> { valid, invalid }));

                Assert.True(string.IsNullOrEmpty(IssuingCard.Get(cards[0].ID).HolderID));
            });
        }

        [Fact]
        public void ApiRejectsIncompleteHolderFieldsOnUpdateMulti()
        {
            WithPendingCards(1, (cards, holder) =>
                AssertRejected("invalidHolder", () => IssuingCard.UpdateMulti(new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object> { { "id", cards[0].ID }, { "holderName", holder.Name } }
                })));
        }
    }
}
