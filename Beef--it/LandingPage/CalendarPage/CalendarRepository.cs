using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.ComponentModel;

namespace Beef__it
{
    public class CalendarRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public CalendarRepository()
        {
            // Initializes the database connection.
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "calendar.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // Creates the table if it doesn't exist.
            _database.CreateTableAsync<CalendarDay>().Wait();
        }
        public Task<List<CalendarDay>> GetCalendarDaysAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);
            return _database.Table<CalendarDay>()
                .Where(d => d.Date >= startDate && d.Date < endDate)
                .ToListAsync();
        }

        public Task<int> InsertCalendarDayAsync(CalendarDay day)
        {
            return _database.InsertAsync(day);
        }

        public Task<int> UpdateCalendarDayAsync(CalendarDay day)
        {
            return _database.UpdateAsync(day);
        }

        // Populates the database with calendar days for the current month.
  
        public async Task PopulateCurrentMonthAsync(int year, int month)
        {
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);

            for (int i = 0; i < daysInMonth; i++)
            {
                DateTime dayDate = firstDayOfMonth.AddDays(i);
                string fileName = $"{dayDate:ddMMyyyy}.png";
                string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
                string? imageSource = File.Exists(filePath) ? filePath : null;

                var calendarDay = new CalendarDay
                {
                    Date = dayDate,
                    ImageSource = imageSource
                };

                await InsertCalendarDayAsync(calendarDay);
            }
        }
        public class CalendarDay : INotifyPropertyChanged
        {
            private int _id;
            private DateTime _date;
            private string? _imageSource;

            [PrimaryKey, AutoIncrement]
            public int Id
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

            public string? ImageSource
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
            protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

