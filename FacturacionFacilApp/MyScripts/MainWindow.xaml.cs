using FacturacionFacilApp.MyScripts.Ingresos;
using FacturacionFacilApp.MyScripts.MostarCuentas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FacturacionFacilApp
{
    public partial class MainWindow : Window
    {
        private FacturaWindow factura_window;
        private LibroDeRegistroWindow mostrar_cuentas_window;

        //El inicializador de la clase MainWindow
        public MainWindow()
        {
            InitializeComponent();
             
            try 
            {
                ingresos_btn_.Click += AbrirCrearIngreso;
                ver_cuentas_btn_.Click += AbirirVerCuentas;
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
    }
}
