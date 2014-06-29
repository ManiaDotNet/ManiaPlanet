using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ManiaNet.ManiaPlanet.XmlEntities
{
    /// <summary>
    /// Represents a skin-entity that's part of the nation-entities in the NationsList.xml
    /// </summary>
    public class Skin
    {
        /// <summary>
        /// Gets the author of the skin.
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// Gets what collection the skin is part of.
        /// </summary>
        public string Collection { get; private set; }

        /// <summary>
        /// Gets an identifier describing what kind of car it is.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the name (path) of the skin, relative to the UserData folder.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Fills the properties with the information from the given skin-XElement.
        /// </summary>
        /// <param name="xElement">The skin-XElement containing the information.</param>
        /// <returns>Whether it was successul or not.</returns>
        public bool ParseXml(XElement xElement)
        {
            if (!xElement.Name.LocalName.Equals("skin")
             || xElement.HasElements
             || !xElement.HasAttributes || xElement.Attributes().Count() != 7)
                return false;

            XAttribute id = xElement.Attribute("id");
            XAttribute collection = xElement.Attribute("collection");
            XAttribute author = xElement.Attribute("author");
            XAttribute name = xElement.Attribute("name");

            if (id == null || collection == null || author == null || name == null)
                return false;

            Id = id.Value;
            Collection = collection.Value;
            Author = author.Value;
            Name = name.Value;

            return true;
        }
    }
}