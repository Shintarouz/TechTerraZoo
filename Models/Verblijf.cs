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
        
        public string verblijfID;
        public string naam;
        // private int aantalDieren;
        private decimal temperatuur;
        private int capaciteit;
        private string typeOmgeving;

        private List<Dier> dierenInVerblijf;
        

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

        public void VoegDierToe(Dier dier)
        {
            dierenInVerblijf.Add(dier);
        }

        public void VerwijderDier(Dier dier)
        {
            dierenInVerblijf.Remove(dier);
        }

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

        public bool VerwijderDierOpIndex(int index)
        {
            if (index < 0 || index >= dierenInVerblijf.Count)
                return false;

            dierenInVerblijf.RemoveAt(index);
            return true;
        }

        public int AantalDieren()
        {
            return dierenInVerblijf.Count;
        }

        public override string ToString()
        {
            string result = $"VerblijfID: {verblijfID}, Naam: {naam}, AantalDieren: {dierenInVerblijf.Count}\n";
            result += "Dieren in verblijf:\n";

            if (dierenInVerblijf.Count == 0)
            {
                result += "  (geen dieren)\n";
            }
            else
            {
                foreach (Dier dier in dierenInVerblijf)
                {
                    result += "  - " + dier + "\n";
                }
            }
            return result;
        }
    }
}
