using ManiaNet.ManiaPlanet.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.ManiaPlanet.WebServices
{
    /// <summary>
    /// Stores information about the Title played on a Server.
    /// </summary>
    [JsonObject, UsedImplicitly]
    // ReSharper disable once ClassCannotBeInstantiated
    public sealed class TitleInfo
    {
        [CanBeNull, JsonProperty("dependencies")]
        private string[] dependencies;

        [JsonProperty("isCustom"), UsedImplicitly]
        private byte? isCustom;

        /// <summary>
        /// Gets the cost of the Title. May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("cost")]
        public uint? Cost
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Ids of the Titles that this Title depends on. May be empty if the data wasn't complete, or there are none.
        /// </summary>
        [JsonIgnore, UsedImplicitly]
        public IEnumerable<string> Dependencies
        {
            get
            {
                if (dependencies == null)
                    yield break;

                foreach (var dependency in dependencies)
                    yield return dependency;
            }
        }

        /// <summary>
        /// Gets the Id of the Title. May be null if the data wasn't complete.
        /// </summary>
        [JsonProperty("id"), UsedImplicitly]
        public uint? Id
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the Id-String of the Title. May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("idString")]
        public string IdString
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets whether the Title is a custom (i.e. user-made) Title. May be null if the data wasn't complete.
        /// </summary>
        [JsonIgnore, UsedImplicitly]
        public bool? IsCustom
        {
            get { return !isCustom.HasValue ? (bool?)null : Convert.ToBoolean(isCustom.Value); }
        }

        /// <summary>
        /// Gets the name of the Title. May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("name")]
        public string Name
        {
            get;
            [UsedImplicitly]
            private set;
        }

        /// <summary>
        /// Gets the address of the website of the Title. May be null if the data wasn't complete.
        /// </summary>
        [CanBeNull, JsonProperty("web")]
        public string Website
        {
            get;
            [UsedImplicitly]
            private set;
        }

        private TitleInfo()
        { }
    }
}