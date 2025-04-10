using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calendar_notifier.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SmallNotificationWindow wnd = null;
        public MainWindow()
        {
            InitializeComponent();
            btnShowNotification.Click += BtnShowNotification_Click;
        }

        private void BtnShowNotification_Click(object sender, RoutedEventArgs e)
        {

            if (wnd == null)
            {
                wnd = new SmallNotificationWindow();
                wnd.Show();
            }

            wnd.AddNotification("Test Notification", "This is a test notification");

        }
    }
}