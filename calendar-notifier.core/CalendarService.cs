using Outlook = Microsoft.Office.Interop.Outlook;

namespace calendar_notifier.core
{
    public class CalendarService
    {
        // Simulate the calendar service for testing purposes
        bool simulated = false;


        public EventHandler<Outlook.AppointmentItem> OnNotifcation { get; set; } = null!;

        public CalendarService()
        {

        }

        public void ReadCalendarEvents()
        {
            try
            {
                if (!simulated)
                    ReadFromOutlook();
                else
                    SimulateCalendarEvent();
            }
            catch (FileNotFoundException ex)
            {
                simulated = true;
                SimulateCalendarEvent();
            }

        }


        private void ReadFromOutlook()
        {
            var outlookApp = new Outlook.Application();
            Outlook.NameSpace outlookNs = outlookApp.GetNamespace("MAPI");
            outlookNs.Logon();

            Outlook.MAPIFolder calendar = outlookNs.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);

            var items = calendar.Items.Restrict("[Start] >= Now()"); // Find items starting from now

            items.Sort("[Start]"); // Sort by start date

            foreach (Outlook.AppointmentItem item in items)
            {
                //skip canceled meetings
                if (item.MeetingStatus == Outlook.OlMeetingStatus.olMeetingCanceled)
                    continue;

                Console.WriteLine($"{item.Subject} at {item.Start}");

                OnNotifcation?.Invoke(this, item);
                // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
             
            }
        }

        private void SimulateCalendarEvent()
        {
            OnNotifcation?.Invoke(this, null);
        }
    }
}

