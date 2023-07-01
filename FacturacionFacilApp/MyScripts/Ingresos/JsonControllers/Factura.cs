using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts.Ingresos.JsonModels
{
    public class Factura
    {
        public string Numero { get; set; }  
        public Cliente Cliente { get; set; }
        public string Fecha { get; set; }
        public string Dinero { get; set; }
    }
}
