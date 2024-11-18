using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace airportTicketBooking.repositry
{
    public class BookingRep
    {
        private const string BookingsFilePath = @"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\booking.csv";

        public List<Booking> GetBookings()
        {
            using var reader = new StreamReader(BookingsFilePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            return csv.GetRecords<Booking>().ToList();
        }
        public void SaveBookings(List<Booking> bookingList)
        {
            using var writer = new StreamWriter(BookingsFilePath);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecords(bookingList);
        }
    }
}