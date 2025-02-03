using System;
using System.ComponentModel.DataAnnotations;
using Airport_Management_System.Entities;

namespace Airport_Management_System.DTOs
{
    public class FlightDTO
    {
        [Required(ErrorMessage = "Flight ID is required.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Departure country is required.")]
        public string DepartureCountry { get; set; }

        [Required(ErrorMessage = "Destination country is required.")]
        public string DestinationCountry { get; set; }

        [Required(ErrorMessage = "Departure airport is required.")]
        public string DepartureAirport { get; set; }

        [Required(ErrorMessage = "Destination airport is required.")]
        public string DestinationAirport { get; set; }

        [Required(ErrorMessage = "Departure date is required.")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Arrival date is required.")]
        public DateTime ArrivalDate { get; set; }

        [Range(1, 500, ErrorMessage = "Max seat size must be between 1 and 500.")]
        public int MaxSeatSize { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Economy price must be a positive value.")]
        public decimal EconomyPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Business price must be a positive value.")]
        public decimal BusinessPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "First class price must be a positive value.")]
        public decimal FirstClassPrice { get; set; }
        public List<Passenger> Passengers { get; set; }
    }
}