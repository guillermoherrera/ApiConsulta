using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConsulta.Entities
{
    public class Contrato
    {
        public string status { get; set; }
        public long contratoId { get; set; }
        public string nombreGeneral { get; set; }
        public string fechaTermina { get; set; }
        /*public double importeTotal { get; set; }
        public double saldoActual { get; set; }
        public double saldoAtrasado { get; set; }
        public int diasAtraso { get; set; }
        public double pagoPlazo { get; set; }*/
    }
}
