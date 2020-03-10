using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConsulta.Entities
{
    public class ContratoDetalle
    {
        public string fechaTermina { get; set; }
        public string fechaInicio { get; set; }
        public double importe { get; set; }
        public double saldoActual { get; set; }
        public double saldoAtrazado { get; set; }
        public int diasAtrazo { get; set; }
        public double pagoXPlazo { get; set; }
        public int ultimoPlazoPag { get; set; }
        public int plazos { get; set; }
        public double capital { get; set; }
        public double interes { get; set; }
        public string contacto { get; set; }
        public string status { get; set; }
        public bool renovado { get; set; }
        public int integrantesCant { get; set; }
        public List<Integrante> integrantes { get; set; }
        
    }
}
