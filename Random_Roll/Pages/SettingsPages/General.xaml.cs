using System.Windows.Controls;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace Random_Roll.Pages.SettingsPages
{
    /// <summary>
    /// General.xaml 的交互逻辑
    /// </summary>
    public partial class General : Page
    {
        public General()
        {
            InitializeComponent();
        }

        Classes.Settings settings;

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            settings = Classes.Settings.GetSettings();
            DataContext = settings;
        }

        private void CheckBox_IsCheckedChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                CheckBox checkBox = sender as CheckBox;
                if (checkBox.Name == "AlwaysOnTop")
                {
                    settings.AlwaysOnTop = (bool)checkBox.IsChecked;
                }
                else if (checkBox.Name == "ConfirmBeforeClosing")
                {
                    settings.ConfirmBeforeClosing = (bool)checkBox.IsChecked;
                }
                else if (checkBox.Name == "EnableAvatar")
                {
                    settings.EnableAvatar = (bool)checkBox.IsChecked;
                }
                settings.SaveSettingsFile();
            }
        }
    }
}
