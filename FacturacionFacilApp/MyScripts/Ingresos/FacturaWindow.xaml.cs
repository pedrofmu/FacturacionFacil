using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
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

namespace FacturacionFacilApp.MyScripts.Ingresos
{
    public partial class FacturaWindow : Window
    {
        private ClientesController clients_controller;

        private CrearFacturaController factura_controller;
        public FacturaWindow()
        {
            InitializeComponent();

            try
            {
                ComboBox clients_dropdown = FindName("clients_dropdown_") as ComboBox;
                clients_controller = new ClientesController(clients_dropdown);
                factura_controller = new CrearFacturaController();
            }
            catch
            {
                throw new Exception("No se ha podido conseguir acceso a alguno de los botones");
            }
        }

        private void OnNewClientIsClicked(object sender, RoutedEventArgs e)
        {
            AddNewClientWindow add_new_client_window = new AddNewClientWindow();
            add_new_client_window.Show();
        }

        private void guardad_factura_btn__Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = ClientesController.clientes_list.FirstOrDefault(cliente_busca => cliente_busca.Nombre == clients_dropdown_.Text);
            factura_controller.CrearFactura("F001", cliente, "01/07/2023", dinero_txt_.Text);
        }
    }
}
