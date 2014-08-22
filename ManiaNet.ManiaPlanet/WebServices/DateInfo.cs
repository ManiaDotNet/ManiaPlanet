using ManiaNet.ManiaPlanet.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.ManiaPlanet.WebServices
{
    /// <summary>
    /// Stores information about a Date.
    /// </summary>
    [JsonObject, UsedImplicitly]
    // ReSharper disable once ClassCannotBeInstantiated
    public sealed class DateInfo
    {
        /// <summary>
        /// Gets the formatted Date. Format is 'YYYY-MM-DD HH:MM:SS'. May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("date")]
        public string Date
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the TimeZone-Id of the Date. Seems to always be 3. May be null if the data wasn't complete.
        /// </summary>
        [JsonProperty("timezone_type"), UsedImplicitly]
        public uint? TimeZoneId
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the TimeZone-Name of the Date. Seems to always be 'Europe/Berlin'. May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("timezone")]
        public string TimeZoneName
        {
            get;
            [UsedImplicitly]
            private set;
        }

        private DateInfo()
        { }
    }
}