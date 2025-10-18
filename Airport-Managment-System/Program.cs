using Airport_Management_System.Entities;
using Airport_Management_System.Services;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var flightService = new FlightService();
        var bookingService = new BookingService();
        var passengerService = new PassengerService();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Airport Management System");
            Console.WriteLine("1. Create Flight");
            Console.WriteLine("2. View Flight Details");
            Console.WriteLine("3. Update Flight");
            Console.WriteLine("4. Delete Flight");
            Console.WriteLine("5. Create Booking");
            Console.WriteLine("6. View Booking Details");
            Console.WriteLine("7. Cancel Booking");
            Console.WriteLine("8. Reserve Seat");
            Console.WriteLine("9. View Passenger Details");
            Console.WriteLine("0. Exit");
            Console.Write("Please select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateFlight(flightService);
                    break;

                case "2":
                    ViewFlightDetails(flightService);
                    break;

                case "3":
                    UpdateFlight(flightService);
                    break;

                case "4":
                    DeleteFlight(flightService);
                    break;

                case "5":
                    CreateBooking(bookingService);
                    break;

                case "6":
                    ViewBookingDetails(bookingService);
                    break;

                case "7":
                    CancelBooking(bookingService);
                    break;

                case "8":
                    ReserveSeat(flightService);
                    break;

                case "9":
                    ViewPassengerDetails(passengerService);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    private static void CreateFlight(FlightService flightService)
    {
        Console.WriteLine("Enter Flight Details:");

        Console.Write("Flight ID: ");
        string id = Console.ReadLine();

        Console.Write("Departure Country: ");
        string departureCountry = Console.ReadLine();

        Console.Write("Destination Country: ");
        string destinationCountry = Console.ReadLine();

        Console.Write("Departure Date (yyyy-mm-dd): ");
        DateTime departureDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Arrival Date (yyyy-mm-dd): ");
        DateTime arrivalDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Departure Airport: ");
        string departureAirport = Console.ReadLine();

        Console.Write("Destination Airport: ");
        string destinationAirport = Console.ReadLine();

        Console.Write("Economy Price: ");
        decimal economyPrice = decimal.Parse(Console.ReadLine());

        Console.Write("Business Price: ");
        decimal businessPrice = decimal.Parse(Console.ReadLine());

        Console.Write("First Class Price: ");
        decimal firstClassPrice = decimal.Parse(Console.ReadLine());

        Console.Write("Max Seat Size: ");
        int maxSeatSize = int.Parse(Console.ReadLine());

        Flight newFlight = new(id, departureCountry, destinationCountry, departureDate, arrivalDate, departureAirport, destinationAirport, economyPrice, businessPrice, firstClassPrice, maxSeatSize) { Id = null };

        Console.WriteLine(flightService.CreateFlight(newFlight));
    }

    private static void ViewFlightDetails(FlightService flightService)
    {
        Console.Write("Enter Flight ID: ");
        string flightId = Console.ReadLine();
        Console.WriteLine(flightService.GetFlightDetails(flightId));
    }

private static void UpdateFlight(FlightService flightService)
{
    Console.Write("Enter Flight ID to update: ");
    string flightId = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(flightId))
    {
        Console.WriteLine("Flight ID is required.");
        return;
    }

    Console.Write("New Departure Country (leave blank to keep current): ");
    string departureCountry = Console.ReadLine();

    Console.Write("New Destination Country (leave blank to keep current): ");
    string destinationCountry = Console.ReadLine();

    Console.Write("New Departure Date (yyyy-MM-dd HH:mm) or leave blank: ");
    string departureDateInput = Console.ReadLine();
    DateTime departureDate = DateTime.TryParse(departureDateInput, out DateTime parsedDepartureDate)
        ? parsedDepartureDate
        : default;

    Console.Write("New Arrival Date (yyyy-MM-dd HH:mm) or leave blank: ");
    string arrivalDateInput = Console.ReadLine();
    DateTime arrivalDate = DateTime.TryParse(arrivalDateInput, out DateTime parsedArrivalDate)
        ? parsedArrivalDate
        : default;

    Console.Write("New Departure Airport (leave blank to keep current): ");
    string departureAirport = Console.ReadLine();

    Console.Write("New Destination Airport (leave blank to keep current): ");
    string destinationAirport = Console.ReadLine();

    Console.Write("New Economy Price (leave blank to keep current): ");
    string economyPriceInput = Console.ReadLine();
    decimal economyPrice = decimal.TryParse(economyPriceInput, out decimal parsedEconomy)
        ? parsedEconomy
        : 0;

    Console.Write("New Business Price (leave blank to keep current): ");
    string businessPriceInput = Console.ReadLine();
    decimal businessPrice = decimal.TryParse(businessPriceInput, out decimal parsedBusiness)
        ? parsedBusiness
        : 0;

    Console.Write("New First Class Price (leave blank to keep current): ");
    string firstClassPriceInput = Console.ReadLine();
    decimal firstClassPrice = decimal.TryParse(firstClassPriceInput, out decimal parsedFirstClass)
        ? parsedFirstClass
        : 0;

    Console.Write("New Max Seat Size (leave blank to keep current): ");
    string maxSeatInput = Console.ReadLine();
    int maxSeatSize = int.TryParse(maxSeatInput, out int parsedMaxSeats)
        ? parsedMaxSeats
        : 0;

    Flight updatedFlight = new Flight
    {
        Id = flightId,
        DepartureCountry = string.IsNullOrWhiteSpace(departureCountry) ? null : departureCountry,
        DestinationCountry = string.IsNullOrWhiteSpace(destinationCountry) ? null : destinationCountry,
        DepartureDate = departureDate,
        ArrivalDate = arrivalDate,
        DepartureAirport = string.IsNullOrWhiteSpace(departureAirport) ? null : departureAirport,
        DestinationAirport = string.IsNullOrWhiteSpace(destinationAirport) ? null : destinationAirport,
        EconomyPrice = economyPrice,
        BusinessPrice = businessPrice,
        FirstClassPrice = firstClassPrice,
        MaxSeatSize = maxSeatSize
    };

    bool success = flightService.UpdateFlight(updatedFlight);

    if (success)
        Console.WriteLine("✅ Flight updated successfully.");
    else
        Console.WriteLine("❌ Flight update failed.");
}


    private static void DeleteFlight(FlightService flightService)
    {
        Console.Write("Enter Flight ID to delete: ");
        string flightId = Console.ReadLine();
        Console.WriteLine(flightService.DeleteFlight(flightId));
    }

    private static void CreateBooking(BookingService bookingService)
    {
        Console.Write("Enter Flight ID for Booking: ");
        string flightId = Console.ReadLine();

        Console.Write("Enter Passenger ID: ");
        int passengerId = int.Parse(Console.ReadLine());

        Console.Write("Enter Booking Class (Economy/Business/FirstClass): ");
        string bookingClass = Console.ReadLine();

        Console.Write("Enter Total Price: ");
        decimal totalPrice = decimal.Parse(Console.ReadLine());

        Booking newBooking = new Booking(flightId, passengerId, bookingClass, totalPrice);
        Console.WriteLine(bookingService.CreateBooking(newBooking));
    }

    private static void ViewBookingDetails(BookingService bookingService)
    {
        Console.Write("Enter Booking ID: ");
        int bookingId = int.Parse(Console.ReadLine());
        Console.WriteLine(bookingService.GetBookingDetails(bookingId));
    }

    private static void CancelBooking(BookingService bookingService)
    {
        Console.Write("Enter Booking ID to cancel: ");
        int bookingId = int.Parse(Console.ReadLine());
        Console.WriteLine(bookingService.DeleteBooking(bookingId));
    }

    private static void ReserveSeat(FlightService flightService)
    {
        Console.Write("Enter Flight ID: ");
        string flightId = Console.ReadLine();

        Console.Write("Enter Seat Number: ");
        int seatNumber = int.Parse(Console.ReadLine());

        Console.WriteLine(flightService.ReserveSeat(flightId, seatNumber));
    }

    private static void ViewPassengerDetails(PassengerService passengerService)
    {
        Console.Write("Enter Passenger ID: ");
        int passengerId = int.Parse(Console.ReadLine());
        Console.WriteLine(passengerService.GetPassengerDetails(passengerId));
    }
}
