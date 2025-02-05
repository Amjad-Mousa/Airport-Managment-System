using Airport_Management_System.Helper; 
namespace Airport_Management_System.Entities
{

    public class Passenger
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public List<Booking?>? Bookings { get; set; }
        public override string ToString()
        {
            return $"Passenger ID: {Id}, First Name: {FirstName}, Last Name: {LastName}, Phone Number: {PhoneNumber}, Email: {Email}";      
        }

    }
}