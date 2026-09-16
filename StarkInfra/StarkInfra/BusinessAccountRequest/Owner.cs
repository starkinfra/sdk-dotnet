using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// BusinessAccountRequest.Owner object
    /// <br/>
    /// The Owner object represents a company owner referenced by a BusinessAccountRequest. Each owner
    /// completes its own identity verification through an independent webview. It is embedded on the
    /// parent's Owners field and has no endpoints of its own.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>TaxID [string]: owner's tax ID (CPF). ex: "012.345.678-90"</item>
    ///     <item>Name [string]: owner's full name (minimum 5 characters). ex: "Jamie Lannister"</item>
    ///     <item>Role [string]: owner's role in the company. ex: "partner", "representative"</item>
    ///     <item>IdentityID [string]: unique id of the identity verification linked to this owner. ex: "5709594221805568"</item>
    ///     <item>ValidatorLink [string]: webview link to be delivered to the owner to complete biometrics and document capture. Treat it as a credential: deliver it through a secure channel and never log it.</item>
    ///     <item>Status [string]: current status of the owner verification. ex: "created", "approved", "denied"</item>
    /// </list>
    /// </summary>
    public partial class Owner : StarkCore.Utils.SubResource
    {
        public string TaxID { get; }
        public string Name { get; }
        public string Role { get; }
        public string IdentityID { get; }
        public string ValidatorLink { get; }
        public string Status { get; }

        /// <summary>
        /// BusinessAccountRequest.Owner object
        /// <br/>
        /// The Owner object represents a company owner referenced by a BusinessAccountRequest. Each owner
        /// completes its own identity verification through an independent webview. It is embedded on the
        /// parent's owners field and has no endpoints of its own.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>taxID [string]: owner's tax ID (CPF). ex: "012.345.678-90"</item>
        ///     <item>name [string]: owner's full name (minimum 5 characters). ex: "Jamie Lannister"</item>
        ///     <item>role [string]: owner's role in the company. Options: "partner", "representative"</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>identityID [string]: unique id of the identity verification linked to this owner. ex: "5709594221805568"</item>
        ///     <item>validatorLink [string]: webview link to be delivered to the owner to complete biometrics and document capture. Treat it as a credential: deliver it through a secure channel and never log it.</item>
        ///     <item>status [string]: current status of the owner verification. Options: "created", "approved", "denied"</item>
        /// </list>
        /// </summary>
        public Owner(
            string taxID, string name, string role, string identityID = null, string validatorLink = null,
            string status = null
        )
        {
            TaxID = taxID;
            Name = name;
            Role = role;
            IdentityID = identityID;
            ValidatorLink = validatorLink;
            Status = status;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Owner", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            string taxID = json.taxId;
            string name = json.name;
            string role = json.role;
            string identityID = json.identityId;
            string validatorLink = json.validatorLink;
            string status = json.status;

            return new Owner(
                taxID: taxID, name: name, role: role, identityID: identityID, validatorLink: validatorLink,
                status: status
            );
        }
    }
}
