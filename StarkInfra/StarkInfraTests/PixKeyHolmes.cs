using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixKeyHolmesTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        // [M1] create accepts a list of PixKeyHolmes (keyID required; tags optional) and returns
        //      the list with a server-assigned id. [M3] the resource exposes NO get — the created
        //      object is asserted directly off the create return, NOT re-fetched via Get.
        [Fact]
        public void Create()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Create(new List<PixKeyHolmes>() { Example() });
            PixKeyHolmes sherlock = holmes.First();
            Assert.NotNull(sherlock.ID);
            TestUtils.Log(sherlock);
        }

        // [M1][M5] create returns the output-only fields populated. [M6] created/updated parse to
        //      the native DateTime type via StarkCore.Utils.Checks.CheckDateTime
        //      (see sdk-infra/dotnet/StarkInfra/StarkInfra/CreditHolmes/CreditHolmes.cs:289,291),
        //      so IsType<DateTime> is the canonical convention here — NOT a normalized string.
        // status is parsed and non-empty only — per the PixFraud-run lesson, NO closed-enum
        //      assertion (documented values created|solving|solved|failed are an open set).
        // result is intentionally NOT asserted on a fresh create: per the contract it is populated
        //      only once the case is solved (registered|unregistered), so a just-created holmes may
        //      legitimately carry an empty/null result.
        [Fact]
        public void CreateOutputFields()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Create(new List<PixKeyHolmes>() { Example() });
            PixKeyHolmes sherlock = holmes.First();
            Assert.NotNull(sherlock.ID);
            Assert.False(string.IsNullOrEmpty(sherlock.Status));
            Assert.NotNull(sherlock.Created);
            Assert.NotNull(sherlock.Updated);
            Assert.IsType<DateTime>(sherlock.Created.Value);
            Assert.IsType<DateTime>(sherlock.Updated.Value);
            TestUtils.Log(sherlock);
        }

        // [M2][M3] query iterates entities created in the API. No get exists — the only single-entity
        //      access path is via query/page filters.
        [Fact]
        public void Query()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Query(limit: 10).ToList();
            foreach (PixKeyHolmes sherlock in holmes)
            {
                TestUtils.Log(sherlock);
                Assert.NotNull(sherlock.ID);
            }
            Assert.True(holmes.Count <= 10);
        }

        // [M2] query respects the ids filter as a round-trip.
        [Fact]
        public void QueryIds()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Query(limit: 10).ToList();
            List<string> holmesIdsExpected = new List<string>();
            foreach (PixKeyHolmes sherlock in holmes)
            {
                Assert.NotNull(sherlock.ID);
                holmesIdsExpected.Add(sherlock.ID);
            }

            List<PixKeyHolmes> holmesResult = PixKeyHolmes.Query(limit: 10, ids: holmesIdsExpected).ToList();
            List<string> holmesIdsResult = new List<string>();
            foreach (PixKeyHolmes sherlock in holmesResult)
            {
                Assert.NotNull(sherlock.ID);
                holmesIdsResult.Add(sherlock.ID);
            }

            holmesIdsExpected.Sort();
            holmesIdsResult.Sort();
            Assert.Equal(holmesIdsExpected, holmesIdsResult);
        }

        // [M2] query exercises every documented filter param at once (limit, after, before, status, tags, ids).
        [Fact]
        public void QueryParams()
        {
            List<PixKeyHolmes> holmes = PixKeyHolmes.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "solved" },
                tags: new List<string> { "iron", "bank" },
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(holmes.Count == 0);
        }

        // [M2][M4] page uses an opaque cursor (not a numeric page index) and accumulates distinct
        //      entities across pages.
        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixKeyHolmes> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixKeyHolmes.Page(limit: 5, cursor: cursor);
                foreach (PixKeyHolmes entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count <= 10);
        }

        // [M2][M4] page exercises every documented filter param at once (cursor, limit, after, before, status, tags, ids).
        [Fact]
        public void PageParams()
        {
            List<PixKeyHolmes> page;
            string cursor = null;
            (page, cursor) = PixKeyHolmes.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "solved" },
                tags: new List<string> { "iron", "bank" },
                ids: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }

        // [M3] The resource exposes ONLY create, query, page — there is NO get and NO cancel/delete.
        //      Asserted statically: the PixKeyHolmes type must NOT declare a public static Get,
        //      Cancel, or Delete method. This is a guard against the scaffolder accidentally adding
        //      them. Reflection over the public surface is the only way to assert absence at runtime.
        [Fact]
        public void ExposesOnlyCreateQueryPage()
        {
            Type type = typeof(PixKeyHolmes);
            Assert.Null(type.GetMethod("Get", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.Null(type.GetMethod("Cancel", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.Null(type.GetMethod("Delete", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.NotNull(type.GetMethod("Create", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.NotNull(type.GetMethod("Query", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
            Assert.NotNull(type.GetMethod("Page", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static));
        }

        // [M7] tags defaults to an empty/null list when omitted — the factory below omits tags in
        //      one of its forms; here we assert the constructor accepts a keyID-only PixKeyHolmes
        //      without throwing and leaves Tags unset (null) for the SDK to default API-side.
        [Fact]
        public void TagsDefaultWhenOmitted()
        {
            PixKeyHolmes sherlock = new PixKeyHolmes(keyID: "valid@sandbox.com");
            Assert.Null(sherlock.Tags);
            Assert.Equal("valid@sandbox.com", sherlock.KeyID);
        }

        // Per-resource factory used by the Create tests.
        // [M1] required field keyID; tags optional. keyID is a valid Pix key (email form, per the
        //      contract example "valid@sandbox.com" and the sibling CreditHolmes/PixClaim fixtures).
        internal static PixKeyHolmes Example()
        {
            return new PixKeyHolmes(
                keyID: "valid@sandbox.com",
                tags: new List<string> { "travel", "food" }
            );
        }
    }
}
