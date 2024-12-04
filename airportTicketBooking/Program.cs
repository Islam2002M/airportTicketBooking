using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using airportTicketBooking;
using airportTicketBooking.repositry;
using CsvHelper;

class Program
{
    static void Main()
    {
        FileWrapper file = new FileWrapper();
        FlightRep flightRep = new FlightRep(file);
        List<Flight> flights =flightRep.GetFlights(@"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv");
        Console.WriteLine(flights.Count);
        Start.start();
    }
}