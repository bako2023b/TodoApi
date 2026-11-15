using System.Windows;
using System.Windows.Controls;
using TodoWpf.App.Models;
using TodoWpf.App.Services;

namespace TodoWpfApp.Views
{
    /// <summary>
    /// Interaction logic for PlacesView.xaml
    /// </summary>
    public partial class PlacesView : UserControl
    {
        private readonly ApiService _api;

        public PlacesView(ApiService api)
        {
            InitializeComponent();
            _api = api;
            LoadData();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtNewName.Text))
            {
                await _api.CreatePlaceAsync(new PlaceDto { Name = TxtNewName.Text });
                TxtNewName.Clear();
                LoadData();
            }
        }

        private async void LoadData()
        {
            DgPlaces.ItemsSource = await _api.GetPlacesAsync();
        }
    }
}