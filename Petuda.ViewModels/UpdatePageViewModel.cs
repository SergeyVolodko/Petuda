using System;
using System.Windows.Input;
using Petuda.ViewModels.Helpers;
using Petuda.ViewModels.Navigation;
using Petuda.ViewModels.Resources;

namespace Petuda.ViewModels
{
    public class UpdatePageViewModel: BaseViewModel
    {
        private readonly INavigationService navigationService;
        
        private DelegateCommand _runUpdateInstallation;
        private readonly Version _version;
        private UpdateInformation _updateInfo;

        public String Version
        {
            get { return _version.ToString(); }
        }

        public UpdateInformation UpdateInfo
        {
            get { return _updateInfo; }
            set
            {
                if (value == _updateInfo)
                    return;

                _updateInfo = value;
                NotifyPropertChanged("UpdateInfo");
                NotifyPropertChanged("NewVersion");
                NotifyPropertChanged("ReleaseNotesLink");
            }
        }

        public String NewVersion
        {
            get
            {
                if (_updateInfo == null || _updateInfo.NewVersion == null)
                {
                    return "";
                }
                return _updateInfo.NewVersion.ToString();
            }
        }

        public String ReleaseNotesLink
        {
            get
            {
                if (_updateInfo == null || String.IsNullOrEmpty(_updateInfo.ReleaseNotesLink))
                {
                    return "";
                }
                return _updateInfo.ReleaseNotesLink;
            }
        }

        public ICommand RunUpdateInstallationCommand
        {
            get
            {
                if (_runUpdateInstallation == null)
                {
                    _runUpdateInstallation = new DelegateCommand(RunUpdateInstallation);
                }

                return _runUpdateInstallation;
            }
        }

        public UpdatePageViewModel(INavigationService navigationService, Version version, UpdateInformation updateInfo)
        {
            this.navigationService = navigationService;
            this._version = version;
            this.UpdateInfo = updateInfo;
        }

        private void RunUpdateInstallation(object obj)
        {
            if (_updateInfo == null)
            {
                return;
            }

            var applicationDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var processToEnd = "Petuda.Views";
            var postProcess = applicationDirectory + @"\Петуда.exe";
            var updater = applicationDirectory + @"\PetudaUpdate.exe";
            
            try
            {
                ApplicationUpdateHelper.InstallUpdateRestart(this.UpdateInfo.Url, 
                                                             this.UpdateInfo.ArchiveName, 
                                                             "\"" + applicationDirectory + "\\",
                                                             processToEnd,
                                                             postProcess,
                                                             "updated", 
                                                             updater);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, String.Format(Strings.FileNotFound, updater));
            }
            catch (Exception e)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, e.Message);
            }
        }

    }//class
}//namespace