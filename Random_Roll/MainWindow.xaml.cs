using iNKORE.UI.WPF.Modern.Controls;
using Random_Roll.Classes;
using System.ComponentModel;
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

            Database.CreateTable_person();
            Database.CreateTable_statistic();
        }

        #region NavigationView_Root
        public Pages.Home Page_Home = new Pages.Home();
        public Pages.Statistic Page_Statistic = new Pages.Statistic();
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
            else if (item == NavigationViewItem_Statistic)
            {
                page = Page_Statistic;
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
        #endregion

        FloatingWindow floatingWindow = new FloatingWindow();

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationView_Root.SelectedItem = NavigationViewItem_Home;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                floatingWindow.Show();
                this.Hide();
            }
        }
    }
}