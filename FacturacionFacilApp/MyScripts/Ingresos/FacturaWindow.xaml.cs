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
        private Button new_client_btn;

        private ClientesController clients_controller;
        public FacturaWindow()
        {
            InitializeComponent();
            try
            {
                ComboBox clients_dropdown = FindName("clients_dropdown_") as ComboBox;
                clients_controller = new ClientesController(clients_dropdown);

                new_client_btn = FindName("new_client_btn_") as Button;
                new_client_btn.Click += clients_controller.SerlializeNewClient;
            }
            catch
            {
                throw new Exception("No se ha podido conseguir acceso a alguno de los botones");
            }
        }

        
    }
}
