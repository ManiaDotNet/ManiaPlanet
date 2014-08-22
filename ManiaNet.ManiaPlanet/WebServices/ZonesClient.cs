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
    /// Contains methods for accessing the Zone infos.
    /// </summary>
    [UsedImplicitly]
    public sealed class ZonesClient : WSClient
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ZonesClient"/> class with the given credentials.
        /// </summary>
        /// <param name="username">The WebServices username.</param>
        /// <param name="password">The WebServices password.</param>
        public ZonesClient([NotNull] string username, [NotNull] string password)
            : base(username, password)
        { }

        /// <summary>
        /// Iterates through all <see cref="ZoneInfo"/>s, starting at 0. Empty when the information couldn't be found.
        /// <para/>
        /// Only use this if you really know what you're doing. It will possibly iterate through *ALL* ManiaPlanet Zones.
        /// </summary>
        /// <param name="stepSize">The size of each badge of ZoneInfos that is downloaded.</param>
        /// <param name="retries">The maximum number of retries when the information couldn't be found.</param>
        /// <param name="sort">The Zone-Identifier to sort the results by.</param>
        /// <param name="order">The order to sort the results in.</param>
        /// <returns>All ZoneInfos. Empty when the information couldn't be found.</returns>
        [UsedImplicitly]
        public IEnumerable<ZoneInfo> GetAllZoneInfos(uint stepSize = 50, uint retries = 3, Sort sort = Sort.Id, Order order = Order.Ascending)
        {
            if (stepSize == 0 || (sort != Sort.Id && sort != Sort.Path) || (order != Order.Ascending && order != Order.Descending))
                yield break;

            var baseQuery = "/zones/all/index.json?length=" + stepSize + "&sort=" + sort.ToString().ToLower() + "&order=" + (sbyte)order + "&offset=";

            var shouldQuery = true;
            uint offset = 0;
            uint retried = 0;
            var pendingZoneInfos = new Stack<ZoneInfo>();
            var runningQuery = execute(RequestType.Get, baseQuery + offset);

            while (shouldQuery || pendingZoneInfos.Count > 0)
            {
                if (shouldQuery && pendingZoneInfos.Count == 0)
                {
                    var response = runningQuery.Result;
                    var result = response == null ? null : jsonSerializer.Deserialize<ZoneInfo[]>(new JsonTextReader(new StringReader(response)));

                    if (result != null)
                    {
                        if (result.Length < stepSize)
                            shouldQuery = false;

                        pendingZoneInfos = new Stack<ZoneInfo>(result);
                        offset += stepSize;
                    }
                    else
                        retried++;
                }

                if (shouldQuery && pendingZoneInfos.Count < stepSize)
                {
                    if (retried < retries)
                        runningQuery = execute(RequestType.Get, baseQuery + offset);
                    else
                        shouldQuery = false;
                }

                if (pendingZoneInfos.Count > 0)
                    yield return pendingZoneInfos.Pop();
            }
        }

        /// <summary>
        /// Gets the specified number of <see cref="ZoneInfo"/>s, starting at the given index (0-based).
        /// All the returned Zones are children of the Zone given by the Id.
        /// Null when the information couldn't be found.
        /// </summary>
        /// <param name="id">The Id of the parent Zone.</param>
        /// <param name="offset">The starting index (0-based).</param>
        /// <param name="length">The maximum number of Zones to return.</param>
        /// <param name="sort">The Zone-Identifier to sort the results by.</param>
        /// <param name="order">The order to sort the results in.</param>
        /// <returns>The children ZoneInfos in the given range. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<ZoneInfo[]> GetChildrenInfosByIdAsync(uint id, uint offset = 0, uint length = 10, Sort sort = Sort.Id, Order order = Order.Ascending)
        {
            if (id == 0 || (sort != Sort.Id && sort != Sort.Path) || (order != Order.Ascending && order != Order.Descending))
                return null;

            if (length == 0)
                return new ZoneInfo[0];

            var response = await execute(RequestType.Get,
                "zones/id/" + id + "/children/index.json?offset=" + offset + "&length=" + length + "&sort=" + sort.ToString().ToLower() + "&order=" + (sbyte)order);

            return response == null ? null : jsonSerializer.Deserialize<ZoneInfo[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the specified number of <see cref="ZoneInfo"/>s, starting at the given index (0-based).
        /// All the returned Zones are children of the Zone given by the Path.
        /// Null when the information couldn't be found.
        /// </summary>
        /// <param name="path">The Path of the parent Zone.</param>
        /// <param name="offset">The starting index (0-based).</param>
        /// <param name="length">The maximum number of Zones to return.</param>
        /// <param name="sort">The Zone-Identifier to sort the results by.</param>
        /// <param name="order">The order to sort the results in.</param>
        /// <returns>The children ZoneInfos in the given range. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<ZoneInfo[]> GetChildrenInfosByPathAsync([NotNull] string path, uint offset = 0, uint length = 10, Sort sort = Sort.Id, Order order = Order.Ascending)
        {
            if (string.IsNullOrWhiteSpace(path) || (sort != Sort.Id && sort != Sort.Path) || (order != Order.Ascending && order != Order.Descending))
                return null;

            if (length == 0)
                return new ZoneInfo[0];

            var response = await execute(RequestType.Get,
                "zones/path/" + path + "/children/index.json?offset=" + offset + "&length=" + length + "&sort=" + sort.ToString().ToLower() + "&order=" + (sbyte)order);

            return response == null ? null : jsonSerializer.Deserialize<ZoneInfo[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the Id of the Zone given by the Path. Null when the information couldn't be found.
        /// </summary>
        /// <param name="path">The Path of the Zone.</param>
        /// <returns>The Id of the Zone. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<uint?> GetIdAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            var response = await execute(RequestType.Get, "zones/path/" + path + "/id/index.txt");

            uint id;
            return !uint.TryParse(response, out id) ? (uint?)null : id;
        }

        /// <summary>
        /// Gets the <see cref="ZoneInfo"/> for the Zone given by the Id. Null when the information couldn't be found.
        /// </summary>
        /// <param name="id">The Id of the Zone.</param>
        /// <returns>The Zone-Info.</returns>
        [UsedImplicitly]
        public async Task<ZoneInfo> GetInfoByIdAsync(uint id)
        {
            if (id == 0)
                return null;

            var response = await execute(RequestType.Get, "zones/id/" + id + "/index.json");

            return response == null ? null : jsonSerializer.Deserialize<ZoneInfo>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the <see cref="ZoneInfo"/> for the Zone given by the Path. Null when the information couldn't be found.
        /// </summary>
        /// <param name="path">The Path of the Zone. Zones are separated by pipe characters ('|').</param>
        /// <returns>The Zone-Info.</returns>
        [UsedImplicitly]
        public async Task<ZoneInfo> GetInfoByPathAsync([NotNull] string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            var response = await execute(RequestType.Get, "zones/path/" + path + "/index.json");

            return response == null ? null : jsonSerializer.Deserialize<ZoneInfo>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the specified number of <see cref="ZoneInfo"/>s, starting at the given index (0-based). Null when the information couldn't be found.
        /// </summary>
        /// <param name="offset">The starting index (0-based).</param>
        /// <param name="length">The maximum number of Zones to return.</param>
        /// <param name="sort">The Zone-Identifier to sort the results by.</param>
        /// <param name="order">The order to sort the results in.</param>
        /// <returns>The ZoneInfos in the given range. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<ZoneInfo[]> GetInfosAsync(uint offset = 0, uint length = 10, Sort sort = Sort.Id, Order order = Order.Ascending)
        {
            if ((sort != Sort.Id && sort != Sort.Path) || (order != Order.Ascending && order != Order.Descending))
                return null;

            if (length == 0)
                return new ZoneInfo[0];

            var response =
                await execute(RequestType.Get, "zones/all/index.json?offset=" + offset + "&length=" + length + "&sort=" + sort.ToString().ToLower() + "&order=" + (sbyte)order);

            return response == null ? null : jsonSerializer.Deserialize<ZoneInfo[]>(new JsonTextReader(new StringReader(response)));
        }

        /// <summary>
        /// Gets the population of the Zone given by the Id. Null when the information couldn't be found.
        /// </summary>
        /// <param name="id">The Id of the Zone.</param>
        /// <returns>The population of the Zone. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<uint?> GetPopulationByIdAsync(uint id)
        {
            if (id == 0)
                return null;

            var response = await execute(RequestType.Get, "zones/id/" + id + "/population/index.txt");

            uint p;
            return !uint.TryParse(response, out p) ? (uint?)null : p;
        }

        /// <summary>
        /// Gets the population of the Zone given by the Path. Null when the information couldn't be found.
        /// </summary>
        /// <param name="path">The Path of the Zone.</param>
        /// <returns>The population of the Zone. Null when the information couldn't be found.</returns>
        [UsedImplicitly]
        public async Task<uint?> GetPopulationByPathAsync([NotNull] string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            var response = await execute(RequestType.Get, "zones/path/" + path + "/population/index.txt");

            uint p;
            return !uint.TryParse(response, out p) ? (uint?)null : p;
        }

        /// <summary>
        /// Contains the options for the ordering of the Zone results.
        /// </summary>
        [UsedImplicitly]
        public enum Order : sbyte
        {
            /// <summary>
            /// Results will be ordered in Descending order.
            /// </summary>
            [UsedImplicitly]
            Descending = -1,

            /// <summary>
            /// Results will be ordered in Ascending order.
            /// </summary>
            [UsedImplicitly]
            Ascending = 1
        }

        /// <summary>
        /// Contains the options for the sorting of the Zone results.
        /// </summary>
        [UsedImplicitly]
        public enum Sort : byte
        {
            /// <summary>
            /// Results will be sorted by the Id.
            /// </summary>
            [UsedImplicitly]
            Id,

            /// <summary>
            /// Results will be sorted by the  Path.
            /// </summary>
            [UsedImplicitly]
            Path
        }
    }
}