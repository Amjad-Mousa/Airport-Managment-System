using Airport_Management_System.Helper; 
using Airport_Management_System.Entities;

namespace Airport_Management_System.Services
{
    public class PassengerService
    {
        private readonly List<Passenger?> passengers;
        private string csvFilePath = @"../../../Data/Passenger.csv";
        public PassengerService()
        {
            this.passengers = CsvHelperService
                .ReadFromCsv<Passenger?>(csvFilePath) ?? new List<Passenger?>();
        }

        public static bool IsPassengerExists(int passengerId)
        {
            return  
                passengers.Any(passenger => passenger.Id == passengerId);     
        }

        public Passenger? PrintPassengerDetails(int passengerId)
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
