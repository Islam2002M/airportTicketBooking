using System;
using System.Collections.Generic;
using System.Linq;
using airportTicketBooking.repositry;

namespace airportTicketBooking
{
    public class ManageFlights:IManageFlights
    {
        
        private readonly IFlightRep _flights;

        public ManageFlights(IFlightRep flights)
        {
            _flights=flights;
        }
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


        public List<Flight> GetAllFlights()
        {
            throw new NotImplementedException();
        }

        public virtual Flight GetFlightById(int flightId)
        {
            var f = _flights.GetFlights(@"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\flights.csv");
            return f.FirstOrDefault(ele => ele.FlightNumber == flightId);
        }

        public bool BookFlight(int flightId, int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}