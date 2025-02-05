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
        public string GetBookingDetails(int BookingID)
        {
            var booking = GetBookingById(BookingID);
            return booking != null ? booking.ToString() : "Booking not found.";
        }

        public string UpdateBooking(Booking updatedBooking)
        {
            if (updatedBooking == null || updatedBooking.BookingId == 0)
            {
                return "Invalid booking data.";
            }

            var booking = GetBookingById(updatedBooking.BookingId);
            if (booking == null)
            {
                return "Booking not found.";
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
                booking.BookingDate = updatedBooking.BookingDate;
            }

            if (updatedBooking.TotalPrice >= 0)
            {
                booking.TotalPrice = updatedBooking.TotalPrice;
            }
            CsvHelperService.WriteToCsv(csvFilePath, Bookings);
            return "Booking updated successfully.";

        }

        public List<Booking?>? GetBookings()
        {
            return Bookings;
        }

        public string DeleteBooking(int BookingID)
        {
            var flight = GetBookingById(BookingID);
            if (flight == null) return "Booking not found.";

            Bookings?.Remove(flight);
            CsvHelperService.WriteToCsv(csvFilePath, Bookings);
            return "Booking canceled successfully!";
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

        public string CreateBooking(Booking booking)
        {
            if (Bookings == null)
            {
                return "Invalid Booking data.";
            }

            if (Bookings.Any(b => b?.BookingId == booking.BookingId))
            {
                return "Booking with the given ID already exists.";
            }

            Bookings.Add(booking);
            CsvHelperService.AddToCsv(csvFilePath, booking);
            return "Booking created successfully!";
        }
        private List<Booking?> FilterByBookingDate()
        {
            return Bookings.Where(b => b != null).OrderBy(b => b?.BookingDate).ToList();
        }
        private List<Booking?> FilterByTotalPrice()
        {
            return Bookings.Where(b => b != null).OrderBy(b => b?.TotalPrice).ToList();
        }
        private Booking? GetBookingById(int bookingId) => Bookings.FirstOrDefault(b => b?.BookingId == bookingId);
    } }
