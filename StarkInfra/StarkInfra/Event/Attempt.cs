using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;

namespace StarkInfra
{
    public partial class Event
    {
        /// <summary>
        /// Event.Attempt object
        /// <br/>
        /// When an Event delivery fails, an event attempt will be registered.
        /// It carries information meant to help you debug event reception issues.        
        /// <br/>
        /// Attributes:
        /// <list>
        ///     <item>ID [string]: unique id that identifies the delivery attempt. ex: "5656565656565656"</item>
        ///     <item>Code [string]: delivery error code. ex: badHttpStatus, badConnection, timeout</item>
        ///     <item>Message [string]: delivery error full description. ex: "HTTP POST request returned status 404"</item>
        ///     <item>EventID [string]: ID of the Event whose delivery failed. ex: "4848484848484848"</item>
        ///     <item>WebhookID [string]: ID of the Webhook that triggered this event. ex: "5656565656565656"</item>
        ///     <item>Created [DateTime]: DateTime representing the moment when the attempt was made. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public class Attempt : Utils.Resource
        {
            public string Code { get; }
            public string Message { get; }
            public string EventID { get; }
            public string WebhookID { get; }
            public DateTime? Created { get; }

            /// <summary>
            /// Event.Attempt object
            /// <br/>
            /// When an Event delivery fails, an event attempt will be registered.
            /// It carries information meant to help you debug event reception issues.        
            /// <br/>
            /// Attributes (return-only):
            /// <list>
            ///     <item>ID [string]: unique id that identifies the delivery attempt. ex: "5656565656565656"</item>
            ///     <item>Code [string]: delivery error code. ex: badHttpStatus, badConnection, timeout</item>
            ///     <item>Message [string]: delivery error full description. ex: "HTTP POST request returned status 404"</item>
            ///     <item>EventID [string]: ID of the Event whose delivery failed. ex: "4848484848484848"</item>
            ///     <item>WebhookID [string]: ID of the Webhook that triggered this event. ex: "5656565656565656"</item>
            ///     <item>Created [DateTime]: DateTime representing the moment when the attempt was made. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
            /// </list>
            /// </summary>
            public Attempt(string id, string code, string message, string eventID, string webhookID, DateTime? created) : base(id)
            {
                Code = code;
                Message = message;
                EventID = eventID;
                WebhookID = webhookID;
                Created = created;
            }

            /// <summary>
            /// Retrieve a specific Event.Attempt by its id
            /// <br/>
            /// Receive a single Event.Attempt object previously created by the Stark Infra API by its id
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
            ///     <item>Event.Attempt object with updated attributes</item>
            /// </list>
            /// </summary>
            public static Attempt Get(string id, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Utils.Rest.GetId(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    id: id,
                    user: user
                ) as Attempt;
            }

            /// <summary>
            /// Retrieve Event.Attempt objects
            /// <br/>
            /// Receive an IEnumerable of Event.Attempt objects previously created in the Stark Infra API
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>eventIds [list of strings, default null]: list of Event ids to filter attempts. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>webhookIds [list of strings, default null]: list of Webhook ids to filter attempts. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.User.Default was set before function call.</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>IEnumerable of Event.Attempt objects with updated attributes</item>
            /// </list>
            /// </summary>
            public static IEnumerable<Attempt> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
                                                     List<string> eventIds = null, List<string> webhookIds = null, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                return Utils.Rest.GetList(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "eventIds", eventIds },
                        { "webhookIds", webhookIds }
                    },
                    user: user
                ).Cast<Attempt>();
            }

            /// <summary>
            /// Retrieve paged Event.Attempt objects
            /// <br/>
            /// Receive a list of up to 100 Event.Attempt objects previously created in the Stark Infra API and the cursor to the next page.
            /// Use this function instead of query if you want to manually page your requests.
            /// <br/>
            /// Parameters (optional):
            /// <list>
            ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
            ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35.</item>
            ///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
            ///     <item>eventIds [list of strings, default null]: list of Event ids to filter attempts. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>webhookIds [list of strings, default null]: list of Webhook ids to filter attempts. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
            ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.User.Default was set before function call.</item>
            /// </list>
            /// <br/>
            /// Return:
            /// <list>
            ///     <item>list of Event.Attempt objects with updated attributes</item>
            ///     <item>cursor to retrieve the next page of Event.Attempt objects</item>
            /// </list>
            /// </summary>
            public static (List<Attempt> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
                DateTime? before = null, List<string> eventIds = null, List<string> webhookIds = null, User user = null)
            {
                (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
                (List<StarkCore.Utils.SubResource> page, string pageCursor) = Utils.Rest.GetPage(
                    resourceName: resourceName,
                    resourceMaker: resourceMaker,
                    query: new Dictionary<string, object> {
                        { "cursor", cursor },
                        { "limit", limit },
                        { "after", new StarkDate(after) },
                        { "before", new StarkDate(before) },
                        { "eventIds", eventIds },
                        { "webhookIds", webhookIds }
                    },
                    user: user
                );
                List<Attempt> attempts = new List<Attempt>();
                foreach (StarkCore.Utils.SubResource subResource in page)
                {
                    attempts.Add(subResource as Attempt);
                }
                return (attempts, pageCursor);
            }

            internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
            {
                return (resourceName: "EventAttempt", resourceMaker: ResourceMaker);
            }

            internal static Utils.Resource ResourceMaker(dynamic json)
            {
                string id = json.id;
                string code = json.code;
                string message = json.message;
                string eventID = json.eventId;
                string webhookID = json.webhookId;
                string createdString = json.created;
                DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);

                return new Attempt(id, code, message, eventID, webhookID, created);
            }
        }
    }
}
