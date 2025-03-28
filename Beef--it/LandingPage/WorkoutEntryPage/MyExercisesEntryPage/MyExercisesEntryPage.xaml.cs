using Microsoft.Maui.Controls;
using System;
using Beef__it.Database; // Add this to use Workout + WorkoutRepository

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

            // Create workout object
            var workout = new Workout
            {
                Date = selectedDate,
                Details = workoutDetails
            };

            // Save to database
            var repo = new WorkoutRepository();
            await repo.AddWorkoutAsync(workout);

            await DisplayAlert("Workout Saved",
                $"Workout for {selectedDate.ToString("D")} saved!\nDetails: {workoutDetails}",
                "OK");

            // Clear the input for a fresh entry
            WorkoutDetailsEditor.Text = string.Empty;
        }
    }
}
