using System;

namespace SchematicHQ.Client.Datastream
{
    /// <summary>
    /// Options for configuring the Datastream functionality
    /// </summary>
    public class DatastreamOptions
    {
        /// <summary>
        /// Time-to-live for cached resources (companies and users)
        /// </summary>
        public TimeSpan CacheTTL { get; set; } = TimeSpan.FromHours(24);
    }
}
