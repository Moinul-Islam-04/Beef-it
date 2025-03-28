using Microsoft.Maui.Controls;
using System;

namespace Beef__it
{
    public partial class WorkoutEntryPage : ContentPage
    {
        public WorkoutEntryPage()
        {
            InitializeComponent();
        }
        private async void AllExercisesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllExercisesPage());
        }
        private async void MyExercisesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyExercisesEntryPage());
        }
        private async void HistoryButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutHistoryEntryPage());
        }
    }
}
