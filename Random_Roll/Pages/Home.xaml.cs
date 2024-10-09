using iNKORE.UI.WPF.Modern.Controls;
using Random_Roll.Classes;
using System.Windows;
using System.Windows.Controls;
using Page = iNKORE.UI.WPF.Modern.Controls.Page;

namespace Random_Roll.Pages
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            int count = Database.GetCount();
            Count.Maximum = count < 2 ? 1 : count - 1;
        }

        // 避免手动输入小数（四舍五入）
        private void Count_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            if (Count.Value % 1 != 0)
            {
                Count.Value = Math.Round(Count.Value);
            }
        }

        // 开始抽选
        private void Start_Roll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Database.GetCount() != 0)
            {
                Roll roll = new Roll();
                List<string> rolledName = roll.Start(Count.Value);
                Statistic.Text = "本次抽选共" + Count.Text + "/" + Database.GetCount() + "人";
                NamePanel.Children.Clear();
                foreach (string name in rolledName)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = name;
                    textBlock.FontSize = 36;
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.Margin = new Thickness(0, 0, 8, 0);
                    NamePanel.Children.Add(textBlock);
                }
            }
            else
            {
                Statistic.Text = "本次抽选共0/0人";
                Tip.Text = "请到设置>管理添加姓名";
            }
        }
    }
}
