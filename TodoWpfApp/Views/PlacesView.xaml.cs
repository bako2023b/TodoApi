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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoWpfApp.Services;

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
                await _api.CreatePlaceAsync(new Models.PlaceDto { Name = TxtNewName.Text });
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
