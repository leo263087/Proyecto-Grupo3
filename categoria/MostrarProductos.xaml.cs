using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.IO;
using Microsoft.Maui.Storage;
using static Proyecto_Grupo3.datos;

namespace Proyecto_Grupo3.categoria;

public partial class NewPage1 : ContentPage
{

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ProductosViewModel vm)
            await vm.CargarProductosAsync();
    }


    public class ProductosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<datos.Producto> Productos { get; set; } = new ObservableCollection<datos.Producto>();

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
                    OnPropertyChanged(nameof(ProductosFiltrados));
                }
            }
        }
        private static async Task<List<datos.Producto>> CargarProductosDesdeJsonAsync(string fileName)
        {
            string fullPath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            if (!File.Exists(fullPath))
                return new List<datos.Producto>();

            string json = await File.ReadAllTextAsync(fullPath);
            return JsonSerializer.Deserialize<List<datos.Producto>>(json) ?? new List<datos.Producto>();
        }

        public async Task CargarProductosAsync()
        {
            var productos = await CargarProductosDesdeJsonAsync("productos.json");
            this.Productos.Clear();
            foreach (var p in productos)
                Productos.Add(p);
            OnPropertyChanged(nameof(ProductosFiltrados));
        }
        public IEnumerable<datos.Producto> ProductosFiltrados
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TextoBusqueda))
                    return Productos;
                return Productos.Where(p =>
                    (p.Nombre?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (p.ID?.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ?? false));
            }
        }

        public Command<datos.Producto> EliminarProductoCommand { get; }


        public ProductosViewModel()
        {
            EliminarProductoCommand = new Command<datos.Producto>(async (producto) => await EliminarProductoAsync(producto));
        }

     

        private async Task EliminarProductoAsync(datos.Producto producto)
        {
            if (producto != null)
            {
                Productos.Remove(producto);
                OnPropertyChanged(nameof(ProductosFiltrados));
                await GuardarProductosEnJsonAsync("productos.json");
            }
        }

        private async Task GuardarProductosEnJsonAsync(string fileName)
        {
            string fullPath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            var productosList = Productos.ToList();
            string json = JsonSerializer.Serialize(productosList, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(fullPath, json);
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
        BindingContext = new ProductosViewModel();
    }
    private async void OnEditarProductoClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Producto producto)
        {
            // Navega a la página de edición, pasando el producto seleccionado
            await Navigation.PushAsync(new EditarProductoPage(producto));
        }
    }

    private async void OnSeleccionProducto(object sender, SelectionChangedEventArgs e)
    {
        var producto = e.CurrentSelection.FirstOrDefault() as Producto;
        if (producto != null)
        {
            await DisplayAlert(
                "Detalles del producto",
                $"ID: {producto.ID}\nNombre: {producto.Nombre}\nDescripción: {producto.Descripcion}\nCantidad: {producto.Cantidad}\nPrecio: {producto.Precio}",
                "Cerrar");
            ((CollectionView)sender).SelectedItem = null;
        }
    }


}




