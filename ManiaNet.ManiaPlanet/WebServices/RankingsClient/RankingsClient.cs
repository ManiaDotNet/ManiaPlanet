using ManiaNet.ManiaPlanet.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.ManiaPlanet.WebServices
{
    /// <summary>
    /// Contains methods for accessing the Ranking infos.
    /// </summary>
    [UsedImplicitly]
    public sealed partial class RankingsClient : WSClient
    {
        /// <summary>
        /// Creates a new instance of the <see cref="RankingsClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public RankingsClient([NotNull] string username, [NotNull] string password)
            : base(username, password)
        { }
    }
}