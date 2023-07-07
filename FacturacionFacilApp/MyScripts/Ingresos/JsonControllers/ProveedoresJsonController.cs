using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers.Models;
using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts.Ingresos.JsonControllers
{
    public class ProveedoresJsonController
    {
        //Convierte la lista de provedores a el archivo json
        public static void SerializeProvedores(List<Proveedor> _provedor_list, string _path)
        {
            string json = JsonConvert.SerializeObject(_provedor_list.ToArray(), Formatting.Indented);

            File.WriteAllText(_path, json);
        }

        //Convierte el json en un string lejible
        public static List<Proveedor> DeserializeProvedores(Uri _path)
        {
            string provedores;

            using (var reader = new StreamReader(_path.ToString()))
            {
                provedores = reader.ReadToEnd();
            }

            if (provedores != "")
            {
                return JsonConvert.DeserializeObject<List<Proveedor>>(provedores);
            }
            else
            {
                return new List<Proveedor>();
            }
        }
    }
}
