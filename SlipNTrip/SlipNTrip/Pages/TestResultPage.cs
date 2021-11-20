using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;
using Xamarin.Forms;

namespace SlipNTrip.Pages
{
    public class TestResultPage : ContentPage
    {
        string dbTestResultsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "testResults.db3");

        private Patient patient;
        private TestResults testResults;
        private Boolean buttonLayout;

        private int arraySize = 11;
        private double[,] startingLocationArray;
        private double[,] endingLocationArray;

        private Label patientID;
        private Label patientName;
        private Label testName;
        private Label testDate;
        private Label patientGender;
        private Label patientAge;
        private Label patientHeight;
        private Label patientWeight;
        private Label patientShoeSize;
        private Label deviceDirection;
        private Label deviceDistance;
        private Label deviceVelocity;
        private Label stepTaken;
        private Label timeBetweenStep;
        private Label distanceBetweenStep;

        private Button steppingSurfaceGraphicButton;
        private Button saveButton;
        private Button newTestButton;
        private Button homeButton;
        private Button exportButton;
        private Button deleteButton;

        public TestResultPage(Patient patient, TestResults testResults, Boolean buttonLayout)
        {
            this.patient = patient;
            this.testResults = testResults;
            this.buttonLayout = buttonLayout;
            this.Title = patient.PatientID + ": " + patient.Name + " - " + testResults.TestName;

            startingLocationArray = new double[arraySize, arraySize];
            endingLocationArray = new double[arraySize, arraySize];
            generateArray(); // For Testing

            ToolbarItem helpToolbarItem = new ToolbarItem
            {
                Text = "?",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            helpToolbarItem.Clicked += helpButtonClicked;
            this.ToolbarItems.Add(helpToolbarItem);

            StackLayout stackLayout = new StackLayout();

            patientID = new Label();
            patientID.Text = "Patient ID: " + patient.ID;
            patientID.FontSize = 24;
            stackLayout.Children.Add(patientID);

            patientName = new Label();
            patientName.Text = "Patient Name: " + patient.Name;
            patientName.FontSize = 24;
            stackLayout.Children.Add(patientName);

            testName = new Label();
            testName.Text = "Test Name: " + testResults.TestName;
            testName.FontSize = 24;
            stackLayout.Children.Add(testName);

            testDate = new Label();
            testDate.Text = "Date: " + testResults.Date.ToString();
            testDate.FontSize = 24;
            stackLayout.Children.Add(testDate);

            patientGender = new Label();
            patientGender.Text = "Gender: " + patient.Gender;
            patientGender.FontSize = 24;
            stackLayout.Children.Add(testDate);

            patientAge = new Label();
            patientAge.Text = "Age: " + patient.Age.ToString();
            patientAge.FontSize = 24;
            stackLayout.Children.Add(patientAge);

            patientHeight = new Label();
            patientHeight.Text = "Height: " + patient.Height.ToString();
            patientHeight.FontSize = 24;
            stackLayout.Children.Add(patientHeight);

            patientWeight = new Label();
            patientWeight.Text = "Weight: " + patient.Weight.ToString();
            patientWeight.FontSize = 24;
            stackLayout.Children.Add(patientWeight);

            patientShoeSize = new Label();
            patientShoeSize.Text = "Shoe Size: " + patient.ShoeSize.ToString();
            patientShoeSize.FontSize = 24;
            stackLayout.Children.Add(patientShoeSize);

            deviceDirection = new Label();
            deviceDirection.Text = "Direction: " + testResults.Direction;
            deviceDirection.FontSize = 24;
            stackLayout.Children.Add(deviceDirection);

            deviceDistance = new Label();
            deviceDistance.Text = "Distance: " + testResults.Distance.ToString();
            deviceDistance.FontSize = 24;
            stackLayout.Children.Add(deviceDistance);

            deviceVelocity = new Label();
            deviceVelocity.Text = "Velocity/Speed: " + testResults.MotorSpeed.ToString();
            deviceVelocity.FontSize = 24;
            stackLayout.Children.Add(deviceVelocity);

            stepTaken = new Label();
            stepTaken.Text = "Was a step taken? " + testResults.WasAStepTaken();
            stepTaken.FontSize = 24;
            stackLayout.Children.Add(stepTaken);

            timeBetweenStep = new Label();
            timeBetweenStep.Text = "Time between steps: " + testResults.TimeBetweenStep.ToString();
            timeBetweenStep.FontSize = 24;
            stackLayout.Children.Add(timeBetweenStep);

            distanceBetweenStep = new Label();
            distanceBetweenStep.Text = "Distance between steps: " + testResults.DistanceBetweenStep.ToString();
            distanceBetweenStep.FontSize = 24;
            stackLayout.Children.Add(distanceBetweenStep);

            steppingSurfaceGraphicButton = new Button();
            steppingSurfaceGraphicButton.Text = "View Patient's Location on Device";
            steppingSurfaceGraphicButton.Clicked += steppingSurfaceGraphicButtonClicked;
            stackLayout.Children.Add(steppingSurfaceGraphicButton);

            saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Clicked += SaveButtonCLicked;
            stackLayout.Children.Add(saveButton);

            newTestButton = new Button();
            newTestButton.Text = "New Test";
            newTestButton.Clicked += NewTestButtonCLicked;
            stackLayout.Children.Add(newTestButton);

            exportButton = new Button();
            exportButton.Text = "Export";
            exportButton.Clicked += ExportButtonCLicked;
            stackLayout.Children.Add(exportButton);

            deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.Clicked += DeleteButtonCLicked;
            stackLayout.Children.Add(deleteButton);

            homeButton = new Button();
            homeButton.Text = "Return to Home Page";
            homeButton.Clicked += HomeButtonCLicked;
            stackLayout.Children.Add(homeButton);

            if (buttonLayout)
            {
                saveButton.IsVisible = false;
                newTestButton.IsVisible = false;
                deleteButton.IsVisible = true;
                homeButton.IsVisible = false;
            }
            else
            {
                saveButton.IsVisible = true;
                newTestButton.IsVisible = true;
                deleteButton.IsVisible = false;
                homeButton.IsVisible = true;
            }
            
            Content = stackLayout;
            //ScrollView scrollView = new ScrollView { Content = stackLayout };
        }

        async void steppingSurfaceGraphicButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.SSGraphicPage(startingLocationArray, endingLocationArray));
        }

        async void SaveButtonCLicked(object sender, EventArgs e)
        {
            var dbTestResults = new SQLiteConnection(dbTestResultsPath);
            dbTestResults.CreateTable<TestResults>();

            dbTestResults.Insert(testResults);

            await DisplayAlert("Save", "Results Sucessfully Saved", "Done");
        }

        async void NewTestButtonCLicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("New Test", "Did you save the results?", "Yes", "No");
            if(response)
            {
                await Navigation.PushAsync(new TestPage(patient));
            }
        }

        async void ExportButtonCLicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Export To?", "Cancel", null, "Text File", "Excel File", "PDF");
            if(action == "Test File")
            {
                //string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
                //File.WriteAllText(fileName, "Hello");
            }
            else if(action == "Excel File")
            {

            }
            else if(action == "PDF")
            {

            }
        }

        async void DeleteButtonCLicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dbTestResultsPath);
            bool response = await DisplayAlert("Delete: Test Result", "Are you sure you would like to delete the test result?", "Yes", "No");
            if (response)
            {
                db.Table<TestResults>().Delete(x => x.ID == this.testResults.ID);
                await Navigation.PopAsync();
            }
        }

        async void HomeButtonCLicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Return home", "Did you save the results?", "Yes", "No");
            if (response)
            {
                await Navigation.PushAsync(new HomePage());
            }
        }

        void helpButtonClicked(object sender, EventArgs e)
        {
            string helpMessage1 = "Purpose: To view the test results\n" +
                "The test results include patient information, device control information and data collected from the stepping surface\n" +
                "Save: Saves test results into database\n" +
                "New Test: Navigates to the test page\n" +
                "Export: Exports data into desired file format (text file, excel file, PDF)\n" +
                "Home Page: Navigates to home page";
            string helpMessage2 = "Purpose: To view the test results\n" +
                "The test results include patient information, device control information and data collected from the stepping surface\n" +
                "Export: Exports data into desired file format (text file, excel file, PDF)\n" +
                "Delete: Removes test results from database";
            if(buttonLayout)
            {
                DisplayAlert("Help - Test Results Page", helpMessage2, "Done");
            }
            else
            {
                DisplayAlert("Help - Test Results Page", helpMessage1, "Done");
            }
        }

        // For Testing
        private void generateArray()
        {
            Random rnd = new Random();
            for (int x = 0; x < arraySize; x++)
            {
                for (int y = 0; y < arraySize; y++)
                {
                    startingLocationArray[x, y] = rnd.Next(0, 1000);
                }
            }

            for (int x = 0; x < arraySize; x++)
            {
                for (int y = 0; y < arraySize; y++)
                {
                    endingLocationArray[x, y] = rnd.Next(0, 1000);
                }
            }
        }
    }
}