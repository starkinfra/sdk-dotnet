using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingTokenRequestTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Create()
        {
            List<IssuingCard> cards = IssuingCard.Query(limit: 1, status: new List<string> { "active" }).ToList();
            IssuingCard card = cards.First();
            IssuingTokenRequest request = IssuingTokenRequest.Create(Example(card.ID));
            Assert.NotNull(request.Content);
            Assert.NotNull(request.Signature);
            TestUtils.Log(request);
        }

        internal static IssuingTokenRequest Example(string cardID)
        {
            return new IssuingTokenRequest(
                cardId: cardID,
                walletId: "google",
                methodCode: "app"
            );
        }
    }
}
