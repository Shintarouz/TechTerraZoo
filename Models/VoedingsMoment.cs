using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateCode.Models
{
    class VoedingsMoment
    {
        public string MomentID { get; }
        public string DierID { get; }
        public DateTime Tijdstip { get; }

        public VoedingsMoment(string momentID, string dierID, DateTime tijdstip)
        {
            MomentID = momentID;
            DierID = dierID;
            Tijdstip = tijdstip;
        }

        public override string ToString()
        {
            // moet nog gedaan worden, zoek uit hoe je dit moet printen
            return base.ToString();
        }
    }
}
