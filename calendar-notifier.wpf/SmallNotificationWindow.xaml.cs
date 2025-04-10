using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace calendar_notifier.wpf
{
    /// <summary>
    /// Interaction logic for SmallNotificationWindow.xaml
    /// </summary>
    public partial class SmallNotificationWindow : Window
    {

        public IList<object> Notifications { get; set; } = new ObservableCollection<object>() { new object(), new object() };

        public SmallNotificationWindow()
        {
            InitializeComponent();
            this.closeButton.Click += CloseButton_Click;

            this.DataContext = this;
        }



        public void AddNotification(string title, string message)
        {
            // Create a new notification item
            Notifications.Add("TESTE");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
