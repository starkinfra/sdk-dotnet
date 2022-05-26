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
    public partial class Event : Utils.Resource
    {
        public Utils.Resource Log { get; }
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
        public Event(string id, Utils.Resource log, bool? isDelivered, string subscription, string workspaceId,
            DateTime? created = null) : base(id)
        {
            Log = log;
            Created = created;
            IsDelivered = isDelivered;
            Subscription = subscription;
            WorkspaceId = workspaceId;
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
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
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
        
        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Event", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            bool? isDelivered = json.isDelivered;
            string subscription = json.subscription;
            string createdString = json.created;
            string workspaceId = json.workspaceId;
            DateTime created = Utils.Checks.CheckDateTime(createdString);

            Utils.Resource log = null;
            
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

            return new Event(
                id: id, isDelivered: isDelivered, subscription: subscription, created: created, log: log,
                workspaceId: workspaceId
            );
        }
    }
}
