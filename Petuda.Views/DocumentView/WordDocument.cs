using System;
using Petuda.ViewModels;
using Petuda.ViewModels.Navigation;
using Petuda.ViewModels.ViewModelsFactory;
using Petuda.Views.DocumentView.Generators;

namespace Petuda.Views.DocumentView
{
    public class WordDocument
    {
        private readonly INavigationService navigationService;
        private readonly ScriptContentViewModel viewModel;
        
        public WordDocument(INavigationService navigationService, ScriptContentViewModel scriptContentViewModel)
        {
            this.navigationService = navigationService;
            this.viewModel = scriptContentViewModel;
        }

        public void GenerateDoc(JokeTitleMode titleMode)
        {
            var script = this.viewModel.SelectedScript;

            if (script == null)
            {
                var messageVM = PetudaViewModelsFactory.CreateMessageViewModel(Resources.Strings.ScriptNotSelectedTitle, Resources.Strings.ScriptNotSelectedDocGenText, false);
                navigationService.ShowMessage(messageVM);
                return;
            }
            
            var documentTitle = script.Name;

            if (script.GameDate.HasValue)
            {
                documentTitle += String.Format(" {0} {1}", Resources.Strings.GameDate, script.GameDate.Value.ToString("dd-MMM-yyyy"));
            }

            var docGenerator = new WordDocumentGenerator(documentTitle);

            foreach (var joke in this.viewModel.JokesInSelectedScript)
            {
                docGenerator.AddJokeText(joke.Name, joke.Text, titleMode);
            }

            docGenerator.SaveDocument();
            
        }
    }
}