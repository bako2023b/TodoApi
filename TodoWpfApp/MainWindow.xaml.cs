using System.Text;
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
using TodoWpfApp.Views;

namespace TodoWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService = new();
        public MainWindow()
        {
            InitializeComponent();
            NavigateTo("Todos");
        }

        private void Navigate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string viewName)
            {
                NavigateTo(viewName);
            }
        }

        private void NavigateTo(string viewName)
        {
            MainContent.Content = viewName switch
            {
                "Todos" => new TodosView(_apiService),
                "Members" => new MembersView(_apiService),
                "Places" => new PlacesView(_apiService),
                _ => null
            };
        }
    }
}