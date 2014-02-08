using System;

namespace PetudaDAL.XML.Exceptions
{
    public class SaveFileException: Exception
    {
        public string FileName { get; set; }

        public SaveFileException(string fileName = "")
        {
            FileName = fileName;
        }
    }
}
