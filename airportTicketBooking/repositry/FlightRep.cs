using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace airportTicketBooking.repositry
{
    public class FlightRep:IFlightRep
    {
        private readonly IFileWrapper _fileWrapper;
        public FlightRep(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }
        public List<Flight> GetFlights(string flightsFilePath)
        {
            if (!_fileWrapper.Exists(flightsFilePath)) 
                throw new FileNotFoundException("Could not find file");

            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null,
                    HeaderValidated = null
                };

                using var reader = _fileWrapper.OpenFile(flightsFilePath);
                using var csv = new CsvReader(reader, config); 
                return csv.GetRecords<Flight>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading CSV file: {e.Message}");
                return new List<Flight>();
            }
        }

    }
}