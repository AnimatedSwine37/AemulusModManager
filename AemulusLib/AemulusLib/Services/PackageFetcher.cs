using AemulusEx.Models;
using AemulusEx.Models.OldModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AemulusEx.Services
{
    public class PackageFetcher
    {
        public string PackagesFolder { get; }

        public PackageFetcher(string packagesFolder)
        {
            PackagesFolder = packagesFolder;
        }

        public List<Package> GetPackages()
        {
            List<Package> packages = new List<Package>();
            if (!Directory.Exists(PackagesFolder))
                return packages;

            ConvertOldPackages();

            foreach (string packageFile in Directory.GetFiles(PackagesFolder, "Package.json", SearchOption.AllDirectories))
            {
                if(!TryGetPackage(packageFile, out Package package))
                {
                    Console.WriteLine($"Unable to parse invalid file {packageFile} to a package");
                    continue;
                }
                packages.Add(package);
            }
            
            return packages;
        }

        /// <summary>
        /// Converts old Aemulus Package.xml files to the newer Package.json files
        /// </summary>
        private void ConvertOldPackages()
        {
            foreach (var packageFile in Directory.GetFiles(PackagesFolder, "Package.xml", SearchOption.AllDirectories))
            {
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    string packageXml = File.ReadAllText(packageFile);
                    xmlDoc.LoadXml(packageXml);
                    string json = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented);
                    OldPackage? oldPackage = JsonSerializer.Deserialize<OldPackage>(json);
                    if (oldPackage == null)
                    {
                        Console.WriteLine($"Unable to parse invalid file {packageFile} to a package");
                        continue;
                    }

                    Uri.TryCreate(oldPackage.Metadata.link, UriKind.Absolute, out Uri? packageLink);
                    Author author = new Author(oldPackage.Metadata.author);
                    if (!Version.TryParse(oldPackage.Metadata.version, out Version? version))
                        version = new Version("1.0");

                    Package newPackage = new Package(oldPackage.Metadata.name, oldPackage.Metadata.id, author, version, packageLink, oldPackage.Metadata.description);
                    string newPackageJson = JsonSerializer.Serialize(newPackage);
                    File.WriteAllText(Path.ChangeExtension(packageFile, "json"), newPackageJson);
                    File.Delete(packageFile); // Remove the old Package.xml
                }
                catch (XmlException e)
                {
                    Console.WriteLine($"Error loading {packageFile}: {e.Message}");
                }
            }
        }

        /// <summary>
        /// Tries to get a <see cref="Package"/> from a path retunring true if it succeeds
        /// The found <see cref="Package"/> is returned in the out variable
        /// </summary>
        /// <param name="packageDir">The full path to the package's folder</param>
        /// <param name="package">The <see cref="Package"/> that is found. If no <see cref="Package"/> was found this will have default values.</param>
        /// <returns>True if the package exists, otherwise false</returns>
        public bool TryGetPackage(string packageDir, out Package package)
        {
            package = new Package();
            if (!File.Exists(Path.Combine(packageDir, "Package.json")))
                return false;

            string json = File.ReadAllText(Path.Combine(packageDir, "Package.json"));
            Package? parsedPackage = JsonSerializer.Deserialize<Package>(json);
            if (parsedPackage == null)
                return false;

            package = parsedPackage;
            return true;
        }
    }
}
