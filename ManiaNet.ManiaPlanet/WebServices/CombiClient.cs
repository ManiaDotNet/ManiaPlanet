using ManiaNet.ManiaPlanet.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.ManiaPlanet.WebServices
{
    /// <summary>
    /// Contains all the differnet Clients in one.
    /// </summary>
    [UsedImplicitly]
    public sealed class CombiClient
    {
        /// <summary>
        /// Gets the ManiaFlash Client.
        /// </summary>
        [NotNull, UsedImplicitly]
        public ManiaFlashClient ManiaFlash { get; private set; }

        /// <summary>
        /// Gets the Manialinks Client.
        /// </summary>
        [NotNull, UsedImplicitly]
        public ManialinksClient Manialinks { get; private set; }

        /// <summary>
        /// Gets the Players Client.
        /// </summary>
        [NotNull, UsedImplicitly]
        public PlayersClient Players { get; private set; }

        /// <summary>
        /// Gets the Rankings Client.
        /// </summary>
        [NotNull, UsedImplicitly]
        public RankingsClient Rankings { get; private set; }

        /// <summary>
        /// Gets the Servers Client.
        /// </summary>
        [NotNull, UsedImplicitly]
        public ServersClient Servers { get; private set; }

        /// <summary>
        /// Gets the Teams Client.
        /// </summary>
        [NotNull, UsedImplicitly]
        public TeamsClient Teams { get; private set; }

        /// <summary>
        /// Gets the Titles Client.
        /// </summary>
        [NotNull, UsedImplicitly]
        public TitlesClient Titles { get; private set; }

        /// <summary>
        /// Gets the Trust Circles Client.
        /// </summary>
        [NotNull, UsedImplicitly]
        public TrustCirclesClient TrustCircles { get; private set; }

        /// <summary>
        /// Gets the Zones Client.
        /// </summary>
        [NotNull, UsedImplicitly]
        public ZonesClient Zones { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="CombiClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public CombiClient([NotNull] string username, [NotNull] string password)
        {
            Rankings = new RankingsClient(username, password);
            ManiaFlash = new ManiaFlashClient(username, password);
            Manialinks = new ManialinksClient(username, password);
            Players = new PlayersClient(username, password);
            Servers = new ServersClient(username, password);
            Teams = new TeamsClient(username, password);
            Titles = new TitlesClient(username, password);
            TrustCircles = new TrustCirclesClient(username, password);
            Zones = new ZonesClient(username, password);
        }
    }
}