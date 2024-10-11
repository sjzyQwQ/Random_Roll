using Microsoft.Data.Sqlite;
using Random_Roll.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                iNKORE.UI.WPF.Modern.Controls.ListViewItem newPerson = new iNKORE.UI.WPF.Modern.Controls.ListViewItem
                {
                    Tag = dataReader["guid"].ToString().Replace("-", "_"),
                    Content = dataReader["name"]
                };
                Names.Items.Add(newPerson);
            }
            await Database.connection.CloseAsync();
        }

        // 删除
        private async void DeletePerson_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (iNKORE.UI.WPF.Modern.Controls.ListViewItem item in Names.SelectedItems)
            {
                Database.DeletePerson(item.Tag.ToString().Replace("_", "-"));
            }
            Names.Items.Clear();
            await ListPerson();
        }

        // 全选
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            Names.SelectAll();
        }

        // 刷新列表
        private async void Refresh_Click(object sender, RoutedEventArgs e)
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
            foreach (string name in NewPerson_Name.Text.Split(Environment.NewLine))
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

        // 在未选择项目时禁用删除
        private void Names_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeletePerson_Button.IsEnabled = Convert.ToBoolean(Names.SelectedItems.Count);
            DeletePerson_Context.IsEnabled = Convert.ToBoolean(Names.SelectedItems.Count);
        }

        // 解决因SimpleStackPanel(ui)嵌套ListView(ui)和TextBox导致滚轮事件失效的问题
        private void UI_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
