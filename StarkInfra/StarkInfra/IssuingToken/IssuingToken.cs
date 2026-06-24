using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingToken object
    /// <br/>
    /// The IssuingToken object displays the information of the tokens created in your Workspace.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when IssuingToken is created. ex: "5656565656565656"</item>
    ///     <item>CardID [string]: card ID which the token is bounded to. ex: "5656565656565656"</item>
    ///     <item>WalletID [string]: wallet provider which the token is bounded to. ex: "google"</item>
    ///     <item>WalletName [string]: wallet name. ex: "GOOGLE"</item>
    ///     <item>MerchantID [string]: merchant unique id. ex: "5656565656565656"</item>
    ///     <item>ExternalID [string]: a unique string among all your IssuingTokens, used to avoid resource duplication. ex: "DSHRMC00002626944b0e3b539d4d459281bdba90c2588791"</item>
    ///     <item>Tags [list of strings]: list of strings for reference when searching for IssuingToken. ex: new List<string>{ "employees", "monthly" }</item>
    ///     <item>Status [string]: current IssuingToken status. ex: "active", "blocked", "canceled", "frozen" or "pending"</item>
    ///     <item>ActivationCode [string]: activation code received through the bank app or sms. ex: "481632"</item>
    ///     <item>MethodCode [string]: provisioning method. Options: "app", "token", "manual", "server" or "browser"</item>
    ///     <item>DeviceType [string]: device type used for tokenization. ex: "Phone"</item>
    ///     <item>DeviceName [string]: device name used for tokenization. ex: "My phone"</item>
    ///     <item>DeviceSerialNumber [string]: device serial number used for tokenization. ex: "2F6D63"</item>
    ///     <item>DeviceOsName [string]: device operational system name used for tokenization. ex: "Android"</item>
    ///     <item>DeviceOsVersion [string]: device operational system version used for tokenization. ex: "4.4.4"</item>
    ///     <item>DeviceImei [string]: device imei used for tokenization. ex: "352099001761481"</item>
    ///     <item>WalletInstanceID [string]: unique id refered to the wallet app in the current device. ex: "71583be4777eb89aaf0345eebeb82594f096615ed17862d0"</item>
    ///     <item>Url [string]: token URL. ex: "https://token.starkinfra.com/5656565656565656"</item>
    ///     <item>WalletDeviceScore [float]: wallet device score. ex: 7.6</item>
    ///     <item>WalletAccountScore [float]: wallet account score. ex: 7.6</item>
    ///     <item>Updated [DateTime]: latest update datetime for the IssuingToken. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the IssuingToken. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class IssuingToken : Resource
    {
        public string CardID { get; }
        public string WalletID { get; }
        public string WalletName { get; }
        public string MerchantID { get; }
        public string ExternalID { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public string ActivationCode { get; }
        public string MethodCode { get; }
        public string DeviceType { get; }
        public string DeviceName { get; }
        public string DeviceSerialNumber { get; }
        public string DeviceOsName { get; }
        public string DeviceOsVersion { get; }
        public string DeviceImei { get; }
        public string WalletInstanceID { get; }
        public string Url { get; }
        public double? WalletDeviceScore { get; }
        public double? WalletAccountScore { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// IssuingToken object
        /// <br/>
        /// The IssuingToken object displays the information of the tokens created in your Workspace.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when IssuingToken is created. ex: "5656565656565656"</item>
        ///     <item>cardID [string]: card ID which the token is bounded to. ex: "5656565656565656"</item>
        ///     <item>walletID [string]: wallet provider which the token is bounded to. ex: "google"</item>
        ///     <item>walletName [string]: wallet name. ex: "GOOGLE"</item>
        ///     <item>merchantID [string]: merchant unique id. ex: "5656565656565656"</item>
        ///     <item>updated [DateTime]: latest update datetime for the IssuingToken. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the IssuingToken. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>url [string]: token URL. ex: "https://token.starkinfra.com/5656565656565656"</item>
        ///     <item>walletDeviceScore [float]: wallet device score. ex: 7.6</item>
        ///     <item>walletAccountScore [float]: wallet account score. ex: 7.6</item>
        /// </list>
        /// Attributes (authorization request only):
        /// <list>
        ///     <item>externalID [string]: a unique string among all your IssuingTokens, used to avoid resource duplication. ex: "DSHRMC00002626944b0e3b539d4d459281bdba90c2588791"</item>
        ///     <item>tags [list of strings]: list of strings for reference when searching for IssuingToken. ex: new List<string>{ "employees", "monthly" }</item>
        ///     <item>status [string]: current IssuingToken status. ex: "active", "blocked", "canceled", "frozen" or "pending"</item>
        ///     <item>activationCode [string]: activation code received through the bank app or sms. ex: "481632"</item>
        ///     <item>methodCode [string]: provisioning method. Options: "app", "token", "manual", "server" or "browser"</item>
        ///     <item>deviceType [string]: device type used for tokenization. ex: "Phone"</item>
        ///     <item>deviceName [string]: device name used for tokenization. ex: "My phone"</item>
        ///     <item>deviceSerialNumber [string]: device serial number used for tokenization. ex: "2F6D63"</item>
        ///     <item>deviceOsName [string]: device operational system name used for tokenization. ex: "Android"</item>
        ///     <item>deviceOsVersion [string]: device operational system version used for tokenization. ex: "4.4.4"</item>
        ///     <item>deviceImei [string]: device imei used for tokenization. ex: "352099001761481"</item>
        ///     <item>walletInstanceID [string]: unique id refered to the wallet app in the current device. ex: "71583be4777eb89aaf0345eebeb82594f096615ed17862d0"</item>
        /// </list>
        /// </summary>
        public IssuingToken(string id = null, string cardID = null, string walletID = null, string walletName = null,
            string merchantID = null, string externalID = null, List<string> tags = null, string status = null,
            string activationCode = null, string methodCode = null, string deviceType = null, string deviceName = null,
            string deviceSerialNumber = null, string deviceOsName = null, string deviceOsVersion = null, string deviceImei = null,
            string walletInstanceID = null, string url = null, double? walletDeviceScore = null, double? walletAccountScore = null,
            DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            CardID = cardID;
            WalletID = walletID;
            WalletName = walletName;
            MerchantID = merchantID;
            ExternalID = externalID;
            Tags = tags;
            Status = status;
            ActivationCode = activationCode;
            MethodCode = methodCode;
            DeviceType = deviceType;
            DeviceName = deviceName;
            DeviceSerialNumber = deviceSerialNumber;
            DeviceOsName = deviceOsName;
            DeviceOsVersion = deviceOsVersion;
            DeviceImei = deviceImei;
            WalletInstanceID = walletInstanceID;
            Url = url;
            WalletDeviceScore = walletDeviceScore;
            WalletAccountScore = walletAccountScore;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Retrieve a specific IssuingToken by its id
        /// <br/>
        /// Receive a single IssuingToken object previously created in the Stark Infra API by passing its id
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
        ///     <item>IssuingToken object that corresponds to the given id.</item>
        /// </list>
        /// </summary>
        public static IssuingToken Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingToken;
        }

        /// <summary>
        /// Retrieve IssuingToken objects
        /// <br/>
        /// Receive an IEnumerable of IssuingToken objects previously created in the Stark Infra API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "active", "blocked", "canceled", "frozen", "pending" }</item>
        ///     <item>cardIds [list of strings, default null]: list of card_ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>externalIds [list of strings, default null]: list of external ids to filter retrieved objects. ex: new List<string>{ "my-token-1", "my-token-2" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of IssuingToken objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<IssuingToken> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> status = null, List<string> cardIds = null, List<string> tags = null, List<string> ids = null,
            List<string> externalIds = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "status", status },
                    { "cardIds", cardIds },
                    { "tags", tags },
                    { "ids", ids },
                    { "externalIds", externalIds }
                },
                user: user
            ).Cast<IssuingToken>();
        }

        /// <summary>
        /// Retrieve paged IssuingToken objects
        /// <br/>
        /// Receive a list of up to 100 IssuingToken objects previously created in the Stark Infra API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default 100]: maximum number of objects to be retrieved. Max = 100. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>status [list of strings, default null]: filter for status of retrieved objects. ex: new List<string>{ "active", "blocked", "canceled", "frozen", "pending" }</item>
        ///     <item>cardIds [list of strings, default null]: list of card_ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: new List<string>{ "5656565656565656", "4545454545454545" }</item>
        ///     <item>externalIds [list of strings, default null]: list of external ids to filter retrieved objects. ex: new List<string>{ "my-token-1", "my-token-2" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of IssuingToken objects with updated attributes</item>
        ///     <item>cursor to retrieve the next page of IssuingToken objects</item>
        /// </list>
        /// </summary>
        public static (List<IssuingToken> page, string pageCursor) Page(string cursor = null, int? limit = null,
            DateTime? after = null, DateTime? before = null, List<string> status = null, List<string> cardIds = null,
            List<string> tags = null, List<string> ids = null, List<string> externalIds = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", new StarkDate(after) },
                    { "before", new StarkDate(before) },
                    { "status", status },
                    { "cardIds", cardIds },
                    { "tags", tags },
                    { "ids", ids },
                    { "externalIds", externalIds }
                },
                user: user
            );
            List<IssuingToken> tokens = new List<IssuingToken>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                tokens.Add(subResource as IssuingToken);
            }
            return (tokens, pageCursor);
        }

        /// <summary>
        /// Update IssuingToken entity
        /// <br/>
        /// Update an IssuingToken by passing its id.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: IssuingToken id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>status [string, default null]: You may block the IssuingToken by passing "blocked" or activate by passing "active" in the status. ex: "active", "blocked"</item>
        ///     <item>tags [list of strings, default null]: list of strings for tagging. ex: new List<string>{ "travel", "food" }</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>target IssuingToken with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingToken Update(string id, string status = null, List<string> tags = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                payload: new Dictionary<string, object> {
                    { "status", status },
                    { "tags", tags }
                },
                user: user
            ) as IssuingToken;
        }

        /// <summary>
        /// Cancel an IssuingToken entity
        /// <br/>
        /// Cancel an IssuingToken entity previously created in the Stark Infra API by its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: IssuingToken unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>canceled IssuingToken object</item>
        /// </list>
        /// </summary>
        public static IssuingToken Cancel(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as IssuingToken;
        }

        /// <summary>
        /// Create a single verified IssuingToken request from a content string
        /// <br/>
        /// Use this method to parse and verify the authenticity of the request received at the informed endpoint.
        /// Token requests are posted to your registered endpoint whenever IssuingTokens are received.
        /// If the provided digital signature does not check out with the StarkInfra public key, a Error.InvalidSignatureException will be raised.
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
        ///     <item>Parsed IssuingToken object</item>
        /// </list>
        /// </summary>
        public static IssuingToken Parse(string content, string signature, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Parse.ParseAndVerify(
                content: content,
                signature: signature,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                user: user,
                key: ""
            ) as IssuingToken;
        }

        /// <summary>
        /// Helps you respond to an IssuingToken authorization request
        /// <br/>
        /// When a new tokenization is triggered by your user, a POST request will be made to your registered URL to get your decision to complete the tokenization.
        /// The POST request must be answered in the following format, within 2 seconds, and with an HTTP status code 200.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>status [string]: sub-issuer response to the authorization. ex: "approved" or "denied"</item>
        /// </list>
        /// Parameters (conditionally required):
        /// <list>
        ///     <item>reason [string, default null]: denial reason. Options: "other", "bruteForce", "subIssuerError", "lostCard", "invalidCard", "invalidHolder", "expiredCard", "canceledCard", "blockedCard", "invalidExpiration", "invalidSecurityCode", "missingTokenAuthorizationUrl", "maxCardTriesExceeded", "maxWalletInstanceTriesExceeded"</item>
        ///     <item>activationMethods [list of dictionaries, default null]: list of dictionaries with "type":string and "value":string pairs</item>
        ///     <item>designId [string, default null]: design unique id. ex: "5656565656565656"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved object. ex: new List<string>{ "tony", "stark" }</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Dumped JSON string that must be returned to us on the IssuingToken request</item>
        /// </list>
        /// </summary>
        public static string ResponseAuthorization(string status, string reason = null,
            List<Dictionary<string, object>> activationMethods = null, string designId = null, List<string> tags = null)
        {
            Dictionary<string, object> rawResponse = new Dictionary<string, object>
            {
                {"authorization", new Dictionary<string, object>
                    {
                        {"status", status},
                        {"reason", reason ?? ""},
                        {"activationMethods", activationMethods},
                        {"designId", designId},
                        {"tags", tags}
                    }
                }
            };
            Dictionary<string, object> response = StarkCore.Utils.Api.CastJsonToApiFormat(rawResponse);
            return JsonConvert.SerializeObject(response);
        }

        /// <summary>
        /// Helps you respond to an IssuingToken activation request
        /// <br/>
        /// When a new token activation is triggered by your user, a POST request will be made to your registered URL for you to confirm the activation code you informed to them.
        /// The POST request must be answered in the following format, within 2 seconds, and with an HTTP status code 200.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>status [string]: sub-issuer response to the activation. ex: "approved" or "denied"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>reason [string, default null]: denial reason. Options: "other", "bruteForce", "subIssuerError", "lostCard", "invalidCard", "invalidHolder", "expiredCard", "canceledCard", "blockedCard", "invalidExpiration", "invalidSecurityCode", "missingTokenAuthorizationUrl", "maxCardTriesExceeded", "maxWalletInstanceTriesExceeded"</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved object. ex: new List<string>{ "tony", "stark" }</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Dumped JSON string that must be returned to us on the IssuingToken request</item>
        /// </list>
        /// </summary>
        public static string ResponseActivation(string status, string reason = null, List<string> tags = null)
        {
            Dictionary<string, object> rawResponse = new Dictionary<string, object>
            {
                {"authorization", new Dictionary<string, object>
                    {
                        {"status", status},
                        {"reason", reason ?? ""},
                        {"tags", tags}
                    }
                }
            };
            Dictionary<string, object> response = StarkCore.Utils.Api.CastJsonToApiFormat(rawResponse);
            return JsonConvert.SerializeObject(response);
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingToken", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string cardID = json.cardId;
            string walletID = json.walletId;
            string walletName = json.walletName;
            string merchantID = json.merchantId;
            string externalID = json.externalId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            string status = json.status;
            string activationCode = json.activationCode;
            string methodCode = json.methodCode;
            string deviceType = json.deviceType;
            string deviceName = json.deviceName;
            string deviceSerialNumber = json.deviceSerialNumber;
            string deviceOsName = json.deviceOsName;
            string deviceOsVersion = json.deviceOsVersion;
            string deviceImei = json.deviceImei;
            string walletInstanceID = json.walletInstanceId;
            string url = json.url;
            double? walletDeviceScore = json.walletDeviceScore;
            double? walletAccountScore = json.walletAccountScore;
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);

            return new IssuingToken(
                id: id, cardID: cardID, walletID: walletID, walletName: walletName, merchantID: merchantID,
                externalID: externalID, tags: tags, status: status, activationCode: activationCode, methodCode: methodCode,
                deviceType: deviceType, deviceName: deviceName, deviceSerialNumber: deviceSerialNumber,
                deviceOsName: deviceOsName, deviceOsVersion: deviceOsVersion, deviceImei: deviceImei,
                walletInstanceID: walletInstanceID, url: url, walletDeviceScore: walletDeviceScore,
                walletAccountScore: walletAccountScore, updated: updated, created: created
            );
        }
    }
}
