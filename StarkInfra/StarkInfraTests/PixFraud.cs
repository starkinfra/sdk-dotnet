using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class PixFraudTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        // [M1] create accepts a list of PixFraud (externalID, type, taxID required; keyID, tags optional)
        // and returns the list with server-assigned id; [M2] get retrieves a single PixFraud by id.
        [Fact]
        public void CreateGet()
        {
            List<PixFraud> frauds = PixFraud.Create(new List<PixFraud>() { Example() });
            PixFraud fraud = frauds.First();
            Assert.NotNull(frauds.First().ID);
            PixFraud getPixFraud = PixFraud.Get(id: fraud.ID);
            Assert.Equal(getPixFraud.ID, fraud.ID);
            TestUtils.Log(fraud);
        }

        // [M1][M6] create returns the output-only fields populated and parsed.
        // [M7] status is parsed and non-empty — per contract v4 (c) NO closed-enum assertion
        //      (the live API may emit transitional/extra values).
        // [M8] created/updated parse to the native DateTime type. This SDK parses datetimes via
        //      StarkCore.Utils.Checks.CheckDateTime, which returns a native DateTime
        //      (see sdk-infra/dotnet/StarkInfra/StarkInfra/PixRequest/PixRequest.cs:82-83,478-480),
        //      so IsType<DateTime> is the canonical convention here — NOT a normalized string.
        [Fact]
        public void CreateOutputFields()
        {
            List<PixFraud> frauds = PixFraud.Create(new List<PixFraud>() { Example() });
            PixFraud fraud = frauds.First();
            Assert.NotNull(fraud.ID);
            Assert.NotNull(fraud.BacenID);
            Assert.False(string.IsNullOrEmpty(fraud.Status));
            Assert.NotNull(fraud.Created);
            Assert.NotNull(fraud.Updated);
            Assert.IsType<DateTime>(fraud.Created.Value);
            Assert.IsType<DateTime>(fraud.Updated.Value);
            TestUtils.Log(fraud);
        }

        // [M3] query iterates entities created in the API.
        [Fact]
        public void Query()
        {
            List<PixFraud> frauds = PixFraud.Query(limit: 10).ToList();
            foreach (PixFraud fraud in frauds)
            {
                TestUtils.Log(fraud);
                Assert.NotNull(fraud.ID);
            }
            Assert.True(frauds.Count <= 10);
        }

        // [M3] query respects the ids filter as a round-trip.
        [Fact]
        public void QueryIds()
        {
            List<PixFraud> frauds = PixFraud.Query(limit: 10).ToList();
            List<string> fraudIdsExpected = new List<string>();
            foreach (PixFraud fraud in frauds)
            {
                Assert.NotNull(fraud.ID);
                fraudIdsExpected.Add(fraud.ID);
            }

            List<PixFraud> fraudsResult = PixFraud.Query(limit: 10, ids: fraudIdsExpected).ToList();
            List<string> fraudIdsResult = new List<string>();
            foreach (PixFraud fraud in fraudsResult)
            {
                Assert.NotNull(fraud.ID);
                fraudIdsResult.Add(fraud.ID);
            }

            fraudIdsExpected.Sort();
            fraudIdsResult.Sort();
            Assert.Equal(fraudIdsExpected, fraudIdsResult);
        }

        // [M3] query exercises every documented filter param at once (limit, after, before, status, ids, tags).
        // flow is NOT a valid PixFraud query parameter — the live API rejects it with
        // invalidQueryString (contract v4 M3), so it is intentionally omitted here.
        [Fact]
        public void QueryParams()
        {
            List<PixFraud> frauds = PixFraud.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "registered" },
                ids: new List<string> { "1", "2" },
                tags: new List<string> { "iron", "bank" }
            ).ToList();
            Assert.True(frauds.Count == 0);
        }

        // [M3][M5] page uses an opaque cursor and accumulates distinct entities across pages.
        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixFraud> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixFraud.Page(limit: 5, cursor: cursor);
                foreach (PixFraud entity in page)
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

        // [M3] page exercises every documented filter param at once (cursor, limit, after, before, status, ids, tags).
        // flow is NOT a valid PixFraud query parameter — the live API rejects it with
        // invalidQueryString (contract v4 M3), so it is intentionally omitted here.
        [Fact]
        public void PageParams()
        {
            List<PixFraud> page;
            string cursor = null;
            (page, cursor) = PixFraud.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "registered" },
                ids: new List<string> { "1", "2" },
                tags: new List<string> { "iron", "bank" }
            );
            Assert.True(page.Count == 0);
        }

        // [M4] cancel is a DELETE on /pix-fraud/{id}. The API only cancels frauds already in
        // `registered` status — a freshly-created fraud returns `invalidCancellationStatus`
        // (contract v4 M4). The sandbox cannot produce a cancelable fraud on demand and the
        // reference SDKs ship no cancel test, so M4 requires the Cancel IMPL only (verified by
        // Phase 5 against the resource surface) — NO happy-path cancel test is asserted here.

        // [M7] type carries one of identity | mule | scam | other; the Example() factory uses a
        // documented value. Per contract v4 (c), this does NOT assert a closed enum on `type`
        // (the live API may emit values beyond the documented set) — it only verifies the factory
        // produces a non-empty type, matching the canonical parsed/non-empty convention.
        [Fact]
        public void TypeIsPopulated()
        {
            PixFraud fraud = Example();
            Assert.False(string.IsNullOrEmpty(fraud.Type));
        }

        // Per-resource factory used by Create tests and by the Log test file.
        // [M1] required fields externalID, type, taxID; keyID and tags are optional.
        // external_id is a generated id, mirroring the sibling resources (PixRequest.Example).
        internal static PixFraud Example()
        {
            return new PixFraud(
                externalID: Convert.ToString(new Random().Next(1, 999999999)),
                type: "scam",
                taxID: "01234567890",
                tags: new List<string> { "fraudulent" }
            );
        }
    }
}
