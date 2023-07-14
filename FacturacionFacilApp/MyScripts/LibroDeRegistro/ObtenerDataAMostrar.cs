using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts.LibroDeRegistro
{
    public class ObtenerDataAMostrar
    {
        public static LibroDeRegistroPlaceHolder[] ObtenerData()
        {
            List<Factura> facturas = FacturasJsonController.GetFacturasFromJson(ControladorURI.FacturasJson.ToString());
            List<LibroDeRegistroPlaceHolder> data_a_mostrar = new List<LibroDeRegistroPlaceHolder>();

            foreach (Factura factura in facturas)
            {
                LibroDeRegistroPlaceHolder placeholder =  new LibroDeRegistroPlaceHolder {
                     Numero = factura.Numero,
                     Fecha = factura.Fecha,
                     NombreCliente = factura.Cliente.Nombre,
                     ProductosOfrecidos = ObtenerProductos(factura),
                     BaseImponible = $"{ObtenerBaseImponible(factura)}€",
                     IVA = $"{ObtenerTodosLosIVAs(factura)}",
                     AñadidoPorIVA = $"{ObtenerAñadidoPorIVA(factura)}€",
                     RetenidoPorIRPF = $"{ObtenerRetenidoPorIRPF(factura)}€",
                     Total = $"{(ObtenerBaseImponible(factura) + ObtenerAñadidoPorIVA(factura) - ObtenerRetenidoPorIRPF(factura))}€",
                     Actividad = factura.Actividad
                };

                data_a_mostrar.Add(placeholder);
            }

            return data_a_mostrar.ToArray();
        }

        public static LibroDeRegistroPlaceHolder[] ObtenerDataOrdenadoPorCliente(string _nombre_cliente)
        {
            LibroDeRegistroPlaceHolder[] temp_place_holders = ObtenerData();

            List<LibroDeRegistroPlaceHolder> return_data = new List<LibroDeRegistroPlaceHolder>();

            foreach (LibroDeRegistroPlaceHolder libro in temp_place_holders)
            {
                if (libro.NombreCliente == _nombre_cliente)
                {
                    return_data.Add(libro);
                }
            }

            return return_data.ToArray();
        }
        public static LibroDeRegistroPlaceHolder[] ObtenerDataOrdenadaPorIVA(string _IVA)
        {
            LibroDeRegistroPlaceHolder[] temp_place_holders = ObtenerData();

            if (_IVA == string.Empty) return temp_place_holders;

            List<LibroDeRegistroPlaceHolder> return_data = new List<LibroDeRegistroPlaceHolder>();

            foreach (LibroDeRegistroPlaceHolder data in temp_place_holders)
            {
                if (data.IVA.Contains(_IVA))
                {
                    return_data.Add(data);
                }
            }

            if (return_data.Count == 0) return temp_place_holders;

            return return_data.ToArray();
        }

        private static float ObtenerRetenidoPorIRPF(Factura _factura)
        {
            float total_base_imponible = ObtenerBaseImponible(_factura);

            float irpf = 0;
            float retenido_irpf = 0;
            if (float.TryParse(_factura.IRPF, out irpf))
            {
                retenido_irpf = (total_base_imponible * (irpf / 100));
            }
            else
            {
                retenido_irpf = 0;
            }

            return retenido_irpf;
        }

        private static float ObtenerAñadidoPorIVA(Factura _factura)
        {
            float añadido_por_iva = 0;

            foreach (UnidadComprada unidad_comprada in _factura.UnidadesCompradas)
            {
                añadido_por_iva += unidad_comprada.AñadidoPorIVA;
            }

            return añadido_por_iva;
        }

        private static string ObtenerTodosLosIVAs(Factura _factura)
        {
            string IVAs = "";
            foreach (UnidadComprada unidad_comprada in _factura.UnidadesCompradas)
            {
                IVAs += $"{unidad_comprada.IVA}%\n";
            }

            return IVAs;
        }

        private static float ObtenerBaseImponible(Factura _factura)
        {
            float base_imponible = 0;   

            foreach (UnidadComprada unidad_comprada in _factura.UnidadesCompradas)
            {
                base_imponible += unidad_comprada.UnidadesDelProducto * unidad_comprada.PrecioPorUnidad;
            }

            return base_imponible;
        }

        private static string ObtenerProductos(Factura _factura)
        {
            string productos = "";
            foreach (UnidadComprada unidad_comprada in _factura.UnidadesCompradas)
            {
                productos += $"{unidad_comprada.TipoDeUnidad}\n";
            }

            return productos;
        }
    }

    public class LibroDeRegistroPlaceHolder
    {
        public string Numero { get; set; }
        public string Fecha { get; set; }
        public string NombreCliente { get; set; }
        public string ProductosOfrecidos { get; set; }  // separados por /n
        public string BaseImponible { get; set; }   
        public string IVA { get; set; }
        public string AñadidoPorIVA { get; set; }
        public string RetenidoPorIRPF { get; set; }
        public string Total { get; set; }
        public string Actividad { get; set; }
    }
}
