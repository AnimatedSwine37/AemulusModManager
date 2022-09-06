using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AemulusEx.Models
{
    public class Author
    {
        public string Name { get; set; }

        public Author(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Declares an <see cref="Author"/> with default values
        /// </summary>
        public Author() : this("unknown") { }
    }
}
