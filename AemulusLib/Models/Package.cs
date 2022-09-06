using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AemulusEx.Models
{
    public class Package
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public Author Author { get; set; }
        public Version Version { get; set; }
        public Uri? Link { get; set; }
        public string Description { get; set; }

        public Package(string name, string id, Author author, Version version, Uri? link, string description)
        {
            Name = name;
            Id = id;
            Author = author;
            Version = version;
            Link = link;
            Description = description;
        }

        /// <summary>
        /// Declares a <see cref="Package"/> with default values
        /// </summary>
        public Package() : this("unknown", "unknown", new Author(), new Version(1, 0), null, "") { }
    }
}
