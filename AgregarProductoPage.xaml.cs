namespace Proyecto_Grupo3;
    public partial class AgregarProductoPage : ContentPage
    {

      public Producto NuevoProducto { get; set; }

      public Action<Producto> ProductoAgregadoCallback { get; set; }

        public AgregarProductoPage()
        {
            InitializeComponent();
        }

       
        private async void OnConfirmarClicked(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(IdEntry.Text) ||
                string.IsNullOrWhiteSpace(NombreEntry.Text) ||
                string.IsNullOrWhiteSpace(CategoriaEntry.Text) || 
                string.IsNullOrWhiteSpace(DescripcionEntry.Text) ||
                !int.TryParse(CantidadEntry.Text, out int cantidad) ||
                !float.TryParse(PrecioUnitarioEntry.Text, out float precioUnitario)) 
            {
                await DisplayAlert("Error de validación", "Por favor, complete todos los campos y asegúrese de que 'Cantidad' y 'Precio unitario' sean números válidos.", "OK");
                return; 
            }

            //Aca intente como incorporar la categoria en esta parte del codigo
            Categoria categoriaSeleccionada = new Categoria(CategoriaEntry.Text, $"Categoría de {CategoriaEntry.Text}");

            
            NuevoProducto = new Producto
            {
                ID = IdEntry.Text,
                Nombre = NombreEntry.Text,
                Categoria = categoriaSeleccionada, // Asigna la categoría creada
                Descripcion = DescripcionEntry.Text,
                Cantidad = cantidad,
                Precio = precioUnitario
            };

         
            NuevoProducto.AgregarPropiedadEspecifica("Fecha de Ingreso", DateTime.Now.ToShortDateString());


            
            ProductoAgregadoCallback?.Invoke(NuevoProducto);

            
            await Navigation.PopAsync();
        }

        private async void OnCerrarClicked(object dreadful, EventArgs e)
        {
            await Navigation.PopAsync(); 
        }
    }
}