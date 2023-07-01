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
    /// <summary>
    /// Interaction logic for AddNewClientWindow.xaml
    /// </summary>
    public partial class AddNewClientWindow : Window
    {
        public AddNewClientWindow()
        {
            InitializeComponent();

            nombre_txt_.TextChanged += (e, o) => 
            { 
                nombre_txt_.FontStyle = FontStyles.Normal;
                nombre_txt_.Opacity = 1;
            };

            nif_txt_.TextChanged += (e, o) => 
            { 
                nif_txt_.FontStyle = FontStyles.Normal; 
                nif_txt_.Opacity = 1;
            };

            direccion_txt_.TextChanged += (e, o) => 
            { 
                direccion_txt_.FontStyle = FontStyles.Normal; 
                direccion_txt_.Opacity = 1;
            };

            correo_txt_.TextChanged += (e, o) => 
            { 
                correo_txt_.FontStyle = FontStyles.Normal; 
                correo_txt_.Opacity = 1;
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Cliente> clientes = ClientesController.clientes_list;

            clientes.Add(new Cliente
            {
                Nombre = nombre_txt_.Text,
                NIF = nif_txt_.Text,
                Direccion = direccion_txt_.Text,
                Correo = correo_txt_.Text
            });

            ClientesJsonController.SerializeClients(clientes, ClientesController.json_path.ToString());

            this.Close();
        }
    }
}
