using Airport_Management_System.Services;
using Airport_Management_System.Entities;
namespace Airport_Management_System
{
    internal abstract class Program
    {
        static void Main()
        {
            FlightService flightService = new FlightService();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Flight Management System");
                Console.WriteLine("1. Create Flight");
                Console.WriteLine("2. Update Flight");
                Console.WriteLine("3. View Flight Details");
                Console.WriteLine("4. Delete Flight");
                Console.WriteLine("5. Reserve Seat");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                
                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateFlight(flightService);
                        break;
                    case "2":
                        UpdateFlight(flightService);
                        break;
                    case "3":
                        ViewFlightDetails(flightService);
                        break;
                    case "4":
                        DeleteFlight(flightService);
                        break;
                    case "5":
                        ReserveSeat(flightService);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CreateFlight(FlightService flightService)
        {
            Console.WriteLine("Create a new flight");

            Flight newFlight = new Flight
            {
                Id = GetInput("Flight ID: "),
                DepartureCountry = GetInput("Departure Country: "),
                DestinationCountry = GetInput("Destination Country: "),
                DepartureDate = DateTime.Parse(GetInput("Departure Date (yyyy-mm-dd): ")),
                ArrivalDate = DateTime.Parse(GetInput("Arrival Date (yyyy-mm-dd): ")),
                DepartureAirport = GetInput("Departure Airport: "),
                DestinationAirport = GetInput("Destination Airport: "),
                EconomyPrice = decimal.Parse(GetInput("Economy Price: ")),
                BusinessPrice = decimal.Parse(GetInput("Business Price: ")),
                FirstClassPrice = decimal.Parse(GetInput("First Class Price: ")),
                MaxSeatSize = int.Parse(GetInput("Max Seat Size: "))
            };

            string result = flightService.CreateFlight(newFlight);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static void UpdateFlight(FlightService flightService)
        {
            Console.WriteLine("Update a flight");

            string flightId = GetInput("Enter flight ID to update: ");
            Flight updatedFlight = new Flight
            {
                Id = flightId,
                DepartureCountry = GetInput("New Departure Country (leave empty to skip): "),
                DestinationCountry = GetInput("New Destination Country (leave empty to skip): "),
                DepartureDate = GetDateInput("New Departure Date (leave empty to skip): "),
                ArrivalDate = GetDateInput("New Arrival Date (leave empty to skip): "),
                DepartureAirport = GetInput("New Departure Airport (leave empty to skip): "),
                DestinationAirport = GetInput("New Destination Airport (leave empty to skip): "),
                EconomyPrice = GetDecimalInput("New Economy Price (leave empty to skip): "),
                BusinessPrice = GetDecimalInput("New Business Price (leave empty to skip): "),
                FirstClassPrice = GetDecimalInput("New First Class Price (leave empty to skip): "),
                MaxSeatSize = GetIntInput("New Max Seat Size (leave empty to skip): ")
            };

            string result = flightService.UpdateFlight(updatedFlight);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static void ViewFlightDetails(FlightService flightService)
        {
            Console.WriteLine("View Flight Details");

            string flightId = GetInput("Enter flight ID to view: ");
            string result = flightService.GetFlightDetails(flightId);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static void DeleteFlight(FlightService flightService)
        {
            Console.WriteLine("Delete a flight");

            string flightId = GetInput("Enter flight ID to delete: ");
            string result = flightService.DeleteFlight(flightId);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static void ReserveSeat(FlightService flightService)
        {
            Console.WriteLine("Reserve a seat");

            string flightId = GetInput("Enter flight ID: ");
            int seatNumber = int.Parse(GetInput("Enter seat number: "));
            string result = flightService.ReserveSeat(flightId, seatNumber);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        static string GetInput(string prompt)
        {
            string? input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }


        static DateTime GetDateInput(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? default : DateTime.Parse(input);
        }

        static decimal GetDecimalInput(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? 0 : decimal.Parse(input);
        }

        static int GetIntInput(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? 0 : int.Parse(input);
        }
    }
}
