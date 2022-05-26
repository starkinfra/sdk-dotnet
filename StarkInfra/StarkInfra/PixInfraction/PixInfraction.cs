using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixInfraction object
    /// <br/>
    /// PixInfraction objects are used to report transactions that are suspected of
    /// fraud, to request a refund or to reverse a refund.
    /// When you initialize a PixInfraction, the entity will not be automatically
    /// created in the Stark Infra API.The 'create' function sends the objects
    /// to the Stark Infra API and returns the created object.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ReferenceID [string]: endToEndId or returnId of the transaction being reported. ex: "E20018183202201201450u34sDGd19lz"</item>
    ///     <item>Type [string]: type of infraction report. Options: "fraud", "reversal", "reversalChargeback"</item>
    ///     <item>Description [string, Default null]: description for any details that can help with the infraction investigation.</item>
    ///     <item>ID [string]: unique id returned when the PixInfraction is created. ex: "5656565656565656"</item>
    ///     <item>CreditedBankCode [string]: bankCode of the credited Pix participant in the reported transaction. ex: "20018183"</item>
    ///     <item>Agent [string]: Options: "reporter" if you created the PixInfraction, "reported" if you received the PixInfraction.</item>
    ///     <item>Analysis [string]: analysis that led to the result.</item>
    ///     <item>BacenID [string]: central bank's unique UUID that identifies the infraction report.</item>
    ///     <item>DebitedBankCode [string]: bankCode of the debited Pix participant in the reported transaction. ex: "20018183"</item>
    ///     <item>ReportedBy [string]: agent that reported the PixInfraction. Options: "debited", "credited".</item>
    ///     <item>Result [string]: result after the analysis of the PixInfraction by the receiving party. Options: "agreed", "disagreed"</item>
    ///     <item>Status [string]: current PixInfraction status. Options: "created", "failed", "delivered", "closed", "canceled".</item>
    ///     <item>Created [DateTime]: creation datetime for the PixInfraction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Updated [DateTime]: latest update datetime for the PixInfraction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class PixInfraction : Resource
    {
        public string ReferenceID { get; }
        public string Type { get; }
        public string Description { get; }
        public string CreditedBankCode { get; }
        public string Agent { get; }
        public string Analysis { get; }
        public string BacenID { get; }
        public string DebitedBankCode { get; }
        public string ReportedBy { get; }
        public string Result { get; }
        public string Status { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// PixInfraction object
        /// <br/>
        /// <br/>
        /// Infraction reports are used to report transactions that are suspected of
        /// fraud, to request a refund or to reverse a refund.
        /// When you initialize a PixInfraction, the entity will not be automatically
        /// created in the Stark Infra API.The 'create' function sends the objects
        /// to the Stark Infra API and returns the created object.
        /// <br/>
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>referenceId [string]: endToEndId or returnId of the transaction being reported. ex: "E20018183202201201450u34sDGd19lz"</item>
        ///     <item>type [string]: type of infraction report. Options: "fraud", "reversal", "reversalChargeback"</item>
        ///</list>
        /// Parameters (optional):
        /// <list>
        ///     <item>description [string, Default null]: description for any details that can help with the infraction investigation.</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>creditedBankCode [string]: bankCode of the credited Pix participant in the reported transaction. ex: "20018183"</item>
        ///     <item>agent [string]: Options: "reporter" if you created the PixInfraction, "reported" if you received the PixInfraction.</item>
        ///     <item>analysis [string]: analysis that led to the result.</item>
        ///     <item>bacenId [string]: central bank's unique UUID that identifies the infraction report.</item>
        ///     <item>debitedBankCode [string]: bankCode of the debited Pix participant in the reported transaction. ex: "20018183"</item>
        ///     <item>id [string]: unique id returned when the PixInfraction is created. ex: "5656565656565656"</item>
        ///     <item>reportedBy [string]: agent that reported the PixInfraction. Options: "debited", "credited".</item>
        ///     <item>result [string]: result after the analysis of the PixInfraction by the receiving party. Options: "agreed", "disagreed"</item>
        ///     <item>status [string]: current PixInfraction status. Options: "created", "failed", "delivered", "closed", "canceled".</item>
        ///     <item>created [DateTime]: creation datetime for the PixInfraction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>updated [DateTime]: latest update datetime for the PixInfraction. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public PixInfraction(string referenceId, string type, string description = null, string creditedBankCode = null, 
            string agent = null, string analysis = null, string bacenId = null, string debitedBankCode = null, 
            string reportedBy = null, string result = null, string status = null,  DateTime? updated = null, 
            DateTime? created = null, string id = null) : base(id)
        {
            ReferenceID = referenceId;
            Type = type;
            Description = description;
            CreditedBankCode = creditedBankCode;
            Agent = agent;
            Analysis = analysis;
            BacenID = bacenId;
            DebitedBankCode = debitedBankCode;
            ReportedBy = reportedBy;
            Result = result;
            Status = status;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create PixInfractions
        /// <br/>
        /// Create PixInfraction objects in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>infractions [list of PixInfraction object]: list of PixInfraction objects to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>List of PixInfraction object with updated attributes.</item>
        /// </list>
        /// </summary>
        public static List<PixInfraction> Create(List<PixInfraction> infractions, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: infractions,
                user: user
            ).ToList().ConvertAll(o => (PixInfraction)o);
        }
        
        /// <summary>
        /// Create PixInfractions
        /// <br/>
        /// Send PixInfraction dictionaries for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>infractions [list of disctionaries]: list of PixInfraction dictionaries to be created in the API.</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>List of PixInfraction object with updated attributes.</item>
        /// </list>
        /// </summary>
        public static List<PixInfraction> Create(List<Dictionary<string, object>> infractions, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: infractions,
                user: user
            ).ToList().ConvertAll(o => (PixInfraction)o);
        }

        /// <summary>
        /// Retrieve a specific PixInfraction
        /// <br/>
        /// Retrieve a PixInfraction object linked to your Workspace in the Stark Infra API using its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixInfraction object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static PixInfraction Get(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixInfraction;
        }

        /// <summary>
        /// Retrieve PixInfractions
        /// <br/>
        /// Receive a IEnumerable of PixInfraction objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created after a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created before a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. Options: "created", "failed", "delivered", "closed", "canceled".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>type [list of strings, default null]: filter for the type of retrieved PixInfractions. Options: "fraud", "reversal", "reversalChargeback"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of PixInfraction objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<PixInfraction> Query(int? limit = null, DateTime? after = null, DateTime? before = null, string status = null, 
            List<string> ids = null, string type = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "status", status },
                    { "after", after },
                    { "before", before },
                    { "type", type },
                    { "ids", ids },
                    { "limit", limit },
                    { "user", user }
                },
                user: user
            ).Cast<PixInfraction>();
        }

        /// <summary>
        /// Retrieve paged PixInfractions
        /// <br/>
        /// Receive a list of up to 100 PixInfraction objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created after a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created before a specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. Options: "created", "failed", "delivered", "closed", "canceled".</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>type [list of strings, default null]: filter for the type of retrieved PixInfractions. Options: "fraud", "reversal", "reversalChargeback"</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PixInfraction objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of PixInfraction objects</item>
        /// </list>
        /// </summary>
        public static (List<PixInfraction> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null, 
            DateTime? before = null, string status = null, List<string> ids = null, string type = null, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            (List<SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "status", status },
                    { "after", after },
                    { "before", before },
                    { "type", type },
                    { "ids", ids },
                    { "limit", limit },
                    { "user", user }
                },
                user: user
            );
            List<PixInfraction> infractions = new List<PixInfraction>();
            foreach (SubResource subResource in page)
            {
                infractions.Add(subResource as PixInfraction);
            }
            return (infractions, pageCursor);
        }

        /// <summary>
        /// Update PixInfraction entity
        /// <br/>
        /// Update a PixInfraction by passing id.
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id [string]: PixInfraction id. ex: '5656565656565656'</item>
        ///     <item>result [string]: result after the analysis of the PixInfraction. Options: "agreed", "disagreed"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>patchData Dictionary of parameters</item>
        ///     <list>
        ///         <item>analysis [string, default null]: analysis that led to the result.</item>
        ///     </list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixInfraction with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixInfraction Update(string id, string result, Dictionary<string, object> patchData = null, User user = null)
        {
            if (patchData == null)
            {
                patchData = new Dictionary<string, object> { };
            }
            patchData.Add("result", result);
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: patchData,
                user: user
            ) as PixInfraction;
        }

        /// <summary>
        /// Cancel a PixInfraction entity
        /// <br/>
        /// Cancel a PixInfraction entity previously created in the Stark Infra API
        /// <br/>
        /// Parameters(required):
        /// <list>
        ///     <item>id[string]: PixInfraction unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user[Organization/Project object, default null]: Project object. Not necessary if StarkInfra.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled PixInfraction object</item>
        /// </list>
        /// </summary>
        public static PixInfraction Cancel(string id, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as PixInfraction;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixInfraction", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string referenceId = json.referenceId;
            string type = json.type;
            string description = json.description;
            string creditedBankCode = json.creditedBankCode;
            string agent = json.agent;
            string analysis = json.analysis;
            string bacenId = json.bacenId;
            string debitedBankCode = json.debitedBankCode;
            string reportedBy = json.reportedBy;
            string result = json.result;
            string status = json.status;
            string createdString = json.created;
            DateTime created = Checks.CheckDateTime(createdString);
            string updatedString = json.updated;
            DateTime updated = Checks.CheckDateTime(updatedString);

            return new PixInfraction(
                id: id, referenceId: referenceId, type: type, description: description, creditedBankCode: creditedBankCode,
                agent: agent, analysis: analysis, bacenId: bacenId, debitedBankCode: debitedBankCode, reportedBy: reportedBy,
                result: result, status: status, updated: updated, created: created
            );
        }
    }
}
