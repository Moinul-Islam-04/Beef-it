using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Beefit.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public string? Name { get; set; }
        public DateTime Date { get; set; }  //DateTime is a built in C# struct. Constructs with current time
        public ICollection<ExerciseLog>? ExerciseLogs { get; set; }
    }
}
