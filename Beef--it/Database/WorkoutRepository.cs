using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beef__it.Database
{
    public class WorkoutRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public WorkoutRepository()
        {
            _database = ConnectionFactory.GetConnection();
            _database.CreateTableAsync<Workout>().Wait(); // Create table if not exists
        }

        public Task<int> AddWorkoutAsync(Workout workout)
        {
            return _database.InsertAsync(workout);
        }

        public Task<List<Workout>> GetAllWorkoutsAsync()
        {
            return _database.Table<Workout>().ToListAsync();
        }

        public Task<List<Workout>> GetWorkoutsByUserIdAsync(int userId)
        {
            return _database.Table<Workout>()
                            .Where(w => w.UserId == userId)
                            .ToListAsync();
        }


        public Task<int> DeleteWorkoutAsync(Workout workout)
        {
            return _database.DeleteAsync(workout);
        }

        public Task<int> UpdateWorkoutAsync(Workout workout)
        {
            return _database.UpdateAsync(workout);
        }
    }
}
