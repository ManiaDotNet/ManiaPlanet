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
        /// Blacklists the Player given by the login, and returns whether it was successful.
        /// </summary>
        /// <param name="login">The login of the Player.</param>
        /// <returns>Whether it was successful.</returns>
        [UsedImplicitly]
        public async Task<bool> BlacklistPlayerAsync([NotNull] string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return false;

            var response = await execute(RequestType.Post, "trust/black/index.txt", login);

            return response != null;
        }

        /// <summary>
        /// Gets the <see cref="ListEntry"/>s on the Blacklist of the given Trust Circle. Null when the data couldn't be found.
        /// </summary>
        /// <param name="circle">The name of the Trust Circle.</param>
        /// <returns>The Entries on the Blacklist of the Trust Circle.</returns>
        [UsedImplicitly]
        public async Task<ListEntry[]> GetBlacklistAsyncFor([NotNull] string circle)
        {
            if (string.IsNullOrWhiteSpace(circle))
                return null;

            var response = await execute(RequestType.Get, "trust/" + circle + "/black/index.json");

            return response == null ? null : jsonSerializer.Deserialize<ListEntry[]>(new JsonTextReader(new StringReader(response)));
        }

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

            return response == null ? null : jsonSerializer.Deserialize<Karma>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the logins of the Players that are on your own Blacklist. Null when the data couldn't be found.
        /// </summary>
        /// <returns>The logins of the Players on your own Blacklist. Null when the data couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<string[]> GetOwnBlacklistAsync()
        {
            var response = await execute(RequestType.Get, "trust/black/index.json");

            return response == null ? null : jsonSerializer.Deserialize<string[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the logins of the Players that are on your own Whitelist. Null when the data couldn't be found.
        /// </summary>
        /// <returns>The logins of the Players on your own Whitelist. Null when the data couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<string[]> GetOwnWhitelistAsync()
        {
            var response = await execute(RequestType.Get, "trust/white/index.json");

            return response == null ? null : jsonSerializer.Deserialize<string[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the <see cref="ListEntry"/>s on the Whitelist of the given Trust Circle. Null when the data couldn't be found.
        /// </summary>
        /// <param name="circle">The name of the Trust Circle.</param>
        /// <returns>The Entries on the Whitelist of the Trust Circle.</returns>
        [UsedImplicitly]
        public async Task<ListEntry[]> GetWhitelistAsyncFor([NotNull] string circle)
        {
            if (string.IsNullOrWhiteSpace(circle))
                return null;

            var response = await execute(RequestType.Get, "trust/" + circle + "/white/index.json");

            return response == null ? null : jsonSerializer.Deserialize<ListEntry[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Unblacklists the Player given by the login, and returns whether it was successful.
        /// </summary>
        /// <param name="login">The login of the Player.</param>
        /// <returns>Whether it was successful.</returns>
        [UsedImplicitly]
        public async Task<bool> UnblacklistPlayerAsync([NotNull] string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return false;

            var response = await execute(RequestType.Post, "trust/unblack/index.txt", login);

            return response != null;
        }

        /// <summary>
        /// Unwhitelists the Player given by the login, and returns whether it was successful.
        /// </summary>
        /// <param name="login">The login of the Player.</param>
        /// <returns>Whether it was successful.</returns>
        [UsedImplicitly]
        public async Task<bool> UnwhitelistPlayerAsync([NotNull] string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return false;

            var response = await execute(RequestType.Post, "trust/unwhite/index.txt", login);

            return response != null;
        }

        /// <summary>
        /// Whitelists the Player given by the login, and returns whether it was successful.
        /// </summary>
        /// <param name="login">The login of the Player.</param>
        /// <returns>Whether it was successful.</returns>
        [UsedImplicitly]
        public async Task<bool> WhitelistPlayerAsync([NotNull] string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return false;

            var response = await execute(RequestType.Post, "trust/white/index.txt", login);

            return response != null;
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

        /// <summary>
        /// Stores informationation about an entry in a Black- or Whitelist.
        /// </summary>
        public sealed class ListEntry
        {
            /// <summary>
            /// Gets the number of times that the Player was listed.
            /// </summary>
            [JsonProperty("count"), UsedImplicitly]
            public uint? Count
            {
                get;
                [UsedImplicitly]
                private set;
            }

            /// <summary>
            /// Gets the login of the Player. May be null if the data wasn't complete.
            /// </summary>
            [CanBeNull, JsonProperty("login")]
            public string Login
            {
                get;
                [UsedImplicitly]
                private set;
            }
        }
    }
}