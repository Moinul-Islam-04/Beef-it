using Microsoft.Maui.Controls.Xaml; // Add this using directive
using Microsoft.Maui.Controls;
using System;

namespace Beef__it
{
    public partial class CreateAccountPage : ContentPage
    {
        private static Dictionary<string, string> accounts = new Dictionary<string, string>();

        public CreateAccountPage()
        {
            InitializeComponent();
        }

        private async void CreateAccountButtonClicked(object sender, EventArgs e)
        {
            string username = newUsernameEntry.Text?.Trim();
            string password = newPasswordEntry.Text?.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both a username and password.", "OK");
                return;
            }

            // Simple account creation without AccountManager
            if (accounts.ContainsKey(username))
            {
                await DisplayAlert("Error", "Username already exists. Choose another.", "OK");
            }
            else
            {
                accounts[username] = password;
                await DisplayAlert("Success", $"Account '{username}' created!", "OK");
                // Navigate back to the login page
                await Navigation.PopAsync();
            }
        }

        private async void BackButtonClicked(object sender, EventArgs e)
        {
            // Simply go back to the previous page
            await Navigation.PopAsync();
        }
    }
}