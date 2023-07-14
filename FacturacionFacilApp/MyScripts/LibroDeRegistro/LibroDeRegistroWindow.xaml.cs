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
                mostrar_data_.ItemsSource = ObtenerDataAMostrar.ObtenerDataOrdenadaPorIVA(ordenar_iva_combox_.SelectedItem.ToString()); 
            };

            ActualizarComboBoxConClientes();
            ordenar_clientes_combox_.SelectionChanged += (object o, SelectionChangedEventArgs e) =>
            {
                mostrar_data_.ItemsSource = ObtenerDataAMostrar.ObtenerDataOrdenadoPorCliente(ordenar_clientes_combox_.SelectedItem.ToString());
            };

            mostrar_data_.ItemsSource = ObtenerDataAMostrar.ObtenerData();
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
        }
    }
}
