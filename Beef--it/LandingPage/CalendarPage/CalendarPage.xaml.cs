using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Diagnostics;

namespace Beef__it
{
    public partial class CalendarPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<CalendarDay> Days { get; set; } = new ObservableCollection<CalendarDay>();

        // SQLite database connection.
        private SQLiteAsyncConnection _database;

        // Command that is triggered when a day is tapped on CalendarPage View.
        public ICommand SelectImageCommand { get; }

        public CalendarPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Initializes the command with a method that takes a CalendarDay as parameter.
            SelectImageCommand = new Command<CalendarDay>(OnSelectImage);

            InitializeDatabaseAndLoadData();
        }

        // Initialize the SQLite connection, create table if necessary, and load data.
        private async void InitializeDatabaseAndLoadData()
        {
            // Sets up the database path in user's local file system.
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "calendar.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // Creates the table if it doesn't exist.
            await _database.CreateTableAsync<CalendarDay>();

            // Loads existing calendar data.
            var savedDays = await _database.Table<CalendarDay>().ToListAsync();
            Days.Clear();

            if (savedDays.Count > 0)
            {
                foreach (var day in savedDays)
                {
                    Days.Add(day);
                }
            }
            else
            {
                // If no saved data found, populates the current month.
                await PopulateCurrentMonth();
            }
        }

        // Populates the calendar with days for the current month.
        private async Task PopulateCurrentMonth()
        {
            DateTime today = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

            for (int i = 0; i < daysInMonth; i++)
            {
                DateTime dayDate = firstDayOfMonth.AddDays(i);
                // Creates the expected file name using the day, month, and year.
                string fileName = $"{dayDate:ddMMyyyy}.png"; // e.g., "01012022.png"
                string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                // Checks if the image file exists, if not makes it null.
                string? imageSource = File.Exists(filePath) ? filePath : null;

                var calendarDay = new CalendarDay
                {
                    Date = dayDate,
                    ImageSource = imageSource
                };

                // Saves to the database.
                await _database.InsertAsync(calendarDay);
                Days.Add(calendarDay);
            }
        }

        // Command handler for when a day is tapped.
        private async void OnSelectImage(CalendarDay day)
        {
            try
            {
                // Lets the user pick a new image from local file system.
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    string pickedImagePath = result.FullPath;

                    // Builds the destination file name based on the CalendarDay's date.
                    string fileName = $"{day.Date:ddMMyyyy}.png";
                    string destinationPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                    // Copies the selected image to the destination overwrites if needed).(
                    using (var sourceStream = File.OpenRead(pickedImagePath))
                    using (var destinationStream = File.Create(destinationPath))
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                    }

                    // Updates the CalendarDay's ImageSource.
                    // Appends string with a timestamp using ticks.
                    day.ImageSource = destinationPath + $"?t={DateTime.Now.Ticks}"; //Ticks are smallest unit of time in C#
                                                                                    //Added in case user adds new picture within same day.
                    // Updates the record in the database.
                    await _database.UpdateAsync(day);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Unable to pick image: " + ex.Message, "OK");
            }
        }

        // CalendarDay model with SQLite attributes and INotifyPropertyChanged.
        public class CalendarDay : INotifyPropertyChanged
        {
            private int _id;
            private DateTime _date;
            private string? _imageSource;

            [PrimaryKey, AutoIncrement] // PrimaryKey tells the SQLite database to use property below (Id) for identification0.
            public int Id               // AutoIncrement sets ID to next available when setter is called.
            {
                get => _id;
                set
                {
                    if (_id != value)
                    {
                        _id = value;
                        OnPropertyChanged();
                    }
                }
            }

            public DateTime Date
            {
                get => _date;
                set
                {
                    if (_date != value)
                    {
                        _date = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string ImageSource
            {
                get => _imageSource;
                set
                {
                    if (_imageSource != value)
                    {
                        _imageSource = value;
                        OnPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
