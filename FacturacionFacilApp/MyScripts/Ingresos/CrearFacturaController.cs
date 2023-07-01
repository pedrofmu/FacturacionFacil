using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts.Ingresos.JsonControllers
{
    internal class CrearFacturaController
    {
        public static Uri json_path { get; private set; }

        public CrearFacturaController()
        {
            json_path = new Uri("Json/Facturas.json", UriKind.Relative);
        }
        public void CrearFactura(string _numero, Cliente _cliente, string _fecha, string _dinero)
        {
            List<Factura> facturaList = FacturasJsonController.GetFacturasFromJson(json_path.ToString());

            facturaList.Add( new Factura
            {
                Numero = _numero,
                Cliente = _cliente,
                Fecha = _fecha,
                Dinero = _dinero
            });

            FacturasJsonController.SerializeFacturas(facturaList, json_path.ToString());
        }
    }
}
