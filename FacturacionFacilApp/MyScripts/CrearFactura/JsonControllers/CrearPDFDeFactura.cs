using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using System.IO;
using System.Windows.Forms;
using FacturacionFacilApp.MyScripts.LibroDeRegistro;
using System.Text;
using iText.Html2pdf;
using System.Drawing.Printing;
using System;
using System.Security.Cryptography;


namespace FacturacionFacilApp.MyScripts.Ingresos.JsonControllers
{
    public class CrearPDFDeFactura
    {
        public static bool CrearPDF(Factura _factura)
        {
            string css = "";

            using (StreamReader sr = new StreamReader(ControladorURI.Style.ToString()))
            {
                css = sr.ReadToEnd();
            }

            string html = $"<!DOCTYPE html>\r\n" +
                          $"<html>\r\n" +
                          $"\r\n<head>\r\n" +
                          $"    <meta charset=\"UTF-8\" />\r\n" +
                          $"    <title>Factura</title>\r\n" +
                          $"    <style>{css}</style>\r\n" +
                          $"</head>\r\n" +
                          $"\r\n<body>\r\n" +
                          $"    <div class=\"Header\">\r\n" +
                          $"        <div class=\"Remitente\">\r\n" +
                          $"            <h2>{_factura.Proveedor.Nombre}</h2>\r\n" +
                          $"            <h5>{_factura.Proveedor.Identificador}</h5>\r\n" +
                          $"            <h5>{_factura.Proveedor.Direccion}</h5>\r\n" +
                          $"            <h5>{_factura.Proveedor.Contacto}</h5>\r\n" +
                          $"        </div>\r\n" +
                          $"\r\n        <img src={ControladorURI.StyleImage.ToString()}>\r\n" +
                          $"    </div>\r\n" +
                          $"    <div class=\"Datos1\">\r\n" +
                          $"        <div class=\"Comprador\">\r\n" +
                          $"            <h2>{_factura.Cliente.Nombre}</h2>\r\n" +
                          $"            <h5>{_factura.Cliente.Identificador}</h5>\r\n" +
                          $"            <h5>{_factura.Cliente.Direccion}</h5>\r\n" +
                          $"            <h5>{_factura.Cliente.Contacto}</h5>\r\n" +
                          $"        </div>\r\n" +
                          $"        <div class=\"DatosFactura\">\r\n" +
                          $"            <h2>{_factura.Numero}</h2>\r\n" +
                          $"            <h5>{_factura.Fecha}</h5>\r\n" +
                          $"        </div>\r\n" +
                          $"    </div>\r\n" + 
                          CreateRows(_factura) +
                          $"    <div class=\"Datos2\">\r\n" +
                          $"        <div class=\"Total\">\r\n" +
                          $"            <h5>Total Base Impoible: {Math.Round(_factura.TotalBaseImponible, 2)}€</h5>\r\n" +
                          $"            <h5>IRPF: {_factura.IRPF}%</h5>\r\n" +
                          $"            <h5>Añadido por IVA: {Math.Round(_factura.AñadidoPorIVA, 2)}€</h5>\r\n" +
                          $"            <h5>Importe total: {Math.Round(_factura.TotalBaseImponible + _factura.AñadidoPorIVA - ObtenerDataAMostrar.ObtenerRetenidoPorIRPF(_factura), 2)}€</h5>\r\n" +
                          $"        </div>\r\n" +
                          $"        <div class=\"DatosDelPago\">\r\n" +
                          $"            <h5>{_factura.CondicionesFormaDePago}</h5>\r\n" +
                          $"        </div>\r\n" +
                          $"    </div>\r\n" +
                          $"</body>\r\n" +
                          $"\r\n</html>";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog.Title = "Guardar archivo PDF";
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.AddExtension = true;

            saveFileDialog.FileName = $"{_factura.Numero}.pdf";

            // Mostrar el diálogo y obtener la ruta de archivo seleccionada
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Obtener la ruta de archivo seleccionada por el usuario
                    string filePath = saveFileDialog.FileName;

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {

                        HtmlConverter.ConvertToPdf(html, fileStream);
                    }

                    MessageBox.Show("Archivo PDF guardado con éxito.");
                }
                catch
                {
                    MessageBox.Show("Error al cargar el PDF");
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }

        }

        private static string CreateRows(Factura factura)
        {
            StringBuilder rowsToAdd = new StringBuilder();

            foreach (UnidadComprada unidad in factura.UnidadesCompradas)
            {
                string row = $"<tr>\r\n" +
                             $"    <td>{unidad.UnidadesDelProducto}</td>\r\n" +
                             $"    <td>{unidad.TipoDeUnidad}</td>\r\n" +
                             $"    <td>{unidad.PrecioPorUnidad}€</td>\r\n" +
                             $"    <td>{unidad.IVA}%</td>\r\n" +
                             $"    <td>{Math.Round(unidad.PrecioConIVA, 2)}€</td>\r\n" +
                             $"</tr>\r\n";

                rowsToAdd.Append(row);
            }

            string returnRow = $"<table class=\"Productos\">\r\n" +
                               $"    <thead>\r\n" +
                               $"        <tr>\r\n" +
                               $"            <th>Cantidad</th>\r\n" +
                               $"            <th>Unidad</th>\r\n" +
                               $"            <th>Precio unidad</th>\r\n" +
                               $"            <th>IVA</th>\r\n" +
                               $"            <th>Total (con IVA)</th>\r\n" +
                               $"        </tr>\r\n" +
                               $"    </thead>\r\n" +
                               $"    <tbody>\r\n" +
                               $"{rowsToAdd.ToString()}" +
                               $"    </tbody>\r\n" +
                               $"</table>\r\n";

            return returnRow;
        }
    }
}

