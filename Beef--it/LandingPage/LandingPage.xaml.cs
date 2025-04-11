using Beef__it.Database;
using Microsoft.Maui.Controls;

namespace Beef__it
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage() {
             InitializeComponent();
             InitUserGreeting();
        }
        public async void InitUserGreeting()
        {
        string username = Preferences.Get("LoggedInUsername", null);

        if (string.IsNullOrEmpty(username))
        {
            await DisplayAlert("Error", "No user logged in.", "OK");
            return;
        }

        var userRepo = new UserRepository();
        var user = await userRepo.GetUserByUsernameAsync(username);

        if (user == null)
        {
            await DisplayAlert("Error", "User not found.", "OK");
            return;
        }

        this.FindByName<Label>("TitleLabel").Text = $"Hello {user.FirstName}";
        }

        private async void WorkoutButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new WorkoutEntryPage());
        }
        private async void NutritionButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NutritionPage());
        }
        /*private async void ProgressPicButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new ProgressPicPage());
        }*/
        private async void CalendarButton_Clicked(object sender, EventArgs e)
        {
            var calendarPage = App.Current.Services.GetRequiredService<CalendarPage>();
            await Navigation.PushAsync(calendarPage);
        }
    }
}
