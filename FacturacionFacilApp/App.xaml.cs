using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FacturacionFacilApp
{
    public partial class App : Application
    {
        public App()
        {
            // Suscribirse al evento Exit de la aplicación
            this.Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            // Tu código de cierre personalizado aquí
            // Esto se ejecutará cuando la aplicación se cierre.
        }
    }
}
