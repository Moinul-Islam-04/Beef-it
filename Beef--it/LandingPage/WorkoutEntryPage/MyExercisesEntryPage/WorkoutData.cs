using System;
using System.Collections.ObjectModel;

namespace Beef__it
{
    public class Workout
    {
        public DateTime Date { get; set; }
        public string Details { get; set; }
    }

    public static class WorkoutService
    {
        
        public static ObservableCollection<Workout> Workouts { get; } = new ObservableCollection<Workout>();
    }
}
