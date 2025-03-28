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
            
            var historyItem = new Models.ExerciseHistoryItem
            {
                ExerciseName = workoutDetails,
                SelectedDate = selectedDate
            };

            Services.ExerciseHistoryService.AddToHistory(historyItem);

            await DisplayAlert("Workout Saved",
                $"Workout for {selectedDate.ToString("D")} saved!\nDetails: {workoutDetails}",
                "OK");

            await Navigation.PushAsync(new WorkoutHistoryEntryPage());
        }
    }
}
