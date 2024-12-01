﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace airportTicketBooking.repositry
{
    public class FlightRep
    {
        public List<Flight> GetFlights(string flightsFilePath)
        {
            if (!File.Exists(flightsFilePath)) 
                throw new FileNotFoundException();
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null, HeaderValidated = null
                };

                using var reader = new StreamReader(flightsFilePath);
                using var csv = new CsvReader(reader, config);

                return csv.GetRecords<Flight>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Flight>();
            }
        }
    }
}