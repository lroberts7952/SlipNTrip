using SlipNTrip.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using System.Net;
using System.Net.Sockets;
using Xamarin.Forms;

namespace SlipNTrip.Pages
{
    public class RunPage : ContentPage
    {
        private Patient patient;
        private TestResults testResults;
        private bool buttonLayout;
        private object _udpClient;

        public RunPage(Patient patient, TestResults testResults, bool buttonLayout)
        {
            this.patient = patient;
            this.testResults = testResults;
            this.buttonLayout = buttonLayout;

            Title = "Test in Progress.....";
            StackLayout stackLayout = new StackLayout();

            ToolbarItem helpToolbarItem = new ToolbarItem
            {
                Text = "?",
                Order = ToolbarItemOrder.Primary,
                Priority = 1
            };
            helpToolbarItem.Clicked += helpToolbarItemClicked;
            this.ToolbarItems.Add(helpToolbarItem);

            ImageButton emergancyStopButton = new ImageButton
            {
                Source = "Emergency_Stop.png",
                HorizontalOptions = LayoutOptions.Center
            };
            emergancyStopButton.Clicked += emergencyStopClicked;

            navigateToTestPage();
            stackLayout.Children.Add(emergancyStopButton);
            Content = stackLayout;
        }

        async void helpToolbarItemClicked(object sender, EventArgs e)
        {
            string helpMessage = "Purpose: Shows test is in progress & displays emergency stop\n" + 
                "Page will automatically navigate to test results page when test is complete\n" +
                "Emergency Stop will navigate back to device controls page";
            await DisplayAlert("Help - Test in progress.....", helpMessage, "Done");
        }

        async void emergencyStopClicked(object sender, EventArgs e)
        {
            using (var client = new UdpClient())
            {
                client.EnableBroadcast = true;
                var endpoint = new IPEndPoint(IPAddress.Broadcast, 4210);
                var message = Encoding.ASCII.GetBytes("-999");
                await client.SendAsync(message, message.Length, endpoint);
                client.Close();
            }
            await DisplayAlert("Emergency Stop", "Emergency Stop Engaged", "Done");
            //await Navigation.PushAsync(new TestResultPage(patient, testResults, false)); // For testing
            await Navigation.PushAsync(new DeviceControlsPage(patient));
        }
        
        async void navigateToTestPage()
        {
            UdpClient _udpClient = new UdpClient(4210);
            string message;
            do
            {
                var result = await _udpClient.ReceiveAsync();
                message = Encoding.ASCII.GetString(result.Buffer);
                //Console.WriteLine(message);
            } while (message.Length == 0);

            if(message.Contains("-999"))
            {
                await DisplayAlert("Emergency Stop", "Manual Emergency Stop Engaged", "Done");
                await Navigation.PushAsync(new DeviceControlsPage(patient));
            }
            else
            {
                await Navigation.PushAsync(new TestResultPage(patient, testResults, false));
            }
        }


    }
}