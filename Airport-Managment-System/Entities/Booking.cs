using System;
using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace Airport_Management_System.Entities
{
    public class Booking
    {
        private static int _bookingId = 1;

        [Index(0)]
        [Name("BookingId")]
        public int BookingId { get; }

        [Index(1)]
        [Name("FlightId")]
        [Required]
        public string? FlightId { get; set; }

        [Index(2)]
        [Name("PassengerId")]
        [Required]
        public int PassengerId { get; set; }

        [Index(3)]
        [Name("BookingClass")]
        public string? BookingClass { get; set; }

        [Index(4)]
        [Name("BookingDate")]
        public DateTime BookingDate { get; set; }

        [Index(5)]
        [Name("TotalPrice")]
        public decimal TotalPrice { get; set; }

        public Booking(string flightId, int passengerId, string bookingClass, decimal totalPrice)
        {
            FlightId = flightId;
            PassengerId = passengerId;
            BookingClass = bookingClass;
            BookingDate = DateTime.Now;
            TotalPrice = totalPrice;
            BookingId = _bookingId++;
        }

        public Booking() { }

        public override string ToString()
        {
            return $"Booking ID: {BookingId}, Flight ID: {FlightId}, Passenger ID: {PassengerId}, Booking Class: {BookingClass}, Booking Date: {BookingDate}, Total Price: {TotalPrice}";
        }
    }
}