using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConsulta.Entities
{
    public class IntegranteDetalle
    {
        public string fechaTermina { get; set; }
        public string fechaUltimoPago { get; set; }
        public long noCda { get; set; }
        public double importeT { get; set; }
        public double saldoActual { get; set; }
        public double salAtr { get; set; }
        public int diaAtr { get; set; }
        public int pagos { get; set; }
        public long folio { get; set; }
        public double capital { get; set; }
        public double interes { get; set; }
    }
}
