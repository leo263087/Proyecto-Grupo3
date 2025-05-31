namespace Proyecto_Grupo3;

using System.Runtime.Versioning;
using static datos;

[SupportedOSPlatform("MacCatalyst13.1"), SupportedOSPlatform("iOS11.0"), SupportedOSPlatform("Android21.0")]

public partial class DefinirPropiedades : ContentPage
{
    private Categoria categoriaActual;

    public List<Categoria> Subcategorias { get; set; }

    // Ensure the correct declaration of PropiedadesStack to avoid ambiguity  
   
    public DefinirPropiedades(Categoria categoria)
    {
        InitializeComponent();
        categoriaActual = categoria;
        Subcategorias = categoriaActual.Subcategorias ?? new List<Categoria>();
        BindingContext = this; // Ahora el binding es a la página, no a la categoría  
    }

    // Elimina la propiedad extra PropiedadesStack

    // ... el resto de tu clase igual ...

    private void CrearEntrysParaPropiedades(List<string> propiedades)
    {
        PropiedadesStack.Children.Clear();
        foreach (var propiedad in propiedades)
        {
            var label = new Label { Text = propiedad, TextColor = Colors.White };
            var entry = new Entry { Placeholder = $"Definir {propiedad}", BackgroundColor = Colors.White, TextColor = Colors.Black, AutomationId = propiedad };
            PropiedadesStack.Children.Add(label);
            PropiedadesStack.Children.Add(entry);
        }
    }


    private void OnSubcategoriaSeleccionada(object sender, EventArgs e)
    {
        if (SubcategoriaPicker.SelectedIndex == -1)
            return;

        var subcategoriaSeleccionada = SubcategoriaPicker.SelectedItem as Categoria;
        if (subcategoriaSeleccionada != null)
        {
            CrearEntrysParaPropiedades(subcategoriaSeleccionada.Propiedades);
        }
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        var valores = new Dictionary<string, string>();
        foreach (var view in PropiedadesStack.Children)
        {
            if (view is Entry entry && !string.IsNullOrWhiteSpace(entry.AutomationId))
            {
                valores[entry.AutomationId] = entry.Text ?? string.Empty;
            }
        }

        await DisplayAlert("Propiedades guardadas",
            string.Join("\n", valores.Select(kv => $"{kv.Key}: {kv.Value}")),
            "OK");
        await Navigation.PopAsync();
    }
}
