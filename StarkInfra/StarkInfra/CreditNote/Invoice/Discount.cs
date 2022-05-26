using System;
using System.Collections.Generic;
using System.Linq;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// CreditNote.Invoice.Discount object
    /// <br/>
    /// Discount object for the Invoice
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Percentage [float]: percentage of discount applied until specified due date</item>
    ///     <item>Due [DateTime, or string]: due datetime for the discount</item>
    /// </list>
    /// </summary>
    public partial class Discount : SubResource
    {
        public float? Percentage { get; }
        public DateTime? Due { get; }

        /// <summary>
        /// CreditNote.Invoice.Discount object
        /// <br/>
        /// Discount object for the Invoice
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>Percentage [float]: percentage of discount applied until specified due date</item>
        ///     <item>Due [DateTime, or string]: due datetime for the discount</item>
        /// </summary>
        public Discount(float? percentage, DateTime? due)
        {
            Percentage = percentage;
            Due = due;
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Discount", resourceMaker: ResourceMaker);
        }

        internal static SubResource ResourceMaker(dynamic json)
        {
            float? percentage = json.percentage;
            DateTime? due = json.due;

            return new Discount(percentage: percentage, due: due);
        }
    }
}
