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
    /// Contains methods for accessing the TrustCircles infos.
    /// </summary>
    [UsedImplicitly]
    public sealed class TrustCirclesClient : WSClient
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TrustCirclesClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public TrustCirclesClient([NotNull] string username, [NotNull] string password)
            : base(username, password)
        { }

        /// <summary>
        /// Gets the <see cref="Karma"/> of the given Player in the given Trust Circle. Null when the data couldn't be found.
        /// </summary>
        /// <param name="circle">The name of the Trust Circle.</param>
        /// <param name="login">The login of the Player.</param>
        /// <returns>The Karma of the Player in the Trust Circle.</returns>
        [UsedImplicitly]
        public async Task<Karma> GetKarmaAsyncFor([NotNull] string circle, [NotNull] string login)
        {
            if (string.IsNullOrWhiteSpace(circle) || string.IsNullOrWhiteSpace(login))
                return null;

            var response = await execute(RequestType.Get, "trust/" + circle + "/karma/" + login + "/index.json");

            if (response == null)
                return null;

            var karma = jsonSerializer.Deserialize<Karma>(new JsonTextReader(new StringReader(response)));

            if (karma == null)
                return null;

            karma.GetType().GetProperty("Login").GetSetMethod(true).Invoke(karma, new object[] { login });
            return karma;
        }

        /// <summary>
        /// Stores information about a Player's Karma in a Trust Circle.
        /// </summary>
        [JsonObject, UsedImplicitly]
        // ReSharper disable once ClassCannotBeInstantiated
        public sealed class Karma
        {
            /// <summary>
            /// Gets the number of times that the player has been blacklisted in the Trust Circle. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("blacks"), UsedImplicitly]
            public uint? Blacks
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the login of the Player. Won't be null.
            /// </summary>
            [NotNull, JsonIgnore, UsedImplicitly]
            public string Login
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the number of times that the player has been whitelisted in the Trust Circle. May be null if the data wasn't complete.
            /// </summary>
            [JsonProperty("whites"), UsedImplicitly]
            public uint? Whites
            {
                get;
                [UsedImplicitly]
                private set;
            }

            private Karma()
            { }
        }
    }
}