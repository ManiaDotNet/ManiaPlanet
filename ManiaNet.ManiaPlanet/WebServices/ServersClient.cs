﻿using ManiaNet.ManiaPlanet.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManiaNet.ManiaPlanet.WebServices
{
    /// <summary>
    /// Contains methods for accessing the Server infos.
    /// </summary>
    public sealed class ServersClient : WSClient
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ServersClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public ServersClient([NotNull] string username, [NotNull] string password)
            : base(username, password)
        { }

        /// <summary>
        /// Gets the <see cref="ServerInfo"/> for the dedicated server given by the login. Null when the information couldn't be found.
        /// </summary>
        /// <param name="login">The login of the dedicated server.</param>
        /// <returns>The Server information of the dedicated server. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<ServerInfo> GetServerInfoAsyncFor(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;

            var response = await execute(RequestType.Get, "servers/" + login + "/index.json");

            return response == null ? null : jsonSerializer.Deserialize<ServerInfo>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Stores information about a Server.
        /// </summary>
        [JsonObject, UsedImplicitly]
        // ReSharper disable once ClassCannotBeInstantiated
        public sealed class ServerInfo
        {
            [CanBeNull, JsonProperty("mapsList")]
            private readonly string[] maps;

            /// <summary>
            /// Gets the description text of the Server. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("description")]
            public string Description
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Id of the Server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("id"), UsedImplicitly]
            public int Id
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets whether the Server is a dedicated server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("isDedicated"), UsedImplicitly]
            public bool? IsDedicated
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets whether the Server is a ladder Server or not. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("isLadder"), UsedImplicitly]
            public bool? IsLadder
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets whether the Server is a lobby Server or not. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("isLobby"), UsedImplicitly]
            public bool? IsLobby
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets whether the Server is currently online. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("isOnline"), UsedImplicitly]
            public bool? IsOnline
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets whether the Server is private or not. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("isPrivate"), UsedImplicitly]
            public bool? IsPrivate
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the average ladder points of the players on the Server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("ladderPointsAvg"), UsedImplicitly]
            public float? LadderPointsAverage
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the upper ladder limit of the Server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("ladderLimitMax"), UsedImplicitly]
            public float? LadderPointsLimitMax
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the lower ladder limit of the Server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("ladderLimitMin"), UsedImplicitly]
            public float? LadderPointsLimitMin
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the highest ladder points of a player on the Server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("ladderPointsMax"), UsedImplicitly]
            public float? LadderPointsMax
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the lowest ladder points of a player on the Server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("ladderPointsMin"), UsedImplicitly]
            public float? LadderPointsMin
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the login of the Server Account. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("login")]
            public string Login
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Maps of the Server. May be empty if the data wasn't complete.
            /// </summary>
            [JsonIgnore, UsedImplicitly]
            public IEnumerable<string> Maps
            {
                get
                {
                    if (maps == null)
                        yield break;

                    foreach (var map in maps)
                        yield return map;
                }
            }

            /// <summary>
            /// Gets the maximum number of players allowed on the Server.  May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("maxPlayerCount"), UsedImplicitly]
            public int? MaxPlayerCount
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the name of the Server, including $-formats. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("serverName")]
            public string Name
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the login of the Server Account's owner. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("owner")]
            public string OwnerLogin
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the current number of players on the Server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("playerCount"), UsedImplicitly]
            public int? PlayerCount
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the name of the Script used by the Server. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("scriptName")]
            public string ScriptName
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets whether the Script used by the Server is using team mode or not. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("scriptTeam"), UsedImplicitly]
            public bool? ScriptUsesTeamMode
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// The version of the Script used by the Server. Format is: 'YYYY-MM-DD'. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("scriptVersion")]
            public string ScriptVersion
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Title-Information for the Server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("title"), UsedImplicitly]
            public TitleInfo Title
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the build version of the Server. Format is 'YYYY-MM-DD_HH_MM'. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("buildVersion")]
            public string Version
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Zone-Information for the Server. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("zone"), UsedImplicitly]
            public ZoneInfo Zone
            {
                get;
                [UsedImplicitly]
                private set;
            }

            private ServerInfo()
            { }

            /// <summary>
            /// Stores information about the Title played on a Server.
            /// </summary>
            [JsonObject, UsedImplicitly]
            // ReSharper disable once ClassCannotBeInstantiated
            public sealed class TitleInfo
            {
                [CanBeNull, JsonProperty("dependencies")]
                private readonly string[] dependencies;

                /// <summary>
                /// Gets the cost of the Title. May be null if the data wasn't complete.
                /// </summary>
                [CanBeNull, JsonProperty("cost")]
                public string Cost
                {
                    get;
                    [UsedImplicitly]
                    private set;
                }

                /// <summary>
                /// Gets the Ids of the Titles that this Title depends on. May be empty if the data wasn't complete, or there are none.
                /// </summary>
                [JsonIgnore, UsedImplicitly]
                public IEnumerable<string> Dependencies
                {
                    get
                    {
                        if (dependencies == null)
                            yield break;

                        foreach (var dependency in dependencies)
                            yield return dependency;
                    }
                }

                /// <summary>
                /// Gets the Id of the Title. May be null if the data wasn't complete.
                /// </summary>
                [JsonProperty("id"), UsedImplicitly]
                public int? Id
                {
                    get;
                    [UsedImplicitly]
                    private set;
                }

                /// <summary>
                /// Gets the Id-String of the Title. May be null if the data wasn't complete.
                /// </summary>
                [CanBeNull, JsonProperty("idString")]
                public string IdString
                {
                    get;
                    [UsedImplicitly]
                    private set;
                }

                /// <summary>
                /// Gets whether the Title is a custom (i.e. user-made) Title. May be null if the data wasn't complete.
                /// </summary>
                [JsonProperty("isCustom"), UsedImplicitly]
                public bool? IsCustom
                {
                    get;
                    [UsedImplicitly]
                    private set;
                }

                /// <summary>
                /// Gets the name of the Title. May be null if the data wasn't complete.
                /// </summary>
                [CanBeNull, JsonProperty("name")]
                public string Name
                {
                    get;
                    [UsedImplicitly]
                    private set;
                }

                /// <summary>
                /// Gets the address of the website of the Title. May be null if the data wasn't complete.
                /// </summary>
                [CanBeNull, JsonProperty("web")]
                public string Website
                {
                    get;
                    [UsedImplicitly]
                    private set;
                }

                private TitleInfo()
                { }
            }

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
                /// Gets the Url to a JPEG icon for the Zone. May be null if the data wasn't complete, or there's none.
                /// </summary>
                [CanBeNull, JsonProperty("iconJPEGURL")]
                public string IconJpegUrl
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
                public int? Id
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
    }
}