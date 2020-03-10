using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConsulta.Entities
{
    public class Integrante
    {
        public string cveCli { get; set; }
        public string nombreCom { get; set; }
        public string telefonoCel { get; set; }
        public double importeT { get; set; }
        public int diaAtr { get; set; }
        public double capital { get; set; }
        public long noCda { get; set; }
        public bool tesorero { get; set; }
        public bool presidente { get; set; }
    }
}
