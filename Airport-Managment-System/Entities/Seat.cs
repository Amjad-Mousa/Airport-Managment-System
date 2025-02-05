using CsvHelper.Configuration.Attributes;

public class Seat
{
    [Index(0)]
    [Name("SeatNumber")]
    public int SeatNumber { get; set; }

    [Index(1)]
    [Name("Status")]
    public string Status { get; set; }
}
