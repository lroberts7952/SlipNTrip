/* 
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
        private Entry usernameEntry;
        private Entry passwordEntry;
        private Button loginButton;

        public LoginPage()
        {
            StackLayout stackLayout = new StackLayout();
            //this.Title = "Slip N Trip App";

            var image = new Image {
                Source = "Defying_Gravity_Logo.png"
            };

            stackLayout.Children.Add(image);

            usernameEntry = new Entry();
            usernameEntry.Placeholder = "Username";
            stackLayout.Children.Add(usernameEntry);

            passwordEntry = new Entry();
            passwordEntry.Placeholder = "Password";
            stackLayout.Children.Add(passwordEntry);

            loginButton = new Button();
            loginButton.Text = "Login";
            loginButton.CornerRadius = 5;
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