using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AemulusEx.Models.OldModels
{
    // A class representing the old Package.xml files used by Aemulus
    class OldPackage
    {
        public OldPackageMetadata Metadata { get; set; }
    }

    class OldPackageMetadata
    {
        public string name { get; set; }
        public string id { get; set; }
        public string author { get; set; }
        public string version { get; set; }
        public string link { get; set; }
        public string description { get; set; }
    }
}
