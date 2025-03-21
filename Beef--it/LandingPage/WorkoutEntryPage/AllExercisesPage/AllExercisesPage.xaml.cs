using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Beef__it.Models;

namespace Beef__it
{
    public partial class AllExercisesPage : ContentPage
    {
        public ObservableCollection<ExerciseGroup> ExerciseGroups { get; set; }
        
        public AllExercisesPage()
        {
            InitializeComponent();
        
            ExerciseGroups = new ObservableCollection<ExerciseGroup>
            {
                new ExerciseGroup("Chest")
                {
                    new Exercise { Name = "Push-ups" },
                    new Exercise { Name = "Bench Press (Barbell/Dumbbell)" },
                    new Exercise { Name = "Incline Bench Press" },
                    new Exercise { Name = "Chest Flys (Machine/Dumbbell)" },
                    new Exercise { Name = "Dips (Chest-focused)" }
                },
                new ExerciseGroup("Back")
                {
                    new Exercise { Name = "Pull-ups/Chin-ups" },
                    new Exercise { Name = "Lat Pulldowns" },
                    new Exercise { Name = "Barbell Rows" },
                    new Exercise { Name = "Dumbbell Rows" },
                    new Exercise { Name = "Deadlifts" },
                    new Exercise { Name = "T-Bar Rows" },
                    new Exercise { Name = "Seated Cable Rows" }
                },
                new ExerciseGroup("Shoulders")
                {
                    new Exercise { Name = "Overhead Press (Barbell/Dumbbell)" },
                    new Exercise { Name = "Lateral Raises" },
                    new Exercise { Name = "Front Raises" },
                    new Exercise { Name = "Reverse Flys" },
                    new Exercise { Name = "Arnold Press" }
                },
                new ExerciseGroup("Arms")
                {
                    new Exercise { Name = "Bicep Curls (Barbell/Dumbbell)" },
                    new Exercise { Name = "Hammer Curls" },
                    new Exercise { Name = "Tricep Dips" },
                    new Exercise { Name = "Skull Crushers (Lying Tricep Extensions)" },
                    new Exercise { Name = "Tricep Pushdowns (Cable)" },
                    new Exercise { Name = "Preacher Curls" }
                },
                new ExerciseGroup("Legs")
                {
                    new Exercise { Name = "Squats (Barbell)" },
                    new Exercise { Name = "Hack Squats" },
                    new Exercise { Name = "Leg Press" },
                    new Exercise { Name = "Lunges" },
                    new Exercise { Name = "Leg Extensions" },
                    new Exercise { Name = "Hamstring Curls" },
                    new Exercise { Name = "Romanian Deadlifts (Barbell)" },
                    new Exercise { Name = "Deadlifts" },
                    new Exercise { Name = "Hip Thrust" },
                    new Exercise { Name = "Calf Raises" },
                    new Exercise { Name = "Bulgarian Split-Squats" },
                    new Exercise { Name = "Hip Adductors (Machine)" },
                    new Exercise { Name = "Hip Abductors (Machine)" }
                },
                new ExerciseGroup("Core")
                {
                    new Exercise { Name = "Planks" },
                    new Exercise { Name = "Crunches" },
                    new Exercise { Name = "Leg Raises" },
                    new Exercise { Name = "Russian Twists" },
                    new Exercise { Name = "Mountain Climbers" },
                    new Exercise { Name = "Bicycle Crunches" },
                    new Exercise { Name = "Hanging Leg Raises" }
                }
            };

            BindingContext = this;
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var selectedExercise = e.CurrentSelection[0] as Exercise;
            if (selectedExercise == null)
                return;

            ((CollectionView)sender).SelectedItem = null;

            await Navigation.PushAsync(new AddExercisePage(selectedExercise.Name));
        }
    }
}