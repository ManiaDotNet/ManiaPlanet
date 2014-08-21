using ManiaNet.ManiaPlanet.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.ManiaPlanet.WebServices
{
    /// <summary>
    /// Stores information about the Zone that a Server is in.
    /// </summary>
    [JsonObject, UsedImplicitly]
    // ReSharper disable once ClassCannotBeInstantiated
    public sealed class ZoneInfo
    {
        /// <summary>
        /// Gets the Url to a DDS icon for the Zone. May be null if the data wasn't complete, or there's none.
        /// </summary>
        [CanBeNull, JsonProperty("iconDDSURL")]
        public string IconDdsUrl
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Url to a JPG icon for the Zone. May be null if the data wasn't complete, or there's none.
        /// </summary>
        [CanBeNull, JsonProperty("iconJPGURL")]
        public string IconJpgUrl
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Url to an icon for the Zone. Can be DDS or JPEG. May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("iconURL")]
        public string IconUrl
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Id of the Zone. May be null if the data wasn't complete.
        /// </summary>
        [JsonProperty("id"), UsedImplicitly]
        public uint? Id
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the name of the Zone (the last part of the Path). May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("name")]
        public string Name
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Path of the Zone. Zones are separated by pipe characters ('|'). May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("path")]
        public string Path
        {
            get;
            [UsedImplicitly]
            private set;
        }

        private ZoneInfo()
        { }
    }
}