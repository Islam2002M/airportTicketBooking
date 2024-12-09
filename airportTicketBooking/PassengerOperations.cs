using System;
using System.Linq;
using airportTicketBooking.repositry;

namespace airportTicketBooking
{
    public class PassengerOperations : IUserOperations
    {
        static FileWrapper fileWrapper = new FileWrapper();
        static FlightRep flightRep = new FlightRep(fileWrapper);
        
        private readonly ManageBookings manageBookings = new();
        private readonly ManageFlights manageFlights = new(flightRep);
        private int bookingId;

        public void List()
        {
            Console.WriteLine("\nPassenger Menu:");
            Console.WriteLine("1. Search for Available Flights");
            Console.WriteLine("2. Book a Flight");
            Console.WriteLine("3. Manage Bookings");
            Console.WriteLine("4. Exit");
        }

        public void ExecuteChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    SearchAvailableFlights();
                    break;
                case 2:
                    BookFlight();
                    break;
                case 3:
                    ManageBookingsMenu();
                    break;
                case 4:
                    Console.WriteLine("Exiting Passenger menu.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        public bool ShouldExit(int choice) => choice == 4;

        private void SearchAvailableFlights()
        {
            Console.WriteLine("Search for flights...");
            Console.Write("Departure Country (press Enter to skip): ");
            var depCountry = Console.ReadLine();
            Console.Write("Destination Country (press Enter to skip): ");
            var destCountry = Console.ReadLine();

            Console.Write("Departure Date (yyyy-MM-dd, press Enter to skip): ");
            DateTime? depDate = null;

            if (DateTime.TryParse(Console.ReadLine(), out var date)) depDate = date;
            
            Console.Write("Departure Airport (press Enter to skip): ");
            var depAirport = Console.ReadLine();
            
            Console.Write("Arrival Airport (press Enter to skip): ");
            var arrivalAirport = Console.ReadLine();
            
            Console.Write("class for flight (press Enter to skip): ");
            var flightClass = Console.ReadLine();
            
            Console.Write("price (press Enter to skip): ");
            decimal ? price = null;
            if (decimal.TryParse(Console.ReadLine(), out var pricee)) price = pricee;
            
            var flights = manageFlights.FilterFlights(price, depCountry, destCountry, depDate, depAirport, arrivalAirport,flightClass);
            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight Number: {flight.FlightNumber}, From {flight.DepartureCountry} to {flight.ArrivalCountry}, Departure: {flight.DepartureDate}");
            }
        }

        private void BookFlight()
        {
            Console.WriteLine("Booking a flight...");
            Console.Write("Enter your Passenger ID: ");
            if (!int.TryParse(Console.ReadLine(), out var passengerID))
            {
                Console.WriteLine("Invalid Passenger ID.");
                return;
            }

            SearchAvailableFlights();
            Console.Write("Enter the Flight ID you want to book: ");
            if (!int.TryParse(Console.ReadLine(), out var flightID))
            {
                Console.WriteLine("Invalid Flight ID.");
                return;
            }

            Console.Write("Enter the class (Economy, Business, First Class): ");
            var flightClass = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(flightClass) || !new[] { "Economy", "Business", "First Class" }.Contains(flightClass, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid class selection.");
                return;
            }

            manageBookings.BookAFlight(flightID, flightClass, passengerID);
            Console.WriteLine("Flight booked successfully!");
        }

        private void ManageBookingsMenu()
        {
            Console.WriteLine("1. View Personal Bookings");
            Console.WriteLine("2. Cancel a Booking");
            Console.WriteLine("3. Modify a Booking");
            Console.Write("Enter choice: ");
            if (!int.TryParse(Console.ReadLine(), out var choice))
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            switch (choice)
            {
                case 1:
                    ViewPersonalBookings();
                    break;
                case 2:
                    CancelBooking();
                    break;
                case 3:
                    ModifyBooking();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private void ViewPersonalBookings()
        {
            Console.Write("Enter Passenger ID: ");
            if (!int.TryParse(Console.ReadLine(), out var passengerID))
            {
                Console.WriteLine("Invalid Passenger ID.");
                return;
            }

            var bookings = manageBookings.ViewPersonalBookings(passengerID);
            if (bookings == null || bookings.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                return;
            }

            foreach (var booking in bookings)
            {
                Console.WriteLine($"Booking #{booking.BookingNumber}: Flight {booking.FlightId}, Price: ${booking.Price}, Status: {booking.Status}");
            }
        }

        private void CancelBooking()
        {
            ViewPersonalBookings();
            Console.Write("Enter the Booking ID to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out var bookingID))
            {
                Console.WriteLine("Invalid Booking ID.");
                return;
            }

            manageBookings.CancelBooking(bookingID);
            Console.WriteLine("Booking canceled successfully!");
        }

        private void ModifyBooking()
        {
            Console.Write("Enter the Booking ID to modify: ");
            if (!int.TryParse(Console.ReadLine(), out bookingId))
            {
                Console.WriteLine("Invalid Booking ID.");
                return;
            }

            Console.WriteLine("1. Modify Booking Status");
            Console.WriteLine("2. Modify Booking Class");
            Console.Write("Enter choice: ");
            if (!int.TryParse(Console.ReadLine(), out var choice))
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            switch (choice)
            {
                case 1:
                    ModifyBookingStatus();
                    break;
                case 2:
                    ModifyBookingClass();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private void ModifyBookingStatus()
        {
            Console.Write("Enter the new status (Pending, Confirmed, Canceled): ");
            var status = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(status) || !new[] { "Pending", "Confirmed", "Canceled" }.Contains(status, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid status.");
                return;
            }

            manageBookings.ChangeBookingStatus(bookingId, status);
            Console.WriteLine("Booking status updated successfully!");
        }

        private void ModifyBookingClass()
        {
            Console.Write("Enter the new class (Economy, Business, FirstClass): ");
            var flightClass = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(flightClass) || !new[] { "Economy", "Business", "First Class" }.Contains(flightClass, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("Invalid class selection.");
                return;
            }

            manageBookings.ChangeBookingType(bookingId, flightClass);
            Console.WriteLine("Booking class updated successfully!");
        }
    }
}
