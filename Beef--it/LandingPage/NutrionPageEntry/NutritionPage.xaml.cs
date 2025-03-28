using System.Collections.ObjectModel;
using Beef__it.Database;
using System;

namespace Beef__it
{
    public partial class NutritionPage : ContentPage
    {
        public ObservableCollection<FoodItem> FoodLog { get; set; } = new ObservableCollection<FoodItem>();
        public Command DeleteCommand { get; }

        // Use normal integer variables (not pointers)
        private int caloriesConsumed = 0;
        private int proteinConsumed = 0;
        private int dailyCalorieGoal = 2000;
        private int dailyProteinGoal = 150;

        // Track the current date
        private DateTime currentDate = DateTime.Today;

        private Foodhelper _db;

        public NutritionPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Initialize database
            _db = new Foodhelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "nutrition.db"));

            DeleteCommand = new Command<FoodItem>(OnDeleteFoodItem);
            FoodLogCollection.ItemsSource = FoodLog;

            // Initialize the summary labels
            UpdateSummaryLabels();
            UpdateDateLabel();

            // Load initial data
            LoadDateData();
        }

        private async void OnNextDateClicked(object sender, EventArgs e)
        {
            // Move to next day (don't allow future dates beyond today)
            if (currentDate < DateTime.Today)
            {
                currentDate = currentDate.AddDays(1);
                UpdateDateLabel();
                await LoadDateData();
            }
        }

        private async void OnPreviousDateClicked(object sender, EventArgs e)
        {
            // Move to previous day
            currentDate = currentDate.AddDays(-1);
            UpdateDateLabel();
            await LoadDateData();
        }

        private void UpdateDateLabel()
        {
            // Format the date display
            if (currentDate.Date == DateTime.Today)
            {
                DateLabel.Text = "Today";
            }
            else if (currentDate.Date == DateTime.Today.AddDays(-1))
            {
                DateLabel.Text = "Yesterday";
            }
            else
            {
                DateLabel.Text = currentDate.ToString("MMM d, yyyy");
            }
        }

        private async Task LoadDateData()
        {
            // Clear current food log
            FoodLog.Clear();
            caloriesConsumed = 0;
            proteinConsumed = 0;

            // Load data from database
            var foodItems = await _db.GetFoodLogByDateAsync(currentDate);

            foreach (var item in foodItems)
            {
                FoodLog.Add(item);
                caloriesConsumed += item.Calories;
                proteinConsumed += item.Protein;
            }

            // Update UI
            UpdateSummaryLabels();
        }

        private void OnQuickAddClicked(object sender, EventArgs e)
        {
            // Get the button that was clicked
            Button button = sender as Button;
            string mealType = button.CommandParameter.ToString();

            // Pre-select the meal type in the picker
            MealTypePicker.SelectedItem = mealType;

            // Focus on the food name entry to start adding food
            FoodNameEntry.Focus();
        }

        private async void OnAddFoodClicked(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(FoodNameEntry.Text))
            {
                await DisplayAlert("Error", "Please enter a food name", "OK");
                return;
            }

            if (!int.TryParse(CaloriesEntry.Text, out int calories) || calories < 0)
            {
                await DisplayAlert("Error", "Please enter a valid calorie value", "OK");
                return;
            }

            if (!int.TryParse(ProteinEntry.Text, out int protein) || protein < 0)
            {
                await DisplayAlert("Error", "Please enter a valid protein value", "OK");
                return;
            }

            // Ensure a meal type is selected
            if (MealTypePicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please select a meal type", "OK");
                return;
            }

            var foodItem = new FoodItem
            {
                Name = FoodNameEntry.Text,
                MealType = MealTypePicker.SelectedItem.ToString(),
                Calories = calories,
                Protein = protein,
                Date = currentDate
            };

            // Add to database
            await _db.AddFoodItemAsync(foodItem);

            // Add to UI
            FoodLog.Add(foodItem);
            caloriesConsumed += calories;
            proteinConsumed += protein;

            UpdateSummaryLabels();

            // Clear input fields
            FoodNameEntry.Text = string.Empty;
            CaloriesEntry.Text = string.Empty;
            ProteinEntry.Text = string.Empty;
        }

        private async void OnDeleteFoodItem(FoodItem foodItem)
        {
            // Remove from database
            await _db.DeleteFoodItemAsync(foodItem);

            // Remove from UI
            if (FoodLog.Remove(foodItem))
            {
                caloriesConsumed -= foodItem.Calories;
                proteinConsumed -= foodItem.Protein;
                UpdateSummaryLabels();
            }
        }

        private void UpdateSummaryLabels()
        {
            CaloriesLabel.Text = $"{caloriesConsumed}/{dailyCalorieGoal}";
            ProteinLabel.Text = $"{proteinConsumed}/{dailyProteinGoal}g";

            // Update progress bars
            double caloriesProgress = Math.Min(1.0, (double)caloriesConsumed / dailyCalorieGoal);
            double proteinProgress = Math.Min(1.0, (double)proteinConsumed / dailyProteinGoal);

            CaloriesProgressFrame.WidthRequest = caloriesProgress * ((300) - 100); // Approximate width
            ProteinProgressFrame.WidthRequest = proteinProgress * ((300) - 100);
        }
    }
}