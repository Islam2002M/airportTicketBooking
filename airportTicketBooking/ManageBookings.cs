using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using airportTicketBooking.repositry;

namespace airportTicketBooking
{
    public class ManageBookings
    {
        static FileWrapper fileWrapper = new FileWrapper();
        static FlightRep flightRep = new FlightRep(fileWrapper);
        private readonly BookingRep _bookings = new BookingRep(fileWrapper);
        private readonly ManageFlights _manageFlights = new ManageFlights(flightRep);
        private const string BookingsFilePath = @"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\booking.csv";


        public List<Booking> FilterBookings(int? flightId, decimal? price, string departureCountry,
            string destinationCountry, DateTime? departureDate, string departureAirport, string arrivalAirport,
            int? passengerId, string @class, string status)
        {
            List<Booking> bookingList = _bookings.GetBookings(BookingsFilePath);
            return bookingList.Where(book =>
                (book.Price == price || !price.HasValue) &&
                (string.Equals(book.DepartureCountry, departureCountry, StringComparison.OrdinalIgnoreCase) ||
                 string.IsNullOrEmpty(departureCountry)) &&
                (book.DepartureDate == departureDate || !departureDate.HasValue) &&
                (book.FlightId == flightId || !flightId.HasValue) &&
                (string.Equals(book.DestinationCountry, destinationCountry, StringComparison.OrdinalIgnoreCase) ||
                 string.IsNullOrEmpty(destinationCountry)) &&
                (string.Equals(book.DepartureAirport, departureAirport, StringComparison.OrdinalIgnoreCase) ||
                 string.IsNullOrEmpty(departureAirport)) &&
                (string.Equals(book.ArrivalAirport, arrivalAirport, StringComparison.OrdinalIgnoreCase) ||
                 string.IsNullOrEmpty(arrivalAirport)) &&
                (book.PassengerId == passengerId || !passengerId.HasValue) &&
                (string.Equals(book.Status, status, StringComparison.OrdinalIgnoreCase) ||
                 string.IsNullOrEmpty(status)) &&
                (string.Equals(book.Class, @class, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(@class))
            ).ToList();
        }

        public List<Booking> ViewPersonalBookings(int passengerId)
        {
            List<Booking> bookingList = _bookings.GetBookings(BookingsFilePath);
            var personalBookings = bookingList.Where(booking => booking.PassengerId == passengerId).ToList();
            Console.WriteLine($"Found {personalBookings.Count} bookings for Passenger ID: {passengerId}");
            return personalBookings;
        }

        public void CancelBooking(int bookingId)
        {
            List<Booking> bookingList = _bookings.GetBookings(BookingsFilePath);
            var tempBook = bookingList.SingleOrDefault(book => book.BookingNumber == bookingId);
            if (tempBook != null)
            {
                tempBook.Status = "Canceled";
                _bookings.SaveBookings(bookingList,BookingsFilePath);
                Console.WriteLine("Booking canceled");
            }
            else
            {
                Console.WriteLine("Booking not found");
            }
        }

        public bool BookAFlight(int flightId, string @class, int passengerId)
        {
            var flight = _manageFlights.GetFlightById(flightId);
            if (flight == null)
            {
                Console.WriteLine("Flight not found");
                return false;
            }

            Guid flightNumber = Guid.NewGuid();
            int flightNumberAsInt = BitConverter.ToInt32(flightNumber.ToByteArray(), 0);
            flightNumberAsInt = flightNumberAsInt < 0 ? ~flightNumberAsInt + 1 : flightNumberAsInt;

            var booking = new Booking
            {
                BookingNumber = flightNumberAsInt,
                FlightId = flight.FlightNumber,
                DepartureCountry = flight.DepartureCountry,
                DestinationCountry = flight.ArrivalCountry,
                DepartureDate = flight.DepartureDate,
                DepartureAirport = flight.DepartureAirport,
                ArrivalAirport = flight.ArrivalAirport,
                PassengerId = passengerId,
                Status = "Pending",
                Class = @class,
                Price = Flight.calculateprice(@class)
            };
            string bookingFilePath = @"C:\Users\msi\RiderProjects\airportTicketBooking\airportTicketBooking\data\booking.csv";
            try
            {
                var bookingData = $"{booking.BookingNumber},{booking.FlightId},{booking.DepartureCountry},{booking.DestinationCountry},{booking.DepartureDate},{booking.DepartureAirport},{booking.ArrivalAirport},{booking.PassengerId},{booking.Class},{booking.Status},{booking.Price}";
                File.AppendAllText(bookingFilePath, bookingData + "\n");
                Console.WriteLine("Booking added successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving booking: " + ex.Message);
                return false;
            }
        }

        public void ChangeBookingStatus(int bookingId, string status)
        {
            List<Booking> bookingList = _bookings.GetBookings(BookingsFilePath);
            var tempBook = bookingList.SingleOrDefault(book => book.BookingNumber == bookingId);
            if (tempBook != null)
            {
                tempBook.Status = status;
                _bookings.SaveBookings(bookingList,BookingsFilePath);
                Console.WriteLine($@"status changes to {status} successfully.");
            }
            else
            {
                Console.WriteLine("Booking not found");
            }
        }

        public void ChangeBookingType(int bookingId, string classs)
        {
            List<Booking> bookingList = _bookings.GetBookings(BookingsFilePath);
            var tempBook = bookingList.SingleOrDefault(book => book.BookingNumber == bookingId);
            if (tempBook != null)
            {
                tempBook.Class = classs;
                _bookings.SaveBookings(bookingList,BookingsFilePath);
                Console.WriteLine($@"class changes to {classs} successfully.");
            }
            else
            {
                Console.WriteLine("Booking not found");
            }
        }
    }
}