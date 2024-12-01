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
        
        FlightRep flightRep = new FlightRep();
        List<Flight> flights =flightRep.GetFlights(@"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv");
        Console.WriteLine(flights.Count);
        Console.WriteLine("Welcome to Airport Ticket Booking System, if you are a Manager press #1 ,if you are a passenger press #2");
        Start.start();
    }
}