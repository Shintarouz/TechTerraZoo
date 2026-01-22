using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTerra.Models
{
    class Verblijf
    {

        public string verblijfID { get; private set; }
        public string naam { get; private set; }
        // private int aantalDieren;
        public decimal temperatuur { get; private set; }
        public int capaciteit { get; private set; }
        public string typeOmgeving { get; private set; }

        private List<Dier> dierenInVerblijf;

        // Constructor
        public Verblijf(string verblijfID, string naam, decimal temperatuur, int capaciteit, string typeOmgeving)
        {
            this.verblijfID = verblijfID;
            this.naam = naam;
            // this.aantalDieren = aantalDieren;
            this.temperatuur = temperatuur;
            this.capaciteit = capaciteit;
            this.typeOmgeving = typeOmgeving;

            dierenInVerblijf = new List<Dier>();
        }

        // Voeg dier toe aan verblijf
        public void VoegDierToe(Dier dier)
        {
            dierenInVerblijf.Add(dier);
        }

        // Verwijder dier uit verblijf
        public void VerwijderDier(Dier dier)
        {
            dierenInVerblijf.Remove(dier);
        }

        // Toon aantal dieren in verblijf
        public void ToonDieren()
        {
            if (dierenInVerblijf.Count == 0)
            {
                Console.WriteLine("Er zijn geen dieren in dit verblijf.");
                return;
            }
            else
            {
                for (int i = 0; i < dierenInVerblijf.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {dierenInVerblijf[i].naam} ({dierenInVerblijf[i].soort})");
                }
            }
        }

        public bool BevatDier(Dier dier)
        {
            return dierenInVerblijf.Contains(dier);
        }
        public int AantalDieren()
        {
            return dierenInVerblijf.Count;
        }


        // Print informatie bij encapsulation
        public override string ToString()
        {
            if (dierenInVerblijf.Count == 0)
            {
                return $"VerblijfID: {verblijfID}, Naam: {naam}, AantalDieren: 0 (geen dieren)";
            }
            else
            {
                string dierenNamen = string.Join(", ", dierenInVerblijf.Select(d => d.naam));
                return $"VerblijfID: {verblijfID}, Naam: {naam}, AantalDieren: {dierenInVerblijf.Count} (Dieren: {dierenNamen})";
            }
        }
    }
}
