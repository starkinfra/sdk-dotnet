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
            IssuingInvoice issuingInvoice = IssuingInvoice.Create(Example());
            Assert.NotNull(issuingInvoice.ID);
            IssuingInvoice getIssuingInvoice = IssuingInvoice.Get(id: issuingInvoice.ID);
            Assert.Equal(getIssuingInvoice.ID, issuingInvoice.ID);
            TestUtils.Log(issuingInvoice);
        }

        [Fact]
        public void Query()
        {
            List<IssuingInvoice> issuingInvoices = IssuingInvoice.Query(limit: 3, status: "paid").ToList();
            Assert.True(issuingInvoices.Count <= 3);
            Assert.True(issuingInvoices.First().ID != issuingInvoices.Last().ID);
            foreach (IssuingInvoice issuingInvoice in issuingInvoices)
            {
                TestUtils.Log(issuingInvoice);
                Assert.NotNull(issuingInvoice.ID);
                Assert.Equal("paid", issuingInvoice.Status);
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
