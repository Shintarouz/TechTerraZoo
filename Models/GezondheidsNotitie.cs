using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTerra.Models
{
    class GezondheidsNotitie
    {
        public string NotitieID { get; }
        public string DierID { get; }
        public string Notitie { get; }
        public DateTime Datum { get; }

        public GezondheidsNotitie(string notitieID, string dierID, string notitie, DateTime datum)
        {
            NotitieID = notitieID;
            DierID = dierID;
            Notitie = notitie;
            Datum = datum;
        }

        public override string ToString()
        {
            // moet nog gefixt worden, zoek uit hoe je dit moet printen
            return base.ToString();
        }
    }
}
