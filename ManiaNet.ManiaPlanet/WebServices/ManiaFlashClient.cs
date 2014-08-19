using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.ManiaPlanet.WebServices
{
    public class ManiaFlashClient : WSClient
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ManiaNet.ManiaPlanet.WebServices.ManiaFlashClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public ManiaFlashClient(string username, string password)
            : base(username, password)
        { }
    }
}