using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using EllipticCurve;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// Webhook Event object
    /// <br/>
    /// An Event is the notification received from the subscription to the Webhook.
    /// Events cannot be created, but may be retrieved from the Stark Infra API to
    /// list all generated updates on entities.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when the Event is created. ex: "5656565656565656"</item>
    ///     <item>Log [Log]: a Log object from one the subscription services ex: PixRequest.Log, PixReversal.Log</item>
    ///     <item>Created [DateTime]: creation datetime for the notification event. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>IsDelivered [bool]: true if the Event has been successfully delivered to the user url. ex: False</item>
    ///     <item>Subscription [string]: service that triggered this event. ex: "pix-request.in", "pix-request.out"</item>
    ///     <item>WorkspaceId [string]: ID of the Workspace that generated this Event. Mostly used when multiple Workspaces have Webhooks registered to the same endpoint. ex: "4545454545454545"
    /// </list>
    /// </summary>
    public partial class Event : Resource
    {
        public Resource Log { get; }
        public DateTime? Created { get; }
        public bool? IsDelivered { get; }
        public string Subscription { get; }
        public string WorkspaceId { get; }

        /// <summary>
        /// Webhook Event object
        /// <br/>
        /// An Event is the notification received from the subscription to the Webhook.
        /// Events cannot be created, but may be retrieved from the Stark Infra API to
        /// list all generated updates on entities.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the Event is created. ex: "5656565656565656"</item>
        ///     <item>log [Log]: a Log object from one the subscription services ex: PixRequest.Log, PixRversal.Log</item>
        ///     <item>created [DateTime]: creation datetime for the notification event. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>isDelivered [bool]: true if the Event has been successfully delivered to the user url. ex: False</item>
        ///     <item>subscription [string]: service that triggered this event. ex: "pix-request.in", "pix-request.out"</item>
        ///     <item>workspaceId [string]: ID of the Workspace that generated this event. Mostly used when multiple Workspaces have Webhooks registered to the same endpoint. ex: "4545454545454545"</item>
        /// </list>
        /// </summary>
        public Event(string id, Resource log, bool? isDelivered, string subscription, string workspaceId,
            DateTime? created = null) : base(id)
        {
            Log = log;
            Created = created;
            IsDelivered = isDelivered;
            Subscription = subscription;
            WorkspaceId = workspaceId;
        }

        /// <summary>
        /// Retrieve a specific notification Event
        /// <br/>
        /// Receive a single notification Event object previously created in the Stark Infra API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Event object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Event Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Event;
        }
        /// <summary>
        /// Retrieve notification Events
        /// <br/>
        /// Receive an IEnumerable of notification Event objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>isDelivered [bool, default null]: bool to filter successfully delivered events. ex: True or False</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Event objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Event> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            bool? isDelivered = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "isDelivered", isDelivered }
                },
                user: user
            ).Cast<Event>();
        }
        /// <summary>
        /// Retrieve paged notification Events
        /// <br/>
        /// Receive a list of up to 100 Event objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. It must be an integer between 1 and 100. ex: 50</item>
        ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>isDelivered [bool, default null]: bool to filter successfully delivered events. ex: True or False</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Event objects with updated attributes and cursor to retrieve the next page of Event objects</item>
        /// </list>
        /// </summary>
        public static (List<Event> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, bool? isDelivered = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "isDelivered", isDelivered }
                },
                user: user
            );
            List<Event> events = new List<Event>();
            foreach (SubResource subResource in page)
            {
                events.Add(subResource as Event);
            }
            return (events, pageCursor);
        }
        /// <summary>
        /// Cancel a notification Event
        /// <br/>
        /// Cancel a of notification Event entity previously created in the Stark Infra API by its ID
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: Event unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled Event object</item>
        /// </list>
        /// </summary>
        public static Event Cancel(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Event;
        }
        /// <summary>
        /// Update notification Event entity
        /// <br/>
        /// Update notification Event by passing id.
        /// If isDelivered is True, the event will no longer be returned on queries with isDelivered=False.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: Event unique ids. ex: "5656565656565656"</item>
        ///     <item>isDelivered [bool]: If True and event hasn't been delivered already, event will be set as delivered. ex: True</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target Event with updated attributes</item>
        /// </list>
        /// </summary>
        public static Event Update(string id, bool isDelivered, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "isDelivered", isDelivered }
                },
                user: user
            ) as Event;
        }

        /// <summary>
        /// Create single notification Event from a content string
        /// <br/>
        /// Create a single Event object received from event listening at subscribed user endpoint.
        /// If the provided digital signature does not check out with the StarkInfra public key, a
        /// starkinfra.exception.InvalidSignatureException will be raised.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>content [string]: response content from request received at user endpoint (not parsed)</item>
        ///     <item>signature [string]: base-64 digital signature received at response header "Digital-Signature"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Parsed Event object</item>
        /// </list>
        /// </summary>
        public static Event Parse(string content, string signature, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Parse.ParseAndVerify(
                content: content,
                signature: signature,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                user: user,
                key: "event"
            ) as Event;
        }
        
        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Event", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            bool? isDelivered = json.isDelivered;
            string subscription = json.subscription;
            string createdString = json.created;
            string workspaceId = json.workspaceId;
            DateTime created = Checks.CheckDateTime(createdString);

            Resource log = null;
            
            if (subscription.Contains("pix-key"))
            {
                log = PixKey.Log.ResourceMaker(json.log);
            }
            if (subscription.Contains("pix-claim"))
            {
                log = PixClaim.Log.ResourceMaker(json.log);
            }
            if (subscription.Contains("pix-chargeback"))
            {
                log = PixChargeback.Log.ResourceMaker(json.log);
            }
            if (subscription.Contains("pix-infraction"))
            {
                log = PixInfraction.Log.ResourceMaker(json.log);
            }
            if (subscription.Contains("pix-request"))
            {
                log = PixRequest.Log.ResourceMaker(json.log);
            }
            if (subscription.Contains("pix-reversal"))
            {
                log = PixReversal.Log.ResourceMaker(json.log);
            }
            if (subscription.Contains("credit-note"))
            {
                log = CreditNote.Log.ResourceMaker(json.log);
            }
            if (subscription.Contains("issuing-card"))
            {
                log = IssuingCard.Log.ResourceMaker(json.log);
            }
            if (subscription.Contains("issuing-invoice"))
            {
                log = IssuingInvoice.Log.ResourceMaker(json.log);
            }
            if (subscription.Contains("issuing-purchase"))
            {
                log = IssuingPurchase.Log.ResourceMaker(json.log);
            }

            return new Event(
                id: id, isDelivered: isDelivered, subscription: subscription, created: created, log: log,
                workspaceId: workspaceId
            );
        }
    }
}
