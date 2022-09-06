using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AemulusEx.Services
{
    public static class Utils
    {
        /// <summary>
        /// The folder that the Program is running in
        /// </summary>
        public static string AppLocation { get; } = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;
    }
}
