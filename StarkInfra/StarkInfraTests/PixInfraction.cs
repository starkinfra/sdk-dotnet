using System;
using StarkInfra;
using Xunit;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


namespace StarkInfraTests
{
    public class PixInfractionTest
    {
        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Query()
        {
            List<PixInfraction> PixInfractions = PixInfraction.Query(limit: 101, status: "created").ToList();
            foreach (PixInfraction PixInfraction in PixInfractions)
            {
                TestUtils.Log(PixInfraction);
                Assert.NotNull(PixInfraction.ID);
                Assert.Equal("created", PixInfraction.Status);
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<PixInfraction> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = PixInfraction.Page(limit: 5, cursor: cursor);
                foreach (PixInfraction entity in page)
                {
                    TestUtils.Log(entity);
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
        public void Update()
        {
            List<PixInfraction> infractions = PixInfraction.Query(limit: 1, status: "created").ToList();
            List<string> idsToPatch = new List<string>();
            Dictionary<string, object> patchData = new Dictionary<string, object> {
                { "analysis", "Disagreed with infraction" }
            };
            foreach (PixInfraction infraction in infractions)
            {
                if (infraction.Agent == "reporter")
                {
                    idsToPatch.Add(infraction.ID);
                }
            }
            string result = "disagreed";
            foreach (PixInfraction infraction in infractions)
            {
                TestUtils.Log(infraction);
                Assert.NotNull(infraction.ID);
                PixInfraction updatedPixInfraction = PixInfraction.Update(id: infraction.ID, result: result);
                TestUtils.Log(updatedPixInfraction);
                Assert.Equal(result, updatedPixInfraction.Result);
            }
        }

        [Fact]
        public void CreateGet()
        {
            PixInfraction pixInfraction = PixInfraction.Create(Example()).First();

            PixInfraction getPixInfraction = PixInfraction.Get(id: pixInfraction.ID);
            Assert.Equal(getPixInfraction.ID, pixInfraction.ID);

            TestUtils.Log(getPixInfraction);
        }

        [Fact]
        public void QueryGetAndCancel()
        {
            List<PixInfraction> pixInfractions = PixInfraction.Query(status: "delivered").ToList();
            Assert.NotEmpty(pixInfractions);
            TestUtils.Log(pixInfractions.First());
            foreach (PixInfraction pixInfraction in pixInfractions)
            {
                if(pixInfraction.Agent == "reporter")
                {
                    PixInfraction pixPixInfractioner = PixInfraction.Get(pixInfraction.ID);
                    PixInfraction cancelPixInfraction = PixInfraction.Cancel(id: pixPixInfractioner.ID);
                    TestUtils.Log(cancelPixInfraction);
                    break;
                }
            }
        }

        internal static List<PixInfraction> Example()
        {
            List<PixRequest> request = PixRequest.Query(limit: 1).ToList();
            return new List<PixInfraction>{
                new PixInfraction(
                    referenceId: request[0].EndToEndID,
                    type: "fraud"
                )
            };
        }
    }
}
