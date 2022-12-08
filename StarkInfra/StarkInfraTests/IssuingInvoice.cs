using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class IssuingInvoiceTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            IssuingInvoice invoice = IssuingInvoice.Create(Example());
            Assert.NotNull(invoice.ID);
            IssuingInvoice getIssuingInvoice = IssuingInvoice.Get(id: invoice.ID);
            Assert.Equal(getIssuingInvoice.ID, invoice.ID);
            TestUtils.Log(invoice);
        }

        [Fact]
        public void Query()
        {
            List<IssuingInvoice> invoices = IssuingInvoice.Query(limit: 3, status: "paid").ToList();
            Assert.True(invoices.Count <= 3);
            Assert.True(invoices.First().ID != invoices.Last().ID);
            foreach (IssuingInvoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
                Assert.Equal("paid", invoice.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IssuingInvoice> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingInvoice.Page(limit: 5, cursor: cursor);
                foreach (IssuingInvoice entity in page)
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
            Assert.True(ids.Count == 10);
        }

        internal static IssuingInvoice Example()
        {
            return new IssuingInvoice(
                amount: 10000
            );
        }
    }
}
