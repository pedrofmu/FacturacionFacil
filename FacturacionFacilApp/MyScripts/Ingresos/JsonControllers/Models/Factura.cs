using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
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
        public string Fecha { get; set; }
        public UnidadComprada[] UnidadesCompradas { get; set; }
        public float Dinero { get; set; } //Total base imponible UnidadesTotalesSumadas
        //IRPF null , 7 , 15
    }
}
