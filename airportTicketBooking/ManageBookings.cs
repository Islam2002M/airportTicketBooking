using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using airportTicketBooking.repositry;

namespace airportTicketBooking
{
    public class ManageBookings
    {
        private readonly BookingRep _bookings = new BookingRep();
        private readonly ManageFlights _manageFlights = new ManageFlights();

        public List<Booking> FilterBookings(int? flightId, decimal? price, string departureCountry,
            string destinationCountry, DateTime? departureDate, string departureAirport, string arrivalAirport,
            int? passengerId, string @class, string status)
        {
            List<Booking> bookingList = _bookings.GetBookings();
            return bookingList.Where(book => (book.Price == price || !price.HasValue) &&
                                             (book.DepartureCountry == departureCountry ||
                                              string.IsNullOrEmpty(departureCountry)) &&
                                             (book.DepartureDate == departureDate || !departureDate.HasValue) &&
                                             (book.FlightId == flightId || !flightId.HasValue) &&
                                             (book.DestinationCountry == destinationCountry ||
                                              string.IsNullOrEmpty(destinationCountry)) &&
                                             (book.DepartureAirport == departureAirport ||
                                              string.IsNullOrEmpty(departureAirport)) &&
                                             (book.ArrivalAirport == arrivalAirport ||
                                              string.IsNullOrEmpty(arrivalAirport)) &&
                                             (book.PassengerId == passengerId || !passengerId.HasValue) &&
                                             (book.Status == status || string.IsNullOrEmpty(status)) &&
                                             (book.Class == @class || string.IsNullOrEmpty(@class))).ToList();
        }

        public List<Booking> ViewPersonalBookings(int passengerId)
        {
            List<Booking> bookingList = _bookings.GetBookings();
            var personalBookings = bookingList.Where(booking => booking.PassengerId == passengerId).ToList();
            Console.WriteLine($"Found {personalBookings.Count} bookings for Passenger ID: {passengerId}");
            return personalBookings;
        }

        public void CancelBooking(int bookingId)
        {
            List<Booking> bookingList = _bookings.GetBookings();
            var tempBook = bookingList.SingleOrDefault(book => book.BookingNumber == bookingId);
            if (tempBook != null)
            {
                tempBook.Status = "Canceled";
                _bookings.SaveBookings(bookingList);
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
            List<Booking> bookingList = _bookings.GetBookings();
            var tempBook = bookingList.SingleOrDefault(book => book.BookingNumber == bookingId);
            if (tempBook != null)
            {
                tempBook.Status = status;
                _bookings.SaveBookings(bookingList);
                Console.WriteLine($@"status changes to {status} successfully.");
            }
            else
            {
                Console.WriteLine("Booking not found");
            }
        }

        public void ChangeBookingType(int bookingId, string classs)
        {
            List<Booking> bookingList = _bookings.GetBookings();
            var tempBook = bookingList.SingleOrDefault(book => book.BookingNumber == bookingId);
            if (tempBook != null)
            {
                tempBook.Class = classs;
                _bookings.SaveBookings(bookingList);
                Console.WriteLine($@"class changes to {classs} successfully.");
            }
            else
            {
                Console.WriteLine("Booking not found");
            }
        }
    }
}