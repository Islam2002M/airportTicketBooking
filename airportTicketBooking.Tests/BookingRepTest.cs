using System.IO;
using airportTicketBooking.repositry;
using Xunit;
using Moq;

namespace airportTicketBooking.Tests
{
    public class BookingRepTest
    {
        private const string MockFilePath = @"mockPath.csv";
        [Fact]
        public void GetBookings_ShouldReturnBookings()
        {
            var mockData = "BookingNumber,FlightId,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,ArrivalAirport,PassengerID,Class,Status,Price\n1,100,USA,Canada,2024-11-26,NY,Toronto,101,Business,Confirmed,300";
            var mockReader = new Mock<TextReader>();
            mockReader.SetupSequence(r => r.ReadLine())
                .Returns(mockData)
                .Returns((string)null);

            var bookingRep = new BookingRep();

            var result = bookingRep.GetBookings();

            Assert.NotNull(result);
        }
    }
    
}