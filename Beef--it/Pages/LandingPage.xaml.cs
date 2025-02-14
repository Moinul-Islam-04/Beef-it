using Microsoft.Maui.Controls;

namespace BeefIt
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage(string userName)
        {
            InitializeComponent();
            this.FindByName<Label>("TitleLabel").Text = $"Hello {userName}";
        }
        private void WorkoutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutEntryPage());
        }
        private async void ProgressPicButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProgressPicPage());
        }

        private async void NutritionButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NutritionPage());
        }
    }
}
