using System;
using System.Collections.ObjectModel;

namespace Beef__it.Models
{
    public class Exercise
    {
        public string Name { get; set; }
    }

    public class ExerciseGroup : ObservableCollection<Exercise>
    {
        public string GroupName { get; set; }

        public ExerciseGroup(string groupName, params string[] exercises)
        {
            GroupName = groupName;
            foreach (var ex in exercises)
            {
                Add(new Exercise { Name = ex });
            }
        }
    }

    public class ExerciseHistoryItem
    {
        public string ExerciseName { get; set; }
        public DateTime SelectedDate { get; set; }
    }
}

namespace Beef__it.Services
{
    public static class ExerciseHistoryService
    {
        public static ObservableCollection<Beef__it.Models.ExerciseHistoryItem> SavedExercises { get; } =
            new ObservableCollection<Beef__it.Models.ExerciseHistoryItem>();

        public static void AddToHistory(Beef__it.Models.ExerciseHistoryItem item)
        {
            SavedExercises.Add(item);
        }
    }
}
