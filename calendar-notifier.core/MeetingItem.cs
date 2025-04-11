using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar_notifier.core
{
    public class MeetingItem
    {
        public string Subject { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan StartHour { get; set; }
        public string AppointmentId { get; internal set; }
    }
}
