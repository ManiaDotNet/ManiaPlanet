using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManiaNet.ManiaPlanet.WebServices
{
    /// <summary>
    /// Base class for clients that access the ManiaPlanet WebServices.
    /// </summary>
    public abstract class WSClient
    {
        /// <summary>
        /// The base url of for the WebServices. http://ws.maniaplanet.com/
        /// </summary>
        public const string BaseUrl = "http://ws.maniaplanet.com/";

        private readonly HttpClient httpClient;

        /// <summary>
        /// Creates a new instance of the <see cref="ManiaNet.ManiaPlanet.WebServices.WSClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        protected WSClient(string username, string password)
        {
            var httpHandler = new HttpClientHandler { Credentials = new NetworkCredential(username, password) };
            httpClient = new HttpClient(httpHandler) { BaseAddress = new Uri(BaseUrl) };
        }

        /// <summary>
        /// Executes the web request specified by the given arguments. Returns null on failure.
        /// </summary>
        /// <param name="requestType">The type of the request.</param>
        /// <param name="resourcePath">The path that is appended to the BaseUrl. Includes the query string.</param>
        /// <param name="requestBody">Optional request body that is send with POST and PUT requests.</param>
        /// <returns>The content returned by the web request. Null on failure.</returns>
        protected async Task<string> execute(RequestType requestType, string resourcePath, string requestBody = "")
        {
            HttpResponseMessage response;
            switch (requestType)
            {
                case RequestType.GET:
                    response = await httpClient.GetAsync(resourcePath);
                    return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;

                case RequestType.POST:
                    response = await httpClient.PostAsync(resourcePath, new StringContent(requestBody));
                    return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;

                case RequestType.PUT:
                    response = await httpClient.PutAsync(resourcePath, new StringContent(requestBody));
                    return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;

                case RequestType.DELETE:
                    response = await httpClient.DeleteAsync(resourcePath);
                    return response.IsSuccessStatusCode ? string.Empty : null;

                default:
                    throw new ArgumentOutOfRangeException("requestType", "Has to be a valid member of the enum.");
            }
        }

        /// <summary>
        /// The HTTP-RequestTypes supported by the WebServices Client.
        /// </summary>
        protected enum RequestType
        {
            GET,
            POST,
            PUT,
            DELETE,
        }
    }
}