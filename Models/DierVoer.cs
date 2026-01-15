using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTerra.Models
{
    class DierVoer
    {
        public string VoerID { get; }
        public string DierID { get; }
        public string VoerNaam { get; }

        public DierVoer(string voerID, string dierID, string voerNaam)
        {
            VoerID = voerID;
            DierID = dierID;
            VoerNaam = voerNaam;
        }

        public override string ToString()
        {
            return $"{VoerNaam}";
        }
    }
}
