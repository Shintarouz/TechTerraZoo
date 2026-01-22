using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTerra.Models
{
    class Verzorger
    {
        public string verzorgerID { get; private set; }
        public string naam { get; private set; }
        private List<Dier> toegewezenDieren;

        // Constructor
        public Verzorger(string naam, string verzorgerID)
        {
            this.naam = naam;
            this.verzorgerID = verzorgerID;
            toegewezenDieren = new List<Dier>();
        }

        // Voeg dier toe aan verzorger
        public void VoegDierToe(Dier dier)
        {
            toegewezenDieren.Add(dier);
        }

        // Verwijder dier van verzorger
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
