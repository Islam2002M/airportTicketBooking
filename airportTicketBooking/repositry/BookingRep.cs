using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace airportTicketBooking.repositry
{
    public class BookingRep
    {
        private const string BookingsFilePath = @"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\booking.csv";

        public List<Booking> GetBookings()
        {
            if (!File.Exists(BookingsFilePath)) 
                throw new FileNotFoundException($"File {BookingsFilePath} not found");
            try
            {
                using var reader = new StreamReader(BookingsFilePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csv.GetRecords<Booking>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Booking>();
            }
        }

        public void SaveBookings(List<Booking> bookingList)
        {
            if (!File.Exists(BookingsFilePath)) throw new FileNotFoundException($"File {BookingsFilePath} not found");
            try
            {
                using var writer = new StreamWriter(BookingsFilePath);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(bookingList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}