using System.Windows;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace Random_Roll.Pages
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        #region NavigationView_Settings
        public Pages.SettingsPages.Management Page_Management = new SettingsPages.Management();
        public Pages.SettingsPages.Statistic Page_Statistic = new SettingsPages.Statistic();

        private void NavigationView_Settings_SelectionChanged(iNKORE.UI.WPF.Modern.Controls.NavigationView sender, iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            var item = sender.SelectedItem;
            Page? page = null;

            if (item == NavigationViewItem_Settings_Management)
            {
                page = Page_Management;
            }
            else if (item == NavigationViewItem_Settings_Statistic)
            {
                page = Page_Statistic;
            }

            if (page != null)
            {
                NavigationView_Settings.Header = page.Title;
                Frame_Settings.Navigate(page);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationView_Settings.SelectedItem = NavigationViewItem_Settings_Management;
        }
        #endregion
    }
}
