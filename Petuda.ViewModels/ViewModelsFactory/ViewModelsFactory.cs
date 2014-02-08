using System;
using Petuda.Model.DDD;
using Petuda.Model.DDD.Factories;
using Petuda.Model.DDD.Repositories;
using Petuda.Model.DDD.Services;
using Petuda.ViewModels.Helpers;
using Petuda.ViewModels.Navigation;
using PetudaDAL.XML;

namespace Petuda.ViewModels.ViewModelsFactory
{
    public static class PetudaViewModelsFactory
    {
        private readonly static JokeService jokeService;
        private readonly static ScriptService scriptService;

        static PetudaViewModelsFactory()
        {
            var xmlJokeDAL = new JokeXmlDao();
            var jokeRepository = new JokeRepository(xmlJokeDAL);
            var jokeFactory = new JokeFactory();

            var xmlScriptDAL = new ScriptXmlDao();
            var scriptRepository = new ScriptRepository(xmlScriptDAL);
            var scriptFactory = new ScriptFactory();

            jokeService = new JokeService(jokeRepository, jokeFactory);
            scriptService = new ScriptService(scriptRepository, scriptFactory);
        }

        public static StartPageViewModel CreateStartViewModel(INavigationService navigatinService)
        {
            return new StartPageViewModel(navigatinService);
        }

        public static UpdatePageViewModel CreateUpdatePageViewModel(INavigationService navigationService, Version version, UpdateInformation updateInfo)
        {
            return new UpdatePageViewModel(navigationService, version, updateInfo);
        }

        public static MainViewModel CreatMainViewModel(INavigationService navigatinService)
        {
            return new MainViewModel(navigatinService, jokeService, scriptService);
        }

        public static JokeEditorViewModel CreateJokeEditorViewModel(INavigationService navigatinService)
        {
            return new JokeEditorViewModel(navigatinService, jokeService);
        }

        public static JokeEditorViewModel CreateJokeEditorViewModel(INavigationService navigatinService, Joke inputJoke)
        {
            return new JokeEditorViewModel(navigatinService, jokeService, inputJoke);
        }

        public static ScriptPanelViewModel CreateScriptPanelViewModel(INavigationService navigationService)
        {
            return new ScriptPanelViewModel(navigationService, scriptService);
        }

        public static ScriptEditorViewModel CreateScriptEditorViewModel(INavigationService navigationService)
        {
            return new ScriptEditorViewModel(navigationService, scriptService);
        }

        public static ScriptEditorViewModel CreateScriptEditorViewModel(INavigationService navigationService, Script inputScript)
        {
            return new ScriptEditorViewModel(navigationService, scriptService, inputScript);
        }

        public static MessageViewModel CreateMessageViewModel(string title, string message, bool isDialog)
        {
            return new MessageViewModel(title, message, isDialog);
        }

        public static ScriptContentViewModel CreateScriptContentViewModel(INavigationService navigationService, Script script)
        {
            return new ScriptContentViewModel(navigationService, jokeService, scriptService, script);
        }

        public static DraggingJokeViewModel CreateDraggingJokeViewModel(Joke joke, int? prevIndex = null)
        {
            return new DraggingJokeViewModel(joke, prevIndex);
        }

    }//class
}//namespace