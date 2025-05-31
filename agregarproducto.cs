using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Proyecto_Grupo3.datos;

namespace Proyecto_Grupo3//todos los que dicen guna2textbox son espacios en blanco donde se piden al usuario ingresar un dato
{
    /*
    internal class agregarproducto
    {

        public  Producto { get; set; } = new Producto(); // Initialize to avoid nullability issues  
        public List<Producto> Productos = new List<Producto>();
        private int Opcion;
      
        public bool EsIdProductoUnico(int id, List<Producto> productos, int? idActual = null)//sirve para evaluar si el id ingresado ya existe
        {
            // Excluir el ID actual de la validación si se está editando
            return !productos.Any(p => p.ID == id && p.ID != idActual);
        }

        private void guna2Button1_Click(object sender, EventArgs e)//esta funcion es cuando el usuario le dio click al boton de cofirmar al agregar/editar producto 
        {
            try
            {
                int nuevoId = int.Parse(guna2TextBox1.Text); // Suponiendo que el ID se ingresa en `guna2TextBox1`
                //validacion para que el ID sea mayor o igual a 3
                if (guna2TextBox1.Text.Trim().Length < 3)
                {
                    MessageBox.Show("El ID no puede ser menor a 3.");
                    return;
                }
                else if (!EsIdProductoUnico(nuevoId, Productos, producto?.ID))

                {
                    MessageBox.Show("El ID ya existe. Por favor, escoja otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //si los campos son correctos, agregar
                    producto = new Producto();
                    producto.ID = int.Parse(guna2TextBox1.Text);
                    producto.Nombre = guna2TextBox2.Text;
                    producto.Descripcion = guna2TextBox3.Text;
                    producto.Categoria = guna2TextBox4.Text;
                    producto.PrecioUnitario = decimal.Parse(guna2TextBox6.Text);
                    producto.Cantidad = int.Parse(guna2TextBox5.Text);

                    // Validación normal de ID repetido (si se agrega)

                    MessageBox.Show("Operacion exitosa!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Campos Erroneos. Asegurese de haber ingresado correctamente los campos y vuelva a intentar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("El número ingresado es demasiado grande o pequeño para el ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Campos vacios. Verifique de haber LLenado todos los campos e intente nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void SetDatosProducto(string id, string nombre, string categoria, string Cantidad, string descripcion, string precio)
        {
            //Rellenar los textboxes al editar un producto
            try
            {
                guna2TextBox1.Text = id;
                guna2TextBox2.Text = nombre;
                guna2TextBox4.Text = categoria;
                guna2TextBox3.Text = descripcion;
                guna2TextBox5.Text = Cantidad;
                guna2TextBox6.Text = precio;
                // Actualiza el ID del producto actual para la validación
                producto.ID = int.Parse(id);
            }
            catch
            {
            }
        }

        public Producto ObtenerProductoEditado()//Obtener los cambios
        {
            Producto productoEditado = new Producto
            {
                ID = int.Parse(guna2TextBox1.Text),
                Nombre = guna2TextBox2.Text,
                Categoria = guna2TextBox4.Text,
                Descripcion = guna2TextBox3.Text,
                PrecioUnitario = decimal.Parse(guna2TextBox6.Text),
                Cantidad = int.Parse(guna2TextBox5.Text)
            };
            return productoEditado;
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)//para prohibir al usuario ingresar caracteres en el ID
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la entrada de caracteres no numéricos
            }
        }*/
    }

