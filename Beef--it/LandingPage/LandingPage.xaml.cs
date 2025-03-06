using Microsoft.Maui.Controls;

namespace Beef__it
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage() {

             InitializeComponent();
        }
        public LandingPage(string Username) : this() {
           
            this.FindByName<Label>("TitleLabel").Text = $"Hello {Username}";
        }
        private async void WorkoutButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new WorkoutEntryPage());
        }
        private async void NutritionButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NutritionPage());
        }
        private async void ProgressPicButton_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new ProgressPicPage());
        }

        private async void CalendarButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage());
        }
    }
}
