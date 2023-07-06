using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers.Models;
using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FacturacionFacilApp.MyScripts.Ingresos
{
    public class ProvedoresController
    {
        public ComboBox provedores_dropdown;
        public static List<Provedor> provedores_list { get; private set; }
        public static Uri json_path;

        public ProvedoresController(ComboBox _clients_dropdown)
        {
            provedores_dropdown = _clients_dropdown;
            json_path = new Uri("Json/Provedores.json", UriKind.Relative);

            GetProvedoresFromFile();

            provedores_dropdown.DropDownOpened += (o, e) =>
            {
                GetProvedoresFromFile();
            };
        }

        //Consigue los datos del json y los devuelve a la variable de clientes_list
        public void GetProvedoresFromFile()
        {
            provedores_list = ProvedoresJsonController.DeserializeProvedores(json_path);

            RefreshDropdown(provedores_dropdown);
        }

        //Refresca el dropdown
        private void RefreshDropdown(ComboBox _combox)
        {
            List<string> provedores_list = new List<string>();

            foreach (Provedor provedores in ProvedoresController.provedores_list)
            {
                provedores_list.Add(provedores.Nombre);
            }
            _combox.ItemsSource = provedores_list;
        }
    }
}
