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

        // 刷新列表
        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            Names.Items.Clear();
            await ListPerson();
        }

        // 新增
        private void NewPerson_Button_Click(object sender, RoutedEventArgs e)
        {
            NewPerson_Button.Visibility = Visibility.Collapsed;
            NewPerson_Name.Visibility = Visibility.Visible;
            NewPerson_Save_Button.Visibility = Visibility.Visible;
            NewPerson_Cancel_Button.Visibility = Visibility.Visible;
        }

        // 保存
        private async void NewPerson_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            NewPerson_Save_Button.Visibility = Visibility.Collapsed;
            NewPerson_Cancel_Button.Visibility = Visibility.Collapsed;
            NewPerson_Name.Visibility = Visibility.Collapsed;
            NewPerson_Button.Visibility = Visibility.Visible;
            foreach(string name in NewPerson_Name.Text.Split(Environment.NewLine))
            {
                Database.NewPerson(name);
            }
            NewPerson_Name.Text = string.Empty;
            Names.Items.Clear();
            await ListPerson();
        }

        // 取消
        private void NewPerson_Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NewPerson_Save_Button.Visibility = Visibility.Collapsed;
            NewPerson_Cancel_Button.Visibility = Visibility.Collapsed;
            NewPerson_Name.Visibility = Visibility.Collapsed;
            NewPerson_Button.Visibility = Visibility.Visible;
            NewPerson_Name.Text = string.Empty;
        }
    }
}
