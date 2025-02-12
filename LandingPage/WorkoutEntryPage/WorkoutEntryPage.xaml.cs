using Microsoft.Maui.Controls;
using System;

namespace BeefIt
{
    public partial class WorkoutEntryPage : ContentPage
    {
        public WorkoutEntryPage()
        {
            InitializeComponent();
        }
        private async void AllExercisesButton_Clicked(object sender, EventArgs e)
        {
            
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
