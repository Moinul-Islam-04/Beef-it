namespace MauiApp1;
using MauiApp1.Services;
public partial class CreateAccountPage : ContentPage
{
	public CreateAccountPage()
	{
		InitializeComponent();
	}

	private static Dictionary<string, string> existingAccounts = new Dictionary<string, string>();

    private async void CreateAccountButtonClicked(object sender, EventArgs e)
    {
        string username = newUsernameEntry.Text?.Trim();
        string password = newPasswordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Please enter both a username and password.", "OK");
            return;
        }

        if (AccountManager.IsUsernameTaken(username))
        {
            await DisplayAlert("Error", "Username already exists. Choose another.", "OK");
        }
        else
        {
            AccountManager.AddAccount(username, password);
            await DisplayAlert("Success", $"Account '{username}' created!", "OK");

            // Navigate back to the login page
            await Navigation.PopAsync();
        }
    }
    private async void BackButtonClicked(object sender, EventArgs e)
	{ 
		await Navigation.PushAsync(new MainPage());
	}
}