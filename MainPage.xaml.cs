using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Proyecto_Grupo3
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<string> Items { get; set; }
        private ObservableCollection<string> OriginalItems { get; set; } // Guarda la lista original

        public MainPage()
        {
            InitializeComponent();
            Items = new ObservableCollection<string>();
            OriginalItems = new ObservableCollection<string>();
            ItemsListView.ItemsSource = Items;
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            string newItem = NewItemEntry.Text.Trim();

            if (!string.IsNullOrWhiteSpace(newItem))
            {
                Items.Add(newItem);
                OriginalItems.Add(newItem); // Guardar el original para búsqueda
                NewItemEntry.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Advertencia", "Por favor, ingrese un texto para agregar.", "OK");
            }
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            string searchText = SearchEntry.Text?.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                ItemsListView.ItemsSource = Items; // Mostrar lista completa si el campo está vacío
            }
            else
            {
                var filteredItems = OriginalItems.Where(item => item.ToLower().Contains(searchText)).ToList();
                ItemsListView.ItemsSource = new ObservableCollection<string>(filteredItems);
            }
        }
    }
}