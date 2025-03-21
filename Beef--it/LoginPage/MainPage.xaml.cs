using System.Globalization;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using MauiApp1.Services;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private static Dictionary<string, string> existingAccounts = new Dictionary<string, string>();
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text?.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }

            // Validate login credentials using AccountManager
            if (AccountManager.ValidateUser(username, password))
            {
                await DisplayAlert("Success", "Login successful!", "OK");

                // Navigate to HomePage after successful login
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccountPage());
        }

        private bool IsValidUser(string username, string password)
        {
            return username == "admin" && password == "password123";
        }
    }

}
