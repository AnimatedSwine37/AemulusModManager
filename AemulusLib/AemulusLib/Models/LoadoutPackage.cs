using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AemulusEx.Models
{
    /// <summary>
    /// Information about how a <see cref="Package"/> is displayed in the <see cref="Views.LoadoutView"/>
    /// </summary>
    public class LoadoutPackage
    {
        public Package Package { get; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }

        public LoadoutPackage(Package package, bool enabled, bool visible)
        {
            Package = package;
            Enabled = enabled;
            Visible = visible;
        }
    }
}
