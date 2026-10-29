using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingBillingInvoiceTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
        {
            List<IssuingBillingInvoice> invoices = IssuingBillingInvoice.Query(limit: 1).ToList();
            IssuingBillingInvoice invoice = invoices.First();
            IssuingBillingInvoice getInvoice = IssuingBillingInvoice.Get(invoice.ID);
            Assert.NotNull(getInvoice);
            Assert.Equal(getInvoice.ID, invoice.ID);
            TestUtils.Log(getInvoice);
        }

        [Fact]
        public void Query()
        {
            List<IssuingBillingInvoice> invoices = IssuingBillingInvoice.Query(limit: 101).ToList();
            Assert.True(invoices.Count <= 101);
            foreach (IssuingBillingInvoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
            }
        }

        [Fact]
        public void QueryIds()
        {
            List<IssuingBillingInvoice> invoices = IssuingBillingInvoice.Query(limit: 10).ToList();
            List<string> invoicesIdsExpected = new List<string>();
            foreach (IssuingBillingInvoice invoice in invoices)
            {
                Assert.NotNull(invoice.ID);
                invoicesIdsExpected.Add(invoice.ID);
            }

            List<IssuingBillingInvoice> invoicesResult = IssuingBillingInvoice.Query(limit: 10, ids: invoicesIdsExpected).ToList();
            List<string> invoicesIdsResult = new List<string>();
            foreach (IssuingBillingInvoice invoice in invoicesResult)
            {
                Assert.NotNull(invoice.ID);
                invoicesIdsResult.Add(invoice.ID);
            }

            invoicesIdsExpected.Sort();
            invoicesIdsResult.Sort();
            Assert.Equal(invoicesIdsExpected, invoicesIdsResult);
        }

        [Fact]
        public void QueryParams()
        {
            List<IssuingBillingInvoice> invoices = IssuingBillingInvoice.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "paid" },
                tags: new List<string> { "iron", "bank" },
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(invoices.Count == 0);
        }

        [Fact]
        public void Page()
        {
            List<IssuingBillingInvoice> invoices = new List<IssuingBillingInvoice>();
            List<IssuingBillingInvoice> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingBillingInvoice.Page(limit: 5, cursor: cursor);
                foreach (IssuingBillingInvoice entity in page)
                {
                    Assert.DoesNotContain(entity, invoices);
                    invoices.Add(entity);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(invoices.Count <= 10);
        }

        [Fact]
        public void PageParams()
        {
            List<IssuingBillingInvoice> page;
            string cursor = null;
            (page, cursor) = IssuingBillingInvoice.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "paid" },
                tags: new List<string> { "iron", "bank" }
            );
            Assert.True(page.Count == 0);
        }

        [Fact]
        public void OutputFieldsAreExposed()
        {
            List<IssuingBillingInvoice> invoices = IssuingBillingInvoice.Query(limit: 5).ToList();
            Assert.NotEmpty(invoices);
            foreach (IssuingBillingInvoice invoice in invoices)
            {
                TestUtils.Log(invoice);
                Assert.NotNull(invoice.ID);
                Assert.False(string.IsNullOrEmpty(invoice.TaxID));
                Assert.False(string.IsNullOrEmpty(invoice.Name));
                Assert.False(string.IsNullOrEmpty(invoice.Status));
                Assert.NotNull(invoice.Fine);
                Assert.NotNull(invoice.Interest);
                Assert.NotNull(invoice.Amount);
                Assert.NotNull(invoice.NominalAmount);
                Assert.NotNull(invoice.Brcode);
                Assert.NotNull(invoice.Link);
                Assert.NotNull(invoice.Due);
                Assert.NotNull(invoice.Start);
                Assert.NotNull(invoice.End);
                Assert.NotNull(invoice.Created);
                Assert.NotNull(invoice.Updated);
            }
        }
    }
}
