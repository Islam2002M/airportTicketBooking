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
        private readonly FlightRep _flightRep;
       
        [Fact]
        public void GetFlights_FileDoesNotExist_ThrowsFileNotFoundException()
        {
            string filePath = "nonexistentfile.csv";

            var exception = Assert.Throws<FileNotFoundException>(() => _flightRep.GetFlights(filePath));
            Assert.Equal("Could not find file", exception.Message);
        }

        [Fact]
        public void GetFlights_ValidCsvFile_ReturnsListOfFlights()
        {
            
        }

       
    }
    

    
   
}
