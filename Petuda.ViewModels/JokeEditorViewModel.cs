using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Petuda.Model.DDD;
using Petuda.Model.DDD.Exceptions;
using Petuda.Model.DDD.Helpers;
using Petuda.Model.DDD.Services;
using Petuda.ViewModels.Events;
using Petuda.ViewModels.Helpers;
using Petuda.ViewModels.Navigation;
using Petuda.ViewModels.Resources;
using Petuda.ViewModels.ViewModelsFactory;
using PetudaDAL.XML.Exceptions;

namespace Petuda.ViewModels
{
    public class JokeEditorViewModel : BaseViewModel
    {
        #region Fields
        
        private readonly IJokeService jokeService;
        private readonly INavigationService navigationService;

        private readonly Joke inputJoke = null;

        private string _text;
        private string _name;
        private string _theme;
        private string _tags;
        private bool _nameIsNotValid = true;
        private bool _editMode = false;
        private ObservableCollection<String> _themes;

        private DelegateCommand _saveJokeCommand;
        #endregion

        #region Properties

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

        public bool NameIsNotValid
        {
            get { return _nameIsNotValid; }
            set
            {
                if (value == _nameIsNotValid)
                    return;

                _nameIsNotValid = value;
                NotifyPropertChanged("NameIsNotValid");
            }
        }

        public ObservableCollection<string> Themes
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

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                    return;

                _name = value;
                NotifyPropertChanged("Name");
                this.NameIsNotValid = String.IsNullOrEmpty(this.Name);

                if (_saveJokeCommand != null)
                {
                    _saveJokeCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Theme
        {
            get { return _theme; }
            set
            {
                if (value == _theme)
                    return;

                _theme = value;
                NotifyPropertChanged("Theme");
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (value == _text)
                    return;

                _text = value;
                NotifyPropertChanged("Text");
            }
        }

        public string Tags
        {
            get { return _tags; }
            set
            {
                if (value == _tags)
                    return;

                _tags = value;
                NotifyPropertChanged("Tags");
            }
        }

        #endregion

        #region Commands

        public ICommand SaveJokeCommand
        {
            get
            {
                if (_saveJokeCommand == null)
                {
                    _saveJokeCommand = new DelegateCommand(SaveJoke, CanSaveJoke);
                }

                return _saveJokeCommand;
            }
        }

        private bool CanSaveJoke(object obj)
        {
            return !this.NameIsNotValid;
        }

        #endregion

        #region Constructors

        public JokeEditorViewModel(INavigationService navigationService, IJokeService jokeService)
        {
            this.navigationService = navigationService;
            this.jokeService = jokeService;
            _themes = new ObservableCollection<String>(jokeService.GetAllThemes());
        }

        public JokeEditorViewModel(INavigationService navigationService, IJokeService jokeService, Joke inputJoke)
            : this(navigationService, jokeService)
        {
            this.Name = inputJoke.Name;
            this.Theme = inputJoke.Theme;
            this.Tags = GetTagsString(inputJoke.Tags);
            this.Text = inputJoke.Text;
            this.inputJoke = inputJoke;
            this.EditMode = true;
        }

        #endregion

        private String GetTagsString(List<String> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                return String.Empty;
            }

            return tags.Aggregate((t, next) => t + " " + next);
        }

        private void SaveJoke(object obj)
        {
            if (this.inputJoke != null)
            {
                UpdateJoke();
            }
            else
            {
                CreateJoke();
            }
        }

        private void CreateJoke()
        {
            try
            {
                var tags = StringHelper.Split(this.Tags);

                var newJoke = jokeService.CreateJoke(this.Name, this.Theme, this.Text, tags.ToList());

                EventsBus.Instance.RaiseJokeCreated(newJoke.ID);
            }
            catch (MissingRequiredField)
            {
                this.NameIsNotValid = true;
            }
            catch (JokeCantBeCreatedException ex)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, String.Format(Strings.CantWriteFileErrorMessage, "Jokes.xml"));
                ReopenEditor(ex.Joke);
            }
            //catch (SaveFileException ex)
            //{
            //    ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, String.Format(Strings.CantWriteFileErrorMessage, ex.FileName));
            //}
        }

        private void UpdateJoke()
        {
            try
            {
                var tags = StringHelper.Split(this.Tags);

                this.inputJoke.Name = this.Name;
                this.inputJoke.Theme = this.Theme;
                this.inputJoke.Text = this.Text;
                this.inputJoke.Tags = tags.ToList();
            
                this.jokeService.UpdateJoke(this.inputJoke);
                EventsBus.Instance.RaiseJokeUpdated(this.inputJoke.ID);
            }
            catch (SaveFileException ex)
            {
                ErrorHelper.ShowErrorMessage(this.navigationService, Strings.Error, String.Format(Strings.CantWriteFileErrorMessage, ex.FileName));
                ReopenEditor(this.inputJoke);
            }
        }

        private void ReopenEditor(Joke joke)
        {
            var viewModel = PetudaViewModelsFactory.CreateJokeEditorViewModel(this.navigationService, (Joke)joke.Clone());

            this.navigationService.OpenJokeEditor(viewModel);
        }

    }//class
}//namespace