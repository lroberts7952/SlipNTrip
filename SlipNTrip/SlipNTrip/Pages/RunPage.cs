using SlipNTrip.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SlipNTrip.Pages
{
    public class RunPage : ContentPage
    {
        private Patient patient;
        private TestResults testResults;
        private bool buttonLayout;

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

            //navigateToTestPage();
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
            await DisplayAlert("Emergency Stop", "Emergency Stop Engaged", "Done");
            await Navigation.PushAsync(new TestResultPage(patient, testResults, false));
            //await Navigation.PushAsync(new DeviceControlsPage(patient));
        }
        
        async void navigateToTestPage()
        {
            int i = 0;
            while(i < 1000)
            {

            }
            await Navigation.PushAsync(new TestResultPage(patient, testResults, false));
        }
    }
}