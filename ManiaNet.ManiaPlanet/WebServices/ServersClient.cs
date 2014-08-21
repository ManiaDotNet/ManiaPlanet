using ManiaNet.ManiaPlanet.Annotations;
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
        /// Iterates through all <see cref="ServerInfo"/>s, starting at 0. Empty when the information couldn't be found.
        /// <para/>
        /// Only use this if you really know what you're doing. It will possibly iterate through *ALL* ManiaPlanet servers.
        /// </summary>
        /// <param name="stepSize">The size of each badge of ServerInfos that is downloaded.</param>
        /// <param name="retries">The maximum number of retries when the information couldn't be found.</param>
        /// <returns>All ServerInfos. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public IEnumerable<ServerInfo> GetAllServerInfos(uint stepSize = 50, uint retries = 3)
        {
            if (stepSize == 0)
                yield break;

            var baseQuery = "/servers/index.json?length=" + stepSize + "&offset=";

            var shouldQuery = true;
            uint offset = 0;
            uint retried = 0;
            var pendingServerInfos = new Stack<ServerInfo>();
            var runningQuery = execute(RequestType.Get, baseQuery + offset);

            while (shouldQuery || pendingServerInfos.Count > 0)
            {
                if (shouldQuery && pendingServerInfos.Count == 0)
                {
                    var response = runningQuery.Result;
                    var result = response == null ? null : jsonSerializer.Deserialize<ServerInfo[]>(new JsonTextReader(new StringReader(response)));

                    if (result != null)
                    {
                        if (result.Length < stepSize)
                            shouldQuery = false;

                        pendingServerInfos = new Stack<ServerInfo>(result);
                        offset += stepSize;
                    }
                    else
                        retried++;
                }

                if (shouldQuery && pendingServerInfos.Count < stepSize)
                {
                    if (retried < retries)
                        runningQuery = execute(RequestType.Get, baseQuery + offset);
                    else
                        shouldQuery = false;
                }

                if (pendingServerInfos.Count > 0)
                    yield return pendingServerInfos.Pop();
            }
        }

        /// <summary>
        /// Gets the number of players that favorited the Server given by the login. Null when the information couldn't be found.
        /// </summary>
        /// <param name="login">The login of the Server.</param>
        /// <returns>The number of players that favorited the server. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<uint?> GetFavoritedCountAsyncFor(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;

            var response = await execute(RequestType.Get, "servers/" + login + "/favorited/index.txt");

            uint n;
            return !uint.TryParse(response, out n) ? (uint?)null : n;
        }

        /// <summary>
        /// Gets the number of players that favourited the Server given by the login. Null when the information couldn't be found.
        /// </summary>
        /// <param name="login">The login of the Server.</param>
        /// <returns>The number of players that favourited the server. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<uint?> GetFavouritedCountAsyncFor(string login)
        {
            return await GetFavoritedCountAsyncFor(login);
        }

        /// <summary>
        /// Gets the <see cref="PlayerInfo"/>s for the players connected to the Server given by the login. Null when the information couldn't be found.
        /// </summary>
        /// <param name="login">The login of the Server.</param>
        /// <returns>The PlayerInfos for the players connected to the server. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<PlayerInfo[]> GetOnlinePlayersAsyncFor(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;

            var response = await execute(RequestType.Get, "servers/" + login + "/players/index.json");

            return response == null ? null : jsonSerializer.Deserialize<PlayerInfo[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the <see cref="ServerInfo"/> for the Server given by the login. Null when the information couldn't be found.
        /// </summary>
        /// <param name="login">The login of the Server.</param>
        /// <returns>The Server information of the server. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<ServerInfo> GetServerInfoAsyncFor(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;

            var response = await execute(RequestType.Get, "servers/" + login + "/index.json");

            return response == null ? null : jsonSerializer.Deserialize<ServerInfo>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the specified number of <see cref="ServerInfo"/>s, starting at the given index (0-based). Null when the information couldn't be found.
        /// </summary>
        /// <param name="offset">The starting index (0-based).</param>
        /// <param name="length">The maximum number of Servers to return.</param>
        /// <returns>The ServerInfos in the given range. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<ServerInfo[]> GetServerInfosAsync(uint offset = 0, uint length = 10)
        {
            if (length == 0)
                return new ServerInfo[0];

            var response = await execute(RequestType.Get, "servers/index.json?offset=" + offset + "&length=" + length);

            return response == null ? null : jsonSerializer.Deserialize<ServerInfo[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Stores information about a Server.
        /// </summary>
        [JsonObject, UsedImplicitly]
        // ReSharper disable once ClassCannotBeInstantiated
        public sealed class ServerInfo
        {
            [JsonProperty("isDedicated"), UsedImplicitly]
            private byte? isDedicated;

            [JsonProperty("isLadder"), UsedImplicitly]
            private byte? isLadder;

            [JsonProperty("isLobby"), UsedImplicitly]
            private byte? isLobby;

            [JsonProperty("isOnline"), UsedImplicitly]
            private byte? isOnline;

            [JsonProperty("isPrivate"), UsedImplicitly]
            private byte? isPrivate;

            [CanBeNull, JsonProperty("mapsList")]
            private string[] maps;

            [JsonProperty("scriptTeam"), UsedImplicitly]
            private byte? scriptUsesTeamMode;

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
            public uint? Id
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets whether the Server is a dedicated server. May be null if the data wasn't complete.
            /// </summary>
            [JsonIgnore, UsedImplicitly]
            public bool? IsDedicated
            {
                get { return !isDedicated.HasValue ? (bool?)null : Convert.ToBoolean(isDedicated.Value); }
            }

            /// <summary>
            /// Gets whether the Server is a ladder Server or not. May be null if the data wasn't complete.
            /// </summary>
            [JsonIgnore, UsedImplicitly]
            public bool? IsLadder
            {
                get { return !isLadder.HasValue ? (bool?)null : Convert.ToBoolean(isLadder.Value); }
            }

            /// <summary>
            /// Gets whether the Server is a lobby Server or not. May be null if the data wasn't complete.
            /// </summary>
            [JsonIgnore, UsedImplicitly]
            public bool? IsLobby
            {
                get { return !isLobby.HasValue ? (bool?)null : Convert.ToBoolean(isLobby.Value); }
            }

            /// <summary>
            /// Gets whether the Server is currently online. May be null if the data wasn't complete.
            /// </summary>
            [JsonIgnore, UsedImplicitly]
            public bool? IsOnline
            {
                get { return !isOnline.HasValue ? (bool?)null : Convert.ToBoolean(isOnline.Value); }
            }

            /// <summary>
            /// Gets whether the Server is private or not. May be null if the data wasn't complete.
            /// </summary>
            [JsonIgnore, UsedImplicitly]
            public bool? IsPrivate
            {
                get { return !isPrivate.HasValue ? (bool?)null : Convert.ToBoolean(isPrivate.Value); }
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
            public uint? MaxPlayerCount
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
            public uint? PlayerCount
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
            [JsonIgnore, UsedImplicitly]
            public bool? ScriptUsesTeamMode
            {
                get { return !scriptUsesTeamMode.HasValue ? (bool?)null : Convert.ToBoolean(scriptUsesTeamMode.Value); }
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
        }
    }
}