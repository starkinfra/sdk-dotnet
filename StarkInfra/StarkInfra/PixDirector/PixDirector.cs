using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixDirector object
    /// <br/>
    /// Mandatory data that must be registered within the Central Bank for emergency contact purposes.
    /// <br/>
    /// When you initialize a PixDirector, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: name of the PixDirector. ex: "Edward Stark".</item>
    ///     <item>TaxID [string]: tax ID (CPF) of the PixDirector. ex: "012.345.678-90"</item>
    ///     <item>Phone [string]: phone of the PixDirector. ex: "+551198989898"</item>
    ///     <item>Email [string]: email of the PixDirector. ex: "ned.stark@starkbank.com"</item>
    ///     <item>Password [string]: password of the PixDirector. ex: "12345678"</item>
    ///     <item>TeamEmail [string]: team email. ex: "pix.team@company.com"</item>
    ///     <item>TeamPhones [list of strings]: list of phones of the team. ex: new List<string>{ "+5511988889999", "+5511988889998" }</item>
    ///     <item>Status [string]: current PixDirector status. ex: "success"</item>
    /// </list>
    /// </summary>
    public partial class PixDirector : SubResource
    {
        public string Name { get; }
        public string TaxID { get; }
        public string Phone { get; }
        public string Email { get; }
        public string Password { get; }
        public string TeamEmail { get; }
        public List<string> TeamPhones { get; }
        public string Status { get; }

        /// <summary>
        /// PixDirector object
        /// <br/>
        /// Mandatory data that must be registered within the Central Bank for emergency contact purposes.
        /// <br/>
        /// When you initialize a PixDirector, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: name of the PixDirector. ex: "Edward Stark".</item>
        ///     <item>taxID [string]: tax ID (CPF) of the PixDirector. ex: "012.345.678-90"</item>
        ///     <item>phone [string]: phone of the PixDirector. ex: "+551198989898"</item>
        ///     <item>email [string]: email of the PixDirector. ex: "ned.stark@starkbank.com"</item>
        ///     <item>password [string]: password of the PixDirector. ex: "12345678"</item>
        ///     <item>teamEmail [string]: team email. ex: "pix.team@company.com"</item>
        ///     <item>teamPhones [list of strings]: list of phones of the team. ex: List<string>{ "+5511988889999", "+5511988889998" }</item>
        ///</list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>status [string]: current PixDirector status. ex: "success"</item>
        /// </list>
        /// </summary>
        public PixDirector(
            string name, string taxID, string phone, string email, string password, 
            string teamEmail, List<string> teamPhones, string status = null
        ) {
            Name = name;
            TaxID = taxID;
            Phone = phone;
            Email = email;
            Password = password;
            TeamEmail = teamEmail;
            TeamPhones = teamPhones;
            Status = status;
        }

        /// <summary>
        /// Create a PixDirector
        /// <br/>
        /// Send a PixDirector object for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>director [PixDirector Object]: PixDirector object to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixDirector object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixDirector Create(PixDirector director, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: director,
                user: user
            ) as PixDirector;
        }

		/// <summary>
        /// Create a PixDirector
        /// <br/>
        /// Send a PixDirector for creation in the Stark Infra API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>director [dictionary]: PixDirector dictionary to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>PixDirector object with updated attributes</item>
        /// </list>
        /// </summary>
        public static PixDirector Create(Dictionary<string, object> director, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.PostSingle(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: director,
                user: user
            ) as PixDirector;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixDirector", resourceMaker: ResourceMaker);
        }

        internal static SubResource ResourceMaker(dynamic json)
        {
            string name = json.name;
            string taxID = json.taxId;
            string phone = json.phone;
            string email = json.email;
            string password = json.password;
            string teamEmail = json.teamEmail;
            List<string> teamPhones = json.teamPhones;
            string status = json.status;

            return new PixDirector(
                name: name, taxID: taxID, phone: phone, email: email, 
                password: password, teamEmail: teamEmail, teamPhones: teamPhones, 
                status: status
            );
        }
    }
}
