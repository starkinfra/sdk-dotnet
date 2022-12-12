using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// Webhook object
    /// <br/>
    /// A Webhook is used to subscribe to notification events on a user-selected endpoint.
    /// Currently, available services for subscription are contract, credit-note, signer, issuing-card, issuing-invoice, issuing-purchase, pix-request.in, pix-request.out, pix-reversal.in, pix-reversal.out, pix-claim, pix-key, pix-chargeback, pix-infraction
    /// <br/>
    /// Parameters (required):
    /// <list>
    ///     <item>Url [string]: URL that will be notified when an event occurs.</item>
    ///     <item>Subscriptions [list of strings]: list of any non-empty combination of the available services. ex: new List<string>{ "contract", "credit-note", "signer", "issuing-card", "issuing-invoice", "issuing-purchase", "pix-request.in", "pix-request.out", "pix-reversal.in", "pix-reversal.out", "pix-claim", "pix-key", "pix-chargeback", "pix-infraction" }</item>
    /// </list>
    /// Attributes (return-only):
    /// <list>
    ///     <item>ID [string]: unique id returned when the webhook is created. ex: "5656565656565656"</item>
    /// </list>
    /// </summary>
    public partial class Webhook : Resource
    {
        public string Url { get; }
        public List<string> Subscriptions { get; }

        /// <summary>
        /// Webhook object
        /// <br/>
        /// A Webhook is used to subscribe to notification events on a user-selected endpoint.
        /// Currently, available services for subscription are contract, credit-note, signer, issuing-card, issuing-invoice, issuing-purchase, pix-request.in, pix-request.out, pix-reversal.in, pix-reversal.out, pix-claim, pix-key, pix-chargeback, pix-infraction
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>url [string]: Url that will be notified when an event occurs.</item>
        ///     <item>subscriptions [list of strings]: list of any non-empty combination of the available services. ex: new List<string>{ "contract", "credit-note", "signer", "issuing-card", "issuing-invoice", "issuing-purchase", "pix-request.in", "pix-request.out", "pix-reversal.in", "pix-reversal.out", "pix-claim", "pix-key", "pix-chargeback", "pix-infraction" }</item>
        ///</list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the webhook is created. ex: "5656565656565656"</item>
        /// </list>
        /// </summary>
        public Webhook(string url, List<string> subscriptions, string id = null) : base(id)
        {
            Url = url;
            Subscriptions = subscriptions;
        }

        /// <summary>
        /// Create Webhook subscription
        /// <br/>
        /// Send a single Webhook subscription for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>url [string]: url to which notification events will be sent to. ex: "https://webhook.site/60e9c18e-4b5c-4369-bda1-ab5fcd8e1b29"</item>
        ///     <item>subscriptions [list of strings]: list of any non-empty combination of the available services. ex: new List<string>{"contract", "credit-note", "signer" }</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Webhook object with updated attributes.</item>
        /// </list>
        /// </summary>
        public static Webhook Create(string url, List<string> subscription, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            Webhook webhook  = new Webhook(url, subscription);
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: webhook,
                user: user
            ) as Webhook;
        }

        /// <summary>
        /// Retrieve a specific Webhook
        /// <br/>
        /// Receive a single Webhook subscription object previously created in the Stark Infra API by its id
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
        ///     <item>Webhook object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static Webhook Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Webhook;
        }

        /// <summary>
        /// Retrieve Webhook objects
        /// <br/>
        /// Receive a IEnumerable of Webhook objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of Webhook objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<Webhook> Query(int? limit = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit},
                    { "user", user}
                },
                user: user
            ).Cast<Webhook>();
        }

        /// <summary>
        /// Retrieve paged Webhook objects
        /// <br/>
        /// Receive a list of up to 100 Webhook objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Webhook objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of Webhook objects</item>
        /// </list>
        /// </summary>
        public static (List<Webhook> page, string pageCursor) Page(string cursor = null,
            int? limit = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit},
                    { "user", user}
                },
                user: user
            );
            List<Webhook> Webhook = new List<Webhook>();
            foreach (SubResource subResource in page)
            {
                Webhook.Add(subResource as Webhook);
            }
            return (Webhook, pageCursor);
        }

        /// <summary>
        /// Delete a Webhook entity
        /// <br/>
        /// Delete a Webhook entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: Webhook unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Organization/Project object, default null]: Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted Webhook object</item>
        /// </list>
        /// </summary>
        public static Webhook Delete(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as Webhook;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Webhook", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string url = json.url;
            List<string> subscriptions = json.subscriptions.ToObject<List<string>>();

            return new Webhook(id: id, url: url, subscriptions: subscriptions);
        }
    }
}
