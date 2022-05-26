using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StarkInfra;
using Xunit;


namespace StarkInfraTests
{
    public class EventTest
    {
        public readonly string Content = "{\"event\": {\"created\": \"2022-02-15T20:45:09.852878+00:00\", \"id\": \"5015597159022592\", \"log\": {\"created\": \"2022-02-15T20:45:09.436621+00:00\", \"errors\": [{\"code\": \"insufficientFunds\", \"message\": \"Amount of funds available is not sufficient to cover the specified transfer\"}], \"id\": \"5288053467774976\", \"request\": {\"amount\": 1000, \"bankCode\": \"34052649\", \"cashAmount\": 0, \"cashierBankCode\": \"\", \"cashierType\": \"\", \"created\": \"2022-02-15T20:45:08.210009+00:00\", \"description\": \"For saving my life\", \"endToEndId\": \"E34052649202201272111u34srod1a91\", \"externalId\": \"141322efdgber1ecd1s342341321\", \"fee\": 0, \"flow\": \"out\", \"id\": \"5137269514043392\", \"initiatorTaxId\": \"\", \"method\": \"manual\", \"receiverAccountNumber\": \"000001\", \"receiverAccountType\": \"checking\", \"receiverBankCode\": \"00000001\", \"receiverBranchCode\": \"0001\", \"receiverKeyId\": \"\", \"receiverName\": \"Jamie Lennister\", \"receiverTaxId\": \"45.987.245/0001-92\", \"reconciliationId\": \"\", \"senderAccountNumber\": \"000000\", \"senderAccountType\": \"checking\", \"senderBankCode\": \"34052649\", \"senderBranchCode\": \"0000\", \"senderName\": \"tyrion Lennister\", \"senderTaxId\": \"012.345.678-90\", \"status\": \"failed\", \"tags\": [], \"updated\": \"2022-02-15T20:45:09.436661+00:00\"}, \"type\": \"failed\"}, \"subscription\": \"pix-request.out\", \"workspaceId\": \"5692908409716736\"}}";
        public readonly string GoodSignature = "MEYCIQD0oFxFQX0fI6B7oqjwLhkRhkDjrOiD86wguEKWdzkJbgIhAPNGUUdlNpYBe+npOaHa9WJopzy3WJYl8XJG6f4ek2R/";
        public readonly string BadSignature = "MEYCIQD0oFxFQL0fI6B7oqjwLhkRhkDjrOiD86wjjEKWdzkJbgIhAPNGUUdlNpYBe+npOaHa9WJopzy3WJYl8XJG6f4ek2R/";

        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void ParseWithRightSignature()
        {
            Event parsedEvent = Event.Parse(Content, GoodSignature);
            Assert.NotNull(parsedEvent.ID);
            Assert.NotNull(parsedEvent.Log);
            TestUtils.Log(parsedEvent);
        }

        [Fact]
        public void ParseWithWrongSignature()
        {
            try {
                Event parsedEvent = Event.Parse(Content, BadSignature);
            } catch (StarkInfra.Error.InvalidSignatureError e) {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }

        [Fact]
        public void ParseWithMalformedSignature()
        {
            try
            {
                Event parsedEvent = Event.Parse(Content, "something is definitely wrong");
            }
            catch (StarkInfra.Error.InvalidSignatureError e)
            {
                TestUtils.Log(e);
                return;
            }
            throw new Exception("failed to raise InvalidSignatureError");
        }
        
        [Fact]
        public void QueryAndAttempt()
        {
            List<Event> events = Event.Query(limit: 2, isDelivered: false).ToList();
            Assert.Equal(2, events.Count);
            foreach (Event eventObject in events)
            {
                List<Event.Attempt> attempts = Event.Attempt.Query(limit: 1, eventIds: new List<string> { eventObject.ID }).ToList();
                foreach (Event.Attempt attempt in attempts)
                {
                    Event.Attempt attemptGet = Event.Attempt.Get(attempt.ID);
                    Assert.Equal(attempt.ID, attemptGet.ID);
                }
            }
        }

        [Fact]
        public void Page()
        {
            List<string> ids = new List<string>();
            List<Event> page;
            string cursor = null;
            for (int i = 0; i < 2; i++)
            {
                (page, cursor) = Event.Page(limit: 2, cursor: cursor);
                foreach (Event entity in page)
                {
                    TestUtils.Log(entity);
                    Debug.Write(entity);
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

        [Fact]
        public void Update()
        {
            List<Event> events = Event.Query(limit: 2, isDelivered: false).ToList();
            Assert.True(2 >= events.Count);

            foreach (Event eventItem in events)
            {
                TestUtils.Log(eventItem);
                Debug.Write(eventItem);
                Assert.NotNull(eventItem.ID);
                Assert.Equal(false, eventItem.IsDelivered);
                Event updatedEvent = Event.Update(id: eventItem.ID, isDelivered: true);
                TestUtils.Log(updatedEvent);
                Debug.Write(updatedEvent);
                Assert.Equal(true, updatedEvent.IsDelivered);
            }
        }

        [Fact]
        public void GetAndCancel()
        {
            List<Event> events = Event.Query(limit: 2, isDelivered: false).ToList();
            Assert.True(2 >= events.Count);

            Event eventItem = events.First();
            TestUtils.Log(eventItem);
            Debug.Write(eventItem);
            Assert.NotNull(eventItem.ID);
            Event getEventItem = Event.Get(eventItem.ID);
            Assert.Equal(getEventItem.ID, eventItem.ID);
            TestUtils.Log(getEventItem);
            Debug.Write(getEventItem);
            Event canceledEventItem = Event.Cancel(id: eventItem.ID);
            Assert.Equal(canceledEventItem.ID, eventItem.ID);
            TestUtils.Log(canceledEventItem);
            Debug.Write(canceledEventItem);
        }
    }
}
