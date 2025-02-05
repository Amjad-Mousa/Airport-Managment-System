using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;
using Airport_Management_System.Helper;
using System.Collections.Generic;

namespace Airport_Management_System.Entities
{
    public class Passenger
    {
        [Index(0)]
        [Name("Id")]
        public int Id { get; set; }

        [Index(1)]
        [Name("FirstName")]
        [Required]
        public string? FirstName { get; set; }

        [Index(2)]
        [Name("LastName")]
        [Required]
        public string? LastName { get; set; }

        [Index(3)]
        [Name("PhoneNumber")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Index(4)]
        [Name("Email")]
        [EmailAddress]
        public string? Email { get; set; }

        public List<Booking?>? Bookings { get; set; } = new List<Booking?>();
        public Passenger() { }  

        public Passenger(int id, string? firstName, string? lastName, string? phoneNumber, string? email, List<Booking?>? bookings)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Bookings = new List<Booking?>();
        }
    
        public override string ToString()
        {
            return $"Passenger ID: {Id}, First Name: {FirstName}, Last Name: {LastName}, Phone Number: {PhoneNumber}, Email: {Email}";
        }

    }
}