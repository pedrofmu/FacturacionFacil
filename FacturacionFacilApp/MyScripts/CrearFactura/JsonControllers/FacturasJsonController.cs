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
    public class FacturasJsonController
    {
        // Devuelve una lista de facturas del archivo Json
        public static List<Factura> GetFacturasFromJson(string _path)
        {
            string facturas;

            using (var reader = new StreamReader(_path.ToString()))
            {
                facturas = reader.ReadToEnd();
            }

            if (facturas != "")
            {
                return JsonConvert.DeserializeObject<List<Factura>>(facturas);
            }
            else
            {
                return new List<Factura>();
            }
        }

        // Serializa una lista de facturas y la guarda en un archivo Json
        public static void SerializeFacturas(List<Factura> _facturas_list, string _path)
        {
            string json = JsonConvert.SerializeObject(_facturas_list.ToArray(), Formatting.Indented);

            File.WriteAllText(_path, json);
        }
    }
}