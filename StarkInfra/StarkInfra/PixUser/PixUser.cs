using System;
using System.Collections.Generic;
using StarkInfra.Utils;

namespace StarkInfra
{
    public class PixStatistics
    {
        public string Value { get; }
        public string Source { get; }
        public string Type { get; }
        public DateTime? After { get; }
        public DateTime? Updated { get; }

        /// <summary>
        /// Statistics object
        /// <br/>
        /// A Pix User is a collection of fraud statistics regarding a specific user.
        /// Fraud statistics include a summary of pix infractions, pix frauds, pix keys, and pix requests over different periods of time.
        /// In this section, we will teach you how to retrieve information from Pix Users..
        /// <br/>
        /// Attributes(return-only):
        /// <list>
        ///     <item>after [DateTime]: datetime that fraud of the PixUser is created. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>source [string]: source of fraud </item>
        ///     <item>type [string]: type of fraud. ex: registered</item>
        ///     <item>updated [DateTime]: latest update datetime for the balance. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>value [string]: value amount of fraud registed. ex: 6188</item>
        /// </list>
        /// </summary>
        public PixStatistics(DateTime? after, string source, string type, string value, DateTime? updated)
        {
            After = after;
            Source = source;           
            Type = type;    
            Value = value;
            Updated = updated;
        }

    }
    /// <summary>
    /// Statistics object
    /// <br/>
    /// A Pix User is a collection of fraud statistics regarding a specific user.
    /// Fraud statistics include a summary of pix infractions, pix frauds, pix keys, and pix requests over different periods of time.
    /// In this section, we will teach you how to retrieve information from Pix Users..
    /// <br/>
    /// Properties:
    /// <item>after [DateTime]: datetime that fraud of the PixUser is created. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// <item>source [string]: source of fraud </item>
    /// <item>type [string]: type of fraud. ex: registered</item>
    /// <item>updated [DateTime]: latest update datetime for the balance. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// <item>value [string]: value amount of fraud registed. ex: 6188</item>
    /// </summary>
    public class PixUser: Resource
    {
        public List<PixStatistics> Statistics { get; }

        /// <summary>
        /// PixUser object
        /// <br/>
        /// A Pix User is a collection of fraud statistics regarding a specific user.
        /// Fraud statistics include a summary of pix infractions, pix frauds, pix keys, and pix requests over different periods of time.
        /// In this section, we will teach you how to retrieve information from Pix Users..
        /// <br/>
        /// Attributes(return-only):
        /// <item>statistics []: list of statistics</item>
        /// </summary>
        public PixUser(string id, List<PixStatistics> Statistics) : base(id)
        {
            this.Statistics = Statistics;
        }


        /// <summary>
        /// Retrieve the PixUser object
        /// <br/>
        /// Receive the PixUser object linked to your workspace in the Stark Infra API
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>taxId [CPF/CNPJ]: Of User</item>
        ///     <item>user [Organization/Project object, default null]: Organization or Project object. Not necessary if StarkInfra.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <item>PixUser object with updated attributes</item>
        /// </summary>
        public static PixUser Get(string taxId, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: taxId,
                user: user
            ) as PixUser;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PixUser", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            var pixStatistics = new List<PixStatistics>();

            List<dynamic> statistics = json.statistics?.ToObject<List<dynamic>>();
            string id = json.id;

            foreach (dynamic stat in statistics)
            {
                if (stat != null)
                {
                    string afterString = stat.after;
                    DateTime? after = StarkCore.Utils.Checks.CheckNullableDateTime(afterString);

                    string updatedString = stat.updated;
                    DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);
                    string source = stat.source;
                    string type = stat.type;
                    string value = stat.value;

                    pixStatistics.Add(new PixStatistics(after, source, type, value, updated));
                }
            }

            return new PixUser(id: id, pixStatistics);
        }

    }
}
