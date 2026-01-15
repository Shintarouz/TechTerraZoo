using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTerra.Models
{
    class VoedingsMoment
    {
        public string MomentID { get; }
        public string DierID { get; }
        public DateTime TijdStip { get; }

        public VoedingsMoment(string momentID, string dierID, DateTime tijdStip)
        {
            MomentID = momentID;
            DierID = dierID;
            TijdStip = tijdStip;
        }

        public override string ToString()
        {
            // moet nog gedaan worden, zoek uit hoe je dit moet printen
            return base.ToString();
        }
    }
}
