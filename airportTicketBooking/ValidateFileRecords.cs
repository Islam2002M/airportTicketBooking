using System;
using System.Collections.Generic;
using System.IO;
using airportTicketBooking.repositry;

namespace airportTicketBooking
{
    public class ValidateFileRecords
    {
        public List<string> ValidateFileRecordss(string filePath)
        {
            FlightRep flightRep = new FlightRep();
            string flightsPath=@"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv";
            List<Flight> records = flightRep.GetFlights(filePath);
            List<string> errors = new List<string>(); 
            foreach (var flight in records)
            {
                var error = validateFlight(flight);
                if (error != null && error.Count > 0)
                {
                    errors.Add($"Errors in Flight {flight.FlightNumber}:");
                    errors.AddRange(error);
                }
            }

            if (errors.Count == 0) 
            {
                foreach (var flight in records)
                {
                    Guid newGuid = Guid.NewGuid();
                    int bookingId = BitConverter.ToInt32(newGuid.ToByteArray(), 0);
                    flight.FlightNumber = bookingId; 

                    using (StreamWriter writer = new StreamWriter(flightsPath, append: true))
                    {
                        writer.WriteLine($"{bookingId},{flight.DepartureCountry},{flight.ArrivalCountry},{flight.DepartureDate},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.Class},{flight.Price}");
                    }
                }

                Console.WriteLine("Data saved to CSV file at: " + filePath);
            }

            return errors;
        }

        private List<string> validateFlight(Flight flight)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(flight.DepartureCountry))
                errors.Add("Departure country is required");

            if (string.IsNullOrEmpty(flight.ArrivalCountry))
                errors.Add("Arrival country is required");

            if (flight.DepartureCountry == flight.ArrivalCountry)
                errors.Add("Departure country cannot be the same as Arrival country");

            if (flight.DepartureDate == default(DateTime))
                errors.Add("Departure date is required and should be a valid date");

            if (flight.DepartureDate < DateTime.Now)
                errors.Add("Departure date cannot be earlier than today");

            if (string.IsNullOrEmpty(flight.DepartureAirport))
                errors.Add("Departure airport is required");

            if (string.IsNullOrEmpty(flight.ArrivalAirport))
                errors.Add("Arrival airport is required");

            if (flight.ArrivalAirport == flight.DepartureAirport)
                errors.Add("Arrival airport cannot be the same as Departure airport");

            if (string.IsNullOrEmpty(flight.Class))
                errors.Add("Flight class is required");

            if (flight.Price <= 0)
                errors.Add("Flight price should be greater than 0");

            return errors;
        }
    }
}
