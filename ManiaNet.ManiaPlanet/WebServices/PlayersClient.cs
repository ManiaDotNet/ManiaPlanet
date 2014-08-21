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
        /// Gets the number of Maniastars for the Player given by the login. Null when the information couldn't be found.
        /// </summary>
        /// <param name="login">The login of the Player.</param>
        /// <returns>The number of Maniastars the Player has. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<int?> GetManiastarsAsyncFor([NotNull] string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return -1;

            var response = await execute(RequestType.Get, "players/" + login + "/index.txt");

            int n;
            return int.TryParse(response, out n) ? n : (int?)null;
        }
    }
}