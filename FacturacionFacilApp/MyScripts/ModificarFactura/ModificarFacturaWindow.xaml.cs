using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers.Models;
using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using FacturacionFacilApp.MyScripts.Ingresos;
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
using iText.Commons.Bouncycastle.Asn1.Pkcs;

namespace FacturacionFacilApp.MyScripts.ModificarFactura
{

    public partial class ModificarFacturaWindow : Window
    {
        private ClientesController clients_controller;
        private ProvedoresController provedores_controller;
        private CrearFacturaController factura_controller;
        private ListaDeProductosController lista_de_productos_controller;
        private float[] lista_IRPF = { 0, 7, 15 };

        private Factura actual_factura;
        private List<Factura> facturas;

        // Constructor de la clase FacturaWindow
        public ModificarFacturaWindow()
        {
            // Inicialización de componentes de la ventana
            InitializeComponent();

            // Llamada a los métodos para refrescar los ComboBox
            RefreshIRPFComboBox();
            RefreshIngresoGastoBox();
            RefreshLetraComboBox();

            // Creación de instancias de controladores
            lista_de_productos_controller = new ListaDeProductosController(main_grid_, 277);
            clients_controller = new ClientesController(clients_dropdown_);
            provedores_controller = new ProvedoresController(provedores_dropdown_);
            factura_controller = new CrearFacturaController();

            //Actualizar la factura
            selecionar_letra_combobox_.SelectionChanged += (object o, SelectionChangedEventArgs e) =>
            {
                actual_factura = facturas.FirstOrDefault(factura => factura.Numero == selecionar_letra_combobox_.SelectedItem.ToString());

                // Limpiar y agregar elemento en irpf_dropdown_
                irpf_dropdown_.Items.Clear();
                irpf_dropdown_.Items.Add(actual_factura.IRPF);
                irpf_dropdown_.SelectedItem = actual_factura.IRPF;

                // Limpiar y agregar elementos en otros ComboBox
                clients_dropdown_.SelectedItem = actual_factura.Cliente.Nombre;

                provedores_dropdown_.SelectedItem = actual_factura.Proveedor.Nombre;

                // Agregar elementos a lista_de_productos_controller
                foreach (UnidadComprada unidad in actual_factura.UnidadesCompradas)
                {
                    lista_de_productos_controller.AñadirLinea($"{unidad.UnidadesDelProducto}", unidad.TipoDeUnidad, $"{unidad.PrecioPorUnidad}", $"{unidad.IVA}");
                }

                lista_de_productos_controller.ActualizarLineas();

                // Otros ComboBox y cajas de texto
                fecha_box_.Text = actual_factura.Fecha;
                actividad_dropdown_.Text = actual_factura.Actividad;
                datos_del_pago_textbox_.Text = actual_factura.CondicionesFormaDePago;
            };

            ingresos_gastos_dropdown_.SelectionChanged += (object o, SelectionChangedEventArgs e) =>
            {
                RefreshLetraComboBox();
            };
        }

        // Método para refrescar el ComboBox para seleccionar una letra
        private void RefreshLetraComboBox()
        {
            List<Factura> facturasFromJson = FacturasJsonController.GetFacturasFromJson(ingresos_gastos_dropdown_.SelectedItem.ToString() == "Ingresos" ?
                ControladorURI.IngresosFacturaJson.ToString() : ControladorURI.GastosFacturaJson.ToString());

            selecionar_letra_combobox_.Items.Clear();
            foreach (Factura factura in facturasFromJson)
            {
                selecionar_letra_combobox_.Items.Add(factura.Numero);
            }

            facturas = facturasFromJson; 
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

            ingresos_gastos_dropdown_.SelectedItem = "Ingresos";
        }



        // Método para manejar el evento de hacer clic en "Guardar factura"
        private void GuardarFacturaBTN(object sender, RoutedEventArgs e)
        {
            // Obtener la letra seleccionada del ComboBox
            string letra_selecionada = actual_factura.Numero;
            if (selecionar_letra_combobox_.SelectedItem != null)
            {
                object valor_seleccionado = selecionar_letra_combobox_.SelectedItem;
                letra_selecionada = valor_seleccionado.ToString();
            }
            else
            {
                ShowMessageOrError.ShowError("Ninguna factura selecionada");
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

            //Modificar la factura
            factura_controller.ModificarFactura(letra_selecionada, provedor, cliente, fecha, unidades_compradas, irpf_dropdown_.Text, datos_del_pago, actividad_dropdown_.Text, ingresos_gastos_dropdown_.Text);
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

        // Método para manejar el evento de hacer clic en "Atras"
        private void AtrasBTN(object sender, RoutedEventArgs e)
        {
            MainWindow main_window = new MainWindow();
            main_window.Show();

            this.Close();
        }
    }
}
