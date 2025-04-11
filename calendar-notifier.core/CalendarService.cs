using Outlook = Microsoft.Office.Interop.Outlook;

namespace calendar_notifier.core
{
    public class CalendarService
    {
        // Simulate the calendar service for testing purposes
        bool simulated = false;

        public EventHandler<MeetingItem> OnNotifcation { get; set; } = null!;

        public CalendarService()
        {

        }

        public void ReadCalendarEvents()
        {
            try
            {
                if (!simulated)
                    ReadMeetingList();
                else
                    SimulateCalendarEvent();
            }
            catch (FileNotFoundException ex)
            {
                simulated = true;
                SimulateCalendarEvent();
            }

        }

        public List<MeetingItem> ReadMeetingList()
        {
            var outlookApp = new Outlook.Application();
            Outlook.NameSpace outlookNs = outlookApp.GetNamespace("MAPI");
            outlookNs.Logon("Outlook");

            Outlook.MAPIFolder calendar = outlookNs.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);


            var items = calendar.Items.Restrict($"[Start] >= '{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}' AND [Start] < '{DateTime.Today.AddDays(1).ToString("yyyy-MM-dd HH:mm")}'"); // Find items starting from now

            items.Sort("[Start]"); // Sort by start date


            var listMeetings = new List<MeetingItem>();

            foreach (Outlook.AppointmentItem item in items)
            {
                //skip canceled meetings
                if (item.MeetingStatus == Outlook.OlMeetingStatus.olMeetingCanceled)
                    continue;

                if (item.Start.TimeOfDay < DateTime.Now.TimeOfDay)
                    continue;

                MeetingItem meeting = new MeetingItem()
                {
                    Subject = item.Subject,
                    Start = item.Start,
                    StartHour = item.Start.TimeOfDay,
                    AppointmentId = item.GlobalAppointmentID
                };

                listMeetings.Add(meeting);

                Console.WriteLine($"{item.Subject} at {item.Start.TimeOfDay}");
            }

            listMeetings = listMeetings.OrderBy(x => x.StartHour).ToList();


            return listMeetings;

            //foreach (var meeting in listMeetings)
            //{
            //    OnNotifcation?.Invoke(this, meeting);
            //}
        }

        private void SimulateCalendarEvent()
        {
            var meeting = new MeetingItem()
            {
                Start = DateTime.Now.AddMinutes(5),
                Subject = "Simulated Meeting"
            };

            OnNotifcation?.Invoke(this, meeting);
        }
    }
}

