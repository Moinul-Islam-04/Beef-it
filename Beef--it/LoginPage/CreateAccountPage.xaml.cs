using Microsoft.Maui.Controls.Xaml; // Add this using directive
using Microsoft.Maui.Controls;
using System;
using Beef__it.Database;
using BCrypt.Net;

namespace Beef__it
{
    public partial class CreateAccountPage : ContentPage
    {
        private readonly UserRepository _userRepository;

        public CreateAccountPage()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
        }

        private async void CreateAccountButtonClicked(object sender, EventArgs e)
        {
            string username = newUsernameEntry.Text?.Trim();
            string password = newPasswordEntry.Text?.Trim();

            // Validate all required fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both a username and password.", "OK");
                return;
            }

            // Parse additional user details with validation
            if (!int.TryParse(ageEntry.Text, out int age))
            {
                await DisplayAlert("Error", "Please enter a valid age.", "OK");
                return;
            }

            if (!double.TryParse(heightEntry.Text, out double height))
            {
                await DisplayAlert("Error", "Please enter a valid height.", "OK");
                return;
            }

            if (!double.TryParse(weightEntry.Text, out double weight))
            {
                await DisplayAlert("Error", "Please enter a valid weight.", "OK");
                return;
            }

            // Additional validation for age, height, and weight
            if (age < 0 || age > 120)
            {
                await DisplayAlert("Error", "Please enter a valid age between 0 and 120.", "OK");
                return;
            }

            if (height <= 0 || height > 300)
            {
                await DisplayAlert("Error", "Please enter a valid height (in cm).", "OK");
                return;
            }

            if (weight <= 0 || weight > 600)
            {
                await DisplayAlert("Error", "Please enter a valid weight (in kg).", "OK");
                return;
            }

            // Check if username exists
            if (await _userRepository.UserExistsAsync(username))
            {
                await DisplayAlert("Error", "Username already exists. Choose another.", "OK");
                return;
            }

            // Create UserDto with all details
            var userDto = new UserDto
            {
                Username = username,
                FirstName = firstNameEntry.Text?.Trim() ?? "",
                LastName = lastNameEntry.Text?.Trim() ?? "",
                Age = age,
                Height = height,
                Weight = weight
            };

            // Hash the password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // Convert DTO to User entity
            var newUser = userDto.ToEntity(passwordHash);

            // Add user to database
            bool success = await _userRepository.AddUserAsync(newUser);

            if (success)
            {
                await DisplayAlert("Success", $"Account '{username}' created!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to create account. Please try again.", "OK");
            }
        }

        private async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}