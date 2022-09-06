using AemulusEx.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AemulusEx.Models
{
    /// <summary>
    /// A loadout displayed in the <see cref="Views.LoadoutView"/>
    /// </summary>
    public class Loadout
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Game Game { get; set; }
        public List<LoadoutPackage> Packages { get; set; }

        public Loadout(string name, string description, Game game, List<LoadoutPackage> packages)
        {
            Name = name;
            Description = description;
            Game = game;
            Packages = packages;
        }

        /// <summary>
        /// Creates a new Loadout with default values
        /// </summary>
        public Loadout()
        {
            Name = "Default";
            Description = "";
            Game = Game.Persona_4_Golden;
            Packages = new List<LoadoutPackage>();
        }
    }
}
