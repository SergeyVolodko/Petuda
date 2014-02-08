using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Petuda.Model.DDD;
using Petuda.Model.DDD.Exceptions;
using Petuda.Model.DDD.Services;
using Petuda.ViewModels.Events;
using Petuda.ViewModels.Helpers;
using Petuda.ViewModels.Navigation;
using Petuda.ViewModels.Resources;
using Petuda.ViewModels.ViewModelsFactory;

namespace Petuda.ViewModels
{
    public class ScriptContentViewModel: BaseViewModel
    {
        #region Events

        // Scroll to added to script joke
        public delegate void JokeSelectedInScriptHandler(object sender, IndexEventArgs e);
        public event JokeSelectedInScriptHandler JokeSelectedInScript;

        #endregion

        #region Fields

        private readonly IJokeService jokeService;
        private readonly IScriptService scriptService;
        private readonly INavigationService navigationService;

        #endregion

        #region Properties

        private int _selectedJokeInScriptIndex;
        private ObservableCollection<Joke> _jokesInSelectedScript;
        private Script _selectedScript;
        

        public Joke SelectedJokeInScript
        {
            get { return this.JokesInSelectedScript.Count == 0 || this.SelectedJokeInScriptIndex == -1 ? null : this.JokesInSelectedScript[this.SelectedJokeInScriptIndex]; }
        }

        public int SelectedJokeInScriptIndex
        {
            get { return _selectedJokeInScriptIndex; }
            set
            {
                if (value == _selectedJokeInScriptIndex)
                    return;

                _selectedJokeInScriptIndex = value;
                NotifyPropertChanged("SelectedJokeInScriptIndex");
                NotifyCommandsCanExecuteChanged();
            }
        }

        private void NotifyCommandsCanExecuteChanged()
        {
            if (_removeJokeFromScript != null)
            {
                _removeJokeFromScript.RaiseCanExecuteChanged();
            }
            if (_decreaseJokeIndexInScript != null)
            {
                _decreaseJokeIndexInScript.RaiseCanExecuteChanged();
            }
            if (_increaseJokeIndexInScript != null)
            {
                _increaseJokeIndexInScript.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Joke> JokesInSelectedScript
        {
            get { return _jokesInSelectedScript; }
            set
            {
                if (value == _jokesInSelectedScript)
                    return;

                _jokesInSelectedScript = value;
                NotifyPropertChanged("JokesInSelectedScript");
            }
        }

        public Script SelectedScript
        {
            get { return _selectedScript; }
            set
            {
                if (value == _selectedScript)
                    return;

                _selectedScript = value;
                NotifyPropertChanged("SelectedScript");
            }
        }
        
        #endregion

        #region Commands

        private DelegateCommand _removeJokeFromScript;
        private DelegateCommand _increaseJokeIndexInScript;
        private DelegateCommand _decreaseJokeIndexInScript;
        private DelegateCommand _startDragJoke;
        private DelegateCommand _dropJoke;
        private DelegateCommand _genDocWithTitles;
        private DelegateCommand _genDocWithNoTitles;
        private DelegateCommand _genDocOnlyTitles;
        private DelegateCommand _editJoke;

        public ICommand RemoveJokeFromScriptCommand
        {
            get
            {
                if (_removeJokeFromScript == null)
                {
                    _removeJokeFromScript = new DelegateCommand(RemoveJokeFromScript, CanRemoveJokeFromScript);
                }

                return _removeJokeFromScript;
            }
        }

        private bool CanRemoveJokeFromScript(object obj)
        {
            return this.SelectedJokeInScript != null;
        }

        public ICommand IncreaseJokeInScriptIndexCommand
        {
            get
            {
                if (_increaseJokeIndexInScript == null)
                {
                    _increaseJokeIndexInScript = new DelegateCommand(IncreaseJokeIndexInScript, CanIncreaseJokeIndexInScript);
                }

                return _increaseJokeIndexInScript;
            }
        }

        private bool CanIncreaseJokeIndexInScript(object obj)
        {
            var index = this.SelectedJokeInScriptIndex;
            return this.SelectedJokeInScript != null && index < this.JokesInSelectedScript.Count - 1;
        }

        public ICommand DecreaseJokeInScriptIndexCommand
        {
            get
            {
                if (_decreaseJokeIndexInScript == null)
                {
                    _decreaseJokeIndexInScript = new DelegateCommand(DecreaseJokeIndexInScript, CanDecreaseJokeIndexInScript);
                }

                return _decreaseJokeIndexInScript;
            }
        }

        private bool CanDecreaseJokeIndexInScript(object obj)
        {
            var index = this.SelectedJokeInScriptIndex;
            return this.SelectedJokeInScript != null && index > 0;
        }

        public ICommand StartDragJokeCommand
        {
            get
            {
                if (_startDragJoke == null)
                {
                    _startDragJoke = new DelegateCommand(StartDragJoke);
                }

                return _startDragJoke;
            }
        }

        public ICommand DropJokeCommand
        {
            get
            {
                if (_dropJoke == null)
                {
                    _dropJoke = new DelegateCommand(DropJoke);
                }

                return _dropJoke;
            }
        }
        
        public ICommand GenDocWithTitlesCommand
        {
            get
            {
                if (_genDocWithTitles == null)
                {
                    _genDocWithTitles = new DelegateCommand(GenDocWithTitles);
                }

                return _genDocWithTitles;
            }
        }

        public ICommand GenDocWithNoTitlesCommand
        {
            get
            {
                if (_genDocWithNoTitles == null)
                {
                    _genDocWithNoTitles = new DelegateCommand(GenDocWithNoTitles);
                }

                return _genDocWithNoTitles;
            }
        }

        public ICommand GenDocOnlyTitlesCommand
        {
            get
            {
                if (_genDocOnlyTitles == null)
                {
                    _genDocOnlyTitles = new DelegateCommand(GenDocOnlyTitles);
                }

                return _genDocOnlyTitles;
            }
        }

        public ICommand EditJokeCommand
        {
            get
            {
                if (_editJoke == null)
                {
                    _editJoke = new DelegateCommand(OpenUpdateJokeEditor, JokeIsSelected);
                }

                return _editJoke;
            }
        }

        private bool JokeIsSelected(object obj)
        {
            return this.SelectedJokeInScript != null;
        }

        #endregion

        #region Constructors

        public ScriptContentViewModel(INavigationService navigationService, IJokeService jokeService, IScriptService scriptService, Script script)
        {
            this.navigationService = navigationService;
            this.jokeService = jokeService;
            this.scriptService = scriptService;
            this.SelectedScript = script;

            EventsBus.Instance.JokeUpdated += OnJokeUpdated;
            EventsBus.Instance.AddJokeToSelectedScript += OnJokeAddedToScriptByButton;

            UpdateJokes();
        }

        #endregion

        #region Event handling

        private void OnJokeUpdated(object sender, JokeEventArgs e)
        {
            if (this.SelectedScript.ContainsJokeId(e.JokeID))
            {
                UpdateJokes();
            }
        }

        private void OnJokeAddedToScriptByButton(object sender, JokeEventArgs e)
        {
            AddJokeToScript(e.JokeID, null);
        }

        public void UpdateJokes()
        {
            this.JokesInSelectedScript = new ObservableCollection<Joke>();

            if (this.SelectedScript == null)
            {
                return;
            }

            foreach (var jokeID in this.SelectedScript.JokesIDs)
            {
                var joke = this.jokeService.Load(jokeID);
                this.JokesInSelectedScript.Add(joke);
            }
        }

        public void NotifyJokeSelectedInScript()
        {
            if (JokeSelectedInScript != null)
            {
                JokeSelectedInScript(this, new IndexEventArgs(this.SelectedJokeInScriptIndex));
            }
        }

        #endregion

        #region Add joke to selected script

        private void AddJokeToScript(Guid jokeID, int? index)
        {
            try
            {
                this.scriptService.AddJokeToScript(this.SelectedScript.ID, jokeID, index);

                UpdateJokes();

                this.SelectedJokeInScriptIndex = index.HasValue ? index.Value : this.JokesInSelectedScript.Count - 1;
                // Scroll to selected joke
                NotifyJokeSelectedInScript();
            }
            catch (ScriptAlreadyContainsJokeException e)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.ScriptContainsJokeTitle, String.Format(Strings.ScriptContainsJokeText, e.JokeInScriptIndex));
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, ex.Message);
            }
        }
        
        #endregion

        #region Edit joke

        private void OpenUpdateJokeEditor(object obj)
        {
            if (this.SelectedJokeInScript == null)
                return;

            var jokeEditorVM =
                PetudaViewModelsFactory.CreateJokeEditorViewModel(this.navigationService, (Joke) this.SelectedJokeInScript.Clone());

            this.navigationService.OpenJokeEditor(jokeEditorVM);
        }

        #endregion

        #region Move joke in script

        private void IncreaseJokeIndexInScript(object obj)
        {
            var index = this.SelectedJokeInScriptIndex;
            var newIndex = index + 1;

            MoveSelectedJoke(index, newIndex);
        }

        private void DecreaseJokeIndexInScript(object obj)
        {
            var index = this.SelectedJokeInScriptIndex;
            var newIndex = index - 1;

            MoveSelectedJoke(index, newIndex);
        }

        private void MoveSelectedJoke(int index, int? newIndex)
        {
            if (!newIndex.HasValue)
            {
                newIndex = this.JokesInSelectedScript.Count - 1;
            }

            scriptService.MoveJokeInScriptIndex(this.SelectedScript.ID, index, newIndex);

            UpdateJokes();

            this.SelectedJokeInScriptIndex = newIndex.Value;
            // Scroll to selected joke
            NotifyJokeSelectedInScript();
        }
        
        #endregion

        #region Remove joke from script

        private void RemoveJokeFromScript(object obj)
        {
            var jokeID = this.SelectedJokeInScript.ID;
            scriptService.RemoveJokeFromScript(this.SelectedScript.ID, jokeID);
            UpdateJokes();
        }

        #endregion

        #region Drag and Drop

        private void StartDragJoke(object obj)
        {
            if (!this.SelectedScript.IsEditable)
            {
                return;
            }

            var joke = this.SelectedJokeInScript;

            var popupVM = PetudaViewModelsFactory.CreateDraggingJokeViewModel(joke, this.SelectedJokeInScriptIndex);

            this.navigationService.OpenDraggingJokePopup(popupVM);
        }

        private void DropJoke(object dropParameter)
        {
            var popupVM = (DraggingJokeViewModel)this.navigationService.GetDraggingJokePopup();
            if (popupVM == null || popupVM.Joke == null)
            {
                return;
            }

            var dropIndex = (int?)dropParameter;

            if (popupVM.PrevIndex.HasValue)
            {
                MoveSelectedJoke(popupVM.PrevIndex.Value, dropIndex);
            }
            else
            {
                AddJokeToScript(popupVM.Joke.ID, dropIndex);
            }

            this.navigationService.CloseDraggingJokePopup();
        }

        #endregion

        #region Generate word document

        //////////////////////////////
        //
        // JokeTitleMode: 
        //
        //  WithTitles = 0
        //  WithOutTitles = 1
        //  OnlyTitles = 2
        //
        //////////////////////////////

        private void GenDocWithTitles(object obj)
        {
            try
            {
                this.navigationService.GenerateWordDocument(this, 0);
            }
            catch (Exception)
            {
                var messageVM = PetudaViewModelsFactory.CreateMessageViewModel(Resources.Strings.DocumentGenerationErrorTitle, Resources.Strings.DocumentGenerationErrorText, false);
                navigationService.ShowMessage(messageVM);
            }
        }
        private void GenDocWithNoTitles(object obj)
        {
            try
            {
                this.navigationService.GenerateWordDocument(this, 1);
            }
            catch (Exception)
            {
                var messageVM = PetudaViewModelsFactory.CreateMessageViewModel(Resources.Strings.DocumentGenerationErrorTitle, Resources.Strings.DocumentGenerationErrorText, false);
                navigationService.ShowMessage(messageVM);
            }
        }
        private void GenDocOnlyTitles(object obj)
        {
            try
            {
                this.navigationService.GenerateWordDocument(this, 2);
            }
            catch (Exception)
            {
                var messageVM = PetudaViewModelsFactory.CreateMessageViewModel(Resources.Strings.DocumentGenerationErrorTitle, Resources.Strings.DocumentGenerationErrorText, false);
                navigationService.ShowMessage(messageVM);
            }
        }

        #endregion

    }//class
}//namespace