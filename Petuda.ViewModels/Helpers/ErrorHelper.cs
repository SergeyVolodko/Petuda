using Petuda.ViewModels.Navigation;
using Petuda.ViewModels.Resources;
using Petuda.ViewModels.ViewModelsFactory;

namespace Petuda.ViewModels.Helpers
{
    public static class ErrorHelper
    {
        public static void ShowErrorMessage(INavigationService navigationService, string title, string text)
        {
            var messageVM = PetudaViewModelsFactory.CreateMessageViewModel(title, text, false);
            navigationService.ShowMessage(messageVM);
        }
    }
}
