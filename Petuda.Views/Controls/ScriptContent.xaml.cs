using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Petuda.ViewModels;
using Petuda.ViewModels.Events;
using Petuda.Views.Helpers;

namespace Petuda.Views.Controls
{
    /// <summary>
    /// Interaction logic for ScriptContent.xaml
    /// </summary>
    public partial class ScriptContent : UserControl
    {
        private ScriptContentViewModel viewModel;

        public ScriptContent()
        {
            InitializeComponent();
        }

        private void ScriptContentEditorPanel_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Scroll to added to script joke
            if ((bool)e.NewValue)
            {
                this.viewModel = (ScriptContentViewModel)this.DataContext;
                this.viewModel.JokeSelectedInScript += ViewModelOnJokeSelectedInScript;
            }
         }

        #region Scroll to added to script joke

        private void ViewModelOnJokeSelectedInScript(object sender, IndexEventArgs e)
        {
            if (e.Index >= 0)
            {
                ScrollToJokeInScript(e.Index);
            }
        }

        private void ScrollToJokeInScript(int jokeInScriptIndex)
        {
            var jokeInScript = dataGridJokesInScript.Items[jokeInScriptIndex];
            dataGridJokesInScript.ScrollIntoView(jokeInScript);
            dataGridJokesInScript.Focus();
        }
        
        #endregion

        #region Drag and Drop

        private void dataGridJokesInScript_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("DraggedJokeFormat"))
            {
                int? dropIndex = null;
                // Get the dragged Item
                var dataGridRow = PetudaVisualTreeHelper.FindAnchestor<DataGridRow>((DependencyObject)e.OriginalSource);

                if (dataGridRow != null)
                {
                    dropIndex = dataGridJokesInScript.ItemContainerGenerator.IndexFromContainer(dataGridRow);
                }

                this.viewModel = (ScriptContentViewModel)this.DataContext;
                if (this.viewModel.DropJokeCommand.CanExecute(dropIndex))
                {
                    this.viewModel.DropJokeCommand.Execute(dropIndex);
                }
            }
        }

        private void dataGridJokesInScript_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            var dataGridRow = PetudaVisualTreeHelper.FindAnchestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (dataGridRow != null)
            {
                // Initialize the drag & drop operation
                this.viewModel = (ScriptContentViewModel)this.DataContext;
                if (this.viewModel.StartDragJokeCommand.CanExecute(null))
                    this.viewModel.StartDragJokeCommand.Execute(null);

                var dragData = new DataObject("DraggedJokeFormat", "");
                DragDrop.DoDragDrop(dataGridRow, dragData, DragDropEffects.All);
            }
        }

        #endregion

        // Get row index
        private void DataGridJokesInScript_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Tag = (e.Row.GetIndex() + 1).ToString();
        }

        // Open joke editor window on mouse left doubleclick
        private void DataGridJokesInScript_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.GetPosition(dataGridJokesInScript).Y > 50 &&
                e.ChangedButton == MouseButton.Left &&
                dataGridJokesInScript.SelectedItem != null && 
                this.viewModel.EditJokeCommand.CanExecute(null))
            {
                this.viewModel.EditJokeCommand.Execute(null);
            }
        }

        // Remove joke from script by hitting "delete" button
        private void DataGridJokesInScript_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && 
                this.viewModel != null &&
                this.viewModel.SelectedScript != null &&
                this.viewModel.RemoveJokeFromScriptCommand.CanExecute(null))
            {
                this.viewModel.RemoveJokeFromScriptCommand.Execute(null);
            }
        }
    }//window
}//namespace