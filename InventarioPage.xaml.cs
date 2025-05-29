using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace Proyecto_Grupo3
{
    public partial class InventarioPage : ContentPage
    {
        public InventarioPage(ObservableCollection<string> productos)
        {
            InitializeComponent();
            InventarioListView.ItemsSource = productos;
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(); // Cierra la ventana
        }
    }
}