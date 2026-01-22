using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTerra.Models
{
    class Dier
    {
        public string dierID { get; private set; }// CODE IS NIET GE-ENCAPSULEERD VOOR PRINT REDENEN!!
        public string naam { get; private set; }
        public string soort { get; private set; }
        public int leeftijd { get; private set; }

        public Verblijf inVerblijf { get; private set; }
        public Verzorger verzorger { get; private set; }

        private List<DierVoer> voerLijst;
        private List<GezondheidsNotitie> gezondheidsNotities;
        private List<VoedingsMoment> voedingsMomenten;

        // Constructor
        public Dier(string dierID, string naam, string soort, int leeftijd, Verblijf inVerblijf, Verzorger verzorger)
        {
            this.dierID = dierID;
            this.naam = naam;
            this.soort = soort;
            this.leeftijd = leeftijd;

            this.inVerblijf = inVerblijf;
            this.verzorger = verzorger;

            voerLijst = new List<DierVoer>();
            gezondheidsNotities = new List<GezondheidsNotitie>();
            voedingsMomenten = new List<VoedingsMoment>();
        }

        public override string ToString()
        {
            return $"DierID: {dierID}, Naam: {naam}, Soort: {soort}, Leeftijd: {leeftijd}";

            //Verblijf : {inVerblijf.ToString()}
            
        }
    }
}
