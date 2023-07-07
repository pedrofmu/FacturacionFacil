using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers.Models;
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
    public partial class AddNewProvedorWindow : Window
    {
        public AddNewProvedorWindow()
        {
            InitializeComponent();

            // Asigna los eventos TextChanged a los campos de texto para restaurar su estilo y opacidad
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Deserializa la lista de proveedores desde un archivo JSON
            List<Proveedor> provedores = ProveedoresJsonController.DeserializeProvedores(ProvedoresController.json_path);

            // Agrega un nuevo proveedor a la lista utilizando los valores de los campos de texto
            provedores.Add(new Proveedor
            {
                Nombre = nombre_txt_.Text,
                Identificador = nif_txt_.Text,
                Direccion = direccion_txt_.Text,
                Contacto = correo_txt_.Text
            });

            // Serializa la lista actualizada de proveedores y guarda los cambios en el archivo JSON
            ProveedoresJsonController.SerializeProvedores(provedores, ProvedoresController.json_path.ToString());

            // Cierra la ventana actual
            this.Close();
        }
    }
}
