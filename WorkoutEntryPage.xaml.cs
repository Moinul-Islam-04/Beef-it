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

        private async void SaveWorkout_Clicked(object sender, EventArgs e)
        {
            DateTime selectedDate = WorkoutDatePicker.Date;
            string workoutDetails = WorkoutEntry.Text;

            await DisplayAlert("Workout Saved", 
                $"Workout for {selectedDate.ToString("D")} saved!\nDetails: {workoutDetails}", "OK");
        }
    }
}
