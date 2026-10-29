using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace StarkInfraTests
{
    public class IssuingTokenTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 1).ToList();
            IssuingToken token = tokens.First();
            IssuingToken getToken = IssuingToken.Get(token.ID);
            Assert.NotNull(getToken);
            Assert.Equal(getToken.ID, token.ID);
            TestUtils.Log(getToken);
        }

        [Fact]
        public void Query()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 101).ToList();
            Assert.True(tokens.Count <= 101);
            foreach (IssuingToken token in tokens)
            {
                TestUtils.Log(token);
                Assert.NotNull(token.ID);
            }
        }

        [Fact]
        public void QueryIds()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 10).ToList();
            List<string> tokensIdsExpected = new List<string>();
            foreach (IssuingToken token in tokens)
            {
                Assert.NotNull(token.ID);
                tokensIdsExpected.Add(token.ID);
            }

            List<IssuingToken> tokensResult = IssuingToken.Query(limit: 10, ids: tokensIdsExpected).ToList();
            List<string> tokensIdsResult = new List<string>();
            foreach (IssuingToken token in tokensResult)
            {
                Assert.NotNull(token.ID);
                tokensIdsResult.Add(token.ID);
            }

            tokensIdsExpected.Sort();
            tokensIdsResult.Sort();
            Assert.Equal(tokensIdsExpected, tokensIdsResult);
        }

        [Fact]
        public void QueryParams()
        {
            List<IssuingToken> tokens = IssuingToken.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "active" },
                cardIds: new List<string> { "1", "2" },
                tags: new List<string> { "travel", "food" },
                ids: new List<string> { "1", "2" },
                externalIds: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(tokens.Count == 0);
        }

        [Fact]
        public void Page()
        {
            List<IssuingToken> tokens = new List<IssuingToken>();
            List<IssuingToken> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingToken.Page(limit: 5, cursor: cursor);
                foreach (IssuingToken entity in page)
                {
                    Assert.DoesNotContain(entity, tokens);
                    tokens.Add(entity);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(tokens.Count <= 10);
        }

        [Fact]
        public void PageParams()
        {
            List<IssuingToken> page;
            string cursor = null;
            (page, cursor) = IssuingToken.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "active" },
                cardIds: new List<string> { "1", "2" },
                tags: new List<string> { "travel", "food" },
                ids: new List<string> { "1", "2" },
                externalIds: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }

        [Fact]
        public void GetAndUpdate()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 1).ToList();
            IssuingToken token = tokens.First();
            Assert.NotNull(token.ID);
            IssuingToken updatedToken = IssuingToken.Update(id: token.ID, status: "blocked");
            Assert.Equal("blocked", updatedToken.Status);
            TestUtils.Log(updatedToken);
        }

        [Fact]
        public void GetAndCancel()
        {
            List<IssuingToken> tokens = IssuingToken.Query(limit: 1).ToList();
            IssuingToken token = tokens.First();
            Assert.NotNull(token.ID);
            IssuingToken getToken = IssuingToken.Get(token.ID);
            Assert.Equal(getToken.ID, token.ID);
            IssuingToken canceledToken = IssuingToken.Cancel(id: token.ID);
            Assert.Equal(canceledToken.ID, token.ID);
            TestUtils.Log(canceledToken);
        }

        [Fact]
        public void ParseWithRightSignature()
        {
            IssuingToken token = IssuingToken.Parse(Content, GoodSignature);
            Assert.Equal("5189831499972623", token.CardID);
            Assert.Equal("google", token.WalletID);
            Assert.Equal("Google Pay", token.WalletName);
            Assert.Equal("12345678901", token.MerchantID);
            Assert.Equal("manual", token.MethodCode);
            Assert.Equal("Phone", token.DeviceType);
            Assert.Equal("Android", token.DeviceOsName);
            Assert.Equal("1b24f24a24ba98e27d43e345b532a245e4723d7a9c4f624e", token.WalletInstanceID);
            TestUtils.Log(token);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try
            {
                IssuingToken token = IssuingToken.Parse(Content, BadSignature);
            }
            catch (StarkCore.Error.InvalidSignatureError e)
            {
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
                IssuingToken token = IssuingToken.Parse(Content, "Something is definitely wrong");
            }
            catch (StarkCore.Error.InvalidSignatureError e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void ResponseAuthorizationApproved()
        {
            string response = IssuingToken.ResponseAuthorization(
                status: "approved",
                activationMethods: new List<Dictionary<string, object>> {
                    new Dictionary<string, object> { { "type", "app" }, { "value", "com.subissuer.android" } },
                    new Dictionary<string, object> { { "type", "text" }, { "value", "** *****-5678" } }
                },
                designId: "4584031664472031",
                tags: new List<string> { "tony", "stark" }
            );
            JObject authorization = (JObject)JObject.Parse(response)["authorization"];
            Assert.Equal("approved", (string)authorization["status"]);
            Assert.Equal("", (string)authorization["reason"]);
            Assert.Equal("4584031664472031", (string)authorization["designId"]);
            Assert.Equal(2, authorization["activationMethods"].Count());
            Assert.Equal("app", (string)authorization["activationMethods"][0]["type"]);
            Assert.Equal(new List<string> { "tony", "stark" }, authorization["tags"].ToObject<List<string>>());
        }

        [Fact]
        public void ResponseAuthorizationDenied()
        {
            string response = IssuingToken.ResponseAuthorization(
                status: "denied",
                reason: "other",
                tags: new List<string> { "tony", "stark" }
            );
            JObject authorization = (JObject)JObject.Parse(response)["authorization"];
            Assert.Equal("denied", (string)authorization["status"]);
            Assert.Equal("other", (string)authorization["reason"]);
            Assert.Equal(new List<string> { "tony", "stark" }, authorization["tags"].ToObject<List<string>>());
        }

        [Fact]
        public void ResponseActivationApproved()
        {
            string response = IssuingToken.ResponseActivation(
                status: "approved",
                tags: new List<string> { "tony", "stark" }
            );
            JObject authorization = (JObject)JObject.Parse(response)["authorization"];
            Assert.Equal("approved", (string)authorization["status"]);
            Assert.Equal("", (string)authorization["reason"]);
            Assert.Equal(new List<string> { "tony", "stark" }, authorization["tags"].ToObject<List<string>>());
        }

        [Fact]
        public void ResponseActivationDenied()
        {
            string response = IssuingToken.ResponseActivation(
                status: "denied",
                reason: "other"
            );
            JObject authorization = (JObject)JObject.Parse(response)["authorization"];
            Assert.Equal("denied", (string)authorization["status"]);
            Assert.Equal("other", (string)authorization["reason"]);
        }

        public readonly string Content = "{\"deviceName\": \"My phone\", \"methodCode\": \"manual\", \"walletName\": \"Google Pay\", \"activationCode\": \"\", \"deviceSerialNumber\": \"2F6D63\", \"deviceImei\": \"352099001761481\", \"deviceType\": \"Phone\", \"walletInstanceId\": \"1b24f24a24ba98e27d43e345b532a245e4723d7a9c4f624e\", \"deviceOsVersion\": \"4.4.4\", \"cardId\": \"5189831499972623\", \"deviceOsName\": \"Android\", \"merchantId\": \"12345678901\", \"walletId\": \"google\"}";
        public readonly string GoodSignature = "MEYCIQC4XbhjxEp9VhowLeg9JbSOo94FCRWE9GI7l7OuHh0bUwIhAJBuLDl5DAT9L4iMI0qYQ+PVmBIG5scxxvkWSsoWmwi4";
        public readonly string BadSignature = "MEUCIQDOpo1j+V40DNZK2URL2786UQK/8mDXon9ayEd8U0/l7AIgYXtIZJBTs8zCRR3vmted6Ehz/qfw1GRut/eYyvf1yOk=";
    }
}
