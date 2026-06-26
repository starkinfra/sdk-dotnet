using Xunit;
using StarkInfra;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StarkInfraTests
{
    public class BusinessAttachmentTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void CreateGet()
        {
            BusinessIdentity identity = GetBusinessIdentity();
            Assert.NotNull(identity.ID);

            byte[] content = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8/5+hHgAHggJ/PchI7wAAAABJRU5ErkJggg==");
            List<BusinessAttachment> attachments = BusinessAttachment.Create(new List<BusinessAttachment>() {
                new BusinessAttachment(
                    name: "articles-of-incorporation.png",
                    content: content,
                    contentType: "image/png",
                    businessIdentityID: identity.ID
                )
            });
            BusinessAttachment attachment = attachments.First();
            Assert.NotNull(attachment.ID);

            BusinessAttachment getBusinessAttachment = BusinessAttachment.Get(
                id: attachment.ID,
                expand: new List<string> { "content" }
            );
            Assert.Equal(getBusinessAttachment.ID, attachment.ID);
        }

        [Fact]
        public void Query()
        {
            List<BusinessAttachment> attachments = BusinessAttachment.Query(limit: 5).ToList();
            Assert.True(attachments.Count <= 101);
            Assert.True(attachments.First().ID != attachments.Last().ID);
            foreach (BusinessAttachment attachment in attachments)
            {
                Assert.NotNull(attachment.ID);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<BusinessAttachment> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = BusinessAttachment.Page(limit: 2, cursor: cursor);
                foreach (BusinessAttachment entity in page)
                {
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }

                if (cursor == null)
                {
                    break;
                }
            }
            Assert.True(ids.Count == 4);
        }

        internal static BusinessIdentity GetBusinessIdentity()
        {
            List<BusinessIdentity> identities = BusinessIdentity.Query(limit: 1).ToList();
            if (identities.Count > 0)
            {
                return identities.First();
            }
            return BusinessIdentity.Create(new List<BusinessIdentity>() {
                new BusinessIdentity(taxID: "20.018.183/0001-80")
            }).First();
        }
    }
}
