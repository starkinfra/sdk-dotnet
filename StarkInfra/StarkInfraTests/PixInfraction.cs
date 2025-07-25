﻿using System;
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
            List<PixInfraction> infractions = PixInfraction.Query(limit: 10).ToList();
            foreach (PixInfraction infraction in infractions)
            {
                Assert.NotNull(infraction.ID);
            }
            Assert.Equal(10, infractions.Count);
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
                    Assert.DoesNotContain(entity.ID, ids);
                    ids.Add(entity.ID);
                }
                if (cursor == null)
                {
                    break;
                }
            }
            Assert.Equal(10, ids.Count);
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
                if (infraction.Flow == "in")
                {
                    idsToPatch.Add(infraction.ID);
                }
            }
            string result = "agreed";
            string fraudType = "scam";
            foreach (PixInfraction infraction in infractions)
            {
                Assert.NotNull(infraction.ID);
                PixInfraction updatedPixInfraction = PixInfraction.Update(id: infraction.ID, result: result, fraudType: fraudType, patchData: patchData);
                Assert.Equal(result, updatedPixInfraction.Result);
            }
        }

        [Fact]
        public void CreateGet()
        {
            PixInfraction infraction = PixInfraction.Create(Example(), user).First();

            PixInfraction getPixInfraction = PixInfraction.Get(id: infraction.ID);
            Assert.Equal(getPixInfraction.ID, infraction.ID);

        }

        [Fact]
        public void QueryGetAndCancel()
        {
            List<PixInfraction> infractions = PixInfraction.Query(status: "delivered").ToList();
            Assert.NotEmpty(infractions);
            foreach (PixInfraction infraction in infractions)
            {
                if(infraction.Flow == "reporter")
                {
                    PixInfraction infractioner = PixInfraction.Get(infraction.ID);
                    PixInfraction cancelInfraction = PixInfraction.Cancel(id: infractioner.ID);
                    break;
                }
            }
        }

        internal static List<PixInfraction> Example()
        {
            List<PixRequest> createdRequest = PixRequest.Create(new List<PixRequest> { ExamplePixRequest() }).ToList();
            System.Threading.Thread.Sleep(5000);
            PixRequest request = PixRequest.Get(createdRequest[0].ID);
            return new List<PixInfraction>{
                new PixInfraction(
                    referenceID: request.EndToEndID,
                    type: "fraud",
                    method: "scam",
                    tags: new List<string> { "teste sdk" },
                    operatorEmail: "ned.stark@company.com",
                    operatorPhone: "+5511999999999"
                )
            };
        }

        internal static PixRequest ExamplePixRequest()
        {
            return new PixRequest(
                amount: new Random().Next(1, 1000),
                externalID: Convert.ToString(new Random().Next(1, 999999999)),
                senderName: "Arya",
                senderTaxID: "01234567890",
                senderBranchCode: "0000",
                senderAccountNumber: "00000-0",
                senderAccountType: "checking",
                receiverName: "maria",
                receiverTaxID: "01234567890",
                receiverBankCode: "20018183",
                receiverBranchCode: "0001",
                receiverAccountNumber: "00000-1",
                receiverAccountType: "checking",
                endToEndID: "E35547753205" + Convert.ToString(new Random().Next(111111111, 999999999)) + "u34sDGd19l2"
            );
        }
    }
}
