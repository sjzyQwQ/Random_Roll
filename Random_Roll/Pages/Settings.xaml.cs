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
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private void NavigationView_Settings_SelectionChanged(iNKORE.UI.WPF.Modern.Controls.NavigationView sender, iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            var item = sender.SelectedItem;
            Page? page = null;

            if (item == NavigationViewItem_Settings_Management)
            {
                page = Page_Management;
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
