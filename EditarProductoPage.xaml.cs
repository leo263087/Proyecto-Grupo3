using System.Collections.ObjectModel;
using System.Text.Json;
using static Proyecto_Grupo3.datos;

namespace Proyecto_Grupo3;

public partial class EditarProductoPage : ContentPage
{
    private Producto productoOriginal;
    public ObservableCollection<KeyValuePair<string, string>> PropiedadesObservable { get; set; }
    private async void OnCerrarClicked(object dreadful, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    public EditarProductoPage(Producto producto)
    {
        InitializeComponent();
        productoOriginal = producto;
        // Convierte el diccionario a ObservableCollection para el binding
        PropiedadesObservable = new ObservableCollection<KeyValuePair<string, string>>(
            productoOriginal.PropiedadesEspecificas ?? new Dictionary<string, string>());

        BindingContext = productoOriginal;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        // Cargar la lista de productos desde el archivo
        string fileName = "productos.json";
        string fullPath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        List<datos.Producto> productos = new();

        if (File.Exists(fullPath))
        {
            string json = await File.ReadAllTextAsync(fullPath);
            productos = JsonSerializer.Deserialize<List<datos.Producto>>(json) ?? new List<datos.Producto>();
        }

        // Buscar y actualizar el producto en la lista
        var productoEnLista = productos.FirstOrDefault(p => p.ID == productoOriginal.ID);
        if (productoEnLista != null)
        {
            productoEnLista.Nombre = productoOriginal.Nombre;
            productoEnLista.Descripcion = productoOriginal.Descripcion;
            productoEnLista.Cantidad = productoOriginal.Cantidad;
            productoEnLista.Precio = productoOriginal.Precio;
            // Actualiza las propiedades específicas con los valores editados
           productoEnLista.PropiedadesEspecificas = PropiedadesObservable.ToDictionary(kv => kv.Key, kv => kv.Value);

        }

        // Guardar la lista actualizada
        string nuevoJson = JsonSerializer.Serialize(productos, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(fullPath, nuevoJson);

        await DisplayAlert("Éxito", "Producto actualizado correctamente.", "OK");
        await Navigation.PopAsync();
    }
}
