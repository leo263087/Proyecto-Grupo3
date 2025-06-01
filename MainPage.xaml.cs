using System.Collections.ObjectModel;
using System.Linq; 
using Microsoft.Maui.Controls;
using System.Text.Json;
using static Proyecto_Grupo3.datos;


namespace Proyecto_Grupo3
{
    
    public partial class MainPage : ContentPage
    {
        
        private ObservableCollection<datos.Producto> Items { get; set; }
       

        public MainPage()
        {
            InitializeComponent();
            
            Items = new ObservableCollection<Producto>();
           
        }

        
        private async void OnNavigateButtonClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new categoria.NewPage1()); 
        }

       
        private async void OnAgregarProductoClicked(object sender, EventArgs e)
        {
            var agregarProductoPage = new AgregarProductoPage();

            
            agregarProductoPage.ProductoAgregadoCallback = (nuevoProducto) =>
            {
                if (nuevoProducto != null)
                {
                    Items.Add(nuevoProducto); 
                    DisplayAlert("Éxito", $"Producto '{nuevoProducto.Nombre}' agregado al inventario.", "OK");
                }
            };

           
            await Navigation.PushAsync(agregarProductoPage);
        }

        /*
        private void CargarDatosDePrueba()
        {
            Items.Add(new Producto("P001", "Laptop", "Electrónica", "Potente laptop para gaming", 5, 1200.00m));
            Items.Add(new Producto("P002", "Teclado Mecánico", "Periféricos", "Teclado RGB con switches Cherry MX", 10, 80.00m));
            Items.Add(new Producto("P003", "Mouse Inalámbrico", "Periféricos", "Mouse ergonómico para oficina", 20, 25.00m));
        }
        */
        
    }
}


