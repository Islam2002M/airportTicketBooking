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
            FileWrapper file = new FileWrapper();
            FlightRep flightRep = new FlightRep(file);
            string flightsPath=@"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv";
            List<Flight> records = flightRep.GetFlights(filePath);
            List<string> errors = new List<string>(); 
            foreach (var flight in records)
            {
                var error = ValidateFlight(flight);
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
                    Guid flightId = Guid.NewGuid();
                    int flightIdInt = BitConverter.ToInt32(flightId.ToByteArray(), 0);
                    if (flightIdInt < 0) flightIdInt = ~ flightIdInt;
                    flight.FlightNumber = flightIdInt; 

                    using (StreamWriter writer = new StreamWriter(flightsPath, append: true))
                    {
                        writer.WriteLine($"{flightIdInt},{flight.DepartureCountry},{flight.ArrivalCountry},{flight.DepartureDate},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.Class},{flight.Price}");
                    }
                }

                Console.WriteLine("There is No errors in the data , Data saved to CSV file at: " + filePath);
            }

            return errors;
        }

        private List<string> ValidateFlight(Flight flight)
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
            
            if (string .IsNullOrEmpty(flight.Class))
                errors.Add("Class is required");
            
            if (flight.Price <= 0)
                errors.Add("Flight price should be greater than 0");

            return errors;
        }
    }
}
