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

            // Get the current username from Preferences
            string username = Preferences.Get("LoggedInUsername", null);

            if (string.IsNullOrEmpty(username))
            {
                await DisplayAlert("Error", "No user logged in.", "OK");
                return;
            }

            // Get the user ID
            var userRepo = new UserRepository();
            var user = await userRepo.GetUserByUsernameAsync(username);

            if (user == null)
            {
                await DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            // Save the workout with UserId
            var workout = new Workout
            {
                Date = selectedDate,
                Details = workoutDetails,
                UserId = user.Id
            };

            var repo = new WorkoutRepository();
            await repo.AddWorkoutAsync(workout);

            await DisplayAlert("Workout Saved",
                $"Workout for {selectedDate.ToString("D")} saved!\nDetails: {workoutDetails}",
                "OK");

            WorkoutDetailsEditor.Text = string.Empty;
        }

    }
}
