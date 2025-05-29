using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Proyecto_Grupo3.categoria;

public partial class NewPage1 : ContentPage
{
    public class Producto
    {
        public string Nombre { get; set; }
        public string ID { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
    }

    public class ProductosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Producto> Productos { get; set; }

        private string textoBusqueda;
        public string TextoBusqueda
        {
            get => textoBusqueda;
            set
            {
                if (textoBusqueda != value)
                {
                    textoBusqueda = value;
                    OnPropertyChanged(nameof(TextoBusqueda));
                    OnPropertyChanged(nameof(ProductosFiltrados)); // Notifica que la lista filtrada ha cambiado
                }
            }
        }

        public IEnumerable<Producto> ProductosFiltrados
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TextoBusqueda))
                    return Productos; // Mostrar todos los productos si no hay texto de búsqueda
                return Productos.Where(p =>
                    p.Nombre.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ||
                    p.ID.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase)); // Filtrar por nombre o ID
            }
        }

        public Command<Producto> EliminarProductoCommand { get; }

        public ProductosViewModel()
        {
            Productos = new ObservableCollection<Producto>
            {
                new Producto { Nombre = "Laptop", ID = "001", Descripcion = "Laptop potente", Categoria = "Electrónica" },
                new Producto { Nombre = "Mouse", ID = "002", Descripcion = "Mouse ergonómico", Categoria = "Accesorios" },
                new Producto { Nombre = "Teclado", ID = "003", Descripcion = "Teclado mecánico", Categoria = "Accesorios" }
            };

            EliminarProductoCommand = new Command<Producto>(EliminarProducto);
        }

        private void EliminarProducto(Producto producto)
        {
            if (producto != null)
            {
                Productos.Remove(producto);
                OnPropertyChanged(nameof(ProductosFiltrados)); // Actualiza la lista filtrada
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public NewPage1()
    {
        InitializeComponent();
        var viewModel = new ProductosViewModel();
        BindingContext = viewModel;

        // Forzar la actualización inicial
        viewModel.TextoBusqueda = string.Empty;
    }

    private async void OnSeleccionProducto(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Producto productoSeleccionado)
        {
            // Muestra un mensaje con los detalles del producto
            await DisplayAlert("Detalles del Producto",
                $"Nombre: {productoSeleccionado.Nombre}\n" +
                $"ID: {productoSeleccionado.ID}\n" +
                $"Descripción: {productoSeleccionado.Descripcion}\n" +
                $"Categoría: {productoSeleccionado.Categoria}",
                "OK");

            // Deselecciona el producto después de mostrar el mensaje
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
