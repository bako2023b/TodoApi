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

namespace TodoWpfApp.Windows
{
    /// <summary>
    /// Interaction logic for TodoEditorWindow.xaml
    /// </summary>
    public partial class TodoEditorWindow : Window
    {
        private readonly ApiService _api;
        private readonly int? _editingTodoId;
        
        public TodoEditorWindow(ApiService api, TodoItemDto? todoToEdit = null!)
        {
            InitializeComponent();
            _api = api;

            LoadComboBoxes(todoToEdit);

            if (todoToEdit != null)
            {
                _editingTodoId = todoToEdit.Id;
                TxtName.Text = todoToEdit.Name;
                ChkIsCompleted.IsChecked = todoToEdit.IsCompleted;
            }
        }

        private async void LoadComboBoxes(TodoItemDto? todoToEdit)
        {
            var members = await _api.GetMembersAsync();
            var places = await _api.GetPlacesAsync();

            CmbMember.ItemsSource = members;
            CmbPlace.ItemsSource = places;

            if (todoToEdit != null) 
            {
                CmbMember.SelectedValue = members.FirstOrDefault(m => m.Name == todoToEdit.Member)?.Id;
                CmbPlace.SelectedValue = places.FirstOrDefault(p => p.Name == todoToEdit.Place)?.Id;
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            var payload = new TodoSavePayload
            {
                Name = TxtName.Text,
                IsCompleted = ChkIsCompleted.IsChecked ?? false,
                Member = CmbMember.SelectedValue is int mId ? new IdRef { Id = mId } : null,
                Place = CmbPlace.SelectedValue is int pId ? new IdRef { Id = pId } : null
            };

            if (_editingTodoId.HasValue)
            {
                await _api.UpdateTodoAsync(_editingTodoId.Value, payload);
            }
            else
            {
                await _api.CreateTodoAsync(payload);
            }

            DialogResult = true;
        }
    }
}
