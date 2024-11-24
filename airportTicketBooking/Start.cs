using System;
using System.Collections.Generic;
using System.Linq;

namespace airportTicketBooking
{
    public class Start
    {
        private static IUser User;
        private static ManageFlights manageFlights = new();
        private static ManageBookings manageBookings = new();
        private static ValidateFileRecords ValidateFileRecords = new();

        public static void start(int userType)
        {
            User = userType == 1 ? new Manager() : new Passenger();
            int choice;
            do
            {
                if (userType == 1)
                {
                    Console.WriteLine("1. Filter Bookings");
                    Console.WriteLine("2. Batch Flight Upload");
                    Console.WriteLine("3. Exit");
                }
                else
                {
                    Console.WriteLine("1. Search for Available Flights");
                    Console.WriteLine("2. Book a Flight");
                    Console.WriteLine("3. Manage Bookings");
                    Console.WriteLine("4. Exit");
                }

                Console.Write("Enter choice: ");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                if (userType == 1)
                    switch (choice)
                    {
                        case 1:
                            var bookings = manageBookings.FilterBookings(null, null, null, null, null, null, null, null,
                                null, "Canceled");
                            foreach (var booking in bookings)
                                Console.WriteLine(
                                    $"Booking Detail: Flight {booking.FlightId}, Passenger ID: {booking.PassengerID}, Departure Date: {booking.DepartureDate}, Status: {booking.Status}");
                            break;
                        case 2:
                            Console.WriteLine("Enter the file path you want to batch from it");
                            var path = Console.ReadLine();
                            var errors = ValidateFileRecords.ValidateFileRecordss(path);
                            Console.WriteLine(errors.Count);
                            foreach (var error in errors) Console.WriteLine(error);
                            break;
                        case 3:
                            Console.WriteLine("Exiting Manager menu.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            break;
                    }
                else
                    switch (choice)
                    {
                        case 1:
                            Case1();
                            break;
                        case 2:
                            Console.WriteLine("Enter your ID");
                            int.TryParse(Console.ReadLine(), out var passengerID);
                            Case1();
                            Console.WriteLine("Enter the flight ID you want to Book:");
                            int.TryParse(Console.ReadLine(), out var flightID);
                            Console.WriteLine("Enter the class of your flight you want to book:");
                            var @class = Console.ReadLine();
                            if (@class != "Economy" && @class != "Business" && @class != "First Class")
                            {
                                Console.WriteLine("Invalid choice. Please select a valid option.");
                                return;
                            }

                            manageBookings.BookAFlight(flightID, @class, passengerID);
                            break;
                        case 3:
                            Console.WriteLine("Choose what you want to do:");
                            Console.WriteLine("1. View personal bookings");
                            Console.WriteLine("2. Cancel a booking");
                            Console.WriteLine("3. Modify a booking");
                            Console.Write("Enter choice: ");
                            if (!int.TryParse(Console.ReadLine(), out var choicee))
                            {
                                Console.WriteLine("Invalid choice.");
                                return;
                            }

                            switch (choicee)
                            {
                                case 1:
                                    Case1Excution();
                                    break;
                                case 2:
                                    Case1Excution();
                                    Console.WriteLine("What would you like to cancel it ,Enter its ID?");
                                    int.TryParse(Console.ReadLine(), out var cancelId);
                                    manageBookings.CancelBooking(cancelId);
                                    break;
                                case 3:
                                    string[] availabilStatus = { "Pending", "Confirmed", "Canceled" };
                                    string[] availabilType = { "Business", "Economy", "FirstClass" };
                                    Console.WriteLine("Enter the passenger ID:");
                                    int.TryParse(Console.ReadLine(), out var passengerId);
                                    var personalBookings = manageBookings.ViewPersonalBookings(passengerId);
                                    if (personalBookings == null || personalBookings.Count == 0)
                                    {
                                        Console.WriteLine("No bookings found for this passenger ID.");
                                        return;
                                    }

                                    foreach (var booking in personalBookings)
                                        Console.WriteLine(
                                            $"Booking Detail: {booking.BookingNumber}, Flight: {booking.FlightId}, Passenger ID: {booking.PassengerID}, Departure Date: {booking.DepartureDate}, Status: {booking.Status}");

                                    Console.WriteLine("Which booking do you want to modify its status? Enter its ID:");
                                    int.TryParse(Console.ReadLine(), out var bookingId);
                                    Console.WriteLine("Choose what you want to do:");
                                    Console.WriteLine("1. change the status of a booking ");
                                    Console.WriteLine("2. change the class of a booking");
                                    Console.Write("Enter choice: ");
                                    if (!int.TryParse(Console.ReadLine(), out var choiceee))
                                    {
                                        Console.WriteLine("Invalid choice.");
                                        return;
                                    }

                                    switch (choiceee)
                                    {
                                        case 1:
                                            Console.WriteLine("Enter the new status (Pending, Confirmed, Canceled):");
                                            while (true)
                                            {
                                                var status = Console.ReadLine();

                                                if (availabilStatus.Contains(status, StringComparer.OrdinalIgnoreCase))
                                                {
                                                    manageBookings.ChangeBookingStatus(bookingId, status);
                                                    break;
                                                }

                                                Console.WriteLine("Invalid status. Please enter a valid status.");
                                            }

                                            break;
                                        case 2:

                                            Console.WriteLine("Enter the new class (Business, Economy, FirstClass):");
                                            while (true)
                                            {
                                                var type = Console.ReadLine();
                                                if (availabilType.Contains(type, StringComparer.OrdinalIgnoreCase))
                                                {
                                                    manageBookings.ChangeBookingType(bookingId, type);
                                                    break;
                                                }

                                                Console.WriteLine("Invalid type. Please enter a valid type.");
                                            }

                                            break;
                                    }

                                    break;
                            }

                            break;
                        case 4:
                            Console.WriteLine("Exiting Passenger menu.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            break;
                    }
            } while ((userType == 1 && choice != 3) || (userType != 1 && choice != 4));
        }

        public static void Case1Excution()
        {
            List<Booking> bookings = null;
            Console.Write("Enter Passenger number: ");
            int.TryParse(Console.ReadLine(), out var passengerNum);
            {
                bookings = manageBookings.ViewPersonalBookings(passengerNum);
            }
            foreach (var book in bookings)
                Console.WriteLine($"{book.BookingNumber} {book.FlightId} to {book.Price} on {book.DepartureDate}");
        }

        private static void Case1()
        {
            Console.Write("Departure Country (press Enter to skip): ");
            var depCountryInput = Console.ReadLine();
            var depCountry = string.IsNullOrWhiteSpace(depCountryInput) ? null : depCountryInput;

            Console.Write("Destination Country (press Enter to skip): ");
            var destCountryInput = Console.ReadLine();
            var destCountry = string.IsNullOrWhiteSpace(destCountryInput) ? null : destCountryInput;

            Console.Write("Departure Date (press Enter to skip): ");
            var depDateInput = Console.ReadLine();
            var depDate = string.IsNullOrWhiteSpace(depDateInput) ? (DateTime?)null : DateTime.Parse(depDateInput);

            Console.Write("Airline (press Enter to skip): ");
            var DepartureAirportInput = Console.ReadLine();
            var DepartureAirport = string.IsNullOrWhiteSpace(DepartureAirportInput) ? null : DepartureAirportInput;

            Console.Write("Airline (press Enter to skip): ");
            var ArrivalAirportInput = Console.ReadLine();
            var ArrivalAirport = string.IsNullOrWhiteSpace(ArrivalAirportInput) ? null : ArrivalAirportInput;

            Console.Write("Flight Class (press Enter to skip): ");
            var flightClassInput = Console.ReadLine();
            var flightClass = string.IsNullOrWhiteSpace(flightClassInput) ? null : flightClassInput;

            Console.Write("Price Range (press Enter to skip): ");
            var priceRangeInput = Console.ReadLine();
            var priceRange = string.IsNullOrWhiteSpace(priceRangeInput)
                ? (decimal?)null
                : decimal.Parse(priceRangeInput);

            var flights = manageFlights.FilterFlights(priceRange, depCountry, destCountry, depDate, DepartureAirport,
                ArrivalAirport, flightClass);

            foreach (var flight in flights)
                Console.WriteLine(
                    $"Flight Number: {flight.FlightNumber}, Flight from {flight.DepartureCountry} to {flight.ArrivalCountry} on {flight.DepartureDate}");
        }
    }
}