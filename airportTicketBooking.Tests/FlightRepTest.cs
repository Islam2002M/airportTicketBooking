using System.IO;
using airportTicketBooking.repositry;
using Moq;
using Xunit;

namespace airportTicketBooking.Tests
{
    public class FlightRepTest
    {
        private const string MockFilePath = @"mockPath.csv";

        [Fact]
        public void GetFlights_ShouldReturnFlights()
        {
            var mockReader = new Mock<TextReader>();
            mockReader.SetupSequence(r => r.ReadLine())
                .Returns((string)null);

            var bookingRep = new BookingRep();
        }
    }
}