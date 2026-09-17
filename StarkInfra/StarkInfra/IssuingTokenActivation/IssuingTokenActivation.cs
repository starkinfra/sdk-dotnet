using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// IssuingTokenActivation object
    /// <br/>
    /// The IssuingTokenActivation object displays the necessary information to proceed with the card tokenization.
    /// You will receive this object at your registered URL to notify you which method your user want to receive the activation code.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>CardID [string]: card ID which the token is bounded to. ex: "5656565656565656"</item>
    ///     <item>TokenID [string]: token unique id. ex: "5656565656565656"</item>
    ///     <item>Tags [list of strings]: tags to filter retrieved object. ex: new List<string>{ "tony", "stark" }</item>
    ///     <item>ActivationMethod [Dictionary<string, object>]: dictionary object with "type":string and "value":string pairs</item>
    /// </list>
    /// </summary>
    public partial class IssuingTokenActivation : StarkCore.Utils.SubResource
    {
        public string CardID { get; }
        public string TokenID { get; }
        public List<string> Tags { get; }
        public Dictionary<string, object> ActivationMethod { get; }

        /// <summary>
        /// IssuingTokenActivation object
        /// <br/>
        /// The IssuingTokenActivation object displays the necessary information to proceed with the card tokenization.
        /// You will receive this object at your registered URL to notify you which method your user want to receive the activation code.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>cardID [string]: card ID which the token is bounded to. ex: "5656565656565656"</item>
        ///     <item>tokenID [string]: token unique id. ex: "5656565656565656"</item>
        ///     <item>tags [list of strings]: tags to filter retrieved object. ex: new List<string>{ "tony", "stark" }</item>
        ///     <item>activationMethod [Dictionary<string, object>]: dictionary object with "type":string and "value":string pairs</item>
        /// </list>
        /// </summary>
        public IssuingTokenActivation(string cardID = null, string tokenID = null, List<string> tags = null, Dictionary<string, object> activationMethod = null)
        {
            CardID = cardID;
            TokenID = tokenID;
            Tags = tags;
            ActivationMethod = activationMethod;
        }

        /// <summary>
        /// Create a single verified IssuingTokenActivation object from a content string
        /// <br/>
        /// Use this method to parse and verify the authenticity of the notification received at the informed endpoint.
        /// If the provided digital signature does not check out with the StarkInfra public key, a StarkCore.Error.InvalidSignatureError will be raised.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>content [string]: response content from request received at user endpoint (not parsed)</item>
        ///     <item>signature [string]: base-64 digital signature received at response header "Digital-Signature"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>Parsed IssuingTokenActivation object</item>
        /// </list>
        /// </summary>
        public static IssuingTokenActivation Parse(string content, string signature, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Utils.Parse.ParseAndVerify(
                content: content,
                signature: signature,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                user: user
            ) as IssuingTokenActivation;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "IssuingTokenActivation", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            string cardID = json.cardId;
            string tokenID = json.tokenId;
            List<string> tags = json.tags?.ToObject<List<string>>();
            Dictionary<string, object> activationMethod = json.activationMethod?.ToObject<Dictionary<string, object>>();

            return new IssuingTokenActivation(
                cardID: cardID, tokenID: tokenID, tags: tags, activationMethod: activationMethod
            );
        }
    }
}
