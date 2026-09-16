using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class BusinessAccountRequestTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            List<BusinessAccountRequest> requests = BusinessAccountRequest.Create(new List<BusinessAccountRequest>() { Example() });
            BusinessAccountRequest request = requests.First();
            Assert.NotNull(request.ID);
            Assert.Equal("business", request.AccountType);
            Assert.NotNull(request.Address);
            foreach (Owner owner in request.Owners)
            {
                Assert.NotNull(owner.TaxID);
            }

            BusinessAccountRequest getRequest = BusinessAccountRequest.Get(id: request.ID);
            Assert.Equal(getRequest.ID, request.ID);
            foreach (Owner owner in getRequest.Owners)
            {
                Assert.NotNull(owner.Name);
            }
        }

        [Fact]
        public void Query()
        {
            List<BusinessAccountRequest> requests = BusinessAccountRequest.Query(limit: 5).ToList();
            Assert.True(requests.Count <= 5);
            foreach (BusinessAccountRequest request in requests)
            {
                Assert.NotNull(request.ID);
            }
        }

        [Fact]
        public void QueryParams()
        {
            List<BusinessAccountRequest> requests = BusinessAccountRequest.Query(
                limit: 10,
                after: DateTime.Today.AddDays(-100),
                before: DateTime.Today,
                status: new List<string> { "created" },
                tags: new List<string> { "iron", "suit" },
                ids: new List<string> { "1", "2" }
            ).ToList();
            Assert.Empty(requests);
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BusinessAccountRequest> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BusinessAccountRequest.Page(limit: 2, cursor: cursor);
                foreach (BusinessAccountRequest request in page)
                {
                    Assert.DoesNotContain(request.ID, ids);
                    ids.Add(request.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count >= 1);
        }

        [Fact]
        public void StatusEnum()
        {
            List<string> allowed = new List<string> { "created", "processing", "approved", "denied", "failed" };
            List<BusinessAccountRequest> requests = BusinessAccountRequest.Query(limit: 10).ToList();
            foreach (BusinessAccountRequest request in requests)
            {
                if (request.Status != null)
                {
                    Assert.Contains(request.Status, allowed);
                }
            }
        }

        internal static BusinessAccountRequest Example() => new BusinessAccountRequest(
            name: "Stark Bank S.A.",
            taxID: "20.018.183/0001-80",
            address: new Address(
                street: "Av. Faria Lima",
                number: "2000",
                neighborhood: "Itaim Bibi",
                city: "Sao Paulo",
                state: "SP",
                zipCode: "04538-132",
                complement: "Sala 42"
            ),
            revenue: 100000000,
            owners: new List<Owner> {
                new Owner(
                    taxID: "012.345.678-90",
                    name: "Jamie Lannister",
                    role: "partner"
                ),
                new Owner(
                    taxID: "812.531.960-36",
                    name: "Cersei Lannister",
                    role: "representative"
                )
            },
            tags: new List<string> { "employees", "monthly" }
        );
    }
}
