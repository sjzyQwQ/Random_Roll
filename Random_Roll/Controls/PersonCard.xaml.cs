using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Random_Roll.Controls
{
    /// <summary>
    /// PersonCard.xaml 的交互逻辑
    /// </summary>
    public partial class PersonCard : UserControl
    {
        public PersonCard()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static DependencyProperty dependencyProperty_Avatar = DependencyProperty.Register("Avatar", typeof(ImageSource), typeof(PersonCard));
        public static DependencyProperty dependencyProperty_Name = DependencyProperty.Register("Name", typeof(string), typeof(PersonCard));

        public ImageSource Avatar
        {
            get { return (ImageSource)GetValue(dependencyProperty_Avatar); }
            set { SetValue(dependencyProperty_Avatar, value); }
        }

        public string Name
        {
            get { return (string)GetValue(dependencyProperty_Name); }
            set { SetValue(dependencyProperty_Name, value); }
        }
    }
}
