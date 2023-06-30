using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FacturacionFacilApp.MyScripts.Ingresos
{
    public class ClientesController
    {
        public ComboBox clients_dropdown;
        public List<Cliente> clientes_list { get; private set; }
        public Uri json_path;

        public ClientesController(ComboBox _clients_dropdown)
        {
            clients_dropdown = _clients_dropdown;
            json_path = new Uri("Json/Clientes.json", UriKind.Relative);

            GetClientesFromFile();
        }

        //Funcion que se encarga que cuando se presione el boton de nuevo cliente y copie el dato al json
        public void SerlializeNewClient(object sender, RoutedEventArgs e)
        {
            List<Cliente> clientes = new List<Cliente>() {
                new Cliente
                {
                    Nombre = "Rosana",
                    NIF = "21",
                    Direccion = "C/Entenza, 40 03803 Alcoy - Alicante",
                    Correo = "info@kyreo.es"
                },
                new Cliente
                {
                    Nombre = "Pedro",
                    NIF = "45",
                    Direccion = "C/Cronista, 123 03802 Alcoy - Alicante",
                    Correo = "pedrofm3000@gmail.com"
                }
            };

            IngresosJsonController.SerializeClients(clientes, json_path.ToString());

            clientes_list = clientes;

            RefreshDropdown();
        }

        public void GetClientesFromFile()
        {
            string clients;

            using (var reader = new StreamReader(json_path.ToString()))
            {
                clients = reader.ReadToEnd();
            }

            clientes_list = JsonConvert.DeserializeObject<List<Cliente>>(clients);

            RefreshDropdown();
        }

        //Refresca el dropdown
        private void RefreshDropdown()
        {
            List<string> clientes_name = new List<string>();

            foreach (Cliente cliente in clientes_list)
            {
                clientes_name.Add(cliente.Nombre);
            }
            clients_dropdown.ItemsSource = clientes_name;
        }
    }
}
