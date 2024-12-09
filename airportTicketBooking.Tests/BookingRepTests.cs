using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using airportTicketBooking.repositry;
using AutoFixture;
using Moq;
using Xunit;

namespace airportTicketBooking.Tests
{
    public class BookingRepTests
    {
        [Fact]
        public void GetBookings_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            var fileWrapper = new Mock<IFileWrapper>();
            var stub = new BookingRep(fileWrapper.Object);
            fileWrapper.Setup(p => p.Exists(It.IsAny<string>())).Returns(false);

            var exception = Assert.Throws<FileNotFoundException>(() => stub.GetBookings("notexist.csv"));
            Assert.Equal("File not found", exception.Message);

        }

        [Fact]
        public void GetBookings_ShouldReturnBookings_WhenFileExists()
        {
            var fileWrapperMock = new Mock<IFileWrapper>();
            fileWrapperMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);

            var fixture = new Fixture();
            var bookings = fixture.CreateMany<Booking>(3).ToList();

            var csvContent =
                "BookingNumber,FlightId,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,ArrivalAirport,PassengerId,Class,Status,Price\n" +
                string.Join("\n",
                    bookings.Select(b =>
                        $"{b.BookingNumber},{b.FlightId},{b.DepartureCountry},{b.DestinationCountry},{b.DepartureDate:O},{b.DepartureAirport},{b.ArrivalAirport},{b.PassengerId},{b.Class},{b.Status},{b.Price}"));

            var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvContent));
            var streamReader = new StreamReader(memoryStream);

            fileWrapperMock.Setup(f => f.OpenFile(It.IsAny<string>())).Returns(streamReader);

            var bookingRep = new BookingRep(fileWrapperMock.Object);

            var result = bookingRep.GetBookings("valid.csv");

            Assert.Equal(3, result.Count);
            Assert.Equal(bookings.Select(f => f.BookingNumber), result.Select(f => f.BookingNumber));
        }

        [Fact]
        public void SaveBookings_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            List<Booking> bookings = null;
            var fileWrapper = new Mock<IFileWrapper>();
            var stub = new BookingRep(fileWrapper.Object);
            fileWrapper.Setup(p => p.Exists(It.IsAny<string>())).Returns(false);

            var exception = Assert.Throws<FileNotFoundException>(() => stub.SaveBookings(bookings, "notexist.csv"));
            Assert.Equal("File not found", exception.Message);
        }

        [Fact]
        public void SavingBookings_ShouldSaveBookings_WhenFileExists()
        {
            var fileWrapperMock = new Mock<IFileWrapper>();
            fileWrapperMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);

            var fixture = new Fixture();
            var bookings = fixture.CreateMany<Booking>(3).ToList();

            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream, leaveOpen: true); 

            fileWrapperMock.Setup(f => f.CreateFile(It.IsAny<string>())).Returns(streamWriter);

            var bookingRep = new BookingRep(fileWrapperMock.Object);

            bookingRep.SaveBookings(bookings, "valid.csv");

            streamWriter.Flush(); 
            memoryStream.Position = 0; 
            using (var reader = new StreamReader(memoryStream))
            {
                var content = reader.ReadToEnd();
                Assert.Contains("BookingNumber", content); 
                Assert.Contains(bookings[0].BookingNumber.ToString(), content);
            }
        }
    }
}