using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SlipNTrip.Pages
{
    public class RunPage : ContentPage
    {
        public RunPage()
        {
            Title = "Test in Progress.....";
            StackLayout stackLayout = new StackLayout();

            Label testInProgress = new Label();
            testInProgress.Text = "Test in Progress.....";
            testInProgress.FontSize = 50;
            stackLayout.Children.Add(testInProgress);

            ImageButton emergancyStopButton = new ImageButton
            {
                Source = "Emergency_Stop.png",
                //HeightRequest = 100,
                //WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            stackLayout.Children.Add(emergancyStopButton);
            Content = stackLayout;
        }
    }
}