using System.Windows;
using Petuda.ViewModels;
using Petuda.ViewModels.Navigation;
using Petuda.Views.DocumentView;
using Petuda.Views.DocumentView.Generators;

namespace Petuda.Views.Navigation
{
    public class PetudaNavigationService : INavigationService
    {
        private MainWindow mainWindow;
        private readonly StartPage startPage;

        public PetudaNavigationService(StartPage startPage)
        {
            this.startPage = startPage;
        }

        public void OpenUpdatePage(UpdatePageViewModel viewModel)
        {
            this.startPage.updatePage.DataContext = viewModel;
        }

        public void OpenMainWindow(MainViewModel viewModel)
        {
            var mainWindow = new MainWindow(viewModel);
            mainWindow.Show();
            Application.Current.MainWindow = this.mainWindow = mainWindow;
        }

        public void OpenJokeEditor(JokeEditorViewModel viewModel)
        {
            var jokeEditor = new JokeEditor();
            jokeEditor.DataContext = viewModel;
            jokeEditor.Show();
        }

        public void OpenScriptPanel(ScriptPanelViewModel viewModel)
        {
            this.mainWindow.scriptPanel.DataContext = viewModel;
        }

        public void OpenScriptEditor(ScriptEditorViewModel viewModel)
        {
            var scriptEditor = new ScriptEditor();
            scriptEditor.DataContext = viewModel;
            scriptEditor.ShowDialog();
        }

        public void OpenScriptContent(ScriptContentViewModel viewModel)
        {
            this.mainWindow.scriptPanel.scriptContent.DataContext = viewModel;
        }

        public bool ShowMessage(MessageViewModel viewModel)
        {
            var messageWindow = new MessageWindow();
            messageWindow.DataContext = viewModel;
            messageWindow.ShowDialog();
            return messageWindow.DialogResult ?? false;
        }

        public void OpenDraggingJokePopup(DraggingJokeViewModel viewModel)
        {
            this.mainWindow.popupDraggingJoke.DataContext = viewModel;
            viewModel.IsDragging = true;
        }

        public DraggingJokeViewModel GetDraggingJokePopup()
        {
            return (DraggingJokeViewModel)this.mainWindow.popupDraggingJoke.DataContext;
        }

        public void CloseDraggingJokePopup()
        {
            var viewModel = (DraggingJokeViewModel)this.mainWindow.popupDraggingJoke.DataContext;
            if (viewModel != null)
            {
                viewModel.IsDragging = false;
            }
        }

        public void GenerateWordDocument(ScriptContentViewModel viewModel, byte titleMode)
        {
            var document = new WordDocument(this, viewModel);
            document.GenerateDoc((JokeTitleMode)titleMode);
        }
    }
}