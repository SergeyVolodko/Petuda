using System;
using System.IO;
using System.Net;

namespace Petuda.ViewModels.Helpers
{
    public delegate void BytesDownloadedEventHandler(ByteArgs e);

    public class ByteArgs : EventArgs
    {
        public int Downloaded { get; set; }

        public int Total { get; set; }
    }

    public class WebData
    {
        public static event BytesDownloadedEventHandler bytesDownloaded;

        public static bool DownloadFromWeb(string URL, string file, string targetFolder)
        {
            try
            {
                var downloadedData = new byte[0];

                //open a data stream from the supplied URL
                WebRequest webReq = WebRequest.Create(URL + file);
                WebResponse webResponse = webReq.GetResponse();
                Stream dataStream = webResponse.GetResponseStream();

                //Download the data in chuncks
                byte[] dataBuffer = new byte[1024];

                //Get the Total size of the download
                int dataLength = (int)webResponse.ContentLength;

                //lets declare our Downloaded bytes event args
                ByteArgs byteArgs = new ByteArgs();

                byteArgs.Downloaded = 0;
                byteArgs.Total = dataLength;

                //we need to test for a null as if an event is not consumed we will get an exception
                if (bytesDownloaded != null) bytesDownloaded(byteArgs);


                //Download the data
                MemoryStream memoryStream = new MemoryStream();
                while (true)
                {
                    //Let's try and read the data
                    int bytesFromStream = dataStream.Read(dataBuffer, 0, dataBuffer.Length);

                    if (bytesFromStream == 0)
                    {

                        byteArgs.Downloaded = dataLength;
                        byteArgs.Total = dataLength;
                        if (bytesDownloaded != null) bytesDownloaded(byteArgs);

                        //Download complete
                        break;
                    }
                    else
                    {
                        //Write the Downloaded data
                        memoryStream.Write(dataBuffer, 0, bytesFromStream);

                        byteArgs.Downloaded = bytesFromStream;
                        byteArgs.Total = dataLength;
                        if (bytesDownloaded != null) bytesDownloaded(byteArgs);

                    }
                }

                //Convert the Downloaded stream to a byte array
                downloadedData = memoryStream.ToArray();

                //Release resources
                dataStream.Close();
                memoryStream.Close();

                //Write bytes to the specified file
                FileStream newFile = new FileStream(targetFolder + file, FileMode.Create);
                newFile.Write(downloadedData, 0, downloadedData.Length);
                newFile.Close();

                return true;

            }
            catch (Exception)
            {
                //We may not be connected to the internet
                //Or the URL may be incorrect
                return false;
            }
        }

    }
}