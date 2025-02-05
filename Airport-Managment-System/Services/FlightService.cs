using Airport_Management_System.Entities;
using Airport_Management_System.Helper;
namespace Airport_Management_System.Services
{
    public class FlightService
    {
        private readonly List<Flight?> flights;
        private string csvFilePath = @"../../../Data/Flight.csv";

        public FlightService()
        {
            this.flights = CsvHelperService.ReadFromCsv<Flight?>(csvFilePath) ?? new List<Flight?>();
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

        public string GetFlightDetails(string id)
        {
            var flight = GetFlightById(id);
            return flight != null ? flight.ToString() : "Flight not found.";
        }

        public string UpdateFlight(Flight updatedFlight)
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

            if (updatedFlight.DepartureDate != default)
                flight.DepartureDate = updatedFlight.DepartureDate;

            if (updatedFlight.ArrivalDate != default)
                flight.ArrivalDate = updatedFlight.ArrivalDate;

            if (!string.IsNullOrWhiteSpace(updatedFlight.DepartureAirport))
                flight.DepartureAirport = updatedFlight.DepartureAirport;

            if (!string.IsNullOrWhiteSpace(updatedFlight.DestinationAirport))
                flight.DestinationAirport = updatedFlight.DestinationAirport;

            if (updatedFlight.EconomyPrice > 0)
                flight.EconomyPrice = updatedFlight.EconomyPrice;

            if (updatedFlight.BusinessPrice > 0)
                flight.BusinessPrice = updatedFlight.BusinessPrice;

            if (updatedFlight.FirstClassPrice > 0)
                flight.FirstClassPrice = updatedFlight.FirstClassPrice;

            if (updatedFlight.MaxSeatSize > 0)
            {
                flight.MaxSeatSize = updatedFlight.MaxSeatSize;
                flight.AvailableSeats.Clear();
                for (int i = 1; i <= flight.MaxSeatSize; i++)
                {
                    flight.AvailableSeats.Add(new Seat { SeatNumber = i, Status = "Available" });
                }
            }

            CsvHelperService.WriteToCsv(csvFilePath, flights);
            return "Flight updated successfully.";
        }

        public string CreateFlight(Flight flight)
        {
            if (flight == null)
            {
                return "Invalid flight data.";
            }

            if (flights.Any(f => f?.Id == flight.Id))
            {
                return "Flight with the given ID already exists.";
            }

            flights.Add(flight);
            CsvHelperService.AddToCsv(csvFilePath, flight);
            return "Flight created successfully!";
        }

        public string DeleteFlight(string flightId)
        {
            var flight = GetFlightById(flightId);
            if (flight == null) return "Flight not found.";

            flights.Remove(flight);
            CsvHelperService.WriteToCsv(csvFilePath, flights);
            return "Flight deleted successfully!";
        }

        public List<Flight?> GetFlights()
        {
            return flights;
        }

        public string ReserveSeat(string flightId, int seatNumber)
        {
            var flight = GetFlightById(flightId);
            if (flight == null) return "Flight not found.";
            var seat = flight.AvailableSeats.FirstOrDefault(s => s.SeatNumber == seatNumber);
            if (seat == null) return "Seat not found.";

            if (seat.Status == "Booked")
            {
                return "Seat already reserved.";
            }

            seat.Status = "Booked";
            CsvHelperService.WriteToCsv(csvFilePath, flights);
            return "Seat reserved successfully.";
        }

        public bool SeatExists(string flightId, int seatNumber)
        {
            var flight = GetFlightById(flightId);
            if (flight == null) return false;
            var seat = flight.AvailableSeats.FirstOrDefault(s => s.SeatNumber == seatNumber);
            return seat != null;
        }
    }
}
