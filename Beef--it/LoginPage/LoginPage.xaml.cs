using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using System;
using Beef__it.Database;
using BCrypt.Net;

namespace Beef__it
{
    public partial class LoginPage : ContentPage
    {
        private readonly UserRepository _userRepository;

        public LoginPage()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
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

            // Validate user credentials against the database
            bool isValidUser = await ValidateUserCredentials(username, password);

            if (isValidUser)
            {
                // Store the logged-in username (you might want to use a more secure method in a real app)
                Preferences.Set("LoggedInUsername", username);

                await DisplayAlert("Success", "Login successful!", "OK");
                // Navigate to HomePage or LandingPage
                Application.Current.MainPage = new NavigationPage(new LandingPage());
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }

        private async Task<bool> ValidateUserCredentials(string username, string password)
        {
            try
            {
                // Find user by username
                var user = await _userRepository.GetUserByUsernameAsync(username);

                // If user not found, return false
                if (user == null)
                {
                    return false;
                }

                // Verify password using BCrypt
                return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            }
            catch (Exception)
            {
                // Log the exception in a real-world scenario
                return false;
            }
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccountPage());
        }
    }
}