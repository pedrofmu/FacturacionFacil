using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FacturacionFacilApp.MyScripts
{
   //esta clase guarda las direcciones a los diferentes archivos y se asegura de que esten correctamente
    public static class ControladorURI
    {
        public static string BasePath { get; private set; } = AppDomain.CurrentDomain.BaseDirectory;
        public static Uri ClientesJson { get; private set; } = new Uri("Json/Clientes.json", UriKind.Relative);
        public static Uri IngresosFacturaJson { get; private set; } = new Uri("Json/Ingresos.json", UriKind.Relative);
        public static Uri GastosFacturaJson { get; private set; } = new Uri("Json/Gastos.json", UriKind.Relative);
        public static Uri ProvedoresJson { get; private set; } = new Uri("Json/Proveedores.json", UriKind.Relative);
        public static Uri Style { get; private set; } = new Uri("FacturaStyle/style.css", UriKind.Relative);
        public static Uri StyleImage { get; private set; } = new Uri("FacturaStyle/logo.png", UriKind.Relative);

        public static void CheckURI()
        {
            List<Uri> uris = new List<Uri> { ClientesJson, IngresosFacturaJson, GastosFacturaJson, ProvedoresJson, Style, StyleImage };



            if (!Directory.Exists(BasePath + "\\Json"))
                Directory.CreateDirectory(BasePath + "\\Json");

            if (!Directory.Exists(BasePath + "\\FacturaStyle"))
                Directory.CreateDirectory(BasePath + "\\FacturaStyle");

            foreach (Uri uri in uris)
            {
                if (!File.Exists(BasePath + uri.ToString()))
                {
                    FileStream stream = File.Create(BasePath + uri.ToString());

                    if (uri == Style)
                    {
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(default_css);

                        stream.Write(buffer, 0, buffer.Length);
                    }

                    stream.Dispose();
                }
            }
        }

        private static string default_css = ".body {\r\n    font-family: Arial, Helvetica, sans-serif;\r\n}\r\n\r\n.Header {\r\n    line-height: 5%;\r\n    overflow: hidden;\r\n}\r\n\r\n.Header img {\r\n    float: right;\r\n    width: 300px;\r\n    height: 100px\r\n}\r\n\r\n.Remitente {\r\n    float: left;\r\n}\r\n\r\n.Datos1 {\r\n    overflow: hidden;\r\n    line-height: 5%;\r\n    padding-top: 15px;\r\n}\r\n\r\n.Comprador {\r\n    float: left;\r\n}\r\n\r\n.DatosFactura {\r\n    float: right;\r\n    padding-right: 240px;\r\n}\r\n\r\n.Productos {\r\n    width: 100%;\r\n    border-collapse: collapse;\r\n    margin-top: 10px;\r\n}\r\n\r\n.Productos th,\r\n.Productos td {\r\n    border: 1px solid #000;\r\n    padding: 5px;\r\n    text-align: center;\r\n}\r\n\r\n.Total {\r\n    line-height: 5%;\r\n}\r\n\r\n.Datos2 {\r\n    padding-top: 20px;\r\n}\r\n\r\n.DatosDelPago {\r\n    padding-top: 15px;\r\n}\r\n";
    }
}
