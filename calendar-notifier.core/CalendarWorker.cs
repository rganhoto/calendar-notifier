using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar_notifier.core
{
    public class CalendarWorker
    {
        public event EventHandler<Tuple<MeetingItem, NotificationType>> OnNotification = null!;
        public event EventHandler<List<string>> MeetingSummary = null!;

        Dictionary<string, MeetingItemWorkerProvider> providers = new Dictionary<string, MeetingItemWorkerProvider>();

        ManualResetEvent mre = new ManualResetEvent(false);


        //10m
        //5m
        //time of meeting



        //Read Meeting Threads
        public void Start(bool CreateMeetingSummary)
        {
            if (CreateMeetingSummary)
            {
                CalendarService cs = new CalendarService();
                var ml = cs.ReadMeetingList();

                List<string> meetingText = new List<string>();

                foreach (var meeting in ml)
                {
                    meetingText.Add($"{meeting.Start.ToString("HH:mm")} - {meeting.Subject}");
                }

                MeetingSummary?.Invoke(this, meetingText);

            }



            Thread T = new Thread(() =>
            {
                CalendarService cs = new CalendarService();
                while (true)
                {
                    var ml = cs.ReadMeetingList();

                    foreach (var meeting in ml)
                    {
                        if (DateTime.Now.AddMinutes(30).TimeOfDay > meeting.StartHour)
                        {
                            if (!providers.ContainsKey(meeting.AppointmentId))
                            {
                                StartMeetingThreads(meeting);
                            }
                        }
                    }
                    if (mre.WaitOne(0))
                        return;
                    if (mre.WaitOne(TimeSpan.FromMinutes(5)))
                        return;
                }
            });
            T.Start();

        }

        public void Stop()
        {
            mre.Set();
        }


        public void StartMeetingThreads(MeetingItem item)
        {
            MeetingItemWorkerProvider provider = new MeetingItemWorkerProvider();
            provider.Meeting = item;

            provider.AddThread(CreateAndStartThread(item, NotificationType.Now));
            provider.AddThread(CreateAndStartThread(item, NotificationType.FiveMinutes));
            provider.AddThread(CreateAndStartThread(item, NotificationType.TenMinutes));

            providers.Add(item.AppointmentId, provider);
        }


        private Thread CreateAndStartThread(MeetingItem item, NotificationType type)
        {
            TimeSpan? timeSpan = CalculateTimeForThread(item, type);

            if (timeSpan == null)
                return null;

            var thread = new Thread(() =>
            {
                try
                {
                    if (mre.WaitOne(0))
                        return;
                    if (mre.WaitOne(timeSpan.Value))
                        return;
                    Notify(item, type);
                }
                catch (ThreadAbortException) { }
            });
            thread.Start();
            return thread;
        }


        private TimeSpan? CalculateTimeForThread(MeetingItem item, NotificationType type)
        {
            if (DateTime.Now.AddMinutes((int)type).TimeOfDay < item.StartHour)
            {
                return item.StartHour - DateTime.Now.AddMinutes((int)type).TimeOfDay;
            }
            return null;
        }


        private void Notify(MeetingItem meeting, NotificationType notificationType)
        {
            OnNotification?.Invoke(this, Tuple.Create(meeting, notificationType));
        }
    }

    public enum NotificationType
    {
        Now = 0,
        FiveMinutes = 5,
        TenMinutes = 10,
    }

    public class MeetingItemWorkerProvider
    {
        public MeetingItem Meeting { get; set; }
        public List<Thread> Threads { get; set; } = new List<Thread>();

        public void AddThread(Thread t)
        {
            if (t != null)
                Threads.Add(t);
        }

    }
}
