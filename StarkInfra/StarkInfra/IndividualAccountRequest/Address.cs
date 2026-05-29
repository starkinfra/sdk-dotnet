using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// Address object
    /// <br/>
    /// Structured residential address of the individual. It is exposed only as the Address
    /// property on IndividualAccountRequest and is serialized as a nested JSON object on the wire.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Street [string]: street name. ex: "Rua do Estilo Barroco"</item>
    ///     <item>Number [string]: street number. ex: "648"</item>
    ///     <item>Neighborhood [string]: neighborhood / district. ex: "Santo Amaro"</item>
    ///     <item>City [string]: city. ex: "SP"</item>
    ///     <item>State [string]: state (BR 2-letter code). ex: "SP"</item>
    ///     <item>ZipCode [string]: ZIP code (BR CEP). ex: "05724005"</item>
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

        /// <summary>
        /// Address object
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>street [string]: street name. ex: "Rua do Estilo Barroco"</item>
        ///     <item>number [string]: street number. ex: "648"</item>
        ///     <item>neighborhood [string]: neighborhood / district. ex: "Santo Amaro"</item>
        ///     <item>city [string]: city. ex: "SP"</item>
        ///     <item>state [string]: state (BR 2-letter code). ex: "SP"</item>
        ///     <item>zipCode [string]: ZIP code (BR CEP). ex: "05724005"</item>
        /// </list>
        /// </summary>
        public Address(string street, string number, string neighborhood, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Address", resourceMaker: ResourceMaker);
        }

        public static Address Parse(dynamic json)
        {
            if (json is null) return null;
            return (Address)ResourceMaker(json);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            string street = json.street;
            string number = json.number;
            string neighborhood = json.neighborhood;
            string city = json.city;
            string state = json.state;
            string zipCode = json.zipCode;

            return new Address(
                street: street, number: number, neighborhood: neighborhood,
                city: city, state: state, zipCode: zipCode
            );
        }
    }
}
