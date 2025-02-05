namespace Library.Beefit.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public ICollection<Workout>? Workouts { get; set; }

        public User()
        {
            Workouts = new List<Workout>();
        }
    }
}
