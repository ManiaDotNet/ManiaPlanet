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
    /// Contains methods for accessing the Title infos.
    /// </summary>
    public sealed class TitlesClient : WSClient
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TitlesClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public TitlesClient([NotNull] string username, [NotNull] string password)
            : base(username, password)
        { }

        /// <summary>
        /// Gets the <see cref="TitleInfo"/> for the Title with the given IdString. Null when the information couldn't be found.
        /// </summary>
        /// <param name="id">The IdString of the Title.</param>
        /// <returns>The Title Information. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<TitleInfo> GetInfoAsyncFor(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            var response = await execute(RequestType.Get, "titles/" + id + "/index.json");

            return response == null ? null : jsonSerializer.Deserialize<TitleInfo>(new JsonTextReader(new StringReader(response)));
        }
    }
}