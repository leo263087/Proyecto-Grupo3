using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Grupo3
{
        public class Datos
        {

            class Categoria
            {
                public string Nombre { get; set; }
                public List<string> Propiedades { get; private set; }
                public List<Categoria> Subcategorias { get; private set; }

                public Categoria(string nombre)
                {
                    Nombre = nombre;
                    Propiedades = new List<string>();
                    Subcategorias = new List<Categoria>();
                }

                public void AgregarPropiedad(string propiedad)
                {
                    Propiedades.Add(propiedad);
                }

                public List<string> ObtenerPropiedades()
                {
                    return new List<string>(Propiedades);
                }

                public void AgregarSubcategoria(Categoria subcategoria)
                {
                    Subcategorias.Add(subcategoria);
                }

                public List<Categoria> ObtenerSubcategorias()
                {
                    return new List<Categoria>(Subcategorias);
                }
            }

            class Producto
            {
                public string Nombre { get; set; }
                public string Descripcion { get; set; }
                public int Cantidad { get; set; }
                public float Precio { get; set; }
                public Categoria Categoria { get; set; }
                public Dictionary<string, string> PropiedadesEspecificas { get; private set; }

                public Producto(string nombre, string descripcion, int cantidad, float precio, Categoria categoria)
                {
                    Nombre = nombre;
                    Descripcion = descripcion;
                    Cantidad = cantidad;
                    Precio = precio;
                    Categoria = categoria;
                    PropiedadesEspecificas = new Dictionary<string, string>();
                }

                public void AgregarPropiedadEspecifica(string clave, string valor)
                {
                    PropiedadesEspecificas[clave] = valor;
                }

                public void MostrarDetalles()
                {
                    Console.WriteLine($"Producto: {Nombre}");
                    Console.WriteLine($"Descripción: {Descripcion}");
                    Console.WriteLine($"Cantidad: {Cantidad}");
                    Console.WriteLine($"Precio: ${Precio}");
                    Console.WriteLine($"Categoría: {Categoria.Nombre}");
                    Console.WriteLine("Propiedades específicas:");
                    foreach (var propiedad in PropiedadesEspecificas)
                    {
                        Console.WriteLine($"- {propiedad.Key}: {propiedad.Value}");
                    }
                }
            }

            class Inventario
            {
                private List<Producto> productos;
                private List<Categoria> categorias;

                public Inventario()
                {
                    productos = new List<Producto>();
                    categorias = new List<Categoria>();
                }

                public void AgregarProducto(Producto producto)
                {
                    productos.Add(producto);
                }

                public void EliminarProducto(string nombre)
                {
                    productos.RemoveAll(p => p.Nombre == nombre);
                }

                public void AgregarCategoria(Categoria categoria)
                {
                    categorias.Add(categoria);
                }

                public Categoria BuscarCategoria(string nombre)
                {
                    return categorias.FirstOrDefault(c => c.Nombre == nombre);
                }

                public List<Producto> ListarProductos()
                {
                    return new List<Producto>(productos);
                }

                public List<Producto> ListarProductosPorCategoria(string nombreCategoria)
                {
                    return productos.Where(p => p.Categoria.Nombre == nombreCategoria).ToList();
                }

                public Producto BuscarProductoPorNombre(string nombre)
                {
                    return productos.FirstOrDefault(p => p.Nombre == nombre);
                }

                public void ActualizarStock(string nombre, int nuevaCantidad)
                {
                    var producto = BuscarProductoPorNombre(nombre);
                    if (producto != null)
                    {
                        producto.Cantidad = nuevaCantidad;
                    }
                }
            }

        }


}
