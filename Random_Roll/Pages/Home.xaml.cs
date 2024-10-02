using iNKORE.UI.WPF.Modern.Controls;
using Random_Roll.Classes;
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
            if (count < 3)
            {
                Count.Maximum = 1;
            }
            else
            {
                Count.Maximum = Database.GetCount() - 1;
            }
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
                Statistics.Text = "本次抽选共" + Count.Text + "/" + Database.GetCount() + "人";
                NameBlock.Text = string.Empty;
                foreach (string name in rolledName)
                {
                    NameBlock.Text += name;
                    NameBlock.Text += " ";
                }
            }
            else
            {
                Statistics.Text = "本次抽选共0/0人";
                NameBlock.Text = "请到设置>管理添加姓名";
            }
        }
    }
}
