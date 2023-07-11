using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.Kernel.Font;
using Microsoft.Win32;

namespace FacturacionFacilApp.MyScripts.Ingresos.JsonControllers
{
    public class CrearPDFDeFactura
    {
        public static bool CrearPDF(Factura _factura)
        {
            // Crea un cuadro de diálogo para seleccionar la ruta del archivo PDF
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog.Title = "Guardar factura como PDF";
            saveFileDialog.FileName = $"factura_{_factura.Numero}.pdf";

            // Muestra el diálogo y comprueba si el usuario seleccionó un archivo
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Crea un nuevo documento PDF
                PdfWriter writer = new PdfWriter(filePath);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Establece el estilo del documento
                PdfFont font = PdfFontFactory.CreateFont("Courier");
                Style estilo = new Style().SetFont(font).SetFontSize(12);
                document.SetFont(font);
                document.SetFontSize(12);

                // Agrega los datos de la factura al documento
                document.Add(new Paragraph("Factura").SetFontSize(24).SetTextAlignment(TextAlignment.CENTER).SetBold());
                document.Add(new Paragraph("Número de factura: " + _factura.Numero).SetMarginBottom(10));

                // Agrega la fecha
                document.Add(new Paragraph("Fecha: " + _factura.Fecha).SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(10));

                // Agrega los datos del proveedor en un párrafo
                Paragraph proveedor = new Paragraph().SetMarginBottom(20);
                proveedor.Add("Proveedor");
                proveedor.Add("\nNombre: " + _factura.Proveedor.Nombre);
                proveedor.Add("\nIdentificador: " + _factura.Proveedor.Identificador);
                proveedor.Add("\nDirección: " + _factura.Proveedor.Direccion);
                proveedor.Add("\nContacto: " + _factura.Proveedor.Contacto);
                document.Add(proveedor);

                // Agrega los datos del cliente en un párrafo
                Paragraph cliente = new Paragraph().SetMarginBottom(20);
                cliente.Add("Cliente");
                cliente.Add("\nNombre: " + _factura.Cliente.Nombre);
                cliente.Add("\nIdentificador: " + _factura.Cliente.Identificador);
                cliente.Add("\nDirección: " + _factura.Cliente.Direccion);
                cliente.Add("\nContacto: " + _factura.Cliente.Contacto);
                document.Add(cliente);

                // Agrega la tabla de unidades compradas
                Table table = new Table(5).UseAllAvailableWidth();
                table.AddHeaderCell("Cantidad").AddHeaderCell("Unidad").AddHeaderCell("Precio").AddHeaderCell("IVA").AddHeaderCell("Total (con IVA)");

                foreach (var unidad in _factura.UnidadesCompradas)
                {
                    table.AddCell(unidad.UnidadesDelProducto.ToString());
                    table.AddCell(unidad.TipoDeUnidad);
                    table.AddCell(unidad.PrecioPorUnidad.ToString() + "€");
                    table.AddCell(unidad.IVA.ToString() + "%");
                    table.AddCell(unidad.PrecioConIVA.ToString() + "€");
                }

                document.Add(table);

                // Agrega el total base imponible y el restado por IRPF
                document.Add(new Paragraph("Total Base Imponible: " + _factura.TotalBaseImponible + "€").SetMarginTop(10));
                document.Add(new Paragraph("IRPF: " + _factura.IRPF + "%"));
                document.Add(new Paragraph("Añadido Por IVA: " + _factura.AñadidoPorIVA + "€"));
                document.Add(new Paragraph("Importe total final: " + _factura.ImporteTotalFinal + "€"));

                // Agrega las condiciones y forma de pago
                document.Add(new Paragraph("\nCondiciones de pago: " + "\n" + _factura.CondicionesFormaDePago).SetMarginBottom(10));

                // Cierra el documento
                document.Close();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

