using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Petuda.ViewModels.Events;

namespace Petuda.Views.Controls
{
    /// <summary>
    /// Interaction logic for ScriptPanel.xaml
    /// </summary>
    public partial class ScriptPanel : UserControl
    {
        private bool isFirstOpen = true;

        public ScriptPanel()
        {
            InitializeComponent();
        }

        private void ScriptPanel_Loaded(object sender, RoutedEventArgs e)
        {
            SetStackPanelSciptMenuInitialMargin();
        }

        private void labelChooseText_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            comboBoxSelectScript.IsDropDownOpen = !comboBoxSelectScript.IsDropDownOpen;
        }

        private void scriptContent_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.isFirstOpen && (bool)e.NewValue)
            {
                AnimateScriptMenuMovement();
                buttonAddScript.Margin = new Thickness(10, 0, 0, 0);
                this.isFirstOpen = false;
            }
        }

        private void SetStackPanelSciptMenuInitialMargin()
        {
            var topMargin = 0.5 * (Application.Current.MainWindow.ActualHeight - 240.0);
            stackPanelSciptMenu.Margin = new Thickness(0.0, topMargin, 0.0, 0.0);
        }

        private void AnimateScriptMenuMovement()
        {
            // Create a storyboard to apply the animation.
            var moveMenuOnTopStoryboard = new Storyboard();

            var thicknessAnimation = (ThicknessAnimation)this.Resources["moveMenuOnTop"];
            moveMenuOnTopStoryboard.Children.Add(thicknessAnimation);

            var widthChangeAnmation = (DoubleAnimation)this.Resources["changeAddButtonScaleX"];
            moveMenuOnTopStoryboard.Children.Add(widthChangeAnmation);

            var heightChangeAnmation = (DoubleAnimation)this.Resources["changeAddButtonScaleY"];
            moveMenuOnTopStoryboard.Children.Add(heightChangeAnmation);

            var makeEditButtonVisible = (DoubleAnimation)this.Resources["makeEditButtonVisible"];
            moveMenuOnTopStoryboard.Children.Add(makeEditButtonVisible);

            var makeDeleteButtonVisible = (DoubleAnimation)this.Resources["makeDeleteButtonVisible"];
            moveMenuOnTopStoryboard.Children.Add(makeDeleteButtonVisible);

            // Start the storyboard
            moveMenuOnTopStoryboard.Begin(this);
        }

        // update script content view model event bindings
        private void ComboBoxSelectScript_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scriptContent.IsEnabled)
            {
                scriptContent.IsEnabled = false;
                scriptContent.IsEnabled = true;
            }
        }
    }
}