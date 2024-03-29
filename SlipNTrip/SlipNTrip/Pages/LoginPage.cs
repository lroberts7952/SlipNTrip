﻿/* 
 * File: LoginPage.cs
 * Purpose: ....
 * Input(s): Username, Password
 * Output(s): N/A
 * Page Navigation(s): Home Page (File: HomePage.cs)
*/

using SlipNTrip.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SlipNTrip
{
    public class LoginPage : ContentPage
    {
        private Label defyingGravityLabel;
        private Label slipNTripLabel;
        private Label yearLabel;
        

        private Entry usernameEntry;
        private Entry passwordEntry;
        private Button loginButton;

        public LoginPage()
        {
            AttributeValues attributeValues = new AttributeValues();
            StackLayout stackLayout = new StackLayout();
            

            defyingGravityLabel = new Label
            {
                Text = "Defying Gravity",
                FontSize = attributeValues.getLabelFontSize(),
                HorizontalOptions = LayoutOptions.Center
            };
            stackLayout.Children.Add(defyingGravityLabel);

            var image = new Image {
                Source = "Defying_Gravity_Logo.png"
            };
            stackLayout.Children.Add(image);

            slipNTripLabel = new Label
            {
                Text = "Slip N Trip V2",
                FontSize = attributeValues.getLabelFontSize(),
                HorizontalOptions = LayoutOptions.Center
            };
            stackLayout.Children.Add(slipNTripLabel);

            yearLabel = new Label
            {
                Text = "2021",
                FontSize = attributeValues.getLabelFontSize(),
                HorizontalOptions = LayoutOptions.Center
            };
            stackLayout.Children.Add(yearLabel);

            usernameEntry = new Entry
            {
                Placeholder = "Username",
                FontSize = attributeValues.getEntryFontSize()
            };
            stackLayout.Children.Add(usernameEntry);

            passwordEntry = new Entry
            {
                Placeholder = "Password",
                FontSize = attributeValues.getEntryFontSize(),
                IsPassword = true
            };
            stackLayout.Children.Add(passwordEntry);

            loginButton = new Button
            {
                Text = "Login",
                FontSize = attributeValues.getLabelFontSize(),
                CornerRadius = 5,
                BorderWidth = attributeValues.getBorderWidth(),
                BorderColor = Color.DarkGray
            };
            loginButton.Clicked += OnLoginButtonClicked;
            stackLayout.Children.Add(loginButton);

            ConnectionAlert();
            Content = stackLayout;
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(usernameEntry.Text) && !string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                if(usernameEntry.Text.Equals("lmro239") && passwordEntry.Text.Equals("defyingGravity"))
                {
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Login Error", "Incorrect Username or Password", "Done");
                }
            }

            else
                await DisplayAlert("Login Error", "One or more fields missing information", "Done");
        }

        async void ConnectionAlert()
        {
            await DisplayAlert("Connection Error", "Check Wi-Fi Networ in Settings \n Correct Network: defying_gravity_AP \n Password: 123456789", "Done");
        }
    }
}