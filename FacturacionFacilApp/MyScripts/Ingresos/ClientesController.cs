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
        public static List<Cliente> clientes_list { get; private set; }
        public static Uri json_path;

        public ClientesController(ComboBox _clients_dropdown)
        {
            clients_dropdown = _clients_dropdown;
            json_path = new Uri("Json/Clientes.json", UriKind.Relative);

            GetClientesFromFile();

            clients_dropdown.DropDownOpened += (o, e) => 
            { 
                RefreshDropdown(clients_dropdown); 
            };
        }

        //Consigue los datos del json y los devuelve a la variable de clientes_list
        public void GetClientesFromFile()
        {
            clientes_list = ClientesJsonController.DeserializeClients(json_path);

            RefreshDropdown(clients_dropdown);
        }
 
        //Refresca el dropdown
        private void RefreshDropdown(ComboBox _combox)
        {
            List<string> clientes_name = new List<string>();

            foreach (Cliente cliente in clientes_list)
            {
                clientes_name.Add(cliente.Nombre);
            }
            _combox.ItemsSource = clientes_name;
        }
    }
}
