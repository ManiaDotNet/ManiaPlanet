using ManiaNet.ManiaPlanet.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManiaNet.ManiaPlanet.WebServices
{
    // Contains the Player Ranking methods.
    public sealed partial class RankingsClient
    {
        /// <summary>
        /// Gets the <see cref="PlayerRanking"/> for the Player given by the login, for the given Title. Null when the information couldn't be found.
        /// </summary>
        /// <param name="login">The login of the Player.</param>
        /// <param name="title">The Title that the ranking is wanted for. If left empty, rankings for all titles will be returned.</param>
        /// <returns>The Ranking information of the Player for the Title. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<PlayerRanking> GetMultiplayerRankingAsyncFor(string login, string title = "")
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(login))
                return null;

            var response = await execute(RequestType.Get, "titles/rankings/multiplayer/player/" + login + "/index.json?title=" + title);

            return response == null ? null : jsonSerializer.Deserialize<PlayerRanking>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Stores information about a Player's multiplayer ranking.
        /// </summary>
        [JsonObject, UsedImplicitly]
        // ReSharper disable once ClassCannotBeInstantiated
        public sealed class PlayerRanking
        {
            [CanBeNull, JsonProperty("ranks")]
            private Ranking[] rankings;

            /// <summary>
            /// Gets the number of points that the Player has in the Title. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("points"), UsedImplicitly]
            public float? Points
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the rankings of the Player in the different Zones along their ZonePath. May be empty if the data wasn't complete.
            /// </summary>
            [NotNull, JsonIgnore]
            public IEnumerable<Ranking> Rankings
            {
                get
                {
                    if (rankings == null)
                        yield break;

                    // ReSharper disable once LoopCanBeConvertedToQuery
                    foreach (var ranking in rankings)
                        yield return ranking;
                }
            }

            /// <summary>
            /// Gets the name of the Title that the Info is for. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("environment")]
            public string Title
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the name of the Unit that the Points value is in. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("unit")]
            public string Unit
            {
                get;
                [UsedImplicitly]
                private set;
            }

            private PlayerRanking()
            {
                Points = -1f;
            }

            /// <summary>
            /// Stores information about a Player's Mutiplayer Ranking.
            /// </summary>
            [JsonObject, UsedImplicitly]
            // ReSharper disable once ClassCannotBeInstantiated
            public sealed class Ranking
            {
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
                /// Gets the Zone-Id of the Ranking. May be null if the data wasn't complete.
                /// </summary>
                [JsonProperty("idZone"), UsedImplicitly]
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

                private Ranking()
                { }
            }
        }
    }
}