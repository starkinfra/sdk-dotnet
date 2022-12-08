using Xunit;
using System;
using StarkInfra;
using System.Collections.Generic;
using static StarkInfra.CreditPreview;

namespace StarkInfraTests
{
    public class CreditPreviewTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Create()
        {
            CreditNotePreview previewSac = generateSacPreview();
            CreditNotePreview previewPrice = generatePricePreview();
            CreditNotePreview previewAmerican = generateAmericanPreview();
            CreditNotePreview previewBullet = generateBulletPreview();

            List<string> expectedTypes = new List<string> { "credit-note", "credit-note", "credit-note", "credit-note" };
            List<string> expectedNoteTypes = new List<string> { "sac", "price", "american", "bullet" };

            List<CreditPreview> creditNotePreviews = new List<CreditPreview>();
            creditNotePreviews.Add(new CreditPreview(type: "credit-note", credit: previewSac));
            creditNotePreviews.Add(new CreditPreview(type: "credit-note", credit: previewPrice));
            creditNotePreviews.Add(new CreditPreview(type: "credit-note", credit: previewAmerican));
            creditNotePreviews.Add(new CreditPreview(type: "credit-note", credit: previewBullet));

            List<string> previewedTypes = new List<string>();
            List<string> noteTypes = new List<string>();

            foreach (CreditPreview creditNotePreview in CreditPreview.Create(creditNotePreviews))
            {
                CreditNotePreview typedCredit = (CreditNotePreview)creditNotePreview.Credit;
                previewedTypes.Add(creditNotePreview.Type);
                noteTypes.Add(typedCredit.Type);
                TestUtils.Log(creditNotePreview);
            }
            Assert.Equal(expectedNoteTypes, noteTypes);
            Assert.Equal(previewedTypes, expectedTypes);
        }

        public Invoice generateInvoice(DateTime? days = null, long amount = 100)
        {
            return new Invoice(
                amount: amount,
                due: days is null ? DateTime.Today.Date.AddDays(new Random().Next(1, 999999999)) : days
            );
        }

        public List<Invoice> generateInvoicePreview(int n = 1, long amount = 100)
        {
            List<Invoice> invoices = new List<Invoice> { };
            for(int i = 0; i < n; i++)
            {
                invoices.Add(generateInvoice(days: DateTime.Now.AddDays((i + 1) * 30), amount: (int)(amount / n)));
            }
            return invoices;
        }

        List<string> intervalList = new List<string> { "month", "year" };

        public CreditNotePreview generateSacPreview()
        {
            return new CreditNotePreview(
                taxID: "095.550.500-31",
                type: "sac",
                nominalAmount: new Random().Next(1, 100000),
                rebateAmount: new Random().Next(60, 1000),
                nominalInterest: (float)new Random().NextDouble(),
                scheduled: DateTime.Today.Date.AddDays(new Random().Next(11, 20)),
                initialDue: DateTime.Today.Date.AddDays(new Random().Next(30, 40)),
                initialAmount: new Random().Next(1, 9999),
                interval: intervalList[new Random().Next(0, 1)]
            );
        }

        public CreditNotePreview generatePricePreview()
        {
            return new CreditNotePreview(
                taxID: "095.550.500-31",
                type: "price",
                nominalAmount: new Random().Next(1, 100000),
                rebateAmount: new Random().Next(60, 1000),
                nominalInterest: (float)new Random().NextDouble(),
                scheduled: DateTime.Today.Date.AddDays(new Random().Next(11, 20)),
                initialDue: DateTime.Today.Date.AddDays(new Random().Next(30, 40)),
                initialAmount: new Random().Next(1, 9999),
                interval: intervalList[new Random().Next(0, 1)]
            );
        }

        public CreditNotePreview generateAmericanPreview()
        {
            return new CreditNotePreview(
                taxID: "095.550.500-31",
                type: "american",
                nominalAmount: new Random().Next(1, 100000),
                rebateAmount: new Random().Next(60, 1000),
                nominalInterest: (float)new Random().NextDouble(),
                scheduled: DateTime.Today.Date.AddDays(new Random().Next(11, 20)),
                initialDue: DateTime.Today.Date.AddDays(new Random().Next(30, 40)),
                count: new Random().Next(1, 12),
                interval: intervalList[new Random().Next(0, 1)]
            );
        }

        public CreditNotePreview generateBulletPreview()
        {
            return new CreditNotePreview(
                taxID: "095.550.500-31",
                type: "bullet",
                nominalAmount: new Random().Next(1, 100000),
                rebateAmount: new Random().Next(1, 1000),
                nominalInterest: (float)new Random().NextDouble(),
                scheduled: DateTime.Today.Date.AddDays(new Random().Next(11, 20)),
                initialDue: DateTime.Today.Date.AddDays(new Random().Next(30, 40))
            );
        }

        public CreditNotePreview generateCustomPreview()
        {
            int amount = new Random().Next(1, 100000);
            return new CreditNotePreview(
                taxID: "095.550.500-31",
                type: "custom",
                nominalAmount: amount,
                scheduled: DateTime.Today.Date.AddDays(new Random().Next(1, 90)),
                rebateAmount: new Random().Next(1, 1000),
                invoices: generateInvoicePreview(n: new Random().Next(1, 12), amount: amount)
            );
        }
    }
}
