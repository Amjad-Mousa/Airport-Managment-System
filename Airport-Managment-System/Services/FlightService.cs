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

        public Flight? GetFlightById(string flightId)
        {
            if (string.IsNullOrWhiteSpace(flightId))
            {
                Console.WriteLine("Invalid flight ID.");
                return null;
            }

            return flights.FirstOrDefault(f => f?.Id == flightId);
        }

        public string PrintFlightDetails(string id)
        {
            var flight = GetFlightById(id);
            return flight != null ? flight.ToString() : "Flight not found.";
        }

        public bool UpdateFlight(Flight? updatedFlight)
        {
            if (updatedFlight == null || string.IsNullOrWhiteSpace(updatedFlight.Id))
            {
                Console.WriteLine( "Invalid flight data.");
            }

            var flight = GetFlightById(updatedFlight.Id);
            if (flight == null) {
                Console.WriteLine("Flight not found.");
                return false;
            };

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
            Console.WriteLine("Flight updated successfully.");
            return true;
        }

        public bool CreateFlight(Flight flight)
        {
            if (flight == null)
            {
                Console.WriteLine("Invalid flight data.");
                return false;
            }

            if (flights.Any(f => f?.Id == flight.Id))
            {
                Console.WriteLine("Flight already exists.");
                return false;   
            }

            flights.Add(flight);
            CsvHelperService.AddToCsv(csvFilePath, flight);
            Console.WriteLine("Flight created successfully.");
            return true;
        }

        public bool DeleteFlight(string flightId)
        {
            var flight = GetFlightById(flightId);
            if (flight == null) {
                Console.WriteLine("Flight not found.");
                return false;   
            } 

            flights.Remove(flight);
            CsvHelperService.WriteToCsv(csvFilePath, flights);
            Console.WriteLine("Flight deleted successfully.");
            return true;    
        }

        public List<Flight?> GetFlights()
        {
            return flights;
        }

        public bool ReserveSeat(string flightId, int seatNumber)
        {
            var flight = GetFlightById(flightId);
            if (flight == null) 
            {
                Console.WriteLine("Flight not found.");
                return false;   
            }
            var seat = flight.AvailableSeats.FirstOrDefault(s => s.SeatNumber == seatNumber);
            if (seat == null) {Console.WriteLine("Seat not found."); return false; }
        

            if (seat.Status == "Booked")
            {
                Console.WriteLine("Seat already booked.");
                return false;       
            }

            seat.Status = "Booked";
            CsvHelperService.WriteToCsv(csvFilePath, flights);
            Console.WriteLine("Seat reserved successfully.");
            return true;
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
