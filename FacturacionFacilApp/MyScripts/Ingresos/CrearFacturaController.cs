using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace FacturacionFacilApp.MyScripts.Ingresos.JsonControllers
{
    internal class CrearFacturaController
    {
        public static Uri json_path { get; private set; }

        public CrearFacturaController()
        {
            json_path = new Uri("Json/Facturas.json", UriKind.Relative);
        }
        public void CrearFactura(string _letra_factura, Cliente _cliente, string _fecha, List<UnidadComprada> _unidades_compradas)
        {
            List<Factura> factura_list = FacturasJsonController.GetFacturasFromJson(json_path.ToString());

            float dinero_total = ConseguirPrecioTotal(_unidades_compradas);
            string numero = HacerNumeroDeFactura(_letra_factura, factura_list);

            factura_list.Add( new Factura
            {
                Numero = numero,
                Cliente = _cliente,
                Fecha = _fecha,
                UnidadesCompradas = _unidades_compradas.ToArray(),
                Dinero = dinero_total
            });

            FacturasJsonController.SerializeFacturas(factura_list, json_path.ToString());
        }

        string HacerNumeroDeFactura(string _letra_factura, List<Factura> _facturas)
        {
            List<int> numeros = new List<int>();

            foreach (Factura factura in _facturas)
            {
                if (factura.Numero.StartsWith(_letra_factura))
                    numeros.Add(int.Parse(factura.Numero.Remove(0, 1)));
            }

            if (numeros.Count == 0)
                return _letra_factura + 1;

            numeros.Sort();

            return _letra_factura + (numeros.Last() + 1);
        }

        float ConseguirPrecioTotal(List<UnidadComprada> _unidades_compradas)
        {
            float precio_total = 0;

            if (_unidades_compradas.Count == 0)
                return 0;

            foreach (UnidadComprada unidad in _unidades_compradas)
            {
                precio_total += unidad.PrecioConIVA;
            }

            return precio_total;
        }
    }
}
