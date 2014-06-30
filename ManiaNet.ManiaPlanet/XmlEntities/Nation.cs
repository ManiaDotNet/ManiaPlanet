using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ManiaNet.ManiaPlanet.XmlEntities
{
    /// <summary>
    /// Represents a nation-entity from the NationsList.xml
    /// </summary>
    public class Nation
    {
        /// <summary>
        /// Gets the name (path) of the nation's avatar, relative to the UserData folder.
        /// </summary>
        public string AvatarName { get; private set; }

        /// <summary>
        /// Gets the Hymn of the Nation. Empty string for all.
        /// </summary>
        public string Hymn { get; private set; }

        /// <summary>
        /// Gets the name of the nation.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the zone path. World|&lt;Continent&gt;|&lt;Nation&gt;
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the informations for the nation's skin.
        /// </summary>
        public Skin Skin { get; private set; }

        /// <summary>
        /// Fills the properties with the information from the given nation-XElement.
        /// </summary>
        /// <param name="xElement">The nation-XElement containing the information.</param>
        /// <returns>Whether it was successul or not.</returns>
        public bool ParseXml(XElement xElement)
        {
            if (!xElement.Name.LocalName.Equals("nation")
             || !xElement.HasAttributes || xElement.Attributes().Count() != 6
             || !xElement.HasElements || xElement.Elements().Count() != 1)
                return false;

            XAttribute path = xElement.Attribute("path");
            XAttribute hymn = xElement.Attribute("hymn");
            XAttribute avatarName = xElement.Attribute("avatar_name");

            if (path == null || hymn == null || avatarName == null)
                return false;

            Path = path.Value;
            Hymn = hymn.Value;
            AvatarName = avatarName.Value;

            string[] zoneparts = Path.Split('|');
            Name = (zoneparts.Length >= 3) ? zoneparts[2] : "Other";

            XElement skin = xElement.Elements().First();

            if (this.Skin == null)
                this.Skin = new Skin();

            if (!this.Skin.ParseXml(skin))
                return false;

            return true;
        }
    }
}