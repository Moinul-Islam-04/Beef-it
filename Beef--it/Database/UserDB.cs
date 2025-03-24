using SQLite;
using System;

namespace Beef__it
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }

    // DTO - Separates data transfer from database model
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }

        // Method to convert DTO to User entity
        public User ToEntity(string passwordHash)
        {
            return new User
            {
                Username = this.Username,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Age = this.Age,
                Height = this.Height,
                Weight = this.Weight,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}