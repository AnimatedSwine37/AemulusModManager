using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AemulusEx.Models.OldModels
{
    /// <summary>
    /// Represents the old Loadout.xml files used in Aemulus
    /// </summary>
    class OldLoadout
    {
        public OldLoadoutPackages Packages { get; }

        public OldLoadout(OldLoadoutPackages packages)
        {
            Packages = packages;
        }
    }

    /// <summary>
    /// Represents another layer of the old Loadout.xml files
    /// </summary>
    class OldLoadoutPackages
    {
        public OldLoadoutPackageArray packages { get; }
        public string showHiddenPackages { get; }

        public OldLoadoutPackages(OldLoadoutPackageArray packages, string showHiddenPackages)
        {
            this.packages = packages;
            this.showHiddenPackages = showHiddenPackages;
        }
    }

    /// <summary>
    /// Represents the array of packages in the old Loadout.xml files (I know this is a convoluted way of doing this)
    /// </summary>
    class OldLoadoutPackageArray
    {
        public List<OldLoadoutPackage> Package { get; }

        public OldLoadoutPackageArray(List<OldLoadoutPackage> package)
        {
            Package = package;
        }
    }

    /// <summary>
    /// Reporesents the packages in old Loadout.xml files
    /// </summary>
    class OldLoadoutPackage
    {
        public string name { get; set; }
        public string path { get; set; }
        public string enabled { get; set; }
        public string id { get; set; }
        public string hidden { get; set; }
        public string link { get; set; }

        public OldLoadoutPackage(string name, string path, string enabled, string id, string hidden, string link)
        {
            this.name = name;
            this.path = path;
            this.enabled = enabled;
            this.id = id;
            this.hidden = hidden;
            this.link = link;
        }
    }
}
