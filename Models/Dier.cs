using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCode.Models
{
    class Dier
    {
        public string dierID;
        public string naam;
        public string soort;
        public int leeftijd;

        public Verblijf inVerblijf;
        private Verzorger verzorger;

        private List<DierVoer> voerLijst;
        private List<GezondheidsNotitie> gezondheidsNotities;
        private List<VoedingsMoment> voedingsMomenten;

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

            //this.voerLijst = new List<string>();
            //this.gezondheidsNotities = new List<string>();
            //this.voedingsMomenten = new List<DateTime>();
        }

        public override string ToString()
        {
            return $"DierID: {dierID}, Naam: {naam}, Soort: {soort}, Leeftijd: {leeftijd}, Verblijf: {inVerblijf.naam}";
        }
    }
}
