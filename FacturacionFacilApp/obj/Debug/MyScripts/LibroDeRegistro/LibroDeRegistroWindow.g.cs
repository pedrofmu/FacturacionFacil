﻿#pragma checksum "..\..\..\..\MyScripts\LibroDeRegistro\LibroDeRegistroWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "36BA7D81130ECD48042960848D1DC510DEBF8BE9D76A93548E7887063D2913BB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FacturacionFacilApp.MyScripts.MostarCuentas;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FacturacionFacilApp.MyScripts.MostarCuentas {
    
    
    /// <summary>
    /// LibroDeRegistroWindow
    /// </summary>
    public partial class LibroDeRegistroWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\MyScripts\LibroDeRegistro\LibroDeRegistroWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid mostrar_data_;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\MyScripts\LibroDeRegistro\LibroDeRegistroWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button atras_btn_;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\MyScripts\LibroDeRegistro\LibroDeRegistroWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ordenar_iva_combox_;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\MyScripts\LibroDeRegistro\LibroDeRegistroWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ordenar_clientes_combox_;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FacturacionFacilApp;component/myscripts/libroderegistro/libroderegistrowindow.xa" +
                    "ml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MyScripts\LibroDeRegistro\LibroDeRegistroWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.mostrar_data_ = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.atras_btn_ = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\..\MyScripts\LibroDeRegistro\LibroDeRegistroWindow.xaml"
            this.atras_btn_.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ordenar_iva_combox_ = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.ordenar_clientes_combox_ = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

