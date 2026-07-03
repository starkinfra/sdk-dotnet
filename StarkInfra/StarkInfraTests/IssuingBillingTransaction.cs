using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingBillingTransactionTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingBillingTransaction> transactions = IssuingBillingTransaction.Query(limit: 101).ToList();
            Assert.True(transactions.Count <= 101);
            foreach (IssuingBillingTransaction transaction in transactions)
            {
                TestUtils.Log(transaction);
                Assert.NotNull(transaction.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<IssuingBillingTransaction> transactions = new List<IssuingBillingTransaction>();
            List<IssuingBillingTransaction> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IssuingBillingTransaction.Page(limit: 5, cursor: cursor);
                foreach (IssuingBillingTransaction entity in page)
                {
                    Assert.DoesNotContain(entity, transactions);
                    transactions.Add(entity);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(transactions.Count <= 10);
        }

        [Fact]
        public void QueryParams()
        {
            List<IssuingBillingTransaction> transactions = IssuingBillingTransaction.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                tags: new List<string> { "iron", "bank" }
            ).ToList();
            Assert.True(transactions.Count == 0);
        }

        [Fact]
        public void PageParams()
        {
            List<IssuingBillingTransaction> page;
            string cursor = null;
            (page, cursor) = IssuingBillingTransaction.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                tags: new List<string> { "iron", "bank" }
            );
            Assert.True(page.Count == 0);
        }

        [Fact]
        public void QueryByInvoiceId()
        {
            List<IssuingBillingInvoice> invoices = IssuingBillingInvoice.Query(limit: 1).ToList();
            Assert.True(invoices.Count <= 1);
            foreach (IssuingBillingInvoice invoice in invoices)
            {
                List<IssuingBillingTransaction> transactions = IssuingBillingTransaction.Query(
                    limit: 10,
                    invoiceID: invoice.ID
                ).ToList();
                Assert.True(transactions.Count <= 10);
                foreach (IssuingBillingTransaction transaction in transactions)
                {
                    TestUtils.Log(transaction);
                    Assert.NotNull(transaction.ID);
                }
            }
        }

        [Fact]
        public void QueryNonexistentInvoiceIdRaisesInputErrors()
        {
            try
            {
                IssuingBillingTransaction.Query(invoiceID: "999999999999999999").ToList();
            }
            catch (StarkCore.Error.InputErrors e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InputErrors");
        }

        [Fact]
        public void OutputFieldsAreExposed()
        {
            List<IssuingBillingTransaction> transactions = IssuingBillingTransaction.Query(limit: 1).ToList();
            Assert.True(transactions.Count <= 1);
            foreach (IssuingBillingTransaction transaction in transactions)
            {
                TestUtils.Log(transaction);
                Assert.NotNull(transaction.ID);
                long? amount = transaction.Amount;
                string invoiceID = transaction.InvoiceID;
                long? installment = transaction.Installment;
                long? installmentCount = transaction.InstallmentCount;
                long? balance = transaction.Balance;
                string holderName = transaction.HolderName;
                string source = transaction.Source;
                string externalID = transaction.ExternalID;
                string description = transaction.Description;
                string cardEnding = transaction.CardEnding;
                double? tax = transaction.Tax;
                double? rate = transaction.Rate;
                long? merchantAmount = transaction.MerchantAmount;
                string merchantCurrencyCode = transaction.MerchantCurrencyCode;
                DateTime? created = transaction.Created;
                TestUtils.Log(amount);
                TestUtils.Log(invoiceID);
                TestUtils.Log(installment);
                TestUtils.Log(installmentCount);
                TestUtils.Log(balance);
                TestUtils.Log(holderName);
                TestUtils.Log(source);
                TestUtils.Log(externalID);
                TestUtils.Log(description);
                TestUtils.Log(cardEnding);
                TestUtils.Log(tax);
                TestUtils.Log(rate);
                TestUtils.Log(merchantAmount);
                TestUtils.Log(merchantCurrencyCode);
                TestUtils.Log(created);
            }
        }
    }
}
