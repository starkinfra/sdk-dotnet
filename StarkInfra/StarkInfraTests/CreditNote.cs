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
            TestUtils.Log(note);
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            List<CreditNote> notes = CreditNote.Create(new List<CreditNote>() { Example() });
            CreditNote note = notes.First();
            TestUtils.Log(note);
            CreditNote getCreditNote = CreditNote.Get(id: note.ID);
            Assert.Equal(getCreditNote.ID, note.ID);
            CreditNote cancelCreditNote = CreditNote.Cancel(id: note.ID);
            Assert.Equal(cancelCreditNote.ID, note.ID);
            TestUtils.Log(note);
        }

        [Fact]
        public void Query()
        {
            List<CreditNote> notes = CreditNote.Query(limit: 5, status: new List<string> { "canceled" }).ToList();
            Assert.True(notes.Count <= 101);
            Assert.True(notes.First().ID != notes.Last().ID);
            foreach (CreditNote note in notes)
            {
                TestUtils.Log(note);
                Assert.NotNull(note.ID);
                foreach (CreditSigner signer in note.Signers)
                {
                    TestUtils.Log(signer);
                }
                foreach(Invoice invoice in note.Invoices)
                {
                    TestUtils.Log(invoice);

                    foreach(Discount discounts in invoice.Discounts)
                    {
                        TestUtils.Log(discounts);
                    }
                    foreach (Description description in invoice.Descriptions)
                    {
                        TestUtils.Log(description);
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

        internal static CreditNote Example() => new CreditNote(
            templateID: "5707012469948416",
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
            zipCode: "01234-567"
        );
    }
}
