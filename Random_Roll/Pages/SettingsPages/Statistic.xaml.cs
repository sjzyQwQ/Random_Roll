using Random_Roll.Classes;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace Random_Roll.Pages.SettingsPages
{
    /// <summary>
    /// Statistic.xaml 的交互逻辑
    /// </summary>
    public partial class Statistic : Page
    {
        public Statistic()
        {
            InitializeComponent();
        }

        private void ComfirmClearStatistic_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ClearStatistic_Button.IsEnabled = true;
        }

        private void ComfirmClearStatistic_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ClearStatistic_Button.IsEnabled = false;
        }

        private void ClearStatistic_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Database.ClearTable_Statistic();
        }
    }
}
