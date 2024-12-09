using System;
using System.IO;

namespace airportTicketBooking
{
    public class ManagerOperations : IUserOperations
    {
        
        private readonly ManageBookings manageBookings = new();
        private readonly ValidateFileRecords validateFileRecords = new();

        public void List()
        {
            Console.WriteLine("\nManager Menu:");
            Console.WriteLine("1. Filter Bookings");
            Console.WriteLine("2. Batch Flight Upload");
            Console.WriteLine("3. Exit");
        }

        public void ExecuteChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    FilterBookings();
                    break;
                case 2:
                    BatchUpload();
                    break;
                case 3:
                    Console.WriteLine("Exiting Manager menu.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        public bool ShouldExit(int choice) => choice == 3;

        private void FilterBookings()
        {
            Console.WriteLine("Filtering bookings ");
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
            
            Console.Write("flight id (press Enter to skip): ");
            int ? flightid = null;
            if (int.TryParse(Console.ReadLine(), out var flightidd)) flightid = flightidd;
            
            Console.Write("passenger id  (press Enter to skip): ");
            int ? passengerid = null;
            if (int.TryParse(Console.ReadLine(), out var passengeridd)) passengerid = passengeridd;

            
            Console.Write("status of the booking (press Enter to skip): ");
            var status = Console.ReadLine();
            
            var bookings = manageBookings.FilterBookings(flightid, price, depCountry,destCountry, depDate,  depAirport, arrivalAirport, passengerid, flightClass, status);
            foreach (var booking in bookings)
            {
                Console.WriteLine($"Booking Detail: Flight {booking.FlightId}, Passenger ID: {booking.PassengerId}, Departure Date: {booking.DepartureDate}, Status: {booking.Status}");
            }
        }

        private void BatchUpload()
        {
            Console.Write("Enter the file path for batch upload: ");
            var path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                Console.WriteLine("Invalid file path. Please try again.");
                return;
            }

            var errors = validateFileRecords.ValidateFileRecordss(path);
            if (errors.Count > 0)
            {
                Console.WriteLine("Errors found during file validation:");
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Console.WriteLine("Batch upload completed successfully!");
            }
        }
    }
}
