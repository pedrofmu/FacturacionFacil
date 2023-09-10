using FacturacionFacilApp.MyScripts;
using FacturacionFacilApp.MyScripts.Ingresos;
using FacturacionFacilApp.MyScripts.ModificarFactura;
using FacturacionFacilApp.MyScripts.MostarCuentas;
using System;
using System.Windows;


namespace FacturacionFacilApp
{
    public partial class MainWindow : Window
    {
        private FacturaWindow factura_window;
        private LibroDeRegistroWindow mostrar_cuentas_window;
        private ModificarFacturaWindow modificar_factura_window;

        //El inicializador de la clase MainWindow
        public MainWindow()
        {
            InitializeComponent();

            ControladorURI.CheckURI();
             
            try 
            {
                crear_factura_btn_.Click += AbrirCrearIngreso;
                ver_cuentas_btn_.Click += AbirirVerCuentas;
                modificar_facturas_btn_.Click += AbrirModificarFacturas;
            }
            catch
            {
                throw new Exception("No se ha podido conseguir acceso a alguno de los botones");
            }

        }

        //Esta función es llamada cuando ingresos_btn es llamado y se encarga de cambiar de escena
        public void AbrirCrearIngreso(object sender, RoutedEventArgs e)
        {
            if (factura_window is null)
            {
                factura_window = new FacturaWindow();
            }

            this.Close();
            factura_window.Show();
        }

        //Esta funcion es llamada para cambair a la pantalla de ver ingresos
        public void AbirirVerCuentas(object sender, RoutedEventArgs e)
        {
            if (mostrar_cuentas_window is null)
            {
                mostrar_cuentas_window = new LibroDeRegistroWindow();
            }

            this.Close();
            mostrar_cuentas_window.Show();
        }

        //Esta funcion es llamada para cambiar a la pantalla de modificar la factura
        private void AbrirModificarFacturas(object sender, RoutedEventArgs e)
        {
            if (modificar_factura_window is null)
            {
                modificar_factura_window = new ModificarFacturaWindow();
            }

            this.Close();
            modificar_factura_window.Show();
        }
    }
}
