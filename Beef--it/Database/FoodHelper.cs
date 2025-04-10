using Beef__it.Database;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Foodhelper
{
    private readonly SQLiteAsyncConnection _database;

    public Foodhelper(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<FoodItem>().Wait();
    }

    public Task<List<FoodItem>> GetFoodLogByDateAndUserAsync(DateTime date, int userId)
    {
    return _database.Table<FoodItem>()
                    .Where(f => f.UserId == userId && f.Date == date)
                    .ToListAsync();
    }
    public Task<int> AddFoodItemAsync(FoodItem item)
    {
        return _database.InsertAsync(item);
    }

    public Task<int> DeleteFoodItemAsync(FoodItem item)
    {
        return _database.DeleteAsync(item);
    }
}
