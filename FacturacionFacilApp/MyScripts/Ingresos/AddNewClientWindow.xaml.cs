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
    public partial class AddNewClientWindow : Window
    {
        public AddNewClientWindow()
        {
            InitializeComponent();

            // Configura los controladores de eventos para los cambios en los cuadros de texto
            // Establece la fuente y la opacidad a los valores predeterminados
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

        // Controlador de eventos para el clic en el botón
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Cliente> clientes = ClientesController.clientes_list;

            // Crear un nuevo objeto Cliente con los valores ingresados en los cuadros de texto
            Cliente nuevoCliente = new Cliente
            {
                Nombre = nombre_txt_.Text,
                Identificador = nif_txt_.Text,
                Direccion = direccion_txt_.Text,
                Contacto = correo_txt_.Text
            };

            clientes.Add(nuevoCliente);

            ClientesJsonController.SerializeClients(clientes, ClientesController.json_path.ToString());

            this.Close();
        }
    }
}