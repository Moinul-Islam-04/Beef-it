using Microsoft.Maui.Controls.Xaml; // Add this using directive
using Microsoft.Maui.Controls;
using System;

namespace Beef__it
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text?.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }

            // Replace this with direct validation if AccountManager doesn't exist
            if (IsValidUser(username, password)) // Using local method instead of AccountManager
            {
                await DisplayAlert("Success", "Login successful!", "OK");
                // Navigate to HomePage or create a LandingPage class
                Application.Current.MainPage = new NavigationPage(new LandingPage());

            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }

        // Local validation method to replace AccountManager.ValidateUser
        private bool IsValidUser(string username, string password)
        {
            // Simple validation logic - replace with your actual logic
            return username == "admin" && password == "password123";
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            // Make sure CreateAccountPage exists in your project
            await Navigation.PushAsync(new CreateAccountPage());
        }
    }
}