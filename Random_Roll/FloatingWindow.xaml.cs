using Random_Roll.Classes;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Random_Roll
{
    /// <summary>
    /// FloatingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FloatingWindow : Window
    {
        public FloatingWindow()
        {
            InitializeComponent();

            this.Top = SystemParameters.PrimaryScreenHeight - 192;
            this.Left = SystemParameters.PrimaryScreenWidth - 128;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (Settings.GetSettings().ConfirmBeforeClosing)
            {
                e.Cancel = true;
                MessageBoxResult messageBox = iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("确定关闭随机点名器吗？", "随机点名器", MessageBoxButton.OKCancel);
                if (messageBox == MessageBoxResult.OK)
                {
                    e.Cancel = false;
                    Application.Current.Shutdown();
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        #region MouseEvent
        private Point initialPosition;
        private bool isDragging;

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            initialPosition = e.GetPosition(this);
            isDragging = false;
        }

        private void Window_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!isDragging)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        window.Show();
                        window.WindowState = WindowState.Normal;
                        Hide();
                    }
                }
            }
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPosition = e.GetPosition(this);
                if (Math.Abs(currentPosition.X - initialPosition.X) > 8 || Math.Abs(currentPosition.Y - initialPosition.Y) > 8)
                {
                    isDragging = true;
                    DragMove();
                }
            }
        }
        #endregion
    }
}
