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
        if (e.CurrentSelection.FirstOrDefault() is datos.Producto productoSeleccionado)
        {
            // Información de la categoría principal
            string categoriaNombre = productoSeleccionado.Categoria?.Nombre ?? "Sin categoría";
            string propiedadesCategoria = "Sin propiedades";
            if (productoSeleccionado.Categoria?.Propiedades != null && productoSeleccionado.Categoria.Propiedades.Any())
            {
                propiedadesCategoria = string.Join(", ", productoSeleccionado.Categoria.Propiedades);
            }

            // Información de subcategorías y sus propiedades
            string subcategorias = "Sin subcategorías";
            string propiedadesSubcategorias = "";
            if (productoSeleccionado.Categoria?.Subcategorias != null && productoSeleccionado.Categoria.Subcategorias.Any())
            {
                subcategorias = string.Join(", ", productoSeleccionado.Categoria.Subcategorias.Select(s => s.Nombre));
                propiedadesSubcategorias = string.Join(
                    "\n",
                    productoSeleccionado.Categoria.Subcategorias.Select(sub =>
                        $"- {sub.Nombre}: {(sub.Propiedades != null && sub.Propiedades.Any() ? string.Join(", ", sub.Propiedades) : "Sin propiedades")}"
                    )
                );
            }
            else
            {
                propiedadesSubcategorias = "Sin propiedades de subcategorías";
            }

            string mensaje =
                $"Nombre: {productoSeleccionado.Nombre}\n" +
                $"ID: {productoSeleccionado.ID}\n" +
                $"Descripción: {productoSeleccionado.Descripcion}\n" +
                $"Cantidad: {productoSeleccionado.Cantidad}\n" +
                $"Precio: {productoSeleccionado.Precio}\n" +
                $"Categoría: {categoriaNombre}\n" +
                $"Propiedades de la categoría: {propiedadesCategoria}\n" +
                $"Subcategorías: {subcategorias}\n" +
                $"Propiedades de subcategorías:\n{propiedadesSubcategorias}";
           // mensaje = productoSeleccionado.MostrarDetalles();
            await DisplayAlert("Detalles del Producto", mensaje, "OK");

            ((CollectionView)sender).SelectedItem = null;
        }
    }

}


