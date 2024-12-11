using System.Collections.Generic;

namespace airportTicketBooking
{
    public interface IManageBookings
    {
        List<Booking> GetBookings(string filePath);
        void SaveBookings(List<Booking> bookings,string BookingsFilePath);
    }
}
