using iNKORE.UI.WPF.Modern.Controls;
using Microsoft.Data.Sqlite;
using Random_Roll.Classes;
using Random_Roll.Pages.SettingsPages;
using System.Windows;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace Random_Roll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (!System.IO.File.Exists(System.IO.Directory.GetCurrentDirectory() + "Databases.db"))
            {
                Database.CreateDatabase();
            }
        }

        #region NavigationView_Root
        public Pages.Home Page_Home = new Pages.Home();
        public Pages.Settings Page_Settings = new Pages.Settings();
        public Pages.About Page_About = new Pages.About();

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = sender.SelectedItem;
            Page? page = null;

            if (item == NavigationViewItem_Home)
            {
                page = Page_Home;
            }
            else if (item == NavigationViewItem_Settings)
            {
                page = Page_Settings;
            }
            else if (item == NavigationViewItem_About)
            {
                page = Page_About;
            }

            if (page != null)
            {
                NavigationView_Root.Header = page.Title;
                Frame_Main.Navigate(page);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationView_Root.SelectedItem = NavigationViewItem_Home;
        }
        #endregion
    }
}