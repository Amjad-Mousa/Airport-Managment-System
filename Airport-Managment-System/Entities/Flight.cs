using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;
using Airport_Management_System.DTOs;
using Airport_Management_System.Helper;

namespace Airport_Management_System.Entities
{
    public class Flight
    {
        [Required] public required string Id { get; set; }

        public string? DepartureCountry { get; set; }
        public string? DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string? DepartureAirport { get; set; }
        public string? DestinationAirport { get; set; }

        [Range(0, double.MaxValue)] public decimal EconomyPrice { get; set; } = 50;

        [Range(0, double.MaxValue)] public decimal BusinessPrice { get; set; } = 70;

        [Range(0, double.MaxValue)] public decimal FirstClassPrice { get; set; } = 90;

        public int MaxSeatSize { get; set; }

        public List<Seat> AvailableSeats { get; set; } = new();

        private List<Passenger> Passengers { get; set; } = new();

        public Flight(string id, string? departureCountry, string? destinationCountry, DateTime departureDate,
            DateTime arrivalDate,
            string? departureAirport, string? destinationAirport, decimal economyPrice, decimal businessPrice,
            decimal firstClassPrice, int maxSeatSize)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            DepartureAirport = departureAirport;
            DestinationAirport = destinationAirport;
            EconomyPrice = economyPrice;
            BusinessPrice = businessPrice;
            FirstClassPrice = firstClassPrice;

            if (maxSeatSize <= 0)
            {
                throw new ArgumentException("MaxSeatSize must be greater than zero.");
            }

            MaxSeatSize = maxSeatSize;

            for (int i = 1; i <= MaxSeatSize; i++)
            {
                AvailableSeats.Add(new Seat { SeatNumber = i, Status = "Available" });
            }
        }

        public Flight()
        {
        }

        public static Flight FromDto(FlightDTO flightDto)
        {
            Flight flight = new Flight(flightDto.Id, flightDto.DepartureCountry, flightDto.DestinationCountry,
                flightDto.DepartureDate, flightDto.ArrivalDate, flightDto.DepartureAirport,
                flightDto.DestinationAirport, flightDto.EconomyPrice, flightDto.BusinessPrice,
                flightDto.FirstClassPrice, flightDto.MaxSeatSize)
            {
                Id = null
            };
            CsvHelperService.AddToCsv(@"../../../Data/Flight.csv", flight);
            return flight;
        }

        public List<Seat> GetSeats()
        {
            return AvailableSeats;
        }


        public override string ToString()
        {
            var seatsInfo = string.Join(", ", AvailableSeats.Select(s => $"{s.SeatNumber}: {s.Status}"));
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

        public List<Passenger> GetPassengers()
        {
            return this.Passengers;
        }
    }

    public class Seat
    {
        public int SeatNumber { get; set; }
        public string Status { get; set; }
    }
}