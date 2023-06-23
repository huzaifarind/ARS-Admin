using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ArsAdmin.Models
{
    public class FlightSchedule
    {
        public int FlightScheduleId { get; set; }
        [Display(Name = "Ref No")]

   
        public int PlaneId { get; set; }

        [Display(Name = "Plane Name")]
        public string PlaneName { get; set; }

        [Display(Name = "Arrival Airport")]
        public int ArrivalAirportId { get; set; }

        [Display(Name = "Arrival Airport Name")]
        public string ArrivalAirportName { get; set; }

        [Display(Name = "Departure Airport")]
        public int DepartureAirportId { get; set; }

        [Display(Name = "Departure Airport Name")]
        public string DepartureAirportName { get; set; }

        [Display(Name = "Flight Date and Time")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTiming { get; set; }

        public SelectList PlaneNameSelectList { get; set; }
        public SelectList ArrivalAirportSelectList { get; set; }
        public SelectList DepartureAirportSelectList { get; set; }
        public IEnumerable<FlightSchedule> GetFlightSchedules { get; set; }
    }
}
