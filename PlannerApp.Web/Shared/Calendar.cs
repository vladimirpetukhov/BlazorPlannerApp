
using Microsoft.AspNetCore.Components;
using PlannerApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Web.Shared
{
    public partial class Calendar
    {

        [Parameter]
        public RenderFragment<CalendarDay> DayTemplate { get; set; }


        private int year = 2020;
        private int month = 05;
        private List<CalendarDay> days = new List<CalendarDay>();
        private int rowsCount = 0;

        async Task SelectYear(ChangeEventArgs e)
        {
            year = Convert.ToInt32(e.Value.ToString());
            // Render Calendar
            UpdateCalendar();
        }

        async Task SelectMonth(ChangeEventArgs e)
        {
            month = Convert.ToInt32(e.Value.ToString());
            // Render Calendar
            UpdateCalendar();

        }

        void UpdateCalendar()
        {
            days = new List<CalendarDay>();

            // Calculate the number of empty days 
            var firstDayDate = new DateTime(year, month, 1);
            int weekDayNumber = (int)firstDayDate.DayOfWeek;
            int numberOfEmptyDays = 0;
            if (weekDayNumber == 7) // Sunday 
            {
                numberOfEmptyDays = 0;
            }
            else
            {
                numberOfEmptyDays = weekDayNumber;
            }

            // Add the empty days 
            for (int i = 0; i < numberOfEmptyDays; i++)
            {
                days.Add(new CalendarDay
                {
                    DayNumber = 0,
                    IsEmpty = true
                });
            }

            // Add the month days 
            int numberOfDaysInMonth = DateTime.DaysInMonth(year, month);

            for (int i = 0; i < numberOfDaysInMonth; i++)
            {
                days.Add(new CalendarDay
                {
                    DayNumber = i + 1,
                    IsEmpty = false,
                    Date = new DateTime(year, month, i + 1),
                    Events = new List<CalendarEvent>()
                });
            }

            // 2- Calcualte the number of rows 
            int remaning = days.Count % 7;
            if (remaning == 0)
                rowsCount = days.Count / 7;
            else
                rowsCount = Convert.ToInt32(days.Count / 7) + 1;

            Console.WriteLine($"Total Rows: {rowsCount} | Number of Empty Days {numberOfEmptyDays} | Month Days {numberOfDaysInMonth}");

        }
    }
}
