using System;

namespace airportTicketBooking
{
    public class Booking
    {
        public int BookingNumber { get; set; }
        public int FlightId { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public int PassengerId { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public decimal Price { get; private set; }

        public Booking()
        {
            Setprice();
        }

        private void Setprice()
        {
            if (Class == "Economy") Price = 200;
            else if (Class == "Business") Price = 300;
            else if (Class == "First Class") Price = 400;
        }
    }
}