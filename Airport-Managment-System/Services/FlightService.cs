using Airport_Management_System.Entities;
using Airport_Management_System.Helper;
namespace Airport_Management_System.Services
{
    public class FlightService
    {
        private readonly List<Flight?> flights;
        private  string csvFilePath = @"../../../Data/Flight.csv";

        public FlightService()
        {
            try
            {
                this.flights = CsvHelperService.ReadFromCsv<Flight>(csvFilePath) ?? new List<Flight>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
                this.flights = new List<Flight>(); 
            }
        }

        private Flight? GetFlightById(string flightId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(flightId))
                {
                    Console.WriteLine("Invalid flight ID.");
                    return null;
                }

                return flights.FirstOrDefault(f => f?.Id == flightId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting flight by ID: {ex.Message}");
                return null;
            }
        }

        public string GetFlightDetails(string id)
        {
            try
            {
                var flight = GetFlightById(id);
                return flight != null ? flight.ToString() : "Flight not found.";
            }
            catch (Exception ex)
            {
                return $"Error retrieving flight details: {ex.Message}";
            }
        }

        public string UpdateFlight(Flight updatedFlight)
        {
            try
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
            catch (Exception ex)
            {
                return $"Error updating flight: {ex.Message}";
            }
        }

        public string CreateFlight(Flight flight)
        {
            try
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
            catch (Exception ex)
            {
                return $"Error creating flight: {ex.Message}";
            }
        }

        public string DeleteFlight(string flightId)
        {
            try
            {
                var flight = GetFlightById(flightId);
                if (flight == null) return "Flight not found.";

                flights.Remove(flight);
                CsvHelperService.WriteToCsv(csvFilePath, flights); // Save changes to CSV
                return "Flight deleted successfully!";
            }
            catch (Exception ex)
            {
                return $"Error deleting flight: {ex.Message}";
            }
        }

        public List<Flight> GetFlights()
        {
            try
            {
                return flights;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving flights: {ex.Message}");
                return new List<Flight>();
            }
        }

        public string ReserveSeat(string flightId, int seatNumber)
        {
            try
            {
                var flight = GetFlightById(flightId);
                if (flight == null) return "Flight not found.";
                var seat = flight.GetSeats().FirstOrDefault(s => s.SeatNumber == seatNumber);
                if (seat == null) return "Seat not found.";

                if (seat.Status == "Booked")
                {
                    return "Seat already reserved.";
                }

                seat.Status = "Booked";
                CsvHelperService.WriteToCsv(csvFilePath, flights);
                return "Seat reserved successfully.";
            }
            catch (Exception ex)
            {
                return $"Error reserving seat: {ex.Message}";
            }
        }

        public bool SeatExists(string flightId, int seatNumber)
        {
            try
            {
                var flight = GetFlightById(flightId);
                if (flight == null) return false;
                var seat = flight.GetSeats().FirstOrDefault(s => s.SeatNumber == seatNumber);
                return seat != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking seat existence: {ex.Message}");
                return false;
            }
        }
    }
}
