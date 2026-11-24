using Xunit;
using StarkInfra;
using System;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace StarkInfraTests
{
    // Offline: no network and no credentials. Serialization goes through the same
    // StarkCore.Utils.Api.ApiJson call that Rest.Post uses to build the request body.
    public class CreditNoteTemplateReplacementsTest
    {
        private static readonly Dictionary<string, object> Replacements = new Dictionary<string, object> {
            { "guarantorName", "Jon Snow" },
            { "cpfCnpjAvalista", "336.959.235-55" },
            { "guarantorAddress", "Av. Paulista, 200 - Bela Vista - Sao Paulo, SP" },
            { "emptyValue", null }
        };

        [Fact]
        public void ObjectAndDictionarySendTheSamePayload()
        {
            TemplateReplacements typed = new TemplateReplacements(Replacements);

            string fromObject = JsonConvert.SerializeObject(StarkCore.Utils.Api.ApiJson(Build(templateReplacements: typed)));
            string fromDictionary = JsonConvert.SerializeObject(StarkCore.Utils.Api.ApiJson(Build(templateReplacements: Replacements)));

            Assert.Equal(fromDictionary, fromObject);
            Assert.Equal("Jon Snow", (string)JObject.Parse(fromObject)["templateReplacements"]["guarantorName"]);
        }

        [Fact]
        public void ConstructorConvertsDictionaryIntoTemplateReplacements()
        {
            CreditNote note = Build(templateReplacements: Replacements);

            Assert.IsType<TemplateReplacements>(note.TemplateReplacements);
            Assert.Equal(Replacements.Count, note.TemplateReplacements.Count);
            Assert.Equal("Jon Snow", note.TemplateReplacements["guarantorName"]);
        }

        [Fact]
        public void ConstructorKeepsTemplateReplacementsInstance()
        {
            TemplateReplacements typed = new TemplateReplacements(Replacements);

            Assert.Same(typed, Build(templateReplacements: typed).TemplateReplacements);
        }

        [Fact]
        public void ConstructorKeepsNullAsNull()
        {
            Assert.Null(Build(templateReplacements: null).TemplateReplacements);
        }

        [Fact]
        public void ReadsEveryKeyOfResponseIntoTemplateReplacements()
        {
            JObject response = ResponseJson();
            response["templateReplacements"] = ParseJson(
                "{\"guarantorName\": \"Jon Snow\", \"cpfCnpjAvalista\": \"336.959.235-55\", \"emptyValue\": null, \"unknownKey\": \"kept\", \"nested\": {\"innerKey\": 1}}"
            );

            CreditNote note = ParseResponse(response);

            Assert.IsType<TemplateReplacements>(note.TemplateReplacements);
            Assert.Equal(5, note.TemplateReplacements.Count);
            Assert.Equal("kept", note.TemplateReplacements["unknownKey"]);
            Assert.Equal(1L, (long)((JObject)note.TemplateReplacements["nested"])["innerKey"]);
        }

        [Fact]
        public void ReadResponseSerializesBackWithAllKeys()
        {
            JObject response = ResponseJson();
            response["templateReplacements"] = ParseJson("{\"unknownKey\": \"kept\", \"guarantorName\": \"Jon Snow\"}");

            JObject body = RequestBody(ParseResponse(response));

            Assert.Equal("kept", (string)body["templateReplacements"]["unknownKey"]);
            Assert.Equal("Jon Snow", (string)body["templateReplacements"]["guarantorName"]);
        }

        [Fact]
        public void SerializationKeepsCamelCaseKeys()
        {
            CreditNote note = Build(templateReplacements: Replacements);

            JObject body = RequestBody(note);

            JObject sent = (JObject)body["templateReplacements"];
            Assert.NotNull(sent);
            Assert.Equal("Jon Snow", (string)sent["guarantorName"]);
            Assert.Equal("336.959.235-55", (string)sent["cpfCnpjAvalista"]);
            Assert.Equal("Av. Paulista, 200 - Bela Vista - Sao Paulo, SP", (string)sent["guarantorAddress"]);
            Assert.Equal(
                new List<string> { "guarantorName", "cpfCnpjAvalista", "guarantorAddress", "emptyValue" },
                new List<string>(KeyNames(sent))
            );
            Assert.Equal(JTokenType.Null, sent["emptyValue"].Type);
        }

        [Fact]
        public void SerializationDoesNotConvertNestedDateTime()
        {
            DateTime when = new DateTime(2030, 1, 1);
            CreditNote note = Build(templateReplacements: new Dictionary<string, object> { { "signatureDate", when } });

            JObject body = ParseJson(JsonConvert.SerializeObject(StarkCore.Utils.Api.ApiJson(note)));

            // top-level dates get the core's conversion ("2030-01-01"); nested ones are left to Newtonsoft
            Assert.Equal("2030-01-01", (string)body["scheduled"]);
            Assert.Equal(JsonConvert.SerializeObject(when).Trim('"'), (string)body["templateReplacements"]["signatureDate"]);
            Assert.Equal("2030-01-01T00:00:00", (string)body["templateReplacements"]["signatureDate"]);
        }

        [Fact]
        public void SerializationKeepsKeysOfDictionaryInput()
        {
            Dictionary<string, object> note = new Dictionary<string, object> {
                { "name", "Jamie Lannister" },
                { "templateReplacements", Replacements }
            };

            JObject body = JObject.Parse(JsonConvert.SerializeObject(StarkCore.Utils.Api.ApiJson(note)));

            Assert.Equal("Jon Snow", (string)body["templateReplacements"]["guarantorName"]);
            Assert.Equal("336.959.235-55", (string)body["templateReplacements"]["cpfCnpjAvalista"]);
        }

        [Fact]
        public void SerializationOmitsFieldWhenAbsent()
        {
            JObject body = RequestBody(Build(templateReplacements: null));

            Assert.False(body.ContainsKey("templateReplacements"));
            Assert.Equal("Jamie Lannister", (string)body["name"]);
        }

        [Fact]
        public void ReadsFieldAsReceived()
        {
            JObject response = ResponseJson();
            response["templateReplacements"] = ParseJson(
                "{\"guarantorName\": \"Jon Snow\", \"cpfCnpjAvalista\": \"336.959.235-55\", \"emptyValue\": null}"
            );

            CreditNote note = ParseResponse(response);

            Assert.Equal("Jon Snow", note.TemplateReplacements["guarantorName"]);
            Assert.Equal("336.959.235-55", note.TemplateReplacements["cpfCnpjAvalista"]);
            Assert.True(note.TemplateReplacements.ContainsKey("emptyValue"));
            Assert.Null(note.TemplateReplacements["emptyValue"]);
            Assert.Equal(3, note.TemplateReplacements.Count);
        }

        [Fact]
        public void ReadsAbsentFieldAsNull()
        {
            Assert.Null(ParseResponse(ResponseJson()).TemplateReplacements);

            JObject withNull = ResponseJson();
            withNull["templateReplacements"] = JValue.CreateNull();
            Assert.Null(ParseResponse(withNull).TemplateReplacements);
        }

        [Fact]
        public void ConstructorKeepsPositionsOfPreviousParameters()
        {
            DateTime created = new DateTime(2030, 1, 2);
            DateTime updated = new DateTime(2030, 1, 3);

            CreditNote note = new CreditNote(
                "templateId", "Jamie Lannister", "012.345.678-90", 100000L, new DateTime(2030, 1, 1),
                new List<Invoice>(), new Transfer(bankCode: "00000000", branchCode: "1234", accountNumber: "1-1", name: "J", taxID: "1"),
                new List<CreditSigner>(), "externalId", "line1", "line2", "district",
                "city", "SP", "01234-567", "transfer",
                500L, new List<string> { "tag" }, "604800",
                new List<Rule>(), "5656565656565656", 99L, "documentId",
                "created", new List<string> { "tx" }, "workspaceId",
                "debtorWorkspaceId", 7L,
                1.5f, 2.5f, created,
                updated
            );

            Assert.Equal("5656565656565656", note.ID);
            Assert.Equal(99L, note.Amount);
            Assert.Equal("documentId", note.DocumentID);
            Assert.Equal("created", note.Status);
            Assert.Equal("workspaceId", note.WorkspaceID);
            Assert.Equal("debtorWorkspaceId", note.DebtorWorkspaceID);
            Assert.Equal(7L, note.TaxAmount);
            Assert.Equal(1.5f, note.NominalInterest);
            Assert.Equal(2.5f, note.Interest);
            Assert.Equal(created, note.Created);
            Assert.Equal(updated, note.Updated);
            Assert.Null(note.TemplateReplacements);
        }

        private static IEnumerable<string> KeyNames(JObject json)
        {
            foreach (JProperty property in json.Properties())
            {
                yield return property.Name;
            }
        }

        private static JObject RequestBody(CreditNote note)
        {
            return JObject.Parse(JsonConvert.SerializeObject(StarkCore.Utils.Api.ApiJson(note)));
        }

        private static CreditNote ParseResponse(JObject response)
        {
            MethodInfo resourceMaker = typeof(CreditNote).GetMethod(
                "ResourceMaker",
                BindingFlags.NonPublic | BindingFlags.Static
            );
            return (CreditNote)resourceMaker.Invoke(null, new object[] { response });
        }

        private static CreditNote Build(Dictionary<string, object> templateReplacements)
        {
            return new CreditNote(
                templateID: "5706627130851328",
                name: "Jamie Lannister",
                taxID: "012.345.678-90",
                nominalAmount: 100000,
                scheduled: new DateTime(2030, 1, 1),
                invoices: new List<Invoice> { new Invoice(amount: 100000, due: new DateTime(2030, 2, 1)) },
                payment: new Transfer(
                    bankCode: "00000000",
                    branchCode: "1234",
                    accountNumber: "129340-1",
                    name: "Jamie Lannister",
                    taxID: "012.345.678-90"
                ),
                signers: new List<CreditSigner> {
                    new CreditSigner(name: "Jamie Lannister", contact: "jamie@invaliddomain.com", method: "link")
                },
                externalID: "offline-test",
                streetLine1: "Rua ABC",
                streetLine2: "Ap 123",
                district: "Jardim Paulista",
                city: "Sao Paulo",
                stateCode: "SP",
                zipCode: "01234-567",
                templateReplacements: templateReplacements
            );
        }

        // The core parses responses with DateParseHandling.None, so dates reach ResourceMaker as strings.
        private static JObject ParseJson(string content)
        {
            using (JsonTextReader reader = new JsonTextReader(new System.IO.StringReader(content)) { DateParseHandling = DateParseHandling.None })
            {
                return JObject.Load(reader);
            }
        }

        private static JObject ResponseJson()
        {
            return ParseJson(@"{
                ""id"": ""5656565656565656"",
                ""templateId"": ""5706627130851328"",
                ""name"": ""Jamie Lannister"",
                ""taxId"": ""012.345.678-90"",
                ""nominalAmount"": 100000,
                ""scheduled"": ""2030-01-01"",
                ""externalId"": ""offline-test"",
                ""streetLine1"": ""Rua ABC"",
                ""streetLine2"": ""Ap 123"",
                ""district"": ""Jardim Paulista"",
                ""city"": ""Sao Paulo"",
                ""stateCode"": ""SP"",
                ""zipCode"": ""01234-567"",
                ""paymentType"": ""transfer"",
                ""payment"": { ""name"": ""Jamie Lannister"", ""taxId"": ""012.345.678-90"", ""bankCode"": ""00000000"",
                               ""branchCode"": ""1234"", ""accountNumber"": ""129340-1"", ""scheduled"": ""2030-01-01"" },
                ""expiration"": ""604800"",
                ""status"": ""created"",
                ""transactionIds"": [],
                ""signers"": [],
                ""invoices"": [],
                ""rules"": [],
                ""created"": ""2030-01-01T00:00:00.000000+00:00"",
                ""updated"": ""2030-01-01T00:00:00.000000+00:00""
            }");
        }
    }
}
