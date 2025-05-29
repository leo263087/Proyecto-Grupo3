
using System.Collections.ObjectModel; 
using Microsoft.Maui.Controls;


namespace Proyecto_Grupo3
{
  
        public partial class MainPage : ContentPage
        {
            // ObservableCollection notifica a la UI automáticamente sobre cambios en la colección
            public ObservableCollection<string> Items { get; set; }

            public MainPage()
            {
                InitializeComponent();
                Items = new ObservableCollection<string>();
                ItemsListView.ItemsSource = Items; // Asigna la colección a la ListView
            }

            private void OnAddClicked(object sender, EventArgs e)
            {
                string newItem = NewItemEntry.Text.Trim(); // Obtener el texto y quitar espacios en blanco

                if (!string.IsNullOrWhiteSpace(newItem)) // Verificar que el texto no esté vacío o solo espacios
                {
                    Items.Add(newItem); // Agregar el nuevo elemento a la colección
                    NewItemEntry.Text = string.Empty; // Limpiar la entrada de texto
                }
                else
                {
                    // Opcional: Mostrar una alerta si el campo está vacío
                    DisplayAlert("Advertencia", "Por favor, ingrese un texto para agregar.", "OK");
                }
            }

            private void OnDeleteClicked(object sender, EventArgs e)
            {
                if (ItemsListView.SelectedItem != null) // Verificar si hay un elemento seleccionado
                {
                    // Cast el SelectedItem al tipo esperado (string en este caso)
                    string selectedItem = ItemsListView.SelectedItem as string;

                    if (selectedItem != null)
                    {
                        Items.Remove(selectedItem); // Eliminar el elemento de la colección
                        ItemsListView.SelectedItem = null; // Deseleccionar el elemento en la ListView
                    }
                }
                else
                {
                    // Opcional: Mostrar una alerta si no hay elemento seleccionado
                    DisplayAlert("Advertencia", "Por favor, seleccione un elemento para eliminar.", "OK");
                }
            }
        }
    }