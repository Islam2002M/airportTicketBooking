using System.IO;

namespace airportTicketBooking.repositry
{
    public class FileWrapper:IFileWrapper
    {
        public bool Exists(string filePath)=>File.Exists(filePath);
        public StreamReader OpenFile(string filePath)=>new StreamReader(filePath);
        public StreamWriter CreateFile(string filePath)=>new StreamWriter(filePath);
        
    }
    
}