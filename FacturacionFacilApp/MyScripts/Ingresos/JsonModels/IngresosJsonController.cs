using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts.Ingresos.JsonModels
{
    //Esta clase se encarga de controlar el json
    public class IngresosJsonController
    {
        //Convierte la lista de clientes a el archivo json
        public static void SerializeClients(List<Cliente> _clienteList, string _path)
        {
            string json = JsonConvert.SerializeObject(_clienteList.ToArray(), Formatting.Indented);

            File.WriteAllText(_path, json);
        }
    }
}
