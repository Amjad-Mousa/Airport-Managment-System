using CsvHelper.Configuration;
using Airport_Management_System.Entities;

namespace Airport_Management_System.Helper
{
    public sealed class FlightMap : ClassMap<Flight>
    {
        public FlightMap()
        {
            Map(m => m.Id).Name("FlightID");
            Map(m => m.DepartureCountry).Name("DepartureCountry");
            Map(m => m.DestinationCountry).Name("DestinationCountry");
            Map(m => m.DepartureDate).Name("DepartureDate");
            Map(m => m.ArrivalDate).Name("ArrivalDate");
            Map(m => m.DepartureAirport).Name("DepartureAirport");
            Map(m => m.DestinationAirport).Name("DestinationAirport");
            Map(m => m.EconomyPrice).Name("EconomyPrice");
            Map(m => m.BusinessPrice).Name("BusinessPrice");
            Map(m => m.FirstClassPrice).Name("FirstClassPrice");
            Map(m => m.MaxSeatSize).Name("MaxSeatSize");
        }
    }
}