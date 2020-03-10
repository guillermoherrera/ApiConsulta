using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConsulta.Entities
{
    public class CreditoDetalle
    {
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string rfc { get; set; }
        public int creditosActivos { get; set; }

        public List<Credito> creditos { get; set; }
    }
    public class Credito
    {
        public string sistema { get; set; }
        public Int64 noCda { get; set; }
        public decimal importeTotal { get; set; }
        public decimal saldoActual { get; set; }
        public decimal saldoAtrasado { get; set; }
        public int diasAtraso { get; set; }
    }
}
