using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Management_System.Entities;

namespace Airport_Management_System.Services
{
    public class FlightService
    {
        private readonly List<Flight?> flights;

        public FlightService(List<Flight?> flights)
        {
            this.flights = flights ?? throw new ArgumentNullException(nameof(flights), "Flights list cannot be null.");
        }

        private Flight? GetFlightById(string flightId)
        {
            if (string.IsNullOrWhiteSpace(flightId))
            {
                Console.WriteLine("Invalid flight ID.");
                return null;
            }

            return flights.FirstOrDefault(f => f?.Id == flightId);
        }

        public string? GetFlightDetails(string id)
        {
            var flight = GetFlightById(id);
            return flight != null ? flight.ToString() : "Flight not found.";
        }

        // Update the details of an existing flight
        public string UpdateFlight(Flight? updatedFlight)
        {
            if (updatedFlight == null || string.IsNullOrWhiteSpace(updatedFlight.Id))
            {
                return "Invalid flight data.";
            }

            var flight = GetFlightById(updatedFlight.Id);
            if (flight == null) return "Flight not found.";

            if (!string.IsNullOrWhiteSpace(updatedFlight.DepartureCountry))
                flight.DepartureCountry = updatedFlight.DepartureCountry;

            if (!string.IsNullOrWhiteSpace(updatedFlight.DestinationCountry))
                flight.DestinationCountry = updatedFlight.DestinationCountry;

            if (!string.IsNullOrWhiteSpace(updatedFlight.DepartureAirport))
                flight.DepartureAirport = updatedFlight.DepartureAirport;

            if (!string.IsNullOrWhiteSpace(updatedFlight.DestinationAirport))
                flight.DestinationAirport = updatedFlight.DestinationAirport;

            if (updatedFlight.DepartureDate != default)
                flight.DepartureDate = updatedFlight.DepartureDate;

            if (updatedFlight.ArrivalDate != default)
                flight.ArrivalDate = updatedFlight.ArrivalDate;

            if (updatedFlight.EconomyPrice > 0)
                flight.EconomyPrice = updatedFlight.EconomyPrice;

            if (updatedFlight.BusinessPrice > 0)
                flight.BusinessPrice = updatedFlight.BusinessPrice;

            if (updatedFlight.FirstClassPrice > 0)
                flight.FirstClassPrice = updatedFlight.FirstClassPrice;

            var updatedSeats = updatedFlight.GetSeats();
            if (updatedSeats.Count > 0)
            {
                foreach (var seat in updatedSeats)
                {
                    if (flight.GetSeats().ContainsKey(seat.Key))
                    {
                        flight.GetSeats()[seat.Key] = seat.Value;
                    }
                }
            }

            return "Flight updated successfully.";
        }
    }
}