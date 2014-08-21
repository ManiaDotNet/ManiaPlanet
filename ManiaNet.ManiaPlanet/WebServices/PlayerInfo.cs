using ManiaNet.ManiaPlanet.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.ManiaPlanet.WebServices
{
    /// <summary>
    /// Stores information about a Player.
    /// </summary>
    [JsonObject, UsedImplicitly]
    // ReSharper disable once ClassCannotBeInstantiated
    public sealed class PlayerInfo
    {
        /// <summary>
        /// Gets the Id of the Player. May be null if the data wasn't complete.
        /// </summary>
        [JsonProperty("id"), UsedImplicitly]
        public uint? Id
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Login of the Player. May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("login")]
        public string Login
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Nickname of the Player, including the $-formats. May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("nickname")]
        public string Nickname
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Zone-Id of the Player. May be null if the data wasn't complete.
        /// </summary>
        [JsonProperty("idZone"), UsedImplicitly]
        public uint? ZoneId
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Zone-Path of the Player. Zones are separated by pipe characters ('|'). May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("path")]
        public string ZonePath
        {
            get;
            [UsedImplicitly]
            private set;
        }

        private PlayerInfo()
        { }
    }
}