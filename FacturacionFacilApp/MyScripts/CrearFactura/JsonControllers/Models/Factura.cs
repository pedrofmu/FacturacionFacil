﻿using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionFacilApp.MyScripts.Ingresos.JsonModels
{
    public class Factura
    {
        public string Numero { get; set; }  //letra + numero ej: A1 A2 A63 T48
        public Cliente Cliente { get; set; }
        public Proveedor Proveedor { get; set; }
        public string Fecha { get; set; }
        public UnidadComprada[] UnidadesCompradas { get; set; }
        public float TotalBaseImponible { get; set; } //Total base imponible UnidadesTotalesSumadas
        public string IRPF { get; set; }
        public float AñadidoPorIVA { get; set; }
        public float ImporteTotalFinal { get ; set; }
        public string CondicionesFormaDePago { get; set; }
        public string Actividad { get; set; }
    }
}
