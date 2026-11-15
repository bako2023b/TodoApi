using System.Windows;
using System.Windows.Controls;
using TodoWpf.App.Services;
using TodoWpf.App.Models;

namespace TodoWpfApp.Views
{
    /// <summary>
    /// Interaction logic for MembersView.xaml
    /// </summary>
    public partial class MembersView : UserControl
    {
        private readonly ApiService _api;
        public MembersView(ApiService api)
        {
            InitializeComponent();
            _api = api;
            LoadData();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtNewName.Text))
            {
                await _api.CreateMemberAsync(new MemberDto { Name = TxtNewName.Text });
                TxtNewName.Clear();
                LoadData();
            }
        }

        private async void LoadData()
        {
            DgMembers.ItemsSource = await _api.GetMembersAsync();
        }
    }
}