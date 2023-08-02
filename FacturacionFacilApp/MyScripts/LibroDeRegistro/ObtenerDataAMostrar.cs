using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts.LibroDeRegistro
{
    public class ObtenerDataAMostrar
    {
        public static float ObtenerBaseImpoibleTotal(LibroDeRegistroPlaceHolder[] _data, string _IVA, out float _añadido_por_iva)
        {
            _añadido_por_iva = 0;
            float total_bImopnible = 0;

            List<string> facturasID = new List<string>();

            foreach (LibroDeRegistroPlaceHolder data in _data)
            {
                facturasID.Add(data.Numero);
            }

            List<UnidadComprada> unidades_con_iva = ObtenerUnidadesDelIVA(facturasID.ToArray(), _IVA);

            foreach (UnidadComprada unidad in unidades_con_iva)
            {
                total_bImopnible += unidad.UnidadesDelProducto * unidad.PrecioPorUnidad;
            }

            foreach (UnidadComprada unidad in unidades_con_iva)
            {
                _añadido_por_iva += unidad.AñadidoPorIVA;
            }

            return total_bImopnible;

        }

        static List<UnidadComprada> ObtenerUnidadesDelIVA(string[] _facturasID, string _IVA)
        {
            List<UnidadComprada> unidades = new List<UnidadComprada>();

            List<Factura> facturas = FacturasJsonController.GetFacturasFromJson(ControladorURI.FacturasJson.ToString());

            // Filtrar las facturas que están en _facturasID
            facturas = facturas.Where(factura => _facturasID.Contains(factura.Numero)).ToList();

            foreach (Factura factura in facturas)
            {
                foreach (UnidadComprada unidad in factura.UnidadesCompradas)
                {
                    unidades.Add(unidad);
                }
            }


            if (_IVA == "-Ninguno-")
                return unidades;

            // Filtrar las unidades por IVA
            unidades = unidades.Where(unidad => unidad.IVA == float.Parse(_IVA)).ToList();

            return unidades;
        }
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
                     BaseImponible = $"{ObtenerBaseImponible(factura)}",
                     IVA = $"{ObtenerTodosLosIVAs(factura)}",
                     AñadidoPorIVA = $"{ObtenerAñadidoPorIVA(factura)}",
                     RetenidoPorIRPF = $"{ObtenerRetenidoPorIRPF(factura)}€",
                     Total = $"{(factura.TotalBaseImponible + factura.AñadidoPorIVA - ObtenerRetenidoPorIRPF(factura))}€",
                     Actividad = factura.Actividad
                };

                data_a_mostrar.Add(placeholder);
            }

            return data_a_mostrar.ToArray();
        }

        public static LibroDeRegistroPlaceHolder[] ObtenerDataOrdenada(string _nombre_cliente, string _actividad, string _IVA)    
        {
            LibroDeRegistroPlaceHolder[] return_data = ObtenerData();

            if (_nombre_cliente != "-Ninguno-")
                return_data = ObtenerDataOrdenadoPorCliente(_nombre_cliente, return_data);

            if (_actividad != "-Ninguno-")
                return_data = ObtenerDataOrdenadaPorActividad(_actividad, return_data);

            if (_IVA != "-Ninguno-")
                return_data = ObtenerDataOrdenadaPorIVA(_IVA, return_data);

            return return_data;


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

        private static LibroDeRegistroPlaceHolder[] ObtenerDataOrdenadoPorCliente(string _nombre_cliente, LibroDeRegistroPlaceHolder[] _data_to_sort)
        {
            LibroDeRegistroPlaceHolder[] temp_place_holders = _data_to_sort.ToArray();

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

        public static LibroDeRegistroPlaceHolder[] ObtenerDataOrdenadaPorActividad(string _actividad)
        {
            LibroDeRegistroPlaceHolder[] temp_place_holders = ObtenerData();

            List<LibroDeRegistroPlaceHolder> return_data = new List<LibroDeRegistroPlaceHolder>();

            foreach (LibroDeRegistroPlaceHolder libro in temp_place_holders)
            {
                if (libro.Actividad == _actividad)
                {
                    return_data.Add(libro);
                }
            }

            return return_data.ToArray();
        }

        private static LibroDeRegistroPlaceHolder[] ObtenerDataOrdenadaPorActividad(string _actividad, LibroDeRegistroPlaceHolder[] _data_to_sort)
        {
            LibroDeRegistroPlaceHolder[] temp_place_holders = _data_to_sort;

            List<LibroDeRegistroPlaceHolder> return_data = new List<LibroDeRegistroPlaceHolder>();

            foreach (LibroDeRegistroPlaceHolder libro in temp_place_holders)
            {
                if (libro.Actividad == _actividad)
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

        private  static LibroDeRegistroPlaceHolder[] ObtenerDataOrdenadaPorIVA(string _IVA, LibroDeRegistroPlaceHolder[] _data_to_sort)
        {
            LibroDeRegistroPlaceHolder[] temp_place_holders = _data_to_sort;

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
            float total_base_imponible = _factura.TotalBaseImponible;

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

        private static string ObtenerAñadidoPorIVA(Factura _factura)
        {
            string añadido_por_iva = "";

            foreach (UnidadComprada unidad_comprada in _factura.UnidadesCompradas)
            {
                añadido_por_iva += $"{unidad_comprada.AñadidoPorIVA}€\n";
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

        private static string ObtenerBaseImponible(Factura _factura)
        {
            List<float> bases_imponibles = new List<float>();
            foreach (UnidadComprada unidad_comprada in _factura.UnidadesCompradas)
            {
                bases_imponibles.Add(unidad_comprada.UnidadesDelProducto * unidad_comprada.PrecioPorUnidad);
            }

            string return_bImponibles = "";

            foreach(float base_imp in bases_imponibles)
            {
                return_bImponibles += base_imp + "€\n";
            }

            return return_bImponibles;
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
