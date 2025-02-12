using Microsoft.Maui.Controls;
using System;

namespace BeefIt
{
    public partial class MyExercisesEntryPage : ContentPage
    {
        public MyExercisesEntryPage()
        {
            InitializeComponent();
        }
        private async void SaveWorkout_Clicked(object sender, EventArgs e)
        {
            DateTime selectedDate = ExerciseDatePicker.Date;
            string workoutDetails = WorkoutDetailsEditor.Text;
            await DisplayAlert("Workout Saved",
                $"Workout for {selectedDate.ToString("D")} saved!\nDetails: {workoutDetails}",
                "OK");
        }
    }
}
