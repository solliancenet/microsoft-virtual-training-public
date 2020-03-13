using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationHubs
{
    //https://docs.microsoft.com/en-us/dotnet/api/overview/azure/notification-hubs?view=azure-dotnet
    class Program
    {
        static void Main(string[] args)
        {
            string hubConnectionString = Environment.GetEnvironmentVariable("HUB_CONNECTION_STRING"); ;
            string hubName = Environment.GetEnvironmentVariable("HUB_NAME"); ;

            MainAsync(hubConnectionString, hubName).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string hubConnectionString, string hubName)
        {
            NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(hubConnectionString, hubName);
            string toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">Hello from a .NET App!</text></binding></visual></toast>";
            await hub.SendWindowsNativeNotificationAsync(toast);
            
            //hub.SendAdmNativeNotificationAsync;
            //hub.SendAppleNativeNotificationAsync;
            //hub.SendBaiduNativeNotificationAsync;
            //hub.SendMpnsNativeNotificationAsync;
        }
    }
}