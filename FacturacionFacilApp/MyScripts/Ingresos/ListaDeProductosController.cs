using FacturacionFacilApp.MyScripts.Ingresos.JsonControllers;
using FacturacionFacilApp.MyScripts.Ingresos.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FacturacionFacilApp.MyScripts
{
    //Esta clase se encarga de controlar la lista con los diferentes productos
    public class ListaDeProductosController
    {
        public List<Linea> lineas { get; private set; }

        private Grid grid;

        private int current_index = 0;

        private float starting_position = 277;

        public ListaDeProductosController(Grid _grid, float _starting_position)
        {
            grid = _grid;

            lineas = new List<Linea>();
            starting_position = _starting_position;
        }

        //Añade un nuevo producto
        public void AñadirLinea()
        {
            if (lineas is null)
            {
                lineas = new List<Linea>();
            }

            Linea nueva_linea = new Linea
            {
                indice = lineas.Count + 1,
                numero_indice = new Label { Content = (lineas.Count + 1).ToString(), Width = 30, Height = 30, HorizontalAlignment = System.Windows.HorizontalAlignment.Left, VerticalAlignment = System.Windows.VerticalAlignment.Top },
                unidades = new TextBox { Width = 60, Height = 20, Text = "Cantidad", HorizontalAlignment = System.Windows.HorizontalAlignment.Left, VerticalAlignment = System.Windows.VerticalAlignment.Top },
                tipo = new TextBox { Width = 108, Height = 20, Text = "Tipo", HorizontalAlignment = System.Windows.HorizontalAlignment.Left, VerticalAlignment = System.Windows.VerticalAlignment.Top },
                precio_unidad = new TextBox { Width = 108, Height = 20, Text = "Precio por unidad", HorizontalAlignment = System.Windows.HorizontalAlignment.Left, VerticalAlignment = System.Windows.VerticalAlignment.Top },
                iva = new TextBox { Width = 60, Height = 20, Text = "IVA", HorizontalAlignment = System.Windows.HorizontalAlignment.Left, VerticalAlignment = System.Windows.VerticalAlignment.Top }
            };

            lineas.Add(nueva_linea);

            if (lineas.Count > 5)
            {
                current_index = lineas.Count - 5;
            }
        }

        //Elimina el ultimo producto
        public void EliminarLinea()
        {
            if (lineas.Count > 0)
            {
                Linea linea_remover = lineas.Last();

                grid.Children.Remove(linea_remover.numero_indice);
                grid.Children.Remove(linea_remover.unidades);
                grid.Children.Remove(linea_remover.tipo);
                grid.Children.Remove(linea_remover.precio_unidad);
                grid.Children.Remove(linea_remover.iva);

                lineas.RemoveAt(lineas.Count - 1);

                if (lineas.Count > 5)
                {
                    current_index = lineas.Count - 5;
                }
                ActualizarLineas();
            }
        }

        //Ajusta visualizacion de las lineas
        public void ActualizarLineas()
        {
            float vertical_margin = starting_position;

            foreach (Linea linea in lineas)
            {
                grid.Children.Remove(linea.numero_indice);
                grid.Children.Remove(linea.unidades);
                grid.Children.Remove(linea.tipo);
                grid.Children.Remove(linea.precio_unidad);
                grid.Children.Remove(linea.iva);
            }

            List<Linea> lineas_mostrar = lineas.Skip(current_index).Take(5).ToList();
            foreach (Linea linea in lineas_mostrar)
            {
                grid.Children.Add(linea.numero_indice);
                linea.numero_indice.Margin = new System.Windows.Thickness(10, vertical_margin, 0, 0);

                grid.Children.Add(linea.unidades);
                linea.unidades.Margin = new System.Windows.Thickness(55, vertical_margin, 0, 0);

                grid.Children.Add(linea.tipo);
                linea.tipo.Margin = new System.Windows.Thickness(132, vertical_margin, 0, 0);

                grid.Children.Add(linea.precio_unidad);
                linea.precio_unidad.Margin = new System.Windows.Thickness(262, vertical_margin, 0, 0);

                grid.Children.Add(linea.iva);
                linea.iva.Margin = new System.Windows.Thickness(393, vertical_margin, 0, 0);

                vertical_margin += 30;
            }
        }

        //Sube una de las filas en la visualizacion
        public void SubirFila()
        {
            if (current_index > 0)
            {
                current_index--;
                ActualizarLineas();
            }
        }

        //Baja una de las filas en la visualizacion
        public void BajarFila()
        {
            if (current_index + 5 < lineas.Count)
            {
                current_index++;
                ActualizarLineas();
            }
        }

        //Obtiene las unidades representadas en la lista 
        public bool ConseguirUnidades(out List<UnidadComprada> _unidades_compradas)
        {
            List<UnidadComprada> unidades_compradas = new List<UnidadComprada>();

            foreach(Linea linea in lineas)
            {
                try
                {
                    UnidadComprada unidad = CrearUnidad(linea.unidades.Text, linea.tipo.Text, linea.precio_unidad.Text, linea.iva.Text);
                    unidades_compradas.Add(unidad);
                }
                catch
                {
                    _unidades_compradas = new List<UnidadComprada>();
                    return false;
                }
            }

            _unidades_compradas = unidades_compradas;

            return true;
        }

        //Funcion que se encarga de crear las unidades
        UnidadComprada CrearUnidad(string _cantiadad_uniades, string _tipo_unidad, string _precio_uniada, string _IVA)
        {
            float precio_total = float.Parse(_cantiadad_uniades) * float.Parse(_precio_uniada) + (float.Parse(_cantiadad_uniades) * float.Parse(_precio_uniada) * (float.Parse(_IVA) / 100));

            float precio_sin_IVA = float.Parse(_cantiadad_uniades) * float.Parse(_precio_uniada);

            float precio_añadido = precio_total - precio_sin_IVA;

            UnidadComprada unidad_comprada = new UnidadComprada
            {
                UnidadesDelProducto = int.Parse(_cantiadad_uniades),
                TipoDeUnidad = _tipo_unidad,
                PrecioPorUnidad = int.Parse(_precio_uniada),
                IVA = int.Parse(_IVA),
                AñadidoPorIVA = precio_añadido,
                PrecioConIVA = precio_total
            };

            return unidad_comprada;
        }
    }

    //Clase para representar cada una de las lineas
    public class Linea
    {
        public int indice;

        public Label numero_indice;
        public TextBox unidades;
        public TextBox tipo;
        public TextBox precio_unidad;
        public TextBox iva;
    }
}
