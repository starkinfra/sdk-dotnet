using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using EllipticCurve;


namespace StarkInfra.Utils
{
    internal static class Parse
    {
        internal static SubResource ParseAndVerify(string content, string signature, string resourceName, 
            Api.ResourceMaker resourceMaker, User user, string key = null)
        {
            dynamic json = Utils.Json.Decode(content);
            if (key != null)
            {
                json = json[key]; 
            }

            Signature signatureObject;
            try
            {
                signatureObject = Signature.fromBase64(signature);
            } catch
            {
                throw new Error.InvalidSignatureError("The provided signature is not valid");
            }

            if (VerifySignature(content, signatureObject, user)) {
                return Api.FromApiJson(resourceMaker, json);
            }
            if (VerifySignature(content, signatureObject, user, true)) {
                return Api.FromApiJson(resourceMaker, json);
            }

            throw new Error.InvalidSignatureError("The provided signature and content do not match the Stark Infra public key");
        }
        
        private static bool VerifySignature(string content, Signature signature, User user, bool refresh = false)
        {
            
            PublicKey publicKey = Utils.Cache.StarkInfraPublicKey;

            if (publicKey is null || refresh)
            {
                publicKey = GetPublicKeyPem(user);
            }

            return Ecdsa.verify(content, signature, publicKey);
        }

        private static PublicKey GetPublicKeyPem(User user)
        {
            dynamic json = Utils.Request.Fetch(
                method: Utils.Request.Get,
                path: "public-key",
                query: new Dictionary<string, object> { { "limit", 1 } },
                user: user
            ).Json();
            List<JObject> publicKeys = json.publicKeys.ToObject<List<JObject>>();
            dynamic publicKey = publicKeys.First();
            string content = publicKey.content;
            PublicKey publicKeyObject = PublicKey.fromPem(content);
            Utils.Cache.StarkInfraPublicKey = publicKeyObject;
            return publicKeyObject;
        }
    }
}
