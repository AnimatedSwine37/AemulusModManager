using AemulusEx.Models.Enums;
using ReactiveUI;
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
    public class Loadout : ReactiveObject
    {
        private string _name;
        public string Name { get => _name; set => this.RaiseAndSetIfChanged(ref _name, value); }
        private string _description;
        public string Description { get => _description; set => this.RaiseAndSetIfChanged(ref _description, value); }
        private Game _game;
        public Game Game { get => _game; set => this.RaiseAndSetIfChanged(ref _game, value);  }
        public ObservableCollection<LoadoutPackage> Packages { get; set; }

        public Loadout(string name, string description, Game game, List<LoadoutPackage> packages)
        {
            _name = name;
            _description = description;
            _game = game;
            Packages = new ObservableCollection<LoadoutPackage>(packages);
            Packages.CollectionChanged += (_, _) => this.RaisePropertyChanged("Packages");
        }

        /// <summary>
        /// Creates a new Loadout with default values
        /// </summary>
        public Loadout()
        {
            _name = "Default";
            _description = "";
            _game = Game.Persona_4_Golden;
            Packages = new ObservableCollection<LoadoutPackage>();
            Packages.CollectionChanged += (_, _) => this.RaisePropertyChanged("Packages");
        }
    }
}
