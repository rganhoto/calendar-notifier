// See https://aka.ms/new-console-template for more information

using System.Reflection;
using calendar_notifier.core;
using Microsoft.Toolkit.Uwp.Notifications;

var calendarWorker = new CalendarWorker();

calendarWorker.OnNotification += (sender, item) =>
{
    new ToastContentBuilder()
       .AddText(item.Item1.Subject)
       .AddText(item.Item1.StartHour.ToString("hh':'mm"))
       .AddAudio(new Uri("ms-winsoundevent:Notification.Looping.Call6"))
       .Show();
};

calendarWorker.MeetingSummary += (sender, item) =>
{
    var bld = new ToastContentBuilder()
       .AddText("Today's Meetings")
       .AddAudio(new Uri("ms-winsoundevent:Notification.Looping.Call6"));

    foreach (var str in item)
    {
        bld.AddText(str);
    }

    bld.Show();
};


Console.ForegroundColor = ConsoleColor.Green;

Console.WriteLine("Welcome to Calendar Notifier");
Console.WriteLine($"v{Assembly.GetExecutingAssembly().GetName().Version}");
Console.WriteLine();

Console.ResetColor();


calendarWorker.Start(true);

Console.ReadLine();


calendarWorker.Stop();
