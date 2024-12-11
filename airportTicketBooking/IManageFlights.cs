using System.Collections.Generic;

namespace airportTicketBooking
{
    public interface IManageFlights
    {
       
        List<Flight> GetAllFlights(); 
        Flight GetFlightById(int flightId);
        bool BookFlight(int flightId, int bookingId); 
    }

}