using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;
using Xamarin.Forms;

namespace SlipNTrip
{
    public class TestPage : ContentPage
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "testResults.db3");

        private Patient patient;
        private TestResults testResults = new TestResults();
        private Button newTest;
        private ListView testResultsListView;
        
        public TestPage(Patient patient)
        {
            var db = new SQLiteConnection(dbPath);

            this.patient = patient;
            this.Title = "Test";

            ToolbarItem helpToolbarItem = new ToolbarItem
            {
                Text = "?",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            helpToolbarItem.Clicked += helpButtonClicked;
            this.ToolbarItems.Add(helpToolbarItem);

            StackLayout stackLayout = new StackLayout();

            SearchBar searchBar = new SearchBar { Placeholder = "Search items..." };
            //searchBar.TextChanged += OnTextChanged;
            stackLayout.Children.Add(searchBar);

            newTest = new Button();
            newTest.Text = "New Test";
            newTest.Clicked += newTestButtonClicked;
            stackLayout.Children.Add(newTest);

            var tableInfo = db.GetTableInfo("TestResults");
            if(tableInfo.Count > 0)
            {
                testResultsListView = new ListView();
                testResultsListView.ItemsSource = db.Table<TestResults>().Where(x => x.PatientID == patient.ID).ToList();
                testResultsListView.ItemSelected += listView_ItemSelected;
                stackLayout.Children.Add(testResultsListView);
            }
            Content = stackLayout;
        }

        async void newTestButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeviceControlsPage(patient));
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            testResults = (TestResults)e.SelectedItem;
            Navigation.PushAsync(new Pages.TestResultPage(patient, testResults, true));
        }

        void helpButtonClicked(object sender, EventArgs e)
        {
            string helpMessage = "Purpose: To access patients test results by selecting a test name\n" +
                "New Test: Navigates to device controls page to input required information needed for device movements" +
                "and generate the pertubation"; //Check spelling
            DisplayAlert("Help - View Test Results", helpMessage, "Done");
        }
    }
}