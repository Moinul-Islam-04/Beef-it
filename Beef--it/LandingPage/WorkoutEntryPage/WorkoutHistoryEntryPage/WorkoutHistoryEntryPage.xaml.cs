using Microsoft.Maui.Controls;

namespace Beef__it
{
    public partial class WorkoutHistoryEntryPage : ContentPage
    {
        public WorkoutHistoryEntryPage()
        {
            InitializeComponent();
            WorkoutsCollectionView.ItemsSource = WorkoutService.Workouts;
        }
    }
}
