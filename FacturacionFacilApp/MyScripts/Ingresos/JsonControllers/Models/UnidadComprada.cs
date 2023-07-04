using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts.Ingresos.JsonControllers
{
    public class UnidadComprada
    {
        public int UnidadesDelProducto { get; set; }
        public string TipoDeUnidad { get; set; }
        public float PrecioPorUnidad { get; set; }
        public float IVA { get; set; }
        public float AñadidoPorIVA { get; set; }
        public float PrecioConIVA { get; set; }
    }
}
