using System.IO;

namespace airportTicketBooking.repositry
{
    public interface IFileWrapper
    {
       public bool Exists(string filePath);
       public StreamReader OpenFile(string filePath);
       public StreamWriter CreateFile(string filePath);
    }
}