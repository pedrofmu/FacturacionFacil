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

namespace FacturacionFacilApp.MyScripts.MostarCuentas
{
    public partial class LibroDeRegistroWindow : Window
    {
        public LibroDeRegistroWindow()
        {
            InitializeComponent();

            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Fecha";
            column.Binding = new Binding("Fecha");

            mostrar_data_.Columns.Add(column);

            mostrar_data_.ItemsSource = FacturasJsonController.GetFacturasFromJson(new Uri("Json/Facturas.json", UriKind.Relative).ToString());
        }
    }
}
