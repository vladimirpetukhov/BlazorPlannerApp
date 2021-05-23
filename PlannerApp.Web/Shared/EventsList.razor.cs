
using Microsoft.AspNetCore.Components;
using PlannerApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Web.Shared
{
    public partial class EventsList
    {
        [Parameter]
        public CalendarDay SelectedDay { get; set; }

        protected override void OnInitialized()
        {
            AddEvent.OnEventAdded += () =>
            {
                StateHasChanged();
            };
        }

        
    }
}
