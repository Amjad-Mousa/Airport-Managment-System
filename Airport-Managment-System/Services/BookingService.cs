using Airport_Management_System.Entities;
using Airport_Management_System.Helper;
namespace Airport_Management_System.Services
{
    public class BookingService
    {
        private readonly List<Booking?> Bookings;
        private string csvFilePath = @"../../../Data/Booking.csv";

        public BookingService()
        {
            this.Bookings = CsvHelperService.ReadFromCsv<Booking?>(csvFilePath) ?? new List<Booking?>();
        }
        public string PrintBookingDetails(int BookingID)
        {
            var booking = GetBookingById(BookingID);
            return booking != null ? booking.ToString() : "Booking not found.";
        }

        public bool UpdateBooking(Booking updatedBooking)
        {
            if (updatedBooking == null || updatedBooking.BookingId == 0)
            {
                Console.WriteLine( "Invalid booking data.");
                return false;   
            }

            var booking = GetBookingById(updatedBooking.BookingId);
            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return false;   
            }

            if (!string.IsNullOrWhiteSpace(updatedBooking.FlightId))
            {
                booking.FlightId = updatedBooking.FlightId;
            }

            if (updatedBooking.PassengerId > 0)
            {
                booking.PassengerId = updatedBooking.PassengerId;
            }

            if (!string.IsNullOrWhiteSpace(updatedBooking.BookingClass))
            {
                booking.BookingClass = updatedBooking.BookingClass;
            }

            if (updatedBooking.BookingDate != default)
            {
                if (updatedBooking.BookingDate<DateTime.Now.Date)
                {
                    Console.WriteLine("Booking date cannot be in the past.");
                    return false;
                }
                booking.BookingDate = updatedBooking.BookingDate;
            }

            if (updatedBooking.TotalPrice >= 0)
            {
                booking.TotalPrice = updatedBooking.TotalPrice;
            }
            CsvHelperService.WriteToCsv(csvFilePath, Bookings);
            Console.WriteLine("Booking updated successfully!");
            return true;        

        }

        public List<Booking?>? GetBookings()
        {
            return Bookings;
        }

        public bool DeleteBooking(int BookingID)
        {
            var flight = GetBookingById(BookingID);
            if (flight == null) {
                Console.WriteLine("Booking not found.");
                return false; }

            Bookings?.Remove(flight);
            CsvHelperService.WriteToCsv(csvFilePath, Bookings);
            Console.WriteLine( "Booking canceled successfully!");
            return true;    
        }

        public List<Booking> GetAllBookings(Passenger passenger)
        {
            if (PassengerService.IsPassengerExists(passenger.Id))
                return passenger.Bookings;
            return [];
        }

        public List<Booking> GetFillterdBookings(string Fillter)
        {
            if (Fillter.ToLower().Equals("date"))
                return FilterByBookingDate();

            if (Fillter.ToLower().Equals("price"))
                return FilterByTotalPrice();
            return new List<Booking>();
        }

        public bool CreateBooking(Booking booking)
        {
            if (Bookings == null)
            {
                Console.WriteLine("Invalid booking data.");
                return false;   
            }

            if (Bookings.Any(b => b?.BookingId == booking.BookingId))
            {
                Console.WriteLine("Booking already exists.");   
                return false;
            }

            Bookings.Add(booking);
            CsvHelperService.AddToCsv(csvFilePath, booking);
            Console.WriteLine("Booking created successfully!");
            return true;
        }
        private List<Booking?> FilterByBookingDate()
        {
            return Bookings.Where(b => b != null).OrderBy(b => b?.BookingDate).ToList();
        }
        private List<Booking?> FilterByTotalPrice()
        {
            return Bookings.Where(b => b != null).OrderBy(b => b?.TotalPrice).ToList();
        }
        public Booking? GetBookingById(int bookingId) => Bookings.FirstOrDefault(b => b?.BookingId == bookingId);
    } }
