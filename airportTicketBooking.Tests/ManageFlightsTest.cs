using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using airportTicketBooking.repositry;

namespace airportTicketBooking.Tests
{
    public class ManageFlightsTests
    {
        private readonly Mock<IFlightRep> _mockFlightRep;
        private readonly ManageFlights _manageFlights;

        public ManageFlightsTests()
        {
            _mockFlightRep = new Mock<IFlightRep>();
            _manageFlights = new ManageFlights(_mockFlightRep.Object);
        }

        [Fact]
        public void FilterFlights_ShouldReturnFilteredFlights_WhenCriteriaMatch()
        {
            var flights = new List<Flight>
            {
                new()
                {
                    FlightNumber = 1,
                    DepartureAirport = "JFK",
                    ArrivalAirport = "LAX",
                    DepartureCountry = "USA",
                    ArrivalCountry = "USA",
                    Class = "Economy",
                    Price = 300,
                    DepartureDate = new DateTime(2024, 12, 20)
                },
                new()
                {
                    FlightNumber = 2,
                    DepartureAirport = "JFK",
                    ArrivalAirport = "DXB",
                    DepartureCountry = "USA",
                    ArrivalCountry = "UAE",
                    Class = "Business",
                    Price = 500, 
                    DepartureDate = new DateTime(2024, 12, 25)
                }
            };

            _mockFlightRep.Setup(repo => repo.GetFlights(It.IsAny<string>())).Returns(flights);

            var result = _manageFlights.FilterFlights(
                price: 500,
                departureCountry: "USA",
                destinationCountry: "UAE",
                departureDate: new DateTime(2024, 12, 25),
                departureAirport: "JFK",
                arrivalAirport: "DXB",
                flightClass: "Business");

            Assert.Single(result);
            Assert.Equal(2, result[0].FlightNumber);
        }

        [Fact]
        public void GetFlightById_ShouldReturnCorrectFlight_WhenFlightExists()
        {
            var flights = new List<Flight>
            {
                new() { FlightNumber = 1, DepartureAirport = "JFK", ArrivalAirport = "LAX" },
                new() { FlightNumber = 2, DepartureAirport = "JFK", ArrivalAirport = "DXB" }
            };

            _mockFlightRep.Setup(repo => repo.GetFlights(It.IsAny<string>())).Returns(flights);

            var result = _manageFlights.GetFlightById(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.FlightNumber);
        }

        [Fact]
        public void GetFlightById_ShouldReturnNull_WhenFlightDoesNotExist()
        {
            var flights = new List<Flight>
            {
                new Flight { FlightNumber = 1, DepartureAirport = "JFK", ArrivalAirport = "LAX" }
            };

            _mockFlightRep.Setup(repo => repo.GetFlights(It.IsAny<string>())).Returns(flights);

            var result = _manageFlights.GetFlightById(3);

            Assert.Null(result);
        }

    }
}
