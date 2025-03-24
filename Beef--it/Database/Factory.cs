using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using BCrypt.Net;

namespace Beef__it.Database
{
    // Update ConnectionFactory to be more robust
    public class ConnectionFactory
    {
        // Make this a static method for easier use
        public static SQLiteAsyncConnection GetConnection()
        {
            return new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "Beefit.db3"),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        }
    }

    // Add a repository for user-related database operations
    public class UserRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public UserRepository()
        {
            _database = ConnectionFactory.GetConnection();
            // Ensure table is created
            _database.CreateTableAsync<User>().Wait();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var user = await _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Username == username);
            return user != null;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                // Check if username already exists
                if (await UserExistsAsync(user.Username))
                {
                    return false;
                }

                // Insert the new user
                int rowsAffected = await _database.InsertAsync(user);
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                // Log the exception in a real-world scenario
                return false;
            }
        }
    }
}
