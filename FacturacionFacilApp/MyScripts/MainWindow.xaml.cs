using FacturacionFacilApp.MyScripts.Ingresos;
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
        private Button ingresos_btn;

        private FacturaWindow factura_window;

        //El inicializador de la clase MainWindow
        public MainWindow()
        {
            InitializeComponent();
             
            try 
            {
                ingresos_btn = FindName("ingresos_btn_") as Button;
                ingresos_btn.Click += OnIngresosIsClicked;
            }
            catch
            {
                throw new Exception("No se ha podido conseguir acceso a alguno de los botones");
            }

        }

        //Esta función es llamada cuando ingresos_btn es llamado y se encarga de cambiar de escena
        public void OnIngresosIsClicked(object sender, RoutedEventArgs e)
        {
            if (factura_window is null)
            {
                factura_window = new FacturaWindow();
            }

            this.Close();
            factura_window.Show();
        }
    }
}
