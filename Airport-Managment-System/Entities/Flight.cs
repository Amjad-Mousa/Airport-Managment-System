using System;
using System.Collections.Generic;

namespace Airport_Management_System.Entities
{
    public class Flight
    {
        public required string Id { get; set; }
        public string? DepartureCountry { get; set; }
        public string? DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string? DepartureAirport { get; set; }
        public string? DestinationAirport { get; set; }
        public decimal EconomyPrice { get; set; } = 50;
        public decimal BusinessPrice { get; set; } = 70;
        public decimal FirstClassPrice { get; set; } = 90;

        public int MaxSeatSize { get; private set; }
        private Dictionary<int, string> AvailableSeats { get; set; } = new();

        public Flight(int maxSeatSize)
        {
            if (maxSeatSize <= 0)
            {
                throw new ArgumentException("MaxSeatSize must be greater than zero.");
            }

            MaxSeatSize = maxSeatSize;

            for (int i = 1; i <= MaxSeatSize; i++)
            {
                AvailableSeats[i] = "Available";
            }
        }

        public Dictionary<int, string> GetSeats()
        {
            return AvailableSeats;
        }


        public override string ToString()
        {
            var seatsInfo = string.Join(", ", AvailableSeats.Select(s => $"{s.Key}: {s.Value}"));
            return $"Flight ID: {this.Id}\n" +
                   $"Departure: {this.DepartureCountry} ({this.DepartureAirport})\n" +
                   $"Destination: {this.DestinationCountry} ({this.DestinationAirport})\n" +
                   $"Departure Date: {this.DepartureDate}\n" +
                   $"Arrival Date: {this.ArrivalDate}\n" +
                   $"Prices:\n" +
                   $"  Economy: ${this.EconomyPrice}\n" +
                   $"  Business: ${this.BusinessPrice}\n" +
                   $"  First Class: ${this.FirstClassPrice}\n" +
                   $"Max Seats: {MaxSeatSize}\n" +
                   $"Seats: {seatsInfo}";
        }
    }
}