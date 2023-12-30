using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditNote.Invoice.Discount object
    /// <br/>
    /// Invoice discount information.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Percentage [float]: percentage of discount applied until specified due date. ex: 2.5</item>
    ///     <item>Due [DateTime]: due DateTime for the discount. ex: new DateTime(2022, 01, 10)</item>
    /// </list>
    /// </summary>
    public partial class Discount : StarkCore.Utils.SubResource
    {
        public float? Percentage { get; }
        public DateTime? Due { get; }

        /// <summary>
        /// CreditNote.Invoice.Discount object
        /// <br/>
        /// Invoice discount information.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>Percentage [float]: percentage of discount applied until specified due date. ex: 2.5</item>
        ///     <item>Due [DateTime]: due DateTime for the discount. ex: new DateTime(2022, 01, 10)</item>
        /// </list>
        /// </summary>
        public Discount(float? percentage, DateTime? due)
        {
            Percentage = percentage;
            Due = due;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Discount", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            float? percentage = json.percentage;
            DateTime? due = json.due;

            return new Discount(percentage: percentage, due: due);
        }
    }
}
