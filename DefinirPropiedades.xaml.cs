namespace Proyecto_Grupo3;
using static datos;

public partial class DefinirPropiedades : ContentPage
{
    private readonly List<datos.Categoria> _subcategorias;
    private datos.Categoria _subcategoriaSeleccionada;

    public DefinirPropiedades(List<datos.Categoria> subcategorias)
    {
        InitializeComponent();
        _subcategorias = subcategorias ?? new();
        SubcategoriaPicker.ItemsSource = _subcategorias.Select(s => s.Nombre).ToList();
    }

    private void OnSubcategoriaSeleccionada(object sender, EventArgs e)
    {
        int idx = SubcategoriaPicker.SelectedIndex;
        if (idx < 0) return;

        _subcategoriaSeleccionada = _subcategorias[idx];
        GenerarCampos(_subcategoriaSeleccionada.Propiedades);
    }

    private void GenerarCampos(List<string> propiedades)
    {
        PropiedadesStack.Children.Clear();
        foreach (var propiedad in propiedades)
        {
            var label = new Label { Text = propiedad };
            var entry = new Entry { Placeholder = $"Introduce {propiedad}", AutomationId = propiedad };
            PropiedadesStack.Children.Add(label);
            PropiedadesStack.Children.Add(entry);
        }
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        // Recoge los valores introducidos por el usuario para las propiedades de la subcategoría seleccionada
        var valores = new Dictionary<string, string>();
        foreach (var propiedad in _subcategoriaSeleccionada.Propiedades)
        {
            var entry = PropiedadesStack.Children
                .OfType<Entry>()
                .FirstOrDefault(e => e.AutomationId == propiedad);
            valores[propiedad] = entry?.Text ?? string.Empty;
        }

        // Aquí puedes usar 'valores' según lo que necesites (guardar, enviar, etc.)

        await DisplayAlert("Propiedades guardadas",
            string.Join("\n", valores.Select(kv => $"{kv.Key}: {kv.Value}")),
            "OK");
        await Navigation.PopAsync();
    }
}

