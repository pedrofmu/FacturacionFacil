﻿using FacturacionFacilApp.MyScripts.MessagesWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts
{
    //Esta clase se encarga de mostrar si hay un error
    public class ShowMessageOrError
    {
        public static void ShowError(string error_to_show)
        {
            ErrorWindow error_window = new ErrorWindow(error_to_show);

            error_window.Show();
        }
    }
}
