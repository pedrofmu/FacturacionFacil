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
                     ClienteName = factura.Cliente.Contacto,
                     ProductosOfrecidos = ObtenerProductos(factura),
                     BaseImponible = ObtenerBaseImponible(factura),
                     IVA = ObtenerTodosLosIVAs(factura),
                     AñadidoPorIVA = ObtenerAñadidoPorIVA(factura),
                     RetenidoPorIRPF = ObtenerRetenidoPorIRPF(factura)
                };
            }

            return data_a_mostrar.ToArray();
        }

        private static float ObtenerRetenidoPorIRPF(Factura _factura)
        {
            float total_base_imponible = ObtenerBaseImponible(_factura);

            return 0;
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
                IVAs += $"\n{unidad_comprada.IVA}";
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
                productos += $"\n{unidad_comprada.TipoDeUnidad}";
            }

            return productos;
        }
    }

    public class LibroDeRegistroPlaceHolder
    {
        public string Numero { get; set; }
        public string Fecha { get; set; }
        public string ClienteName { get; set; }
        public string ProductosOfrecidos { get; set; }  // separados por /n
        public float BaseImponible { get; set; }   
        public string IVA { get; set; }
        public float AñadidoPorIVA { get; set; }
        public float Total { get; set; }
        public float RetenidoPorIRPF { get; set; }
    }

    /* 
   N factura
   Fecha
   Cliente
   Productos Ofrecidos
   Base Imponible (Suma de precio total de los productos)
   IVA
   Añadido por IVA
   Total (Con IVA)
   Retenido por IRPF
*/
}
