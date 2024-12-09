using System.Collections.Generic;

namespace airportTicketBooking.repositry
{
    public interface IFlightRep
    {
        List<Flight> GetFlights(string flightsFilePath);

    }
}