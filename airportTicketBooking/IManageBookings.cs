using System.Collections.Generic;

namespace airportTicketBooking
{
    public interface IManageBookings
    {
        List<Booking> GetBookings(string filePath);
    }
}