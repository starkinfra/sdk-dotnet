using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;


namespace StarkInfraTests
{
    public class PixStatementTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            PixStatement pixStatement = PixStatement.Create( Example());
            Assert.NotNull(pixStatement.ID);
            PixStatement getPixStatement = PixStatement.Get(id: pixStatement.ID);
            Assert.Equal(getPixStatement.ID, pixStatement.ID);
            TestUtils.Log(pixStatement);
        }

        [Fact]
        public void Query()
        {
            List<PixStatement> pixStatements = PixStatement.Query(limit: 12).ToList();
            Assert.True(pixStatements.Count <= 12);
            Assert.True(pixStatements.First().ID != pixStatements.Last().ID);
            foreach (PixStatement pixStatement in pixStatements)
            {
                TestUtils.Log(pixStatement);
                Assert.NotNull(pixStatement.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixStatement> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixStatement.Page(limit: 5, cursor: cursor);
                foreach (PixStatement entity in page)
                {
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

        [Fact]
        public void QueryIds()
        {
            List<PixStatement> pixStatements = PixStatement.Query(limit: 10).ToList();
            List<String> pixStatementsIdsExpected = new List<string>();
            Assert.Equal(10, pixStatements.Count);
            Assert.True(pixStatements.First().ID != pixStatements.Last().ID);
            foreach (PixStatement transaction in pixStatements)
            {
                Assert.NotNull(transaction.ID);
                pixStatementsIdsExpected.Add(transaction.ID);
            }

            List<PixStatement> pixStatementsResult = PixStatement.Query(limit:10, ids: pixStatementsIdsExpected).ToList();
            List<String> pixStatementsIdsResult = new List<string>();
            Assert.Equal(10, pixStatements.Count);
            Assert.True(pixStatements.First().ID != pixStatements.Last().ID);
            foreach (PixStatement transaction in pixStatementsResult)
            {
                Assert.NotNull(transaction.ID);
                pixStatementsIdsResult.Add(transaction.ID);
            }

            pixStatementsIdsExpected.Sort();
            pixStatementsIdsResult.Sort();
            Assert.Equal(pixStatementsIdsExpected, pixStatementsIdsResult);
        }
        
        [Fact]
        public void QueryParams()
        {
            List<PixStatement> pixStatements = PixStatement.Query(
                limit: 10,
                ids: new List<string> {"1", "2"}
            ).ToList();
            Assert.True(pixStatements.Count == 0);
        }
        
        [Fact]
        public void PageParams()
        {
            List<PixStatement> page;
            string cursor = null;
            (page, cursor) = PixStatement.Page(
                cursor: null,
                limit: 10,
                ids: new List<string> {"1", "2"}
            );
            Assert.True(page.Count == 0);
        }
        
        internal static PixStatement Example()
        {
            return new PixStatement(
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 01),
                type: "transaction"
            );
        }
    }
}