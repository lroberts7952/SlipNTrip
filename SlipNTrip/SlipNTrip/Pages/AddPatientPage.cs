using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Xamarin.Forms;
using SQLite;

namespace SlipNTrip
{
    public class AddPatientPage : ContentPage
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "patients.db3");

        private Label nameLabel;
        private Label genderLabel;
        private Label ageLabel;
        private Label heightLabel;
        private Label weightLabel;
        private Label ShoeSizeLabel;

        private Entry nameEntry;
        private Entry genderEntry;
        private Entry ageEntry;
        private Entry heightEntry;
        private Entry weightEntry;
        private Entry shoeSizeEntry;

        private Button saveButton;

        public AddPatientPage()
        {
            this.Title = "Add Patient";

            ToolbarItem helpToolbarItem = new ToolbarItem
            {
                Text = "?",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            helpToolbarItem.Clicked += helpButtonClicked;
            this.ToolbarItems.Add(helpToolbarItem);

            StackLayout stackLayout = new StackLayout();

            nameLabel = new Label();
            nameLabel.Text = "Name";
            nameLabel.FontSize = 24;
            stackLayout.Children.Add(nameLabel);
            nameEntry = new Entry();
            nameEntry.Placeholder = "Jane Doe";
            stackLayout.Children.Add(nameEntry);

            genderLabel = new Label();
            genderLabel.Text = "Gender";
            genderLabel.FontSize = 24;
            stackLayout.Children.Add(genderLabel);
            genderEntry = new Entry();
            genderEntry.Placeholder = "Female";
            stackLayout.Children.Add(genderEntry);

            ageLabel = new Label();
            ageLabel.Text = "Age";
            ageLabel.FontSize = 24;
            stackLayout.Children.Add(ageLabel);
            ageEntry = new Entry();
            ageEntry.Keyboard = Keyboard.Numeric;
            ageEntry.Placeholder = "23";
            stackLayout.Children.Add(ageEntry);

            heightLabel = new Label();
            heightLabel.Text = "Height (ft.in)";
            heightLabel.FontSize = 24;
            stackLayout.Children.Add(heightLabel);
            heightEntry = new Entry();
            heightEntry.Keyboard = Keyboard.Numeric;
            heightEntry.Placeholder = "5.5";
            stackLayout.Children.Add(heightEntry);

            weightLabel = new Label();
            weightLabel.Text = "Weight (lb)";
            weightLabel.FontSize = 24;
            stackLayout.Children.Add(weightLabel);
            weightEntry = new Entry();
            weightEntry.Keyboard = Keyboard.Numeric;
            weightEntry.Placeholder = "126";
            stackLayout.Children.Add(weightEntry);

            ShoeSizeLabel = new Label();
            ShoeSizeLabel.Text = "Shoe Size";
            ShoeSizeLabel.FontSize = 24;
            stackLayout.Children.Add(ShoeSizeLabel);
            shoeSizeEntry = new Entry();
            shoeSizeEntry.Keyboard = Keyboard.Numeric;
            shoeSizeEntry.Placeholder = "7";
            stackLayout.Children.Add(shoeSizeEntry);

            saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Clicked += OnButtonClicked;
            stackLayout.Children.Add(saveButton);

            Content = stackLayout;

        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Patient>();

            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(genderEntry.Text) 
                && !string.IsNullOrWhiteSpace(ageEntry.Text) && !string.IsNullOrWhiteSpace(heightEntry.Text)
                && !string.IsNullOrWhiteSpace(weightEntry.Text) && !string.IsNullOrWhiteSpace(shoeSizeEntry.Text))
            {
                Patient patient = new Patient()
                {
                    Name = nameEntry.Text,
                    Gender = genderEntry.Text,
                    Age = int.Parse(ageEntry.Text),
                    Height = double.Parse(heightEntry.Text),
                    Weight = double.Parse(weightEntry.Text),
                    ShoeSize = double.Parse(shoeSizeEntry.Text)
                };

                db.Insert(patient);

                await Navigation.PushAsync(new TestPage(patient));
            }
            else
                await DisplayAlert("Add Patient Error", "One or more fields missing information", "Done");
        }

        void helpButtonClicked(object sender, EventArgs e)
        {
            string helpMessage = "Input the patients name, age, gender, weight, height and shoe size\n" +
                "Gender: Female, Male, Other??\n" +
                "Weight: Measured in lb\n" +
                "Height: Measured in ft";
            DisplayAlert("Help - Add Patient Page", helpMessage, "Done");
        }
    } 
}