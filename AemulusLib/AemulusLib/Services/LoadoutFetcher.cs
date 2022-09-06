using AemulusEx.Models;
using AemulusEx.Models.Enums;
using AemulusEx.Models.OldModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AemulusEx.Services
{
    public static class LoadoutFetcher
    {
        
        public static List<Loadout> GetLoadouts(string loadoutsPath)
        {
            List<Loadout> loadouts = new();
            loadouts.Concat(ConvertOldLoadouts(loadoutsPath));
            string loadoutsJson = File.ReadAllText(loadoutsPath);
            List<Loadout>? parsedLoadouts = JsonSerializer.Deserialize<List<Loadout>>(loadoutsJson);
            if (parsedLoadouts != null)
                loadouts.Concat(parsedLoadouts);
            return loadouts;
        }

        /// <summary>
        /// Converts old Aemulus Loadout.xml files to a List of new Loadout objects, deleting the old loadout files once done
        /// </summary>
        private static List<Loadout> ConvertOldLoadouts(string loadoutsPath)
        {
            List<Loadout> loadouts = new List<Loadout>();

            if (!Directory.Exists(loadoutsPath))
                return loadouts;

            // Get the loadouts for each game as they were seperated by folder
            foreach (var gameFolder in Directory.GetDirectories(loadoutsPath))
            {
                foreach (var loadoutFile in Directory.GetFiles(gameFolder, "*.xml"))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    try
                    {
                        string loadoutXml = File.ReadAllText(loadoutFile);
                        xmlDoc.LoadXml(loadoutXml);
                        string json = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented);
                        json = Regex.Replace(json, "(Package\":) ([^[].*[^]]\\s*)},", "$1[$2]},", RegexOptions.Singleline);
                        OldLoadout? oldLoadout = JsonSerializer.Deserialize<OldLoadout>(json);
                        if (oldLoadout == null)
                        {
                            Console.WriteLine($"Unable to parse invalid file {loadoutFile} to a package");
                            continue;
                        }

                        // Get the information from the old loadout
                        string name = Path.GetFileNameWithoutExtension(loadoutFile);
                        string description = "A loadout automatically imported from the old Aemulus format.";
                        string gameName = new DirectoryInfo(gameFolder).Name;
                        Game game = (Game)Enum.Parse(typeof(Game), gameName.Replace(" ", "_"));
                        List<LoadoutPackage> packages = ConvertOldLoadoutPackages(oldLoadout.Packages.packages.Package, gameName);

                        loadouts.Add(new Loadout(name, description, game, packages));
                        File.Delete(loadoutFile); // Remove the old Loadout xml
                    }
                    catch (XmlException e)
                    {
                        Console.WriteLine($"Error loading {loadoutFile}: {e.Message}");
                    }
                }
            }

            return loadouts;
        }

        /// <summary>
        /// Converts the old loadout packages to the new ones
        /// </summary>
        /// <param name="oldPackages">The list of old <see cref="LoadoutPackage"/> generated from an old Loadout xml</param>
        /// <param name="game">The name of the game the packages are from</param>
        /// <returns></returns>
        private static List<LoadoutPackage> ConvertOldLoadoutPackages(List<OldLoadoutPackage> oldPackages, string game)
        {
            List<LoadoutPackage> newPackages = new List<LoadoutPackage>();

            foreach (var oldPackage in oldPackages)
            {
                string packageDir = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, "Packages", game, oldPackage.path);
                if (!PackageFetcher.TryGetPackage(packageDir, out Package newPackage))
                {
                    // Create a dummy package with what information is available from the from the loadout package data as the package doesn't exist
                    newPackage.Id = oldPackage.id;
                    newPackage.Name = oldPackage.name;
                    if (Uri.TryCreate(oldPackage.link, UriKind.Absolute, out Uri? link))
                        newPackage.Link = link;
                }

                bool.TryParse(oldPackage.enabled, out bool enabled);
                bool.TryParse(oldPackage.hidden, out bool hidden);

                newPackages.Add(new LoadoutPackage(newPackage, enabled, !hidden));
            }

            return newPackages;
        }
    }
}
