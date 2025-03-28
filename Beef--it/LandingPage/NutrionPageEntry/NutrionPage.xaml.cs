using System.Collections.ObjectModel;
//using Microcharts;
//using SkiaSharp;
//using Beef__it.Models;

namespace Beef__it
{
    public partial class NutritionPage : ContentPage
    {
        public ObservableCollection<FoodItem> FoodLog { get; set; } = new ObservableCollection<FoodItem>();
        public Command DeleteCommand { get; }

        private int _caloriesConsumed = 0;
        private int _proteinConsumed = 0;
        private int _carbsConsumed = 0;
        private int _fatsConsumed = 0;
        private int _waterIntake = 0;

        public NutritionPage()
        {
            InitializeComponent();
            BindingContext = this;

            DeleteCommand = new Command<FoodItem>(OnDeleteFoodItem);
            FoodLogCollection.ItemsSource = FoodLog;

           // UpdateProgress();
           // UpdateChart();
        }

        private void OnAddFoodClicked(object sender, EventArgs e)
        {
            if (int.TryParse(CaloriesEntry.Text, out int calories) && int.TryParse(ProteinEntry.Text, out int protein))
            {
                var foodItem = new FoodItem
                {
                    Name = FoodNameEntry.Text,
                    Calories = calories,
                    Protein = protein
                };

                FoodLog.Add(foodItem);
                _caloriesConsumed += calories;
                _proteinConsumed += protein;
                _carbsConsumed += calories / 4; // Simplified calculation
                _fatsConsumed += calories / 9;  // Simplified calculation

               // UpdateProgress();
               // UpdateChart();

                FoodNameEntry.Text = string.Empty;
                CaloriesEntry.Text = string.Empty;
                ProteinEntry.Text = string.Empty;
            }
        }

        private void OnDeleteFoodItem(FoodItem foodItem)
        {
            FoodLog.Remove(foodItem);
            _caloriesConsumed -= foodItem.Calories;
            _proteinConsumed -= foodItem.Protein;
            _carbsConsumed -= foodItem.Calories / 4;
            _fatsConsumed -= foodItem.Calories / 9;

          //  UpdateProgress();
          //  UpdateChart();
        }

        /*private void OnAddWaterClicked(object sender, EventArgs e)
        {
            _waterIntake++;
            WaterIntakeLabel.Text = $"{_waterIntake}/8 cups";
        }

        private void UpdateProgress()
        {
            CaloriesLabel.Text = $"{_caloriesConsumed}/2000";
            ProteinLabel.Text = $"{_proteinConsumed}/150g";
            CarbsLabel.Text = $"{_carbsConsumed}/250g";
            FatsLabel.Text = $"{_fatsConsumed}/70g";
        }

        private void UpdateChart()
        {
            var entries = new[]
            {
                new ChartEntry(2000 - _caloriesConsumed) { Label = "Remaining", ValueLabel = $"{2000 - _caloriesConsumed}", Color = SKColor.Parse("#77D065") },
                new ChartEntry(_caloriesConsumed) { Label = "Consumed", ValueLabel = $"{_caloriesConsumed}", Color = SKColor.Parse("#FF1943") }
            };

            CalorieChart.Chart = new DonutChart { Entries = entries };
        }*/
    }

    public class FoodItem
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
    }
}