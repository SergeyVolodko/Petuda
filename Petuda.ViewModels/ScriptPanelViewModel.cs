using System;
using System.Linq;
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
    public class ScriptPanelViewModel : BaseViewModel
    {
        #region Fields

        private readonly INavigationService navigationService;
        private readonly IScriptService scriptService;
        
        private ScriptContentViewModel contentVM;

        #endregion

        #region Commands

        private DelegateCommand _deleteScript;
        private DelegateCommand _editScript;
        private DelegateCommand _addScript;

        public ICommand DeleteScriptCommand
        {
            get
            {
                if (_deleteScript == null)
                {
                    _deleteScript = new DelegateCommand(DeleteCurrentScript);
                }

                return _deleteScript;
            }
        }

        public ICommand EditScriptCommand
        {
            get
            {
                if (_editScript == null)
                {
                    _editScript = new DelegateCommand(OpenCurrentScripteEditor);
                }

                return _editScript;
            }
        }

        public ICommand AddScriptCommand
        {
            get
            {
                if (_addScript == null)
                {
                    _addScript = new DelegateCommand(OpenAddScriptEditor);
                }

                return _addScript;
            }
        }

        #endregion

        #region Properties

        private Script _script;
        private ObservableCollection<Script> _scripts;
        private ObservableCollection<string> _leagues;
        private bool _scriptContentEditorOpened = false;

        public bool ScriptContentEditorOpened
        {
            get { return _scriptContentEditorOpened; }
            set
            {
                if (value == _scriptContentEditorOpened)
                    return;

                _scriptContentEditorOpened = value;
                NotifyPropertChanged("ScriptContentEditorOpened");
            }
        }

        public ObservableCollection<Script> Scripts
        {
            get { return _scripts; }
            set
            {
                if (value == _scripts)
                    return;

                _scripts = value;
                NotifyPropertChanged("Scripts");
            }
        }

        public Script SelectedScript
        {
            get { return _script; }
            set
            {
                if (value == _script)
                    return;

                _script = value;
                NotifyPropertChanged("SelectedScript");

                OpenScriptContentEditor();
            }
        }

        public ObservableCollection<string> Leagues
        {
            get { return _leagues; }
            set
            {
                if (value == _leagues)
                    return;

                _leagues = value;
                NotifyPropertChanged("Leagues");
            }
        }

        #endregion

        #region Constructor

        public ScriptPanelViewModel(INavigationService navigationService, IScriptService scriptService)
        {
            this.navigationService = navigationService;
            this.scriptService = scriptService;

            EventsBus.Instance.ScriptCreated += OnScriptCreated;
            EventsBus.Instance.ScriptUpdated += OnScriptUpdated;
            EventsBus.Instance.JokeDeleted += UpdateScriptAfterJokeDeleted;

            LoadScripts();
        }

        #endregion

        private void LoadScripts()
        {
            Scripts = new ObservableCollection<Script>(scriptService.LoadAllScripts());
        }

        private void OpenScriptContentEditor()
        {
            contentVM = PetudaViewModelsFactory.CreateScriptContentViewModel(this.navigationService, this._script);
            this.navigationService.OpenScriptContent(contentVM);

            ScriptContentEditorOpened = true;
        }
        
        public void UpdateScriptAfterJokeDeleted(object sender, JokeEventArgs e)
        {
            var currentScriptId = this.SelectedScript != null ? this.SelectedScript.ID : Guid.Empty;

            LoadScripts();

            if (currentScriptId != Guid.Empty)
            {
                // Content view model selected script is binded on this.SelectedScript
                // update of this.SelectedScript causes Content view model selected update
                this.SelectedScript = this.Scripts.FirstOrDefault(s => s.ID == currentScriptId);
            }

            if (contentVM == null)
            {
                return;
            }
            
            contentVM.UpdateJokes();
        }

        #region Create script

        private void OpenAddScriptEditor(object obj)
        {
            var scriptEditorVM = PetudaViewModelsFactory.CreateScriptEditorViewModel(this.navigationService);
            
            this.navigationService.OpenScriptEditor(scriptEditorVM);
        }

        private void OnScriptCreated(object sender, ScriptEventArgs e)
        {
            LoadScripts();

            var createdScript = Scripts.FirstOrDefault(s => s.ID == e.ScriptID);

            this.SelectedScript = createdScript;
        }

        #endregion

        #region Update script

        private void OpenCurrentScripteEditor(object obj)
        {
            if (this.SelectedScript == null)
            {
                return;
            }

            var scriptEditorVM = PetudaViewModelsFactory.CreateScriptEditorViewModel(this.navigationService, (Script)SelectedScript.Clone());
            
            this.navigationService.OpenScriptEditor(scriptEditorVM);
        }

        private void OnScriptUpdated(object sender, ScriptEventArgs e)
        {
            LoadScripts();

            var updatedScript = Scripts.FirstOrDefault(s => s.ID == e.ScriptID);

            this.SelectedScript = updatedScript;
        }

        #endregion

        #region Delete script

        private void DeleteCurrentScript(object obj)
        {
            if (this.SelectedScript == null)
            {
                return;
            }

            var gameDate = Resources.Strings.NotSpecified;
            if (this.SelectedScript.GameDate.HasValue)
            {
                gameDate = SelectedScript.GameDate.Value.ToString("dd.MM.yyyy");
            }
            var message = String.Format("{0} {1} ?\n{2} {3}", Strings.RemoveScriptText, SelectedScript.Name, Strings.GameDate, gameDate);
            var messageVM = PetudaViewModelsFactory.CreateMessageViewModel(Strings.RemoveScriptTitle, message, true);

            if (navigationService.ShowMessage(messageVM))
            {
                try
                {
                    this.scriptService.RemoveScript(SelectedScript.ID);

                    LoadScripts();

                    this.SelectedScript = null;
                }
                catch (SaveFileException ex)
                {
                    ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, String.Format(Strings.CantWriteFileErrorMessage, ex.FileName));
                }
                catch (Exception ex)
                {
                    ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, ex.Message);
                }
            }
        }

        #endregion

    }//class
}//namespace