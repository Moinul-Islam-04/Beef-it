using Microsoft.Maui.Controls;
using System;
using Beef__it.Models;
using Beef__it.Services;
using Microsoft.Maui.Storage;
using Beef__it.Database;    

namespace Beef__it
{
    public partial class AddExercisePage : ContentPage
    {
        private readonly string _exerciseName;

        public AddExercisePage(string exerciseName)
        {
            InitializeComponent();
            _exerciseName = exerciseName;
            ExerciseNameLabel.Text = $"Selected: {_exerciseName}";
            ExerciseDatePicker.MaximumDate = DateTime.Now;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var selectedDate = ExerciseDatePicker.Date;
            var username = Preferences.Get("LoggedInUsername", null);

            if (string.IsNullOrEmpty(username)) {
                await DisplayAlert("Error", "No user logged in.", "OK");
                return;
            }

            var userRepo = new UserRepository();
            var user = await userRepo.GetUserByUsernameAsync(username);
            if (user == null) {
                await DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            var workout = new Workout
            {
                UserId = user.Id,
                Date = selectedDate,
                Details = _exerciseName
            };

            var workoutRepo = new WorkoutRepository();
            await workoutRepo.AddWorkoutAsync(workout);
            await DisplayAlert("Saved", $"Saved {_exerciseName} on {selectedDate:D}", "OK");
            await Navigation.PushAsync(new WorkoutHistoryEntryPage());
        }
        
    }
}

