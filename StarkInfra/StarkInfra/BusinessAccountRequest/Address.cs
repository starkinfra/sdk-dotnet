using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// BusinessAccountRequest.Address object
    /// <br/>
    /// The Address object is the structured address of the company referenced by a
    /// BusinessAccountRequest. It is embedded on the parent's Address field and has no endpoints of its own.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Street [string]: street name. ex: "Av. Faria Lima"</item>
    ///     <item>Number [string]: street number. ex: "2000"</item>
    ///     <item>Neighborhood [string]: neighborhood / district. ex: "Itaim Bibi"</item>
    ///     <item>City [string]: city. ex: "Sao Paulo"</item>
    ///     <item>State [string]: state (BR 2-letter code). ex: "SP"</item>
    ///     <item>ZipCode [string]: ZIP code (BR CEP), formatted or digit-only. ex: "04538-132"</item>
    ///     <item>Complement [string]: address complement. ex: "Sala 42"</item>
    /// </list>
    /// </summary>
    public partial class Address : StarkCore.Utils.SubResource
    {
        public string Street { get; }
        public string Number { get; }
        public string Neighborhood { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }
        public string Complement { get; }

        /// <summary>
        /// BusinessAccountRequest.Address object
        /// <br/>
        /// The Address object is the structured address of the company referenced by a
        /// BusinessAccountRequest. It is embedded on the parent's address field and has no endpoints of its own.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>street [string]: street name. ex: "Av. Faria Lima"</item>
        ///     <item>number [string]: street number. ex: "2000"</item>
        ///     <item>neighborhood [string]: neighborhood / district. ex: "Itaim Bibi"</item>
        ///     <item>city [string]: city. ex: "Sao Paulo"</item>
        ///     <item>state [string]: state (BR 2-letter code). ex: "SP"</item>
        ///     <item>zipCode [string]: ZIP code (BR CEP), formatted or digit-only. ex: "04538-132"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>complement [string, default null]: address complement. ex: "Sala 42"</item>
        /// </list>
        /// </summary>
        public Address(
            string street, string number, string neighborhood, string city, string state, string zipCode,
            string complement = null
        )
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
            Complement = complement;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Address", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            string street = json.street;
            string number = json.number;
            string neighborhood = json.neighborhood;
            string city = json.city;
            string state = json.state;
            string zipCode = json.zipCode;
            string complement = json.complement;

            return new Address(
                street: street, number: number, neighborhood: neighborhood, city: city, state: state,
                zipCode: zipCode, complement: complement
            );
        }
    }
}
