using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Management_System.Entities
{
    public class Booking

    {
        private static int _bookingId = 1;
        public string? FlightId { get; set; }
        public int PassengerId { get; set; }
        public string? BookingClass { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int BookingId { get; }
        public Booking(string flightId, int passengerId, string bookingClass, decimal totalPrice)
        {
            FlightId = flightId;
            PassengerId = passengerId;
            BookingClass = bookingClass;
            BookingDate = DateTime.Now;
            TotalPrice = totalPrice;
            BookingId = _bookingId++;
        }

        public override string ToString()
        {
            return $"Booking ID: {BookingId}, Flight ID: {FlightId}, Passenger ID: {PassengerId}, Booking Class: {BookingClass}, Booking Date: {BookingDate}, Total Price: {TotalPrice}";
        }
            
    }
}
