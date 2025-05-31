namespace Proyecto_Grupo3;

using System.Runtime.Versioning;
using static datos;

[SupportedOSPlatform("MacCatalyst13.1"), SupportedOSPlatform("iOS11.0"), SupportedOSPlatform("Android21.0")]
public partial class DefinirPropiedades : ContentPage
{
    private Categoria categoriaActual;
    private readonly Action<Dictionary<string, string>> _callback;

    public List<Categoria> Subcategorias { get; set; }

    public DefinirPropiedades(Categoria categoria, Action<Dictionary<string, string>> callback)
    {
        InitializeComponent();
        categoriaActual = categoria;
        Subcategorias = categoriaActual.Subcategorias ?? new List<Categoria>();
        _callback = callback;
        BindingContext = this;
    }

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

        // Devuelve los valores a la página anterior
        _callback?.Invoke(valores);

        await Navigation.PopAsync();
    }
}
/* <ListView Grid.Row="1"
                      Grid.Column="0"
                      x:Name="ItemsListView"
                      SelectionMode="Single"
                      Margin="0,10,0,10"
                      Background="Transparent">
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <ViewCell>
                            <Grid Padding="10" BackgroundColor="#E0E0E0">
                                <Grid.RowDefinitions>
                                    <RowDefinition Height="Auto" />
                                    <RowDefinition Height="Auto" />
                                </Grid.RowDefinitions>
                                <Grid.ColumnDefinitions>
                                    <ColumnDefinition Width="*" />
                                    <ColumnDefinition Width="Auto" />
                                </Grid.ColumnDefinitions>
                                <Label Grid.Row="0" Grid.Column="0" Text="{Binding Nombre}" FontSize="18" TextColor="Black" FontAttributes="Bold" />
                                <Label Grid.Row="0" Grid.Column="1" Text="{Binding ID, StringFormat='ID: {0}'}" FontSize="14" TextColor="Gray" HorizontalTextAlignment="End"/>
                                <Label Grid.Row="1" Grid.Column="0" Text="{Binding Descripcion}" FontSize="14" TextColor="DarkGray" />
                                <Label Grid.Row="1" Grid.Column="1" Text="{Binding Cantidad, StringFormat='Cant: {0}'}" FontSize="14" TextColor="DarkCyan" HorizontalTextAlignment="End"/>
                            </Grid>
                        </ViewCell>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>*/