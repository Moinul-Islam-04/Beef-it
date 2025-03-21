using Microsoft.Maui.Controls;
using System;

namespace Beef__it
{
    public partial class MyExercisesEntryPage : ContentPage
    {
        public MyExercisesEntryPage()
        {
            InitializeComponent();
            ExerciseDatePicker.MaximumDate = DateTime.Now;
        }
        private async void SaveWorkout_Clicked(object sender, EventArgs e)
        {
            DateTime selectedDate = ExerciseDatePicker.Date;
            string workoutDetails = WorkoutDetailsEditor.Text;
            

            /*
            await DisplayAlert("Workout Saved",
                $"Workout for {selectedDate.ToString("D")} saved!\nDetails: {workoutDetails}",
                "OK");
                */

            var newWorkout = new Workout {
                Date = selectedDate,
                Details = workoutDetails
            };

            WorkoutService.Workouts.Add(newWorkout);

            await Navigation.PushAsync(new WorkoutHistoryEntryPage());

            WorkoutDetailsEditor.Text = string.Empty;
        }
    }
}
