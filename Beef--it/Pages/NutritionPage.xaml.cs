using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Beef__it.Pages;

public partial class NutritionPage : ContentPage
{
    public class FoodEntry
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
    }

    private ObservableCollection<FoodEntry> foodLog;
    private int totalCalories = 0;
    private int totalProtein = 0;
    public ICommand DeleteCommand { get; private set; }

    public NutritionPage()
    {
        InitializeComponent();
        foodLog = new ObservableCollection<FoodEntry>();
        FoodLogCollection.ItemsSource = foodLog;

        DeleteCommand = new Command<FoodEntry>(OnDeleteFoodEntry);
    }

    private void OnAddFoodClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FoodNameEntry.Text) ||
            string.IsNullOrWhiteSpace(CaloriesEntry.Text) ||
            string.IsNullOrWhiteSpace(ProteinEntry.Text))
        {
            DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        if (!int.TryParse(CaloriesEntry.Text, out int calories) ||
            !int.TryParse(ProteinEntry.Text, out int protein))
        {
            DisplayAlert("Error", "Please enter valid numbers", "OK");
            return;
        }

        var newEntry = new FoodEntry
        {
            Name = FoodNameEntry.Text,
            Calories = calories,
            Protein = protein
        };

        foodLog.Add(newEntry);
        UpdateTotals();
        ClearEntries();
    }

    private void OnDeleteFoodEntry(FoodEntry entry)
    {
        if (entry != null)
        {
            foodLog.Remove(entry);
            UpdateTotals();
        }
    }

    private void UpdateTotals()
    {
        totalCalories = foodLog.Sum(f => f.Calories);
        totalProtein = foodLog.Sum(f => f.Protein);

        CaloriesLabel.Text = $"{totalCalories}/2000";
        ProteinLabel.Text = $"{totalProtein}/150g";
    }

    private void ClearEntries()
    {
        FoodNameEntry.Text = string.Empty;
        CaloriesEntry.Text = string.Empty;
        ProteinEntry.Text = string.Empty;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateTotals();
    }
}