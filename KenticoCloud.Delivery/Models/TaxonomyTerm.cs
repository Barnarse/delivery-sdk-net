﻿using Newtonsoft.Json.Linq;

namespace KenticoCloud.Delivery
{
    /// <summary>
    /// Represents a taxonomy term assigned to a Taxonomy element.
    /// </summary>
    public sealed class TaxonomyTerm
    {
        /// <summary>
        /// Gets the name of the taxonomy term.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the codename of the taxonomy term.
        /// </summary>
        public string Codename { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyTerm"/> class with the specified JSON data.
        /// </summary>
        /// <param name="source">The JSON data to deserialize.</param>
        internal TaxonomyTerm(JToken source)
        {
            Name = source["name"].ToString();
            Codename = source["codename"].ToString();
        }
    }
}
