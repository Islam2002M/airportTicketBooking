using System;
using System.Collections.Generic;
using System.Linq;
using airportTicketBooking.repositry;

namespace airportTicketBooking
{
    public class ManageFlights
    {
        private readonly FlightRep _flights = new();

        public List<Flight> FilterFlights(decimal? price, string departureCountry, string destinationCountry,
            DateTime? departureDate, string departureAirport, string arrivalAirport,string flightClass)
        {
            var f = _flights.GetFlights(@"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv");
            return f.Where(ele =>
                (string.Equals(ele.DepartureAirport, departureAirport, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(departureAirport)) &&
                (string.Equals(ele.DepartureCountry, departureCountry, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(departureCountry)) &&
                (ele.Price <= price || !price.HasValue) &&
                (string.Equals(ele.ArrivalAirport, arrivalAirport, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(arrivalAirport)) &&
                (string.Equals(ele.ArrivalCountry, destinationCountry, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(destinationCountry)) &&
                (string.Equals(ele.Class, flightClass, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(flightClass)) &&
                (ele.DepartureDate == departureDate || !departureDate.HasValue)
            ).ToList();

        }


        public Flight GetFlightById(int flightId)
        {
            var f = _flights.GetFlights(@"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv");
            return f.FirstOrDefault(ele => ele.FlightNumber == flightId);
        }
    }
}