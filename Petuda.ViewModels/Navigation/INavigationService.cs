namespace Petuda.ViewModels.Navigation
{
    public interface INavigationService
    {
        void OpenUpdatePage(UpdatePageViewModel viewModel);
        void OpenMainWindow(MainViewModel viewModel);
        void OpenJokeEditor(JokeEditorViewModel viewModel);
        void OpenScriptPanel(ScriptPanelViewModel viewModel);
        void OpenScriptEditor(ScriptEditorViewModel viewModel);
        void OpenScriptContent(ScriptContentViewModel viewModel);

        bool ShowMessage(MessageViewModel viewModel);

        void OpenDraggingJokePopup(DraggingJokeViewModel viewModel);
        DraggingJokeViewModel GetDraggingJokePopup();
        void CloseDraggingJokePopup();

        void GenerateWordDocument(ScriptContentViewModel viewModel, byte titleMode);
    }
}