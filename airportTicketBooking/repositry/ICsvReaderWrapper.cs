using System.Collections.Generic;

namespace airportTicketBooking.repositry
{
    public interface ICsvReaderWrapper
    {
        IEnumerable<T> GetRecords<T>();

    }
}