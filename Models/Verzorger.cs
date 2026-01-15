using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTerra.Models
{
    class Verzorger
    {
        public string naam;
        public string verzorgerID;
        private List<Dier> toegewezenDieren;

        public Verzorger(string naam, string verzorgerID)
        {
            this.naam = naam;
            this.verzorgerID = verzorgerID;
            toegewezenDieren = new List<Dier>();
        }

        public void WijzigDierToe(Dier dier)
        {
            toegewezenDieren.Add(dier);
        }

        public void VerwijderDier(Dier dier)
        {
            toegewezenDieren.Remove(dier);
        }

        public override string ToString()
        {
            string result = $"VerzorgerID: {verzorgerID}, Naam: {naam}, Verzorgde Dieren: {toegewezenDieren.Count}\n";
            result += "Dieren bij Verzorger:\n";

            if (toegewezenDieren.Count == 0)
            {
                result += "  (geen dieren)\n";
            }
            else
            {
                foreach (Dier dier in toegewezenDieren)
                {
                    result += "  - " + dier + "\n";
                }
            }

            return result;
        }
    }
}
