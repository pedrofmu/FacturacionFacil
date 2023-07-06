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
    public partial class FacturaWindow : Window
    {
        private ClientesController clients_controller;
        private ProvedoresController provedores_controller;

        private CrearFacturaController factura_controller;

        private ListaDeProductosController lista_de_productos_controller;

        private float[] lista_IRPF = { 0, 7, 15}; 
        public FacturaWindow()
        {
            InitializeComponent();
            RefreshComboBox();
            RefreshIRPFComboBox();

            lista_de_productos_controller = new ListaDeProductosController(main_grid_, 277);

            clients_controller = new ClientesController(clients_dropdown_);
            provedores_controller = new ProvedoresController(provedores_dropdown_);
            factura_controller = new CrearFacturaController();
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

        private void GuardarFacturaBTN(object sender, RoutedEventArgs e)
        {
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

            if (lista_de_productos_controller.lineas.Count == 0)
            {
                ShowMessageOrError.ShowError("Ninguna unidad creada");
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

            Provedor provedor = ProvedoresController.provedores_list.FirstOrDefault(provedor_busca => provedor_busca.Nombre == provedores_dropdown_.Text);
            if (provedor == null)
            {
                ShowMessageOrError.ShowError("Selecione un provedor valido");
                return;
            }

            List<UnidadComprada> unidades_compradas = new List<UnidadComprada>();
            if (!lista_de_productos_controller.ConseguirUnidades(out unidades_compradas))
            {
                ShowMessageOrError.ShowError("Introduzca un tipo de valor adecuado en las unidades, número, texto, número, número (0 al 100 segun porcentaje)");
                return;
            }

            factura_controller.CrearFactura(letra_selecionada, provedor, cliente, fecha, unidades_compradas, irpf_dropdown_.Text);
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

        private void AñadirUnidadBTN(object sender, RoutedEventArgs e)
        {
            lista_de_productos_controller.AñadirLinea();
            lista_de_productos_controller.ActualizarLineas();
        }

        private void SubirFilaBTN(object sender, RoutedEventArgs e)
        {
            lista_de_productos_controller.SubirFila();
        }

        private void BajarFilaBTN(object sender, RoutedEventArgs e)
        {
            lista_de_productos_controller.BajarFila();
        }

        private void EliminarUnidadBTN(object sender, RoutedEventArgs e)
        {
            lista_de_productos_controller.EliminarLinea();
        }

        private void NuevoProvedorBTN(object sender, RoutedEventArgs e)
        {
            AddNewProvedorWindow new_provedor_window = new AddNewProvedorWindow();
            new_provedor_window.Show();
        }
    }
}
