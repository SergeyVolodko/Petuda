using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Petuda.Model.DDD;
using Petuda.Model.DDD.Services;
using Petuda.ViewModels.Events;
using Petuda.ViewModels.Helpers;
using Petuda.ViewModels.Navigation;
using Petuda.ViewModels.Resources;
using Petuda.ViewModels.ViewModelsFactory;
using PetudaDAL.XML.Exceptions;

namespace Petuda.ViewModels
{
    public class ScriptEditorViewModel: BaseViewModel
    {
        #region Fields

        private readonly Script inputScript = null;

        private readonly IScriptService scriptService;
        private readonly INavigationService navigationService;

        #endregion

        #region Properties

        private bool _editMode = false;
        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                if (value == _editMode)
                    return;

                _editMode = value;
                NotifyPropertChanged("EditMode");
            }
        }

        private bool _nameIsNotValid = true;
        public bool NameIsNotValid
        {
            get { return _nameIsNotValid; }
            set
            {
                if (value == _nameIsNotValid)
                    return;

                _nameIsNotValid = value;
                NotifyPropertChanged("NameIsNotValid");
                
                if (_saveScriptCommand != null)
                {
                    _saveScriptCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                    return;

                _name = value;
                NotifyPropertChanged("Name");
                NameIsNotValid = String.IsNullOrEmpty(_name);
            }
        }

        //private string _league;
        //private ObservableCollection<string> _leagues;
        //public string League
        //{
        //    get { return _league; }
        //    set
        //    {
        //        if (value == _league)
        //            return;

        //        _league = value;
        //        NotifyPropertChanged("League");
        //    }
        //}

        //public ObservableCollection<string> Leagues
        //{
        //    get { return _leagues; }
        //    set
        //    {
        //        if (value == _leagues)
        //            return;

        //        _leagues = value;
        //        NotifyPropertChanged("Leagues");
        //    }
        //}

        private DateTime? _gameDate;
        public DateTime? GameDate
        {
            get { return _gameDate; }
            set
            {
                if (value == _gameDate)
                    return;

                _gameDate = value;
                NotifyPropertChanged("GameDate");
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _saveScriptCommand;
        
        public ICommand SaveScriptCommand
        {
            get
            {
                if (_saveScriptCommand == null)
                {
                    _saveScriptCommand = new DelegateCommand(SaveScript, CanSaveScript);
                }

                return _saveScriptCommand;
            }
        }

        private bool CanSaveScript(object obj)
        {
            return !this.NameIsNotValid;
        }
        
        #endregion

        #region Constructors

        public ScriptEditorViewModel(INavigationService navigationService, IScriptService scriptService)
        {
            this.scriptService = scriptService;
            this.navigationService = navigationService;
            //Leagues = leagues;
        }

        public ScriptEditorViewModel(INavigationService navigationService, IScriptService scriptService, Script inputScript)
            : this(navigationService, scriptService)
        {
            this.Name = inputScript.Name;
            //League = inputScript.League;
            this.GameDate = inputScript.GameDate;
            this.inputScript = inputScript;
            this.EditMode = true;
        }

        #endregion
        
        private void SaveScript(object obj)
        {
            if (this.GameDate.HasValue && 
                this.GameDate.Value.Date < DateTime.Now.Date && 
                !ConfirmExpiredDate())
            {
                this.navigationService.OpenScriptEditor(this);
                return;
            }

            if (this.inputScript != null)
            {
                UpdateScript();
            }
            else
            {
                CreateScript();
            }
        }

        private void CreateScript()
        {
            try
            {
                var newScript = scriptService.CreateScript(this.Name, this.GameDate);

                EventsBus.Instance.RaiseScriptCreated(newScript.ID);
            }
            catch (SaveFileException ex)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, String.Format(Strings.CantWriteFileErrorMessage, ex.FileName));
            }
        }

        private void UpdateScript()
        {
            try
            {
                this.inputScript.Name = this.Name;
                this.inputScript.GameDate = this.GameDate;
                //_inputScript.League = this.League;

                scriptService.UpdateScript(this.inputScript);

                EventsBus.Instance.RaiseScriptUpdated(this.inputScript.ID);
            }
            catch (SaveFileException ex)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, String.Format(Strings.CantWriteFileErrorMessage, ex.FileName));
            }
        }

        private bool ConfirmExpiredDate()
        {
            var messageVM = PetudaViewModelsFactory.CreateMessageViewModel(Strings.SelectedGameDateHasExpiredTitle, Strings.SelectedGameDateHasExpiredText, true);
            return this.navigationService.ShowMessage(messageVM);
        }

    }//class
}//namespace