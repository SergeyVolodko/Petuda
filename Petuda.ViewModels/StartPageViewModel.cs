using System;
using System.Windows.Input;
using Petuda.ViewModels.Helpers;
using Petuda.ViewModels.Navigation;
using Petuda.ViewModels.Resources;
using Petuda.ViewModels.ViewModelsFactory;

namespace Petuda.ViewModels
{
    public class StartPageViewModel: BaseViewModel
    {
        private readonly INavigationService navigationService;
        private DelegateCommand _openMainVindow;
        private DelegateCommand _checkForUpdates;
        private readonly Version _version;
        private Boolean _updateModeIsShown;
        private UpdateInformation _updateInfo;

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

        public String Version
        {
            get { return _version.ToString(); }
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

        public Boolean UpdateModeIsShown
        {
            get { return _updateModeIsShown; }
            set
            {
                if (value == _updateModeIsShown)
                    return;

                _updateModeIsShown = value;
                NotifyPropertChanged("UpdateModeIsShown");
            }
        }

        public ICommand OpenMainVindowCommand
        {
            get
            {
                if (_openMainVindow == null)
                {
                    _openMainVindow = new DelegateCommand(OpenMainVindow);
                }

                return _openMainVindow;
            }
        }

        public ICommand CheckForUpdatesCommand
        {
            get
            {
                if (_checkForUpdates == null)
                {
                    _checkForUpdates = new DelegateCommand(CheckForUpdates);
                }

                return _checkForUpdates;
            }
        }

        public StartPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            _version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.UpdateModeIsShown = false;
        }

        private void OpenMainVindow(object obj)
        {
            var mainVM = PetudaViewModelsFactory.CreatMainViewModel(this.navigationService);
            this.navigationService.OpenMainWindow(mainVM);
        }

        private void CheckForUpdates(object obj)
        {
            this.UpdateInfo = ApplicationUpdateHelper.GetUpdateInformation();

            // Update PetudaUpdate.exe file if new version was downloaded
            ApplicationUpdateHelper.UpdateFilesWithPrefix(AppDomain.CurrentDomain.BaseDirectory);

            AnalyzeUpdateInformation();
        }

        private void AnalyzeUpdateInformation()
        {
            if (_updateInfo == null ||
                !_updateInfo.IsFull ||
                !UpdateIsNeeded(_updateInfo.NewVersion))
            {
                return;
            }

            var messageVM = PetudaViewModelsFactory.CreateMessageViewModel(String.Format(Strings.UpdateNotificationTitle, _updateInfo.NewVersion),
                                                                           Strings.UpdateNotificationMessage,
                                                                           true);
            if (!navigationService.ShowMessage(messageVM))
            {
                return;
            }

            OpenUpdatePage();
        }

        private bool UpdateIsNeeded(Version newVersion)
        {
            return _version.CompareTo(newVersion) < 0;
        }

        private void OpenUpdatePage()
        {
            var updatePageViewModel = PetudaViewModelsFactory.CreateUpdatePageViewModel(this.navigationService, this._version, this.UpdateInfo);
            this.navigationService.OpenUpdatePage(updatePageViewModel);
            this.UpdateModeIsShown = true;
        }

    }//class
}//namespace