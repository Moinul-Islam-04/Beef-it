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
                new ExerciseGroup("Chest",
                    "Push-ups",
                    "Bench Press (Barbell/Dumbbell)",
                    "Incline Bench Press",
                    "Chest Flys (Machine/Dumbbell)",
                    "Dips (Chest-focused)"
                ),
                new ExerciseGroup("Back",
                    "Pull-ups/Chin-ups",
                    "Lat Pulldowns",
                    "Barbell Rows",
                    "Dumbbell Rows",
                    "Deadlifts",
                    "T-Bar Rows",
                    "Seated Cable Rows"
                ),
                new ExerciseGroup("Shoulders",
                    "Overhead Press (Barbell/Dumbbell)",
                    "Lateral Raises",
                    "Front Raises",
                    "Reverse Flys",
                    "Arnold Press"
                ),
                new ExerciseGroup("Arms",
                    "Bicep Curls (Barbell/Dumbbell)",
                    "Hammer Curls",
                    "Tricep Dips",
                    "Skull Crushers (Lying Tricep Extensions)",
                    "Tricep Pushdowns (Cable)",
                    "Preacher Curls"
                ),
                new ExerciseGroup("Legs",
                    "Squats (Barbell)",
                    "Hack Squats",
                    "Leg Press",
                    "Lunges",
                    "Leg Extensions",
                    "Hamstring Curls",
                    "Romanian Deadlifts (Barbell)",
                    "Deadlifts",
                    "Hip Thrust",
                    "Calf Raises",
                    "Bulgarian Split-Squats",
                    "Hip Adductors (Machine)",
                    "Hip Abductors (Machine)"
                ),
                new ExerciseGroup("Core",
                    "Planks",
                    "Crunches",
                    "Leg Raises",
                    "Russian Twists",
                    "Mountain Climbers",
                    "Bicycle Crunches",
                    "Hanging Leg Raises"
                )
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