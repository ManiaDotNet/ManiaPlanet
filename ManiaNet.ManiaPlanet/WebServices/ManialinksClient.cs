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
    /// Contains methods for accessing the Manialink infos.
    /// </summary>
    [UsedImplicitly]
    public sealed class ManialinksClient : WSClient
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ManialinksClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public ManialinksClient([NotNull] string username, [NotNull] string password)
            : base(username, password)
        { }

        /// <summary>
        /// Gets the <see cref="ManialinkInfo"/> for the given Manialink code. Null when the information couldn't be found.
        /// </summary>
        /// <param name="code">The short code for the Manialink.</param>
        /// <returns>The information about the Manialink. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<ManialinkInfo> GetInfoAsyncFor([NotNull] string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            var response = await execute(RequestType.Get, "manialinks/" + code + "/index.json");

            return response == null ? null : jsonSerializer.Deserialize<ManialinkInfo>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Stores information about a Manialink.
        /// </summary>
        [JsonObject, UsedImplicitly]
        // ReSharper disable once ClassCannotBeInstantiated
        public sealed class ManialinkInfo
        {
            /// <summary>
            /// Gets the short code for the Manialink. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("code")]
            public string Code
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the login of the Owner of the Manialink. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("login")]
            public string OwnerLogin
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the cost in Planets to visit the Manialink. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("planetCost"), UsedImplicitly]
            public uint? PlanetCost
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the Url that the Manialink points to. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("url")]
            public string Url
            {
                get;
                [UsedImplicitly]
                private set;
            }

            private ManialinkInfo()
            { }
        }
    }
}