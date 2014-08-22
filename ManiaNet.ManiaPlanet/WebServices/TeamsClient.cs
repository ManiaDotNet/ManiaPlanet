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
        /// Gets the <see cref="PlayerInfo"/>s for the Admins of the Team given by the Id. Null when the information couldn't be found.
        /// </summary>
        /// <param name="id">The Id of the Team.</param>
        /// <returns>The Admins of the Team. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<TeamInfo[]> GetAdminsAsync(uint id)
        {
            if (id < 1)
                return null;

            var response = await execute(RequestType.Get, "teams/" + id + "/admins/index.json");

            return response == null ? null : jsonSerializer.Deserialize<TeamInfo[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the <see cref="ContractInfo"/>s for the Team given by the Id. Null when the information couldn't be found.
        /// </summary>
        /// <param name="id">The Id of the Team.</param>
        /// <returns>The Contracts of the Team. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<ContractInfo[]> GetContractsAsync(uint id)
        {
            if (id < 1)
                return null;

            var response = await execute(RequestType.Get, "teams/" + id + "/contracts/index.json");

            return response == null ? null : jsonSerializer.Deserialize<ContractInfo[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the <see cref="TeamInfo"/> for the Team given by the Id. Null when the information couldn't be found.
        /// </summary>
        /// <param name="id">The Id of the Team.</param>
        /// <returns>The information about the Team. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<TeamInfo> GetInfoAsync(uint id)
        {
            if (id < 1)
                return null;

            var response = await execute(RequestType.Get, "teams/" + id + "/index.json");

            return response == null ? null : jsonSerializer.Deserialize<TeamInfo>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the <see cref="TeamRanking"/>s for the Team given by the Id. Null when the information couldn't be found.
        /// </summary>
        /// <param name="id">The Id of the Team.</param>
        /// <returns>The Rankings of the Team. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<TeamRanking[]> GetRankingsAsync(uint id)
        {
            if (id < 1)
                return null;

            var response = await execute(RequestType.Get, "teams/" + id + "/rank/index.json");

            return response == null ? null : jsonSerializer.Deserialize<TeamRanking[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Stores information about a Team Contract.
        /// </summary>
        [JsonObject, UsedImplicitly]
        // ReSharper disable once ClassCannotBeInstantiated
        public sealed class ContractInfo
        {
            /// <summary>
            /// Gets the Date-Information for the creation of the Contract.
            /// </summary>
            [CanBeNull, JsonProperty("date")]
            public DateInfo Date
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Id of the Contract.
            /// </summary>
            [JsonProperty("id"), UsedImplicitly]
            public uint? Id
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Player involved in the contract.
            /// </summary>
            [CanBeNull, JsonProperty("player")]
            public PlayerInfo Player
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the state of the Contract. Specific values unknown.
            /// </summary>
            [JsonProperty("state"), UsedImplicitly]
            public int? State
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Team-Information for the Team involved in the Contract.
            /// </summary>
            [CanBeNull, JsonProperty("team")]
            public TeamInfo Team
            {
                get;
                [UsedImplicitly]
                private set;
            }

            private ContractInfo()
            { }
        }

        /// <summary>
        /// Stores information about a Team.
        /// </summary>
        [JsonObject, UsedImplicitly]
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

            private TeamInfo()
            { }
        }

        /// <summary>
        /// Stores information about a Teams's Mutiplayer Ranking.
        /// </summary>
        [JsonObject, UsedImplicitly]
        // ReSharper disable once ClassCannotBeInstantiated
        public sealed class TeamRanking
        {
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
            /// Gets the actual Rank number. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("rank"), UsedImplicitly]
            public uint? Rank
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Id of the Title that the Ranking is for.
            /// </summary>
            [JsonProperty("titleId"), UsedImplicitly]
            public uint? TitleId
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Zone-Id of the Ranking. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("zoneId"), UsedImplicitly]
            public uint? ZoneId
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Zone-Path of the Ranking. Zones are separated by pipe characters ('|'). May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("path"), UsedImplicitly]
            public string ZonePath
            {
                get;
                [UsedImplicitly]
                private set;
            }

            private TeamRanking()
            { }
        }
    }
}