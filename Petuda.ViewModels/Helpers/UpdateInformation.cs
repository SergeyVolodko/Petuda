using System;

namespace Petuda.ViewModels.Helpers
{
    public class UpdateInformation
    {
        public Version NewVersion { get; set; }
        public String Url { get; set; }
        public String ArchiveName { get; set; }
        public String ReleaseNotesLink { get; set; }

        public bool IsFull
        {
            get
            {
                return this.NewVersion != null &&
                       !String.IsNullOrEmpty(this.Url) &&
                       !String.IsNullOrEmpty(this.ArchiveName) &&
                       !String.IsNullOrEmpty(this.ReleaseNotesLink);
            }
        }
    }
}