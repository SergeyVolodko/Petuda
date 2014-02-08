using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace PetudaDAL
{
    public class XMLSerializer<T>
    {
        private const string _directoreyName = "Data\\";
        private readonly string _fileName;

        public XMLSerializer(string fileName)
        {
            _fileName = fileName;
        }

        public void Save(ObservableCollection<T> collection, bool autoDirectory = true)
        {
            SerializeToXml(collection, autoDirectory);
        }

        public void Read(out ObservableCollection<T> collection, bool autoDirectory = true)
        {
            DeserializeFromXml(out collection, autoDirectory);
        }

        private void SerializeToXml(ObservableCollection<T> collection, bool autoDirectory = true)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<T>));

            var fullFileName = _fileName;

            if (autoDirectory)
            {
                CreatDirectoryIfNotExists();
                fullFileName = _directoreyName + _fileName;
            }

            using (Stream stream = new FileStream(fullFileName, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(stream, collection);
            }
        }

        private void DeserializeFromXml(out ObservableCollection<T> collection, bool autoDirectory = true)
        {
            var deserializer = new XmlSerializer(typeof(ObservableCollection<T>));

            var fullFileName = autoDirectory ? _directoreyName + _fileName : _fileName;

            if (!File.Exists(fullFileName))
            {
                collection = new ObservableCollection<T>();
                return;
            }

            using (Stream stream = File.OpenRead(fullFileName))
            {
                collection = (ObservableCollection<T>)deserializer.Deserialize(stream);
            }
        }

        private static void CreatDirectoryIfNotExists()
        {
            if (!Directory.Exists(_directoreyName))
            {
                Directory.CreateDirectory(_directoreyName);
            }
        }

    }//class
}//namespace