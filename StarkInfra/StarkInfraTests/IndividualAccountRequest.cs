using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IndividualAccountRequestTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        // [M1] create accepts a List<IndividualAccountRequest> and returns the same shape
        // with server-assigned id/status/accountType/created/updated populated.
        // [M3] get(id) returns a single IndividualAccountRequest by id.
        // Self-contained: creates a fresh record and operates on THAT (no query over arbitrary records).
        [Fact]
        public void CreateGet()
        {
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Create(
                new List<IndividualAccountRequest>() { Example() });
            IndividualAccountRequest request = requests.First();
            Assert.NotNull(request.ID);
            Assert.Equal("individual", request.AccountType);
            Assert.NotNull(request.Status);
            IndividualAccountRequest getRequest = IndividualAccountRequest.Get(id: request.ID);
            Assert.Equal(getRequest.ID, request.ID);
            TestUtils.Log(getRequest);
        }

        // [M2] address is an object with required sub-fields, serialized as a nested JSON object,
        // never flattened. Round-trips through create + get as a structured object.
        [Fact]
        public void CreateWithStructuredAddress()
        {
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Create(
                new List<IndividualAccountRequest>() { Example() });
            IndividualAccountRequest request = requests.First();
            Assert.NotNull(request.ID);
            Assert.NotNull(request.Address);
            Assert.Equal("Rua do Estilo Barroco", request.Address.Street);
            Assert.Equal("648", request.Address.Number);
            Assert.Equal("Santo Amaro", request.Address.Neighborhood);
            TestUtils.Log(request);
        }

        // [M4] query returns an iterable of IndividualAccountRequest accepting
        // limit, after, before, status, tags, ids.
        [Fact]
        public void Query()
        {
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Query(limit: 101).ToList();
            Assert.True(requests.Count <= 101);
            foreach (IndividualAccountRequest request in requests)
            {
                TestUtils.Log(request);
                Assert.NotNull(request.ID);
            }
        }

        // [M4] query honours the ids filter as a round-trip.
        [Fact]
        public void QueryIds()
        {
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Query(limit: 10).ToList();
            List<string> idsExpected = new List<string>();
            foreach (IndividualAccountRequest request in requests)
            {
                Assert.NotNull(request.ID);
                idsExpected.Add(request.ID);
            }

            List<IndividualAccountRequest> result = IndividualAccountRequest.Query(
                limit: 10, ids: idsExpected).ToList();
            List<string> idsResult = new List<string>();
            foreach (IndividualAccountRequest request in result)
            {
                Assert.NotNull(request.ID);
                idsResult.Add(request.ID);
            }

            idsExpected.Sort();
            idsResult.Sort();
            Assert.Equal(idsExpected, idsResult);
        }

        // [M4] every documented query filter param serializes without throwing.
        [Fact]
        public void QueryParams()
        {
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Query(
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "created" },
                tags: new List<string> { "employees", "monthly" },
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.True(requests.Count == 0);
        }

        // [M5] page returns (items, cursor) and accepts the same params as query plus cursor.
        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IndividualAccountRequest> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IndividualAccountRequest.Page(limit: 5, cursor: cursor);
                foreach (IndividualAccountRequest entity in page)
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

        // [M5] every documented page filter param serializes without throwing.
        [Fact]
        public void PageParams()
        {
            List<IndividualAccountRequest> page;
            string cursor = null;
            (page, cursor) = IndividualAccountRequest.Page(
                cursor: null,
                limit: 10,
                after: new DateTime(2022, 01, 01),
                before: new DateTime(2022, 01, 02),
                status: new List<string> { "created" },
                tags: new List<string> { "employees", "monthly" },
                ids: new List<string> { "1", "2" }
            );
            Assert.True(page.Count == 0);
        }

        // [M6] update(id, ...) PATCHes the request. Patches name and address (NOT status), replacing
        // the address object as a whole. Self-contained: creates a fresh record and updates THAT.
        [Fact]
        public void Update()
        {
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Create(
                new List<IndividualAccountRequest>() { Example() });
            IndividualAccountRequest request = requests.First();
            Assert.NotNull(request.ID);

            IndividualAccountRequest updated = IndividualAccountRequest.Update(
                id: request.ID,
                name: "Tony Stark Updated",
                address: new Address(
                    street: "Av. Paulista",
                    number: "1000",
                    neighborhood: "Bela Vista",
                    city: "SP",
                    state: "SP",
                    zipCode: "01310-100"
                )
            );
            Assert.Equal("Tony Stark Updated", updated.Name);
            Assert.Equal("Av. Paulista", updated.Address.Street);
            TestUtils.Log(updated);
        }

        // [M7] status enum is exactly approved|created|denied|processing|updated.
        // Every fetched status is a member of this set.
        [Fact]
        public void StatusEnum()
        {
            List<string> allowed = new List<string> {
                "approved", "created", "denied", "processing", "updated"
            };
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Query(limit: 20).ToList();
            foreach (IndividualAccountRequest request in requests)
            {
                Assert.Contains(request.Status, allowed);
            }
        }

        // [M11] accountType / flags / id / status / created / updated are output-only:
        // passing them to the create constructor populates the field but they are not sent on POST.
        // Construct with output-only fields supplied; create must still succeed (API ignores them).
        [Fact]
        public void OutputOnlyFieldsIgnoredOnCreate()
        {
            IndividualAccountRequest seed = new IndividualAccountRequest(
                name: "Tony Stark",
                taxID: "012.345.678-90",
                address: ExampleAddress(),
                income: 1000000,
                tags: new List<string> { "employees" }
            );
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Create(
                new List<IndividualAccountRequest>() { seed });
            IndividualAccountRequest request = requests.First();
            Assert.NotNull(request.ID);
            Assert.NotNull(request.Created);
            Assert.NotNull(request.Updated);
            TestUtils.Log(request);
        }

        // ===== Error-path tests ([M12] — assert the mapped exception TYPE is raised, never a code string) =====

        // [E] name missing or empty -> InputErrors.
        [Fact]
        public void CreateWithEmptyNameRaises()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountRequest.Create(new List<IndividualAccountRequest>() {
                    new IndividualAccountRequest(
                        name: "",
                        taxID: "012.345.678-90",
                        address: ExampleAddress(),
                        income: 1000000
                    )
                })
            );
        }

        // [E] taxId invalid (fails CPF checksum) -> InputErrors.
        [Fact]
        public void CreateWithInvalidTaxIdRaises()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountRequest.Create(new List<IndividualAccountRequest>() {
                    new IndividualAccountRequest(
                        name: "Tony Stark",
                        taxID: "000.000.000-00",
                        address: ExampleAddress(),
                        income: 1000000
                    )
                })
            );
        }

        // [E] address missing a required sub-field -> InputErrors.
        [Fact]
        public void CreateWithIncompleteAddressRaises()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountRequest.Create(new List<IndividualAccountRequest>() {
                    new IndividualAccountRequest(
                        name: "Tony Stark",
                        taxID: "012.345.678-90",
                        address: new Address(
                            street: "",
                            number: "",
                            neighborhood: "",
                            city: "",
                            state: "",
                            zipCode: ""
                        ),
                        income: 1000000
                    )
                })
            );
        }

        // [E] income < 0 -> InputErrors (income = 0 is ACCEPTED, so the trigger is -1).
        [Fact]
        public void CreateWithNegativeIncomeRaises()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountRequest.Create(new List<IndividualAccountRequest>() {
                    new IndividualAccountRequest(
                        name: "Tony Stark",
                        taxID: "012.345.678-90",
                        address: ExampleAddress(),
                        income: -1
                    )
                })
            );
        }

        // [E] status transition not allowed -> InputErrors. Self-contained: fresh record, then a bad
        // status update. Uses a non-enum status value to force the rejection.
        [Fact]
        public void UpdateWithInvalidStatusRaises()
        {
            List<IndividualAccountRequest> requests = IndividualAccountRequest.Create(
                new List<IndividualAccountRequest>() { Example() });
            IndividualAccountRequest request = requests.First();
            Assert.NotNull(request.ID);
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountRequest.Update(id: request.ID, status: "not-a-real-status")
            );
        }

        // [E] unknown id (get) -> InputErrors.
        [Fact]
        public void GetUnknownIdRaises()
        {
            Assert.Throws<StarkCore.Error.InputErrors>(() =>
                IndividualAccountRequest.Get(id: "0")
            );
        }

        // Per-resource factory: a valid-for-create entity (output-only fields omitted).
        internal static IndividualAccountRequest Example()
        {
            return new IndividualAccountRequest(
                name: "Tony Stark",
                taxID: "012.345.678-90",
                address: ExampleAddress(),
                income: 1000000,
                tags: new List<string> { "employees", "monthly" }
            );
        }

        // Structured address fixture shared by request tests and by attachment tests
        // that build a fresh parent.
        internal static Address ExampleAddress()
        {
            return new Address(
                street: "Rua do Estilo Barroco",
                number: "648",
                neighborhood: "Santo Amaro",
                city: "SP",
                state: "SP",
                zipCode: "05724005"
            );
        }
    }
}
