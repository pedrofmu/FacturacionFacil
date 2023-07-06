using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts.Ingresos.JsonModels
{
    public class Factura
    {
        public string Numero { get; set; }  //letra + numero ej: A1 A2 A63 T48
        public Cliente Cliente { get; set; }
        public Provedor Provedor { get; set; }
        public string Fecha { get; set; }
        public UnidadComprada[] UnidadesCompradas { get; set; }
        public float TotalBaseImponible { get; set; } //Total base imponible UnidadesTotalesSumadas
        public string RestadoPorIRPF { get; set; }
    }
}
