using System;
using System.Linq;
using System.Collections.Generic;
using StarkInfra.Utils;


namespace StarkInfra
{
    /// <summary>
    /// DynamicBrcode.Discount object
    /// <br/>
    /// Used to define a Discount in the BR Code
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Percentage [float]: discount percentage that will be applied. ex: 2.5</item>
    ///     <item>Due [DateTime]: Date after when the discount will be overdue in UTC ISO format</item>
    /// </list>
    /// </summary>
    public class Discounts : StarkCore.Utils.SubResource
    {
        public float Percentage { get; }
        public DateTime Due { get; }

        /// <summary>
        /// DynamicBrcode.Discount object
        /// <br/>
        /// Used to define a Discount in the BR Code
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>percentage [float]: discount percentage that will be applied. ex: 2.5</item>
        ///     <item>due [DateTime]: Date after when the discount will be overdue in UTC ISO format</item>
        /// </list>
        /// </summary>
        public Discounts(float percentage, DateTime due)
        {
            Percentage = percentage;
            Due = due;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Discounts", resourceMaker: ResourceMaker);
        }

        internal static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
        {
            float percentage = json.percentage;
            DateTime due = json.due;

            return new Discounts(
                percentage: percentage, due: due
            );
        }
    }
}
