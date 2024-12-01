using System;

namespace airportTicketBooking
{
    public class Start
    {
        public static void start()
        {
            Console.WriteLine("Welcome to Airport Ticket Booking System");
            Console.WriteLine("Are you a: ");
            Console.WriteLine("1. Manager");
            Console.WriteLine("2. Passenger");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out var userType) || (userType != 1 && userType != 2))
            {
                Console.WriteLine("Invalid choice. Exiting...");
                return;
            }

            IUserOperations operations = userType == 1 ? new ManagerOperations() : new PassengerOperations();
            
            int choice;
            do
            {
                operations.List();
                Console.Write("Enter choice: ");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }
                operations.ExecuteChoice(choice);
            } while (!operations.ShouldExit(choice));
        }
    }
}