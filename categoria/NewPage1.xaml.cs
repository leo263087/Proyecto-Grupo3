using System.Collections.ObjectModel;

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
    public class ProductosViewModel
    {
        public ObservableCollection<Producto> Productos { get; set; }

        public ProductosViewModel()
        {
            Productos = new ObservableCollection<Producto>
        {
            new Producto { Nombre = "Laptop", ID = "001", Descripcion = "Laptop potente", Categoria = "Electr�nica" },
            new Producto { Nombre = "Mouse", ID = "002", Descripcion = "Mouse ergon�mico", Categoria = "Accesorios" }
        };
        }
    }
    
    public NewPage1()
	{
		InitializeComponent();
        BindingContext = new ProductosViewModel(); 
    }
    private void OnSeleccionProducto(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Producto productoSeleccionado)
        {
            // Actualizar los detalles en la UI
            NombreLabel.Text = productoSeleccionado.Nombre;
            IDLabel.Text = "ID: " + productoSeleccionado.ID;
            DescripcionLabel.Text = "Descripci�n: " + productoSeleccionado.Descripcion;
            CategoriaLabel.Text = "Categor�a: " + productoSeleccionado.Categoria;

            // Mostrar el frame con los detalles
            DetallesFrame.IsVisible = true;
        }
    }
    private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        // Obt�n el producto seleccionado
        var productoSeleccionado = e.CurrentSelection.FirstOrDefault() as Producto;

        if (productoSeleccionado != null)
        {
            // Muestra un mensaje con los detalles del producto
            await DisplayAlert("Detalles del Producto",
                $"Nombre: {productoSeleccionado.Nombre}\n" +
                $"ID: {productoSeleccionado.ID}\n" +
                $"Descripci�n: {productoSeleccionado.Descripcion}\n" +
                $"Categor�a: {productoSeleccionado.Categoria}",
                "OK");

            // Deselecciona el producto despu�s de mostrar el mensaje
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        // Crear una nueva barra (puede ser un BoxView o cualquier otro control)
        var nuevaBarra = new BoxView
        {
            HeightRequest = 5,
            WidthRequest = 300,
            BackgroundColor = Colors.Black
        };

        // Agregar la barra al contenedor
        BarContainer.Children.Add(nuevaBarra);
    }
}