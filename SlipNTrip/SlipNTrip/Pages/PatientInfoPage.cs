using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;

using Xamarin.Forms;

namespace SlipNTrip
{
    public class PatientInfoPage : ContentPage
    {
        string dbPatientPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "patients.db3");
        string dbTestResultsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "testResults.db3");

        private Patient patient;

        private Label patientIDLabel;
        private Label nameLabel;
        private Label genderLabel;
        private Label ageLabel;
        private Label heightLabel;
        private Label weightLabel;
        private Label ShoeSizeLabel;

        private Entry patientIDEntry;
        private Entry nameEntry;
        private Entry genderEntry;
        private Entry ageEntry;
        private Entry heightEntry;
        private Entry weightEntry;
        private Entry shoeSizeEntry;

        private Button updateButton;
        private Button testButton;
        private Button deleteButton;

        public PatientInfoPage(Patient patient)
        {
            this.patient = patient;
            this.Title = patient.PatientID + ": " + patient.Name;

            ToolbarItem helpToolbarItem = new ToolbarItem
            {
                Text = "?",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            helpToolbarItem.Clicked += helpButtonClicked;
            this.ToolbarItems.Add(helpToolbarItem);

            StackLayout stackLayout = new StackLayout();

            patientIDLabel = new Label();
            patientIDLabel.Text = "Patient ID";
            patientIDLabel.FontSize = 24;
            stackLayout.Children.Add(patientIDLabel);
            patientIDEntry = new Entry();
            patientIDEntry.Placeholder = "M_000";
            patientIDEntry.Text = patient.PatientID;
            stackLayout.Children.Add(patientIDEntry);

            nameLabel = new Label();
            nameLabel.Text = "Name";
            nameLabel.FontSize = 24;
            stackLayout.Children.Add(nameLabel);
            nameEntry = new Entry();
            nameEntry.Placeholder = "Jane Doe";
            nameEntry.Text = patient.Name;
            stackLayout.Children.Add(nameEntry);

            genderLabel = new Label();
            genderLabel.Text = "Gender";
            genderLabel.FontSize = 24;
            stackLayout.Children.Add(genderLabel);
            genderEntry = new Entry();
            genderEntry.Placeholder = "Female";
            genderEntry.Text = patient.Gender;
            stackLayout.Children.Add(genderEntry);

            ageLabel = new Label();
            ageLabel.Text = "Age";
            ageLabel.FontSize = 24;
            stackLayout.Children.Add(ageLabel);
            ageEntry = new Entry();
            ageEntry.Keyboard = Keyboard.Numeric;
            ageEntry.Placeholder = "23";
            ageEntry.Text = patient.Age.ToString();
            stackLayout.Children.Add(ageEntry);

            heightLabel = new Label();
            heightLabel.Text = "Height (ft.in)";
            heightLabel.FontSize = 24;
            stackLayout.Children.Add(heightLabel);
            heightEntry = new Entry();
            heightEntry.Keyboard = Keyboard.Numeric;
            heightEntry.Placeholder = "5.5";
            heightEntry.Text = patient.Height.ToString();
            stackLayout.Children.Add(heightEntry);

            weightLabel = new Label();
            weightLabel.Text = "Weight (lb)";
            weightLabel.FontSize = 24;
            stackLayout.Children.Add(weightLabel);
            weightEntry = new Entry();
            weightEntry.Keyboard = Keyboard.Numeric;
            weightEntry.Placeholder = "126";
            weightEntry.Text = patient.Weight.ToString();
            stackLayout.Children.Add(weightEntry);

            ShoeSizeLabel = new Label();
            ShoeSizeLabel.Text = "Shoe Size";
            ShoeSizeLabel.FontSize = 24;
            stackLayout.Children.Add(ShoeSizeLabel);
            shoeSizeEntry = new Entry();
            shoeSizeEntry.Keyboard = Keyboard.Numeric;
            shoeSizeEntry.Placeholder = "7";
            shoeSizeEntry.Text = patient.ShoeSize.ToString();
            stackLayout.Children.Add(shoeSizeEntry);

            updateButton = new Button();
            updateButton.Text = "Update";
            updateButton.Clicked += UpdateButtonClicked;
            stackLayout.Children.Add(updateButton);

            testButton = new Button();
            testButton.Text = "Test";
            testButton.Clicked += TestButtonClicked;
            stackLayout.Children.Add(testButton);

            deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.Clicked += DeleteButtonClicked;
            stackLayout.Children.Add(deleteButton);

            Content = stackLayout;
        }

        async void UpdateButtonClicked(object sender, EventArgs e)
        {
            this.Title = patientIDEntry.Text + ": " + nameEntry.Text;
            var db = new SQLiteConnection(dbPatientPath);
          
            if (!string.IsNullOrWhiteSpace(patientIDEntry.Text) && !string.IsNullOrWhiteSpace(nameEntry.Text) 
                && !string.IsNullOrWhiteSpace(ageEntry.Text) && !string.IsNullOrWhiteSpace(heightEntry.Text) 
                && !string.IsNullOrWhiteSpace(genderEntry.Text) && !string.IsNullOrWhiteSpace(weightEntry.Text) 
                && !string.IsNullOrWhiteSpace(shoeSizeEntry.Text))
            {
                Patient patient = new Patient()
                {
                    ID = this.patient.ID,
                    PatientID = patientIDEntry.Text,
                    Name = nameEntry.Text,
                    Gender = genderEntry.Text,
                    Age = int.Parse(ageEntry.Text),
                    Height = double.Parse(heightEntry.Text),
                    Weight = double.Parse(weightEntry.Text),
                    ShoeSize = double.Parse(shoeSizeEntry.Text)
                };

                db.Update(patient);
            }

            else
                await DisplayAlert("Patient Info", "One or more fields missing information", "Done");
        }

        async void DeleteButtonClicked(object sender, EventArgs e)
        {
            var dbPatient = new SQLiteConnection(dbPatientPath);
            var dbTestResults = new SQLiteConnection(dbTestResultsPath);
            bool response = await DisplayAlert("Delete: Patient", "Are you sure you would like to delete the patient?", "Yes", "No");
            if (response)
            {
                dbPatient.Table<Patient>().Delete(x => x.ID == this.patient.ID);
                dbTestResults.Table<TestResults>().Delete(x => x.PatientID == this.patient.ID);
                await Navigation.PopAsync();
            }
        }

        async void TestButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestPage(patient));
        }

        void helpButtonClicked(object sender, EventArgs e)
        {
            string helpMessage = "Purpose: To view the patients information\n" +
                "Update: Saves the information changed with patients information\n" +
                "Test: Navigates to page to view the list of test results\n" +
                "Delete: Removes patient information and test results from database";
            DisplayAlert("Help - Patient Infomation Page", helpMessage, "Done");
        }
    }
}