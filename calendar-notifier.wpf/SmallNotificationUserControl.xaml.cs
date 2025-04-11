using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using calendar_notifier.core;

namespace calendar_notifier.wpf
{
    /// <summary>
    /// Interaction logic for SmallNotificationUserControl.xaml
    /// </summary>
    public partial class SmallNotificationUserControl : UserControl
    {
        public SmallNotificationUserControl()
        {
            InitializeComponent();
            closeButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MeetingItemDP item)
            {
                item.Subject = "Clicou";
                item.SubTitle = "Clicou no botão";
            }
        }
    }
}
