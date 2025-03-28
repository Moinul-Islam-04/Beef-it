using Microsoft.Maui.Controls.Xaml; // Add this using directive
using Microsoft.Maui.Controls;
using System;
using Beef__it.Database;
using BCrypt.Net;

namespace Beef__it
{
    public partial class ForgotPasswordPage : ContentPage
    {
        private readonly UserAuthenticationService _authService;

        public ForgotPasswordPage()
        {
            InitializeComponent();
            _authService = new UserAuthenticationService();
        }

        private async void OnRequestResetClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();

            if (string.IsNullOrEmpty(username))
            {
                await DisplayAlert("Error", "Please enter your username.", "OK");
                return;
            }

            // Generate reset token (in a real app, this would be sent via email)
            string resetToken = await _authService.GeneratePasswordResetTokenAsync(username);
            if (resetToken == null)
            {
                await DisplayAlert("Error", "Username not found.", "OK");
                return;
            }

            // Show token display layout and populate the token
            ResetTokenDisplay.Text = resetToken;
            TokenDisplayLayout.IsVisible = true;
        }

        private async void OnCopyTokenClicked(object sender, EventArgs e)
        {
            // Copy token to clipboard
            await Clipboard.SetTextAsync(ResetTokenDisplay.Text);
            await DisplayAlert("Copied", "Reset token copied to clipboard", "OK");
        }

        private async void OnResetPasswordClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();
            string resetToken = ResetTokenEntry.Text?.Trim();
            string newPassword = NewPasswordEntry.Text?.Trim();

            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(resetToken) ||
                string.IsNullOrEmpty(newPassword))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            bool success = await _authService.ResetPasswordAsync(username, resetToken, newPassword);

            if (success)
            {
                await DisplayAlert("Success", "Password reset successfully.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to reset password. Check your details.", "OK");
            }
        }
    }

}