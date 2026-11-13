using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TodoWpfApp.Models;
using TodoWpfApp.Services;
using TodoWpfApp.Windows;

namespace TodoWpfApp.Views
{
    /// <summary>
    /// Interaction logic for TodosView.xaml
    /// </summary>
    public partial class TodosView : UserControl
    {
        private readonly ApiService _api;
        public TodosView(ApiService api)
        {
            InitializeComponent();
            _api = api;
            LoadData();
        }
        private async void LoadData()
        {
            DgTodos.ItemsSource = await _api.GetTodosAsync();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var win = new TodoEditorWindow(_api);
            if (win.ShowDialog() == true) LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (DgTodos.SelectedItem is TodoItemDto selected)
            {
                var win = new TodoEditorWindow(_api, selected);
                if (win.ShowDialog() == true) LoadData();
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DgTodos.SelectedItem is TodoItemDto selected &&
                MessageBox.Show($"Biztosan törlöd a(z) '{selected.Name}' feladatot?", "Törlés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _api.DeleteTodoAsync(selected.Id);
                LoadData();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
