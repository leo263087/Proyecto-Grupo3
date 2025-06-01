using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Microsoft.Maui.Storage;
using static Proyecto_Grupo3.datos;
using System.Collections;
using System.Runtime.Versioning;
namespace Proyecto_Grupo3.categoria;
[SupportedOSPlatform("ios11.0")]
[SupportedOSPlatform("maccatalyst11.0")]
public partial class AgregarCategoria : ContentPage
{
    private const int MaxCategorias = 4;
    private static List<Categoria> lista = new List<datos.Categoria>();

    public AgregarCategoria()
    {
        InitializeComponent();
    }
    [SupportedOSPlatform("ios11.0")]
    [SupportedOSPlatform("maccatalyst11.0")]
    private void OnAgregarSubcategoriaClicked(object sender, EventArgs e)
    {
        var nuevaEntry = new Entry
        {
            Placeholder = $"Propiedad {SubcategoriasStack.Children.Count + 1}",
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            Margin = new Thickness(0, 5, 0, 0)
        };
        SubcategoriasStack.Children.Add(nuevaEntry);
    }
   
    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OnAgregarCategoriaClicked(object sender, EventArgs e)
    {
        if (CategoriasStack.Children.Count < MaxCategorias)
        {
            var nuevaEntry = new Entry
            {
                Placeholder = $"Propiedad {CategoriasStack.Children.Count + 1}",
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                Margin = new Thickness(0, 5, 0, 0)
            };
            CategoriasStack.Children.Add(nuevaEntry);
        }
        else
        {
            DisplayAlert("Límite alcanzado", "Solo puedes agregar hasta 4 propiedades.", "OK");
        }
    }
    [SupportedOSPlatform("ios11.0")]
    [SupportedOSPlatform("maccatalyst11.0")]
    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        // 1. Obtiene el texto del Entry donde se escribe el nombre de la categoría, quitando espacios al inicio y final.
        string nombreCategoria = NombreCategoriaEntry?.Text?.Trim() ?? string.Empty;
        var propiedadesCategoria = CategoriasStack.Children
            .OfType<Entry>()
            .Select(entry => entry.Text?.Trim())
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .ToList();

        string nombreSubcategoria = NombreSubcategoriaEntry?.Text?.Trim() ?? string.Empty;
        var propiedadesSubcategoria = SubcategoriasStack.Children
            .OfType<Entry>()
            .Select(entry => entry.Text?.Trim())
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .ToList();

        // 2. Validaciones
        if (string.IsNullOrWhiteSpace(nombreCategoria))
        {
            await DisplayAlert("Error", "Debes ingresar el nombre de la categoría.", "OK");
            return;
        }

        if (propiedadesCategoria.Count == 0)
        {
            await DisplayAlert("Error", "Agrega al menos una propiedad para la categoría.", "OK");
            return;
        }

        // 3. Crear la categoría y agregar propiedades
        var categoria = new Proyecto_Grupo3.datos.Categoria(nombreCategoria);
        categoria.AgregarPropiedad(propiedadesCategoria);

        // 4. Crear la subcategoría y agregar propiedades si existe
        if (!string.IsNullOrWhiteSpace(nombreSubcategoria))
        {
            var subcategoria = new Proyecto_Grupo3.datos.Categoria(nombreSubcategoria);
            subcategoria.AgregarPropiedad(propiedadesSubcategoria);
            categoria.AgregarSubcategoria(subcategoria);
        }

        // 5. Agregar la categoría a la lista
        lista.Add(categoria);

        // 6. Guardar la lista de categorías en un archivo JSON
        string ruta = await GuardarCategoriaComoJsonAsync("categoria.json");

        // 7. Mostrar mensaje de confirmación
        await DisplayAlert("Categoría creada", $"Nombre: {nombreCategoria}\nPropiedades: {string.Join(", ", propiedadesCategoria)}", "OK");

        // 8. Cierra la página actual y vuelve a la anterior
        await Navigation.PopAsync();
    }
    


    [SupportedOSPlatform("ios11.0")]
    [SupportedOSPlatform("maccatalyst11.0")]
    private static async Task<string> GuardarCategoriaComoJsonAsync(string fileName)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(lista, options);

        string fullPath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        await File.WriteAllTextAsync(fullPath, json);

        return fullPath;
    }

}
