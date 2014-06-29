using ManiaNet.ManiaPlanet.XmlEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ManiaNet.ManiaPlanet
{
    /// <summary>
    /// Contains static helpers and information about the various Nations.
    /// </summary>
    public static class Nations
    {
        private static Dictionary<string, Nation> nations = new Dictionary<string, Nation>();

        static Nations()
        {
            XElement nationsList = XDocument.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("ManiaNet.ManiaPlanet.NationsList.xml")).Root;

            foreach (var nationElement in nationsList.Elements())
            {
                Nation nation = new Nation();

                if (!nation.ParseXml(nationElement))
                    throw new FormatException("NationsList.xml wasn't in the correct format.");

                nations.Add(nation.Path.ToLower(), nation);
            }
        }

        /// <summary>
        /// Gets the nation-information for the nation in the given zone path.
        /// </summary>
        /// <param name="path">The zone path to get the nation-information for.</param>
        /// <returns>The nation-information for the given zone path.</returns>
        public static Nation GetNation(string path)
        {
            string nationPath = GetNationPath(path).ToLower();

            if (!nations.ContainsKey(nationPath))
                return nations[""];

            return nations[nationPath];
        }

        /// <summary>
        /// Takes a zone path and stript it to be only a path to the nation.
        /// </summary>
        /// <param name="path">The zone path to strip.</param>
        /// <returns>The path to the nation.</returns>
        public static string GetNationPath(string path)
        {
            int firstPipe = path.IndexOf('|');

            if (firstPipe < 0 || path.Length == firstPipe + 1)
                return string.Empty;

            int secondPipe = path.IndexOf('|', firstPipe + 1);

            if (secondPipe < 0 || path.Length == secondPipe + 1)
                return string.Empty;

            int thirdPipe = path.IndexOf('|', secondPipe + 1);

            if (thirdPipe < 0)
                return path;

            return path.Substring(0, thirdPipe);
        }
    }
}