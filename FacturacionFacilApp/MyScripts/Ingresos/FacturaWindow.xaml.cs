using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
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
    public partial class FacturaWindow : Window
    {
        private ClientesController clients_controller;

        private CrearFacturaController factura_controller;
        public FacturaWindow()
        {
            InitializeComponent();
            RefreshComboBox();

            try
            {
                ComboBox clients_dropdown = FindName("clients_dropdown_") as ComboBox;
                clients_controller = new ClientesController(clients_dropdown);
                factura_controller = new CrearFacturaController();
            }
            catch
            {
                throw new Exception("No se ha podido conseguir acceso a alguno de los botones");
            }
        }

        private void OnNewClientIsClicked(object sender, RoutedEventArgs e)
        {
            AddNewClientWindow add_new_client_window = new AddNewClientWindow();
            add_new_client_window.Show();
        }

        private void  RefreshComboBox()
        {
            // Obtener todas las letras del alfabeto
            char[] letras = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            // Agregar las letras al ComboBox
            foreach (char letra in letras)
            {
                selecionar_letra_combobox_.Items.Add(letra);
            }
        }

        void ActualizarFilasDeUnidades()
        {
            
        }

        private void guardad_factura_btn__Click(object sender, RoutedEventArgs e)
        {
            string letra_selecionada = " ";
            if (selecionar_letra_combobox_.SelectedItem != null)
            {
                object valor_seleccionado = selecionar_letra_combobox_.SelectedItem;
                letra_selecionada = valor_seleccionado.ToString();
            }
            else
            {
                ShowMessageOrError.ShowError("Ninguna letra selecionada");
                return;
            }

            string fecha = "";
            if (ComprobarFechaValida(fecha_box_.Text))
                fecha = fecha_box_.Text;
            else
                return;

            Cliente cliente = ClientesController.clientes_list.FirstOrDefault(cliente_busca => cliente_busca.Nombre == clients_dropdown_.Text);
            if (cliente == null)
            {
                ShowMessageOrError.ShowError("Selecione un cliente valido");
                return;
            }

            List<UnidadComprada> unidades_compradas = new List<UnidadComprada>();
            unidades_compradas.Add(CrearUnidad(unidades_producto_txt_.Text, tipo_unidad_txt_.Text, precio_unidad_txt_.Text, iva_txt_.Text));

            factura_controller.CrearFactura(letra_selecionada, cliente, fecha, unidades_compradas);


        }

        bool ComprobarFechaValida(string _fecha)
        {
            string format = "yyyy-MM-dd";
            DateTime date;

            if (DateTime.TryParseExact(_fecha, format, null, System.Globalization.DateTimeStyles.None, out date))
            {
                return true;
            }
            else
            {
                ShowMessageOrError.ShowError("Formato de fecha invalido, pruebe con: aaaa-MM-dd");
                return false;
            }
        }

        UnidadComprada CrearUnidad(string _cantiadad_uniades, string _tipo_unidad, string _precio_uniada, string _IVA)
        {
            float precio_total = float.Parse(_cantiadad_uniades) * float.Parse(_precio_uniada) + (float.Parse(_cantiadad_uniades) * float.Parse(_precio_uniada) * (float.Parse(_IVA) / 100));

            float precio_sin_IVA = float.Parse(_cantiadad_uniades) * float.Parse(_precio_uniada);

            float precio_añadido = precio_total - precio_sin_IVA;

            UnidadComprada unidad_comprada = new UnidadComprada
            {
                UnidadesDelProducto = int.Parse(_cantiadad_uniades),
                TipoDeUnidad = _tipo_unidad,
                PrecioPorUnidad = int.Parse(_precio_uniada),
                IVA = int.Parse(_IVA),
                AñadidoPorIVA = precio_añadido,
                PrecioConIVA = precio_total
            };

            return unidad_comprada;
        }
    }
}
