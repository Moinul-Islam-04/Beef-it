using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using Beef__it.Database; // Needed for Workout + WorkoutRepository
using System.Collections.Generic;

namespace Beef__it
{
    public partial class WorkoutHistoryEntryPage : ContentPage
    {
        public WorkoutHistoryEntryPage()
        {
            InitializeComponent();
            LoadWorkoutHistory(); // Load workouts when page is created
        }

        private async void LoadWorkoutHistory()
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

            var repo = new WorkoutRepository();
            List<Workout> workouts = await repo.GetWorkoutsByUserIdAsync(user.Id);

            foreach (var workout in workouts)
            {
                WorkoutListLayout.Children.Add(new Label
                {
                    Text = $"{workout.Date.ToString("D")}: {workout.Details}",
                    TextColor = Colors.White,
                    FontSize = 16
                });
            }

            if (workouts.Count == 0)
            {
                WorkoutListLayout.Children.Add(new Label
                {
                    Text = "No workouts logged yet.",
                    TextColor = Colors.Gray,
                    HorizontalOptions = LayoutOptions.Center
                });
            }
        }

    }
}
