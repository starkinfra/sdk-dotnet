using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class CreditNoteTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<CreditNote> notes = CreditNote.Create(new List<CreditNote>() { Example() });
            CreditNote note = notes.First();
            Assert.NotNull(note.ID);
            CreditNote getCreditNote = CreditNote.Get(id: note.ID);
            Assert.Equal(getCreditNote.ID, note.ID);
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            List<CreditNote> notes = CreditNote.Create(new List<CreditNote>() { Example() });
            CreditNote note = notes.First();

            CreditNote getCreditNote = CreditNote.Get(id: note.ID);
            Assert.Equal(getCreditNote.ID, note.ID);

            CreditNote cancelCreditNote = CreditNote.Cancel(id: note.ID);
            Assert.Equal(cancelCreditNote.ID, note.ID);
        }

        [Fact]
        public void Query()
        {
            List<CreditNote> notes = CreditNote.Query(limit: 5, status: new List<string> { "canceled" }).ToList();
            Assert.True(notes.Count <= 101);
            Assert.True(notes.First().ID != notes.Last().ID);
            foreach (CreditNote note in notes)
            {
                Assert.NotNull(note.ID);
                foreach (CreditSigner signer in note.Signers)
                {
                    Assert.NotNull(signer.ID);
                }
                foreach(Invoice invoice in note.Invoices)
                {
                    Assert.NotNull(invoice.ID);

                    foreach(Discount discounts in invoice.Discounts)
                    {
                        Assert.NotNull(discounts.Percentage);
                    }
                    foreach (Description description in invoice.Descriptions)
                    {
                        Assert.NotNull(description.Key);
                    }
                }
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<CreditNote> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = CreditNote.Page(limit: 1, cursor: cursor);
                foreach (CreditNote entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }

                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 2);
        }

        [Fact]
        public void RuleConstructsFromKeyAndValue()
        {
            Rule rule = new Rule(key: "invoiceCreationMode", value: "scheduled");
            Assert.Equal("invoiceCreationMode", rule.Key);
            Assert.Equal("scheduled", rule.Value);
        }

        [Fact]
        public void CreateWithRuleObjects()
        {
            List<CreditNote> notes = CreditNote.Create(new List<CreditNote>() { Example() });

            CreditNote note = notes.First();
            Assert.NotNull(note.ID);
            Assert.NotNull(note.Rules);
            Assert.NotEmpty(note.Rules);

            Rule rule = note.Rules.First();
            Assert.Equal("invoiceCreationMode", rule.Key);
            Assert.Equal("scheduled", rule.Value);
        }

        [Fact]
        public void CreateWithRuleDicts()
        {
            Dictionary<string, object> note = new Dictionary<string, object> {
                { "templateId", "5706627130851328" },
                { "name", "Jamie Lannister" },
                { "taxId", "012.345.678-90" },
                { "nominalAmount", 100000 },
                { "scheduled", DateTime.Now.AddDays(5) },
                { "invoices", new List<Dictionary<string, object>> {
                    new Dictionary<string, object> {
                        { "amount", 50000 },
                        { "due", DateTime.Now.AddDays(35) }
                    },
                    new Dictionary<string, object> {
                        { "amount", 50000 },
                        { "due", DateTime.Now.AddDays(65) }
                    }
                } },
                { "payment", new Transfer(
                    bankCode: "00000000",
                    branchCode: "1234",
                    accountNumber: "129340-1",
                    name: "Jamie Lannister",
                    taxID: "012.345.678-90"
                ) },
                { "paymentType", "transfer" },
                { "signers", new List<Dictionary<string, object>> {
                    new Dictionary<string, object> {
                        { "name", "Jamie Lannister" },
                        { "contact", "jamie.lannister.invaliddomain@invaliddomain.com" },
                        { "method", "link" }
                    }
                } },
                { "externalId", Guid.NewGuid().ToString() },
                { "streetLine1", "Rua ABC" },
                { "streetLine2", "Ap 123" },
                { "district", "Jardim Paulista" },
                { "city", "São Paulo" },
                { "stateCode", "SP" },
                { "zipCode", "01234-567" },
                { "rules", new List<Dictionary<string, object>> {
                    new Dictionary<string, object> {
                        { "key", "invoiceCreationMode" },
                        { "value", "scheduled" }
                    }
                } }
            };
            List<CreditNote> notes = CreditNote.Create(new List<Dictionary<string, object>>() { note });

            CreditNote created = notes.First();
            Assert.NotNull(created.ID);
            Assert.NotNull(created.Rules);
            Assert.NotEmpty(created.Rules);

            Rule rule = created.Rules.First();
            Assert.Equal("invoiceCreationMode", rule.Key);
            Assert.Equal("scheduled", rule.Value);
        }

        [Fact]
        public void DebtorWorkspaceIDIsAccessible()
        {
            List<CreditNote> notes = CreditNote.Create(new List<CreditNote>() { Example() });
            
            CreditNote note = notes.First();
            Assert.NotNull(note.ID);
            string debtorWorkspaceID = note.DebtorWorkspaceID;
            Assert.NotNull(debtorWorkspaceID);
        }

        internal static CreditNote Example() => new CreditNote(
            templateID: "5706627130851328",
            name: "Jamie Lannister",
            taxID: "012.345.678-90",
            nominalAmount: 100000,
            scheduled: DateTime.Now.AddDays(5),
            invoices: new List<Invoice> {
                new Invoice(
                    amount: 50000,
                    due: DateTime.Now.AddDays(35)
                ),
                new Invoice(
                    amount: 50000,
                    due: DateTime.Now.AddDays(65)
                )
            },
            payment: new Transfer(
                bankCode: "00000000",
                branchCode: "1234",
                accountNumber: "129340-1",
                name: "Jamie Lannister",
                taxID: "012.345.678-90"
            ),
            signers: new List<CreditSigner>{
                new CreditSigner(
                    name: "Jamie Lannister",
                    contact: "jamie.lannister.invaliddomain@invaliddomain.com",
                    method: "link"
                ),
                new CreditSigner(
                    name: "Arya Stark",
                    contact: "arya.stark.invaliddomain@invaliddomain.com",
                    method: "link"
                )
            },
            externalID: Guid.NewGuid().ToString(),
            streetLine1: "Rua ABC",
            streetLine2: "Ap 123",
            district: "Jardim Paulista",
            city: "São Paulo",
            stateCode: "SP",
            zipCode: "01234-567",
            rules: new List<Rule> {
                new Rule(
                    key: "invoiceCreationMode",
                    value: "scheduled"
                )
            }
        );
    }
}
