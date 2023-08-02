using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using FacturacionFacilApp.MyScripts.LibroDeRegistro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FacturacionFacilApp.MyScripts.MostarCuentas
{
    public partial class LibroDeRegistroWindow : Window
    {
        public LibroDeRegistroWindow()
        {
            InitializeComponent();

            ActualizarComboBoxConIVAs();
            ordenar_iva_combox_.SelectionChanged += (object o, SelectionChangedEventArgs e) => 
            {
                LibroDeRegistroPlaceHolder[] data = ObtenerDataAMostrar.ObtenerDataOrdenada(ordenar_clientes_combox_.SelectedItem.ToString(), ordenar_actividad_combox_.SelectedItem.ToString(), ordenar_iva_combox_.SelectedItem.ToString());
                mostrar_data_.ItemsSource = data;
                float añadido_por_iva_ = 0;
                total_bImponible_txt.Text = "Total base imponible (solo los producto del iva seleccionado): " + ObtenerDataAMostrar.ObtenerBaseImpoibleTotal(data, ordenar_iva_combox_.SelectedItem.ToString(), out añadido_por_iva_) + "€";
                total_ivas_txt.Text = "Total añadido por IVA(excluyendo los no seleccionados): " + añadido_por_iva_ + "€";
            };

            ActualizarComboBoxConClientes();
            ordenar_clientes_combox_.SelectionChanged += (object o, SelectionChangedEventArgs e) =>
            {
                LibroDeRegistroPlaceHolder[] data = ObtenerDataAMostrar.ObtenerDataOrdenada(ordenar_clientes_combox_.SelectedItem.ToString(), ordenar_actividad_combox_.SelectedItem.ToString(), ordenar_iva_combox_.SelectedItem.ToString());
                mostrar_data_.ItemsSource = data;
                float añadido_por_iva_ = 0;
                total_bImponible_txt.Text = "Total base imponible (solo los producto del iva seleccionado): " + ObtenerDataAMostrar.ObtenerBaseImpoibleTotal(data, ordenar_iva_combox_.SelectedItem.ToString(), out añadido_por_iva_) + "€";
                total_ivas_txt.Text = "Total añadido por IVA(excluyendo los no seleccionados): " + añadido_por_iva_ + "€";
            };

            ActualizarComboxConActividades();
            ordenar_actividad_combox_.SelectionChanged += (object o, SelectionChangedEventArgs e) =>
            {
                LibroDeRegistroPlaceHolder[] data = ObtenerDataAMostrar.ObtenerDataOrdenada(ordenar_clientes_combox_.SelectedItem.ToString(), ordenar_actividad_combox_.SelectedItem.ToString(), ordenar_iva_combox_.SelectedItem.ToString());
                mostrar_data_.ItemsSource = data;
                float añadido_por_iva_ = 0;
                total_bImponible_txt.Text = "Total base imponible (solo los producto del iva seleccionado): " + ObtenerDataAMostrar.ObtenerBaseImpoibleTotal(data, ordenar_iva_combox_.SelectedItem.ToString(), out añadido_por_iva_) + "€";
                total_ivas_txt.Text = "Total añadido por IVA(excluyendo los no seleccionados): " + añadido_por_iva_ + "€";
            };

            mostrar_data_.ItemsSource = ObtenerDataAMostrar.ObtenerData();
            float añadido_por_iva = 0;
            total_bImponible_txt.Text = "Total base imponible (solo los producto del iva seleccionado): " + ObtenerDataAMostrar.ObtenerBaseImpoibleTotal(ObtenerDataAMostrar.ObtenerData(), ordenar_iva_combox_.SelectedItem.ToString(), out añadido_por_iva) + "€";
            total_ivas_txt.Text = "Total añadido por IVA(excluyendo los no seleccionados): " + añadido_por_iva + "€";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main_windoww = new MainWindow(); 
            main_windoww.Show();

            this.Close();
        }

        void ActualizarComboBoxConIVAs()
        {
            List<Factura> facturas = FacturasJsonController.GetFacturasFromJson(ControladorURI.FacturasJson.ToString());

            List<float> final_ivas = new List<float>(); 
            foreach (Factura factura in facturas)
            {
                foreach (UnidadComprada unidad_comprada in factura.UnidadesCompradas)
                {
                    if (!final_ivas.Contains(unidad_comprada.IVA))
                    {
                        final_ivas.Add(unidad_comprada.IVA);
                    }
                }
            }

            foreach(float f in final_ivas)
            {
                ordenar_iva_combox_.Items.Add($"{f}");
            }

            ordenar_iva_combox_.Items.Add("-Ninguno-");
            ordenar_iva_combox_.SelectedItem = ("-Ninguno-");
        }

        void ActualizarComboBoxConClientes()
        {
            List<Cliente> clientes = ClientesJsonController.DeserializeClients(ControladorURI.ClientesJson);

            List<string> final_clientes = new List<string>();
            foreach (Cliente cliente in clientes)
            {
                if (!final_clientes.Contains(cliente.Nombre))
                {
                    final_clientes.Add(cliente.Nombre);
                }
            }

            foreach (string s in final_clientes)
            {
                ordenar_clientes_combox_.Items.Add($"{s}");
            }

            ordenar_clientes_combox_.Items.Add("-Ninguno-");
            ordenar_clientes_combox_.SelectedItem = ("-Ninguno-");
        }

        void ActualizarComboxConActividades()
        {
            List<Factura> facturas = FacturasJsonController.GetFacturasFromJson(ControladorURI.FacturasJson.ToString());

            List<string> actividades = new List<string>();
            foreach (Factura factura in facturas)
            {
                if (!actividades.Contains(factura.Actividad))
                {
                    actividades.Add(factura.Actividad);
                }
            }

            foreach (string s in actividades)
            {
                ordenar_actividad_combox_.Items.Add($"{s}");
            }

            ordenar_actividad_combox_.Items.Add("-Ninguno-");
            ordenar_actividad_combox_.SelectedItem = ("-Ninguno-");
        }
    }
}
