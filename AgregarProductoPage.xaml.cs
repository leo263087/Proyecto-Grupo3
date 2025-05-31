namespace Proyecto_Grupo3;

using System.Text.Json;
using static datos;
public partial class AgregarProductoPage : ContentPage
{
    // Removed duplicate declaration of categoriasDisponibles  
    private List<datos.Categoria> categoriasDisponibles = new();

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarCategoriasAsync();
    }

    private async Task CargarCategoriasAsync()
    {
        try
        {
            // Usa la ruta segura para almacenamiento local en MAUI
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "categoria.json");
            if (!File.Exists(filePath))
            {
                categoriasDisponibles.Clear();
                CategoriaPicker.ItemsSource = null;
                return;
            }

            string json = await File.ReadAllTextAsync(filePath);
            categoriasDisponibles.Clear();

            if (!string.IsNullOrWhiteSpace(json))
            {
                if (json.TrimStart().StartsWith("["))
                {
                    // El JSON es una lista de categorías
                   List<datos.Categoria> categorias = JsonSerializer.Deserialize<List<datos.Categoria>>(json) ?? new List<datos.Categoria>();
                    categoriasDisponibles.AddRange(categorias);
                }
                else
                {
                    // El JSON es una sola categoría (caso antiguo)
                    var categoria = JsonSerializer.Deserialize<Categoria>(json);
                    if (categoria != null)
                        categoriasDisponibles.Add(categoria);
                }
            }

            CategoriaPicker.ItemsSource = categoriasDisponibles.Select(c => c.Nombre).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar las categorías.\n{ex.Message}", "OK");
            categoriasDisponibles.Clear();
            CategoriaPicker.ItemsSource = null;
        }
    }


    public Producto NuevoProducto { get; set; }

    public Action<Producto> ProductoAgregadoCallback { get; set; }

    public AgregarProductoPage()
    {
        InitializeComponent();
    }

    public async void OnCategoriaSeleccionada(object sender, EventArgs e)
    {
        if (CategoriaPicker.SelectedIndex == -1)
            return;

        // Obtén el nombre seleccionado
        string nombreSeleccionado = CategoriaPicker.SelectedItem as string;
        if (string.IsNullOrEmpty(nombreSeleccionado))
            return;

        // Busca la instancia real en la lista
        var instancia = categoriasDisponibles.FirstOrDefault(c => c.Nombre == nombreSeleccionado);
        if (instancia != null)
        {
            await Navigation.PushAsync(new DefinirPropiedades(instancia));
        }
    }


    private async void OnAgregarCategoriaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Proyecto_Grupo3.categoria.AgregarCategoria());
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(IdEntry.Text) ||
            string.IsNullOrWhiteSpace(NombreEntry.Text) ||
            string.IsNullOrWhiteSpace(DescripcionEntry.Text) ||
            !int.TryParse(CantidadEntry.Text, out int cantidad) ||
            !float.TryParse(PrecioUnitarioEntry.Text, out float precioUnitario))
        {
            await DisplayAlert("Error de validación", "Por favor, complete todos los campos y asegúrese de que 'Cantidad' y 'Precio unitario' sean números válidos.", "OK");
            return;
        }

        ProductoAgregadoCallback?.Invoke(NuevoProducto);

        await Navigation.PopAsync();
    }

    private async void OnCerrarClicked(object dreadful, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
