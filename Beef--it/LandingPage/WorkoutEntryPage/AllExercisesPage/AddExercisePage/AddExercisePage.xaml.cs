using Microsoft.Maui.Controls;
using System;
using Beef__it.Models;
using Beef__it.Services;

namespace Beef__it
{
    public partial class AddExercisePage : ContentPage
    {
        private string _exerciseName;

        public AddExercisePage(string exerciseName)
        {
            InitializeComponent();
            _exerciseName = exerciseName;
            ExerciseNameLabel.Text = $"Selected: {_exerciseName}";
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var historyItem = new WorkoutHistoryItem
            {
                ExerciseName = _exerciseName,
                SelectedDate = ExerciseDatePicker.Date
            };

            WorkoutHistoryService.AddToHistory(historyItem);

            await Navigation.PushAsync(new WorkoutHistoryEntryPage());
        }
    }
}
