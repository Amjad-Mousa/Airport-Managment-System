using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Airport_Management_System.Helper
{
    public abstract class CsvHelperService
    {
        public static List<T> ReadFromCsv<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim
            });

            return csv.GetRecords<T>().ToList();
        }

        public static void WriteToCsv<T>(string filePath, List<T> records)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecords(records);
        }

        public static void AddToCsv<T>(string filePath, T record)
        {
            using var writer = new StreamWriter(filePath, append: true);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecord(record);
            csv.NextRecord();
        }

        public static bool DeleteFromCsv<T>(string filePath, Func<T, bool> predicate)
        {
            var records = ReadFromCsv<T>(filePath);

            if (!records.Any(predicate))
            {
                return false;
            }

            var updatedRecords = records.Where(record => !predicate(record)).ToList();

            WriteToCsv(filePath, updatedRecords);

            return true;
        }
    }
}
