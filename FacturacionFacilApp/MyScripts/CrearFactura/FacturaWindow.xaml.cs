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
    // Clase que representa la ventana de la factura
    public partial class FacturaWindow : Window
    {
        private ClientesController clients_controller;
        private ProvedoresController provedores_controller;
        private CrearFacturaController factura_controller;
        private ListaDeProductosController lista_de_productos_controller;
        private float[] lista_IRPF = { 0, 7, 15 };

        // Constructor de la clase FacturaWindow
        public FacturaWindow()
        {
            // Inicialización de componentes de la ventana
            InitializeComponent();

            // Llamada a los métodos para refrescar los ComboBox
            RefreshLetraComboBox();
            RefreshIRPFComboBox();
            RefreshIngresoGastoBox();

            // Creación de instancias de controladores
            lista_de_productos_controller = new ListaDeProductosController(main_grid_, 277);
            clients_controller = new ClientesController(clients_dropdown_);
            provedores_controller = new ProvedoresController(provedores_dropdown_);
            factura_controller = new CrearFacturaController();
        }

        // Método para manejar el evento de hacer clic en "Nuevo cliente"
        private void OnNewClientIsClicked(object sender, RoutedEventArgs e)
        {
            // Creación de una nueva ventana para agregar un nuevo cliente
            AddNewClientWindow add_new_client_window = new AddNewClientWindow();
            add_new_client_window.Show();
        }

        // Método para refrescar el ComboBox para seleccionar una letra
        private void RefreshLetraComboBox()
        {
            char[] letras = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            foreach (char letra in letras)
            {
                selecionar_letra_combobox_.Items.Add(letra);
            }
        }

        // Método para refrescar el ComboBox de IRPF (Impuesto sobre la Renta de las Personas Físicas)
        private void RefreshIRPFComboBox()
        {
            foreach (float tramo in lista_IRPF)
            {
                if (tramo == 0)
                {
                    irpf_dropdown_.Items.Add("Ninguno");
                }
                else
                {
                    irpf_dropdown_.Items.Add(tramo.ToString());
                }
            }
        }

        private void RefreshIngresoGastoBox() 
        {
            ingresos_gastos_dropdown_.Items.Add("Ingresos");
            ingresos_gastos_dropdown_.Items.Add("Gastos");
        }
        


        // Método para manejar el evento de hacer clic en "Guardar factura"
        private void GuardarFacturaBTN(object sender, RoutedEventArgs e)
        {
            // Obtener la letra seleccionada del ComboBox
            string letra_selecionada = string.Empty;
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

            // Verificar si las unidades de productos se han creado correctamente
            if (lista_de_productos_controller.lineas.Count == 0)
            {
                ShowMessageOrError.ShowError("Ninguna unidad creada");
                return;
            }

            // Obtener la fecha ingresada y comprobar si es válida
            string fecha = "";
            if (ComprobarFechaValida(fecha_box_.Text))
                fecha = fecha_box_.Text;
            else
                return;

            // Obtener el cliente seleccionado
            Cliente cliente = ClientesController.clientes_list.FirstOrDefault(cliente_busca => cliente_busca.Nombre == clients_dropdown_.Text);
            if (cliente == null)
            {
                ShowMessageOrError.ShowError("Selecione un cliente valido");
                return;
            }

            // Obtener el proveedor seleccionado
            Proveedor provedor = ProvedoresController.provedores_list.FirstOrDefault(provedor_busca => provedor_busca.Nombre == provedores_dropdown_.Text);
            if (provedor == null)
            {
                ShowMessageOrError.ShowError("Selecione un provedor valido");
                return;
            }

            // Obtener las unidades compradas
            List<UnidadComprada> unidades_compradas = new List<UnidadComprada>();
            if (!lista_de_productos_controller.ConseguirUnidades(out unidades_compradas))
            {
                ShowMessageOrError.ShowError("Introduzca un tipo de valor adecuado en las unidades, número, texto, número, número (0 al 100 segun porcentaje)");
                return;
            }

            string datos_del_pago = datos_del_pago_textbox_.Text;

            if (datos_del_pago == "Introduzca aquí los datos de pago")
            {
                ShowMessageOrError.ShowError("Introduzca una forma y condición del pago");
                return;
            }

            factura_controller.CrearFactura(letra_selecionada, provedor, cliente, fecha, unidades_compradas, irpf_dropdown_.Text, datos_del_pago, actividad_dropdown_.Text, ingresos_gastos_dropdown_.Text);
        }

        // Método para comprobar si una fecha es válida
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

        // Método para manejar el evento de hacer clic en "Añadir unidad"
        private void AñadirUnidadBTN(object sender, RoutedEventArgs e)
        {
            lista_de_productos_controller.AñadirLinea();
            lista_de_productos_controller.ActualizarLineas();
        }

        // Método para manejar el evento de hacer clic en "Subir fila"
        private void SubirFilaBTN(object sender, RoutedEventArgs e)
        {
            lista_de_productos_controller.SubirFila();
        }

        // Método para manejar el evento de hacer clic en "Bajar fila"
        private void BajarFilaBTN(object sender, RoutedEventArgs e)
        {
            lista_de_productos_controller.BajarFila();
        }

        // Método para manejar el evento de hacer clic en "Eliminar unidad"
        private void EliminarUnidadBTN(object sender, RoutedEventArgs e)
        {
            lista_de_productos_controller.EliminarLinea();
        }

        // Método para manejar el evento de hacer clic en "Nuevo proveedor"
        private void NuevoProvedorBTN(object sender, RoutedEventArgs e)
        {
            AddNewProvedorWindow new_provedor_window = new AddNewProvedorWindow();
            new_provedor_window.Show();
        }

        // Método para manejar el evento de hacer clic en "Atras"
        private void AtrasBTN(object sender, RoutedEventArgs e)
        {
            MainWindow main_window = new MainWindow();
            main_window.Show();

            this.Close();
        }
    }
}
