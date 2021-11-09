using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SlipNTrip
{
    public class DeviceControlsPage : ContentPage
    {
        private Patient patient;

        private Label testLabel;
        private Entry testEntry;

        private Label directionLabel;
        private Entry directionEntry;
        private Picker directionPicker;

        private Label distanceLabel;
        private Entry distanceEntry;
        private Picker distancePicker;

        private Label velocityLabel;
        private Entry velocityEntry;
        private Picker velocityPicker;

        private Button runButton;
        private Button returnHome;

        public DeviceControlsPage()
        {
            string[] directionInputs = { "Forward", "Backward" };
            string[] distanceInputs = { "3.0", "6.0", "9.0", "12.0", "15.0" };
            string[] velocityInputs = { "15.0", "18.0", "21.0", "24.0", "27.0", "30.0", "33.0", "35.0" };

            this.Title = "Device Controls";

            ToolbarItem helpToolbarItem = new ToolbarItem
            {
                Text = "?",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            helpToolbarItem.Clicked += helpButtonClicked;
            this.ToolbarItems.Add(helpToolbarItem);

            StackLayout stackLayout = new StackLayout();

            testLabel = new Label();
            testLabel.Text = "Test Name";
            testLabel.FontSize = 24;
            stackLayout.Children.Add(testLabel);
            testEntry = new Entry();
            testEntry.Placeholder = "Test #1";
            stackLayout.Children.Add(testEntry);

            directionLabel = new Label();
            directionLabel.Text = "Direction";
            directionLabel.FontSize = 24;
            stackLayout.Children.Add(directionLabel);
            directionEntry = new Entry();
            stackLayout.Children.Add(directionEntry);
            directionEntry.IsVisible = true;
            directionPicker = new Picker();
            directionPicker.Title = "Direction";
            foreach (string direction in directionInputs)
            {
                directionPicker.Items.Add(direction);
            }
            stackLayout.Children.Add(directionPicker);
            directionPicker.IsVisible = false;

            distanceLabel = new Label();
            distanceLabel.Text = "Distance";
            distanceLabel.FontSize = 24;
            stackLayout.Children.Add(distanceLabel);
            distanceEntry = new Entry();
            distanceEntry.Keyboard = Keyboard.Numeric;
            stackLayout.Children.Add(distanceEntry);
            distanceEntry.IsVisible = true;
            distancePicker = new Picker();
            distancePicker.Title = "Distance";
            foreach (string distance in distanceInputs)
            {
                distancePicker.Items.Add(distance);
            }
            stackLayout.Children.Add(distancePicker);
            distancePicker.IsVisible = false;

            velocityLabel = new Label();
            velocityLabel.Text = "Velocity";
            velocityLabel.FontSize = 24;
            stackLayout.Children.Add(velocityLabel);
            velocityEntry = new Entry();
            velocityEntry.Keyboard = Keyboard.Numeric;
            stackLayout.Children.Add(velocityEntry);
            velocityEntry.IsVisible = true;
            velocityPicker = new Picker();
            velocityPicker.Title = "Velocity";
            foreach (string velocity in velocityInputs)
            {
                velocityPicker.Items.Add(velocity);
            }
            stackLayout.Children.Add(velocityPicker);
            velocityPicker.IsVisible = false;

            runButton = new Button();
            runButton.Text = "Run";
            runButton.Clicked += runButtonClicked;
            stackLayout.Children.Add(runButton);

            returnHome = new Button();
            returnHome.Text = "Return to Home Page";
            returnHome.Clicked += returnHomeButtonClicked;
            stackLayout.Children.Add(returnHome);

            Content = stackLayout;
        }

        public DeviceControlsPage(Patient patient)
        {
            this.patient = patient; 

            string[] directionInputs = { "Forward", "Backward" };
            string[] distanceInputs = { "3.0", "6.0", "9.0", "12.0", "15.0" };
            string[] velocityInputs = { "15.0", "18.0", "21.0", "24.0", "27.0", "30.0", "33.0", "35.0" };

            this.Title = "Device Controls";

            ToolbarItem helpToolbarItem = new ToolbarItem
            {
                Text = "?",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            helpToolbarItem.Clicked += helpButtonClicked;
            this.ToolbarItems.Add(helpToolbarItem);

            StackLayout stackLayout = new StackLayout();

            testLabel = new Label();
            testLabel.Text = "Test Name";
            testLabel.FontSize = 24;
            stackLayout.Children.Add(testLabel);
            testEntry = new Entry();
            testEntry.Placeholder = "Test #1";
            stackLayout.Children.Add(testEntry);

            directionLabel = new Label();
            directionLabel.Text = "Direction";
            directionLabel.FontSize = 24;
            stackLayout.Children.Add(directionLabel);
            directionEntry = new Entry();
            stackLayout.Children.Add(directionEntry);
            directionEntry.IsVisible = true;
            directionPicker = new Picker();
            directionPicker.Title = "Direction";
            foreach(string direction in directionInputs)
            {
                directionPicker.Items.Add(direction);
            }
            stackLayout.Children.Add(directionPicker);
            directionPicker.IsVisible = false;

            distanceLabel = new Label();
            distanceLabel.Text = "Distance";
            distanceLabel.FontSize = 24;
            stackLayout.Children.Add(distanceLabel);
            distanceEntry = new Entry();
            distanceEntry.Keyboard = Keyboard.Numeric;
            stackLayout.Children.Add(distanceEntry);
            distanceEntry.IsVisible = true;
            distancePicker = new Picker();
            distancePicker.Title = "Distance";
            foreach (string distance in distanceInputs)
            {
                distancePicker.Items.Add(distance);
            }
            stackLayout.Children.Add(distancePicker);
            distancePicker.IsVisible = false;

            velocityLabel = new Label();
            velocityLabel.Text = "Velocity";
            velocityLabel.FontSize = 24;
            stackLayout.Children.Add(velocityLabel);
            velocityEntry = new Entry();
            velocityEntry.Keyboard = Keyboard.Numeric;
            stackLayout.Children.Add(velocityEntry);
            velocityEntry.IsVisible = true;
            velocityPicker = new Picker();
            velocityPicker.Title = "Velocity";
            foreach (string velocity in velocityInputs)
            {
                velocityPicker.Items.Add(velocity);
            }
            stackLayout.Children.Add(velocityPicker);
            velocityPicker.IsVisible = false;

            runButton = new Button();
            runButton.Text = "Run";
            runButton.Clicked += runButtonClicked;
            stackLayout.Children.Add(runButton);

            returnHome = new Button();
            returnHome.Text = "Return to Home Page";
            returnHome.Clicked += returnHomeButtonClicked;
            stackLayout.Children.Add(returnHome);

            Content = stackLayout;
        }

        async void runButtonClicked(object sender, EventArgs e)
        {
            if(distanceEntry.IsVisible)
            {
                if (!string.IsNullOrWhiteSpace(testEntry.Text) && !string.IsNullOrWhiteSpace(directionEntry.Text) && 
                    !string.IsNullOrWhiteSpace(distanceEntry.Text) && !string.IsNullOrWhiteSpace(velocityEntry.Text))
                {
                    TestResults testResults = new TestResults()
                    {
                        PatientName = patient.Name,
                        PatientID = patient.ID,
                        TestName = testEntry.Text,
                        Date = DateTime.Today,
                        Direction = directionEntry.Text,
                        Distance = double.Parse(distanceEntry.Text),
                        MotorSpeed = double.Parse(velocityEntry.Text),
                        StepTaken = false,
                        TimeBetweenStep = 0.0,
                        DistanceBetweenStep = 0.0
                    };

                    await Navigation.PushAsync(new Pages.TestResultPage(patient, testResults, false));
                }
                else
                    await DisplayAlert("Device Controls Error", "One or more fields missing information", "Done");
            }
            else
            {
                if (directionPicker.SelectedIndex != -1 && distancePicker.SelectedIndex != -1 && velocityPicker.SelectedIndex != -1)
                {
                    TestResults testResults = new TestResults()
                    {
                        PatientName = patient.Name,
                        TestName = testEntry.Text,
                        Date = DateTime.Now,
                        Direction = directionEntry.Text,
                        Distance = double.Parse(distanceEntry.Text),
                        MotorSpeed = double.Parse(velocityEntry.Text),
                        StepTaken = false,
                        TimeBetweenStep = 0.0,
                        DistanceBetweenStep = 0.0
                    };
                    await Navigation.PushAsync(new Pages.TestResultPage(patient, testResults, false));
                }
                else
                    await DisplayAlert("Device Controls Error", "One or more fields missing information", "Done");
            }
        }

        async void returnHomeButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PopAsync(new HomePage());
            await Navigation.PopAsync(true);
        }

        void helpButtonClicked(object sender, EventArgs e)
        {
            string helpMessage = "Direction: Forward/Backward\n" +
                "Distance: 0 cm to 15 cm\n" +
                "Velocity/Speed: 15cm/s to 35cm/s";
            DisplayAlert("Help - Device Controls Page", helpMessage, "Done");
        }
    }
}