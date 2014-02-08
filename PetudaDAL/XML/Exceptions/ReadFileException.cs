using System;

namespace PetudaDAL.XML.Exceptions
{
    public class ReadFileException : Exception
    {
        public string FileName { get; set; }

        public ReadFileException(string fileName = "")
        {
            FileName = fileName;
        }
    }
}