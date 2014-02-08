using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Petuda.Model.DDD;
using Petuda.Model.DDD.Exceptions;
using Petuda.Model.DDD.Services;
using Petuda.ViewModels.Events;
using Petuda.ViewModels.Helpers;
using Petuda.ViewModels.Navigation;
using Petuda.ViewModels.Resources;
using Petuda.ViewModels.ViewModelsFactory;
using PetudaDAL.XML.Exceptions;

namespace Petuda.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Events

        public delegate void DragStartHandler(object sender, EventArgs e);

        public event DragStartHandler DragStarted;

        public delegate void JokeAddedOrChangedHandler(object sender, IndexEventArgs e);

        public event JokeAddedOrChangedHandler JokeAddedOrChanged;

        #endregion

        #region Fields

        private readonly INavigationService navigationService;

        private readonly IJokeService jokeService;
        private readonly IScriptService scriptService;

        private ScriptPanelViewModel scriptPanelVM;

        #endregion

        #region Properties

        private Joke _selectedJoke;
        private ObservableCollection<Joke> _jokes;
        private ObservableCollection<string> _themes;

        private String _textFilter = "";
        private String _themeFilter;
        private DateTime? _dateFromFilter;
        private DateTime? _dateToFilter;
        //private string _leagueFilter;
        //private bool _isNeverUsed;
        private String _noJokesMessage = "";

        public String NoJokesMessage
        {
            get { return _noJokesMessage; }
            set
            {
                if (value == _noJokesMessage)
                    return;

                _noJokesMessage = value;
                NotifyPropertChanged("NoJokesMessage");
            }
        }

        #region filters

        public String TextFilter
        {
            get { return _textFilter; }
            set
            {
                if (value == _textFilter)
                    return;

                _textFilter = value;
                NotifyPropertChanged("TextFilter");
                FilterJokes();
            }
        }

        public String ThemeFilter
        {
            get { return _themeFilter; }
            set
            {
                if (value == _themeFilter)
                    return;

                _themeFilter = value;
                NotifyPropertChanged("ThemeFilter");
                FilterJokes();
            }
        }

        public DateTime? DateFromFilter
        {
            get { return _dateFromFilter; }
            set
            {
                if (value == _dateFromFilter)
                    return;

                _dateFromFilter = value;
                NotifyPropertChanged("DateFromFilter");
                FilterJokes();
            }
        }

        public DateTime? DateToFilter
        {
            get { return _dateToFilter; }
            set
            {
                if (value == _dateToFilter)
                    return;

                _dateToFilter = value;
                NotifyPropertChanged("DateToFilter");
                FilterJokes();
            }
        }

        //public string LeagueFilter
        //{
        //    get { return _leagueFilter; }
        //    set
        //    {
        //        if (value == _leagueFilter)
        //            return;

        //        _leagueFilter = value;
        //        NotifyPropertChanged("LeagueFilter");
        //        FilterJokes();
        //    }
        //}

        //public bool IsNeverUsed
        //{
        //    get { return _isNeverUsed; }
        //    set
        //    {
        //        if (value == _isNeverUsed)
        //            return;

        //        _isNeverUsed = value;
        //        NotifyPropertChanged("IsNeverUsed");
        //        FilterJokes();
        //    }
        //}

        #endregion

        public Joke SelectedJoke
        {
            get { return _selectedJoke; }
            set
            {
                if (value == _selectedJoke)
                    return;

                _selectedJoke = value;
                NotifyPropertChanged("SelectedJoke");

                _editJoke.RaiseCanExecuteChanged();
                _deleteJoke.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Joke> Jokes
        {
            get { return _jokes; }
            set
            {
                if (value == _jokes)
                    return;

                _jokes = value;
                NotifyPropertChanged("Jokes");
            }
        }

        public ObservableCollection<String> Themes
        {
            get { return _themes; }
            set
            {
                if (value == _themes)
                    return;

                _themes = value;
                NotifyPropertChanged("Themes");
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _addJoke;
        private DelegateCommand _editJoke;
        private DelegateCommand _deleteJoke;
        private DelegateCommand _openScriptPanel;
        private DelegateCommand _addJokeToScript;

        public ICommand AddJokeToScriptCommand
        {
            get
            {
                if (_addJokeToScript == null)
                {
                    _addJokeToScript = new DelegateCommand(AddJokeToScript);
                }

                return _addJokeToScript;
            }
        }

        public ICommand OpenScriptPanelCommand
        {
            get
            {
                if (_openScriptPanel == null)
                {
                    _openScriptPanel = new DelegateCommand(OpenScriptPanel);
                }

                return _openScriptPanel;
            }
        }

        public ICommand DeleteJokeCommand
        {
            get
            {
                if (_deleteJoke == null)
                {
                    _deleteJoke = new DelegateCommand(DeleteSelectedJoke, JokeIsSelected);
                }

                return _deleteJoke;
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
            return this.SelectedJoke != null;
        }

        public ICommand AddJokeCommand
        {
            get
            {
                if (_addJoke == null)
                {
                    _addJoke = new DelegateCommand(OpenAddJokeEditor);
                }

                return _addJoke;
            }
        }

        #endregion

        #region Constructors

        public MainViewModel(INavigationService navigationService, IJokeService jokeService, IScriptService scriptService)
        {
            this.navigationService = navigationService;
            this.jokeService = jokeService;
            this.scriptService = scriptService;

            _jokes = new ObservableCollection<Joke>(jokeService.LoadAllJokes());
            _themes = new ObservableCollection<String>(this.jokeService.GetAllThemes());

            DragStarted += OnDragStarted;
            EventsBus.Instance.JokeCreated += this.OnJokeCreated;
            EventsBus.Instance.JokeUpdated += this.OnJokeUpdated;

            UpdateNoJokesMessage();
        }

        #endregion

        private void UpdateNoJokesMessage()
        {
            // check if any joke is shown
            if (_jokes.Count > 0)
            {
                this.NoJokesMessage = "";
                return;
            }

            // no jokes are shown: select a message text depending on jokes in base count
            this.NoJokesMessage = this.jokeService.GetJokesCount() > 0 ? Strings.NoSearchResults : Strings.BaseIsEmpty;
        }

        private void FilterJokes()
        {
            var filtredJokes = jokeService.GetFiltredJokes(this.TextFilter, this.ThemeFilter, this.DateFromFilter, this.DateToFilter);
            this.Jokes = new ObservableCollection<Joke>(filtredJokes);
            
            UpdateNoJokesMessage();
        }

        private void RefreshThemes()
        {
            this.Themes = new ObservableCollection<String>(jokeService.GetAllThemes());
        }

        private void UpdateSelectedJoke(Guid id)
        {
            var joke = this.Jokes.FirstOrDefault(j => j.ID == id);
            if (joke != null)
            {
                this.SelectedJoke = joke;
            }
            else
            {
                this.SelectedJoke = null; 
            }

            // Scroll to selected joke
            NotifyJokeAddedOrChanged();
        }

        public void NotifyJokeAddedOrChanged()
        {
            if (JokeAddedOrChanged != null)
            {
                var index = this.Jokes.IndexOf(this.SelectedJoke);

                JokeAddedOrChanged(this, new IndexEventArgs(index));
            }
        }

        #region Add joke

        private void OpenAddJokeEditor(object obj)
        {
            var jokeEditorVM = PetudaViewModelsFactory.CreateJokeEditorViewModel(this.navigationService);
            
            this.navigationService.OpenJokeEditor(jokeEditorVM);
        }

        private void OnJokeCreated(object sender, JokeEventArgs e)
        {
            RefreshThemes();
            FilterJokes();
            UpdateSelectedJoke(e.JokeID);
        }

        #endregion

        #region Update joke

        private void OpenUpdateJokeEditor(object obj)
        {
            if (this.SelectedJoke == null)
                return;

            var jokeEditorVM = PetudaViewModelsFactory.CreateJokeEditorViewModel(this.navigationService, (Joke)this.SelectedJoke.Clone());
            
            this.navigationService.OpenJokeEditor(jokeEditorVM);
        }

        private void OnJokeUpdated(object sender, JokeEventArgs e)
        {
            RefreshThemes();
            FilterJokes();
            UpdateSelectedJoke(e.JokeID);
        }

        #endregion

        #region Delete joke

        private void DeleteSelectedJoke(object obj)
        {
            if (this.SelectedJoke == null)
                return;

            var message = String.Format(Strings.RemoveJokeText, SelectedJoke.Name);
            var messageVM = PetudaViewModelsFactory.CreateMessageViewModel(Strings.RemoveJokeTitle, message, true);

            if (this.navigationService.ShowMessage(messageVM))
            {
                try
                {
                    this.scriptService.RemoveJokeFromScripts(this.SelectedJoke.ID);

                    EventsBus.Instance.RaiseJokeDeleted(this.SelectedJoke.ID);

                    this.jokeService.DeleteJoke(this.SelectedJoke.ID);

                    RefreshThemes();
                    FilterJokes();
                }
                catch (JokeCantBeDeletedException)
                {
                    ErrorHelper.ShowErrorMessage(this.navigationService, Strings.JokeCantBeDeletedTitle, Strings.JokeCantBeDeletedText);
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

        #region Open script panel

        private void OpenScriptPanel(object obj)
        {
            scriptPanelVM = PetudaViewModelsFactory.CreateScriptPanelViewModel(navigationService);
            navigationService.OpenScriptPanel(scriptPanelVM);
        }

        #endregion

        #region Add joke to script

        private void AddJokeToScript(object obj)
        {
            var script = this.scriptPanelVM != null ? this.scriptPanelVM.SelectedScript : null;

            if (SelectedJoke == null || script == null)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.ScriptNotSelectedTitle, Strings.ScriptNotSelectedMessageText);
                return;
            }

            if (!script.IsEditable)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.ScriptIsPlayedTitle, Strings.ScriptIsPlayedMessageText);
                return;
            }

            EventsBus.Instance.RaiseAddJokeToSelectedScript(this.SelectedJoke.ID);
        }

        public void NotifyStartJokeDrag()
        {
            if (DragStarted != null)
            {
                DragStarted(this, new EventArgs());
            }
        }

        private void OnDragStarted(object sender, EventArgs e)
        {
            var popupVM = PetudaViewModelsFactory.CreateDraggingJokeViewModel(this.SelectedJoke);

            this.navigationService.OpenDraggingJokePopup(popupVM);
        }

        public void CloseDraggingPopup()
        {
            this.navigationService.CloseDraggingJokePopup();
        }

        #endregion

    }//class
}//namespace