namespace PlannerApp.Web.Shared
{

    #region usings
    using Microsoft.AspNetCore.Components;
    using PlannerApp.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    #endregion

    public partial class AddEvent
    {
        #region props
        [Parameter]
        public CalendarDay SelectedDay { get; set; }

        private CalendarEvent model = new CalendarEvent();
        #endregion

        #region methods
        private async Task AddEventToSelectedDay()
        {
            // Convert the time to the a new date within the selected day 
            model.StartDate = new DateTime(SelectedDay.Date.Year,
                                            SelectedDay.Date.Month,
                                            SelectedDay.Date.Day,
                                            model.StartDate.Hour,
                                            model.StartDate.Minute,
                                            0);

            model.EndDate = new DateTime(SelectedDay.Date.Year,
                                            SelectedDay.Date.Month,
                                            SelectedDay.Date.Day,
                                            model.EndDate.Hour,
                                            model.EndDate.Minute,
                                            0);

            if (SelectedDay.Events == null)
                SelectedDay.Events = new List<CalendarEvent>();

            var calendarEvent = new CalendarEvent
            {
                Subject = model.Subject,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };
            SelectedDay.Events.Add(calendarEvent);
            OnEventAdded.Invoke();

            Console.WriteLine($"Total Events in Day {SelectedDay.Events.Count} | {model.Subject} | {model.StartDate} | {model.EndDate}");

            model = new CalendarEvent();
        }

        public static event Action OnEventAdded = () => { };
        #endregion
    }
}
