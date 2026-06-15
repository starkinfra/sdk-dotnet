using Xunit;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IssuingStockRuleTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<IssuingStockRule> rules = IssuingStockRule.Query(limit: 10).ToList();
            Assert.True(rules.Count <= 10);
            foreach (IssuingStockRule rule in rules)
            {
                TestUtils.Log(rule);
                Assert.NotNull(rule.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<IssuingStockRule> page;
            string cursor;
            (page, cursor) = IssuingStockRule.Page(limit: 3);
            foreach (IssuingStockRule entity in page)
            {
                TestUtils.Log(entity);
                Assert.NotNull(entity.ID);
            }
            Assert.NotNull(cursor);
        }

        [Fact]
        public void CreateUpdateAndCancel()
        {
            IssuingStock stock = IssuingStock.Query(limit: 1).ToList().First();

            List<IssuingStockRule> activeRules = IssuingStockRule.Query(
                stockIds: new List<string> { stock.ID },
                status: new List<string> { "active" }
            ).ToList();
            foreach (IssuingStockRule activeRule in activeRules)
            {
                IssuingStockRule.Cancel(id: activeRule.ID);
            }

            List<IssuingStockRule> rules = IssuingStockRule.Create(new List<IssuingStockRule>() {
                new IssuingStockRule(
                    minimumBalance: 10000,
                    stockID: stock.ID,
                    emails: new List<string> { "john.doe@enterprise.com" },
                    phones: new List<string> { "+5511912345678" }
                )
            });
            IssuingStockRule rule = rules.First();
            TestUtils.Log(rule);
            Assert.False(string.IsNullOrEmpty(rule.ID));

            IssuingStockRule updatedRule = IssuingStockRule.Update(id: rule.ID, minimumBalance: 20000);
            TestUtils.Log(updatedRule);
            Assert.Equal(20000, updatedRule.MinimumBalance);

            IssuingStockRule canceledRule = IssuingStockRule.Cancel(id: rule.ID);
            TestUtils.Log(canceledRule);
            Assert.Equal("canceled", canceledRule.Status);
        }
    }
}
