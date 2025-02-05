using System;
using System.ComponentModel.DataAnnotations;
using Airport_Management_System.Entities;

namespace Airport_Management_System.DTOs
{
    public class FlightDTO
    {
        [Required(ErrorMessage = "Flight ID is required.")]
        [Display(Name = "Flight ID")]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Departure country is required.")]
        [Display(Name = "Departure Country")]
        public string? DepartureCountry { get; set; }

        [Required(ErrorMessage = "Destination country is required.")]
        [Display(Name = "Destination Country")]
        public string? DestinationCountry { get; set; }

        [Required(ErrorMessage = "Departure airport is required.")]
        [Display(Name = "Departure Airport")]
        public string? DepartureAirport { get; set; }

        [Required(ErrorMessage = "Destination airport is required.")]
        [Display(Name = "Destination Airport")]
        public string? DestinationAirport { get; set; }

        [Required(ErrorMessage = "Departure date is required.")]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Arrival date is required.")]
        [Display(Name = "Arrival Date")]
        public DateTime ArrivalDate { get; set; }

        [Range(1, 500, ErrorMessage = "Max seat size must be between 1 and 500.")]
        [Display(Name = "Max Seat Size")]
        public int MaxSeatSize { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Economy price must be a positive value.")]
        [Display(Name = "Economy Price")]
        public decimal EconomyPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Business price must be a positive value.")]
        [Display(Name = "Business Price")]
        public decimal BusinessPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "First class price must be a positive value.")]
        [Display(Name = "First Class Price")]
        public decimal FirstClassPrice { get; set; }

        [Display(Name = "Passengers")]
        public List<Passenger?> Passengers { get; set; }
    }


}
