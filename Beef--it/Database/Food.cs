using SQLite;
using System;

namespace Beef__it.Database;

    [Table("FoodItems")]
    public class FoodItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MealType { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public DateTime Date { get; set; } // To track food logs by date

        public int UserId { get; set; } // To associate food items with users
    }
