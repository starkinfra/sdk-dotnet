using Xunit;
using StarkInfra;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class IndividualDocumentTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {

            List<IndividualIdentity> identities = IndividualIdentity.Create(new List<IndividualIdentity>() { IdentityExample() });
            IndividualIdentity identity = identities.First();
            Assert.NotNull(identity.ID);

            byte[] image = System.IO.File.ReadAllBytes("../../../identity/identity-front-face.png");
            List<IndividualDocument> documents = IndividualDocument.Create(new List<IndividualDocument>() { 
                new IndividualDocument(
                    content: image,
                    contentType: "image/png",
                    type: "identity-front",
                    identityID: identity.ID
                )
            });
            IndividualDocument document = documents.First();
            Assert.NotNull(document.ID);

            image = System.IO.File.ReadAllBytes("../../../identity/identity-back-face.png");
            documents = IndividualDocument.Create(new List<IndividualDocument>() { 
                new IndividualDocument(
                    content: image,
                    contentType: "image/png",
                    type: "identity-back",
                    identityID: identity.ID
                )
            });
            document = documents.First();
            Assert.NotNull(document.ID);

            image = System.IO.File.ReadAllBytes("../../../identity/walter-white.png");
            documents = IndividualDocument.Create(new List<IndividualDocument>() { 
                new IndividualDocument(
                    content: image,
                    contentType: "image/png",
                    type: "selfie",
                    identityID: identity.ID
                )
            });
            document = documents.First();
            Assert.NotNull(document.ID);

            IndividualIdentity individual = IndividualIdentity.Update(
                id: identities[0].ID,
                status: "processing"
            );

            Assert.NotNull(individual.ID);
            Assert.True(individual.Status == "processing");
        }

        [Fact]
        public void Query()
        {
            List<IndividualDocument> identities = IndividualDocument.Query(limit: 5).ToList();
            Assert.True(identities.Count <= 101);
            Assert.True(identities.First().ID != identities.Last().ID);
            foreach (IndividualDocument identity in identities)
            {
                TestUtils.Log(identity);
                Assert.NotNull(identity.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<IndividualDocument> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = IndividualDocument.Page(limit: 1, cursor: cursor);
                foreach (IndividualDocument entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }

                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 2);
        }

        internal static IndividualIdentity IdentityExample() => new IndividualIdentity(
            name: "Jamie Lannister",
            taxID: "012.345.678-90"
        );
    }
}
