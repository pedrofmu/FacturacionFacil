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
        public static Uri FacturasJson { get; private set; } = new Uri("Json/Facturas.json", UriKind.Relative);
        public static Uri ProvedoresJson { get; private set; } = new Uri("Json/Proveedores.json", UriKind.Relative);
    }
}
