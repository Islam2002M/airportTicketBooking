using System;
using System.IO;
using airportTicketBooking.repositry;
using Moq;
using Xunit;

namespace airportTicketBooking.Tests
{
    public class BookingRepTests
    {
        [Fact]
        public void GetBookings_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            var fileWrapper =new Mock<IFileWrapper>();
            var stub = new BookingRep(fileWrapper.Object);
            fileWrapper.Setup(p=>p.Exists(It.IsAny<string>())).Returns(false);
            
           var exception= Assert.Throws<FileNotFoundException>(()=>stub.GetBookings());
           Assert.Equal("File not found",exception.Message);
            
        }

        [Fact]
        public void GetBookings_ShouldReturnBookings_WhenFileExists()
        {
            var fileWrapper = new Mock<IFileWrapper>();
            
        }
       
    }
}