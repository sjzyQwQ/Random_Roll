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

            Settings.CreateSettingsFile();
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
            if (Settings.GetSettings().ConfirmBeforeClosing)
            {
                e.Cancel = true;
                MessageBoxResult messageBox = iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("确定关闭吗？点击“否”以最小化", "随机点名器", MessageBoxButton.YesNoCancel);
                if (messageBox == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                    Application.Current.Shutdown();
                }
                else if (messageBox == MessageBoxResult.No)
                {
                    WindowState = WindowState.Minimized;
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationView_Root.SelectedItem = NavigationViewItem_Home;
            DataContext = Settings.GetSettings();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                floatingWindow.Show();
                Hide();
            }
        }
    }
}