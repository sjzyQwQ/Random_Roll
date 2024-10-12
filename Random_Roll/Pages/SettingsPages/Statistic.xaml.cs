using Random_Roll.Classes;
using System.Windows;
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

        private void ConfirmClearStatistic_IsCheckedChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            ClearStatistic_Button.IsEnabled = (bool)ConfirmClearStatistic.IsChecked;
        }

        private void ClearStatistic_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("清空后将无法恢复！", "确定要清空统计信息吗？", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Database.ClearTable_Statistic();
                ConfirmClearStatistic.IsChecked = false;
            }
        }
    }
}
