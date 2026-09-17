using Xunit;
using System;
using StarkInfra;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingTokenActivationTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void ParseWithRightSignature()
        {
            IssuingTokenActivation activation = IssuingTokenActivation.Parse(Content, GoodSignature);
            Assert.Equal("5189831499972623", activation.CardID);
            Assert.Equal("5585821789122165", activation.TokenID);
            Assert.Equal("text", activation.ActivationMethod["type"]);
            TestUtils.Log(activation);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try
            {
                IssuingTokenActivation activation = IssuingTokenActivation.Parse(Content, BadSignature);
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
                IssuingTokenActivation activation = IssuingTokenActivation.Parse(Content, "Something is definitely wrong");
            }
            catch (StarkCore.Error.InvalidSignatureError e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        public readonly string Content = "{\"activationMethod\": {\"type\": \"text\", \"value\": \"** *****-5678\"}, \"tokenId\": \"5585821789122165\", \"tags\": [\"token\", \"user/1234\"], \"cardId\": \"5189831499972623\"}";
        public readonly string GoodSignature = "MEUCIAxn0FmsPWI4r3Y7Nq8xFNQHYZgo0QAGDQ4/7CajKoVuAiEA09kXWrPMhsw4JbgC3pmNccCWr+hidfop/KsSNqza0yE=";
        public readonly string BadSignature = "MEUCIQDOpo1j+V40DNZK2URL2786UQK/8mDXon9ayEd8U0/l7AIgYXtIZJBTs8zCRR3vmted6Ehz/qfw1GRut/eYyvf1yOk=";
    }
}
