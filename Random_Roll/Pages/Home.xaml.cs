using iNKORE.UI.WPF.Modern.Controls;
using Random_Roll.Classes;
using Random_Roll.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
            roll = new Roll();
        }

        // 避免手动输入小数（四舍五入）
        private void Count_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            if (Count.Value % 1 != 0)
            {
                Count.Value = Math.Round(Count.Value);
            }
        }

        Roll roll;

        // 开始抽选
        private void Start_Roll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Database.GetCount() != 0)
            {
                List<Person> rolledPerson = roll.Start(Count.Value);
                Statistic.Text = $"本次抽选共{Count.Text}/{Database.GetCount()}人";
                NamePanel.Children.Clear();
                if (!Classes.Settings.GetSettings().EnableAvatar)
                {
                    foreach (Person person in rolledPerson)
                    {
                        NamePanel.Children.Add(new TextBlock
                        {
                            Text = person.Name,
                            FontSize = 36,
                            FontWeight = FontWeights.Bold,
                            Margin = new Thickness(0, 0, 8, 0)
                        });
                    }
                }
                else
                {
                    foreach (Person person in rolledPerson)
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(System.IO.File.Exists($"Avatars/{person.Guid}.png") ? $"Avatars/{person.Guid}.png" : "pack://application:,,,/Random_Roll;component/Assets/defaultAvatar.png", UriKind.RelativeOrAbsolute);
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        NamePanel.Children.Add(new PersonCard { Avatar = bitmapImage, Name = person.Name, Margin = new Thickness(0, 0, 8, 8) });
                        bitmapImage.Freeze();
                    }
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
