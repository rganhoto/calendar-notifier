using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using calendar_notifier.core;

namespace calendar_notifier.wpf
{
    public class MeetingItemDP : DependencyObject
    {
        public MeetingItemDP()
        {
            //this.DataContext = this;
        }
        public MeetingItemDP(MeetingItem item)
        {
            this.Subject = item.Subject;
            this.Start = item.Start;
        }
        public DateTime Start { get; set; } = DateTime.Now;


        public string Subject
        {
            get { return (string)GetValue(SubjectProperty); }
            set { SetValue(SubjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Subject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubjectProperty =
            DependencyProperty.Register("Subject", typeof(string), typeof(MeetingItemDP), new PropertyMetadata(""));


        //public string SubTitle { get; set; }
        public string SubTitle
        {
            get { return (string)GetValue(SubTitleProperty); }
            set { SetValue(SubTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubTitleProperty =
            DependencyProperty.Register("SubTitle", typeof(string), typeof(MeetingItemDP), new PropertyMetadata(""));



    }
}
