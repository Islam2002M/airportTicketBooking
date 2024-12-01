namespace airportTicketBooking
{
    public interface IUserOperations
    {
        public void List();
       // public void filter();
        void ExecuteChoice(int choice); 
        bool ShouldExit(int choice);  

    }
}