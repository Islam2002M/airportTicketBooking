using System;

namespace airportTicketBooking
{
    public class Flight
    {
        public int FlightNumber { get; set; }
        public string DepartureCountry { get; set; }
        public string ArrivalCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public decimal Price { get; set; }
        
        public string Class{ get; set; }
        public static decimal calculateprice(string flightClass)
        {
            if (flightClass.Equals("Economy", StringComparison.OrdinalIgnoreCase)) return 200;
            if (flightClass.Equals("Business", StringComparison.OrdinalIgnoreCase)) return 300;
            if (flightClass.Equals("First Class", StringComparison.OrdinalIgnoreCase)) return 400;
            return 0;
        }

    }
}