using System.Collections.ObjectModel;
using Beef__it.Models;

namespace Beef__it.Services
{
    public static class WorkoutHistoryService
    {
        public static ObservableCollection<WorkoutHistoryItem> SavedExercises { get; } = new ObservableCollection<WorkoutHistoryItem>();

        public static void AddToHistory(WorkoutHistoryItem item)
        {
            SavedExercises.Add(item);
        }
    }
}
