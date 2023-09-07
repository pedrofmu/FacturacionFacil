using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts
{
    public static class ControladorURI
    {
        public static Uri ClientesJson { get; private set; } = new Uri("Json/Clientes.json", UriKind.Relative);
        public static Uri IngresosFacturaJson { get; private set; } = new Uri("Json/Ingresos.json", UriKind.Relative);
        public static Uri GastosFacturaJson { get; private set; } = new Uri("Json/Gastos.json", UriKind.Relative);
        public static Uri ProvedoresJson { get; private set; } = new Uri("Json/Proveedores.json", UriKind.Relative);
    }
}
