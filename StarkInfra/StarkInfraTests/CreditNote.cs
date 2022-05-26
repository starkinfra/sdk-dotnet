using Xunit;
using StarkInfra;
using StarkInfra.Utils;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;
using System.Diagnostics;


namespace StarkInfraTests
{
    public class CreditNoteTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<CreditNote> creditNotes = CreditNote.Create(new List<CreditNote>() { Example() });
            CreditNote creditNote = creditNotes.First();
            Assert.NotNull(creditNote.ID);
            CreditNote getCreditNote = CreditNote.Get(id: creditNote.ID);
            Assert.Equal(getCreditNote.ID, creditNote.ID);
            TestUtils.Log(creditNote);
        }

        [Fact]
        public void CreateGetAndCancel()
        {
            List<CreditNote> creditNotes = CreditNote.Create(new List<CreditNote>() { Example() });
            CreditNote creditNote = creditNotes.First();
            TestUtils.Log(creditNote);
            CreditNote getCreditNote = CreditNote.Get(id: creditNote.ID);
            Assert.Equal(getCreditNote.ID, creditNote.ID);
            CreditNote cancelCreditNote = CreditNote.Cancel(id: creditNote.ID);
            Assert.Equal(cancelCreditNote.ID, creditNote.ID);
            TestUtils.Log(creditNote);
        }

        [Fact]
        public void Query()
        {
            List<CreditNote> creditNotes = CreditNote.Query(limit: 5).ToList();
            Assert.True(creditNotes.Count <= 101);
            Assert.True(creditNotes.First().ID != creditNotes.Last().ID);
            foreach (CreditNote creditNote in creditNotes)
            {
                TestUtils.Log(creditNote);
                Assert.NotNull(creditNote.ID);
                foreach(Invoice invoice in creditNote.Invoices)
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
            templateId: "5707012469948416",
            name: "Jamie Lannister",
            taxId: "012.345.678-90",
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
                taxId: "012.345.678-90"
            ),
            paymentType: "transfer",
            signers: new List<Signer>{
                new Signer(
                    name: "Jamie Lannister",
                    contact: "jamie.lannister.invaliddomain@invaliddomain.com",
                    method: "link"
                ),
                new Signer(
                    name: "Arya Stark",
                    contact: "arya.stark.invaliddomain@invaliddomain.com",
                    method: "link"
                )
            },
            externalId: Guid.NewGuid().ToString()
        );
    }
}
