namespace Beef__it
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnNavigateButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlaceholderPage());
        }


        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Get entered values
            string enteredUsername = UsernameEntry.Text?.Trim();
            string enteredPassword = PasswordEntry.Text?.Trim();

            // Define test credentials
            string correctUsername = "test";
            string correctPassword = "test";

            // Check if credentials match
            if (enteredUsername == correctUsername && enteredPassword == correctPassword)
            {
                // Navigate to LandingPage if login is successful
                await Navigation.PushAsync(new LandingPage(enteredUsername));
            }
            else
            {
                // Do nothing (or optionally show an error message)
                await DisplayAlert("Login Failed", "Incorrect username or password.", "OK");
            }
        }
    }

}
