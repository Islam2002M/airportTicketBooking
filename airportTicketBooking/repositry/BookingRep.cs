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
        private IFileWrapper _fileWrapper;

        public BookingRep(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }
        public virtual List<Booking> GetBookings(string BookingsFilePath)
        {
            if (!_fileWrapper.Exists(BookingsFilePath)) 
                throw new FileNotFoundException($"File not found");
            try
            {
                using var reader = _fileWrapper.OpenFile(BookingsFilePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csv.GetRecords<Booking>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Booking>();
            }
        }

        public void SaveBookings(List<Booking> bookingList,string BookingsFilePath)
        {
            if (!_fileWrapper.Exists(BookingsFilePath)) 
                throw new FileNotFoundException($"File not found");
            try
            {
                using var writer = _fileWrapper.CreateFile(BookingsFilePath);
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