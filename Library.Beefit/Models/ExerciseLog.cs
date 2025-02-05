using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Beefit.Models
{
    public class ExerciseLog
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public Workout? Workout { get; set; }
        public string? ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}
