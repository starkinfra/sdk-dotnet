using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingTokenRequest object
    /// <br/>
    /// The IssuingTokenRequest object displays the necessary information to proceed with the card tokenization.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>CardID [string]: card id to be tokenized. ex: "5734340247945216"</item>
    ///     <item>WalletID [string]: desired wallet to be integrated. ex: "google"</item>
    ///     <item>MethodCode [string]: method code. ex: "app" or "manual"</item>
    ///     <item>Content [string]: token request content. ex: "eyJwdWJsaWNLZXlGaW5nZXJwcmludCI6ICJlNTNiZThjZTRhYWQxNWU2OWNmMjExOTA5Mjk4YzJkOTE0O..."</item>
    ///     <item>Signature [string]: token request signature. ex: "eyJwdWJsaWNLZXlGaW5nZXJwcmludCI6ICJlNTNiZThjZTRhYWQxNWU2OWNmMjExOTA5Mjk4YzJkOTE0O..."</item>
    ///     <item>Metadata [dictionary]: dictionary object used to store additional information about the IssuingTokenRequest object.</item>
    /// </list>
    /// </summary>
    public partial class IssuingTokenRequest : StarkCore.Utils.SubResource
    {
        public string CardID { get; }
        public string WalletID { get; }
        public string MethodCode { get; }
        public string Content { get; }
        public string Signature { get; }
        public Dictionary<string, object> Metadata { get; }

        /// <summary>
        /// IssuingTokenRequest object
        /// <br/>
        /// The IssuingTokenRequest object displays the necessary information to proceed with the card tokenization.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>cardId [string]: card id to be tokenized. ex: "5734340247945216"</item>
        ///     <item>walletId [string]: desired wallet to be integrated. ex: "google"</item>
        ///     <item>methodCode [string]: method code. ex: "app" or "manual"</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>content [string]: token request content. ex: "eyJwdWJsaWNLZXlGaW5nZXJwcmludCI6ICJlNTNiZThjZTRhYWQxNWU2OWNmMjExOTA5Mjk4YzJkOTE0O..."</item>
        ///     <item>signature [string]: token request signature. ex: "eyJwdWJsaWNLZXlGaW5nZXJwcmludCI6ICJlNTNiZThjZTRhYWQxNWU2OWNmMjExOTA5Mjk4YzJkOTE0O..."</item>
        ///     <item>metadata [dictionary]: dictionary object used to store additional information about the IssuingTokenRequest object.</item>
        /// </list>
        /// </summary>
        public IssuingTokenRequest(string cardId, string walletId, string methodCode, string content = null,
            string signature = null, Dictionary<string, object> metadata = null)
        {
            CardID = cardId;
            WalletID = walletId;
            MethodCode = methodCode;
            Content = content;
            Signature = signature;
            Metadata = metadata;
        }

        /// <summary>
        /// Create an IssuingTokenRequest object
        /// <br/>
        /// Send an IssuingTokenRequest object to Stark Infra API to create the payload to proceed with the card tokenization.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>request [IssuingTokenRequest object]: IssuingTokenRequest object to the API to generate the payload</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IssuingTokenRequest object with updated attributes</item>
        /// </list>
        /// </summary>
        public static IssuingTokenRequest Create(IssuingTokenRequest request, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: request,
                user: user
            ) as IssuingTokenRequest;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingTokenRequest", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            string cardId = json.cardId;
            string walletId = json.walletId;
            string methodCode = json.methodCode;
            string content = json.content;
            string signature = json.signature;
            Dictionary<string, object> metadata = json.metadata is null ? null : json.metadata.ToObject<Dictionary<string, object>>();

            return new IssuingTokenRequest(
                cardId: cardId, walletId: walletId, methodCode: methodCode,
                content: content, signature: signature, metadata: metadata
            );
        }
    }
}
