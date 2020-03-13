using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace NotificationsUWPDemo
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void showToastRichVisualContent()
        {
            XmlDocument doc = new XmlDocument();
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///ToastTemplates/ToastRichVisualContent.xml", UriKind.Absolute));
            IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read);
            XDocument xmldoc = XDocument.Load(readStream.AsStreamForRead());
            var toastTemplate = xmldoc.ToString();
            doc.LoadXml(toastTemplate);
            var toast = new ToastNotification(doc);
            //Set how long notification should be visible:
            //toast.ExpirationTime = DateTime.Now.AddSeconds(2);
            var toastNotificationManager = ToastNotificationManager.CreateToastNotifier();
            toastNotificationManager.Show(toast);
        }

        private void ToastRichVisualContentButton_Click(object sender, RoutedEventArgs e)
        {
            showToastRichVisualContent();
        }

        private async void showToastWithActionSample1()
        {
            XmlDocument doc = new XmlDocument();
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///ToastTemplates/ToastWithActionSample1.xml", UriKind.Absolute));
            IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read);
            XDocument xmldoc = XDocument.Load(readStream.AsStreamForRead());
            var toastTemplate = xmldoc.ToString();
            doc.LoadXml(toastTemplate);
            var toast = new ToastNotification(doc);
            var toastNotificationManager = ToastNotificationManager.CreateToastNotifier();
            toastNotificationManager.Show(toast);
        }

        private void ToastWithActionSample1Button_Click(object sender, RoutedEventArgs e)
        {
            showToastWithActionSample1();
        }

        private async void showToastWithActionSample2()
        {
            XmlDocument doc = new XmlDocument();
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///ToastTemplates/ToastWithActionSample2.xml", UriKind.Absolute));
            IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read);
            XDocument xmldoc = XDocument.Load(readStream.AsStreamForRead());
            var toastTemplate = xmldoc.ToString();
            doc.LoadXml(toastTemplate);
            var toast = new ToastNotification(doc);
            var toastNotificationManager = ToastNotificationManager.CreateToastNotifier();
            toastNotificationManager.Show(toast);
        }

        private void ToastWithActionSample2Button_Click(object sender, RoutedEventArgs e)
        {
            showToastWithActionSample2();
        }

        private async void showToastWithTextInputAndActions()
        {
            XmlDocument doc = new XmlDocument();
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///ToastTemplates/ToastWithTextInputAndActions.xml", UriKind.Absolute));
            IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read);
            XDocument xmldoc = XDocument.Load(readStream.AsStreamForRead());
            var toastTemplate = xmldoc.ToString();
            doc.LoadXml(toastTemplate);
            var toast = new ToastNotification(doc);
            var toastNotificationManager = ToastNotificationManager.CreateToastNotifier();
            toastNotificationManager.Show(toast);
        }

        private void ToastWithTextInputAndActionsButton_Click(object sender, RoutedEventArgs e)
        {
            showToastWithTextInputAndActions();
        }

        private async void showToastReminderNotification()
        {
            XmlDocument doc = new XmlDocument();
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///ToastTemplates/ToastReminderNotification.xml", UriKind.Absolute));
            IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read);
            XDocument xmldoc = XDocument.Load(readStream.AsStreamForRead());
            var toastTemplate = xmldoc.ToString();
            doc.LoadXml(toastTemplate);
            var toast = new ToastNotification(doc);
            var toastNotificationManager = ToastNotificationManager.CreateToastNotifier();
            toastNotificationManager.Show(toast);
        }

        private void ToastReminderNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            showToastReminderNotification();
        }
    }
}
