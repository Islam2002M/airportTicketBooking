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
        public void GetFlights_ValidCsvFile_ReturnsListOfFlights()
        {
            
        }
    }
}