using System;
using System.Collections.Generic;
using System.Linq;
using airportTicketBooking.repositry;

namespace airportTicketBooking
{
    public class ManageFlights
    {
        private FlightRep flights = new();

        public List<Flight> FilterFlights(decimal? price, string departureCountry, string destinationCountry,
            DateTime? departureDate, string departureAirport, string arrivalAirport, string @class)
        {
            var f = flights.GetFlights(
                @"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv");
            return f.Where(ele =>
                    (ele.DepartureAirport == departureAirport || string.IsNullOrEmpty(departureAirport)) &&
                    (ele.DepartureCountry == departureCountry || string.IsNullOrEmpty(departureCountry)) &&
                    (ele.Class == @class || string.IsNullOrEmpty(@class)) && (ele.Price <= price || !price.HasValue) &&
                    (ele.ArrivalAirport == arrivalAirport || string.IsNullOrEmpty(arrivalAirport)) &&
                    (ele.ArrivalCountry == destinationCountry || string.IsNullOrEmpty(destinationCountry)) &&
                    (ele.DepartureDate == departureDate || !departureDate.HasValue))
                .ToList();
        }


        public Flight GetFlightById(int flightId)
        {
            var f =
                flights.GetFlights(
                    @"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv");
            return f.FirstOrDefault(ele => ele.FlightNumber == flightId);
        }
    }
}