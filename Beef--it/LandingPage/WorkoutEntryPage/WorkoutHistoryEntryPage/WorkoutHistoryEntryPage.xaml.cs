using Microsoft.Maui.Controls;
using Beef__it.Services;


namespace Beef__it
{
    public partial class WorkoutHistoryEntryPage : ContentPage
    {
        public WorkoutHistoryEntryPage()
        {
            InitializeComponent();
             HistoryCollectionView.ItemsSource = ExerciseHistoryService.SavedExercises;
        }
    }
}
