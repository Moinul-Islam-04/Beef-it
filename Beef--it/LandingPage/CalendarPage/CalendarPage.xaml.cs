using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;

namespace Beef__it
{
    public partial class CalendarPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<CalendarRepository.CalendarDay> Days { get; set; } = new ObservableCollection<CalendarRepository.CalendarDay>();

        // Use the repository for CalendarDay CRUD.
        private readonly CalendarRepository _repository;

        public ICommand SelectImageCommand { get; }

        private int currentMonth;
        private int currentYear;

        public CalendarPage(CalendarRepository repository)
        {
            InitializeComponent();
            BindingContext = this;

            _repository = repository;

            SelectImageCommand = new Command<CalendarRepository.CalendarDay>(OnSelectImage);

            //Current month and year for calendar
            currentMonth = DateTime.Today.Month;
            currentYear = DateTime.Today.Year;

            InitializeDataAsync();
        }

        private async Task InitializeDataAsync()
        {
            var savedDays = await _repository.GetCalendarDaysAsync(currentYear, currentMonth);
            Days.Clear();

            if (savedDays.Any())
            {
                foreach (var day in savedDays)
                {
                    Days.Add(day);
                }
            }
            else
            {
                // No saved data: populate current month in the database.
                await _repository.PopulateCurrentMonthAsync(currentYear, currentMonth);
                savedDays = await _repository.GetCalendarDaysAsync(currentYear, currentMonth);
                foreach (var day in savedDays)
                {
                    Days.Add(day);
                }
            }

            MonthYearLabel.Text = new DateTime(currentYear, currentMonth, 1).ToString("MMMM yyyy");
        }
        private async void PreviousMonthButton_Clicked(object sender, EventArgs e)
        {
            // Navigates to the previous month.
            if (currentMonth == 1)
            {
                currentMonth = 12;
                currentYear--;
            }
            else
            {
                currentMonth--;
            }
            await InitializeDataAsync();
        }

        private async void NextMonthButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to the next month.
            if (currentMonth == 12)
            {
                currentMonth = 1;
                currentYear++;
            }
            else
            {
                currentMonth++;
            }
            await InitializeDataAsync();
        }


        // Handler for selecting an image.
        private async void OnSelectImage(CalendarRepository.CalendarDay day)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    string pickedImagePath = result.FullPath;
                    string fileName = $"{day.Date:ddMMyyyy}_{DateTime.Now.Ticks}.png";
                    string destinationPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }

                    using (var sourceStream = File.OpenRead(pickedImagePath))
                    using (var destinationStream = File.Create(destinationPath))
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                    }

                    // Update the day image source. Append a query string to force refresh.
                    day.ImageSource = destinationPath + $"?t={DateTime.Now.Ticks}";
                    await _repository.UpdateCalendarDayAsync(day);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Unable to pick image: " + ex.Message, "OK");
            }
        }

    }
}
