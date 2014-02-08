using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Petuda.ViewModels.Helpers
{
    public static class ApplicationUpdateHelper
    {
        private const String appcastUrl = @"http://petuda.url.ph/version.xml";
                                         //@"E:\version.xml";
                                          // @"E:\Projects\PetudaStub\version.xml";
                                         
        private const String updaterPrefix = "m4a_";
                                        

        public static UpdateInformation GetUpdateInformation()
        {
            UpdateInformation result = null;

            XmlTextReader reader = null;

            try
            {
                reader = new XmlTextReader(appcastUrl);

                reader.MoveToContent();

                result = new UpdateInformation();

                string elementName = "";

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        elementName = reader.Name;
                    }
                    else if (reader.NodeType == XmlNodeType.Text && reader.HasValue)
                    {
                        switch (elementName)
                        {
                            case "version":
                                result.NewVersion = new Version(reader.Value); break;
                            case "url":
                                result.Url = reader.Value; break;
                            case "archiveName":
                                result.ArchiveName = reader.Value; break;
                            case "releaseNotesLink":
                                result.ReleaseNotesLink = reader.Value; break;
                        }
                    }

                    if (result.IsFull)
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return result;
        }

        /// <summary>Starts the PetudaUpdate application passing across relevant information</summary>
        /// <param name="downloadsURL">URL to download file from</param>
        /// <param name="filename">Name of the file to download</param>
        /// <param name="destinationFolder">Folder on the local machine to download the file to</param>
        /// <param name="processToEnd">Name of the process to end before applying the updates</param>
        /// <param name="postProcess">Name of the process to restart</param>
        /// <param name="startupCommand">Command line to be passed to the process to restart</param>
        /// <param name="updater"></param>
        /// <returns>Void</returns>
        public static void InstallUpdateRestart(string downloadsURL, string filename, string destinationFolder, string processToEnd, string postProcess, string startupCommand, string updater)
        {
            string cmdLn = "";

            cmdLn += "|downloadFile|" + filename;
            cmdLn += "|URL|" + downloadsURL;
            cmdLn += "|destinationFolder|" + destinationFolder;
            cmdLn += "|processToEnd|" + processToEnd;
            cmdLn += "|postProcess|" + postProcess;
            cmdLn += "|command|" + @" / " + startupCommand;

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = updater;
            startInfo.Arguments = cmdLn;
            Process.Start(startInfo);
        }

        /// <summary>Updates the update application by renaming prefixed files</summary>
        /// <param name="containingFolder">Folder on the local machine where the prefixed files exist</param>
        /// <returns>Void</returns>
        public static void UpdateFilesWithPrefix(string containingFolder)
        {
            DirectoryInfo dInfo = new DirectoryInfo(containingFolder);
            FileInfo[] updaterFiles = dInfo.GetFiles(updaterPrefix + "*.*");
            
            foreach (FileInfo file in updaterFiles)
            {
                var newFile = containingFolder + file.Name;
                var origFile = containingFolder + @"\" + file.Name.Substring(updaterPrefix.Length, file.Name.Length - updaterPrefix.Length);

                if (File.Exists(origFile))
                {
                    File.Delete(origFile);
                }

                File.Move(newFile, origFile);
            }
        }

    }//class
}//namespace