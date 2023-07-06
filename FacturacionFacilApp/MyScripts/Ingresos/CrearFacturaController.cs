using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers.Models;
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
        public void CrearFactura(string _letra_factura, Provedor _provedor, Cliente _cliente, string _fecha, List<UnidadComprada> _unidades_compradas, string _irpf)
        {
            float irpf = 0f;
            if (!float.TryParse(_irpf, out irpf))
            {
                irpf = 0;
            }

            List<Factura> factura_list = FacturasJsonController.GetFacturasFromJson(json_path.ToString());

            float dinero_total = ConseguirPrecioTotal(_unidades_compradas, irpf);
            string numero = HacerNumeroDeFactura(_letra_factura, factura_list); 

            factura_list.Add( new Factura
            {
                Numero = numero,
                Provedor = _provedor,
                Cliente = _cliente,
                Fecha = _fecha,
                UnidadesCompradas = _unidades_compradas.ToArray(),
                TotalBaseImponible = dinero_total,
                RestadoPorIRPF = _irpf
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

        float ConseguirPrecioTotal(List<UnidadComprada> _unidades_compradas, float IRPF)
        {
            float precio_total = 0;

            if (_unidades_compradas.Count == 0)
                return 0;

            foreach (UnidadComprada unidad in _unidades_compradas)
            {
                precio_total += unidad.PrecioConIVA;
            }

            return precio_total - (precio_total * IRPF / 100);
        }
    }
}
