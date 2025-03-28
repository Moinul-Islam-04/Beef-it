using SQLite;
using System;

namespace Beef__it.Database
{
    [Table("Workouts")]
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Details { get; set; }

        // Optional: add a UserId if you want to associate workouts with users
        public int? UserId { get; set; }
    }
}
