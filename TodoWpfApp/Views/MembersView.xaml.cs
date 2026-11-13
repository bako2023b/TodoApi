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
using TodoWpfApp.Services;

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
                await _api.CreateMemberAsync(new Models.MemberDto { Name = TxtNewName.Text });
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
