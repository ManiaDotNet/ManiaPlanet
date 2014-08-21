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
    /// Contains methods for accessing the Player infos.
    /// </summary>
    [UsedImplicitly]
    public sealed class PlayersClient : WSClient
    {
        /// <summary>
        /// Creates a new instance of the <see cref="PlayersClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public PlayersClient([NotNull] string username, [NotNull] string password)
            : base(username, password)
        { }

        /// <summary>
        /// Gets the <see cref="PlayerInfo"/> for the Player given by the login. Null when the information couldn't be found.
        /// </summary>
        /// <param name="login">The login of the Player.</param>
        /// <returns>The information about the Player. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<PlayerInfo> GetInfoAsyncFor([NotNull] string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;

            var response = await execute(RequestType.Get, "players/" + login + "/index.json");

            return response == null ? null : jsonSerializer.Deserialize<PlayerInfo>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the number of Maniastars for the Player given by the login. -1 when the information couldn't be found.
        /// </summary>
        /// <param name="login">The login of the Player.</param>
        /// <returns>The number of Maniastars the Player has. -1 when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<int> GetManiastarsAsyncFor([NotNull] string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return -1;

            var response = await execute(RequestType.Get, "players/" + login + "/index.txt");

            int n;
            return int.TryParse(response, out n) ? n : -1;
        }

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
}