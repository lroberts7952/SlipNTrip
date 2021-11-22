using SlipNTrip.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SlipNTrip
{
    public class HomePage : ContentPage
    {
        private Label welcomeMessage;
        private Label instructions;
        private Button addPatientButton;
        private Button existingPatientsButton;
        private Button deviceControlsButton;
        private Button logoutButton;

        public HomePage()
        {
            ListofPatients listofPatients = new ListofPatients();
            listofPatients.GenerateListofPatients();

            ToolbarItem helpToolbarItem = new ToolbarItem
            {
                Text = "?",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            helpToolbarItem.Clicked += helpButtonClicked;
            this.ToolbarItems.Add(helpToolbarItem);

            StackLayout stackLayout = new StackLayout();
            
            this.Title = "Home";

            /*welcomeMessage = new Label();
            welcomeMessage.Text = "Welcome to Defying Gravity's Slip N Trip App. The app accepts and store/access patient information, test results" +
                "and input parameters required to control the Slip N Trip Device. ";*/
            
            instructions = new Label();
            instructions.Text = "Select Option:";
            instructions.FontSize = 24;
            stackLayout.Children.Add(instructions);
            
            addPatientButton = new Button();
            addPatientButton.Text = "Add Patient";
            addPatientButton.Clicked += OnAddPatientButtonClicked;
            stackLayout.Children.Add(addPatientButton);

            existingPatientsButton = new Button();
            existingPatientsButton.Text = "Existing Patients";
            existingPatientsButton.Clicked += OnExistingPatientsButtonClicked;
            stackLayout.Children.Add(existingPatientsButton);

            deviceControlsButton = new Button();
            deviceControlsButton.Text = "Device Controls";
            deviceControlsButton.Clicked += OnDeviceControlsButtonClicked;
            stackLayout.Children.Add(deviceControlsButton);

            logoutButton = new Button();
            logoutButton.Text = "Logout/Exit";
            stackLayout.Children.Add(logoutButton);

            Content = stackLayout;

        }

        async void OnAddPatientButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPatientPage());
        }

        async void OnExistingPatientsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExistingPatientsPage());
        }

        async void OnDeviceControlsButtonClicked(object sender, EventArgs e)
        {
            Patient patient = new Patient
            {
                PatientID = "N/A",
                Name = "N/A",
                Gender = "N/A",
                Age = 0,
                Height = 0.0,
                Weight = 0.0,
                ShoeSize = 0.0
            };
            await Navigation.PushAsync(new DeviceControlsPage(patient));
        }

        void helpButtonClicked(object sender, EventArgs e)
        {
            string helpMessage = "Add Patient: Add new patient into database\n" +
                "Existing Patients: View patients added into database\n" +
                "Device Controls: Access the input parameters for device without needing a patient (Used for maintenance/testing)";
            DisplayAlert("Help - Home Page", helpMessage, "Done");
        }
    }
}