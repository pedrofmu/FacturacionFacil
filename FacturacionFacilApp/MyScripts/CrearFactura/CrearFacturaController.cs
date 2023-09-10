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
    //Clase para crear la factura
    public class CrearFacturaController
    {
        //Obtine los valores para la factura y los crea
        public void CrearFactura(string _letra_factura, Proveedor _provedor, Cliente _cliente, string _fecha, List<UnidadComprada> _unidades_compradas, string _irpf, string _condiciones_forma_pago, string _actividad, string _ingreso_gasto)
        {
            List<Factura> factura_list = FacturasJsonController.GetFacturasFromJson(_ingreso_gasto == "Gastos" ? ControladorURI.GastosFacturaJson.ToString() : ControladorURI.IngresosFacturaJson.ToString());

            float dinero_total = ConseguirPrecioTotal(_unidades_compradas);
            string numero = HacerNumeroDeFactura(_letra_factura, factura_list);

            float añadido_por_iva = ObtenerAñadidoPorIVA(_unidades_compradas);

            float porcentaje_irpf = 0;
            if (!float.TryParse(_irpf, out porcentaje_irpf))
            {
                porcentaje_irpf = 1;
            }
            else
            {
                porcentaje_irpf = porcentaje_irpf / 100;
            }

            Factura factura = new Factura
            {
                Numero = numero,
                Proveedor = _provedor,
                Cliente = _cliente,
                Fecha = _fecha,
                UnidadesCompradas = _unidades_compradas.ToArray(),
                TotalBaseImponible = dinero_total,
                IRPF = _irpf,
                AñadidoPorIVA = añadido_por_iva,
                ImporteTotalFinal = dinero_total + añadido_por_iva - (dinero_total * porcentaje_irpf),
                CondicionesFormaDePago = _condiciones_forma_pago,
                Actividad = _actividad
            };

            if (CrearPDFDeFactura.CrearPDF(factura))
            {
                factura_list.Add(factura);

                FacturasJsonController.SerializeFacturas(factura_list, _ingreso_gasto == "Gastos" ? ControladorURI.GastosFacturaJson.ToString() : ControladorURI.IngresosFacturaJson.ToString());
            }
        }

        public void ModificarFactura(string _numero_factura, Proveedor _provedor, Cliente _cliente, string _fecha, List<UnidadComprada> _unidades_compradas, string _irpf, string _condiciones_forma_pago, string _actividad, string _ingreso_gasto)
        {
            List<Factura> factura_list = FacturasJsonController.GetFacturasFromJson(_ingreso_gasto == "Gastos" ? ControladorURI.GastosFacturaJson.ToString() : ControladorURI.IngresosFacturaJson.ToString());

            float dinero_total = ConseguirPrecioTotal(_unidades_compradas);
            string numero = _numero_factura;

            float añadido_por_iva = ObtenerAñadidoPorIVA(_unidades_compradas);

            float porcentaje_irpf = 0;
            if (!float.TryParse(_irpf, out porcentaje_irpf))
            {
                porcentaje_irpf = 1;
            }
            else
            {
                porcentaje_irpf = porcentaje_irpf / 100;
            }

            Factura facturaToModify = factura_list.FirstOrDefault(getFactura => getFactura.Numero == _numero_factura);

            facturaToModify.Numero = numero;
            facturaToModify.Proveedor = _provedor;
            facturaToModify.Cliente = _cliente;
            facturaToModify.Fecha = _fecha;
            facturaToModify.UnidadesCompradas = _unidades_compradas.ToArray();
            facturaToModify.TotalBaseImponible = dinero_total;
            facturaToModify.IRPF = _irpf;
            facturaToModify.AñadidoPorIVA = añadido_por_iva;
            facturaToModify.ImporteTotalFinal = dinero_total + añadido_por_iva - (dinero_total * porcentaje_irpf);
            facturaToModify.Actividad = _actividad;

            if (CrearPDFDeFactura.CrearPDF(facturaToModify))
            {
                FacturasJsonController.SerializeFacturas(factura_list, _ingreso_gasto == "Gastos" ? ControladorURI.GastosFacturaJson.ToString() : ControladorURI.IngresosFacturaJson.ToString());
            }
        }

        //Genera el numero de serie de la factura
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

        //Calcula el precio total de la factura
        float ConseguirPrecioTotal(List<UnidadComprada> _unidades_compradas)
        {
            float precio_total = 0;

            if (_unidades_compradas.Count == 0)
                return 0;

            foreach (UnidadComprada unidad in _unidades_compradas)
            {
                precio_total += unidad.PrecioPorUnidad * unidad.UnidadesDelProducto;
            }

            return precio_total;
        }

        float ObtenerAñadidoPorIVA(List<UnidadComprada> _unidades_compradas)
        {
            float añadido_por_iva = 0;

            foreach (UnidadComprada unidad in _unidades_compradas)
            {
                añadido_por_iva += unidad.AñadidoPorIVA;
            }

            return añadido_por_iva;
        }
    }
}
