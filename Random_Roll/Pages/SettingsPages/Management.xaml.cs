using iNKORE.UI.WPF.Modern.Controls;
using Microsoft.Data.Sqlite;
using Random_Roll.Classes;
using System.Windows;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace Random_Roll.Pages.SettingsPages
{
    /// <summary>
    /// Management.xaml 的交互逻辑
    /// </summary>
    public partial class Management : Page
    {
        public Management()
        {
            InitializeComponent();
        }

        private async void Management_Loaded(object sender, RoutedEventArgs e)
        {
            if (Names.Items.Count == 0)
            {
                await ListPerson();
            }
        }

        private async Task ListPerson()
        {
            await Database.connection.OpenAsync();
            SqliteCommand command = new SqliteCommand(@"SELECT * FROM person", Database.connection);
            var dataReader = await command.ExecuteReaderAsync();
            while (await dataReader.ReadAsync())
            {
                ListViewItem newPerson = new ListViewItem
                {
                    Tag = dataReader["guid"].ToString().Replace("-", "_"),
                    Content = dataReader["name"]
                };
                Names.Items.Add(newPerson);
            }
            await Database.connection.CloseAsync();
        }

        // 删除
        private async void DeletePerson_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (ListViewItem item in Names.SelectedItems)
            {
                Database.DeletePerson(item.Tag.ToString().Replace("_", "-"));
            }
            Names.Items.Clear();
            await ListPerson();

        }

        // 全选
        private void SelectAll_Button_Click(object sender, RoutedEventArgs e)
        {
            Names.SelectAll();
        }

        // 刷新姓名列表
        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            Names.Items.Clear();
            await ListPerson();
        }
    }
}
