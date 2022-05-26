using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// PixDirector object
    /// <br/>
    /// Mandatory data that must be registered within the Central Bank for emergency contact purposes.
    /// When you initialize a PixDirector, the entity will not be automatically
    /// created in the Stark Infra API. The 'create' function sends the objects
    /// to the Stark Infra API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>name [string]: name of the PixDirector. ex: "Edward Stark".</item>
    ///     <item>taxId [string]: tax ID (CPF) of the PixDirector. ex: "012.345.678-90"</item>
    ///     <item>phone [string]: phone of the PixDirector. ex: "+551198989898"</item>
    ///     <item>email [string]: email of the PixDirector. ex: "ned.stark@starkbank.com"</item>
    ///     <item>password [string]: password of the PixDirector. ex: "12345678"</item>
    ///     <item>teamEmail [string]: team email. ex: "pix.team@company.com"</item>
    ///     <item>teamPhones [list of strings]: list of phones of the team. ex: new List<string>{ "+5511988889999", "+5511988889998" }</item>
    ///     <item>id [string]: unique id returned when the PixDirector is created. ex: "5656565656565656"</item>
    ///     <item>status [string]: current PixDirector status. ex: "success"</item>
    /// </list>
    /// </summary>
    public partial class PixDirector : Resource
    {
        public string Email { get; }
        public string Name { get; }
        public string Password { get; }
        public string Phone { get; }
        public string TaxID { get; }
        public string TeamEmail { get; }
        public List<string> TeamPhones { get; }
        public string Status { get; }

        /// <summary>
        /// PixDirector object
        /// <br/>
        /// Mandatory data that must be registered within the Central Bank for emergency contact purposes.
        /// When you initialize a PixDirector, the entity will not be automatically
        /// created in the Stark Infra API. The 'create' function sends the objects
        /// to the Stark Infra API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: name of the PixDirector. ex: "Edward Stark".</item>
        ///     <item>taxId [string]: tax ID (CPF) of the PixDirector. ex: "012.345.678-90"</item>
        ///     <item>phone [string]: phone of the PixDirector. ex: "+551198989898"</item>
        ///     <item>email [string]: email of the PixDirector. ex: "ned.stark@starkbank.com"</item>
        ///     <item>password [string]: password of the PixDirector. ex: "12345678"</item>
        ///     <item>teamEmail [string]: team email. ex: "pix.team@company.com"</item>
        ///     <item>teamPhones [list of strings]: list of phones of the team. ex: List<string>{ "+5511988889999", "+5511988889998" }</item>
        ///</list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when the PixDirector is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current PixDirector status. </item>
        /// </list>
        /// </summary>
        public PixDirector(string email, string name, string password, string phone, string taxId, string teamEmail, List<string> teamPhones,
            string id = null, string status = null) : base(id)
        {
            Email = email;
            Name = name;
            Password = password;
            Phone = phone;
            TaxID = taxId;
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

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string email = json.email;
            string name = json.name;
            string password = json.password;
            string phone = json.phone;
            string taxId = json.taxId;
            string teamEmail = json.teamEmail;
            List<string> teamPhones = json.teamPhones;
            string status = json.status;

            return new PixDirector(
                id: id, email: email, name: name, password: password, phone: phone, taxId: taxId, teamEmail: teamEmail,
                teamPhones: teamPhones, status: status
            );
        }
    }
}
