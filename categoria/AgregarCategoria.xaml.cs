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

public partial class AgregarCategoria : ContentPage
{
    private const int MaxCategorias = 4;
    private static List<Categoria> lista = new List<Categoria>();

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
    [SupportedOSPlatform("ios11.0")]
    [SupportedOSPlatform("maccatalyst11.0")]
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

     
        // 2. Recorre todos los controles hijos de CategoriasStack, toma solo los Entry,
        //    obtiene su texto, lo limpia y crea una lista solo con los que no están vacíos.
       

        // 3. Si el nombre de la categoría está vacío, muestra un mensaje de error y termina la función.
        if (string.IsNullOrWhiteSpace(nombreCategoria))
        {
            await DisplayAlert("Error", "Debes ingresar el nombre de la categoría.", "OK");
            return;
        }

        // 4. Si no hay propiedades ingresadas, muestra un mensaje de error y termina la función.
        if (propiedadesCategoria.Count == 0)
        {
            await DisplayAlert("Error", "Agrega al menos una propiedad.", "OK");
            return;
        }

        var categoria = new Proyecto_Grupo3.datos.Categoria(nombreCategoria);
       
        categoria.AgregarPropiedad(propiedadesCategoria);
        if (!string.IsNullOrWhiteSpace(nombreSubcategoria))
        {
            var subcategoria = new Proyecto_Grupo3.datos.Categoria(nombreSubcategoria);
            subcategoria.AgregarPropiedad(propiedadesSubcategoria);
            categoria.AgregarSubcategoria(subcategoria);
        }


      
      
        categoria.Nombre = nombreCategoria;
        // 6. Agrega la nueva categoría a la lista estática de categorías.
        lista.Add(categoria);

        // 7. Guarda la lista de categorías en un archivo JSON y obtiene la ruta del archivo.
        string ruta = await GuardarCategoriaComoJsonAsync("categoria.json");

   
        // 9. Muestra un mensaje de confirmación con el nombre y las propiedades de la categoría creada.
        await DisplayAlert("Categoría creada", $"Nombre: {nombreCategoria}\nPropiedades: {string.Join(", ", propiedadesCategoria)}", "OK");

        // 10. Cierra la página actual y vuelve a la anterior.
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
