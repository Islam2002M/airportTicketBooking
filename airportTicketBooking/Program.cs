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
        
        FlightRep f = new FlightRep();
        List<Flight> ff =f.GetFlights(@"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv");
        Console.WriteLine(ff.Count);
        Console.WriteLine("Welcome to Airport Ticket Booking System, if you are a Manager press #1 ,if you are a passenger press #2");
        int.TryParse(Console.ReadLine(),out int UserType);
        Start.start(UserType);
    }
}