using System.Collections.Generic;
using System.IO;
using airportTicketBooking.repositry;
using CsvHelper;
using CsvHelper.Configuration;

public class CsvReaderWrapper : ICsvReaderWrapper
{
    private readonly CsvReader _csvReader;

    public CsvReaderWrapper(StreamReader reader, CsvConfiguration config)
    {
        _csvReader = new CsvReader(reader, config);
    }

    public IEnumerable<T> GetRecords<T>()
    {
        return _csvReader.GetRecords<T>();
    }
}