using ManiaNet.ManiaPlanet.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.ManiaPlanet.WebServices
{
    /// <summary>
    /// Contains methods for accessing the Team infos.
    /// </summary>
    [UsedImplicitly]
    public sealed class TeamsClient : WSClient
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TeamsClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public TeamsClient([NotNull] string username, [NotNull] string password)
            : base(username, password)
        { }

        /// <summary>
        /// Stores information about a Team.
        /// </summary>
        [UsedImplicitly]
        // ReSharper disable once ClassCannotBeInstantiated
        public sealed class TeamInfo
        {
            [JsonProperty("deleted"), UsedImplicitly]
            private byte? deleted;

            /// <summary>
            /// Gets the City of the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("city")]
            public string City
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the creation date of the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("creationDate")]
            public DateInfo CreationDateInfo
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the login of the creator of the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("creatorLogin")]
            public string Creator
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the description of the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("description")]
            public string Description
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Url to the emblem image of the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("emblem")]
            public string Emblem
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Url to the emblem image of the Team for usage on the web. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("emblemWeb")]
            public string EmblemWeb
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Id of the Team. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("id"), UsedImplicitly]
            public uint? Id
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets whether the Team has been deleted or not. May be null if the data wasn't complete.
            /// </summary>
            [UsedImplicitly]
            public bool? IsDeleted
            {
                get { return !deleted.HasValue ? (bool?)null : Convert.ToBoolean(deleted.Value); }
            }

            /// <summary>
            /// Gets the number of ladder points that the Team has. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("ladderPoints"), UsedImplicitly]
            public uint? LadderPoints
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the level of the Team. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("level"), UsedImplicitly]
            public uint? Level
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Url to the logo of the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("logo")]
            public string Logo
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the maximum number of player that can be part of the Team. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("maxTeamSize"), UsedImplicitly]
            public uint? MaxSize
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the minimum number of players that have to be part of the Team. Can be greater than the Size property. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("minTeamSize"), UsedImplicitly]
            public uint? MinSize
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the name of the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("name")]
            public string Name
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the primary color of the team. Format is 3 character RGB hex. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("primaryColor")]
            public string PrimaryColor
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the secondary color of the Team. Format is 3 character RGB hex. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("secondaryColor")]
            public string SecondaryColor
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the number of players that are part of the Team. Can be less than the MinSize property. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("teamSize"), UsedImplicitly]
            public uint? Size
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the tag of the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("tag")]
            public string Tag
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Title-Information for the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("title")]
            public TitleInfo Title
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Zone-Information for the Team. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("zone")]
            public ZoneInfo Zone
            {
                get;
                [UsedImplicitly]
                private set;
            }

            //deleted: boolean - Whether the team has been deleted. May be null if the data wasn't complete.
            private TeamInfo()
            { }

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
    }
}