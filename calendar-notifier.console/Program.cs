// See https://aka.ms/new-console-template for more information

using calendar_notifier.core;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Foundation.Collections;
using Outlook = Microsoft.Office.Interop.Outlook;

Console.WriteLine("Hello, World!");



// Initialize any required services or configurations here
// Listen to notification activation
ToastNotificationManagerCompat.OnActivated += toastArgs =>
{
    // Obtain the arguments from the notification
    ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

    // Obtain any user input (text boxes, menu selections) from the notification
    ValueSet userInput = toastArgs.UserInput;

    // Need to dispatch to UI thread if performing UI operations
    //Application.Current.Dispatcher.Invoke(delegate
    //{
    //    // TODO: Show the corresponding content
    //    MessageBox.Show("Toast activated. Args: " + toastArgs.Argument);
    //});
};


var calendarService = new CalendarService();

calendarService.OnNotifcation += (sender, item) =>
{
    if (item != null)
    {
        // Create a toast notification
        new ToastContentBuilder()
            .AddText(item.Subject)
            .AddText(item.Start.ToString())
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", item.ConversationIndex.ToString())
            .Show();
    }
    else
    {    // Create a toast notification
        new ToastContentBuilder()
            .AddText("ISTO é UM TESTE")
            .AddText("MAS QUE GRANDE TESTE")
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId")
            .Show();
    }

};

calendarService.ReadCalendarEvents();


Console.ReadLine();
