using Airport_Management_System.Helper; 
using Airport_Management_System.Entities;

namespace Airport_Management_System.Services
{
    public class PassengerService
    {
        private readonly List<Passenger?> passengers;
        private string csvFilePath = @"../../../Data/Passengers.csv";
        public PassengerService()
        {
            this.passengers = CsvHelperService
                .ReadFromCsv<Passenger?>(csvFilePath) ?? new List<Passenger?>();
        }

        public static bool IsPassengerExists(int passengerId)
        {
            return CsvHelperService
                .ReadFromCsv<Passenger>("Passengers.csv")
                .Any(passenger => passenger.Id == passengerId);     
        }

        public string? GetPassengerDetails(int passengerId)
        {
            var passenger = GetPassengerById(passengerId);
            return passenger != null ? passenger
                .ToString() : "Passenger not found.";
        }



        private Passenger? GetPassengerById(int passengerId)
        {
            return passengers.FirstOrDefault(p => p?.Id == passengerId);
        }
    }

}
