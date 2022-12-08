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
            PixStatement statement = PixStatement.Create( Example());
            Assert.NotNull(statement.ID);
            PixStatement getPixStatement = PixStatement.Get(id: statement.ID);
            Assert.Equal(getPixStatement.ID, statement.ID);
            TestUtils.Log(statement);
        }

        [Fact]
        public void Query()
        {
            List<PixStatement> statements = PixStatement.Query(limit: 12).ToList();
            Assert.True(statements.Count <= 12);
            Assert.True(statements.First().ID != statements.Last().ID);
            foreach (PixStatement statement in statements)
            {
                TestUtils.Log(statement);
                Assert.NotNull(statement.ID);
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
            List<PixStatement> statements = PixStatement.Query(limit: 10).ToList();
            List<string> statementsIdsExpected = new List<string>();
            Assert.Equal(10, statements.Count);
            Assert.True(statements.First().ID != statements.Last().ID);
            foreach (PixStatement transaction in statements)
            {
                Assert.NotNull(transaction.ID);
                statementsIdsExpected.Add(transaction.ID);
            }

            List<PixStatement> statementsResult = PixStatement.Query(limit:10, ids: statementsIdsExpected).ToList();
            List<string> statementsIdsResult = new List<string>();
            Assert.Equal(10, statements.Count);
            Assert.True(statements.First().ID != statements.Last().ID);
            foreach (PixStatement transaction in statementsResult)
            {
                Assert.NotNull(transaction.ID);
                statementsIdsResult.Add(transaction.ID);
            }

            statementsIdsExpected.Sort();
            statementsIdsResult.Sort();
            Assert.Equal(statementsIdsExpected, statementsIdsResult);
        }
        
        [Fact]
        public void QueryParams()
        {
            List<PixStatement> statements = PixStatement.Query(
                limit: 10,
                ids: new List<string> {"1", "2"}
            ).ToList();
            Assert.True(statements.Count == 0);
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
