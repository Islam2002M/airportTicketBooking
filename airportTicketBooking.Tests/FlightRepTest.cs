using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using airportTicketBooking.repositry;
using Moq;
using Xunit;
using AutoFixture;
using CsvHelper;
using CsvHelper.Configuration;

namespace airportTicketBooking.Tests
{
    public class FlightRepTests
    {
        [Fact]
        public void GetFlights_FileDoesNotExist_ThrowsFileNotFoundException()
        {
            var fileWrapper = new Mock<IFileWrapper>();
            var stub = new FlightRep(fileWrapper.Object);
            fileWrapper.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);

            var exception = Assert.Throws<FileNotFoundException>(() => stub.GetFlights("dosen'texist.csv"));
            Assert.Equal("Could not find file", exception.Message);
        }

        [Fact]
        public void GetFlights_ValidFile_ReturnsFlights()
        {
            var fileWrapperMock = new Mock<IFileWrapper>();
            fileWrapperMock.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);

            var fixture = new Fixture();
            var flights = fixture.CreateMany<Flight>(3).ToList();

            var csvContent = "FlightNumber,DepartureCountry,ArrivalCountry,DepartureDate,DepartureAirport,ArrivalAirport,Price,Class\n" +
                             string.Join("\n", flights.Select(f => $"{f.FlightNumber},{f.DepartureCountry},{f.ArrivalCountry},{f.DepartureDate},{f.DepartureAirport},{f.ArrivalAirport},{f.Price},{f.Class}"));

            var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csvContent));
            var streamReader = new StreamReader(memoryStream);

            fileWrapperMock.Setup(f => f.OpenFile(It.IsAny<string>())).Returns(streamReader);

            var flightRep = new FlightRep(fileWrapperMock.Object);

            var result = flightRep.GetFlights("valid.csv");

            Assert.Equal(3, result.Count);
            Assert.Equal(flights.Select(f => f.FlightNumber), result.Select(f => f.FlightNumber));
        }
    }
}