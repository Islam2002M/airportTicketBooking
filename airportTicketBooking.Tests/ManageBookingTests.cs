using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using airportTicketBooking;

public class ManageBookingsTests
{
    private static readonly ManageFlights ManageFlights;
    //Fixture fixture = new Fixture();


    [Fact]
    public void FilterBookings_ReturnsFilteredBookings_WhenFiltersMatch()
    {
        var mockBookingRep = new Mock<IManageBookings>();
        var bookings = new List<Booking>
        {
            new()
            {
                Price = 100m, DepartureCountry = "USA", DestinationCountry = "UK", FlightId = 1, Status = "Confirmed"
            },
            new()
            {
                Price = 200m, DepartureCountry = "USA", DestinationCountry = "Canada", FlightId = 2, Status = "Pending"
            }
        };
        //var bookings = fixture.CreateMany<Booking>(2).ToList();


        mockBookingRep.Setup(rep => rep.GetBookings(It.IsAny<string>())).Returns(bookings);

        var manageFlights = new Mock<IManageFlights>();
        var manageBookings = new ManageBookings(mockBookingRep.Object, manageFlights.Object);

        var result = manageBookings.FilterBookings(flightId: 1, price: 100m, departureCountry: "USA",
            destinationCountry: "UK", departureDate: null, departureAirport: null, arrivalAirport: null,
            passengerId: null, @class: null, status: null);

        Assert.Single(result);
        Assert.Equal(100m, result.First().Price);
    }

    [Fact]
    public void FilterBookings_ReturnsNoBookings_WhenNoMatch()
    {
        var mockBookingRep = new Mock<IManageBookings>();
        var bookings = new List<Booking>
        {
            new()
            {
                Price = 100m, DepartureCountry = "USA", DestinationCountry = "UK", FlightId = 1, Status = "Confirmed"
            }
        };

        mockBookingRep.Setup(rep => rep.GetBookings(It.IsAny<string>())).Returns(bookings);

        var manageFlights = new Mock<IManageFlights>();
        var manageBookings = new ManageBookings(mockBookingRep.Object, manageFlights.Object);

        var result = manageBookings.FilterBookings(flightId: 2, price: null, departureCountry: "USA",
            destinationCountry: "UK", departureDate: null, departureAirport: null, arrivalAirport: null,
            passengerId: null, @class: null, status: null);

        Assert.Empty(result);
    }

    [Fact]
    public void ViewPersonalBookings_ReturnsCorrectBookingsForGivenPassengerId()
    {
        var mockBookings = new List<Booking>
        {
            new() { BookingNumber = 1, FlightId = 100, PassengerId = 1, Status = "Pending" },
            new() { BookingNumber = 2, FlightId = 101, PassengerId = 1, Status = "Confirmed" },
            new() { BookingNumber = 3, FlightId = 102, PassengerId = 2, Status = "Pending" }
        };

        var mockBookingRep = new Mock<IManageBookings>();
        mockBookingRep.Setup(b => b.GetBookings(It.IsAny<string>())).Returns(mockBookings);

        var manageBookings = new ManageBookings(mockBookingRep.Object, ManageFlights);

        var result = manageBookings.ViewPersonalBookings(1);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, booking => Assert.Equal(1, booking.PassengerId));
    }

    [Fact]
    public void CancelBooking_UpdatesStatusToCanceled()
    {
        var mockBookingRep = new Mock<IManageBookings>();
        var mockFlightRep = new Mock<IManageFlights>();

        var mockBookings = new List<Booking>
        {
            new() { BookingNumber = 1, Status = "Pending" },
            new() { BookingNumber = 2, Status = "Confirmed" }
        };

        mockBookingRep.Setup(b => b.GetBookings(It.IsAny<string>())).Returns(mockBookings);


        var manageBookings = new ManageBookings(mockBookingRep.Object, mockFlightRep.Object);

        manageBookings.CancelBooking(1);

        var updatedBooking = mockBookings.FirstOrDefault(b => b.BookingNumber == 1);
        Assert.NotNull(updatedBooking);
        Assert.Equal("Canceled", updatedBooking.Status);
    }


    [Fact]
    public void ChangeBookingStatus_ChangesStatusSuccessfully()
    {
        var mockBookings = new List<Booking>
        {
            new() { BookingNumber = 1, Status = "Pending" },
            new() { BookingNumber = 2, Status = "Confirmed" }
        };

        var mockBookingRep = new Mock<IManageBookings>();
        mockBookingRep.Setup(b => b.GetBookings(It.IsAny<string>())).Returns(mockBookings);

        var mockManageFlights = new Mock<IManageFlights>();

        var manageBookings = new ManageBookings(mockBookingRep.Object, mockManageFlights.Object);

        manageBookings.ChangeBookingStatus(1, "Confirmed");

        var updatedBooking = mockBookings.First(b => b.BookingNumber == 1);
        Assert.Equal("Confirmed", updatedBooking.Status);
    }

    [Fact]
    public void ChangeBookingType_ShouldUpdateClass_WhenBookingExists()
    {
        var mockBookings = new Mock<IManageBookings>();
        var mockManageFlights = new Mock<IManageFlights>();
        var bookingList = new List<Booking>
        {
            new() { BookingNumber = 1, Class = "Economy" },
            new() { BookingNumber = 2, Class = "Business" }
        };

        mockBookings.Setup(b => b.GetBookings(It.IsAny<string>())).Returns(bookingList);


        var manageBookings = new ManageBookings(mockBookings.Object, mockManageFlights.Object);

        manageBookings.ChangeBookingType(1, "FirstClass");


        Assert.Equal("FirstClass", bookingList[0].Class);
    }
}